using SD.CacheManager;
using SD.Common.PoweredByLee;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories;
using SD.IdentitySystem.Domain.Mediators;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.CustomExceptions;
using System;
using System.Configuration;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

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

            if (!string.IsNullOrWhiteSpace(WebConfigSetting.AuthenticationTimeout))
            {
                if (!int.TryParse(WebConfigSetting.AuthenticationTimeout, out _Timeout))
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

        #region # 登录 —— Guid Login(string loginId, string password)
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        /// <returns>公钥</returns>
        public LoginInfo Login(string loginId, string password)
        {
            lock (_Sync)
            {
                /****************验证机器****************/
                this.AuthenticateMachine();

                /****************登录验证****************/
                User currentUser = this._repMediator.UserRep.SingleOrDefault(loginId);

                #region # 验证

                if (currentUser == null)
                {
                    throw new InvalidOperationException(string.Format("用户名\"{0}\"不存在！", loginId));
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

                //生成登录信息，以公钥为键，登录信息为值，存入分布式缓存
                LoginInfo loginInfo = new LoginInfo(currentUser.Number, currentUser.Name, publicKey);
                CacheMediator.Set(publicKey.ToString(), loginInfo, DateTime.Now.AddMinutes(_Timeout));

                //获取客户端IP
                MessageProperties properties = OperationContext.Current.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = (RemoteEndpointMessageProperty)properties[RemoteEndpointMessageProperty.Name];
                string ip = endpoint.Address;

                //生成登录记录
                Task.Run(() => this.GenerateLoginRecord(publicKey, ip, currentUser)).Wait();

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
            //读取配置文件，是否开启服务器机器验证
            string authenticateMachineStr = ConfigurationManager.AppSettings["AuthenticateMachine"];
            bool authenticateMachine;

            if (!bool.TryParse(authenticateMachineStr, out authenticateMachine))
            {
                authenticateMachine = true;
            }
            if (authenticateMachine)
            {
                string machineCode = CommonExtension.GetMachineCode();
                Server currentServer = this._repMediator.ServerRep.SingleOrDefault(machineCode);

                if (currentServer == null)
                {
                    throw new NoPermissionException(string.Format("服务器\"{0}\"未授权！", Dns.GetHostName()));
                }
                if (DateTime.Today > currentServer.ServiceOverDate)
                {
                    throw new NoPermissionException(string.Format("服务器\"{0}\"授权已过期！", currentServer.Name));
                }
            }
        }
        #endregion

        #region # 生成登录记录 —— void GenerateLoginRecord(Guid publicKey, string ip...
        /// <summary>
        /// 生成登录记录
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="ip">IP地址</param>
        /// <param name="user">用户</param>
        private void GenerateLoginRecord(Guid publicKey, string ip, User user)
        {
            //生成记录
            LoginRecord loginRecord = new LoginRecord(publicKey, user.Number, user.Name, ip);

            this._unitOfWork.RegisterAdd(loginRecord);
            this._unitOfWork.Commit();
        }
        #endregion
    }
}
