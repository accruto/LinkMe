using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LinkMe.Framework.Tools.Controls
{
	internal class ExceptionPanes : TwoPanes
	{
		internal System.Windows.Forms.TreeView tvwProperties;
		internal LinkMe.Framework.Tools.Controls.TextBox txtValue;
		internal System.Windows.Forms.Label lblValue;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ImageList imgTreeIcons;
		private System.ComponentModel.IContainer components = null;

		internal ExceptionPanes()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ExceptionPanes));
			this.label3 = new System.Windows.Forms.Label();
			this.tvwProperties = new System.Windows.Forms.TreeView();
			this.imgTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.txtValue = new LinkMe.Framework.Tools.Controls.TextBox();
			this.lblValue = new System.Windows.Forms.Label();
			this.PaneOne.SuspendLayout();
			this.PaneTwo.SuspendLayout();
			this.SuspendLayout();
			// 
			// PaneOne
			// 
			this.PaneOne.Controls.Add(this.label3);
			this.PaneOne.Controls.Add(this.tvwProperties);
			this.PaneOne.Name = "PaneOne";
			// 
			// PaneTwo
			// 
			this.PaneTwo.Controls.Add(this.txtValue);
			this.PaneTwo.Controls.Add(this.lblValue);
			this.PaneTwo.Name = "PaneTwo";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Exception:";
			// 
			// tvwProperties
			// 
			this.tvwProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tvwProperties.HideSelection = false;
			this.tvwProperties.ImageList = this.imgTreeIcons;
			this.tvwProperties.Location = new System.Drawing.Point(0, 16);
			this.tvwProperties.Name = "tvwProperties";
			this.tvwProperties.Size = new System.Drawing.Size(728, 152);
			this.tvwProperties.TabIndex = 8;
			// 
			// imgTreeIcons
			// 
			this.imgTreeIcons.ImageSize = new System.Drawing.Size(16, 16);
			this.imgTreeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTreeIcons.ImageStream")));
			this.imgTreeIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// txtValue
			// 
			this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtValue.Location = new System.Drawing.Point(0, 16);
			this.txtValue.Multiline = true;
			this.txtValue.Name = "txtValue";
			this.txtValue.ReadOnly = true;
			this.txtValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtValue.Size = new System.Drawing.Size(728, 152);
			this.txtValue.TabIndex = 4;
			this.txtValue.Text = "";
			// 
			// lblValue
			// 
			this.lblValue.Location = new System.Drawing.Point(0, 0);
			this.lblValue.Name = "lblValue";
			this.lblValue.Size = new System.Drawing.Size(80, 16);
			this.lblValue.TabIndex = 3;
			this.lblValue.Text = "Message:";
			// 
			// ExceptionPanes
			// 
			this.Name = "ExceptionPanes";
			this.SplitterBorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.PaneOne.ResumeLayout(false);
			this.PaneTwo.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}

