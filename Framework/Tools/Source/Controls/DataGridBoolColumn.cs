using System.Windows.Forms;

namespace LinkMe.Framework.Tools.Controls
{
	public class DataGridBoolColumn : System.Windows.Forms.DataGridBoolColumn
	{
		private bool m_defaultValue = false;

		public DataGridBoolColumn()
		{
		}

		public bool DefaultValue
		{
			get { return m_defaultValue; }
			set { m_defaultValue = value; }
		}

		// The base DataGridBoolColumn class allows the initial value to be null, even if AllowNull is false.
		// Whether this is by design or a bug, it doesn't work too well. Specify a default value instead
		// using the DefaultValue property.

		protected override object GetColumnValueAtRow(CurrencyManager source, int rowNum)
		{
			object value = base.GetColumnValueAtRow(source, rowNum);

			if (!AllowNull && value is System.DBNull)
			{
				value = DefaultValue;
				base.SetColumnValueAtRow(source, rowNum, value);
			}

			return value;
		}
	}
}
