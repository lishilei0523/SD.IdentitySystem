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
            this.label3 = new System.Windows.Forms.Label();
            this.Txt_UniqueCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Txt_EnterpriseName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Dtp_ExpiredDate = new System.Windows.Forms.DateTimePicker();
            this.Btn_CreateLicense = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(60, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 32;
            this.label3.Text = "机器唯一码：";
            // 
            // Txt_UniqueCode
            // 
            this.Txt_UniqueCode.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_UniqueCode.Location = new System.Drawing.Point(147, 99);
            this.Txt_UniqueCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Txt_UniqueCode.Name = "Txt_UniqueCode";
            this.Txt_UniqueCode.Size = new System.Drawing.Size(270, 24);
            this.Txt_UniqueCode.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(60, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 34;
            this.label1.Text = "企业名称：";
            // 
            // Txt_EnterpriseName
            // 
            this.Txt_EnterpriseName.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_EnterpriseName.Location = new System.Drawing.Point(147, 55);
            this.Txt_EnterpriseName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Txt_EnterpriseName.Name = "Txt_EnterpriseName";
            this.Txt_EnterpriseName.Size = new System.Drawing.Size(270, 24);
            this.Txt_EnterpriseName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(60, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 36;
            this.label2.Text = "过期日期：";
            // 
            // Dtp_ExpiredDate
            // 
            this.Dtp_ExpiredDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Dtp_ExpiredDate.CustomFormat = "yyyy年 MM月 dd日";
            this.Dtp_ExpiredDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_ExpiredDate.Location = new System.Drawing.Point(147, 143);
            this.Dtp_ExpiredDate.MaxDate = new System.DateTime(2078, 6, 6, 0, 0, 0, 0);
            this.Dtp_ExpiredDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.Dtp_ExpiredDate.Name = "Dtp_ExpiredDate";
            this.Dtp_ExpiredDate.Size = new System.Drawing.Size(270, 21);
            this.Dtp_ExpiredDate.TabIndex = 2;
            this.Dtp_ExpiredDate.Value = new System.DateTime(2018, 5, 9, 0, 0, 0, 0);
            // 
            // Btn_CreateLicense
            // 
            this.Btn_CreateLicense.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_CreateLicense.Location = new System.Drawing.Point(189, 215);
            this.Btn_CreateLicense.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Btn_CreateLicense.Name = "Btn_CreateLicense";
            this.Btn_CreateLicense.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Btn_CreateLicense.Size = new System.Drawing.Size(123, 56);
            this.Btn_CreateLicense.TabIndex = 3;
            this.Btn_CreateLicense.Text = "生成许可证";
            this.Btn_CreateLicense.UseVisualStyleBackColor = true;
            this.Btn_CreateLicense.Click += new System.EventHandler(this.Btn_CreateLicense_Click);
            // 
            // MainWindow
            // 
            this.AcceptButton = this.Btn_CreateLicense;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 335);
            this.Controls.Add(this.Btn_CreateLicense);
            this.Controls.Add(this.Dtp_ExpiredDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Txt_EnterpriseName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Txt_UniqueCode);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "许可证生成工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Txt_UniqueCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Txt_EnterpriseName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker Dtp_ExpiredDate;
        private System.Windows.Forms.Button Btn_CreateLicense;
    }
}

