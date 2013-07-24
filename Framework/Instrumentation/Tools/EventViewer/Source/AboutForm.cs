using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using TC = LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
	/// <summary>
	/// A dialog that displays information about the application.
	/// </summary>
	public class AboutForm : TC.Form
	{
		private System.Windows.Forms.Label lblProductName;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.PictureBox picIcon;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblVersion;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AboutForm()
		{
			InitializeComponent();

			Text = "About " + EventViewerSettings.ProductName;
			lblProductName.Text = EventViewerSettings.ProductName;
			lblVersion.Text = typeof(MainForm).Assembly.GetName().Version.ToString();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutForm));
			this.lblProductName = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.picIcon = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblProductName
			// 
			this.lblProductName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblProductName.Location = new System.Drawing.Point(48, 16);
			this.lblProductName.Name = "lblProductName";
			this.lblProductName.Size = new System.Drawing.Size(186, 24);
			this.lblProductName.TabIndex = 0;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnOK.Location = new System.Drawing.Point(162, 90);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(72, 22);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			// 
			// picIcon
			// 
			this.picIcon.Image = ((System.Drawing.Image)(resources.GetObject("picIcon.Image")));
			this.picIcon.Location = new System.Drawing.Point(8, 8);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new System.Drawing.Size(32, 32);
			this.picIcon.TabIndex = 2;
			this.picIcon.TabStop = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(48, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Version:";
			// 
			// lblVersion
			// 
			this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblVersion.Location = new System.Drawing.Point(104, 56);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(88, 16);
			this.lblVersion.TabIndex = 6;
			// 
			// AboutForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnOK;
			this.ClientSize = new System.Drawing.Size(242, 119);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.picIcon);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lblProductName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);

		}
		#endregion
	}
}
