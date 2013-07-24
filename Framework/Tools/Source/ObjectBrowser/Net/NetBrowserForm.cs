using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

using LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Tools.ObjectBrowser.Net
{
	/// <summary>
	/// A dialog that displays the Component Catalogue object browser.
	/// </summary>
	public class NetBrowserForm
		:	Dialog
	{
		private ObjectBrowser browser;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NetBrowserForm()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NetBrowserForm));
			this.browser = new LinkMe.Framework.Tools.ObjectBrowser.ObjectBrowser();
			this.SuspendLayout();
			// 
			// browser
			// 
			this.browser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.browser.Location = new System.Drawing.Point(8, 8);
			this.browser.MinimumSize = new System.Drawing.Size(0, 0);
			this.browser.Name = "browser";
			this.browser.Size = new System.Drawing.Size(736, 360);
			this.browser.TabIndex = 0;
			this.browser.TypeSelectionChanged += new System.EventHandler(this.browser_TypeSelectionChanged);
			// 
			// NetBrowserForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(752, 421);
			this.Controls.Add(this.browser);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "NetBrowserForm";
			this.ShowInTaskbar = false;
			this.Text = ".NET Assembly Browser";
			this.ResumeLayout(false);

		}
		#endregion

		public ObjectBrowser Browser
		{
			get { return browser; }
		}

		public void DisplayAssembly(string filePath, bool showMembers, bool showCheckBoxes)
		{
			if (filePath == null)
				throw new ArgumentNullException("filePath");

			NetBrowserManager manager = new NetBrowserManager();

			browser.Settings = new NetBrowserSettings(showMembers, showCheckBoxes, Net.Language.CSharp);
			browser.Manager = manager;
			browser.Repositories = new IRepositoryBrowserInfo[] { manager.GetAssemblyInfo(filePath) };

			SetButtonEnabled(DialogResult.OK, false);
		}

		private void browser_TypeSelectionChanged(object sender, EventArgs e)
		{
			SetButtonEnabled(DialogResult.OK, browser.SelectedType is NetTypeBrowserInfo);
		}
	}
}
