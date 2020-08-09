namespace ExcelTools
{
    partial class ToolMainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolMainForm));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btn_chooseFolder = new System.Windows.Forms.Button();
            this.txt_folderPath = new System.Windows.Forms.TextBox();
            this.btn_startMerge = new System.Windows.Forms.Button();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.txt_excelList = new System.Windows.Forms.TextBox();
            this.processBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btn_chooseFolder
            // 
            this.btn_chooseFolder.Font = new System.Drawing.Font("微软雅黑", 10.28571F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_chooseFolder.Location = new System.Drawing.Point(34, 15);
            this.btn_chooseFolder.Name = "btn_chooseFolder";
            this.btn_chooseFolder.Size = new System.Drawing.Size(127, 35);
            this.btn_chooseFolder.TabIndex = 1;
            this.btn_chooseFolder.Text = "选择文件夹：";
            this.btn_chooseFolder.UseVisualStyleBackColor = true;
            this.btn_chooseFolder.Click += new System.EventHandler(this.btn_chooseFolder_Click);
            // 
            // txt_folderPath
            // 
            this.txt_folderPath.Location = new System.Drawing.Point(167, 23);
            this.txt_folderPath.Name = "txt_folderPath";
            this.txt_folderPath.Size = new System.Drawing.Size(537, 25);
            this.txt_folderPath.TabIndex = 2;
            // 
            // btn_startMerge
            // 
            this.btn_startMerge.Font = new System.Drawing.Font("微软雅黑", 13.91597F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_startMerge.Location = new System.Drawing.Point(277, 272);
            this.btn_startMerge.Name = "btn_startMerge";
            this.btn_startMerge.Size = new System.Drawing.Size(165, 77);
            this.btn_startMerge.TabIndex = 3;
            this.btn_startMerge.Text = "开始合并";
            this.btn_startMerge.UseVisualStyleBackColor = true;
            this.btn_startMerge.Click += new System.EventHandler(this.btn_startMerge_Click);
            // 
            // txt_result
            // 
            this.txt_result.Location = new System.Drawing.Point(34, 397);
            this.txt_result.Multiline = true;
            this.txt_result.Name = "txt_result";
            this.txt_result.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_result.Size = new System.Drawing.Size(670, 210);
            this.txt_result.TabIndex = 4;
            this.txt_result.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_result_KeyDown);
            // 
            // txt_excelList
            // 
            this.txt_excelList.Location = new System.Drawing.Point(34, 56);
            this.txt_excelList.Multiline = true;
            this.txt_excelList.Name = "txt_excelList";
            this.txt_excelList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_excelList.Size = new System.Drawing.Size(670, 210);
            this.txt_excelList.TabIndex = 5;
            this.txt_excelList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_excelList_KeyDown);
            // 
            // processBar1
            // 
            this.processBar1.Location = new System.Drawing.Point(34, 358);
            this.processBar1.Maximum = 200;
            this.processBar1.Name = "processBar1";
            this.processBar1.Size = new System.Drawing.Size(670, 23);
            this.processBar1.TabIndex = 6;
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ToolMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 619);
            this.Controls.Add(this.processBar1);
            this.Controls.Add(this.txt_excelList);
            this.Controls.Add(this.txt_result);
            this.Controls.Add(this.btn_startMerge);
            this.Controls.Add(this.txt_folderPath);
            this.Controls.Add(this.btn_chooseFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ToolMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Excel Tool";
            this.Load += new System.EventHandler(this.ToolMainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btn_chooseFolder;
        private System.Windows.Forms.TextBox txt_folderPath;
        private System.Windows.Forms.Button btn_startMerge;
        private System.Windows.Forms.TextBox txt_result;
        private System.Windows.Forms.TextBox txt_excelList;
        private System.Windows.Forms.ProgressBar processBar1;
        private System.Windows.Forms.Timer timer1;
    }
}

