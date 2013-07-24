using System;
using System.Diagnostics;
using System.Reflection;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// A ListBox control that works around several bugs in the .NET framework implementation.
	/// </summary>
	public class ListBox : System.Windows.Forms.ListBox
	{
		public ListBox()
		{
		}

		public new void SetSelected(int index, bool value)
		{
			base.SetSelected(index, value);
			base.GetSelected(index); // SetSelected has no effect without this.
		}

		protected override void Dispose(bool disposing)
		{
			const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

			if (disposing)
			{
				// MAJOR HACK: Clear the items collection using reflection, otherwise the items won't get garbage
				// collected. We cannot use Clear() or Remove(), because they doesn't actually remove the items
				// references. Normally the collection is set to null in OnHandleDestroyed, but not if the
				// parent form is shown with ShowDialog() - in that case OnHandleDestroyed gets called when
				// Disposing is still false, so the logic in the base ListBox control does not clear it.

				FieldInfo hackField = typeof(System.Windows.Forms.ListBox).GetField("itemsCollection", flags);
				Debug.Assert(hackField != null, "hackField != null");

				hackField.SetValue(this, null);
			}

			base.Dispose(disposing);
		}
	}
}
