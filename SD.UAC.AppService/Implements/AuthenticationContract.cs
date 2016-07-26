using System;
using System.ServiceModel;
using SD.CacheManager;
using SD.UAC.Common.CustomExceptions;
using SD.UAC.Domain.Entities;
using SD.UAC.Domain.IRepositories;
using SD.UAC.Domain.Mediators;
using SD.UAC.IAppService.Interfaces;
using ShSoft.Common.PoweredByLee;

namespace SD.UAC.AppService.Implements
{
    /// <summary>
    /// 身份认证服务契约实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class AuthenticationContract : IAuthenticationContract
    {
        #region # 字段及依赖注入构造器

        /// <summary>
        /// 领域服务中介者
        /// </summary>
        private readonly DomainServiceMediator _svcMediator;

        /// <summary>
        /// 仓储中介者
        /// </summary>
        private readonly RepositoryMediator _repMediator;

        /// <summary>
        /// 单元事务
        /// </summary>
        private readonly IUnitOfWorkUAC _unitOfWork;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="svcMediator">领域服务中介者</param>
        /// <param name="repMediator">仓储中介者</param>
        /// <param name="unitOfWork">单元事务</param>
        public AuthenticationContract(DomainServiceMediator svcMediator, RepositoryMediator repMediator, IUnitOfWorkUAC unitOfWork)
        {
            this._svcMediator = svcMediator;
            this._repMediator = repMediator;
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region # 登录 —— Guid Login(string loginId, string password)
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        /// <returns>公钥</returns>
        public Guid Login(string loginId, string password)
        {
            //验证
            Assert.IsTrue(this._repMediator.UserRep.Exists(loginId), string.Format("用户名\"{0}\"不存在！", loginId));

            User currentUser = this._unitOfWork.Resolve<User>(loginId);
            currentUser.Login(password);

            //生成公钥，以公钥为键，用户信息为值，存入分布式缓存
            Guid publicKey = Guid.NewGuid();
            CacheMediator.Set(publicKey.ToString(), loginId, DateTime.Now.AddMinutes(20));

            return publicKey;
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
            //以公钥为键，查询分布式缓存，如果有值则通过，无值则不通过
            object userInfo = CacheMediator.Get<object>(publicKey.ToString());

            if (userInfo == null)
            {
                throw new NoPermissionException("公钥失效，请重新登录！");
            }

            //通过后，重新设置缓存过期时间为20分钟
            CacheMediator.Set(publicKey.ToString(), userInfo, DateTime.Now.AddMinutes(20));
        }
        #endregion
    }
}
