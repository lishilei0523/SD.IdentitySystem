using System.Windows.Forms;

namespace SD.IdentitySystem.InitializationTool
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.Btn_Init = new System.Windows.Forms.Button();
            this.Brn_OpenFile = new System.Windows.Forms.Button();
            this.Cbx_SystemKind = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_FilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Btn_Init
            // 
            this.Btn_Init.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Init.Location = new System.Drawing.Point(298, 229);
            this.Btn_Init.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Btn_Init.Name = "Btn_Init";
            this.Btn_Init.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Btn_Init.Size = new System.Drawing.Size(123, 56);
            this.Btn_Init.TabIndex = 18;
            this.Btn_Init.Text = "初始化";
            this.Btn_Init.UseVisualStyleBackColor = true;
            this.Btn_Init.Click += new System.EventHandler(this.Btn_Init_Click);
            // 
            // Brn_OpenFile
            // 
            this.Brn_OpenFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Brn_OpenFile.Location = new System.Drawing.Point(623, 59);
            this.Brn_OpenFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Brn_OpenFile.Name = "Brn_OpenFile";
            this.Brn_OpenFile.Size = new System.Drawing.Size(55, 28);
            this.Brn_OpenFile.TabIndex = 17;
            this.Brn_OpenFile.Text = "打开";
            this.Brn_OpenFile.UseVisualStyleBackColor = true;
            this.Brn_OpenFile.Click += new System.EventHandler(this.Btn_OpenFile_Click);
            // 
            // Cbx_SystemKind
            // 
            this.Cbx_SystemKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_SystemKind.FormattingEnabled = true;
            this.Cbx_SystemKind.Location = new System.Drawing.Point(134, 137);
            this.Cbx_SystemKind.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Cbx_SystemKind.Name = "Cbx_SystemKind";
            this.Cbx_SystemKind.Size = new System.Drawing.Size(474, 25);
            this.Cbx_SystemKind.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(47, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "信息系统：";
            // 
            // Txt_FilePath
            // 
            this.Txt_FilePath.Location = new System.Drawing.Point(134, 62);
            this.Txt_FilePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Txt_FilePath.Name = "Txt_FilePath";
            this.Txt_FilePath.Size = new System.Drawing.Size(474, 23);
            this.Txt_FilePath.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(47, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "文件路径：";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(725, 346);
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
            this.Text = "身份认证系统初始化工具";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button Btn_Init;
        private Button Brn_OpenFile;
        private ComboBox Cbx_SystemKind;
        private Label label2;
        private TextBox Txt_FilePath;
        private Label label1;

    }
}

