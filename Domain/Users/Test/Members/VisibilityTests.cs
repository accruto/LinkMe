using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members
{
    [TestClass]
    public class VisibilityTests
    {
        [TestMethod]
        public void TestSetOneFlagPublic()
        {
            const PersonalVisibility flag = PersonalVisibility.Photo;
            
            var member = new Member { VisibilitySettings = new VisibilitySettings() };
            member.VisibilitySettings.Personal.Set(PersonalContactDegree.Public, flag);

            Assert.IsTrue(member.VisibilitySettings.Personal.PublicVisibility.IsFlagSet(flag));
            Assert.IsTrue(member.VisibilitySettings.Personal.SecondDegreeVisibility.IsFlagSet(flag));
            Assert.IsTrue(member.VisibilitySettings.Personal.FirstDegreeVisibility.IsFlagSet(flag));
        }

        [TestMethod]
        public void TestSetAllFlagsPublic()
        {
            const PersonalVisibility flag = PersonalVisibility.All;

            var member = new Member { VisibilitySettings = new VisibilitySettings() };
            member.VisibilitySettings.Personal.Set(PersonalContactDegree.Public, flag);

            Assert.IsTrue(member.VisibilitySettings.Personal.PublicVisibility.IsFlagSet(flag));
            Assert.IsTrue(member.VisibilitySettings.Personal.SecondDegreeVisibility.IsFlagSet(flag));
            Assert.IsTrue(member.VisibilitySettings.Personal.FirstDegreeVisibility.IsFlagSet(flag));
        }

        [TestMethod]
        public void TestSetOneFlagSecondDegree()
        {
            const PersonalVisibility flag = PersonalVisibility.Photo;

            var member = new Member { VisibilitySettings = new VisibilitySettings() };
            member.VisibilitySettings.Personal.Set(PersonalContactDegree.SecondDegree, flag);

            Assert.IsFalse(member.VisibilitySettings.Personal.PublicVisibility.IsFlagSet(flag));
            Assert.IsTrue(member.VisibilitySettings.Personal.SecondDegreeVisibility.IsFlagSet(flag));
            Assert.IsTrue(member.VisibilitySettings.Personal.FirstDegreeVisibility.IsFlagSet(flag));
        }

        [TestMethod]
        public void TestSetAllFlagsSecondDegree()
        {
            const PersonalVisibility flag = PersonalVisibility.All;

            var member = new Member { VisibilitySettings = new VisibilitySettings() };
            member.VisibilitySettings.Personal.Set(PersonalContactDegree.SecondDegree, flag);

            Assert.IsFalse(member.VisibilitySettings.Personal.PublicVisibility.IsFlagSet(flag));
            Assert.IsTrue(member.VisibilitySettings.Personal.SecondDegreeVisibility.IsFlagSet(flag));
            Assert.IsTrue(member.VisibilitySettings.Personal.FirstDegreeVisibility.IsFlagSet(flag));
        }

        [TestMethod]
        public void TestSetOneFlagFirstDegree()
        {
            const PersonalVisibility flag = PersonalVisibility.Photo;

            var member = new Member { VisibilitySettings = new VisibilitySettings() };
            member.VisibilitySettings.Personal.Set(PersonalContactDegree.FirstDegree, flag);

            Assert.IsFalse(member.VisibilitySettings.Personal.PublicVisibility.IsFlagSet(flag));
            Assert.IsFalse(member.VisibilitySettings.Personal.SecondDegreeVisibility.IsFlagSet(flag));
            Assert.IsTrue(member.VisibilitySettings.Personal.FirstDegreeVisibility.IsFlagSet(flag));
        }

        [TestMethod]
        public void TestSetAllFlagsFirstDegree()
        {
            const PersonalVisibility flag = PersonalVisibility.All;

            var member = new Member { VisibilitySettings = new VisibilitySettings() };
            member.VisibilitySettings.Personal.Set(PersonalContactDegree.FirstDegree, flag);

            Assert.IsFalse(member.VisibilitySettings.Personal.PublicVisibility.IsFlagSet(flag));
            Assert.IsFalse(member.VisibilitySettings.Personal.SecondDegreeVisibility.IsFlagSet(flag));
            Assert.IsTrue(member.VisibilitySettings.Personal.FirstDegreeVisibility.IsFlagSet(flag));
        }

        [TestMethod]
        public void TestSetOneFlagHidden()
        {
            const PersonalVisibility flag = PersonalVisibility.Photo;

            var member = new Member { VisibilitySettings = new VisibilitySettings() };
            member.VisibilitySettings.Personal.Set(PersonalContactDegree.Self, flag);

            Assert.IsFalse(member.VisibilitySettings.Personal.PublicVisibility.IsFlagSet(flag));
            Assert.IsFalse(member.VisibilitySettings.Personal.SecondDegreeVisibility.IsFlagSet(flag));
            Assert.IsFalse(member.VisibilitySettings.Personal.FirstDegreeVisibility.IsFlagSet(flag));
        }

        [TestMethod]
        public void TestSetAllFlagsHidden()
        {
            const PersonalVisibility flag = PersonalVisibility.All;

            var member = new Member { VisibilitySettings = new VisibilitySettings() };
            member.VisibilitySettings.Personal.Set(PersonalContactDegree.Self, flag);

            Assert.IsFalse(member.VisibilitySettings.Personal.PublicVisibility.IsFlagSet(flag));
            Assert.IsFalse(member.VisibilitySettings.Personal.SecondDegreeVisibility.IsFlagSet(flag));
            Assert.IsFalse(member.VisibilitySettings.Personal.FirstDegreeVisibility.IsFlagSet(flag));
        }

        private static bool AccessFlagSet(PersonalVisibility flag, PersonalVisibility currentAccess)
        {
            return currentAccess.IsFlagSet(flag);
        }
    }
}
