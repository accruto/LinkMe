using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.CommandLine.MemberNewsletter
{
    [TestClass]
    public abstract class MemberNewsletterTests
        : CommandLineTests
    {
        protected override string GetTaskGroup()
        {
            return "MemberNewsletter";
        }
    }
}