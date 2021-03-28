using SD.CacheManager;
using SD.Common;
using SD.IdentitySystem.AppService.Maps;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories;
using SD.IdentitySystem.Domain.Mediators;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.LicenseManager.Models;
using SD.IdentitySystem.LicenseManager.Toolkits;
using SD.Infrastructure.Constants;
using SD.Infrastructure.CustomExceptions;
using SD.Infrastructure.MemberShip;
using SD.Toolkits.Recursion.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
#if NETFX
using System.ServiceModel;
using System.ServiceModel.Channels;
#endif
#if NETCORE
using CoreWCF;
using SD.Toolkits.OwinCore.Extensions;
#endif


namespace SD.IdentitySystem.AppService.Implements
{
    /// <summary>
    /// 身份认证服务契约实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class AuthenticationContract : IAuthenticationContract
    {
        #region # 字段及依赖注入构造器

        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _Sync;

        /// <summary>
        /// 身份过期时间
        /// </summary>
        private static readonly int _Timeout;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static AuthenticationContract()
        {
            _Sync = new object();

            if (!string.IsNullOrWhiteSpace(GlobalSetting.AuthenticationTimeout))
            {
                if (!int.TryParse(GlobalSetting.AuthenticationTimeout, out _Timeout))
                {
                    //默认20分钟
                    _Timeout = 20;
                }
            }
            else
            {
                //默认20分钟
                _Timeout = 20;
            }
        }


        /// <summary>
        /// 仓储中介者
        /// </summary>
        private readonly RepositoryMediator _repMediator;

        /// <summary>
        /// 单元事务
        /// </summary>
        private readonly IUnitOfWorkIdentity _unitOfWork;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="repMediator">仓储中介者</param>
        /// <param name="unitOfWork">单元事务</param>
        public AuthenticationContract(RepositoryMediator repMediator, IUnitOfWorkIdentity unitOfWork)
        {
            this._repMediator = repMediator;
            this._unitOfWork = unitOfWork;
        }

        #endregion


        //Implements

        #region # 登录 —— LoginInfo Logon(string privateKey)
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <returns>登录信息</returns>
        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public LoginInfo Logon(string privateKey)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(privateKey))
            {
                throw new ArgumentNullException(nameof(privateKey), "私钥不可为空！");
            }

            #endregion

            lock (_Sync)
            {
                //验证许可证
                this.CheckLicense();

                //验证登录
                User currentUser = this._repMediator.UserRep.SingleByPrivateKey(privateKey);

                #region # 验证

                if (currentUser == null)
                {
                    throw new InvalidOperationException("私钥不存在！");
                }
                if (!currentUser.Enabled)
                {
                    throw new InvalidOperationException("用户已停用！");
                }

                #endregion

                LoginInfo loginInfo = this.BuildLoginInfo(currentUser);

                return loginInfo;
            }
        }
        #endregion

        #region # 登录 —— LoginInfo Login(string loginId, string password)
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        /// <returns>登录信息</returns>
        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public LoginInfo Login(string loginId, string password)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(loginId))
            {
                throw new ArgumentNullException(nameof(loginId), "用户名不可为空！");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password), "密码不可为空！");
            }

            #endregion

            lock (_Sync)
            {
                //验证许可证
                this.CheckLicense();

                //验证登录
                User currentUser = this._repMediator.UserRep.SingleOrDefault(loginId);

                #region # 验证

                if (currentUser == null)
                {
                    throw new InvalidOperationException("用户不存在！");
                }
                if (!currentUser.Enabled)
                {
                    throw new InvalidOperationException("用户已停用！");
                }
                if (currentUser.Password != password.ToMD5())
                {
                    throw new InvalidOperationException("登录失败，密码错误！");
                }

                #endregion

                LoginInfo loginInfo = this.BuildLoginInfo(currentUser);

                return loginInfo;
            }
        }
        #endregion


        //Private

        #region # 验证许可证 —— void CheckLicense()
        /// <summary>
        /// 验证许可证
        /// </summary>
        private void CheckLicense()
        {
            bool authenticateMachine = true;
#if DEBUG
            authenticateMachine = false;
#endif
            if (authenticateMachine)
            {
                License? license = LicenseReader.GetLicense();

                if (license == null)
                {
                    throw new NoPermissionException("未找到许可证，请联系系统管理员！");
                }

                string uniqueCode = UniqueCode.Compute();
                bool equal = string.Equals(license.Value.UniqueCode, uniqueCode, StringComparison.CurrentCultureIgnoreCase);

                if (!equal)
                {
                    throw new NoPermissionException("许可证授权与本机不匹配，请联系系统管理员！");
                }
                if (DateTime.Today > license.Value.LicenseExpiredDate)
                {
                    throw new NoPermissionException("许可证授权已过期，请联系系统管理员！");
                }
            }
        }
        #endregion

        #region # 构造登录信息 —— LoginInfo BuildLoginInfo(User user)
        /// <summary>
        /// 构造登录信息
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns>登录信息</returns>
        private LoginInfo BuildLoginInfo(User user)
        {
            //生成公钥
            Guid publicKey = Guid.NewGuid();

            //生成登录信息
            LoginInfo loginInfo = new LoginInfo(user.Number, user.Name, publicKey);

            #region # 登录信息的信息系统部分/菜单部分/权限部分

            ICollection<Guid> roleIds = this._repMediator.RoleRep.FindIds(user.Number, null);

            /*信息系统部分*/
            IEnumerable<string> systemNos = user.GetInfoSystemNos();
            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.Find(systemNos);
            loginInfo.LoginSystemInfos.AddRange(systems.Values.Select(x => x.ToLoginSystemInfo()));

            /*菜单部分*/
            IEnumerable<Guid> authorityIds = this._repMediator.AuthorityRep.FindIdsByRole(roleIds);
            IEnumerable<Menu> menus = this._repMediator.MenuRep.FindByAuthority(authorityIds, null);
            menus = menus.TailRecurseParentNodes();
            ICollection<LoginMenuInfo> menuTree = menus.ToTree(null);
            loginInfo.LoginMenuInfos.AddRange(menuTree);

            /*权限部分*/
            IEnumerable<Authority> authorities = this._repMediator.AuthorityRep.FindByRole(roleIds);
            loginInfo.LoginAuthorityInfos = authorities.GroupBy(x => x.SystemNo).ToDictionary(x => x.Key, x => x.Select(y => y.ToLoginAuthorityInfo()).ToArray());

            #endregion

            //以公钥为键，登录信息为值，存入分布式缓存
            CacheMediator.Set(publicKey.ToString(), loginInfo, DateTime.Now.AddMinutes(_Timeout));

            //获取客户端IP
            string ip = "localhost";
#if NETFX
            if (OperationContext.Current != null)
            {
                MessageProperties messageProperties = OperationContext.Current.IncomingMessageProperties;
                if (messageProperties.ContainsKey(RemoteEndpointMessageProperty.Name))
                {
                    object messageProperty = messageProperties[RemoteEndpointMessageProperty.Name];
                    RemoteEndpointMessageProperty remoteEndpointMessageProperty = (RemoteEndpointMessageProperty)messageProperty;
                    ip = remoteEndpointMessageProperty.Address;
                }
            }
#endif
#if NETCORE
            if (OwinContextReader.Current != null)
            {
                ip = OwinContextReader.Current.Connection.RemoteIpAddress.ToString();
            }
#endif
            //生成登录记录
            LoginRecord loginRecord = new LoginRecord(publicKey, user.Number, user.Name, ip);

            this._unitOfWork.RegisterAdd(loginRecord);
            this._unitOfWork.Commit();

            return loginInfo;
        }
        #endregion
    }
}
