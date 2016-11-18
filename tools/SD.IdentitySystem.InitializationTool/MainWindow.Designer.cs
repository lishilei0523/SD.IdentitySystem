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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Btn_Init = new System.Windows.Forms.Button();
            this.Brn_OpenFile = new System.Windows.Forms.Button();
            this.Cbx_SystemKind = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_FilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(725, 346);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage1.Controls.Add(this.Btn_Init);
            this.tabPage1.Controls.Add(this.Brn_OpenFile);
            this.tabPage1.Controls.Add(this.Cbx_SystemKind);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.Txt_FilePath);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(717, 316);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "权限初始化";
            // 
            // Btn_Init
            // 
            this.Btn_Init.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Init.Location = new System.Drawing.Point(306, 208);
            this.Btn_Init.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Btn_Init.Name = "Btn_Init";
            this.Btn_Init.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Btn_Init.Size = new System.Drawing.Size(123, 56);
            this.Btn_Init.TabIndex = 12;
            this.Btn_Init.Text = "初始化";
            this.Btn_Init.UseVisualStyleBackColor = true;
            this.Btn_Init.Click += new System.EventHandler(this.Btn_Init_Click);
            // 
            // Brn_OpenFile
            // 
            this.Brn_OpenFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Brn_OpenFile.Location = new System.Drawing.Point(631, 38);
            this.Brn_OpenFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Brn_OpenFile.Name = "Brn_OpenFile";
            this.Brn_OpenFile.Size = new System.Drawing.Size(55, 28);
            this.Brn_OpenFile.TabIndex = 11;
            this.Brn_OpenFile.Text = "打开";
            this.Brn_OpenFile.UseVisualStyleBackColor = true;
            this.Brn_OpenFile.Click += new System.EventHandler(this.Brn_OpenFile_Click);
            // 
            // Cbx_SystemKind
            // 
            this.Cbx_SystemKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_SystemKind.FormattingEnabled = true;
            this.Cbx_SystemKind.Location = new System.Drawing.Point(142, 116);
            this.Cbx_SystemKind.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Cbx_SystemKind.Name = "Cbx_SystemKind";
            this.Cbx_SystemKind.Size = new System.Drawing.Size(474, 25);
            this.Cbx_SystemKind.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(31, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "信息系统类别：";
            // 
            // Txt_FilePath
            // 
            this.Txt_FilePath.Location = new System.Drawing.Point(142, 41);
            this.Txt_FilePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Txt_FilePath.Name = "Txt_FilePath";
            this.Txt_FilePath.Size = new System.Drawing.Size(474, 23);
            this.Txt_FilePath.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(55, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "文件路径：";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(717, 316);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "信息系统类别初始化";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(717, 269);
            this.panel1.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(725, 346);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "身份认证系统初始化工具";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button Btn_Init;
        private Button Brn_OpenFile;
        private ComboBox Cbx_SystemKind;
        private Label label2;
        private TextBox Txt_FilePath;
        private Label label1;
        private Panel panel1;
    }
}

