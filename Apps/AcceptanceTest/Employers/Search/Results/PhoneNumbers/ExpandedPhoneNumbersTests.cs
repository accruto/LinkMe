using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Query.Search.Members;
using LinkMe.Web.Areas.Employers.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Results.PhoneNumbers
{
    [TestClass]
    public class ExpandedPhoneNumbersTests
        : PhoneNumbersTests
    {
        protected override void TestPhoneNumbers(Member member, int? credits, bool unlocked, PhoneNumberVisibility visibility)
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            Get(GetPartialSearchUrl(criteria, DetailLevel.Expanded));
            AssertExpandedPhoneNumbers(member, unlocked, visibility);
        }

        private void AssertExpandedPhoneNumbers(IHavePhoneNumbers member, bool unlocked, PhoneNumberVisibility visibility)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//ul[@class='unlocking-and-phones']");
            Assert.IsNotNull(node);

            // Unlocked node.

            var unlockedNode = node.SelectSingleNode("li/span[@class='contact-unlocked-icon']");
            if (unlocked)
            {
                Assert.IsNotNull(unlockedNode);
                Assert.AreEqual("", unlockedNode.InnerText);
            }
            else
            {
                Assert.IsNull(unlockedNode);
            }

            // Locked node.

            var lockedNode = node.SelectSingleNode("li/a[@class='contact-locked-icon']");
            if (unlocked)
            {
                Assert.IsNull(lockedNode);
            }
            else
            {
                Assert.IsNotNull(lockedNode);
                Assert.AreEqual("", lockedNode.InnerText);
            }

            // Mobile node.
            
            var mobileNode = node.SelectSingleNode("li/span[@class='phone-mobile-icon phone-number']");
            var mobileDimmedNode = node.SelectSingleNode("li/span[@class='phone-mobile-dimmed-icon']");
            var notSuppliedNode = node.SelectSingleNode("li/span[@class='emptynumber']");
            var mobilePhoneNumber = member.PhoneNumbers == null
                ? null
                : (from p in member.PhoneNumbers where p.Type == PhoneNumberType.Mobile select p).Single().Number;

            switch (visibility)
            {
                case PhoneNumberVisibility.Shown:
                    Assert.IsNotNull(mobileNode);
                    Assert.AreEqual(mobilePhoneNumber, mobileNode.InnerText);
                    Assert.IsNull(mobileDimmedNode);
                    Assert.IsNull(notSuppliedNode);
                    break;

                case PhoneNumberVisibility.Available:
                    Assert.IsNull(mobileNode);
                    Assert.IsNotNull(mobileDimmedNode);
                    Assert.AreEqual("Available", mobileDimmedNode.InnerText);
                    AssertPageDoesNotContain(mobilePhoneNumber);
                    Assert.IsNull(notSuppliedNode);
                    break;

                default:
                    Assert.IsNull(mobileNode);
                    Assert.IsNull(mobileDimmedNode);
                    if (mobilePhoneNumber != null)
                        AssertPageDoesNotContain(mobilePhoneNumber);
                    Assert.IsNotNull(notSuppliedNode);
                    break;
            }
        }
    }
}