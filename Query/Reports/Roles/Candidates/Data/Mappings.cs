namespace LinkMe.Query.Reports.Roles.Candidates.Data
{
    internal static class Mappings
    {
        private enum EventType
        {
            UploadResume = 8,
            EditResume = 9,
            ReloadResume = 17,
        }

        public static ResumeEventEntity Map(this ResumeEvent evt)
        {
            return new ResumeEventEntity
            {
                candidateId = evt.CandidateId,
                id = evt.Id,
                resumeId = evt.ResumeId,
                resumeCreated = GetResumeCreated(evt),
                time = evt.Time,
                eventType = (int) GetEventType(evt),
            };
        }

        public static ResumeEvent Map(this ResumeEventEntity entity)
        {
            var evt = entity.eventType == (int) EventType.UploadResume
                ? new ResumeUploadEvent()
                : entity.eventType == (int) EventType.ReloadResume
                    ? (ResumeEvent)new ResumeReloadEvent()
                    : new ResumeEditEvent { ResumeCreated = entity.resumeCreated };

            evt.Id = entity.id;
            evt.CandidateId = entity.candidateId;
            evt.ResumeId = entity.resumeId;
            evt.Time = entity.time;
            return evt;
        }

        private static EventType GetEventType(ResumeEvent evt)
        {
            if (evt is ResumeUploadEvent)
                return EventType.UploadResume;
            if (evt is ResumeReloadEvent)
                return EventType.ReloadResume;
            return EventType.EditResume;
        }

        private static bool GetResumeCreated(ResumeEvent evt)
        {
            if (evt is ResumeEditEvent)
                return ((ResumeEditEvent) evt).ResumeCreated;
            return evt is ResumeUploadEvent;
        }
    }
}
