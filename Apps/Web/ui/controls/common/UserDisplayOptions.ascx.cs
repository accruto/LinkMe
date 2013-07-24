using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkMe.Web.UI.Controls.Common
{
	public partial class UserDisplayOptions : UserControl
	{
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (EnableUnlimitedResults)
            {
                ddlNumberOfDisplayResults.Items.Add(new ListItem("All", "0"));
            }
        }

        public int NumberOfDisplayResults
		{
			get { return Int32.Parse(ddlNumberOfDisplayResults.SelectedItem.Value); }
		}

        public bool EnableUnlimitedResults { get; set; }
	}
}