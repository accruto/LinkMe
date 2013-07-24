using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Mvc
{
    public abstract class ControllerTest
        : TestClass
    {
        protected static void AssertView(string expectedViewName, ActionResult result)
        {
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = ((ViewResult)result);
            Assert.AreEqual(expectedViewName, viewResult.ViewName);
        }

        protected static void AssertNoErrors(ActionResult result)
        {
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = ((ViewResult)result);
            Assert.AreEqual(0, viewResult.ViewData.ModelState.Count);
        }

        protected static void AssertErrors(NameValueCollection expectedErrors, ActionResult result)
        {
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = ((ViewResult)result);

            // Do not include ErrorCodes.

            var modelStateKeys = viewResult.ViewData.ModelState.Keys.Where(k => !k.EndsWith("ErrorCode"));
            Assert.AreEqual(expectedErrors.Count, modelStateKeys.Count());

            Assert.IsTrue(
                expectedErrors.AllKeys.All(
                    k =>
                        viewResult.ViewData.ModelState.ContainsKey(k)
                        && viewResult.ViewData.ModelState[k].Errors.Count == 1
                        && viewResult.ViewData.ModelState[k].Errors[0].ErrorMessage == expectedErrors[k]));
        }

        protected static void AssertRedirectToRoute(string expectedRouteName, object expectedRouteValues, ActionResult result)
        {
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var redirectToRouteResult = (RedirectToRouteResult)result;
            Assert.AreEqual(expectedRouteName, redirectToRouteResult.RouteName);
            AssertAreEqual(new RouteValueDictionary(expectedRouteValues), redirectToRouteResult.RouteValues);
        }

        private static void AssertAreEqual(RouteValueDictionary expectedValues, IDictionary<string, object> values)
        {
            Assert.AreEqual(expectedValues.Count, values.Count);
            Assert.IsTrue(expectedValues.Keys.All(k => values.ContainsKey(k) && Equals(expectedValues[k], values[k])));
        }
    }
}
