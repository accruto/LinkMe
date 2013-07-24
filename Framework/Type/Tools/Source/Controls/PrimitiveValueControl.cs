using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using Win32 = LinkMe.Framework.Utility.Win32;

namespace LinkMe.Framework.Type.Tools.Controls
{
	/// <summary>
	/// Summary description for PrimitiveTypeValueControl.
	/// </summary>
	public class PrimitiveValueControl
		:	LinkMe.Framework.Tools.Controls.UserControl
	{
		private string m_value;
		private bool m_isChanging;
		private PrimitiveType m_primitiveType;
		private ErrorProvider m_errorProvider;
		private string m_regularExpression = string.Empty;
		private bool m_required;
		private Regex m_regex;
		private Control[] m_controls;
		private PrimitiveValueTextBox txtValue;
		private LinkMe.Framework.Type.Tools.Controls.DateTimePicker dateTimePicker;
		private LinkMe.Framework.Type.Tools.Controls.DatePicker datePicker;
		private LinkMe.Framework.Type.Tools.Controls.TimeOfDayPicker timeOfDayPicker;
		private LinkMe.Framework.Type.Tools.Controls.TimeSpanPicker timeSpanPicker;
		private LinkMe.Framework.Tools.Editors.GuidEditor guidEditor;
		private LinkMe.Framework.Tools.Editors.BooleanEditor booleanEditor;

		public event EventHandler ValueChanged;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PrimitiveValueControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			m_primitiveType = PrimitiveTypeInfo.String.PrimitiveType;
			m_value = string.Empty;

			dateTimePicker.Format = DateTimePickerFormat.Custom;
			dateTimePicker.CustomFormat = Constants.DateTime.ParseFormat;

			datePicker.Format = DatePickerFormat.Custom;
			datePicker.CustomFormat = Constants.Date.ParseFormat;

			timeOfDayPicker.Format = TimeOfDayPickerFormat.Custom;
			timeOfDayPicker.CustomFormat = Constants.TimeOfDay.ParseFormat;

			timeSpanPicker.Format = TimeSpanPickerFormat.Custom;
			timeSpanPicker.CustomFormat = Constants.TimeSpan.ParseFormat;

			// Make all controls invisible to start with except for the text box.

			m_controls = new Control[] { txtValue, dateTimePicker, datePicker, timeOfDayPicker, timeSpanPicker, guidEditor, booleanEditor };
			foreach ( Control control in m_controls )
				control.Visible = false;
			txtValue.Visible = true;
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
			this.txtValue = new LinkMe.Framework.Type.Tools.Controls.PrimitiveValueControl.PrimitiveValueTextBox();
			this.dateTimePicker = new LinkMe.Framework.Type.Tools.Controls.DateTimePicker();
			this.datePicker = new LinkMe.Framework.Type.Tools.Controls.DatePicker();
			this.timeOfDayPicker = new LinkMe.Framework.Type.Tools.Controls.TimeOfDayPicker();
			this.timeSpanPicker = new LinkMe.Framework.Type.Tools.Controls.TimeSpanPicker();
			this.guidEditor = new LinkMe.Framework.Tools.Editors.GuidEditor();
			this.booleanEditor = new LinkMe.Framework.Tools.Editors.BooleanEditor();
			this.SuspendLayout();
			// 
			// txtValue
			// 
			this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtValue.IgnoreUpKey = false;
			this.txtValue.Location = new System.Drawing.Point(0, 0);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(287, 20);
			this.txtValue.TabIndex = 3;
			this.txtValue.Text = "";
			this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
			// 
			// dateTimePicker
			// 
			this.dateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dateTimePicker.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dateTimePicker.CalendarForeColor = System.Drawing.SystemColors.ControlText;
			this.dateTimePicker.CalendarMonthBackground = System.Drawing.SystemColors.Window;
			this.dateTimePicker.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.dateTimePicker.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.dateTimePicker.CalendarTrailingForeColor = System.Drawing.SystemColors.GrayText;
			this.dateTimePicker.CustomFormat = "dd/MM/yyyy h:mm:ss.n tt";
			this.dateTimePicker.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.dateTimePicker.Format = LinkMe.Framework.Type.Tools.Controls.DateTimePickerFormat.Long;
			this.dateTimePicker.Location = new System.Drawing.Point(0, 0);
			this.dateTimePicker.Name = "dateTimePicker";
			this.dateTimePicker.Size = new System.Drawing.Size(304, 20);
			this.dateTimePicker.TabIndex = 6;
			this.dateTimePicker.Visible = false;
			this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
			// 
			// datePicker
			// 
			this.datePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.datePicker.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.datePicker.CalendarForeColor = System.Drawing.SystemColors.ControlText;
			this.datePicker.CalendarMonthBackground = System.Drawing.SystemColors.Window;
			this.datePicker.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.datePicker.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.datePicker.CalendarTrailingForeColor = System.Drawing.SystemColors.GrayText;
			this.datePicker.CustomFormat = "dd/MM/yyyy";
			this.datePicker.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.datePicker.Format = LinkMe.Framework.Type.Tools.Controls.DatePickerFormat.Custom;
			this.datePicker.Location = new System.Drawing.Point(0, 0);
			this.datePicker.Name = "datePicker";
			this.datePicker.Size = new System.Drawing.Size(304, 20);
			this.datePicker.TabIndex = 6;
			this.datePicker.Visible = false;
			this.datePicker.ValueChanged += new System.EventHandler(this.datePicker_ValueChanged);
			// 
			// timeOfDayPicker
			// 
			this.timeOfDayPicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.timeOfDayPicker.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.timeOfDayPicker.CalendarForeColor = System.Drawing.SystemColors.ControlText;
			this.timeOfDayPicker.CalendarMonthBackground = System.Drawing.SystemColors.Window;
			this.timeOfDayPicker.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.timeOfDayPicker.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.timeOfDayPicker.CalendarTrailingForeColor = System.Drawing.SystemColors.GrayText;
			this.timeOfDayPicker.CustomFormat = "h:mm:ss.n tt";
			this.timeOfDayPicker.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.timeOfDayPicker.Format = LinkMe.Framework.Type.Tools.Controls.TimeOfDayPickerFormat.Custom;
			this.timeOfDayPicker.Location = new System.Drawing.Point(0, 0);
			this.timeOfDayPicker.Name = "timeOfDayPicker";
			this.timeOfDayPicker.ShowUpDown = true;
			this.timeOfDayPicker.Size = new System.Drawing.Size(304, 20);
			this.timeOfDayPicker.TabIndex = 6;
			this.timeOfDayPicker.Visible = false;
			this.timeOfDayPicker.ValueChanged += new System.EventHandler(this.timeOfDayPicker_ValueChanged);
			// 
			// timeSpanPicker
			// 
			this.timeSpanPicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.timeSpanPicker.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.timeSpanPicker.CalendarForeColor = System.Drawing.SystemColors.ControlText;
			this.timeSpanPicker.CalendarMonthBackground = System.Drawing.SystemColors.Window;
			this.timeSpanPicker.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.timeSpanPicker.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.timeSpanPicker.CalendarTrailingForeColor = System.Drawing.SystemColors.GrayText;
			this.timeSpanPicker.CustomFormat = "d HH:mm:ss.n";
			this.timeSpanPicker.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.timeSpanPicker.Format = LinkMe.Framework.Type.Tools.Controls.TimeSpanPickerFormat.Custom;
			this.timeSpanPicker.Location = new System.Drawing.Point(0, 0);
			this.timeSpanPicker.Name = "timeSpanPicker";
			this.timeSpanPicker.ShowUpDown = true;
			this.timeSpanPicker.Size = new System.Drawing.Size(304, 20);
			this.timeSpanPicker.TabIndex = 6;
			this.timeSpanPicker.Visible = false;
			this.timeSpanPicker.ValueChanged += new System.EventHandler(this.timeSpanPicker_ValueChanged);
			// 
			// guidEditor
			// 
			this.guidEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.guidEditor.Location = new System.Drawing.Point(0, 0);
			this.guidEditor.MinimumSize = new System.Drawing.Size(272, 20);
			this.guidEditor.Name = "guidEditor";
			this.guidEditor.Size = new System.Drawing.Size(304, 20);
			this.guidEditor.TabIndex = 7;
			this.guidEditor.Visible = false;
			this.guidEditor.ValueChanged += new System.EventHandler(this.guidEditor_ValueChanged);
			// 
			// booleanEditor
			// 
			this.booleanEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.booleanEditor.Location = new System.Drawing.Point(0, 0);
			this.booleanEditor.MinimumSize = new System.Drawing.Size(0, 0);
			this.booleanEditor.Name = "booleanEditor";
			this.booleanEditor.Size = new System.Drawing.Size(304, 21);
			this.booleanEditor.TabIndex = 8;
			this.booleanEditor.ValueChanged += new System.EventHandler(this.booleanEditor_ValueChanged);
			// 
			// PrimitiveValueControl
			// 
			this.Controls.Add(this.dateTimePicker);
			this.Controls.Add(this.datePicker);
			this.Controls.Add(this.timeOfDayPicker);
			this.Controls.Add(this.timeSpanPicker);
			this.Controls.Add(this.guidEditor);
			this.Controls.Add(this.booleanEditor);
			this.Controls.Add(this.txtValue);
			this.Name = "PrimitiveValueControl";
			this.Size = new System.Drawing.Size(304, 21);
			this.ResumeLayout(false);

		}
		#endregion

		public ErrorProvider ErrorProvider
		{
			get { return m_errorProvider; }
			set { m_errorProvider = value; }
		}

		public bool IsReadOnly
		{
			get
			{
				return txtValue.ReadOnly;
			}
			set
			{
				txtValue.ReadOnly = value;
//				dateTimePicker.IsReadOnly = value;
//				datePicker.IsReadOnly = value;
//				timeOfDayPicker.IsReadOnly = value;
//				timeSpanPicker.IsReadOnly = value;
				guidEditor.ReadOnly = value;
				booleanEditor.ReadOnly = value;
			}
		}

		[Browsable(false)]
		public bool IsValid
		{
			get
			{
				// Check whether it is required.

				if ( m_required && (m_value == null || m_value == string.Empty) )
					return false;

				// Check that the value can be converted to the primitive type.

				if ( !TypeXmlConvert.CanConvert(m_value, m_primitiveType) )
					return false;

				// Check the regular expression if there is one.

				if ( m_regularExpression != string.Empty && m_regex != null )
					return m_regex.IsMatch(m_value == null ? string.Empty : m_value.ToString());

				return true;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PrimitiveType PrimitiveType
		{
			get
			{
				return m_primitiveType;
			}
			set
			{
				m_primitiveType = value;

				Update();
				m_isChanging = true; // prevent event firing
				try
				{
					UpdateDisplay();

					switch ( m_primitiveType )
					{
						case PrimitiveType.Boolean:
							m_value = TypeXmlConvert.ToString(booleanEditor.Value);
							break;

						case PrimitiveType.DateTime:
							m_value = TypeXmlConvert.ToString(dateTimePicker.Value);
							break;

						case PrimitiveType.Date:
							m_value = TypeXmlConvert.ToString(datePicker.Value);
							break;

						case PrimitiveType.TimeOfDay:
							m_value = TypeXmlConvert.ToString(timeOfDayPicker.Value);
							break;
					
						case PrimitiveType.TimeSpan:
							m_value = TypeXmlConvert.ToString(timeSpanPicker.Value);
							break;
					}

					OnValueChanged(EventArgs.Empty);
				}
				finally
				{
					m_isChanging = false;
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object Value
		{
			get
			{
				return TypeXmlConvert.ToType(m_value, m_primitiveType);
			}
			set
			{
				m_value = value == null ? string.Empty : TypeXmlConvert.ToString(value);

				Update();
				m_isChanging = true; // prevent event firing
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

		public string RegularExpression
		{
			get
			{
				return m_regularExpression;
			}
			set
			{
				m_regularExpression = value == null ? string.Empty : value;
				if ( m_regularExpression != string.Empty )
					m_regex = new Regex(m_regularExpression, RegexOptions.Compiled);
			}
		}

		public void SetRequired(bool required)
		{
			m_required = required;
		}

		public bool EqualsValue(object value)
		{
			if ( !IsValid )
				return false;
			return Object.Equals(TypeXmlConvert.ToType(m_value, m_primitiveType), value);
		}

		public void SelectInvalid()
		{
			if ( !IsValid )
			{
				txtValue.Select();
				txtValue.SelectAll();
			}
		}

		public void SelectValue()
		{
			// Based on the type display select what is necessary.

			switch ( m_primitiveType )
			{
				case PrimitiveType.Boolean:
					break;

				case PrimitiveType.DateTime:
					break;

				case PrimitiveType.Date:
					break;

				case PrimitiveType.TimeOfDay:
					break;

				case PrimitiveType.TimeSpan:
					break;

				case PrimitiveType.Guid:
					SelectGuidValue();
					break;

				default:
					SelectDefaultValue();
					break;
			}
		}

		[Browsable(false)]
		internal bool IgnoreUpKey
		{
			get
			{
				return txtValue.IgnoreUpKey;
			}
			set
			{
				txtValue.IgnoreUpKey = value;
				dateTimePicker.IgnoreUpKey = value;
				datePicker.IgnoreUpKey = value;
				timeOfDayPicker.IgnoreUpKey = value;
				timeSpanPicker.IgnoreUpKey = value;
				guidEditor.IgnoreUpKey = value;
				booleanEditor.IgnoreUpKey = value;
			}
		}

		private void UpdateDisplay()
		{
			// Based on that type display what is necessary.

			switch ( m_primitiveType )
			{
				case PrimitiveType.Boolean:
					UpdateBooleanDisplay();
					break;

				case PrimitiveType.DateTime:
					UpdateDateTimeDisplay();
					break;

				case PrimitiveType.Date:
					UpdateDateDisplay();
					break;

				case PrimitiveType.TimeOfDay:
					UpdateTimeOfDayDisplay();
					break;

				case PrimitiveType.TimeSpan:
					UpdateTimeSpanDisplay();
					break;

				case PrimitiveType.Guid:
					UpdateGuidDisplay();
					break;

				default:
					UpdateDefaultDisplay();
					break;
			}

			// Check that it is all valid and up[date the display.

			CheckIsValid();
		}

		private void UpdateBooleanDisplay()
		{
			EnsureVisible(booleanEditor);
			if ( TypeXmlConvert.CanConvert(m_value, PrimitiveType.Boolean) )
				booleanEditor.Value = TypeXmlConvert.ToBoolean(m_value);
			else
				booleanEditor.Value = true;
		}

		private void UpdateDateTimeDisplay()
		{
			EnsureVisible(dateTimePicker);
			if ( TypeXmlConvert.CanConvert(m_value, PrimitiveType.DateTime) )
				dateTimePicker.Value = TypeXmlConvert.ToDateTime(m_value);
			else
				dateTimePicker.Value = DateTimePicker.MinDateTime;
		}

		private void UpdateDateDisplay()
		{
			EnsureVisible(datePicker);
			if ( TypeXmlConvert.CanConvert(m_value, PrimitiveType.Date) )
				datePicker.Value = TypeXmlConvert.ToDate(m_value);
			else
				datePicker.Value = DatePicker.MinDate;
		}

		private void UpdateTimeOfDayDisplay()
		{
			EnsureVisible(timeOfDayPicker);
			if ( TypeXmlConvert.CanConvert(m_value, PrimitiveType.TimeOfDay) )
				timeOfDayPicker.Value = TypeXmlConvert.ToTimeOfDay(m_value);
			else
				timeOfDayPicker.Value = new LinkMe.Framework.Type.TimeOfDay();
		}

		private void UpdateTimeSpanDisplay()
		{
			EnsureVisible(timeSpanPicker);
			if ( TypeXmlConvert.CanConvert(m_value, PrimitiveType.TimeSpan) )
				timeSpanPicker.Value = TypeXmlConvert.ToTimeSpan(m_value);
			else
				timeSpanPicker.Value = new LinkMe.Framework.Type.TimeSpan();
		}

		private void UpdateGuidDisplay()
		{
			EnsureVisible(guidEditor);
			if ( TypeXmlConvert.CanConvert(m_value, PrimitiveType.Guid) )
				guidEditor.Value = TypeXmlConvert.ToGuid(m_value);
		}

		private void SelectGuidValue()
		{
			if ( guidEditor.Visible )
				guidEditor.SelectValue();
		}

		private void UpdateDefaultDisplay()
		{
			EnsureVisible(txtValue);
			if ( TypeXmlConvert.CanConvert(m_value, m_primitiveType) )
			{
				object value = TypeXmlConvert.ToType(m_value, m_primitiveType);
				txtValue.Text = TypeXmlConvert.ToString(value);
			}
			else
			{
				txtValue.Text = m_value;
			}
		}

		private void SelectDefaultValue()
		{
			if ( txtValue.Visible )
			{
				txtValue.Select();
				txtValue.SelectAll();
			}
		}

		private void EnsureVisible(Control visibleControl)
		{
			// Check whether it is already visible.

			if ( visibleControl.Visible )
				return;

			// Find the visible control.

			foreach ( Control control in m_controls )
			{
				if ( control.Visible )
				{
					// Make it not visible.

					control.Visible = false;
					visibleControl.Visible = true;
					break;
				}
			}
		}

		public void CheckIsValid()
		{
			if ( m_errorProvider != null )
			{
				try
				{
					m_errorProvider.SetError(txtValue, GetErrorMessage());
				}
				catch ( System.Exception )
				{
					// Don't allow exceptions to affect the control.
				}
			}
		}

		public void ClearIsValid()
		{
			if ( m_errorProvider != null )
			{
				try
				{
					m_errorProvider.SetError(txtValue, string.Empty);
				}
				catch ( System.Exception )
				{
					// Don't allow exceptions to affect the control.
				}
			}
		}

		private string GetErrorMessage()
		{
			// Check required.

			if ( m_required && (m_value == null || m_value == string.Empty) )
				return "A value is required.";

			if ( !TypeXmlConvert.CanConvert(m_value, m_primitiveType) )
				return "The text cannot be converted to the '" + m_primitiveType.ToString() + "' type.";

			// Check the regular expression if there is one.

			if ( m_regularExpression != string.Empty && m_regex != null )
			{
				if ( !m_regex.IsMatch(m_value == null ? string.Empty : m_value.ToString()) )
					return "The value does not match the regular expression '" + m_regularExpression + "'.";
			}

			// All is OK.

			return string.Empty;
		}

		protected virtual void OnValueChanged(EventArgs e)
		{
			if ( ValueChanged != null )
				ValueChanged(this, e);
		}

		private void txtValue_TextChanged(object sender, System.EventArgs e)
		{
			if ( !m_isChanging )
			{
				m_isChanging = true;
				try
				{
					m_value = txtValue.Text;
					CheckIsValid();
					OnValueChanged(EventArgs.Empty);
				}
				finally
				{
					m_isChanging = false;
				}
			}
		}

		private void dateTimePicker_ValueChanged(object sender, System.EventArgs e)
		{
			if ( !m_isChanging )
			{
				m_isChanging = true;
				try
				{
					m_value = TypeXmlConvert.ToString(dateTimePicker.Value);
					CheckIsValid();
					OnValueChanged(EventArgs.Empty);
				}
				finally
				{
					m_isChanging = false;
				}
			}
		}

		private void datePicker_ValueChanged(object sender, System.EventArgs e)
		{
			if ( !m_isChanging )
			{
				m_isChanging = true;
				try
				{
					m_value = TypeXmlConvert.ToString(datePicker.Value);
					CheckIsValid();
					OnValueChanged(EventArgs.Empty);
				}
				finally
				{
					m_isChanging = false;
				}
			}
		}
	
		private void timeOfDayPicker_ValueChanged(object sender, System.EventArgs e)
		{
			if ( !m_isChanging )
			{
				m_isChanging = true;
				try
				{
					m_value = TypeXmlConvert.ToString(timeOfDayPicker.Value);
					CheckIsValid();
					OnValueChanged(EventArgs.Empty);
				}
				finally
				{
					m_isChanging = false;
				}
			}
		}
	
		private void timeSpanPicker_ValueChanged(object sender, System.EventArgs e)
		{
			if ( !m_isChanging )
			{
				m_isChanging = true;
				try
				{
					m_value = TypeXmlConvert.ToString(timeSpanPicker.Value);
					CheckIsValid();
					OnValueChanged(EventArgs.Empty);
				}
				finally
				{
					m_isChanging = false;
				}
			}
		}

		private void guidEditor_ValueChanged(object sender, System.EventArgs e)
		{
			if ( !m_isChanging )
			{
				m_isChanging = true;
				try
				{
					m_value = TypeXmlConvert.ToString(guidEditor.Text);
					CheckIsValid();
					OnValueChanged(EventArgs.Empty);
				}
				finally
				{
					m_isChanging = false;
				}
			}
		}

		private void booleanEditor_ValueChanged(object sender, System.EventArgs e)
		{
			if ( !m_isChanging )
			{
				m_isChanging = true;
				try
				{
					m_value = TypeXmlConvert.ToString(booleanEditor.Value);
					CheckIsValid();
					OnValueChanged(EventArgs.Empty);
				}
				finally
				{
					m_isChanging = false;
				}
			}
		}

		protected override void OnFontChanged(EventArgs e)
		{
			// Update the font of the controls.

			txtValue.Font = Font;
			dateTimePicker.Font = Font;
			datePicker.Font = Font;
			timeOfDayPicker.Font = Font;
			timeSpanPicker.Font = Font;
			guidEditor.Font = Font;
			booleanEditor.Font = Font;
			base.OnFontChanged(e);
		}

		private class PrimitiveValueTextBox
			:	LinkMe.Framework.Tools.Controls.TextBox
		{
			protected override void WndProc(ref System.Windows.Forms.Message m)
			{
				if ( m.Msg == Constants.Win32.Messages.WM_KEYUP && m_ignoreUpKey )
					return;
				base.WndProc(ref m); 
			}

			public bool IgnoreUpKey
			{
				get { return m_ignoreUpKey; }
				set { m_ignoreUpKey = value; }
			}

			private bool m_ignoreUpKey;	// This is used by the DataGridPrimitiveValueColumn.
		}
	}
}
