using SD.CacheManager;
using SD.Common;
using SD.IdentitySystem.AppService.Maps;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories;
using SD.IdentitySystem.Domain.Mediators;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.LicenseManager.Models;
using SD.IdentitySystem.LicenseManager.Tookits;
using SD.Infrastructure.Constants;
using SD.Infrastructure.CustomExceptions;
using SD.Infrastructure.MemberShip;
using SD.Toolkits.Recursion.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;

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

        #region # 登录 —— LoginInfo Login(string loginId, string password)
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        /// <returns>登录信息</returns>
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
                /****************验证机器****************/
                this.AuthenticateMachine();

                /****************登录验证****************/
                User currentUser = this._repMediator.UserRep.SingleOrDefault(loginId);

                #region # 验证

                if (currentUser == null)
                {
                    throw new InvalidOperationException($"用户名\"{loginId}\"不存在！");
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

                //生成公钥
                Guid publicKey = Guid.NewGuid();

                //生成登录信息
                LoginInfo loginInfo = new LoginInfo(currentUser.Number, currentUser.Name, publicKey);

                #region # 登录信息的信息系统部分/菜单部分/权限部分

                ICollection<Guid> roleIds = this._repMediator.RoleRep.FindIds(loginId, null);

                /*信息系统部分*/
                IEnumerable<string> systemNos = currentUser.GetInfoSystemNos();
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
                MessageProperties properties = OperationContext.Current.IncomingMessageProperties;

                string ip = "localhost";

                if (properties.ContainsKey(RemoteEndpointMessageProperty.Name))
                {
                    RemoteEndpointMessageProperty endpoint = (RemoteEndpointMessageProperty)properties[RemoteEndpointMessageProperty.Name];
                    ip = endpoint.Address;
                }

                //生成登录记录
                this.GenerateLoginRecord(publicKey, ip, currentUser.Number, currentUser.Name);

                return loginInfo;
            }
        }
        #endregion


        //Private

        #region # 验证服务器机器 —— void AuthenticateMachine()
        /// <summary>
        /// 验证服务器机器
        /// </summary>
        private void AuthenticateMachine()
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

        #region # 生成登录记录 —— void GenerateLoginRecord(Guid publicKey, string ip....
        /// <summary>
        /// 生成登录记录
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="ip">IP地址</param>
        /// <param name="loginId">登录名</param>
        /// <param name="realName">真实姓名</param>
        private void GenerateLoginRecord(Guid publicKey, string ip, string loginId, string realName)
        {
            //生成记录
            LoginRecord loginRecord = new LoginRecord(publicKey, loginId, realName, ip);

            this._unitOfWork.RegisterAdd(loginRecord);
            this._unitOfWork.Commit();
        }
        #endregion
    }
}
