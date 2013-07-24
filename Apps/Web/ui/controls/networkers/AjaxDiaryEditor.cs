using System;
using AjaxPro;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Web.Applications.Ajax;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public class AjaxDiaryEditor : AjaxEditorBase
    {
        private readonly ICandidateDiariesQuery _candidateDiariesQuery = Container.Current.Resolve<ICandidateDiariesQuery>();
        private readonly ICandidateDiariesCommand _candidateDiariesCommand = Container.Current.Resolve<ICandidateDiariesCommand>();

        private const string ErrorUnknownEntryId = "A user {0} is trying to modify diary entry with ID {1}, but this ID is not present in his/her list.";

        private Diary _diary;

        private Diary Diary
        {
            get
            {
                if (_diary == null)
                {
                    var member = LoggedInMember;
                    var diary = _candidateDiariesQuery.GetDiaryByCandidateId(member.Id);
                    if (diary == null)
                    {
                        // Need to create one.

                        diary = new Diary();
                        _candidateDiariesCommand.CreateDiary(member.Id, diary);
                    }

                    _diary = diary;
                }

                return _diary;
            }
        }

        [AjaxMethod]
        public AjaxResult SaveDiaryEntry(string id, string title, string startYear, string startMonth, string endYear, string endMonth, bool isCurrent, string totalHoursText, string description)
        {
            Guid returnId;

            try
            {
                EnsureMemberLoggedIn();

                var startDate = ValidateDate(startMonth, startYear);
                if (startDate == null)
                    throw new UserException("Please specify correct start date");

                DateTime? endDate = null;
                if (!isCurrent)
                {
                    endDate = ValidateDate(endMonth, endYear);
                    if (endDate == null)
                        throw new UserException("Please specify correct end date");
                    if (startDate > endDate)
                        throw new UserException("The end date should be after the start date");
                }

                if (string.IsNullOrEmpty(title))
                    throw new UserException("The title is required.");

                double totalHours = 0;
                if (!string.IsNullOrEmpty(totalHoursText) && !double.TryParse(totalHoursText, out totalHours))
                    throw new UserException("Please specify correct total hours");

                DiaryEntry entry;
                var isNew = false;
                if (id == NoRecordId)
                {
                    entry = new DiaryEntry();
                    isNew = true;
                }
                else
                {
                    var entryId = ParseUtil.ParseUserInputGuid(id, "entry ID");
                    entry = _candidateDiariesQuery.GetDiaryEntry(entryId);
                }

                if (entry == null)
                {
                    HandleException(String.Format(ErrorUnknownEntryId, LoggedInMember.Id, id));
                    return new AjaxResult(AjaxResultCode.FAILURE, "Unknown Diary Entry ID");
                }

                entry.Title = title;
                entry.Description = description;
                entry.StartTime = startDate;
                entry.EndTime = endDate;
                entry.TotalHours = totalHours == 0 ? (double?)null : totalHours;

                if (isNew)
                    _candidateDiariesCommand.CreateDiaryEntry(Diary.Id, entry);
                else
                    _candidateDiariesCommand.UpdateDiaryEntry(entry);

                returnId = entry.Id;
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

/*            // Returning new order of things

            IList<Job> jobs = Resume.Jobs;
            Guid?[] jobIDs = new Guid?[jobs.Count];

            for (int i = 0; i < jobs.Count; i++)
            {
                jobIDs[i] = jobs[i].LensID;
            }
            */

            return new AjaxResult(AjaxResultCode.SUCCESS, returnId.ToString("n")); //, new IdArrayUserData(jobIDs));
        }

        [AjaxMethod]
        public AjaxResult DeleteDiaryEntry(string id)
        {
            try
            {
                var entryId = ParseUtil.ParseUserInputGuid(id, "entry ID");

                EnsureMemberLoggedIn();

                _candidateDiariesCommand.DeleteDiaryEntry(entryId);

/*                foreach (Job job in Resume.Jobs)
                {
                    if (job.LensID == jobID)
                    {
                        Resume.Jobs.Remove(job);
                        break;
                    }
                }

                SaveResume(true);
  */
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

            return new AjaxResult(AjaxResultCode.SUCCESS); // | (Resume.Jobs.Count == 0 ? AjaxResultCode.EMPTY : 0));
        }

        private static DateTime? ValidateDate(string month, string year)
        {
            DateTime date;

            if (String.IsNullOrEmpty(month))
            {
                if (DateTime.TryParse("Jan " + year, out date))
                    return date;
                return null;
            }
            
            if (DateTime.TryParse(month + " " + year, out date))
                return date;
            return null;
        }
    }
}
