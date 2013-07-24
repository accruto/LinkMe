using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally.JobG8
{
    [TestClass]
    public class LoggedInLastUsedResumeWithoutRedirectionTests
        : LoggedInLastUsedResumeTests
    {
        protected override bool HasRedirectionUrl
        {
            get { return false; }
        }
    }
}