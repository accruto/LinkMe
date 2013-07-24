using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using LinkMe.Framework.Tools;
using LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
	internal class MssqlConnectionPrompt : Dialog
	{
		private System.Windows.Forms.Button btnEditConnString;
		private LinkMe.Framework.Tools.Controls.TextBox txtDatabase;
        private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MssqlConnectionPrompt()
			: base(MessageBoxButtons.OKCancel)
		{
			InitializeComponent();

			SetButtonEnabled(DialogResult.OK, false);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.btnEditConnString = new System.Windows.Forms.Button();
            this.txtDatabase = new LinkMe.Framework.Tools.Controls.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEditConnString
            // 
            this.btnEditConnString.Location = new System.Drawing.Point(8, 80);
            this.btnEditConnString.Name = "btnEditConnString";
            this.btnEditConnString.Size = new System.Drawing.Size(160, 23);
            this.btnEditConnString.TabIndex = 2;
            this.btnEditConnString.Text = "Edit Connection String...";
            this.btnEditConnString.Click += new System.EventHandler(this.btnEditConnString_Click);
            // 
            // txtDatabase
            // 
            this.txtDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDatabase.Location = new System.Drawing.Point(8, 24);
            this.txtDatabase.Multiline = true;
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(464, 48);
            this.txtDatabase.TabIndex = 1;
            this.txtDatabase.TextChanged += new System.EventHandler(this.txtDatabase_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Logging database connection string:";
            // 
            // MssqlConnectionPrompt
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(480, 150);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.btnEditConnString);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 224);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(264, 184);
            this.Name = "MssqlConnectionPrompt";
            this.ShowInTaskbar = false;
            this.Text = "Connect to MS SQL database";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public string ConnectionString
		{
			get { return txtDatabase.Text; }
			set { txtDatabase.Text = value; }
		}

		private void btnEditConnString_Click(object sender, System.EventArgs e)
		{
			string newConnectionString = OleDbUtil.PromptForConnectionString(txtDatabase.Text, this,
				Constants.OleDb.MsSqlProviderProgID, Constants.OleDb.MsSqlProviderDisplayName);

			if (newConnectionString != null)
			{
				txtDatabase.Text = newConnectionString;
			}
		}

		private void txtDatabase_TextChanged(object sender, System.EventArgs e)
		{
			SetButtonEnabled(DialogResult.OK, txtDatabase.TextLength > 0);
		}
	}
}
