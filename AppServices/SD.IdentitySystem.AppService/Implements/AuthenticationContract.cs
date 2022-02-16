using SD.CacheManager;
using SD.Common;
using SD.IdentitySystem.AppService.Maps;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories;
using SD.IdentitySystem.Domain.Mediators;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using SD.Toolkits.Recursion.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
#if NET40_OR_GREATER
using SD.Toolkits.Owin.Extensions;
using System.ServiceModel;
using System.ServiceModel.Channels;
#endif
#if NETSTANDARD2_0_OR_GREATER
using SD.Toolkits.OwinCore.Extensions;
using CoreWCF;
using CoreWCF.Channels;
#endif

namespace SD.IdentitySystem.AppService.Implements
{
    /// <summary>
    /// 身份认证服务契约实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
    public class AuthenticationContract : IAuthenticationContract
    {
        #region # 字段及依赖注入构造器

        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _Sync = new object();

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
            #region # 验证

            if (string.IsNullOrWhiteSpace(privateKey))
            {
                throw new ArgumentNullException(nameof(privateKey), "私钥不可为空！");
            }

            #endregion

            lock (_Sync)
            {
                //验证登录
                User user = this._repMediator.UserRep.SingleByPrivateKey(privateKey);

                #region # 验证

                if (user == null)
                {
                    throw new InvalidOperationException("私钥不存在！");
                }
                if (!user.Enabled)
                {
                    throw new InvalidOperationException("用户已停用！");
                }

                #endregion

                LoginInfo loginInfo = this.BuildLoginInfo(user);

                return loginInfo;
            }
        }
        #endregion

        #region # 登录 —— LoginInfo Login(string loginId, string password)
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>登录信息</returns>
        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public LoginInfo Login(string loginId, string password)
        {
            #region # 验证

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
                //验证登录
                User user = this._repMediator.UserRep.SingleFully(loginId);

                #region # 验证

                if (user == null)
                {
                    throw new InvalidOperationException("用户不存在！");
                }
                if (!user.Enabled)
                {
                    throw new InvalidOperationException("用户已停用！");
                }
                if (user.Password != password.ToMD5())
                {
                    throw new InvalidOperationException("登录失败，密码错误！");
                }

                #endregion

                LoginInfo loginInfo = this.BuildLoginInfo(user);

                return loginInfo;
            }
        }
        #endregion


        //Private

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

            /*角色部分*/
            ICollection<Guid> roleIds = user.GetRelatedRoleIds();

            /*信息系统部分*/
            IEnumerable<string> infoSystemNos = user.GetRelatedInfoSystemNos();
            IDictionary<string, InfoSystem> infoSystems = this._repMediator.InfoSystemRep.Find(infoSystemNos);
            loginInfo.LoginSystemInfos = infoSystems.Values.Select(x => x.ToLoginSystemInfo()).ToList();

            /*权限部分*/
            IEnumerable<Authority> authorities = this._repMediator.AuthorityRep.FindByRole(roleIds);
            loginInfo.LoginAuthorityInfos = authorities.Select(x => x.ToLoginAuthorityInfo()).ToList();

            /*菜单部分*/
            IEnumerable<Guid> authorityIds = authorities.Select(x => x.Id);
            IEnumerable<Menu> menus = this._repMediator.MenuRep.FindByAuthority(authorityIds, null);
            menus = menus.TailRecurseParentNodes();
            loginInfo.LoginMenuInfos = menus.ToLoginMenuInfoTree(null);

            #endregion

            //以公钥为键，登录信息为值，存入分布式缓存
            CacheMediator.Set(publicKey.ToString(), loginInfo, DateTime.Now.AddMinutes(GlobalSetting.AuthenticationTimeout));

            //获取客户端IP
            string ip = this.GetClientIp();

            //生成登录记录
            LoginRecord loginRecord = new LoginRecord(publicKey, user.Number, user.Name, ip);

            this._unitOfWork.RegisterAdd(loginRecord);
            this._unitOfWork.Commit();

            return loginInfo;
        }
        #endregion

        #region # 获取客户端IP地址 —— string GetClientIp()
        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns>客户端IP地址</returns>
        private string GetClientIp()
        {
            string ip = null;

            #region # WCF获取

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
            if (!string.IsNullOrWhiteSpace(ip))
            {
                return ip;
            }

            #endregion

            #region # WebApi获取

#if NET45_OR_GREATER
            if (OwinContextReader.Current != null)
            {
                ip = OwinContextReader.Current.Request.RemoteIpAddress;
            }
            if (!string.IsNullOrWhiteSpace(ip))
            {
                return ip;
            }
#endif
#if NETSTANDARD2_0_OR_GREATER
            if (OwinContextReader.Current != null)
            {
                ip = OwinContextReader.Current.Connection.RemoteIpAddress.ToString();
            }
            if (!string.IsNullOrWhiteSpace(ip))
            {
                return ip;
            }
#endif
            #endregion

            #region # 本机

            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = "localhost";
            }

            #endregion

            return ip;
        }
        #endregion
    }
}
