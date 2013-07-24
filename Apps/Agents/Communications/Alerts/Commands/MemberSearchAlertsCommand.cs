using System;
using System.Linq;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;

namespace LinkMe.Apps.Agents.Communications.Alerts.Commands
{
    public class MemberSearchAlertsCommand
        : IMemberSearchAlertsCommand
    {
        private readonly ISearchAlertsRepository _repository;
        private readonly IMemberSearchesCommand _memberSearchesCommand;

        public MemberSearchAlertsCommand(ISearchAlertsRepository repository, IMemberSearchesCommand memberSearchesCommand)
        {
            _repository = repository;
            _memberSearchesCommand = memberSearchesCommand;
        }

        void IMemberSearchAlertsCommand.CreateMemberSearch(IUser owner, MemberSearch search)
        {
            _memberSearchesCommand.CreateMemberSearch(owner, search);
        }

        void IMemberSearchAlertsCommand.CreateMemberSearchAlert(IUser owner, MemberSearch search, params AlertType[] alertTypes)
        {
            // Create the search first.

            _memberSearchesCommand.CreateMemberSearch(owner, search);

            // Create the alert(s).

            foreach (var alertType in alertTypes)
            {
                CreateMemberSearchAlert(search, alertType);
            }
        }

        void IMemberSearchAlertsCommand.UpdateMemberSearch(IUser owner, MemberSearch search)
        {
            // Update the search.

            _memberSearchesCommand.UpdateMemberSearch(owner, search);

        }

        void IMemberSearchAlertsCommand.UpdateMemberSearch(IUser owner, MemberSearch search, params Tuple<AlertType, bool>[] alertMethods)
        {
            // Update the search.

            _memberSearchesCommand.UpdateMemberSearch(owner, search);

            // Check whether each alert already exists.

            foreach (var method in alertMethods)
            {
                var alertType = method.Item1;
                var alert = _repository.GetMemberSearchAlert(search.Id, alertType);
                if (method.Item2)
                {
                    if (alert == null)
                        CreateMemberSearchAlert(search, alertType);
                }
                else
                {
                    if (alert != null)
                        DeleteMemberSearchAlert(search, alertType);
                }
            }
        }

        void IMemberSearchAlertsCommand.DeleteMemberSearch(IUser owner, MemberSearch search)
        {
            DeleteMemberSearchAlerts(search);
            _memberSearchesCommand.DeleteMemberSearch(owner, search.Id);
        }

        void IMemberSearchAlertsCommand.DeleteMemberSearchAlerts(IUser owner, MemberSearch search)
        {
            DeleteMemberSearchAlerts(search);
        }

        void IMemberSearchAlertsCommand.DeleteMemberSearchAlert(IUser owner, MemberSearch search, AlertType alertType)
        {
            DeleteMemberSearchAlert(search, alertType);
        }

        void IMemberSearchAlertsCommand.UpdateLastRunTime(Guid searchId, DateTime time, AlertType alertType)
        {
            _repository.UpdateMemberSearchLastRunTime(searchId, time, alertType);
        }

        void IMemberSearchAlertsCommand.AddResults(Guid employerId, IList<SavedResumeSearchAlertResult> results)
        {
            //Get the employer's unviewed candidates
            var unviewedCandidates = _repository.GetUnviewedCandidates(employerId);
            var resultsToSave = new List<SavedResumeSearchAlertResult>();

            foreach (var result in results)
            {
                if (unviewedCandidates.Select(r => r.CandidateId).Contains(result.CandidateId))
                {
                    continue;
                }

                result.Prepare();
                resultsToSave.Add(result);
            }

            _repository.AddResults(resultsToSave);
        }

        void IMemberSearchAlertsCommand.MarkAsViewed(Guid employerId, Guid candidateId)
        {
            //Get the employer's unviewed candidates
            var unviewedCandidates = _repository.GetUnviewedCandidates(employerId);

            var results = unviewedCandidates.Where(r => r.CandidateId == candidateId);

            if (results == null || !results.Any())
            {
                return;
            }

            //Highly unlikely but there may be more than one result. Mark them all read
            foreach (var result in results)
            {
                _repository.MarkAsViewed(result.Id);
            }
        }

        private void CreateMemberSearchAlert(MemberSearch search, AlertType alertType)
        {
            var alert = new MemberSearchAlert { MemberSearchId = search.Id, AlertType = alertType};
            alert.Prepare();
            alert.Validate();
            _repository.CreateMemberSearchAlert(alert);
        }

        private void DeleteMemberSearchAlerts(MemberSearch search)
        {
            _repository.DeleteMemberSearchAlerts(search.Id);
        }

        private void DeleteMemberSearchAlert(MemberSearch search, AlertType alertType)
        {
            _repository.DeleteMemberSearchAlert(search.Id, alertType);
        }
    }
}