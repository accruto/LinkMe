using System;
using System.ComponentModel;
using System.Windows.Forms;

using LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Type.Tools.Controls
{
	/// <summary>
	/// Summary description for DefaultControl.
	/// </summary>
	public class NullablePrimitiveValueControl
		:	System.Windows.Forms.UserControl,
		IReadOnlySettable
    {
		private System.Windows.Forms.CheckBox chkIsNull;
		private System.Windows.Forms.Label label1;
		private LinkMe.Framework.Tools.Controls.TextBox txtValue;
		public event ValueChangedEventHandler ValueChanged;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private object m_value;
		private bool m_isChanging;
		private bool m_isNullable = true;
		private PrimitiveType m_primitiveType;
		private ErrorProvider m_errorProvider;
		private bool m_isReadOnly;

        public NullablePrimitiveValueControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			m_primitiveType = LinkMe.Framework.Type.PrimitiveTypeInfo.String.PrimitiveType;
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
            this.chkIsNull = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtValue = new LinkMe.Framework.Tools.Controls.TextBox();
            this.SuspendLayout();
            // 
            // chkIsNull
            // 
            this.chkIsNull.Location = new System.Drawing.Point(5, 8);
            this.chkIsNull.Name = "chkIsNull";
            this.chkIsNull.Size = new System.Drawing.Size(80, 16);
            this.chkIsNull.TabIndex = 1;
            this.chkIsNull.Text = "Is Null";
            this.chkIsNull.CheckedChanged += new System.EventHandler(this.chkIsNull_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Value:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtValue
            // 
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.Location = new System.Drawing.Point(48, 32);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(304, 20);
            this.txtValue.TabIndex = 2;
            this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
            // 
            // NullablePrimitiveValueControl
            // 
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkIsNull);
            this.Name = "NullablePrimitiveValueControl";
            this.Size = new System.Drawing.Size(376, 56);
            this.Load += new System.EventHandler(this.DefaultControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public ErrorProvider ErrorProvider
		{
			get { return m_errorProvider; }
			set { m_errorProvider = value; }
		}

		[DefaultValue(false)]
		public bool ReadOnly
		{
			get
			{
				return m_isReadOnly;
			}
			set
			{
				m_isReadOnly = value;
				UpdateDisplay();
			}
		}

		[DefaultValue(true)]
		public bool IsNullable
		{
			get
			{
				return m_isNullable;
			}
			set
			{
				m_isNullable = value;
				UpdateDisplay();
			}
		}

		public void SetPrimitiveType(PrimitiveType primitiveType)
		{
			m_primitiveType = primitiveType;
			CheckIsValid();
		}

		public void SetValue(object value)
		{
			m_value = TypeClone.Clone(value);
			CheckIsValid();

			m_isChanging = true; // prevent event firing
			try
			{
				UpdateDisplay();
			}
			finally
			{
				m_isChanging = false;
			}
		}

		public void GetDefault(out object value)
		{
            value = TypeClone.Clone(m_value);
		}

		public override bool Equals(object value)
		{
			return Object.Equals(m_value, value);
		}

        public override int GetHashCode()
        {
            return m_value == null ? 0 : m_value.GetHashCode();
        }

		public bool IsValid
		{
			get { return CanConvertToSelectedType(); }
		}

		public void SelectInvalid()
		{
			if ( !IsValid )
			{
				if ( txtValue.CanSelect )
				{
					txtValue.Select();
					txtValue.SelectAll();
				}
			}
		}

		private void chkIsNull_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( !m_isChanging )
			{
				m_isChanging = true;
				try
				{
					m_value = chkIsNull.Checked ? null : LinkMe.Framework.Type.PrimitiveTypeInfo.GetPrimitiveTypeInfo(m_primitiveType).Default;
					UpdateDisplay();
					OnDefaultChanged(new ValueChangedEventArgs());
				}
				finally
				{
					m_isChanging = false;
				}
			}
		}

		private void txtValue_TextChanged(object sender, System.EventArgs e)
		{
			if ( !m_isChanging )
			{
				m_isChanging = true;
				try
				{
					m_value = txtValue.Text;
					UpdateDisplay();
					OnDefaultChanged(new ValueChangedEventArgs());
				}
				finally
				{
					m_isChanging = false;
				}
			}
		}

		private void UpdateDisplay()
		{
            UpdateIsSetDisplay();

			CheckIsValid();

			// The read only flag will override everything.

			if ( m_isReadOnly )
			{
				chkIsNull.Enabled = false;
				txtValue.ReadOnly = true;
			}
		}

		private void UpdateIsSetDisplay()
		{
			if ( m_isNullable )
			{
				if ( m_value == null )
				{
					chkIsNull.Checked = true;
					txtValue.Text = string.Empty;

					chkIsNull.Enabled = true;
					txtValue.ReadOnly = true;
				}
				else
				{
					chkIsNull.Checked = false;
					txtValue.Text = TypeXmlConvert.ToString(m_value);

					chkIsNull.Enabled = true;
					txtValue.ReadOnly = false;
				}
			}
			else
			{
				chkIsNull.Visible = false;
				txtValue.Text = TypeXmlConvert.ToString(m_value);
				txtValue.ReadOnly = false;
			}
		}

		private void DefaultControl_Load(object sender, System.EventArgs e)
		{
			UpdateDisplay();
		}

		private void CheckIsValid()
		{
			if ( m_errorProvider != null )
				m_errorProvider.SetError(txtValue, GetErrorMessage());
		}

		private bool CanConvertToSelectedType()
		{
			if ( m_value == null )
				return true;
			else if (m_value is string)
				return TypeXmlConvert.CanConvert((string)m_value, m_primitiveType);
			else
				return TypeConvert.CanConvert(m_value, m_primitiveType);
		}

		private string GetErrorMessage()
		{
			if ( CanConvertToSelectedType() )
				return string.Empty;
			else
				return "The text cannot be converted to the '" + m_primitiveType.ToString() + "' type.";
		}


		protected virtual void OnDefaultChanged(ValueChangedEventArgs e)
		{
			if ( ValueChanged != null )
				ValueChanged(this, e);
		}
	}

	public class ValueChangedEventArgs
		:	EventArgs
	{
	}

	public delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs e);
}
