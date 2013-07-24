using System;

namespace LinkMe.Domain.Channels
{
    public class Channel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class App
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ChannelApp
        : App
    {
        public Guid ChannelId { get; set; }
    }
}
