using System;
using System.Linq;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Apps.Presentation.Query.Search;
using LinkMe.Apps.Presentation.Query.Search.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.UI.Controls.Common.ResumeEditor
{
    public partial class DesiredJob
        : LinkMeUserControl
    {
        private readonly ISettingsQuery _settingsQuery = Container.Current.Resolve<ISettingsQuery>();
        private readonly IResumeHighlighterFactory _highlighterFactory = Container.Current.Resolve<IResumeHighlighterFactory>();

        public const int MaxRelocationLocalitiesLength = 200;
        
        private Candidate _candidate;
        private bool _allowEditing;
        private IResumeHighlighter _highlighter;
        public bool StartEditingOnLoad { get; set; }

        protected bool IsRelocationEnabled
        {
            get { return _candidate.RelocationPreference != RelocationPreference.No; }
        }

        protected string GetDesiredSalaryLower()
        {
            if (_candidate == null)
                return string.Empty;
            var salary = _candidate.DesiredSalary;
            return salary == null || !salary.HasLowerBound ? "" : salary.LowerBound.Value.ToString("c0");
        }

        protected string GetDesiredSalaryUpper()
        {
            if (_candidate == null)
                return string.Empty;
            var salary = _candidate.DesiredSalary;
            return salary == null || !salary.HasUpperBound ? "" : salary.UpperBound.Value.ToString("c0");
        }

        protected string GetDesiredJobTypesText()
        {
            return _candidate == null
                ? string.Empty
                : _candidate.DesiredJobTypes.GetDesiredClauseDisplayText();
        }

        protected string GetDesiredSalaryText()
        {
            if (_candidate == null)
                return string.Empty;
            return _candidate.DesiredSalary == null ? string.Empty : _candidate.DesiredSalary.GetDisplayText();
        }

        protected string GetDesiredJobTitle()
        {
            return _candidate == null
                ? string.Empty
                : _highlighter.HighlightDesiredJobTitle(_candidate.DesiredJobTitle);
        }

        protected static string HideIf(bool hide)
        {
            return "display: " + (hide ? "none;" : "block;");
        }

        public void DisplayToSelf(Candidate candidate, bool isEditingAllowed)
        {
            _highlighter = _highlighterFactory.Create(ResumeHighlighterKind.Null, null, new HighlighterConfiguration());
            DisplayInternal(candidate, isEditingAllowed);
        }

        public void DisplayToEmployer(Candidate candidate, Employer employer, IResumeHighlighter highlighter)
        {
            _highlighter = highlighter ?? _highlighterFactory.Create(ResumeHighlighterKind.Null, null, new HighlighterConfiguration()); 
            DisplayInternal(candidate, false);
        }

        private void DisplayInternal(Candidate candidate, bool allowEditingParam)
        {
            if (candidate == null)
                throw new ArgumentNullException("candidate");
            
            _allowEditing = allowEditingParam;
            _candidate = candidate;

            ucWTR.Display(_candidate);
            ucWTR.IsEditable = allowEditingParam;
            if (_allowEditing)
            {
                ucDesiredJobTypes.SelectedJobTypes = _candidate.DesiredJobTypes;
                chkEmailSuggestedJobs.Checked = GetSuggestedJobSetting(_candidate.Id);
            }
        }

        protected bool AllowEditing
        {
            get { return _allowEditing; }
        }

        public static string GetRelocationPreference(RelocationPreference preference)
        {
            if (preference == RelocationPreference.No)
                return "I am not willing to relocate";
            if (preference == RelocationPreference.Yes)
                return "I am willing to relocate to:";
            if (preference == RelocationPreference.WouldConsider)
                return "I would consider relocation to:";

            throw new UserException("RelocationPreference specified is not known. Value: " +  preference);
        }

        protected string GetSelectedLocalities()
        {
            if (_candidate == null)
                return string.Empty;
            return _candidate.RelocationPreference != RelocationPreference.No
                ? TextUtil.TruncateForDisplay(_candidate.GetRelocationsDisplayText(), MaxRelocationLocalitiesLength)
                : string.Empty;
        }

        protected string GetRelocationPreference()
        {
            return _candidate == null
                ? string.Empty
                : GetRelocationPreference(_candidate.RelocationPreference);
        }

        private bool GetSuggestedJobSetting(Guid memberId)
        {
            var category = _settingsQuery.GetCategory("SuggestedJobs");
            var setting = _settingsQuery.GetSettings(memberId).CategorySettings.FirstOrDefault(c => c.CategoryId == category.Id);

            return (setting.Frequency != Frequency.Never);
        }
    }
}
