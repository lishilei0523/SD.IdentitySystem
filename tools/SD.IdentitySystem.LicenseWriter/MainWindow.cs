using SD.IdentitySystem.Tookits;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using System;
using System.IO;
using System.Windows.Forms;

namespace SD.IdentitySystem.LicenseWriter
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class MainWindow : Form
    {
        /// <summary>
        /// 无参构造器
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            //默认值
            this.Dtp_ExpiredDate.Value = DateTime.Today;
            this.Dtp_ExpiredDate.MinDate = DateTime.Today;
        }

        /// <summary>
        /// 生成许可证按钮点击事件
        /// </summary>
        private void Btn_CreateLicense_Click(object sender, System.EventArgs e)
        {
            string enterpriseName = this.Txt_EnterpriseName.Text;
            string uniqueCode = this.Txt_UniqueCode.Text;
            DateTime expiredDate = this.Dtp_ExpiredDate.Value;

            if (string.IsNullOrWhiteSpace(enterpriseName))
            {
                MessageBox.Show(@"企业名称不可为空！", @"警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(uniqueCode))
            {
                MessageBox.Show(@"唯一码不可为空！", @"警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            License license = new License(enterpriseName, uniqueCode, expiredDate);
            this.CreateLicenseFile(license);

            MessageBox.Show(@"生成成功", @"OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 创建许可证文件
        /// </summary>
        private void CreateLicenseFile(License license)
        {
            string binaryString = license.ToBinaryString();
            string aesString = binaryString.Encrypt();
            byte[] buffer = aesString.ToBuffer();

            using (FileStream fileStream = new FileStream(CommonConstants.LicenseFileName, FileMode.Create))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                {
                    binaryWriter.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
