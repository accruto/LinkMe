using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Web.Content;
using LinkMe.Web.UI.Controls.Networkers;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Web.UI.Registered.Networkers
{
    public partial class Diary
        : LinkMePage
    {
        private readonly ICandidateDiariesQuery _candidateDiariesQuery = Container.Current.Resolve<ICandidateDiariesQuery>();

        public const string StartEditingParameter = "startEditing";
        public const string StartEditingEntries = "entries";

        private static readonly EventSource EventSource = new EventSource<Diary>();

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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStyleSheetReference(StyleSheets.ResumePrint);
            AddStyleSheetReference(StyleSheets.ResumeEditor);
            AddStyleSheetReference(StyleSheets.ResumeEditMode);
            AddStyleSheetReference(StyleSheets.Resume);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var diary = _candidateDiariesQuery.GetDiaryByCandidateId(LoggedInMember.Id);
            ucDiaryTextSections.DisplayDiary(diary == null ? (Guid?) null : diary.Id, true);
            AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxDiaryEditor));
            SetStartEditing();
        }

        protected override bool DoRequestValidation
        {
            get { return false; } // Allows the Email resume link to work even with <tags> in the resume.
        }

        private void SetStartEditing()
        {
            const string method = "SetStartEditing";

            switch (Request.QueryString[StartEditingParameter])
            {
                case null:
                case "":
                    return;

                case StartEditingEntries:
                    ucDiaryTextSections.StartEditingEntries();
                    break;

                default:
                    EventSource.Raise(Event.Warning, method, string.Format("Invalid value of {0} parameter: {1}",
                        StartEditingParameter, Request.QueryString[StartEditingParameter]));
                    break;
            }
        }
    }
}
