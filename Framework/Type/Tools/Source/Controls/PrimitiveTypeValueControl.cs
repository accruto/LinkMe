using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace LinkMe.Framework.Type.Tools.Controls
{
	/// <summary>
	/// Summary description for PrimitiveTypeValueControl.
	/// </summary>
	public class PrimitiveTypeValueControl
		:	LinkMe.Framework.Tools.Controls.UserControl
	{
		private bool m_isChanging;
		private ErrorProvider m_errorProvider;

		private System.Windows.Forms.Label label1;
		private PrimitiveTypeControl cboType;
		private System.Windows.Forms.Label label2;
		private LinkMe.Framework.Type.Tools.Controls.PrimitiveValueControl primitiveValueControl;
		public event EventHandler PrimitiveTypeChanged;
		public event EventHandler ValueChanged;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PrimitiveTypeValueControl()
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
			this.label1 = new System.Windows.Forms.Label();
			this.cboType = new LinkMe.Framework.Type.Tools.Controls.PrimitiveTypeControl();
			this.label2 = new System.Windows.Forms.Label();
			this.primitiveValueControl = new LinkMe.Framework.Type.Tools.Controls.PrimitiveValueControl();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Type:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboType
			// 
			this.cboType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboType.Location = new System.Drawing.Point(48, 0);
			this.cboType.Name = "cboType";
			this.cboType.Size = new System.Drawing.Size(304, 21);
			this.cboType.TabIndex = 1;
			this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(0, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "Value:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// primitiveValueControl
			// 
			this.primitiveValueControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.primitiveValueControl.ErrorProvider = null;
			this.primitiveValueControl.Location = new System.Drawing.Point(48, 32);
			this.primitiveValueControl.MinimumSize = new System.Drawing.Size(0, 0);
			this.primitiveValueControl.Name = "primitiveValueControl";
			this.primitiveValueControl.RegularExpression = "";
			this.primitiveValueControl.Size = new System.Drawing.Size(304, 21);
			this.primitiveValueControl.TabIndex = 3;
			this.primitiveValueControl.ValueChanged += new System.EventHandler(this.primitiveValueControl_ValueChanged);
			// 
			// PrimitiveTypeValueControl
			// 
			this.Controls.Add(this.primitiveValueControl);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cboType);
			this.Controls.Add(this.label1);
			this.Name = "PrimitiveTypeValueControl";
			this.Size = new System.Drawing.Size(376, 56);
			this.ResumeLayout(false);

		}
		#endregion

		public ErrorProvider ErrorProvider
		{
			get
			{
				return m_errorProvider;
			}
			set
			{
				m_errorProvider = value;
				primitiveValueControl.ErrorProvider = value;
			}
		}

		[Browsable(false)]
		public bool IsValid
		{
			get { return primitiveValueControl.IsValid; }
		}

		public PrimitiveType PrimitiveType
		{
			get
			{
				return primitiveValueControl.PrimitiveType;
			}
			set
			{
				primitiveValueControl.PrimitiveType = value;
				UpdateDisplay();
			}
		}

		public object Value
		{
			get { return primitiveValueControl.Value; }
			set { primitiveValueControl.Value = value; }
		}

		public bool EqualsValue(object value)
		{
			return primitiveValueControl.EqualsValue(value);
		}

		public void SelectInvalid()
		{
			primitiveValueControl.SelectInvalid();
		}

		private void UpdateDisplay()
		{
			// Set the primitive type.

			cboType.PrimitiveType = primitiveValueControl.PrimitiveType;
			CheckIsValid();
		}

		private void CheckIsValid()
		{
		}

		protected virtual void OnPrimitiveTypeChanged(EventArgs e)
		{
			if ( PrimitiveTypeChanged != null )
				PrimitiveTypeChanged(this, e);
		}

		protected virtual void OnValueChanged(EventArgs e)
		{
			if ( ValueChanged != null )
				ValueChanged(this, e);
		}

		private void cboType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( !m_isChanging )
			{
				m_isChanging = true;
				try
				{
					primitiveValueControl.PrimitiveType = cboType.PrimitiveType;

					UpdateDisplay();
					OnPrimitiveTypeChanged(EventArgs.Empty);
				}
				finally
				{
					m_isChanging = false;
				}
			}
		}

		private void primitiveValueControl_ValueChanged(object sender, System.EventArgs e)
		{
			if ( !m_isChanging )
			{
				m_isChanging = true;
				try
				{
					UpdateDisplay();
					OnValueChanged(EventArgs.Empty);
				}
				finally
				{
					m_isChanging = false;
				}
			}
		}
	}
}
