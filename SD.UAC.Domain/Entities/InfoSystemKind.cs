using ShSoft.Infrastructure.EntityBase;

namespace SD.UAC.Domain.Entities
{
    /// <summary>
    /// 信息系统类别（字典）
    /// </summary>
    public class InfoSystemKind : AggregateRootEntity
    {
        #region # 构造器

        #region 01.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected InfoSystemKind() { }
        #endregion

        #region 02.创建系统类别构造器
        /// <summary>
        /// 创建系统类别构造器
        /// </summary>
        /// <param name="kindNo">类别编号</param>
        /// <param name="kindName">类别名称</param>
        public InfoSystemKind(string kindNo, string kindName)
            : this()
        {
            //验证参数
            base.CheckNumber(kindNo);
            base.CheckName(kindName);

            base.Number = kindNo;
            base.Name = kindName;
        }
        #endregion

        #endregion
    }
}
