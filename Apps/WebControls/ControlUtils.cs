using System;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LinkMe.WebControls
{
    public static class ControlUtils
    {
        public static HtmlForm GetParentForm(Control control, bool mandatory)
        {
            if (control == null)
                throw new ArgumentNullException("control");

            for (Control c = control; c != null; c = c.Parent)
            {
                HtmlForm form = c as HtmlForm;
                if (form != null)
                    return form;
            }

            if (mandatory)
            {
                throw new ApplicationException("The specified control, '" + control.ClientID
                    + "', does not have a parent form.");
            }
            else
                return null;
        }

        /// <summary>
        /// Returns a bitmask that is the combination of enum values for all selected ListItems in the
        /// specified collection.
        /// </summary>
        public static int SelectedListItemValuesToInt(ListItemCollection items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            int value = 0;

            foreach (ListItem item in items)
            {
                Debug.Assert(item.Value != null, "item.Value != null");

                if (item.Selected)
                {
                    value |= int.Parse(item.Value);
                }
            }

            return value;
        }

        public static string ConcatenateSelectedListItemValues(ListItemCollection items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            string text = "";

            foreach (ListItem item in items)
            {
                Debug.Assert(item.Value != null, "item.Value != null");

                if (item.Selected)
                {
                    if (text.Length == 0)
                    {
                        text = item.Value;
                    }
                    else
                    {
                        text += "," + item.Value;
                    }
                }
            }

            return text;
        }
    }
}
