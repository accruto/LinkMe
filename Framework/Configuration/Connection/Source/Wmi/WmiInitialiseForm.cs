using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Management;

using LinkMe.Framework.Tools;
using LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Configuration.Connection.Wmi
{
	public class WmiInitialiseForm : LinkMe.Framework.Tools.Controls.Form
	{
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private string m_namespace;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private LinkMe.Framework.Tools.Controls.TextBox txtMachineName;
		private LinkMe.Framework.Tools.Controls.TextBox txtStartingNamespace;
		private LinkMe.Framework.Tools.Controls.TabOrderManager tabOrderManager;
		private System.Windows.Forms.Button btnConnect;
		private LinkMe.Framework.Tools.Controls.TreeView tvwNamespaces;
		private System.Windows.Forms.ImageList imageListNamespaces;
		private System.ComponentModel.IContainer components;

		public WmiInitialiseForm(string ns)
		{
			m_namespace = (ns == null ? string.Empty : ns);

			InitializeComponent();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WmiInitialiseForm));
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtMachineName = new LinkMe.Framework.Tools.Controls.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtStartingNamespace = new LinkMe.Framework.Tools.Controls.TextBox();
			this.btnConnect = new System.Windows.Forms.Button();
			this.tvwNamespaces = new LinkMe.Framework.Tools.Controls.TreeView();
			this.imageListNamespaces = new System.Windows.Forms.ImageList(this.components);
			this.tabOrderManager = new LinkMe.Framework.Tools.Controls.TabOrderManager();
			((System.ComponentModel.ISupportInitialize)(this.tabOrderManager)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(240, 208);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(328, 208);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			// 
			// txtMachineName
			// 
			this.txtMachineName.Location = new System.Drawing.Point(128, 16);
			this.txtMachineName.Name = "txtMachineName";
			this.txtMachineName.Size = new System.Drawing.Size(136, 20);
			this.txtMachineName.TabIndex = 3;
			this.txtMachineName.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 20);
			this.label1.TabIndex = 4;
			this.label1.Text = "&Machine Name:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 20);
			this.label2.TabIndex = 5;
			this.label2.Text = "&Starting Namespace:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtStartingNamespace
			// 
			this.txtStartingNamespace.Location = new System.Drawing.Point(128, 48);
			this.txtStartingNamespace.Name = "txtStartingNamespace";
			this.txtStartingNamespace.Size = new System.Drawing.Size(136, 20);
			this.txtStartingNamespace.TabIndex = 6;
			this.txtStartingNamespace.Text = "";
			// 
			// btnConnect
			// 
			this.btnConnect.Location = new System.Drawing.Point(272, 48);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(128, 23);
			this.btnConnect.TabIndex = 7;
			this.btnConnect.Text = "&Connect";
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// tvwNamespaces
			// 
			this.tvwNamespaces.HideSelection = false;
			this.tvwNamespaces.ImageList = this.imageListNamespaces;
			this.tvwNamespaces.Location = new System.Drawing.Point(8, 80);
			this.tvwNamespaces.Name = "tvwNamespaces";
			this.tvwNamespaces.Size = new System.Drawing.Size(392, 120);
			this.tvwNamespaces.TabIndex = 8;
			// 
			// imageListNamespaces
			// 
			this.imageListNamespaces.ImageSize = new System.Drawing.Size(16, 16);
			this.imageListNamespaces.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListNamespaces.ImageStream")));
			this.imageListNamespaces.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tabOrderManager
			// 
			this.tabOrderManager.Control = this;
			// 
			// WmiInitialiseForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(416, 245);
			this.ControlBox = false;
			this.Controls.Add(this.tvwNamespaces);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.txtStartingNamespace);
			this.Controls.Add(this.txtMachineName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WmiInitialiseForm";
			this.Text = "Browse For Namespace";
			((System.ComponentModel.ISupportInitialize)(this.tabOrderManager)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		public string Namespace
		{
			get { return m_namespace; }
		}

		protected override void OnLoad(EventArgs e)
		{
			if ( m_namespace.Length == 0 )
			{
				// Set defaults.

				txtMachineName.Text = "\\\\" + System.Environment.MachineName;
				txtStartingNamespace.Text = "root";
			}
			else
			{
				// Parse it.

				if ( m_namespace.Substring(0, 2) == "\\\\" )
				{
					int pos = m_namespace.IndexOf("\\", 2);
					if ( pos == -1 )
					{
						if ( m_namespace.Substring(2) == "." )
							txtMachineName.Text = "\\\\" + System.Environment.MachineName;
						else
							txtMachineName.Text = m_namespace;
						txtStartingNamespace.Text = "root";
					}
					else
					{
						txtMachineName.Text = m_namespace.Substring(0, pos);
						txtStartingNamespace.Text = m_namespace.Substring(pos + 1);
					}
				}
				else
				{
					txtMachineName.Text = "\\\\" + System.Environment.MachineName;
					txtStartingNamespace.Text = m_namespace;
				}
			}

			base.OnLoad(e);
		}

		private void btnConnect_Click(object sender, System.EventArgs e)
		{
			// Fill in the tree view.

			tvwNamespaces.Nodes.Clear();

			try 
			{
				using (new LongRunningMonitor(this))
				{
					string path = txtMachineName.Text + "\\" + txtStartingNamespace.Text;
					if ( path.Substring(0, 2) != "\\\\" )
						path = "\\\\" + path;

					// Enumerate all WMI instances of __namespace WMI class.

					ManagementScope scope = new ManagementScope(path);
					scope.Connect();

					// Add a node for the starting namespace.

					string name;
					int pos = txtStartingNamespace.Text.LastIndexOf("\\");
					if ( pos == -1 )
						name = txtStartingNamespace.Text;
					else
						name = txtStartingNamespace.Text.Substring(pos + 1);

					TreeNode node = new TreeNode(name, 0, 0);
					tvwNamespaces.Nodes.Add(node);
					tvwNamespaces.SelectedNode = node;

					// Add nodes for each namespace.

					AddNamespaces(scope, node.Nodes);

					tvwNamespaces.Focus();
					btnOK.Enabled = true;
				}
			}
			catch ( System.Exception ex )
			{
				new ExceptionDialog(ex, "Cannot connect to the namespace.").ShowDialog();
				tvwNamespaces.Nodes.Clear();
			}
		}

		private void AddNamespaces(ManagementScope scope, TreeNodeCollection nodes)
		{
			// Add all namespaces to the list.

			SortedList list = new SortedList();
			ManagementClass nsClass = new ManagementClass(scope, new ManagementPath("__namespace"), null);

			using (ManagementObjectCollection instances = nsClass.GetInstances())
			{
				foreach ( ManagementObject ns in instances )
					list.Add(ns["Name"], null);
			}

			foreach ( string name in list.Keys )
			{
				// Add a node.

				TreeNode node = new TreeNode(name, 0, 0);
				nodes.Add(node);

				// Iterate.

				ManagementScope nsScope = new ManagementScope(scope.Path + "\\" + name);
				nsScope.Connect();
				AddNamespaces(nsScope, node.Nodes);
			}
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			// Extract the information from the tree view.

			string ns = string.Empty;
			if ( tvwNamespaces.SelectedNode != tvwNamespaces.Nodes[0] )
				ns = GetNamespace(tvwNamespaces.SelectedNode);

			// Build up the full namespace.

			m_namespace = string.Empty;
			if ( txtMachineName.Text.Substring(0, 2) != "\\\\" )
				m_namespace += "\\\\";
			m_namespace += txtMachineName.Text;
			m_namespace += "\\" + txtStartingNamespace.Text;
			if ( ns.Length != 0 )
				m_namespace += "\\" + ns;
		}

		private string GetNamespace(TreeNode node)
		{
			string name = node.Text;
			if ( node.Parent != tvwNamespaces.Nodes[0] )
				name = GetNamespace(node.Parent) + "\\" + name;
			return name;
		}
	}
}
