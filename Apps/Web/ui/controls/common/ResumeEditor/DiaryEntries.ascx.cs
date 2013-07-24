using System;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.UI.Controls.Common.ResumeEditor
{
    public partial class DiaryEntries
        : LinkMeUserControl
    {
        private readonly ICandidateDiariesQuery _candidateDiariesQuery = Container.Current.Resolve<ICandidateDiariesQuery>();

        private bool _allowEditing;
        private bool _haveContent;
        private bool _startEditingOnLoad;

        public bool StartEditingOnLoad
        {
            get { return _startEditingOnLoad; }
            set { _startEditingOnLoad = value; }
        }

        protected bool AllowEditing
        {
            get { return _allowEditing; }
        }

        protected bool HaveContent
        {
            get { return _haveContent; }
        }

        public void DisplayDiary(Guid? diaryId, bool allowEditing)
        {
            _allowEditing = allowEditing;

            if (diaryId == null)
                return;

            var entries = _candidateDiariesQuery.GetDiaryEntries(diaryId.Value);

            _haveContent = entries.Count > 0;
            rptEntries.DataSource = entries;
            rptEntries.DataBind();
        }
    }
}