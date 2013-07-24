using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.PublishedEvents;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Candidates.Commands
{
    public class CandidatesCommand
        : ICandidatesCommand
    {
        private readonly ICandidatesRepository _repository;

        public CandidatesCommand(ICandidatesRepository repository)
        {
            _repository = repository;
        }

        void ICandidatesCommand.CreateCandidate(Candidate candidate)
        {
            candidate.LastUpdatedTime = DateTime.Now;
            candidate.Prepare();
            candidate.Validate();
            _repository.CreateCandidate(candidate);
        }

        void ICandidatesCommand.UpdateCandidate(Candidate candidate)
        {
            // Keep track of changes.

            var originalCandidate = _repository.GetCandidate(candidate.Id);

            // Change and save.

            PrepareUpdate(candidate);
            candidate.Validate();
            _repository.UpdateCandidate(candidate);

            // Fire events.

            Fire(candidate, originalCandidate);
        }

        Candidate ICandidatesCommand.GetCandidate(Guid id)
        {
            return _repository.GetCandidate(id);
        }

        [Publishes(PublishedEvents.PropertiesChanged)]
        public event EventHandler<PropertiesChangedEventArgs> PropertiesChanged;

        [Publishes(PublishedEvents.CandidateUpdated)]
        public event EventHandler<EventArgs<Guid>> CandidateUpdated;

        private static void PrepareUpdate(Candidate candidate)
        {
            candidate.LastUpdatedTime = DateTime.Now;

            if (candidate.RelocationLocations != null)
            {
                foreach (var location in candidate.RelocationLocations)
                    location.Prepare();
            }
        }

        private static ICollection<PropertyChangedEventArgs> GetPropertyChangedEventArgs(ICandidate candidate, ICandidate originalCandidate)
        {
            IList<PropertyChangedEventArgs> eventArgs = null;

            // Status.

            if (originalCandidate.Status != candidate.Status)
            {
                eventArgs = new List<PropertyChangedEventArgs>
                {
                    new CandidateStatusChangedEventArgs(originalCandidate.Status, candidate.Status)
                };
            }

            // Desired job title.

            if (originalCandidate.DesiredJobTitle != candidate.DesiredJobTitle)
            {
                if (eventArgs == null)
                    eventArgs = new List<PropertyChangedEventArgs>();
                eventArgs.Add(new DesiredJobTitleChangedEventArgs(originalCandidate.DesiredJobTitle, candidate.DesiredJobTitle));
            }

            // Desired job types.

            if (originalCandidate.DesiredJobTypes != candidate.DesiredJobTypes)
            {
                if (eventArgs == null)
                    eventArgs = new List<PropertyChangedEventArgs>();
                eventArgs.Add(new DesiredJobTypesChangedEventArgs(originalCandidate.DesiredJobTypes, candidate.DesiredJobTypes));
            }

            // Industries.

            var industryIds = GetIndustryIds(candidate);
            var originalIndustryIds = GetIndustryIds(originalCandidate);
            if (industryIds.Except(originalIndustryIds).Count() != 0 || originalIndustryIds.Except(industryIds).Count() != 0)
            {
                if (eventArgs == null)
                    eventArgs = new List<PropertyChangedEventArgs>();
                eventArgs.Add(new IndustriesChangedEventArgs(originalIndustryIds, industryIds));
            }

            return eventArgs;
        }

        private static Guid[] GetIndustryIds(ICandidate candidate)
        {
            return candidate.Industries == null
                ? new Guid[0]
                : (from i in candidate.Industries select i.Id).ToArray();
        }

        private void Fire(ICandidate candidate, ICandidate originalCandidate)
        {
            // Get all events for property changes.

            var propertyChangedEventArgs = GetPropertyChangedEventArgs(candidate, originalCandidate);
            if (propertyChangedEventArgs != null && propertyChangedEventArgs.Count > 0)
            {
                var handlers = PropertiesChanged;
                if (handlers != null)
                    handlers(this, new PropertiesChangedEventArgs(candidate.Id, propertyChangedEventArgs));
            }

            // Fire a general candidate has been updated event as well.

            var updatedHandlers = CandidateUpdated;
            if (updatedHandlers != null)
                updatedHandlers(this, new EventArgs<Guid>(candidate.Id));
        }
    }
}