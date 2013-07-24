using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Domain.Contacts;
using Moq;

namespace LinkMe.Apps.Web.Test.Mvc
{
    public static class ControllerExtensions
    {
        public static void MockContext(this ViewController controller, IRegisteredUser user)
        {
            var identity = new RegisteredUserIdentity(user.Id, user.UserType, user.IsActivated) { FullName = user.FullName, NeedsReset = false, User = user };
            var principal = new RegisteredUserPrincipal(identity);

            var mockContext = new Mock<HttpContextBase>();
            mockContext.SetupGet(c => c.User).Returns(principal);

            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.SetupGet(r => r.Headers).Returns(new NameValueCollection { { "X-Rewrite-URL", "/" } });
            mockContext.SetupGet(c => c.Request).Returns(mockRequest.Object);

            var mockResponse = new HttpResponseWrapper(new HttpResponse(new StringWriter()));
            mockContext.SetupGet(c => c.Response).Returns(mockResponse);

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.SetupGet(c => c.HttpContext).Returns(mockContext.Object);

            controller.ControllerContext = mockControllerContext.Object;
        }
    }
}
