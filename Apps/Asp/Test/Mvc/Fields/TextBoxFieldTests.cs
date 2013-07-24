using LinkMe.Apps.Asp.Mvc.Fields;
using LinkMe.Apps.Asp.Mvc.Html;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Mvc.Fields
{
    [TestClass]
    public class TextBoxFieldTests
        : FieldTests
    {
        private const string FirstName = "Homer";

        [TestMethod]
        public void TestTextBoxFieldModel()
        {
            var model = new Member {FirstName = FirstName};

            var expectedHtml = GetHtml("FirstName", model.FirstName);
            Assert.AreEqual(expectedHtml.ToString(), Html.TextBoxField(model, m => m.FirstName).ToString());
        }

        [TestMethod]
        public void TestTextBoxFieldModelWithName()
        {
            const string name = "Name";
            var model = new Member { FirstName = FirstName };

            var expectedHtml = GetHtml(name, model.FirstName);
            Assert.AreEqual(expectedHtml.ToString(), Html.TextBoxField(model, name, m => m.FirstName).ToString());
        }

        [TestMethod]
        public void TestTextBoxFieldModelWithLabel()
        {
            const string label = "First Name";
            var model = new Member { FirstName = FirstName };

            var expectedHtml = GetHtml("FirstName", model.FirstName).WithLabel(label);
            Assert.AreEqual(expectedHtml.ToString(), Html.TextBoxField(model, m => m.FirstName).WithLabel(label).ToString());
        }

        [TestMethod]
        public void TestTextBoxFieldModelWithId()
        {
            const string id = "First-Name";
            var model = new Member { FirstName = FirstName };

            var expectedHtml = GetHtml("FirstName", model.FirstName).WithId(id);
            Assert.AreEqual(expectedHtml.ToString(), Html.TextBoxField(model, m => m.FirstName).WithId(id).ToString());
        }

        [TestMethod]
        public void TestTextBoxFieldModelWithIsRequired()
        {
            var model = new Member { FirstName = FirstName };

            var expectedHtml = GetHtml("FirstName", model.FirstName).WithFieldClasses("compulsory_field");
            Assert.AreEqual(expectedHtml.ToString(), Html.TextBoxField(model, m => m.FirstName).WithIsRequired().ToString());
        }

        [TestMethod]
        public void TestTextBoxFieldModelWithIsReadOnly()
        {
            var model = new Member { FirstName = FirstName };

            var expectedHtml = GetHtml("FirstName", model.FirstName).WithFieldClasses("read-only_field").WithAttribute("readonly", "readonly");
            Assert.AreEqual(expectedHtml.ToString(), Html.TextBoxField(model, m => m.FirstName).WithIsReadOnly().ToString());
        }

        [TestMethod]
        public void TestTextBoxFieldModelWithAttribute()
        {
            var model = new Member { FirstName = FirstName };

            const string attributeName = "new-attribute";
            const string attributeValue = "new attribute value";

            var expectedHtml = GetHtml("FirstName", model.FirstName).WithAttribute(attributeName, attributeValue);
            Assert.AreEqual(expectedHtml.ToString(), Html.TextBoxField(model, m => m.FirstName).WithAttribute(attributeName, attributeValue).ToString());
        }

        [TestMethod]
        public void TestTextBoxFieldModelWithAttributes()
        {
            var model = new Member { FirstName = FirstName };

            const string attributeName1 = "new-attribute1";
            const string attributeValue1 = "new attribute1 value";
            const string attributeName2 = "new-attribute2";
            const string attributeValue2 = "new attribute2 value";

            var expectedHtml = GetHtml("FirstName", model.FirstName)
                .WithAttribute(attributeName1, attributeValue1)
                .WithAttribute(attributeName2, attributeValue2);

            Assert.AreEqual(expectedHtml.ToString(), Html.TextBoxField(model, m => m.FirstName).WithAttribute(attributeName1, attributeValue1).WithAttribute(attributeName2, attributeValue2).ToString());
        }

        private static HtmlTextBoxField GetHtml(string name, string value)
        {
            return new HtmlTextBoxField(name, value);
        }
    }
}
