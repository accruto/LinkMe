using System.Windows.Forms;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// Implement this interface in a DataGrid column styles to provide custom tool tips for grid cells.
	/// The tool tip can vary depending on the mouse position within the cell.
	/// </summary>
	internal interface ICellToolTip
	{
		/// <summary>
		/// Gets the tool tip for the current mouse position within the cell.
		/// </summary>
		/// <param name="x">The X-coordinate of the mouse pointer within the cell, relative to the cell bounds.</param>
		/// <param name="y">The Y-coordinate of the mouse pointer within the cell, relative to the cell bounds.</param>
		/// <param name="currencyManager">The CurrencyManager for the parent DataGrid.</param>
		/// <param name="cell">The cell under the mouse pointer.</param>
		/// <returns>The tool tip text for the current mouse position -or- null for standard tool tip behaviour.</returns>
        string GetCellToolTip(int x, int y, CurrencyManager currencyManager, DataGridCell cell);
	}
}
