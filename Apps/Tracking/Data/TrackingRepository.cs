using System;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Instrumentation.Message;
using LinkMe.Framework.Utility.Event;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Apps.Tracking.Data
{
    public class TrackingRepository
        : ITrackingRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public TrackingRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        void ITrackingRepository.CreateCommunicationSentTrack(EventMessage message)
        {
            using (var dc = new TrackingDataContext(_connectionFactory.CreateConnection()))
            {
                // Insert an entry for the communication itself.

                InsertCommunication(dc, message);

                // Look for all links.

                var properties = message.GetParameterValue("Properties");
                if (properties is InstrumentationDetails)
                {
                    var tinyUrls = ((InstrumentationDetails)properties).GetValue("TinyUrls");
                    if (tinyUrls is InstrumentationDetails)
                    {
                        foreach (var tinyUrl in (InstrumentationDetails)tinyUrls)
                        {
                            if (tinyUrl.Value is InstrumentationDetails)
                                InsertCommunicationLink(dc, (InstrumentationDetails)tinyUrl.Value);
                        }
                    }
                }

                dc.SubmitChanges();
            }
        }

        void ITrackingRepository.CreateCommunicationLinksTrack(EventMessage message)
        {
            using (var dc = new TrackingDataContext(_connectionFactory.CreateConnection()))
            {
                // Look for all links.

                var tinyUrls = message.GetParameterValue("TinyUrls");
                if (tinyUrls is InstrumentationDetails)
                {
                    foreach (var tinyUrl in (InstrumentationDetails)tinyUrls)
                    {
                        if (tinyUrl.Value is InstrumentationDetails)
                            InsertCommunicationLink(dc, (InstrumentationDetails)tinyUrl.Value);
                    }
                }

                dc.SubmitChanges();
            }
        }

        void ITrackingRepository.CreateCommunicationOpenedTrack(EventMessage message)
        {
            using (var dc = new TrackingDataContext(_connectionFactory.CreateConnection()))
            {
                InsertCommunicationOpened(dc, message);
                dc.SubmitChanges();
            }
        }

        void ITrackingRepository.CreateCommunicationLinkClickedTrack(EventMessage message)
        {
            using (var dc = new TrackingDataContext(_connectionFactory.CreateConnection()))
            {
                InsertCommunicationLinkClicked(dc, message);
                dc.SubmitChanges();
            }
        }

        void ITrackingRepository.CreateRequestTrack(EventMessage message)
        {
            using (var dc = new TrackingDataContext(_connectionFactory.CreateConnection()))
            {
                InsertRequest(dc, message);
                dc.SubmitChanges();
            }
        }

        private static void InsertRequest(TrackingDataContext dc, EventMessage message)
        {
            var entity = new TrackingRequestEntity
                             {
                                 id = Guid.NewGuid(),
                                 time = message.Time,
                             };

            var messageDetails = message.Details;
            for (var index = 0; index < messageDetails.Count; ++index)
            {
                var details = messageDetails[index];
                if (details is SecurityDetail)
                    ((SecurityDetail)details).MapTo(entity);
                else if (details is HttpDetail)
                    ((HttpDetail) details).MapTo(entity);
                else if (details is ProcessDetail)
                    ((ProcessDetail)details).MapTo(entity);
            }

            dc.TrackingRequestEntities.InsertOnSubmit(entity);
        }

        private static void InsertCommunication(TrackingDataContext dc, EventMessage message)
        {
            // Extract all values.

            var value = message.GetParameterValue("Id");
            if (!(value is Guid))
                return;
            var id = (Guid)value;

            value = message.GetParameterValue("Definition");
            var definition = value is string ? (string)value : null;

            value = message.GetParameterValue("Vertical");
            var vertical = value is Guid ? (Guid)value : (Guid?)null;

            value = message.GetParameterValue("UserId");
            var userId = value is Guid ? (Guid)value : (Guid?)null;

            value = message.GetParameterValue("EmailAddress");
            var emailAddress = value is string ? (string)value : null;

            dc.TrackingInsertCommunication(id, message.Time.Ticks, definition, vertical, userId, emailAddress);
        }

        private static void InsertCommunicationOpened(TrackingDataContext dc, EventMessage message)
        {
            // Extract all values.

            var value = message.GetParameterValue("Id");
            if (value == null || !(value is Guid))
                return;
            var id = (Guid)value;

            // Execute the command.

            dc.TrackingInsertCommunicationOpened(id, message.Time.Ticks);
        }

        private static void InsertCommunicationLinkClicked(TrackingDataContext dc, EventMessage message)
        {
            // Extract all values.

            var value = message.GetParameterValue("TinyId");
            if (value == null || !(value is Guid))
                return;
            var tinyId = (Guid)value;

            value = message.GetParameterValue("ContextId");
            if (value == null || !(value is Guid))
                return;
            var contextId = (Guid)value;

            // Execute the command.

            dc.TrackingInsertCommunicationLinkClicked(tinyId, contextId, message.Time.Ticks);
        }

        private static void InsertCommunicationLink(TrackingDataContext dc, InstrumentationDetails details)
        {
            // Extract all values.

            var value = details.GetValue("TinyId");
            if (!(value is Guid))
                return;
            var tinyId = (Guid)value;

            value = details.GetValue("LongUrl");
            var longUrl = value is string ? (string)value : null;

            value = details.GetValue("ContextId");
            var contextId = value is Guid ? (Guid)value : (Guid?)null;

            value = details.GetValue("Instance");
            var instance = value is int ? (int)value : (int?)null;

            // Execute the command.

            dc.TrackingInsertCommunicationLink(tinyId, contextId, longUrl, instance);
        }
    }
}