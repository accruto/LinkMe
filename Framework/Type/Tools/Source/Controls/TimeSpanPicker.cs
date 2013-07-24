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
	public enum TimeSpanPickerFormat
	{
		Custom,
		Time,
	}

	/// <summary>
	/// Summary description for TimeSpanCalenderPicker.
	/// </summary>
	public class TimeSpanPicker
		:	CalenderPicker,
			IEditor
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TimeSpanPicker()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// No need for the calender.

			ShowUpDown = true;
			Format = TimeSpanPickerFormat.Custom;
			CustomFormat = Constants.TimeSpan.ParseFormat;
			Value = new TimeSpan();
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
			// 
			// TimeSpanPicker
			// 
			this.Name = "TimeSpanPicker";

		}
		#endregion

		#region IEditor

		void IEditor.BeginEditNew()
		{
			Focus();
		}

		bool IEditor.CanDisplay(System.Type type)
		{
			return type == typeof(TimeSpan);
		}

		void IEditor.Clear()
		{
			Value = new TimeSpan();
		}

		void IEditor.DisplayValue(object value)
		{
			const string method = "DisplayValue";
			if ( value == null )
				throw new NullParameterException(GetType(), method, "value");
			if ( value.GetType() != typeof(TimeSpan) )
				throw new InvalidParameterTypeException(GetType(), method, "value", typeof(TimeSpan), value);
			Value = (TimeSpan) value;
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
			get { return (m_isPositive ? m_ts : m_ts.Negate()) != m_originalTs; }
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

		[RefreshProperties(RefreshProperties.Repaint), DefaultValue(TimeSpanPickerFormat.Time)]
		public TimeSpanPickerFormat Format
		{
			get
			{
				switch ( GetFormat() )
				{
					case System.Windows.Forms.DateTimePickerFormat.Custom:
						return TimeSpanPickerFormat.Custom;

					case System.Windows.Forms.DateTimePickerFormat.Time:
					default:
						return TimeSpanPickerFormat.Time;
				}
			}
			set
			{
				switch ( value )
				{
					case TimeSpanPickerFormat.Custom:
						SetFormat(System.Windows.Forms.DateTimePickerFormat.Custom);
						break;

					case TimeSpanPickerFormat.Time:
					default:
						SetFormat(System.Windows.Forms.DateTimePickerFormat.Time);
						break;
				}
			}
		}

		[RefreshProperties(RefreshProperties.All), Bindable(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TimeSpan Value
		{
			get
			{
				return m_isPositive ? m_ts : m_ts.Negate();
			}
			set
			{
				m_originalTs = value;

				// Make sure that m_ts is positive.

				if ( value >= TimeSpan.Zero )
				{
					m_ts = value;
					m_isPositive = true;
				}
				else
				{
					m_ts = value.Negate();
					m_isPositive = false;
				}

				System.DateTime dt = CalenderPicker.GetMinDateTime();
				SetValue(new System.DateTime(dt.Year, dt.Month, dt.Day, System.Math.Abs(m_ts.Hours), System.Math.Abs(m_ts.Minutes), System.Math.Abs(m_ts.Seconds)));
			}
		}

		protected override string GetCustomFormat(string dateTimePickerCustomFormat)
		{
			string format = dateTimePickerCustomFormat.Replace(Constants.TimeSpan.SignCustomFormat, Constants.TimeSpan.SignParseFormat);
			format = format.Replace(Constants.TimeSpan.DaysCustomFormat, Constants.TimeSpan.DaysParseFormat);
			return format.Replace(Constants.TimeSpan.SubsecondsCustomFormat, Constants.TimeSpan.SubsecondsParseFormat);
		}

		protected override string SetCustomFormat(string value)
		{
			string format = value.Replace(Constants.TimeSpan.SignParseFormat, Constants.TimeSpan.SignCustomFormat);
			format = format.Replace(Constants.TimeSpan.DaysParseFormat, Constants.TimeSpan.DaysCustomFormat);
			return format.Replace(Constants.TimeSpan.SubsecondsParseFormat, Constants.TimeSpan.SubsecondsCustomFormat);
		}

		protected override string GetFormatText(string format)
		{
			switch ( format )
			{
				case Constants.TimeSpan.SignCustomFormat:
					return m_isPositive ? "+" : "-";

				case Constants.TimeSpan.DaysCustomFormat:
					return string.Format("{0}", m_ts.Days.ToString());

				case Constants.TimeSpan.SubsecondsCustomFormat:
					return GetSubsecondsFormatText(m_ts.Milliseconds, m_ts.Microseconds, m_ts.Nanoseconds);

				default:
					return format;
			}
		}

		protected override bool FormatKeyDown(string format, System.Windows.Forms.Keys key)
		{
			switch ( format )
			{
				case Constants.TimeSpan.SignCustomFormat:
					if ( m_ts != TimeSpan.Zero )
						m_isPositive = !m_isPositive;
					return true;

				case Constants.TimeSpan.DaysCustomFormat:

				switch( key )
				{
					case Keys.Up:
						m_ts = new TimeSpan(m_ts.Days + 1, m_ts.Hours, m_ts.Minutes, m_ts.Seconds, m_ts.Milliseconds, m_ts.Microseconds, m_ts.Nanoseconds);
						return true;
						
					case Keys.Down:
						m_ts = new TimeSpan(m_ts.Days - 1, m_ts.Hours, m_ts.Minutes, m_ts.Seconds, m_ts.Milliseconds, m_ts.Microseconds, m_ts.Nanoseconds);
						return true;
				}

					return false;

				case Constants.TimeSpan.SubsecondsCustomFormat:

					int millisecond = m_ts.Milliseconds;
					int microsecond = m_ts.Microseconds;
					int nanosecond = m_ts.Nanoseconds;
					if ( UpdateSubseconds(key, ref millisecond, ref microsecond, ref nanosecond) )
					{
						m_ts = new TimeSpan(m_ts.Days, m_ts.Hours, m_ts.Minutes, m_ts.Seconds, millisecond, microsecond, nanosecond);
						return true;
					}
					else
					{
						return false;
					}

				default:
					return false;
			}
		}

		protected override void OnValueChanged(System.EventArgs e)
		{
			// Need to ensure the sign is correct.

			System.DateTime dt = GetValue();
			m_ts = new TimeSpan(m_ts.Days, dt.Hour, dt.Minute, dt.Second, m_ts.Milliseconds, m_ts.Microseconds, m_ts.Nanoseconds);
			base.OnValueChanged(e);
		}

		// m_ts is always >= TimeSpan.Zero, with m_isPositive keeping track of the sign.

		private TimeSpan m_originalTs = new TimeSpan();
		private TimeSpan m_ts = new TimeSpan();
		private bool m_isPositive = true;
	}
}
