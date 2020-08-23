namespace SD.IdentitySystem.UniqueCode
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
            this.Btn_CalculateMachineCode = new System.Windows.Forms.Button();
            this.Btn_Copy = new System.Windows.Forms.Button();
            this.Txt_MachineCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Btn_CalculateMachineCode
            // 
            this.Btn_CalculateMachineCode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_CalculateMachineCode.Location = new System.Drawing.Point(184, 141);
            this.Btn_CalculateMachineCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Btn_CalculateMachineCode.Name = "Btn_CalculateMachineCode";
            this.Btn_CalculateMachineCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Btn_CalculateMachineCode.Size = new System.Drawing.Size(123, 56);
            this.Btn_CalculateMachineCode.TabIndex = 32;
            this.Btn_CalculateMachineCode.Text = "计算";
            this.Btn_CalculateMachineCode.UseVisualStyleBackColor = true;
            this.Btn_CalculateMachineCode.Click += new System.EventHandler(this.Btn_CalculateMachineCode_Click);
            // 
            // Btn_Copy
            // 
            this.Btn_Copy.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Copy.Location = new System.Drawing.Point(399, 47);
            this.Btn_Copy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Btn_Copy.Name = "Btn_Copy";
            this.Btn_Copy.Size = new System.Drawing.Size(79, 28);
            this.Btn_Copy.TabIndex = 31;
            this.Btn_Copy.Text = "点击复制";
            this.Btn_Copy.UseVisualStyleBackColor = true;
            this.Btn_Copy.Click += new System.EventHandler(this.Btn_Copy_Click);
            // 
            // Txt_MachineCode
            // 
            this.Txt_MachineCode.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_MachineCode.Location = new System.Drawing.Point(117, 50);
            this.Txt_MachineCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Txt_MachineCode.Name = "Txt_MachineCode";
            this.Txt_MachineCode.ReadOnly = true;
            this.Txt_MachineCode.Size = new System.Drawing.Size(270, 24);
            this.Txt_MachineCode.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(30, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 29;
            this.label3.Text = "机器唯一码：";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 265);
            this.Controls.Add(this.Btn_CalculateMachineCode);
            this.Controls.Add(this.Btn_Copy);
            this.Controls.Add(this.Txt_MachineCode);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "机器唯一码计算工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_CalculateMachineCode;
        private System.Windows.Forms.Button Btn_Copy;
        private System.Windows.Forms.TextBox Txt_MachineCode;
        private System.Windows.Forms.Label label3;
    }
}

