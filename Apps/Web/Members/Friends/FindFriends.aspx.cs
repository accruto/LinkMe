using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Members.Queries;
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

namespace LinkMe.Web.Members.Friends
{
    public partial class FindFriends
        : LinkMePage
    {
        // Query parameters.

        public const string NameQueryParameter = "nameQuery";
        public const string EmailQueryParameter = "emailQuery";
        public const string ExactMatchParameter = "exactMatch";
        public const string GenericQueryParameter = "genericQuery";
        private const string ResultIndexParameter = "resultIndex";

        private readonly IMemberContactsQuery _memberContactsQuery = Container.Current.Resolve<IMemberContactsQuery>();
        private readonly IMemberViewsQuery _memberViewsQuery = Container.Current.Resolve<IMemberViewsQuery>();
        private readonly IMembersQuery _membersQuery = Container.Current.Resolve<IMembersQuery>();
        private readonly ICandidatesQuery _candidatesQuery = Container.Current.Resolve<ICandidatesQuery>();
        private readonly IResumesQuery _resumesQuery = Container.Current.Resolve<IResumesQuery>();
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStyleSheetReference(StyleSheets.Networking);
            
            btnSearch.AddInput(NameQueryParameter, txtQuery);
            btnSearch.AddInput(EmailQueryParameter, txtEmail);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ucPagingBar.ResultsPerPage = ApplicationContext.Instance.GetIntProperty(
                ApplicationContext.FRIENDS_PER_PAGE);
            ucPagingBar.StartIndexParam = ResultIndexParameter;

            SetFocusOnControl(txtQuery);

            if (!IsPostBack)
            {
                if(Request.QueryString[GenericQueryParameter] != null)
                {
                    ProcessUnknownSearchType();
                }

                else if (Request.QueryString[NameQueryParameter] != null || 
                    Request.QueryString[EmailQueryParameter] != null)
                {
                    try
                    {
                        OnSearchRequested(Request.QueryString);
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

        private void ProcessUnknownSearchType()
        {
            var query = new NameValueCollection();

            string enteredText = Request.QueryString[GenericQueryParameter];
            if (enteredText.Contains("@"))
            {
                query[EmailQueryParameter] = enteredText;
            }
            else
            {
                query[NameQueryParameter] = enteredText;
            }

            try
            {
                OnSearchRequested(query);
            }
            catch (UserException ex)
            {
                AddError(ex.Message);
            }
        }

        private void OnSearchRequested(NameValueCollection query)
        {
            var name = query[NameQueryParameter];
            var emailAddress = query[EmailQueryParameter];

            txtQuery.Text = name;
            txtEmail.Text = emailAddress;

            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(emailAddress))
            {
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(emailAddress))
                {
                    AddError("Please enter a name or email address, not both");
                    return;
                }

                if(!string.IsNullOrEmpty(name))
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
                        AddError(((IErrorHandler) new StandardErrorHandler()).FormatErrorMessage(errors[0]) + " " + ValidationErrorMessages.PLEASE_TRY_AGAIN);
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
            var exactMatch = ParseUtil.ParseUserInputBooleanOptional(Request.QueryString[ExactMatchParameter], "exactMatch parameter", false);

            // Perform find by email address search. 

            IList<Guid> resultIds;
            if (!string.IsNullOrEmpty(emailAddress))
            {
                var id = _memberContactsQuery.GetContact(loggedInNetworkerId, emailAddress);
                resultIds = id == null ? new Guid[0] : new[] { id.Value };
            }
            else
            {
                resultIds = _memberContactsQuery.GetContacts(loggedInNetworkerId, name, exactMatch);
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