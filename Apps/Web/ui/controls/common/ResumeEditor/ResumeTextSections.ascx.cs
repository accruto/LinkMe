using System;
using LinkMe.Apps.Presentation.Query.Search;
using LinkMe.Apps.Presentation.Query.Search.Members;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Utility.Utilities;

namespace LinkMe.Web.UI.Controls.Common.ResumeEditor
{
    /// <summary>
    /// Displays the text sections of a Resume (summary, career objectives, etc.) and, optionally,
    /// allows them to be edited.
    /// </summary>
    public partial class ResumeTextSections : LinkMeUserControl
    {
        public const string FIELD_CAREER_OBJECTIVES = "R0";
        public const string FIELD_SUMMARY = "R1";
        public const string FIELD_SKILLS = "R2";
        public const string FIELD_CITIZENSHIP = "R3";
        public const string FIELD_AFFILIATION = "R4";
        public const string FIELD_OTHERS = "R5";
        public const string FIELD_EDUCATION = "R6";
        public const string FIELD_EMPLOYMENT_HISTORY = "R7";
        public const string FIELD_PROFESSIONAL = "R8";
        public const string FIELD_INTERESTS = "R9";
        public const string FIELD_REFEREES = "R10";
        public const string FIELD_CONTACT_DETAILS = "R11";
        public const string FIELD_COURSES = "R12";
        public const string FIELD_AWARDS = "R13";
        public const string FIELD_IDEAL_JOB = "R14";
        public const string FIELD_PHOTO = "R15";
        public const string FIELD_OBJECTIVE = "R16";

        private const string refereesHiddenText = "To see the references and contact the candidate, call "
            + LinkMe.Apps.Agents.Constants.PhoneNumbers.FreecallText + " to purchase a plan.";

        private readonly IResumeHighlighterFactory _highlighterFactory = Container.Current.Resolve<IResumeHighlighterFactory>();

        public void DisplayResume(Resume resume, bool allowEditing, IResumeHighlighter highlighter,
            bool hideRecentEmployers, bool hideReferees)
        {
            if (allowEditing && (hideRecentEmployers || hideReferees))
                throw new ArgumentException("allowEditing cannot be true while some content is hidden.", "allowEditing");

            highlighter = highlighter ?? _highlighterFactory.Create(ResumeHighlighterKind.Null, null, new HighlighterConfiguration());

            ucCareerObjective.DisplayContent(resume == null ? null : resume.Objective, allowEditing, highlighter);
            ucSummary.DisplayContent(resume == null ? null : resume.Summary, allowEditing, highlighter);
            ucEmploymentHistory.DisplayJobs(resume, allowEditing, highlighter, hideRecentEmployers);
            ucSkills.DisplayContent(resume == null ? null : resume.Skills, allowEditing, highlighter);
            ucEducationHistory.DisplayContent(resume == null ? null : resume.Schools, allowEditing, highlighter);
            ucCourses.DisplayContent(resume == null ? null : StringUtils.Join("\n", resume.Courses),
                allowEditing, highlighter);
            ucAwards.DisplayContent(resume == null ? null : StringUtils.Join("\n", resume.Awards),
                allowEditing, highlighter);
            ucProfessional.DisplayContent(resume == null ? null : resume.Professional, allowEditing, highlighter);
            ucInterests.DisplayContent(resume == null ? null : resume.Interests, allowEditing, highlighter);
            ucCitizenship.DisplayContent(resume == null ? null : resume.Citizenship, allowEditing, highlighter);
            ucAffiliation.DisplayContent(resume == null ? null : resume.Affiliations, allowEditing, highlighter);
            ucOther.DisplayContent(resume == null ? null : resume.Other, allowEditing, highlighter);

            string referees = (resume == null ? null : resume.Referees);
            if (string.IsNullOrEmpty(referees) || !hideReferees)
            {
                ucReferences.DisplayContent(referees, allowEditing, highlighter);
            }
            else
            {
                // No access to referees - display some placeholder text (and don't highlight it).
                ucReferences.DisplayContent(refereesHiddenText, allowEditing, _highlighterFactory.Create(ResumeHighlighterKind.Null, null, new HighlighterConfiguration()));
            }
        }

        public void StartEditingObjectivesOnLoad()
        {
            ucCareerObjective.StartEditingOnLoad = true;
        }

        public void StartEditingEmploymentOnLoad()
        {
            ucEmploymentHistory.StartEditingOnLoad = true;
        }

        public void StartEditingEducationOnLoad()
        {
            ucEducationHistory.StartEditingOnLoad = true;
        }

        public void StartEditingAwardsOnLoad()
        {
            ucAwards.StartEditingOnLoad = true;
        }
    }
}
