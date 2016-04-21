using System.Data.Entity;
using System.Linq;
using System.Transactions;
using SD.AOP.Core.Aspects.ForMethod;
using SD.UAC.Domain.Entities;
using ShSoft.Framework2015.Infrastructure.ValueObjects;
using ShSoft.Framework2016.Infrastructure.IRepository;

namespace SD.UAC.Repository.Base
{
    /// <summary>
    /// 数据初始化器实现
    /// </summary>
    public class DataInitializer : IDataInitializer
    {
        /// <summary>
        /// EF上下文对象
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// 构造器
        /// </summary>
        public DataInitializer()
        {
            this._dbContext = DbSession.CommandInstance;
            //sadasd
        }

        /// <summary>
        /// 构造器
        /// </summary>
        public DataInitializer(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <summary>
        /// 初始化基础数据
        /// </summary>
        [TransactionAspect(TransactionScopeOption.RequiresNew)]
        public void Initialize()
        {
            this.InitInfoSystemKind();
            this.InitInfoSystem();
            this.InitAdmin();
        }

        /// <summary>
        /// 初始化信息系统类别
        /// </summary>
        private void InitInfoSystemKind()
        {
            if (!this._dbContext.Set<InfoSystemKind>().Any())
            {
                InfoSystemKind manageCenterKind = new InfoSystemKind(Constants.MCSystemKindNo, "管理中心系统类别");
                InfoSystemKind supplierKind = new InfoSystemKind(Constants.SupplierSystemKindNo, "供应商系统类别");

                this._dbContext.Set<InfoSystemKind>().Add(manageCenterKind);
                this._dbContext.Set<InfoSystemKind>().Add(supplierKind);

                this._dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 初始化信息系统
        /// </summary>
        private void InitInfoSystem()
        {
            if (!this._dbContext.Set<InfoSystem>().Any())
            {
                InfoSystem manageCenter = new InfoSystem(InfoSystem.ManageCenterSysName, Constants.MCSystemNo, Constants.MCSystemKindNo);

                this._dbContext.Set<InfoSystem>().Add(manageCenter);

                this._dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 初始化超级管理员
        /// </summary>
        private void InitAdmin()
        {
            if (!this._dbContext.Set<User>().Any())
            {
                //获取管理中心信息系统
                InfoSystem manageCenter = this._dbContext.Set<InfoSystem>().Single(x => x.Number == Constants.MCSystemNo);

                //创建用户
                User adminUser = new User(Constants.AdminLoginId, User.InitialPassword);

                //为管理中心设置超级管理员
                manageCenter.SetAdmin(Constants.AdminLoginId);

                //创建角色
                Role adminRole = new Role(InfoSystem.AdminRoleName, InfoSystem.AdminRoleName);
                manageCenter.CreateRole(adminRole);

                //设置角色Id
                adminRole.SetId(Constants.AdminRoleId);


                //为用户设置角色
                adminUser.SetRoles(new[] { adminRole });

                this._dbContext.Set<User>().Add(adminUser);

                this._dbContext.SaveChanges();
            }
        }
    }
}
