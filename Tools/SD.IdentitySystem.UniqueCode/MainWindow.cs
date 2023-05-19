using System;
using System.Windows.Forms;

namespace SD.IdentitySystem.UniqueCode
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class MainWindow : Form
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 计算机器唯一码
        /// </summary>
        private void Btn_CalculateMachineCode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Txt_MachineCode.Text?.Trim()))
            {
                string machineCode = LicenseManager.Toolkits.UniqueCode.Compute();
                this.Txt_MachineCode.Text = machineCode;
            }
        }

        /// <summary>
        /// 复制机器唯一码
        /// </summary>
        private void Btn_Copy_Click(object sender, EventArgs e)
        {
            #region # 验证

            if (string.IsNullOrEmpty(this.Txt_MachineCode.Text))
            {
                MessageBox.Show(@"请先计算再复制！", @"Warning");
                return;
            }

            #endregion

            Clipboard.SetDataObject(this.Txt_MachineCode.Text);
            MessageBox.Show(@"复制成功", @"OK");
        }
    }
}
