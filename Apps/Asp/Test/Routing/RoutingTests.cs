using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Routing
{
    [TestClass]
    public class RoutingTests
    {
        [TestMethod]
        public void TestRouteValueDictionary()
        {
            new RouteValueDictionary(new
            {
                location = (string)null,
            });
        }
    }
}
