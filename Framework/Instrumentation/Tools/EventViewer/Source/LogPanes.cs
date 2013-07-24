using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using TC = LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
	internal class LogPanes : TC.TwoPanes
	{
		internal LinkMe.Framework.Tools.Controls.ListView lvwMessages;
		internal LinkMe.Framework.Tools.Controls.TextBox txtPreview;
		private System.Windows.Forms.ColumnHeader colTime;
		private System.Windows.Forms.ColumnHeader colEvent;
		private System.Windows.Forms.ColumnHeader colSource;
		private System.Windows.Forms.ColumnHeader colMethod;
		private System.Windows.Forms.ColumnHeader colMessage;
		internal System.Windows.Forms.ImageList imgSmallIcons;
		private System.Windows.Forms.ImageList imgLargeIcons;
		private System.Windows.Forms.ColumnHeader colType;
		private System.ComponentModel.IContainer components;

		public LogPanes()
		{
			InitializeComponent();
			Initialise();
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogPanes));
            this.lvwMessages = new LinkMe.Framework.Tools.Controls.ListView();
            this.colTime = new System.Windows.Forms.ColumnHeader();
            this.colEvent = new System.Windows.Forms.ColumnHeader();
            this.colSource = new System.Windows.Forms.ColumnHeader();
            this.colType = new System.Windows.Forms.ColumnHeader();
            this.colMethod = new System.Windows.Forms.ColumnHeader();
            this.colMessage = new System.Windows.Forms.ColumnHeader();
            this.imgLargeIcons = new System.Windows.Forms.ImageList(this.components);
            this.imgSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.txtPreview = new LinkMe.Framework.Tools.Controls.TextBox();
            this.PaneOne.SuspendLayout();
            this.PaneTwo.SuspendLayout();
            this.SuspendLayout();
            // 
            // PaneOne
            // 
            this.PaneOne.Controls.Add(this.lvwMessages);
            this.PaneOne.Size = new System.Drawing.Size(728, 281);
            // 
            // PaneTwo
            // 
            this.PaneTwo.Controls.Add(this.txtPreview);
            this.PaneTwo.Location = new System.Drawing.Point(0, 287);
            this.PaneTwo.Size = new System.Drawing.Size(728, 65);
            // 
            // lvwMessages
            // 
            this.lvwMessages.AutoResizeLastColumn = true;
            this.lvwMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTime,
            this.colEvent,
            this.colSource,
            this.colType,
            this.colMethod,
            this.colMessage});
            this.lvwMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwMessages.FullRowSelect = true;
            this.lvwMessages.HideSelection = false;
            this.lvwMessages.LargeImageList = this.imgLargeIcons;
            this.lvwMessages.Location = new System.Drawing.Point(0, 0);
            this.lvwMessages.Name = "lvwMessages";
            this.lvwMessages.Size = new System.Drawing.Size(728, 281);
            this.lvwMessages.SmallImageList = this.imgSmallIcons;
            this.lvwMessages.TabIndex = 0;
            this.lvwMessages.UseCompatibleStateImageBehavior = false;
            this.lvwMessages.View = System.Windows.Forms.View.Details;
            // 
            // colTime
            // 
            this.colTime.Text = "Time";
            this.colTime.Width = 150;
            // 
            // colEvent
            // 
            this.colEvent.Text = "Event";
            this.colEvent.Width = 90;
            // 
            // colSource
            // 
            this.colSource.Text = "Source";
            this.colSource.Width = 150;
            // 
            // colType
            // 
            this.colType.Text = "Type";
            this.colType.Width = 150;
            // 
            // colMethod
            // 
            this.colMethod.Text = "Method";
            this.colMethod.Width = 100;
            // 
            // colMessage
            // 
            this.colMessage.Text = "Message";
            this.colMessage.Width = 55;
            // 
            // imgLargeIcons
            // 
            this.imgLargeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLargeIcons.ImageStream")));
            this.imgLargeIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLargeIcons.Images.SetKeyName(0, "");
            this.imgLargeIcons.Images.SetKeyName(1, "");
            this.imgLargeIcons.Images.SetKeyName(2, "");
            this.imgLargeIcons.Images.SetKeyName(3, "");
            this.imgLargeIcons.Images.SetKeyName(4, "");
            this.imgLargeIcons.Images.SetKeyName(5, "");
            this.imgLargeIcons.Images.SetKeyName(6, "");
            this.imgLargeIcons.Images.SetKeyName(7, "");
            this.imgLargeIcons.Images.SetKeyName(8, "");
            // 
            // imgSmallIcons
            // 
            this.imgSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgSmallIcons.ImageStream")));
            this.imgSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imgSmallIcons.Images.SetKeyName(0, "");
            this.imgSmallIcons.Images.SetKeyName(1, "");
            this.imgSmallIcons.Images.SetKeyName(2, "");
            this.imgSmallIcons.Images.SetKeyName(3, "");
            this.imgSmallIcons.Images.SetKeyName(4, "");
            this.imgSmallIcons.Images.SetKeyName(5, "");
            this.imgSmallIcons.Images.SetKeyName(6, "");
            this.imgSmallIcons.Images.SetKeyName(7, "");
            this.imgSmallIcons.Images.SetKeyName(8, "");
            // 
            // txtPreview
            // 
            this.txtPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPreview.Location = new System.Drawing.Point(0, 0);
            this.txtPreview.Multiline = true;
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ReadOnly = true;
            this.txtPreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPreview.Size = new System.Drawing.Size(728, 65);
            this.txtPreview.TabIndex = 0;
            // 
            // LogPanes
            // 
            this.Name = "LogPanes";
            this.SplitterBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PaneOne.ResumeLayout(false);
            this.PaneTwo.ResumeLayout(false);
            this.PaneTwo.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

	}
}
