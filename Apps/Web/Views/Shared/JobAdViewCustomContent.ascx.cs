using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Web.Areas.Members.Routes;

namespace LinkMe.Web.Views.Shared
{
    public class JobAdViewCustomContent
        : ViewUserControl<MemberJobAdView>
    {
        private Control _userControl;

        protected string Details
        {
            get
            {
                var details = string.Empty;

                var salary = Model.Description.Salary;
                if (salary != null && !salary.IsEmpty && !salary.IsZero)
                    details += salary.GetJobAdDisplayText() + "; ";

                var jobTypesDetails = JobTypes;
                if (jobTypesDetails.Length != 0)
                    details += jobTypesDetails + "; ";

                details += Model.GetLocationDisplayText();
                if (details.EndsWith("; "))
                    details = details.Substring(0, details.Length - "; ".Length);

                return details;
            }
        }

        protected string JobTypes
        {
            get { return Model.Description.JobTypes.GetDisplayText(", ", false, true); }
        }

        protected string JobIndustries
        {
            get
            {
                return Model.Description.Industries == null
                    ? ""
                    : string.Join(", ", (from i in Model.Description.Industries select i.Name).ToArray());
            }
        }

        protected string PostedDate
        {
            get { return Model.CreatedTime.ToString("dd MMMM yyyy"); }
        }

        protected string BulletPoints
        {
            get
            {
                if (Model.Description.BulletPoints.IsNullOrEmpty())
                    return "";

                var sb = new StringBuilder();

                foreach (var bullet in Model.Description.BulletPoints)
                {
                    if (!string.IsNullOrEmpty(bullet))
                        sb.AppendFormat("<li>{0}</li>", bullet);
                }

                return sb.ToString();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _userControl = Page.Controls[0].Controls[0];
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Model == null)
            {
                _userControl.FindControl("phDisplayJobAd").Visible = false;
                return;
            }

            _userControl.FindControl("phLocalApplicationsOnlyDirty").Visible = Model.Description.ResidencyRequired;  // MF: Dirty (old) HTML

            InitCompanyLogo();
            InitMetaData();

            // MF: i = visible table rows + 1;
            var i = 1 + ((Table) _userControl.FindControl("tblDetails")).Rows.Cast<TableRow>().Count(row => row.Visible);

            // MF: colour the bulletpoints list, depending whether it 'looks like' an odd row or an even row.
            if (BulletPoints != "")
                ((HtmlGenericControl)_userControl.FindControl("ulBulletPoints")).Attributes["class"] += (i % 2 == 1) ? " odd_bulletpoints" : " even_bulletpoints";
            else
                _userControl.FindControl("ulBulletPoints").Visible = false;

            _userControl.FindControl("phCompanyLogo").Visible = true;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // MF: Add appearance classes to table.
            var i = 0;
            foreach (TableRow row in ((Table)_userControl.FindControl("tblDetails")).Rows)
            {
                if (row.Visible) i++;
                row.CssClass = (row.CssClass + (
                    (i % 2 == 1) ? " odd" : " even")
                    ).TrimStart();
                row.Cells[0].CssClass = (row.Cells[0].CssClass +
                    " first"
                    ).TrimStart();
                row.Cells[row.Cells.Count - 1].CssClass = (row.Cells[row.Cells.Count - 1].CssClass +
                    " last"
                    ).TrimStart();
            }

        }

        private void InitCompanyLogo()
        {
            if (Model.LogoId == null)
                _userControl.FindControl("imgCompanyLogo").Visible = false;
            else
                ((Image)_userControl.FindControl("imgCompanyLogo")).ImageUrl = JobAdsRoutes.Logo.GenerateUrl(new { jobAdId = Model.Id }).ToString();
        }

        private void InitMetaData()
        {
            SetMetaDataVisibility(Model.Integration.ExternalReferenceId, _userControl.FindControl("tblDetails").FindControl("trExternalReference"));
            SetMetaDataVisibility(Model.ContactDetails.GetContactDetailsDisplayText(), _userControl.FindControl("tblDetails").FindControl("trCompanyName"));
            SetMetaDataVisibility(Model.ContactDetails == null ? null : Model.ContactDetails.CompanyName, _userControl.FindControl("tblDetails").FindControl("trAdvertiser"));
            SetMetaDataVisibility(Model.Description.Package, _userControl.FindControl("tblDetails").FindControl("trPackage"));
        }

        private static void SetMetaDataVisibility(string metaData, Control row)
        {
            if (string.IsNullOrEmpty(metaData))
                row.Visible = false;
        }
    }
}
