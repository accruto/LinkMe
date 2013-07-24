using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using LinkMe.Framework.Utility.Net;

namespace LinkMe.Framework.Tools.Net.Controls
{
	/// <summary>
	/// A control that allows editing a ClassInfo object.
	/// </summary>
	public class ClassInfoControl : UserControl
	{
		public event ClassInfoChangedEventHandler ClassInfoChanged;

		private ErrorProvider m_errorProvider;

		private System.Windows.Forms.TextBox txtClassInfo;
		private System.Windows.Forms.Button btnSelectClassInfo;
		private System.Windows.Forms.ToolTip toolTipClassInfo;
		private System.ComponentModel.IContainer components;

		public ClassInfoControl()
		{
			// This call is required by the Windows.Forms Form Designer.
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.txtClassInfo = new System.Windows.Forms.TextBox();
			this.btnSelectClassInfo = new System.Windows.Forms.Button();
			this.toolTipClassInfo = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// txtClassInfo
			// 
			this.txtClassInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtClassInfo.Location = new System.Drawing.Point(0, 0);
			this.txtClassInfo.Name = "txtClassInfo";
			this.txtClassInfo.Size = new System.Drawing.Size(272, 20);
			this.txtClassInfo.TabIndex = 2;
			this.txtClassInfo.Text = "";
			this.txtClassInfo.TextChanged += new System.EventHandler(this.txtClassInfo_TextChanged);
			// 
			// btnSelectClassInfo
			// 
			this.btnSelectClassInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelectClassInfo.Location = new System.Drawing.Point(295, 0);
			this.btnSelectClassInfo.Name = "btnSelectClassInfo";
			this.btnSelectClassInfo.Size = new System.Drawing.Size(25, 20);
			this.btnSelectClassInfo.TabIndex = 3;
			this.btnSelectClassInfo.Text = "...";
			this.btnSelectClassInfo.Click += new System.EventHandler(this.btnSelectClassInfo_Click);
			// 
			// ClassInfoControl
			// 
			this.Controls.Add(this.btnSelectClassInfo);
			this.Controls.Add(this.txtClassInfo);
			this.Name = "ClassInfoControl";
			this.Size = new System.Drawing.Size(320, 20);
			this.ResumeLayout(false);

		}
		#endregion

		public ErrorProvider ErrorProvider
		{
			get { return m_errorProvider; }
			set { m_errorProvider = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ClassInfo ClassInfo
		{
			get
			{
				return new ClassInfo(txtClassInfo.Text);
			}
			set
			{
				txtClassInfo.Text = value == null ? string.Empty : value.ToString();
				CheckIsValid();
			}
		}

		[Browsable(false)]
		public bool IsValid
		{
			get
			{
				if ( txtClassInfo.Text.Length == 0 )
					return true;

				try
				{
					new ClassInfo(txtClassInfo.Text);
					return true;
				}
				catch ( System.Exception )
				{
					return false;
				}
			}
		}

		public void SelectInvalid()
		{
			if ( !IsValid )
			{
				txtClassInfo.Select();
				txtClassInfo.SelectAll();
			}
		}

		private void CheckIsValid()
		{
			if ( m_errorProvider != null )
				m_errorProvider.SetError(txtClassInfo, GetErrorMessage());
		}

		private string GetErrorMessage()
		{
			if ( IsValid )
				return string.Empty;
			else
				return "The text cannot specify a .NET class.";
		}

		protected virtual void OnClassInfoChanged(ClassInfoChangedEventArgs e)
		{
			if ( ClassInfoChanged != null )
				ClassInfoChanged(this, e);
		}

		private void txtClassInfo_TextChanged(object sender, System.EventArgs e)
		{
			OnClassInfoChanged(new ClassInfoChangedEventArgs());
			CheckIsValid();
			toolTipClassInfo.SetToolTip(txtClassInfo, txtClassInfo.Text);
		}

		private void btnSelectClassInfo_Click(object sender, System.EventArgs e)
		{
			try
			{
				// First prompt for the assembly.

				System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
				openFileDialog.Filter = ".NET Assemblies (*.dll)|*.dll|All files (*.*)|*.*";
				openFileDialog.DefaultExt = "dll";
				if ( openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK )
				{
					string assemblyName = openFileDialog.FileName;
					System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(assemblyName);

					ObjectBrowser.Net.NetBrowserForm form = new ObjectBrowser.Net.NetBrowserForm();
					form.DisplayAssembly(assemblyName, false, false);
					if ( form.ShowDialog() == System.Windows.Forms.DialogResult.OK )
					{
						ObjectBrowser.Net.NetTypeBrowserInfo info = form.Browser.SelectedType as ObjectBrowser.Net.NetTypeBrowserInfo;
						if ( info != null )
						{
							ClassInfo classInfo = new ClassInfo(info.FullName, assembly.FullName);
							txtClassInfo.Text = classInfo.ToString();
						}
					}
				}
			}
			catch ( System.Exception ex )
			{
				new LinkMe.Framework.Tools.Controls.ExceptionDialog(ex, "Cannot select class").ShowDialog(this);
			}
		}
	}

	public class ClassInfoChangedEventArgs
		:	EventArgs
	{
	}

	public delegate void ClassInfoChangedEventHandler(object sender, ClassInfoChangedEventArgs e);
}
