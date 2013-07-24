using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// A dialog that displays an exception (System.Exception).
	/// </summary>
	public class ExceptionDialog : Dialog
	{
		private bool m_firstActivation = true;

		private ExceptionViewer contentException;
		private System.Windows.Forms.PictureBox picIcon;
		private System.Windows.Forms.Label lblHeading;
		private System.Windows.Forms.Button btnDebug;
		private System.Windows.Forms.Button btnSaveXml;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ExceptionDialog(System.Exception exception, string heading, MessageBoxButtons buttons, Icon icon)
			: this(heading, buttons, icon)
		{
			contentException.DisplayException(exception);
		}

		public ExceptionDialog(System.Exception exception, string heading, MessageBoxButtons buttons,
			MessageBoxIcon icon)
			: this(exception, heading, buttons, GetMessageBoxIcon(icon))
		{
		}

		public ExceptionDialog(System.Exception exception, string heading, MessageBoxButtons buttons)
			: this(exception, heading, buttons, SystemIcons.Error)
		{
		}

		public ExceptionDialog(System.Exception exception, string heading)
			: this(exception, heading, MessageBoxButtons.OK)
		{
		}

		public ExceptionDialog(ExceptionInfo exception, string heading, MessageBoxButtons buttons, Icon icon)
			: this(heading, buttons, icon)
		{
			contentException.DisplayException(exception);
		}

		public ExceptionDialog(ExceptionInfo exception, string heading, MessageBoxButtons buttons,
			MessageBoxIcon icon)
			: this(exception, heading, buttons, GetMessageBoxIcon(icon))
		{
		}

		public ExceptionDialog(ExceptionInfo exception, string heading, MessageBoxButtons buttons)
			: this(exception, heading, buttons, SystemIcons.Error)
		{
		}

		public ExceptionDialog(ExceptionInfo exception, string heading)
			: this(exception, heading, MessageBoxButtons.OK)
		{
		}

		private ExceptionDialog(string heading, MessageBoxButtons buttons, Icon icon)
			: base(buttons)
		{
			InitializeComponent();

			Icon = icon;
			picIcon.Image = (icon == null ? null : icon.ToBitmap());
			lblHeading.Text = heading;

#if DEBUG
			btnDebug.Enabled = true;
			btnDebug.Visible = true;
#endif
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
			this.contentException = new LinkMe.Framework.Tools.Controls.ExceptionViewer();
			this.picIcon = new System.Windows.Forms.PictureBox();
			this.lblHeading = new System.Windows.Forms.Label();
			this.btnDebug = new System.Windows.Forms.Button();
			this.btnSaveXml = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// contentException
			// 
			this.contentException.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.contentException.Location = new System.Drawing.Point(8, 48);
			this.contentException.MinimumSize = new System.Drawing.Size(120, 90);
			this.contentException.Name = "contentException";
			this.contentException.Size = new System.Drawing.Size(568, 376);
			this.contentException.TabIndex = 1;
			// 
			// picIcon
			// 
			this.picIcon.Location = new System.Drawing.Point(8, 8);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new System.Drawing.Size(32, 32);
			this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.picIcon.TabIndex = 2;
			this.picIcon.TabStop = false;
			// 
			// lblHeading
			// 
			this.lblHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblHeading.Location = new System.Drawing.Point(48, 8);
			this.lblHeading.Name = "lblHeading";
			this.lblHeading.Size = new System.Drawing.Size(528, 28);
			this.lblHeading.TabIndex = 0;
			// 
			// btnDebug
			// 
			this.btnDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDebug.Enabled = false;
			this.btnDebug.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnDebug.Location = new System.Drawing.Point(504, 438);
			this.btnDebug.Name = "btnDebug";
			this.btnDebug.Size = new System.Drawing.Size(72, 22);
			this.btnDebug.TabIndex = 300;
			this.btnDebug.Text = "Debug!";
			this.btnDebug.Visible = false;
			this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
			// 
			// btnSaveXml
			// 
			this.btnSaveXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSaveXml.Location = new System.Drawing.Point(8, 437);
			this.btnSaveXml.Name = "btnSaveXml";
			this.btnSaveXml.Size = new System.Drawing.Size(72, 24);
			this.btnSaveXml.TabIndex = 2;
			this.btnSaveXml.Text = "Save...";
			this.btnSaveXml.Click += new System.EventHandler(this.btnSaveXml_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "xml";
			this.saveFileDialog.Filter = "Exception XML files (*.xml)|*.xml|All files (*.*)|*.*";
			this.saveFileDialog.Title = "Save exception to XML file";
			// 
			// ExceptionDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(584, 477);
			this.Controls.Add(this.btnSaveXml);
			this.Controls.Add(this.btnDebug);
			this.Controls.Add(this.lblHeading);
			this.Controls.Add(this.picIcon);
			this.Controls.Add(this.contentException);
			this.MinimumSize = new System.Drawing.Size(336, 232);
			this.Name = "ExceptionDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Exception";
			this.ResumeLayout(false);

		}
		#endregion

		private static Icon GetMessageBoxIcon(MessageBoxIcon icon)
		{
			// Note that the MessageBoxIcon enumeration has several members with the same values (eg. Hand and Stop).

			switch (icon)
			{
				case MessageBoxIcon.Error:
					return SystemIcons.Error;
				case MessageBoxIcon.Information:
					return SystemIcons.Information;
				case MessageBoxIcon.None:
					return null;
				case MessageBoxIcon.Question:
					return SystemIcons.Question;
				case MessageBoxIcon.Warning:
					return SystemIcons.Warning;

				default:
					Debug.Fail("Unexpected value of MessageBoxIcon: " + icon.ToString());
					return null;
			}
		}

		protected override void OnActivated(EventArgs e)
		{
			if (m_firstActivation)
			{
				m_firstActivation = false;
				contentException.InitialiseFocus();
			}

			base.OnActivated(e);
		}

		[DebuggerHidden]
		private void btnDebug_Click(object sender, EventArgs e)
		{
#if DEBUG
			if (Debugger.Launch())
			{
				Debugger.Break();
			}
#endif
		}

		private void btnSaveXml_Click(object sender, EventArgs e)
		{
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				contentException.SaveXml(saveFileDialog.FileName);
			}
		}
	}
}
