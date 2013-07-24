using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LinkMe.Apps.Services.External.Jxt.Schema;

namespace LinkMe.Apps.Services.Test.External.Jxt
{
    public abstract class JxtFeedTests
        : TestClass
    {
        protected static IList<Job> GetJobFeed(string file)
        {
            var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
            var serializer = new XmlSerializer(typeof(Jobs));
            var body = (Jobs)serializer.Deserialize(stream);
            return body.Client.Job;
        }
    }
}
