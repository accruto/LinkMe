using LinkMe.Web.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest
{
    [TestClass]
    public class AutoSuggestHelperTest
    {
        [TestMethod]
        public void TestGetHTML()
        {
            Assert.AreEqual("<ul></ul>", AutoSuggestWebServiceHandler.GetListHTML());

            string[] arr = {"a", "b", "c"};
            string list = "";
            foreach (string s in arr)
            {
                list += "<li>" + s + "</li>";
            }
            Assert.AreEqual("<ul>" + list + "</ul>", AutoSuggestWebServiceHandler.GetListHTML(arr));
        }
    }
}
