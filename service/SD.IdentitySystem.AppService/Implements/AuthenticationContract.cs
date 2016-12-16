using SD.CacheManager;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories;
using SD.IdentitySystem.Domain.Mediators;
using SD.IdentitySystem.IAppService.Interfaces;
using ShSoft.Common.PoweredByLee;
using ShSoft.Infrastructure.Constants;
using ShSoft.ValueObjects.CustomExceptions;
using System;
using System.ServiceModel;
using System.Threading.Tasks;
using User = SD.IdentitySystem.Domain.Entities.User;

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
        /// <param name="repMediator">仓储中介者</param>
        /// <param name="unitOfWork">单元事务</param>
        public AuthenticationContract(RepositoryMediator repMediator, IUnitOfWorkIdentity unitOfWork)
        {
            this._repMediator = repMediator;
            this._unitOfWork = unitOfWork;
        }

        #endregion


        //Implements

        #region # 登录 —— Guid Login(string loginId, string password...
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="ip">IP地址</param>
        /// <returns>公钥</returns>
        public LoginInfo Login(string loginId, string password, string ip)
        {
            lock (_Sync)
            {
                User currentUser = this._repMediator.UserRep.SingleOrDefaultFromCache(loginId);

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

                //生成登录信息
                LoginInfo loginInfo = this.BuildLoginInfo(publicKey, currentUser);

                //以公钥为键，登录信息为值，存入分布式缓存
                CacheMediator.Set(publicKey.ToString(), loginInfo, DateTime.Now.AddMinutes(20));

                //生成登录记录
                Task.Run(() => this.GenerateLoginRecord(publicKey, currentUser, ip)).Wait();

                return loginInfo;
            }
        }
        #endregion

        #region # 认证 —— void Authenticate(Guid publicKey)
        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <returns>是否通过</returns>
        public void Authenticate(Guid publicKey)
        {
            lock (_Sync)
            {
                //以公钥为键，查询分布式缓存，如果有值则通过，无值则不通过
                LoginInfo loginInfo = CacheMediator.Get<LoginInfo>(publicKey.ToString());

                if (loginInfo == null)
                {
                    throw new NoPermissionException("公钥失效，请重新登录！");
                }

                //通过后，重新设置缓存过期时间为20分钟
                CacheMediator.Set(publicKey.ToString(), loginInfo, DateTime.Now.AddMinutes(20));
            }
        }
        #endregion


        //Private

        #region # 构造登录信息 —— LoginInfo BuildLoginInfo(Guid publicKey...
        /// <summary>
        /// 构造登录信息
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="user">用户</param>
        /// <returns>登录信息</returns>
        private LoginInfo BuildLoginInfo(Guid publicKey, User user)
        {
            LoginInfo loginInfo = new LoginInfo(user.Number, user.Name, publicKey);

            return loginInfo;
        }
        #endregion

        #region # 生成登录记录 —— void GenerateLoginRecord(Guid publicKey...
        /// <summary>
        /// 生成登录记录
        /// </summary>
        /// <param name="publicKey">公钥</param>

        /// <param name="user">用户</param>
        /// <param name="ip">IP地址</param>
        private void GenerateLoginRecord(Guid publicKey, User user, string ip)
        {
            //生成记录
            LoginRecord loginRecord = new LoginRecord(publicKey, user.Number, user.Name, ip);

            this._unitOfWork.RegisterAdd(loginRecord);
            this._unitOfWork.Commit();
        }
        #endregion
    }
}
