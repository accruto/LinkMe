using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Utility.Validation;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Registered.Networkers
{
    public partial class DiaryPreview
        : LinkMePage
    {
        public const string PrintModeParam = "print";
        public const string MemberIdParam = "memberId";

        private readonly IMembersQuery _membersQuery = Container.Current.Resolve<IMembersQuery>();
        private readonly ICandidateDiariesQuery _candidateDiariesQuery = Container.Current.Resolve<ICandidateDiariesQuery>();

        private Member _member;

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

            // Read the _member ID from the query string, default to the logged in _member.

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
            }
            catch (UserException ex)
            {
                AddError(ex.Message);
                return;
            }

            // Display the diary.

            var doDisplay = false;
            if (_member.Id == LoggedInMember.Id)
            {
                var diary = _candidateDiariesQuery.GetDiaryByCandidateId(_member.Id);
                ucDiaryTextSections.DisplayDiary(diary == null ? (Guid?) null : diary.Id, false);
                doDisplay = true;
            }

            phContent.Visible = doDisplay;
            if (!doDisplay)
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
    }
}
