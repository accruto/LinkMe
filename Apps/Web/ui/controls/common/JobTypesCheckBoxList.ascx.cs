using System;
using System.Web.UI.WebControls;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Domain;
using LinkMe.Utility.Validation;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class JobTypesCheckBoxList : LinkMeUserControl
    {
        public bool Required
        {
            get { return reqJobTypes.Enabled; }
            set { reqJobTypes.Enabled = value; }
        }

        public JobTypes SelectedJobTypes
        {
            get
            {
                JobTypes value = JobTypes.None;

                foreach (ListItem item in cblJobTypes.Items)
                {
                    if (item.Selected)
                    {
                        value |= (JobTypes)Enum.Parse(typeof(JobTypes), item.Value);
                    }
                }

                return value;
            }
            set
            {
                if (cblJobTypes.Items.Count == 0)
                {
                    throw new InvalidOperationException("The SelectedJobTypes property cannot be set at this stage."
                        + " OnInit must be called first.");
                }

                foreach (ListItem item in cblJobTypes.Items)
                {
                    var itemValue = (JobTypes)Enum.Parse(typeof(JobTypes), item.Value);
                    item.Selected = ((value & itemValue) == itemValue);
                }
            }
        }

        public string ListBoxClientID
        {
            get { return cblJobTypes.ClientID; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            reqJobTypes.ErrorMessage = ValidationErrorMessages.REQUIRED_FIELD_JOB_TYPE;

            // Initialise the CheckBoxList items. This must be done in OnInit(), before it processes the postback
            // data to determine which of them are checked.

            cblJobTypes.Items.Clear();

            foreach (var value in JobTypesDisplay.Values)
            {
                var intValue = ((int)value).ToString();
                var listItem = new ListItem(value.GetDisplayText(), intValue);
                listItem.Attributes.Add("intValue", intValue); // Used by JavaScript in Resume Editor.
                cblJobTypes.Items.Add(listItem);
            }
        }
    }
}