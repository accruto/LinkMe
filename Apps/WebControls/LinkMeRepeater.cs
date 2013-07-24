using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkMe.WebControls
{
	[DefaultProperty("SelectedCheckboxId"), ToolboxData("<{0}:LinkMeRepeater runat=server></{0}:LinkMeRepeater>")]
	public class LinkMeRepeater : Repeater
	{
		private string selectedCheckboxId;
		private string uniqueRowControlId;


		[Bindable(false), Category("Appearance"), DefaultValue("")]
		public string SelectedCheckboxId
		{
			get { return selectedCheckboxId; }
			set { selectedCheckboxId = value; }
		}

		[Bindable(false), Category("Appearance"), DefaultValue("")]
		public string UniqueRowControlId
		{
			get { return uniqueRowControlId; }
			set { uniqueRowControlId = value; }
		}

		public Guid[] SelectedRowIds
		{
			get
			{
                List<Guid> selectedUniqueIds = new List<Guid>();
				for (int i = 0; i < Items.Count; i++)
				{
					CheckBox cb = Items[i].FindControl(SelectedCheckboxId) as CheckBox;
					if (cb.Checked)
					{
						TextBox lbl = Items[i].FindControl(UniqueRowControlId) as TextBox;
						selectedUniqueIds.Add(new Guid(lbl.Text));
					}
				}

                return selectedUniqueIds.ToArray();
			}
		}

		public string[] GetSelectedRowIdsWithFilter(string controlId, bool visible)
		{
			ArrayList selectedUniqueIds = new ArrayList();
			for (int i = 0; i < Items.Count; i++)
			{
				CheckBox cb = Items[i].FindControl(SelectedCheckboxId) as CheckBox;
				if (cb.Checked)
				{
					Control control = Items[i].FindControl(controlId) as Control;
					if(control != null)
					{
						if(control.Visible == visible)
						{
							TextBox lbl = Items[i].FindControl(UniqueRowControlId) as TextBox;
							selectedUniqueIds.Add(lbl.Text);
						}
					}
				}
			}
			string[] arrSelectedUniqueIds = new string[selectedUniqueIds.Count];
			selectedUniqueIds.CopyTo(arrSelectedUniqueIds);
			return arrSelectedUniqueIds;
		}

		public string[] AllRowIds
		{
			get
			{
				ArrayList selectedUniqueIds = new ArrayList();
				for (int i = 0; i < Items.Count; i++)
				{
					TextBox lbl = Items[i].FindControl(UniqueRowControlId) as TextBox;
					selectedUniqueIds.Add(lbl.Text);
				}
				string[] arrSelectedUniqueIds = new string[selectedUniqueIds.Count];
				selectedUniqueIds.CopyTo(arrSelectedUniqueIds);
				return arrSelectedUniqueIds;
			}
		}
	}
}