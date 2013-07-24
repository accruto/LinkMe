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
	public enum TimeOfDayPickerFormat
	{
		Custom,
		Time,
	}

	/// <summary>
	/// Summary description for TimeOfDayPicker.
	/// </summary>
	public class TimeOfDayPicker
		:	CalenderPicker,
			IEditor
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TimeOfDayPicker()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// No need for the calender.

			ShowUpDown = true;
			Format = TimeOfDayPickerFormat.Custom;
			CustomFormat = Constants.TimeOfDay.ParseFormat;
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
			return type == typeof(TimeOfDay);
		}

		void IEditor.Clear()
		{
			Value = new TimeOfDay();
		}

		void IEditor.DisplayValue(object value)
		{
			const string method = "DisplayValue";
			if ( value == null )
				throw new NullParameterException(GetType(), method, "value");
			if ( value.GetType() != typeof(TimeOfDay) )
				throw new InvalidParameterTypeException(GetType(), method, "value", typeof(TimeOfDay), value);
			Value = (TimeOfDay) value;
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
			get { return m_td != m_originalTd; }
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

		[RefreshProperties(RefreshProperties.Repaint), DefaultValue(TimeOfDayPickerFormat.Time)]
		public TimeOfDayPickerFormat Format
		{
			get
			{
				switch ( GetFormat() )
				{
					case System.Windows.Forms.DateTimePickerFormat.Custom:
						return TimeOfDayPickerFormat.Custom;

					case System.Windows.Forms.DateTimePickerFormat.Time:
					default:
						return TimeOfDayPickerFormat.Time;
				}
			}
			set
			{
				switch ( value )
				{
					case TimeOfDayPickerFormat.Custom:
						SetFormat(System.Windows.Forms.DateTimePickerFormat.Custom);
						break;

					case TimeOfDayPickerFormat.Time:
					default:
						SetFormat(System.Windows.Forms.DateTimePickerFormat.Time);
						break;
				}
			}
		}

		[RefreshProperties(RefreshProperties.All), Bindable(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TimeOfDay Value
		{
			get
			{
				return m_td;
			}
			set
			{
				m_originalTd = value;
				m_td = value;
				System.DateTime dt = CalenderPicker.GetMinDateTime();
				SetValue(new System.DateTime(dt.Year, dt.Month, dt.Day, m_td.Hour, m_td.Minute, m_td.Second));
			}
		}

		protected override string GetCustomFormat(string dateTimePickerCustomFormat)
		{
			return dateTimePickerCustomFormat.Replace(Constants.TimeOfDay.SubsecondsCustomFormat, Constants.TimeOfDay.SubsecondsParseFormat);
		}

		protected override string SetCustomFormat(string value)
		{
			return value.Replace(Constants.TimeOfDay.SubsecondsParseFormat, Constants.TimeOfDay.SubsecondsCustomFormat);
		}

		protected override string GetFormatText(string format)
		{
			if ( format == Constants.TimeOfDay.SubsecondsCustomFormat )
				return GetSubsecondsFormatText(m_td.Millisecond, m_td.Microsecond, m_td.Nanosecond);
			else
				return format;
		}

		protected override bool FormatKeyDown(string format, System.Windows.Forms.Keys key)
		{
			int millisecond = m_td.Millisecond;
			int microsecond = m_td.Microsecond;
			int nanosecond = m_td.Nanosecond;
			if ( UpdateSubseconds(key, ref millisecond, ref microsecond, ref nanosecond) )
			{
				m_td = new TimeOfDay(m_td.Hour, m_td.Minute, m_td.Second, millisecond, microsecond, nanosecond);
				return true;
			}
			else
			{
				return false;
			}
		}

		protected override void OnValueChanged(System.EventArgs e)
		{
			System.DateTime dt = GetValue();
			m_td = new TimeOfDay(dt.Hour, dt.Minute, dt.Second, m_td.Millisecond, m_td.Microsecond, m_td.Nanosecond);
			base.OnValueChanged(e);
		}

		private TimeOfDay m_originalTd;
		private TimeOfDay m_td;
	}
}
