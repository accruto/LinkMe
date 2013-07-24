using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using LinkMe.Framework.Tools;
using LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.Editors;

namespace LinkMe.Framework.Configuration.Tools.Forms
{
	/// <summary>
	/// Summary description for CreateRepositoryForm.
	/// </summary>
	public class ResultsForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnOK;
		private ArrayList m_pendingResults;
		private bool m_complete;
		private LinkMe.Framework.Tools.Controls.ListView lvwResults;
		private System.Windows.Forms.ColumnHeader clnHeader;
		private System.Windows.Forms.ImageList imageList;
		private System.ComponentModel.IContainer components;
		private bool m_canInvoke = false;
		private int m_totalMessages = 0;
		private int m_totalErrors = 0;
		private int m_totalWarnings = 0;

		private const int ImageIndexInformation = 0;
		private const int ImageIndexError = 1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtTotalMessages;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtTotalErrors;
		private System.Windows.Forms.TextBox txtTotalWarnings;
		private const int ImageIndexWarning = 2;

		public ResultsForm(string text, string iconResource)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			object image = Images.FindImage(iconResource);
			if (image != null)
			{
				Icon icon = image as Icon;
				if (icon == null)
				{
					throw new ApplicationException(string.Format("A resource named '{0}' was loaded successfully,"
						+ " but its type is '{1}' when an Icon is expected.", iconResource, image.GetType().FullName));
				}

				Icon = icon;
			}

			Text = text;

			m_pendingResults = new ArrayList();
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ResultsForm));
			this.btnOK = new System.Windows.Forms.Button();
			this.lvwResults = new LinkMe.Framework.Tools.Controls.ListView();
			this.clnHeader = new System.Windows.Forms.ColumnHeader();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.txtTotalMessages = new System.Windows.Forms.TextBox();
			this.txtTotalErrors = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtTotalWarnings = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(240, 320);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			// 
			// lvwResults
			// 
			this.lvwResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwResults.AutoResizeLastColumn = true;
			this.lvwResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.clnHeader});
			this.lvwResults.FullRowSelect = true;
			this.lvwResults.LargeImageList = this.imageList;
			this.lvwResults.Location = new System.Drawing.Point(8, 16);
			this.lvwResults.Name = "lvwResults";
			this.lvwResults.Size = new System.Drawing.Size(560, 264);
			this.lvwResults.SmallImageList = this.imageList;
			this.lvwResults.TabIndex = 2;
			this.lvwResults.View = System.Windows.Forms.View.Details;
			this.lvwResults.DoubleClick += new System.EventHandler(this.lvwResults_DoubleClick);
			// 
			// clnHeader
			// 
			this.clnHeader.Text = "Message";
			this.clnHeader.Width = 556;
			// 
			// imageList
			// 
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.Location = new System.Drawing.Point(8, 288);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 20);
			this.label1.TabIndex = 3;
			this.label1.Text = "Total Messages:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtTotalMessages
			// 
			this.txtTotalMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtTotalMessages.Location = new System.Drawing.Point(104, 288);
			this.txtTotalMessages.Name = "txtTotalMessages";
			this.txtTotalMessages.ReadOnly = true;
			this.txtTotalMessages.Size = new System.Drawing.Size(64, 20);
			this.txtTotalMessages.TabIndex = 4;
			this.txtTotalMessages.Text = "0";
			// 
			// txtTotalErrors
			// 
			this.txtTotalErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtTotalErrors.Location = new System.Drawing.Point(280, 288);
			this.txtTotalErrors.Name = "txtTotalErrors";
			this.txtTotalErrors.ReadOnly = true;
			this.txtTotalErrors.Size = new System.Drawing.Size(64, 20);
			this.txtTotalErrors.TabIndex = 6;
			this.txtTotalErrors.Text = "0";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.Location = new System.Drawing.Point(184, 288);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 20);
			this.label2.TabIndex = 5;
			this.label2.Text = "Total Errors:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtTotalWarnings
			// 
			this.txtTotalWarnings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtTotalWarnings.Location = new System.Drawing.Point(456, 288);
			this.txtTotalWarnings.Name = "txtTotalWarnings";
			this.txtTotalWarnings.ReadOnly = true;
			this.txtTotalWarnings.Size = new System.Drawing.Size(64, 20);
			this.txtTotalWarnings.TabIndex = 8;
			this.txtTotalWarnings.Text = "0";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.Location = new System.Drawing.Point(360, 288);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 20);
			this.label3.TabIndex = 7;
			this.label3.Text = "Total Warnings:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ResultsForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnOK;
			this.ClientSize = new System.Drawing.Size(576, 357);
			this.Controls.Add(this.txtTotalWarnings);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtTotalErrors);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtTotalMessages);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lvwResults);
			this.Controls.Add(this.btnOK);
			this.MinimizeBox = false;
			this.Name = "ResultsForm";
			this.ShowInTaskbar = false;
			this.Text = "Results";
			this.SizeChanged += new System.EventHandler(this.ResultsForm_SizeChanged);
			this.HandleCreated += new System.EventHandler(this.ResultsForm_HandleCreated);
			this.ResumeLayout(false);

		}
		#endregion

		public void InvokeRaise(ConfigurationEvent configurationEvent, string message)
		{
			m_pendingResults.Add(new Result(configurationEvent, message));
			if ( m_canInvoke )
				Invoke(new MethodInvoker(Raise));
		}

		public void InvokeRaise(ConfigurationEvent configurationEvent, string message, System.Exception e)
		{
			if ( !m_complete )
			{
				m_pendingResults.Add(new Result(configurationEvent, message, e));
				if ( m_canInvoke )
					Invoke(new MethodInvoker(Raise));
			}
		}

		public void InvokeRaise(ConfigurationEvent configurationEvent, string message, string extraInfo)
		{
			m_pendingResults.Add(new Result(configurationEvent, message, extraInfo));
			if ( m_canInvoke )
				Invoke(new MethodInvoker(Raise));
		}

		public void InvokeComplete()
		{
			m_complete = true;
			if ( m_canInvoke )
				Invoke(new MethodInvoker(Complete));
		}

		private void Raise()
		{
			foreach ( Result result in m_pendingResults )
			{
				++m_totalMessages;
				int imageIndex;
				switch ( result.Event )
				{
					case ConfigurationEvent.Error:
						imageIndex = ImageIndexError;
						++m_totalErrors;
						break;

					case ConfigurationEvent.Warning:
						imageIndex = ImageIndexWarning;
						++m_totalWarnings;
						break;

					default:
						imageIndex = ImageIndexInformation;
						break;
				}

				ListViewItem item = new ListViewItem(result.Message, imageIndex);
				item.Tag = result;
				lvwResults.Items.Add(item);
			}

			txtTotalMessages.Text = m_totalMessages.ToString();
			txtTotalErrors.Text = m_totalErrors.ToString();
			txtTotalWarnings.Text = m_totalWarnings.ToString();

			m_pendingResults.Clear();
		}

		private void Complete()
		{
			// Set the OK button so the dialog can be dismissed.

			if ( m_complete )
				btnOK.Enabled = true;
		}

		private void ResultsForm_HandleCreated(object sender, System.EventArgs e)
		{
			m_canInvoke = true;

			// Invoke so that any messages already in place get processed.

			Raise();
			Complete();
		}

		private void lvwResults_DoubleClick(object sender, System.EventArgs e)
		{
			if ( lvwResults.SelectedItems.Count == 1 )
			{
				ListViewItem item = lvwResults.SelectedItems[0];

				// If there is an exception or extra information then show it.

				Result result = item.Tag as Result;
				if ( result != null )
				{
					if ( result.Exception != null )
					{
						new ExceptionDialog(result.Exception, "The following exception has occurred:").ShowDialog();
					}
					else if ( result.ExtraInfo != null )
					{
						EditorDialog dialog = new EditorDialog();
						dialog.CurrentEditorType = typeof(BasicTypeEditor);
						dialog.DisplayValue(result.ExtraInfo);
						dialog.ShowDialog();
					}
				}
			}
		}

		private void ResultsForm_SizeChanged(object sender, System.EventArgs e)
		{
			// Move the OK button to the centre.

			btnOK.Left = Width / 2 - btnOK.Width / 2;
		}
	}
}
