using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Members.Friends.Commands;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Domain.Users.Members.Views.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Utility.Configuration;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Validation;
using LinkMe.Web.Content;
using LinkMe.Web.UI;
using LinkMe.Web.UI.Controls.Networkers;

namespace LinkMe.Web.Members.Friends
{
    public partial class ViewRepresentative
        : LinkMePage
    {
        // Query parameters.

        private const string PerformSearchParameter = "performSearch";
        private const string NameQueryParameter = "name";
        private const string EmailQueryParameter = "emailAddress";
        private const string ResultIndexParameter = "resultIndex";

        private static readonly IMembersQuery _membersQuery = Container.Current.Resolve<IMembersQuery>();
        private static readonly IMemberContactsQuery _memberContactsQuery = Container.Current.Resolve<IMemberContactsQuery>();
        private static readonly IMemberViewsQuery _memberViewsQuery = Container.Current.Resolve<IMemberViewsQuery>();
        private static readonly IMemberFriendsCommand _memberFriendsCommand = Container.Current.Resolve<IMemberFriendsCommand>();
        private static readonly ICandidatesQuery _candidatesQuery = Container.Current.Resolve<ICandidatesQuery>();
        private static readonly IResumesQuery _resumesQuery = Container.Current.Resolve<IResumesQuery>();

        protected PersonalView Representative { get; private set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStyleSheetReference(StyleSheets.Networking);

            btnSearch.PostBackUrl = NavigationManager.GetUrlForPage<ViewRepresentative>(PerformSearchParameter, bool.TrueString).ToString();
            btnSearch.AddInput(NameQueryParameter, txtName);
            btnSearch.AddInput(EmailQueryParameter, txtEmailAddress);

            btnRemoveRepresentative.Click += btnRemoveRepresentative_Click;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Hide everything initially.

            phRepresentative.Visible = false;
            phResults.Visible = false;
            phNoMatches.Visible = false;

            // Determine whether the user has a representative.

            var representativeId = _memberContactsQuery.GetRepresentativeContact(LoggedInMember.Id);
            if (DisplayRepresentative(representativeId))
                phRepresentative.Visible = true;

            SetFocusOnControl(txtName);
            ucPagingBar.ResultsPerPage = ApplicationContext.Instance.GetIntProperty(ApplicationContext.FRIENDS_PER_PAGE);
            ucPagingBar.StartIndexParam = ResultIndexParameter;
            ucResultList.ExtraContactDetailsFactory = new ContactsListRepresentativeFactory();

            if (!IsPostBack)
            {
                var name = Request.QueryString[NameQueryParameter];
                var emailAddress = Request.QueryString[EmailQueryParameter];
                var performSearch = ParseUtil.ParseUserInputBooleanOptional(Request.QueryString[PerformSearchParameter], "perform search", false);

                // If anything is supplied then search.

                if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(emailAddress) || performSearch)
                {
                    try
                    {
                        OnSearchRequested(name, emailAddress);
                    }
                    catch (UserException ex)
                    {
                        AddError(ex.Message);
                    }
                }
            }
        }

        protected override UserType[] AuthorizedUserTypes
        {
            get { return new[] { UserType.Member }; }
        }

        protected override bool GetRequiresActivation()
        {
            return true;
        }

        protected override UserType GetActiveUserType()
        {
            return UserType.Member;
        }

        private void btnRemoveRepresentative_Click(object sender, EventArgs e)
        {
            var representativeId = _memberContactsQuery.GetRepresentativeContact(LoggedInMember.Id);
            if (representativeId != null)
                _memberFriendsCommand.DeleteRepresentative(LoggedInMember.Id, representativeId.Value);
            phRepresentative.Visible = false;
        }

        private bool DisplayRepresentative(Guid? representativeId)
        {
            if (representativeId == null)
                return false;

            var representative = _membersQuery.GetMember(representativeId.Value);
            if (representative == null)
                return false;

            var candidate = _candidatesQuery.GetCandidate(representative.Id);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

            var views = _memberViewsQuery.GetPersonalViews(LoggedInUserId, new[] {representativeId.Value});
            Representative = views[representativeId];
            ucRepresentativeContactsList.DisplayContacts(new[] {representative.Id}, views, new [] {representative}, new [] {candidate}, new[]{resume});
            return true;
        }

        private void OnSearchRequested(string name, string emailAddress)
        {
            txtName.Text = name;
            txtEmailAddress.Text = emailAddress;

            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(emailAddress))
            {
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(emailAddress))
                {
                    AddError("Please enter a name or email address, not both");
                    return;
                }

                if (!string.IsNullOrEmpty(name))
                {
                    if (new Regex(LinkMe.Domain.RegularExpressions.DisallowedNameCharPattern).IsMatch(name))
                    {
                        AddError(ValidationErrorMessages.INVALID_NAME_SEARCH_CRITERIA + " " + ValidationErrorMessages.PLEASE_TRY_AGAIN);
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(emailAddress))
                {
                    IValidator validator = EmailAddressValidatorFactory.CreateValidator(EmailAddressValidationMode.SingleEmail, false);
                    var errors = validator.IsValid(emailAddress)
                        ? null
                        : validator.GetValidationErrors("EmailAddress");

                    if (errors != null && errors.Length > 0)
                    {
                        AddError(((IErrorHandler)new StandardErrorHandler()).FormatErrorMessage(errors[0]) + " " + ValidationErrorMessages.PLEASE_TRY_AGAIN);
                        return;
                    }
                }
            }
            else
            {
                AddError(string.Format(ValidationErrorMessages.REQUIRED_FIELD_MISSING_1, "search term"));
                return;
            }

            var loggedInNetworkerId = LoggedInUserId.Value;

            // Perform find by email address search. 

            IList<Guid> resultIds;
            if (!string.IsNullOrEmpty(emailAddress))
            {
                var id = _memberContactsQuery.GetContact(loggedInNetworkerId, emailAddress);
                resultIds = id == null ? new Guid[0] : new[] { id.Value };
            }
            else
            {
                resultIds = _memberContactsQuery.GetContacts(loggedInNetworkerId, name, false);
            }

            InitPagingBar(resultIds.Count, name);
            var results = ucPagingBar.GetResultSubset(resultIds);
            var haveResults = (results.Count > 0);
            if (haveResults)
            {
                var members = _membersQuery.GetMembers(results);
                var candidates = _candidatesQuery.GetCandidates(results);
                var resumes = _resumesQuery.GetResumes(from c in candidates where c.ResumeId != null select c.ResumeId.Value);
                var views = _memberViewsQuery.GetPersonalViews(LoggedInUserId, members);
                ucResultList.DisplayContacts(results, views, members, candidates, resumes);
            }

            ucResultList.Visible = haveResults;
            phNoMatches.Visible = !haveResults;
            phResults.Visible = true;
        }

        private void InitPagingBar(int resultsCount, string query)
        {
            Url url = new ApplicationUrl(Request.Url.AbsolutePath);
            url.QueryString.Add(NameQueryParameter, query);
            ucPagingBar.InitPagesList(url, resultsCount, false);
        }
    }
}