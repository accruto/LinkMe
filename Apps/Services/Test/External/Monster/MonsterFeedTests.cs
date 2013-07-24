using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Channels;
using System.Xml;
using LinkMe.Apps.Services.External.Monster.Schema;
using LinkMe.Framework.Utility.Wcf;

namespace LinkMe.Apps.Services.Test.External.Monster
{
    public abstract class MonsterFeedTests
        : TestClass
    {
        protected static IList<Job> GetJobFeed(string file)
        {
            var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
            var reader = XmlReader.Create(stream);
            var message = Message.CreateMessage(reader, 1024, MessageVersion.Soap11);
            var response = message.GetBody<Body>(new XmlSerializerObjectSerializer(typeof(Body)));
            return response.Query.JobCollection.Jobs;
        }
    }
}
