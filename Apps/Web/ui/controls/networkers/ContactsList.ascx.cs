using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class ContactsList
        : LinkMeUserControl
    {
        public const string ContactItemDivId = "ContactItem_";

        private PersonalViews _views;
        private IDictionary<Guid, Member> _members;
        private IDictionary<Guid, Candidate> _candidates;
        private IDictionary<Guid, Resume> _resumes;

        public bool AlwaysDisplayNames { get; set; }
        public bool CanAccessFriends { get; set; }
        public bool CanAddToFriends { get; set; }

        public ContactsList()
        {
            CanAccessFriends = true;
            CanAddToFriends = true;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStyleSheetReference(StyleSheets.Networking);

            rptContacts.ItemCreated += rptContacts_ItemCreated;
            rptContacts.ItemDataBound += rptContacts_DataBound;
        }

        public IExtraContactDetailsFactory ExtraContactDetailsFactory { get; set; }

        public int CountContacts { get; set; }

        internal Repeater ContactsRepeater
        {
            get { return rptContacts; }
        }

        public void DisplayContacts(IEnumerable<Guid> ids, PersonalViews views, IEnumerable<Member> members, IEnumerable<Candidate> candidates, IEnumerable<Resume> resumes)
        {
            _views = views;
            _members = members.ToDictionary(m => m.Id, m => m);
            _candidates = candidates.ToDictionary(c => c.Id, c => c);
            _resumes = candidates.ToDictionary(c => c.Id, c => c.ResumeId == null ? null : (from r in resumes where r.Id == c.ResumeId.Value select r).SingleOrDefault());

            rptContacts.DataSource = ids;
            rptContacts.DataBind();

            CountContacts = members.Count();
        }

        protected static string GetCssClass(int index)
        {
            return (index % 2 == 0 ? "friends-container" : "alternate-friends-container");
        }

        protected static string GetItemSuffix(int index)
        {
            return index.ToString();
        }

        private void rptContacts_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            // This must be done in ItemCreated, not ItemDataBound, otherwise things get messed up
            // on postback (eg. revoking a group membership) - the dynamic controls from the initial
            // page load are still there and if you remove those then the events no longer get bubbled up.

            var details = e.Item.FindControl("ucContactsListDetails") as ContactsListDetails;
            if (details != null && ExtraContactDetailsFactory != null)
            {
                details.CreateDynamicControls(ExtraContactDetailsFactory);
            }
        }

        private void rptContacts_DataBound(object obj, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item)
                return;

            var currentId = (Guid)e.Item.DataItem;
            var details = e.Item.FindControl("ucContactsListDetails") as ContactsListDetails;
            if (details != null)
            {
                details.AlwaysDisplayNames = AlwaysDisplayNames;
                details.CanAccessFriends = CanAccessFriends;
                details.CanAddToFriends = CanAddToFriends;
                details.DisplayContact(_views[currentId], _members[currentId], _candidates[currentId], _resumes[currentId], GetItemSuffix(e.Item.ItemIndex));
            }
        }
    }
}
