namespace AspMinifier
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chk_backup = new System.Windows.Forms.CheckBox();
            this.lbl_status = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.btn_minify = new System.Windows.Forms.Button();
            this.btn_browse = new System.Windows.Forms.Button();
            this.txt_folder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // chk_backup
            // 
            this.chk_backup.AutoSize = true;
            this.chk_backup.Location = new System.Drawing.Point(327, 86);
            this.chk_backup.Name = "chk_backup";
            this.chk_backup.Size = new System.Drawing.Size(93, 17);
            this.chk_backup.TabIndex = 13;
            this.chk_backup.Text = "Make Backup";
            this.chk_backup.UseVisualStyleBackColor = true;
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(12, 86);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(108, 13);
            this.lbl_status.TabIndex = 12;
            this.lbl_status.Text = "Waiting to get started";
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(12, 105);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(410, 23);
            this.progress.Step = 1;
            this.progress.TabIndex = 11;
            // 
            // btn_minify
            // 
            this.btn_minify.Location = new System.Drawing.Point(148, 55);
            this.btn_minify.Name = "btn_minify";
            this.btn_minify.Size = new System.Drawing.Size(140, 23);
            this.btn_minify.TabIndex = 10;
            this.btn_minify.Text = "Minify!";
            this.btn_minify.UseVisualStyleBackColor = true;
            this.btn_minify.Click += new System.EventHandler(this.btn_minify_Click);
            // 
            // btn_browse
            // 
            this.btn_browse.Location = new System.Drawing.Point(345, 26);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(75, 23);
            this.btn_browse.TabIndex = 9;
            this.btn_browse.Text = "Browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // txt_folder
            // 
            this.txt_folder.Location = new System.Drawing.Point(12, 29);
            this.txt_folder.Name = "txt_folder";
            this.txt_folder.Size = new System.Drawing.Size(327, 20);
            this.txt_folder.TabIndex = 8;
            this.txt_folder.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Select a folder";
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 140);
            this.Controls.Add(this.chk_backup);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.btn_minify);
            this.Controls.Add(this.btn_browse);
            this.Controls.Add(this.txt_folder);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asp.Net Published Project Minifier - sprklab.com";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chk_backup;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Button btn_minify;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.TextBox txt_folder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

