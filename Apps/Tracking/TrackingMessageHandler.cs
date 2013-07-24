using System;
using System.Diagnostics;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Instrumentation.Message;
using LinkMe.Framework.Instrumentation.MessageComponents;

namespace LinkMe.Apps.Tracking
{
    public class TrackingMessageHandler
        : BaseMessageHandler
    {
        private readonly ITrackingRepository _repository;

        public TrackingMessageHandler(ITrackingRepository repository)
        {
            _repository = repository;
        }

        protected override void HandleEventMessage(EventMessage message)
        {
            Debug.Assert(message != null, "message != null");

            // Look for the category.

            if (message.Event == Event.CommunicationTracking.Name)
                HandleCommunicationMessage(message);
            else if (message.Event == Event.RequestTracking.Name)
                HandleRequestMessage(message);
        }

        protected override void HandleEventMessages(EventMessages messages)
        {
            Debug.Assert(messages != null, "messages != null");

            foreach (var message in messages)
                HandleEventMessage(message);
        }

        private void HandleCommunicationMessage(EventMessage message)
        {
            CommunicationTrackingType type;
            if (GetCommunicationTrackingType(message, out type))
            {
                switch (type)
                {
                    case CommunicationTrackingType.Sent:
                        _repository.CreateCommunicationSentTrack(message);
                        break;

                    case CommunicationTrackingType.Links:
                        _repository.CreateCommunicationLinksTrack(message);
                        break;

                    case CommunicationTrackingType.Opened:
                        _repository.CreateCommunicationOpenedTrack(message);
                        break;

                    case CommunicationTrackingType.LinkClicked:
                        _repository.CreateCommunicationLinkClickedTrack(message);
                        break;
                }
            }
        }

        private void HandleRequestMessage(EventMessage message)
        {
            _repository.CreateRequestTrack(message);
        }

        private static bool GetCommunicationTrackingType(EventMessage message, out CommunicationTrackingType type)
        {
            type = CommunicationTrackingType.None;

            // Look for the parameters.

            var trackingType = message.GetParameterValue(typeof(CommunicationTrackingType).Name);
            if (trackingType == null)
                return false;

            try
            {
                type = (CommunicationTrackingType)Enum.Parse(typeof(CommunicationTrackingType), trackingType.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}