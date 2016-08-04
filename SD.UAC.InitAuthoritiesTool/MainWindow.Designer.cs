namespace SD.UAC.InitAuthoritiesTool
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.Txt_FilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Cbx_SystemKind = new System.Windows.Forms.ComboBox();
            this.Brn_OpenFile = new System.Windows.Forms.Button();
            this.Btn_Init = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(52, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件路径：";
            // 
            // Txt_FilePath
            // 
            this.Txt_FilePath.Location = new System.Drawing.Point(139, 25);
            this.Txt_FilePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Txt_FilePath.Name = "Txt_FilePath";
            this.Txt_FilePath.Size = new System.Drawing.Size(474, 23);
            this.Txt_FilePath.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(28, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "信息系统类别：";
            // 
            // Cbx_SystemKind
            // 
            this.Cbx_SystemKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_SystemKind.FormattingEnabled = true;
            this.Cbx_SystemKind.Location = new System.Drawing.Point(139, 100);
            this.Cbx_SystemKind.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Cbx_SystemKind.Name = "Cbx_SystemKind";
            this.Cbx_SystemKind.Size = new System.Drawing.Size(474, 25);
            this.Cbx_SystemKind.TabIndex = 4;
            // 
            // Brn_OpenFile
            // 
            this.Brn_OpenFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Brn_OpenFile.Location = new System.Drawing.Point(628, 22);
            this.Brn_OpenFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Brn_OpenFile.Name = "Brn_OpenFile";
            this.Brn_OpenFile.Size = new System.Drawing.Size(55, 28);
            this.Brn_OpenFile.TabIndex = 5;
            this.Brn_OpenFile.Text = "打开";
            this.Brn_OpenFile.UseVisualStyleBackColor = true;
            this.Brn_OpenFile.Click += new System.EventHandler(this.Brn_OpenFile_Click);
            // 
            // Btn_Init
            // 
            this.Btn_Init.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Init.Location = new System.Drawing.Point(303, 192);
            this.Btn_Init.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Btn_Init.Name = "Btn_Init";
            this.Btn_Init.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Btn_Init.Size = new System.Drawing.Size(123, 56);
            this.Btn_Init.TabIndex = 6;
            this.Btn_Init.Text = "初始化";
            this.Btn_Init.UseVisualStyleBackColor = true;
            this.Btn_Init.Click += new System.EventHandler(this.Btn_Init_Click);
            // 
            // MainWindow
            // 
            this.AcceptButton = this.Btn_Init;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 278);
            this.Controls.Add(this.Btn_Init);
            this.Controls.Add(this.Brn_OpenFile);
            this.Controls.Add(this.Cbx_SystemKind);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Txt_FilePath);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "权限初始化工具";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Txt_FilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Cbx_SystemKind;
        private System.Windows.Forms.Button Brn_OpenFile;
        private System.Windows.Forms.Button Btn_Init;
    }
}

