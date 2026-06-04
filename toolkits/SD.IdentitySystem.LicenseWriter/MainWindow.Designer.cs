namespace SD.IdentitySystem.LicenseWriter
{
    partial class MainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.label3 = new System.Windows.Forms.Label();
            this.Txt_UniqueCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Txt_EnterpriseName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Dtp_ServiceExpiredDate = new System.Windows.Forms.DateTimePicker();
            this.Btn_CreateLicense = new System.Windows.Forms.Button();
            this.Btn_LoadLocalMachine = new System.Windows.Forms.Button();
            this.Dtp_LicenseExpiredDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.Btn_OpenLicense = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(44, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 32;
            this.label3.Text = "机器唯一码：";
            // 
            // Txt_UniqueCode
            // 
            this.Txt_UniqueCode.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_UniqueCode.Location = new System.Drawing.Point(131, 88);
            this.Txt_UniqueCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Txt_UniqueCode.Name = "Txt_UniqueCode";
            this.Txt_UniqueCode.Size = new System.Drawing.Size(270, 24);
            this.Txt_UniqueCode.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(56, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 34;
            this.label1.Text = "企业名称：";
            // 
            // Txt_EnterpriseName
            // 
            this.Txt_EnterpriseName.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_EnterpriseName.Location = new System.Drawing.Point(131, 43);
            this.Txt_EnterpriseName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Txt_EnterpriseName.Name = "Txt_EnterpriseName";
            this.Txt_EnterpriseName.Size = new System.Drawing.Size(270, 24);
            this.Txt_EnterpriseName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(32, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 36;
            this.label2.Text = "服务过期日期：";
            // 
            // Dtp_ServiceExpiredDate
            // 
            this.Dtp_ServiceExpiredDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Dtp_ServiceExpiredDate.CustomFormat = "yyyy年 MM月 dd日";
            this.Dtp_ServiceExpiredDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_ServiceExpiredDate.Location = new System.Drawing.Point(131, 133);
            this.Dtp_ServiceExpiredDate.MaxDate = new System.DateTime(2078, 6, 6, 0, 0, 0, 0);
            this.Dtp_ServiceExpiredDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.Dtp_ServiceExpiredDate.Name = "Dtp_ServiceExpiredDate";
            this.Dtp_ServiceExpiredDate.Size = new System.Drawing.Size(270, 21);
            this.Dtp_ServiceExpiredDate.TabIndex = 4;
            this.Dtp_ServiceExpiredDate.Value = new System.DateTime(2018, 5, 9, 0, 0, 0, 0);
            // 
            // Btn_CreateLicense
            // 
            this.Btn_CreateLicense.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Btn_CreateLicense.Location = new System.Drawing.Point(407, 306);
            this.Btn_CreateLicense.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Btn_CreateLicense.Name = "Btn_CreateLicense";
            this.Btn_CreateLicense.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Btn_CreateLicense.Size = new System.Drawing.Size(85, 28);
            this.Btn_CreateLicense.TabIndex = 6;
            this.Btn_CreateLicense.Text = "生成许可证";
            this.Btn_CreateLicense.UseVisualStyleBackColor = true;
            this.Btn_CreateLicense.Click += new System.EventHandler(this.Btn_CreateLicense_Click);
            // 
            // Btn_LoadLocalMachine
            // 
            this.Btn_LoadLocalMachine.Location = new System.Drawing.Point(407, 86);
            this.Btn_LoadLocalMachine.Name = "Btn_LoadLocalMachine";
            this.Btn_LoadLocalMachine.Size = new System.Drawing.Size(85, 28);
            this.Btn_LoadLocalMachine.TabIndex = 3;
            this.Btn_LoadLocalMachine.Text = "加载本机";
            this.Btn_LoadLocalMachine.UseVisualStyleBackColor = true;
            this.Btn_LoadLocalMachine.Click += new System.EventHandler(this.Btn_LoadLocalMachine_Click);
            // 
            // Dtp_LicenseExpiredDate
            // 
            this.Dtp_LicenseExpiredDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Dtp_LicenseExpiredDate.CustomFormat = "yyyy年 MM月 dd日";
            this.Dtp_LicenseExpiredDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_LicenseExpiredDate.Location = new System.Drawing.Point(131, 175);
            this.Dtp_LicenseExpiredDate.MaxDate = new System.DateTime(2078, 6, 6, 0, 0, 0, 0);
            this.Dtp_LicenseExpiredDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.Dtp_LicenseExpiredDate.Name = "Dtp_LicenseExpiredDate";
            this.Dtp_LicenseExpiredDate.Size = new System.Drawing.Size(270, 21);
            this.Dtp_LicenseExpiredDate.TabIndex = 5;
            this.Dtp_LicenseExpiredDate.Value = new System.DateTime(2018, 5, 9, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(33, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 17);
            this.label4.TabIndex = 39;
            this.label4.Text = "授权过期日期：";
            // 
            // Btn_OpenLicense
            // 
            this.Btn_OpenLicense.Location = new System.Drawing.Point(407, 41);
            this.Btn_OpenLicense.Name = "Btn_OpenLicense";
            this.Btn_OpenLicense.Size = new System.Drawing.Size(85, 28);
            this.Btn_OpenLicense.TabIndex = 1;
            this.Btn_OpenLicense.Text = "打开证书";
            this.Btn_OpenLicense.UseVisualStyleBackColor = true;
            this.Btn_OpenLicense.Click += new System.EventHandler(this.Btn_OpenLicense_Click);
            // 
            // MainWindow
            // 
            this.AcceptButton = this.Btn_CreateLicense;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 347);
            this.Controls.Add(this.Btn_OpenLicense);
            this.Controls.Add(this.Dtp_LicenseExpiredDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Btn_LoadLocalMachine);
            this.Controls.Add(this.Btn_CreateLicense);
            this.Controls.Add(this.Dtp_ServiceExpiredDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Txt_EnterpriseName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Txt_UniqueCode);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "身份认证系统 - 许可证生成工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Txt_UniqueCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Txt_EnterpriseName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker Dtp_ServiceExpiredDate;
        private System.Windows.Forms.Button Btn_CreateLicense;
        private System.Windows.Forms.Button Btn_LoadLocalMachine;
        private System.Windows.Forms.DateTimePicker Dtp_LicenseExpiredDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Btn_OpenLicense;
    }
}

