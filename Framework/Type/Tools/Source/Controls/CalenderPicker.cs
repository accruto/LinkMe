using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace LinkMe.Framework.Type.Tools.Controls
{
	/// <summary>
	/// Summary description for BaseDateTimePicker.
	/// </summary>
	public class CalenderPicker
		:	System.Windows.Forms.UserControl //,
			//LinkMeDateTime.IDateTimePickerOwner
	{
//		private LinkMeDateTime.DateTimePicker linkmeDateTimePicker;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CalenderPicker()
		{
			// This call is required by the Windows.Forms Form Designer.
			//InitializeComponent();

			//linkmeDateTimePicker.SetOwner(this);
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
/*		private void InitializeComponent()
		{
			this.linkmeDateTimePicker = new LinkMeDateTime.DateTimePicker();
			this.SuspendLayout();
			// 
			// linkmeDateTimePicker
			// 
			this.linkmeDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.linkmeDateTimePicker.Location = new System.Drawing.Point(0, 0);
			this.linkmeDateTimePicker.Name = "linkmeDateTimePicker";
			this.linkmeDateTimePicker.Size = new System.Drawing.Size(240, 20);
			this.linkmeDateTimePicker.TabIndex = 0;
			this.linkmeDateTimePicker.DropDown += new System.EventHandler(this.linkmeDateTimePicker_DropDown);
			this.linkmeDateTimePicker.CloseUp += new System.EventHandler(this.linkmeDateTimePicker_CloseUp);
			this.linkmeDateTimePicker.ValueChanged += new System.EventHandler(this.linkmeDateTimePicker_ValueChanged);
			this.linkmeDateTimePicker.FormatChanged += new System.EventHandler(this.linkmeDateTimePicker_FormatChanged);
			// 
			// CalenderPicker
			// 
			this.Controls.Add(this.linkmeDateTimePicker);
			this.Name = "CalenderPicker";
			this.Size = new System.Drawing.Size(240, 20);
			this.ResumeLayout(false);

		}
*/
#endregion

		public override string ToString()
		{
			return ""; // linkmeDateTimePicker.ToString();
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get { return Color.AliceBlue; /*linkmeDateTimePicker.BackColor;*/ }
			set { /*linkmeDateTimePicker.BackColor = value;*/ }
		}

		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public override Image BackgroundImage
		{
			get { return null; /*linkmeDateTimePicker.BackgroundImage;*/ }
			set { /*linkmeDateTimePicker.BackgroundImage = value;*/ }
		}

		[Localizable(true), AmbientValue((string) null)]
		public Font CalendarFont
		{
			get { return null; /*linkmeDateTimePicker.CalendarFont;*/ }
			set { /*linkmeDateTimePicker.CalendarFont = value;*/ }
		}
 
		public Color CalendarForeColor
		{
			get { return Color.AliceBlue; /*linkmeDateTimePicker.CalendarForeColor;*/ }
			set { /*linkmeDateTimePicker.CalendarForeColor = value;*/ }
		}
 
		public Color CalendarMonthBackground
		{
			get { return Color.AliceBlue; /*linkmeDateTimePicker.CalendarMonthBackground;*/ }
			set { /*linkmeDateTimePicker.CalendarMonthBackground = value;*/ }
		}
 
		public Color CalendarTitleBackColor
		{
			get { return Color.AliceBlue; /* linkmeDateTimePicker.CalendarTitleBackColor;*/ }
			set { /*linkmeDateTimePicker.CalendarTitleBackColor = value;*/ }
		}
 
		public Color CalendarTitleForeColor
		{
			get { return Color.AliceBlue; /*linkmeDateTimePicker.CalendarTitleForeColor;*/ }
			set { /*linkmeDateTimePicker.CalendarTitleForeColor = value;*/ }
		}
 
		public Color CalendarTrailingForeColor
		{
			get { return Color.AliceBlue; /*linkmeDateTimePicker.CalendarTrailingForeColor;*/ }
			set { /*linkmeDateTimePicker.CalendarTrailingForeColor = value;*/ }
		}
 
		[Bindable(true), DefaultValue(true)]
		public bool Checked
		{
			get { return false; /* linkmeDateTimePicker.Checked;*/ }
			set { /*linkmeDateTimePicker.Checked = value;*/ }
		}
 
		[Localizable(true), DefaultValue(0)]
		public LeftRightAlignment DropDownAlign
		{
			get { return LeftRightAlignment.Left; /* linkmeDateTimePicker.DropDownAlign;*/ }
			set { /*linkmeDateTimePicker.DropDownAlign = value;*/ }
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get { return Color.AliceBlue; /* linkmeDateTimePicker.ForeColor;*/ }
			set { /*linkmeDateTimePicker.ForeColor = value;*/ }
		}
 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public int PreferredHeight
		{
			get { return 0; /* linkmeDateTimePicker.PreferredHeight;*/ }
		}
 
		[DefaultValue(false)]
		public bool ShowCheckBox
		{
			get { return false; /* linkmeDateTimePicker.ShowCheckBox;*/ }
			set { /*linkmeDateTimePicker.ShowCheckBox = value;*/ }
		}
 
		[DefaultValue(false)]
		public bool ShowUpDown
		{
			get { return false; /* linkmeDateTimePicker.ShowUpDown;*/ }
			set { /*linkmeDateTimePicker.ShowUpDown = value;*/ }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IgnoreUpKey
		{
			get { return false; /* linkmeDateTimePicker.GetIgnoreUpKey();*/ }
			set { /*linkmeDateTimePicker.SetIgnoreUpKey(value);*/ }
		}

//		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
//		new public event EventHandler BackColorChanged;

//		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
//		new public event EventHandler BackgroundImageChanged;

		//		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		//		new public event EventHandler ForeColorChanged;
 
//		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
//		new public event PaintEventHandler Paint;
 
		public event EventHandler CloseUp;

		protected virtual void OnCloseUp(EventArgs e)
		{
			if ( CloseUp != null )
				CloseUp(this, e);
		}

		protected override void OnFontChanged(System.EventArgs e)
		{
			//CalendarFont = Font;
			//linkmeDateTimePicker.Font = Font;
			base.OnFontChanged(e);
		}

		private void linkmeDateTimePicker_CloseUp(object sender, System.EventArgs e)
		{
			OnCloseUp(e);
		}

		public event EventHandler DropDown;

		protected virtual void OnDropDown(EventArgs e)
		{
			if ( DropDown != null )
				DropDown(this, e);
		}
 
		private void linkmeDateTimePicker_DropDown(object sender, System.EventArgs e)
		{
			OnDropDown(e);
		}

		public event EventHandler FormatChanged;

		protected virtual void OnFormatChanged(EventArgs e)
		{
			if ( FormatChanged != null )
				FormatChanged(this, e);
		}
 
		private void linkmeDateTimePicker_FormatChanged(object sender, System.EventArgs e)
		{
			OnFormatChanged(e);
		}

		public event EventHandler ValueChanged;

		protected virtual void OnValueChanged(EventArgs e)
		{
			if ( ValueChanged != null )
				ValueChanged(this, e);
		}

		private void linkmeDateTimePicker_ValueChanged(object sender, System.EventArgs e)
		{
			OnValueChanged(e);
		}

		[RefreshProperties(RefreshProperties.Repaint), DefaultValue(null)]
		public string CustomFormat
		{
			get { return ""; /* linkmeDateTimePicker.CustomFormat == null ? null : GetCustomFormat(linkmeDateTimePicker.CustomFormat);*/ }
			set { /*linkmeDateTimePicker.CustomFormat = value == null ? null : SetCustomFormat(value);*/ }
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get { return ""; /*linkmeDateTimePicker.Text;*/ }
		}

		#region IDateTimePickerOwner Members

/*		string LinkMeDateTime.IDateTimePickerOwner.GetFormatText(string format)
		{
			return GetFormatText(format);
		}

		bool LinkMeDateTime.IDateTimePickerOwner.KeyDown(string format, System.Windows.Forms.Keys key)
		{
			return FormatKeyDown(format, key);
		}
*/

		#endregion

		#region Static methods

		protected static System.DateTime GetMinDateTime()
		{
			return System.Windows.Forms.DateTimePicker.MinDateTime;
		}

		protected static System.DateTime GetMaxDateTime()
		{
			return System.Windows.Forms.DateTimePicker.MaxDateTime;
		}

		protected static string GetSubsecondsFormatText(int milliseconds, int microseconds, int nanoseconds)
		{
			return string.Format("{0}{1}{2}", milliseconds.ToString().PadLeft(3, '0'), microseconds.ToString().PadLeft(3, '0'), nanoseconds.ToString().PadLeft(3, '0'));
		}

		protected static bool UpdateSubseconds(System.Windows.Forms.Keys key, ref int milliseconds, ref int microseconds, ref int nanoseconds)
		{
			int origSubseconds = milliseconds * 1000000 + microseconds * 1000 + nanoseconds;
			int subseconds = origSubseconds;

			switch ( key )
			{
				case System.Windows.Forms.Keys.Home:
					subseconds = 0;
					break;

				case System.Windows.Forms.Keys.End:
					subseconds = 999999999;
					break;

				case System.Windows.Forms.Keys.Up: // 38:
					++subseconds;
					if ( subseconds > 999999999 )
						subseconds = 0;
					break;

				case System.Windows.Forms.Keys.Down: // 40:
					--subseconds;
					if ( subseconds < 0 )
						subseconds = 999999999;
					break;
			}

			if ( subseconds != origSubseconds )
			{
				milliseconds = subseconds / 1000000;
				subseconds %= 1000000;
				microseconds = subseconds / 1000;
				subseconds %= 1000;
				nanoseconds = subseconds;
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion

		protected virtual string GetCustomFormat(string dateTimePickerCustomFormat)
		{
			return dateTimePickerCustomFormat;
		}

		protected virtual string SetCustomFormat(string value)
		{
			return value;
		}

		protected System.Windows.Forms.DateTimePickerFormat GetFormat()
		{
			return System.Windows.Forms.DateTimePickerFormat.Short; // linkmeDateTimePicker.Format;
		}

		protected void SetFormat(System.Windows.Forms.DateTimePickerFormat value)
		{
			//linkmeDateTimePicker.Format = value;
		}

		protected void SetValue(System.DateTime dt)
		{
			//linkmeDateTimePicker.Value = dt;
		}

		protected System.DateTime GetValue()
		{
			return new System.DateTime(); // linkmeDateTimePicker.Value;
		}

		protected virtual string GetFormatText(string format)
		{
			return format;
		}

		protected virtual bool FormatKeyDown(string format, System.Windows.Forms.Keys key)
		{
			return false;
		}

		protected bool ReadOnly
		{
			get { return false; /* !linkmeDateTimePicker.Enabled;*/ }
			set { /*linkmeDateTimePicker.Enabled = !value;*/ }
		}
	}
}
