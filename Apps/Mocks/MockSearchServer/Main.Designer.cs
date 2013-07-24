namespace LinkMe.Apps.MockSearchServer
{
    partial class Main
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.chkSynchroniseIndex = new System.Windows.Forms.CheckBox();
            this.chkRebuildIndex = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(45, 57);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(157, 57);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // chkSynchroniseIndex
            // 
            this.chkSynchroniseIndex.AutoSize = true;
            this.chkSynchroniseIndex.Checked = true;
            this.chkSynchroniseIndex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSynchroniseIndex.Location = new System.Drawing.Point(27, 21);
            this.chkSynchroniseIndex.Name = "chkSynchroniseIndex";
            this.chkSynchroniseIndex.Size = new System.Drawing.Size(112, 17);
            this.chkSynchroniseIndex.TabIndex = 2;
            this.chkSynchroniseIndex.Text = "Synchronise index";
            this.chkSynchroniseIndex.UseVisualStyleBackColor = true;
            // 
            // chkRebuildIndex
            // 
            this.chkRebuildIndex.AutoSize = true;
            this.chkRebuildIndex.Checked = true;
            this.chkRebuildIndex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRebuildIndex.Location = new System.Drawing.Point(154, 21);
            this.chkRebuildIndex.Name = "chkRebuildIndex";
            this.chkRebuildIndex.Size = new System.Drawing.Size(90, 17);
            this.chkRebuildIndex.TabIndex = 3;
            this.chkRebuildIndex.Text = "Rebuild index";
            this.chkRebuildIndex.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 105);
            this.Controls.Add(this.chkRebuildIndex);
            this.Controls.Add(this.chkSynchroniseIndex);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Name = "Main";
            this.Text = "Mock Search Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.CheckBox chkSynchroniseIndex;
        private System.Windows.Forms.CheckBox chkRebuildIndex;
    }
}