using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Tools.Editors;
using LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Type.Tools.Controls
{
	public enum DateTimePickerFormat
	{
		Custom,
		Long,
		Short,
		Time,
	}

	/// <summary>
	/// Summary description for DateTimePicker.
	/// </summary>
	public class DateTimePicker
		:	CalenderPicker,
			IEditor
	{
		public static readonly LinkMe.Framework.Type.DateTime MinDateTime;
		public static readonly LinkMe.Framework.Type.DateTime MaxDateTime;

		static DateTimePicker()
		{
			System.DateTime dt = CalenderPicker.GetMinDateTime();
			MinDateTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, 0, 0);
			dt = CalenderPicker.GetMaxDateTime();
			MaxDateTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, 0, 0);
		}

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DateTimePicker()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			Format = DateTimePickerFormat.Custom;
			CustomFormat = Constants.DateTime.ParseFormat;
			Value = MinDateTime;
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
			components = new System.ComponentModel.Container();
		}
		#endregion

		#region IEditor

		void IEditor.BeginEditNew()
		{
			Focus();
		}

		bool IEditor.CanDisplay(System.Type type)
		{
			return type == typeof(LinkMe.Framework.Type.DateTime);
		}

		void IEditor.Clear()
		{
			Value = new LinkMe.Framework.Type.DateTime();
		}

		void IEditor.DisplayValue(object value)
		{
			const string method = "DisplayValue";
			if ( value == null )
				throw new NullParameterException(GetType(), method, "value");
			if ( value.GetType() != typeof(LinkMe.Framework.Type.DateTime) )
				throw new InvalidParameterTypeException(GetType(), method, "value", typeof(LinkMe.Framework.Type.DateTime), value);
			Value = (LinkMe.Framework.Type.DateTime) value;
		}

		object IEditor.GetValue()
		{
			return Value;
		}

		object IEditor.GetValue(System.Type type)
		{
			const string method = "GetValue";
			object value = Value;
			if ( type != null && value != null && !type.IsAssignableFrom(value.GetType()) )
				throw new InvalidParameterValueException(GetType(), method, "type", typeof(System.Type), type);
			return value;
		}

		bool IEditor.Modified
		{
			get { return m_dt != m_originalDt; }
		}

		bool IEditor.SupportsEditing
		{
			get { return true; }
		}

		bool IReadOnlySettable.ReadOnly
		{
			get { return base.ReadOnly; }
			set { base.ReadOnly = value; }
		}

		#endregion

		[RefreshProperties(RefreshProperties.Repaint)]
		public LinkMe.Framework.Type.Tools.Controls.DateTimePickerFormat Format
		{
			get
			{
				switch ( GetFormat() )
				{
					case System.Windows.Forms.DateTimePickerFormat.Custom:
						return LinkMe.Framework.Type.Tools.Controls.DateTimePickerFormat.Custom;

					case System.Windows.Forms.DateTimePickerFormat.Time:
						return LinkMe.Framework.Type.Tools.Controls.DateTimePickerFormat.Time;

					case System.Windows.Forms.DateTimePickerFormat.Short:
						return LinkMe.Framework.Type.Tools.Controls.DateTimePickerFormat.Short;

					case System.Windows.Forms.DateTimePickerFormat.Long:
					default:
						return LinkMe.Framework.Type.Tools.Controls.DateTimePickerFormat.Long;
				}
			}
			set
			{
				switch ( value )
				{
					case LinkMe.Framework.Type.Tools.Controls.DateTimePickerFormat.Custom:
						SetFormat(System.Windows.Forms.DateTimePickerFormat.Custom);
						break;

					case LinkMe.Framework.Type.Tools.Controls.DateTimePickerFormat.Time:
						SetFormat(System.Windows.Forms.DateTimePickerFormat.Time);
						break;

					case LinkMe.Framework.Type.Tools.Controls.DateTimePickerFormat.Short:
						SetFormat(System.Windows.Forms.DateTimePickerFormat.Short);
						break;

					case LinkMe.Framework.Type.Tools.Controls.DateTimePickerFormat.Long:
					default:
						SetFormat(System.Windows.Forms.DateTimePickerFormat.Long);
						break;
				}
			}
		}

		[RefreshProperties(RefreshProperties.All), Bindable(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public LinkMe.Framework.Type.DateTime Value
		{
			get
			{
				return m_dt;
			}
			set
			{
				if ( value < MinDateTime )
					value = MinDateTime;
				if ( value > MaxDateTime )
					value = MaxDateTime;
				m_originalDt = value;
				m_dt = value;
				SetValue(m_dt.ToSystemDateTime());
			}
		}

		protected override string GetCustomFormat(string dateTimePickerCustomFormat)
		{
			return dateTimePickerCustomFormat.Replace(Constants.DateTime.SubsecondsCustomFormat, Constants.DateTime.SubsecondsParseFormat);
		}

		protected override string SetCustomFormat(string value)
		{
			return value.Replace(Constants.DateTime.SubsecondsParseFormat, Constants.DateTime.SubsecondsCustomFormat);
		}

		protected override string GetFormatText(string format)
		{
			if ( format == Constants.DateTime.SubsecondsCustomFormat )
				return GetSubsecondsFormatText(m_dt.Millisecond, m_dt.Microsecond, m_dt.Nanosecond);
			else
				return format;
		}

		protected override bool FormatKeyDown(string format, System.Windows.Forms.Keys key)
		{
			int millisecond = m_dt.Millisecond;
			int microsecond = m_dt.Microsecond;
			int nanosecond = m_dt.Nanosecond;
			if ( UpdateSubseconds(key, ref millisecond, ref microsecond, ref nanosecond) )
			{
				m_dt = new DateTime(m_dt.Year, m_dt.Month, m_dt.Day, m_dt.Hour, m_dt.Minute, m_dt.Second, millisecond, microsecond, nanosecond);
				return true;
			}
			else
			{
				return false;
			}
		}

		protected override void OnValueChanged(EventArgs e)
		{
			System.DateTime dt = GetValue();
			m_dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, m_dt.Millisecond, m_dt.Microsecond, m_dt.Nanosecond);
			base.OnValueChanged(e);
		}

		private LinkMe.Framework.Type.DateTime m_originalDt = MinDateTime;
		private LinkMe.Framework.Type.DateTime m_dt = MinDateTime;
	}
}
