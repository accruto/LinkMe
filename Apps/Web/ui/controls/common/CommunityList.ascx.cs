using System;
using System.Linq;
using System.Web.UI.WebControls;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Web.Domain.Roles.Affiliations.Communities;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class CommunityList
        : LinkMeUserControl
    {
        private readonly ICommunitiesQuery _communitiesQuery = Container.Current.Resolve<ICommunitiesQuery>();
        private readonly IVerticalsQuery _verticalsQuery = Container.Current.Resolve<IVerticalsQuery>();
        private bool _editable;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            InitialiseList();

            // The state of the list is determined by the current context.

            var currentCommunity = _communitiesQuery.GetCurrentCommunity();
            if (currentCommunity != null)
            {
                // Set the default value to that of the current community.

                SetSelectedValue(currentCommunity.Id);

                // If the user is an employer that is not associated with any community then
                // they are a general employer so let them change the selection. Also, no community
                // should be selected.

                if (LoggedInEmployer.CanSearchAllMembers(currentCommunity))
                {
                    SetSelectedValue(null);
                    _editable = true;
                }
            }
            else
            {
                // Default is to not specify any community.

                SetSelectedValue(null);
                _editable = true;
            }
        }

        public bool Editable
        {
            get { return _editable; }
        }

        public DropDownList DropDownList
        {
            get { return ddlCommunity; }
        }

        public Community SelectedValue
        {
            get
            {
                var communityId = SelectedValueId;
                return communityId == null ? null : _communitiesQuery.GetCommunity(communityId.Value);
            }
            set
            {
                SelectedValueId = value == null ? (Guid?)null : value.Id;
            }
        }

        public Guid? SelectedValueId
        {
            get
            {
                if (ddlCommunity.SelectedIndex == -1)
                    return null;

                if (string.IsNullOrEmpty(ddlCommunity.SelectedValue))
                    return null;

                var guid = new Guid(ddlCommunity.SelectedValue);
                if (guid == Guid.Empty)
                    return null;

                return guid;
            }
            set
            {
                // The ability to set the value depends on the current context.

                var currentCommunity = _communitiesQuery.GetCurrentCommunity();
                if (currentCommunity != null)
                {
                    // If the user is an employer that is not associated with any community then
                    // they are a general employer so let them change the selection.

                    if (LoggedInEmployer.CanSearchAllMembers(currentCommunity))
                        SetSelectedValue(value);
                }
                else
                {
                    // Not contextual or there is no current context so let them change.

                    SetSelectedValue(value);
                }
            }
        }

        public short TabIndex
        {
            get { return ddlCommunity.TabIndex; }
            set { ddlCommunity.TabIndex = value; }
        }

        public string CssClass
        {
            get { return ddlCommunity.CssClass; }
            set { ddlCommunity.CssClass = value; }
        }

        public string DropDownClientID
        {
            get { return ddlCommunity.ClientID; }
        }

        private void InitialiseList()
        {
            ddlCommunity.Items.Clear();
            ddlCommunity.Items.Add(new ListItem(string.Empty, null));

            var communities = _communitiesQuery.GetCommunities();

            // Need to check whether any of the verticals associated with the communities have been deleted.

            var verticalIds = (from v in _verticalsQuery.GetVerticals() select v.Id).ToArray();
            communities = (from c in communities where verticalIds.Contains(c.Id) select c).ToList();
            
            foreach (var community in communities)
            {
                // Only add the community if it has members.

                if (community.HasMembers)
                    ddlCommunity.Items.Add(new ListItem(community.Name, community.Id.ToString()));
            }
        }

        private void SetSelectedValue(Guid? guid)
        {
            if (guid != null)
            {
                // It may be possible that the value is not in the list because flags don't match etc so check.

                var item = ddlCommunity.Items.FindByValue(guid.ToString());
                if (item != null)
                {
                    ddlCommunity.SelectedValue = guid.ToString();
                    return;
                }
            }

            ddlCommunity.SelectedValue = string.Empty;
        }
    }
}