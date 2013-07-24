using System.Linq;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Communications.Settings
{
    [TestClass]
    public class CategoryTests
        : TestClass
    {
        private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();

        [TestMethod]
        public void TestNotificationFrequencies()
        {
            Assert.IsTrue(_settingsQuery.GetCategory("MemberToMemberNotification").AvailableFrequencies.SequenceEqual(new[] { Frequency.Immediately, Frequency.Never }));
        }

        [TestMethod]
        public void TestPeriodicFrequencies()
        {
            Assert.IsTrue(_settingsQuery.GetCategory("MemberAlert").AvailableFrequencies.SequenceEqual(new[] { Frequency.Daily, Frequency.Weekly, Frequency.Monthly, Frequency.Never }));
        }

        [TestMethod]
        public void TestMemberUpdateFrequencies()
        {
            Assert.IsTrue(_settingsQuery.GetCategory("MemberUpdate").AvailableFrequencies.SequenceEqual(new[] { Frequency.Monthly, Frequency.Never }));
        }
    }
}
