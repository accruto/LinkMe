using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LinkMe.Framework.Tools.Editors
{
	/// <summary>
	/// A wrapper around a DataGrid that implements the IEditor interface. This control can only display
	/// a DataSet - editing is not currently supported.
	/// </summary>
	public class DataSetEditor : Editor
	{
		private System.Windows.Forms.DataGrid dtgrdData;
		private System.ComponentModel.IContainer components = null;

		public DataSetEditor()
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
			this.dtgrdData = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.dtgrdData)).BeginInit();
			this.SuspendLayout();
			// 
			// dtgrdData
			// 
			this.dtgrdData.CaptionVisible = false;
			this.dtgrdData.DataMember = "";
			this.dtgrdData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dtgrdData.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtgrdData.Location = new System.Drawing.Point(0, 0);
			this.dtgrdData.Name = "dtgrdData";
			this.dtgrdData.Size = new System.Drawing.Size(472, 224);
			this.dtgrdData.TabIndex = 5;
			// 
			// DataSetEditor
			// 
			this.Controls.Add(this.dtgrdData);
			this.Name = "DataSetEditor";
			this.Size = new System.Drawing.Size(472, 224);
			((System.ComponentModel.ISupportInitialize)(this.dtgrdData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		[Browsable(false)]
		public override bool Modified
		{
			get { return false; }
		}

		public override bool SupportsEditing
		{
			get { return false; }
		}

		public override bool ReadOnly
		{
			get { return dtgrdData.ReadOnly; }
			set { dtgrdData.ReadOnly = value; }
		}

		public override void Clear()
		{
			dtgrdData.DataSource = null;
		}

		public override bool CanDisplay(System.Type type)
		{
			if (base.CanDisplay(type))
				return true;

			return (typeof(DataSet).IsAssignableFrom(type) || typeof(DataTable).IsAssignableFrom(type)
				|| typeof(DataView).IsAssignableFrom(type) || typeof(DataViewManager).IsAssignableFrom(type));
		}

		public override void DisplayValue(object value)
		{
			CheckValue(value);

			dtgrdData.SetDataBinding(null, null);
			dtgrdData.SetDataBinding(value, null);
		}

		public override object GetValue()
		{
			return dtgrdData.DataSource;
		}
	}
}
