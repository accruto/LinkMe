using System;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Channels.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Channels
{
    [TestClass]
    public class ChannelsTests
        : TestClass
    {
        private readonly IChannelsQuery _channelsQuery = Resolve<IChannelsQuery>();

        [TestMethod]
        public void TestUnknownChannel()
        {
            const string channelName = "Unknown";
            Assert.IsNull(_channelsQuery.GetChannel(channelName));
        }

        [TestMethod]
        public void TestWebChannel()
        {
            const string channelName = "Web";
            var channel = _channelsQuery.GetChannel(channelName);
            AssertChannel(channelName, channel);

            var appName = "Web";
            AssertChannelApp(channel.Id, appName, _channelsQuery.GetChannelApp(channel.Id, appName));

            appName = "iOS";
            Assert.IsNull(_channelsQuery.GetChannelApp(channel.Id, appName));

            appName = "Unknown";
            Assert.IsNull(_channelsQuery.GetChannelApp(channel.Id, appName));
        }

        [TestMethod]
        public void TestApiChannel()
        {
            const string channelName = "API";
            var channel = _channelsQuery.GetChannel(channelName);
            AssertChannel(channelName, channel);

            var appName = "iOS";
            AssertChannelApp(channel.Id, appName, _channelsQuery.GetChannelApp(channel.Id, appName));

            appName = "Web";
            Assert.IsNull(_channelsQuery.GetChannelApp(channel.Id, appName));

            appName = "Unknown";
            Assert.IsNull(_channelsQuery.GetChannelApp(channel.Id, appName));
        }

        private static void AssertChannelApp(Guid channelId, string name, ChannelApp channelApp)
        {
            Assert.IsNotNull(channelApp);
            Assert.AreEqual(channelId, channelApp.ChannelId);
            Assert.AreEqual(name, channelApp.Name);
        }

        private static void AssertChannel(string name, Channel channel)
        {
            Assert.IsNotNull(channel);
            Assert.AreEqual(name, channel.Name);
        }
    }
}
