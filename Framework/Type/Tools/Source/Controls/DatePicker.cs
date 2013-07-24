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
	public enum DatePickerFormat
	{
		Custom,
		Long,
		Short,
	}

	/// <summary>
	/// Summary description for DateTimePicker.
	/// </summary>
	public class DatePicker
		:	CalenderPicker,
			IEditor
	{
		public static readonly LinkMe.Framework.Type.Date MinDate;
		public static readonly LinkMe.Framework.Type.Date MaxDate;

		static DatePicker()
		{
			System.DateTime dt = CalenderPicker.GetMinDateTime();
			MinDate = new Date(dt.Year, dt.Month, dt.Day);
			dt = CalenderPicker.GetMaxDateTime();
			MaxDate = new Date(dt.Year, dt.Month, dt.Day);
		}

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DatePicker()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			Format = DatePickerFormat.Custom;
			CustomFormat = Constants.Date.ParseFormat;
			Value = MinDate;
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
			return type == typeof(Date);
		}

		void IEditor.Clear()
		{
			Value = new Date();
		}

		void IEditor.DisplayValue(object value)
		{
			const string method = "DisplayValue";
			if ( value == null )
				throw new NullParameterException(GetType(), method, "value");
			if ( value.GetType() != typeof(Date) )
				throw new InvalidParameterTypeException(GetType(), method, "value", typeof(Date), value);
			Value = (Date) value;
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

		[RefreshProperties(RefreshProperties.Repaint), DefaultValue(DatePickerFormat.Long)]
		public DatePickerFormat Format
		{
			get
			{
				switch ( GetFormat() )
				{
					case System.Windows.Forms.DateTimePickerFormat.Custom:
						return DatePickerFormat.Custom;

					case System.Windows.Forms.DateTimePickerFormat.Short:
						return DatePickerFormat.Short;

					case System.Windows.Forms.DateTimePickerFormat.Long:
					default:
						return DatePickerFormat.Long;
				}
			}
			set
			{
				switch ( value )
				{
					case DatePickerFormat.Custom:
						SetFormat(System.Windows.Forms.DateTimePickerFormat.Custom);
						break;

					case DatePickerFormat.Short:
						SetFormat(System.Windows.Forms.DateTimePickerFormat.Short);
						break;

					case DatePickerFormat.Long:
					default:
						SetFormat(System.Windows.Forms.DateTimePickerFormat.Long);
						break;
				}
			}
		}

		[RefreshProperties(RefreshProperties.All), Bindable(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Date Value
		{
			get
			{
				return m_dt;
			}
			set
			{
				if ( value < MinDate )
					value = MinDate;
				if ( value > MaxDate )
					value = MaxDate;
				m_originalDt = value;
				m_dt = value;
				SetValue(new System.DateTime(m_dt.Year, m_dt.Month, m_dt.Day));
			}
		}

		protected override void OnValueChanged(System.EventArgs e)
		{
			System.DateTime dt = GetValue();
			m_dt = new Date(dt.Year, dt.Month, dt.Day);
			base.OnValueChanged(e);
		}

		private Date m_originalDt = MinDate;
		private Date m_dt = MinDate;
	}
}
