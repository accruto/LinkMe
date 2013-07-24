using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Apps.Tracking
{
    public interface ITrackingRepository
    {
        void CreateCommunicationSentTrack(EventMessage message);
        void CreateCommunicationLinksTrack(EventMessage message);
        void CreateCommunicationOpenedTrack(EventMessage message);
        void CreateCommunicationLinkClickedTrack(EventMessage message);

        void CreateRequestTrack(EventMessage message);
    }
}