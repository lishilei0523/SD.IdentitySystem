using SD.IdentitySystem.LicenseManager;
using SD.IdentitySystem.LicenseManager.Models;
using SD.IdentitySystem.LicenseManager.Toolkits;
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
            this.Dtp_ServiceExpiredDate.Value = DateTime.Today;
            this.Dtp_LicenseExpiredDate.Value = DateTime.Today;
            this.Dtp_ServiceExpiredDate.MinDate = DateTime.Today;
            this.Dtp_ServiceExpiredDate.MinDate = DateTime.Today;
        }

        /// <summary>
        /// 生成许可证按钮点击事件
        /// </summary>
        private void Btn_CreateLicense_Click(object sender, EventArgs e)
        {
            string enterpriseName = this.Txt_EnterpriseName.Text;
            string uniqueCode = this.Txt_UniqueCode.Text;
            DateTime serviceExpiredDate = this.Dtp_ServiceExpiredDate.Value;
            DateTime licenseExpiredDate = this.Dtp_LicenseExpiredDate.Value;

            if (string.IsNullOrEmpty(enterpriseName))
            {
                MessageBox.Show(@"企业名称不可为空！", @"警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(uniqueCode))
            {
                MessageBox.Show(@"唯一码不可为空！", @"警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            License license = new License(enterpriseName, uniqueCode, serviceExpiredDate, licenseExpiredDate);
            this.CreateLicenseFile(license);

            MessageBox.Show(@"生成成功", @"OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 加载本机机器码按钮点击事件
        /// </summary>
        private void Btn_LoadLocalMachine_Click(object sender, EventArgs e)
        {
            string uniqueCode = UniqueCode.Compute();
            this.Txt_UniqueCode.Text = uniqueCode;
        }

        /// <summary>
        /// 打开按钮点击事件
        /// </summary>
        private void Btn_OpenLicense_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog { Filter = @"License files (*.key)|*.key" };
            dialog.ShowDialog();

            string licenseFileName = dialog.FileName;

            if (!string.IsNullOrEmpty(licenseFileName))
            {
                License? license = LicenseReader.GetLicense(licenseFileName);

                if (license.HasValue)
                {
                    this.Txt_EnterpriseName.Text = license.Value.EnterpriseName;
                    this.Txt_UniqueCode.Text = license.Value.UniqueCode;
                    this.Dtp_ServiceExpiredDate.Value = license.Value.ServiceExpiredDate;
                    this.Dtp_LicenseExpiredDate.Value = license.Value.LicenseExpiredDate;
                }
            }
        }

        /// <summary>
        /// 创建许可证文件
        /// </summary>
        private void CreateLicenseFile(License license)
        {
            string binaryString = license.ToBinaryString();
            string aesString = binaryString.Encrypt();
            byte[] buffer = aesString.ToBuffer();

            using (FileStream fileStream = new FileStream(Constants.LicenseFileName, FileMode.Create))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                {
                    binaryWriter.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
