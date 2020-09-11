using SD.IdentitySystem.Domain.IDomainServices;
using SD.Toolkits.SerialNumber.Mediators;

namespace SD.IdentitySystem.DomainService.Implements
{
    /// <summary>
    /// 编号领域服务实现
    /// </summary>
    public class NumberService : INumberService
    {
        #region # 常量、字段及依赖注入构造器

        /// <summary>
        /// 序列号生成器
        /// </summary>
        private readonly Keygen _keygen;

        /// <summary>
        /// 默认格式化日期
        /// </summary>
        private readonly string _defaultTimeFormat;

        /// <summary>
        /// 默认流水长度
        /// </summary>
        private readonly int _defaultSerialLength;

        /// <summary>
        /// 构造器
        /// </summary>
        public NumberService()
        {
            this._keygen = new Keygen();
            this._defaultTimeFormat = "yyyyMMdd";
            this._defaultSerialLength = 3;
        }

        #endregion
    }
}
