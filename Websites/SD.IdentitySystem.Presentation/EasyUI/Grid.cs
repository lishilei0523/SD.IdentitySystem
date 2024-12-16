using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Presentation.EasyUI
{
    /// <summary>
    /// EasyUI表格模型
    /// </summary>
    public class Grid<T>
    {
        #region # 构造器

        /// <summary>
        /// 无参构造器
        /// </summary>
        public Grid()
        {
            this.rows = new HashSet<T>();
        }

        /// <summary>
        /// 创建EasyUI表格模型构造器
        /// </summary>
        /// <param name="total">总记录条数</param>
        /// <param name="rows">数据集</param>
        public Grid(int total, IEnumerable<T> rows)
            : this()
        {
            rows = rows?.ToArray() ?? new T[0];

            this.total = total;
            foreach (T row in rows)
            {
                this.rows.Add(row);
            }
        }

        #endregion

        #region # 属性

        #region 总记录条数 —— int total
        /// <summary>
        /// 总记录条数
        /// </summary>
        public int total { get; set; }
        #endregion

        #region 数据集 —— ICollection<T> rows
        /// <summary>
        /// 数据集
        /// </summary>
        public ICollection<T> rows { get; set; }
        #endregion

        #endregion
    }
}
