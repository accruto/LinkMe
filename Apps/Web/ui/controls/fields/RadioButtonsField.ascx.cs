using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Controls.Fields
{
    public partial class RadioButtonsField
        : LinkMeUserControl
    {
        private IEnumerable _data;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            rptRadioButtons.ItemDataBound += rptRadioButtons_ItemDataBound;
            AddJavaScriptReference(JavaScripts.RadioButtonsField);
        }

        public string Label
        {
            get { return lblField.Text; }
            set { lblField.Text = value; }
        }

        public void SetData<T>(IEnumerable<T> data, Func<T, string> getText)
        {
            // Store the data and manage it directly instead of throguh the repeater.

            _data = data;

            // The repeater items are simply the text associated with the data.

            rptRadioButtons.DataSource = data.Select(getText);
            rptRadioButtons.DataBind();
        }

        public void Check<T>(T value)
        {
            // Work through the items.

            var data = (IEnumerable<T>)_data;
            for (var index = 0; index < data.Count(); ++index)
            {
                // Found it, now need to check the associated radio button.

                var item = rptRadioButtons.Items[index];
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    // Check the radio button.

                    var button = (RadioButton)item.FindControl("rdoButton");
                    button.Checked = Equals(data.ElementAt(index), value);
                }
            }
        }

        public T Checked<T>()
        {
            // Find the index of the checked item.

            var index = -1;
            foreach (RepeaterItem item in rptRadioButtons.Items)
            {
                ++index;
                if (item.ItemType != ListItemType.Item && item.ItemType != ListItemType.AlternatingItem)
                    continue;

                // Check the radio button.

                var button = (RadioButton)item.FindControl("rdoButton");
                if (button.Checked)
                    return ((IEnumerable<T>)_data).ElementAt(index);
            }

            return default(T);
        }

        protected static string GetText(object item)
        {
            return item as string;
        }

        protected static void rptRadioButtons_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;

            // Add the script to check/uncheck appropriately as the GroupName is mangled by the repeater.

            var button = (RadioButton)e.Item.FindControl("rdoButton");
            var script = "CheckUniqueRadioButton('rptRadioButtons.*Buttons', this)";
            button.Attributes.Add("onclick", script);
        }
    }
}