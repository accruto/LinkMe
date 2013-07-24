using System.Linq;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Results.PhoneNumbers
{
    [TestClass]
    public class ViewPhoneNumbersTests
        : PhoneNumbersTests
    {
        protected override void TestPhoneNumbers(Member member, int? credits, bool unlocked, PhoneNumberVisibility visibility)
        {
            Get(GetCandidatesUrl(member.Id));
            AssertCandidatePhoneNumbers(member, unlocked, visibility);
            AssertCandidateMenu(credits, visibility);
        }

        private void AssertCandidatePhoneNumbers(IHavePhoneNumbers member, bool unlocked, PhoneNumberVisibility visibility)
        {
            // Unlocked node.

            var unlockedNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//ul/li/span[@class='contact-unlocked-icon']");
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

            var lockedNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//ul/li/a[@class='contact-locked-icon']");
            if (unlocked)
            {
                Assert.IsNull(lockedNode);
            }
            else
            {
                Assert.IsNotNull(lockedNode);
                //Assert.AreEqual(credits == 0 ? "Locked" : "", lockedNode.InnerText);
            }

            // Mobile node.

            var mobileNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//ul/li/span[@class='phone-mobile-icon phone-number']");
            var mobileDimmedNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//ul/li/span[@class='phone-mobile-dimmed-icon']");
            var mobilePhoneNumber = member.PhoneNumbers == null
                ? null
                : (from p in member.PhoneNumbers where p.Type == PhoneNumberType.Mobile select p).Single().Number;

            switch (visibility)
            {
                case PhoneNumberVisibility.Shown:
                    Assert.IsNotNull(mobileNode);
                    Assert.AreEqual(mobilePhoneNumber, mobileNode.InnerText);
                    Assert.IsNull(mobileDimmedNode);
                    break;

                case PhoneNumberVisibility.Available:
                    Assert.IsNull(mobileNode);
                    Assert.IsNotNull(mobileDimmedNode);
                    Assert.AreEqual("Available", mobileDimmedNode.InnerText);
                    AssertPageDoesNotContain(mobilePhoneNumber);
                    break;

                default:
                    Assert.IsNull(mobileNode);
                    Assert.IsNull(mobileDimmedNode);
                    if (mobilePhoneNumber != null)
                        AssertPageDoesNotContain(mobilePhoneNumber);
                    break;
            }
        }

        private void AssertCandidateMenu(int? credits, PhoneNumberVisibility visibility)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//tr[@class='action-items' and position()=2]/td[position()=1]/div");
            Assert.IsNotNull(node);
            Assert.AreEqual("View phone numbers", node.InnerText);

            switch (visibility)
            {
                case PhoneNumberVisibility.Available:
                    if (credits == null)
                        Assert.AreEqual("view-phone-numbers js_action-item action-item", node.Attributes["class"].Value);
                    else if (credits.Value == 0)
                        Assert.AreEqual("view-phone-numbers-disabled js_action-item action-item", node.Attributes["class"].Value);
                    else
                        Assert.AreEqual("view-phone-numbers-locked js_action-item action-item", node.Attributes["class"].Value);
                    break;

                case PhoneNumberVisibility.NotSupplied:
                    Assert.AreEqual("view-phone-numbers-disabled js_action-item action-item", node.Attributes["class"].Value);
                    break;

                case PhoneNumberVisibility.Shown:
                    Assert.AreEqual("view-phone-numbers-disabled js_action-item action-item", node.Attributes["class"].Value);
                    break;
            }
        }
    }
}