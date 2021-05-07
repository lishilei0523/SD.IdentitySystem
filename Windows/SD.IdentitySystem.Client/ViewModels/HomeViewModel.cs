using Caliburn.Micro;
using SD.Infrastructure.WPF.Aspects;
using System.Diagnostics;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels
{
    /// <summary>
    /// 首页视图模型
    /// </summary>
    public class HomeViewModel : Screen
    {
        #region # 字段及构造器

        //TODO 依赖注入构造器

        #endregion

        #region # 属性

        #region 编号 —— string Number
        /// <summary>
        /// 编号
        /// </summary>
        [DependencyProperty]
        public string Number { get; set; }
        #endregion

        #region 名称 —— string Name
        /// <summary>
        /// 名称
        /// </summary>
        [DependencyProperty]
        public string Name { get; set; }
        #endregion

        #region 描述 —— string Description
        /// <summary>
        /// 描述
        /// </summary>
        [DependencyProperty]
        public string Description { get; set; }
        #endregion

        #endregion

        #region # 方法

        #region 赋值 —— void Fill()
        /// <summary>
        /// 赋值
        /// </summary>
        public void Fill()
        {
            this.Number = "编号";
            this.Name = "名称";
            this.Description = "描述";
        }
        #endregion 

        #region 提交 —— void Submit()
        /// <summary>
        /// 提交
        /// </summary>
        public void Submit()
        {
            Trace.WriteLine(this.Number);
            Trace.WriteLine(this.Name);
            Trace.WriteLine(this.Description);

            MessageBox.Show(this.Number);
            MessageBox.Show(this.Name);
            MessageBox.Show(this.Description);
        }
        #endregion 

        #endregion
    }
}
