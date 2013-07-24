using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace LinkMe.Framework.Type.Tools.Controls
{
	/// <summary>
	/// Summary description for VersionControl.
	/// </summary>
	public class VersionControl
		:	LinkMe.Framework.Tools.Controls.TextBox
	{
		private Container components = null;

		public VersionControl()
		{
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
			components = new Container();
		}
		#endregion

		[Browsable(false)]
		[DefaultValue(null)]
		public Version Version
		{
			get
			{
				if ( Text.Length == 0 )
					return null;

				Version version = null;
				try
				{
					version = new Version(Text);
				}
				catch ( System.Exception )
				{
				}
				return version;
			}
			set
			{
				Text = value == null ? string.Empty : value.ToString();
			}
		}
	}
}
