using SD.Common.PoweredByLee;
using System;
using System.Windows.Forms;

namespace SD.IdentitySystem.MachineCodeTool
{
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
            if (string.IsNullOrWhiteSpace(this.Txt_MachineCode.Text))
            {
                string machineCode = CommonExtension.GetMachineCode();
                this.Txt_MachineCode.Text = machineCode;
            }
        }

        /// <summary>
        /// 复制机器唯一码
        /// </summary>
        private void Btn_Copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.Txt_MachineCode.Text);
        }
    }
}
