using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally.JobG8
{
    [TestClass]
    public class LoggedInProfileWithRedirectionTests
        : LoggedInProfileTests
    {
        protected override bool HasRedirectionUrl
        {
            get { return true; }
        }
    }
}