using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace LinkMe.Framework.Type.Tools.Controls
{
	/// <summary>
	/// A ComboBox that allows the user to select an LinkMe primitive type.
	/// </summary>
	public class PrimitiveTypeControl
		: 	LinkMe.Framework.Tools.Controls.ComboBox
	{
		private const PrimitiveType m_defaultType = PrimitiveType.String;

		private System.ComponentModel.Container components = null;

		public PrimitiveTypeControl()
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
			components = new System.ComponentModel.Container();
		}
		#endregion

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ComboBox.ObjectCollection Items
		{
			get { return base.Items; }
		}

		[DefaultValue(m_defaultType)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PrimitiveType PrimitiveType
		{
			get { return Text.Length == 0 ? m_defaultType : PrimitiveTypeInfo.GetPrimitiveTypeInfo(Text).PrimitiveType; }
			set { Text = PrimitiveTypeInfo.GetPrimitiveTypeInfo(value).Name; }
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			// Add the list.

			DropDownStyle = ComboBoxStyle.DropDownList;
			Items.Clear();
			foreach ( PrimitiveTypeInfo primitiveTypeInfo in PrimitiveTypeInfo.PrimitiveTypeInfos )
				Items.Add(primitiveTypeInfo.Name);
			SelectedIndex = 0;
		}

	}
}
