using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Members.Views.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Utility.Validation;
using LinkMe.Web.Applications.Facade;
using LinkMe.Web.Content;
using LinkMe.Web.Domain.Users.Members;
using LinkMe.Web.Helper;
using LinkMe.Web.Helpers;
using Constants=LinkMe.Common.Constants;

namespace LinkMe.Web.UI.Registered.Networkers
{
    public partial class ResumePreview : LinkMePage
    {
        public const string PrintModeParam = "print";
        public const string MemberIdParam = "memberId";

        private static readonly IMembersQuery _membersQuery = Container.Current.Resolve<IMembersQuery>();
        private static readonly IMemberViewsQuery _memberViewsQuery = Container.Current.Resolve<IMemberViewsQuery>();
        private static readonly ICandidatesQuery _candidatesQuery = Container.Current.Resolve<ICandidatesQuery>();
        private static readonly IResumesQuery _resumesQuery = Container.Current.Resolve<IResumesQuery>();
        private Member _member;
        private Candidate _candidate;
        private Resume _resume;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStyleSheetReference(StyleSheets.Resume);
            AddStyleSheetReference(StyleSheets.ResumePreview);
            AddStyleSheetReference(StyleSheets.ResumePrint);
            AddStyleSheetReference(StyleSheets.ResumeEditor);

            Master.HideHeader();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Read the member ID from the query string, default to the logged in member.

            try
            {
                var memberId = ParseUtil.ParseUserInputGuidOptional(Request.QueryString[MemberIdParam], "member ID");
                if (memberId == null)
                {
                    _member = LoggedInMember;
                }
                else
                {
                    _member = _membersQuery.GetMember(memberId.Value);
                    if (_member == null)
                        throw new UserException("There is no member with ID " + memberId + ".");
                }

                _candidate = _candidatesQuery.GetCandidate(_member.Id);
                _resume = _candidate.ResumeId == null ? null : _resumesQuery.GetResume(_candidate.ResumeId.Value);
            }
            catch (UserException ex)
            {
                AddError(ex.Message);
                return;
            }

            // Display the resume.
            bool doDisplay = false;
            if (_member.Id == LoggedInMember.Id)
            {
                ucPrimaryResumeDetails.DisplayMemberToSelf(_member, _candidate, false, false);
                ucDesiredJob.DisplayToSelf(_candidate, false);
                ucEmploymentSummary.DisplayExperienceToSelf(_member, _resume);
                ucResumeTextSections.DisplayResume(_resume, false, null, false, false);
                doDisplay = true;
            }
            else
            {
                var view = _memberViewsQuery.GetPersonalView(LoggedInUserId, _member);
                if (view.CanAccess(PersonalVisibility.Resume))
                {
                    ucPrimaryResumeDetails.DisplayMemberToOther(_member, _candidate);
                    ucEmploymentSummary.DisplayExperienceToOtherMember(_member, _resume);
                    ucResumeTextSections.DisplayResume(_resume, false, null, false, false);
                    doDisplay = true;
                }
            }

            phContent.Visible = doDisplay;
            if (doDisplay) 
            {
                ucProfilePhoto.DisplayPhotoReadOnly(NetworkerFacade.GetProfilePhotoUrlOrDefault(_member));
            } 
            else 
            {
                AddError(ValidationErrorMessages.NO_ACCESS_TO_RESUME);    
            }
        }

        protected override UserType[] AuthorizedUserTypes
        {
            get { return new[] { UserType.Member }; }
        }

        protected override bool GetRequiresActivation()
        {
            return false;
        }

        protected override UserType GetActiveUserType()
        {
            return UserType.Member;
        }

        protected bool PrintMode
        {
            get { return WebUtils.IsQueryStringFlagSet(Request, PrintModeParam, false); }
        }

        protected string GetResumeHeading()
        {
            //return StringUtils.TextToHtml(StringUtils.MakeNamePossessive(member.FullName)) + " resume";
            return HtmlUtil.TextToHtml(_member.FullName);
        }

        protected string GetCandidateStatusText()
        {
            return GetOptionalListItem(NetworkerFacade.GetCandidateStatusText(_candidate.Status));
        }

        protected string GetLastUpdatedText()
        {
            if (_resume == null)
                return "";

            return "<li>Last updated on " + _resume.LastUpdatedTime.ToString(Constants.DATE_FORMAT) + "</li>";
        }

        protected string GetJoinedText()
        {
            return GetOptionalListItem(NetworkerFacade.GetJoinDateText(_member.CreatedTime));
        }

        protected string GetEthnicStatusText()
        {
            string statusText = NetworkerFacade.GetEthnicStatusText(_member, false);

            if (!string.IsNullOrEmpty(statusText))
                statusText = "Indigenous status indicated: " + statusText;

            return ResumeViewHelper.GetOptionalListItem(statusText);
        }

        protected bool CandidateHasPhoto()
        {
            // Does the candidate have a photo which will display under current permissions?
            // (If the user is viewing their own resume, assume yes.)
            
            if (_member.Id == LoggedInMember.Id)
                return true;

            var view = _memberViewsQuery.GetPersonalView(LoggedInUserId, _member);
            return view.GetPhotoUrlOrNull() != null;
        }

        private static string GetOptionalListItem(string text)
        {
            return (string.IsNullOrEmpty(text) ? "" : "<li>" + text + "</li>");
        }
    }
}
