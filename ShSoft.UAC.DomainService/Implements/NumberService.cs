using System;
using SD.Toolkits.NoGenerator.Facade;
using ShSoft.UAC.Domain.Entities;
using ShSoft.UAC.Domain.IDomainServices;

namespace ShSoft.UAC.DomainService.Implements
{
    /// <summary>
    /// 编号领域服务实现
    /// </summary>
    public class NumberService : INumberService
    {
        #region # 常量、字段及依赖注入构造器

        /// <summary>
        /// 编号生成器
        /// </summary>
        private readonly NumberGenerator _generator;

        /// <summary>
        /// 默认格式化日期
        /// </summary>
        private readonly string _defaultFormatDate;

        /// <summary>
        /// 默认流水号长度
        /// </summary>
        private readonly int _defaultLength;

        /// <summary>
        /// 构造器
        /// </summary>
        public NumberService()
        {
            this._generator = new NumberGenerator();
            this._defaultFormatDate = DateTime.Now.ToString("yyyyMMdd");
            this._defaultLength = 3;
        }

        #endregion
    }
}
