using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Domain;
using LinkMe.Domain.Industries;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Controls.Fields
{
    // Hide the list implementation because it might change.

    public enum IndustrySelectionMode
    {
        Multiple,
        Single
    }

    public partial class IndustryField
        : UserControl
    {
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            ucIndustryList.DisplayAllIndustries();
        }

        public string Label
        {
            get { return lblField.Text; }
            set { lblField.Text = value; }
        }

        public bool Required
        {
            get { return ucIndustryList.Required; }
            set
            {
                // Need to set the css on the label at some stage.

                ucIndustryList.Required = value;
            }
        }

        public IndustrySelectionMode SelectionMode
        {
            get { return ucIndustryList.SelectionMode == ListSelectionMode.Multiple ? IndustrySelectionMode.Multiple : IndustrySelectionMode.Single; }
            set { ucIndustryList.SelectionMode = value == IndustrySelectionMode.Multiple ? ListSelectionMode.Multiple : ListSelectionMode.Single; }
        }

        public IList<Industry> SelectedIndustries
        {
            get { return ucIndustryList.SelectedIndustries; }
            set { ucIndustryList.SetSelectedIndustries(value); }
        }
    }
}