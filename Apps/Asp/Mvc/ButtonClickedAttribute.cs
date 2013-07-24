using System.Reflection;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc
{
    public class ButtonClickedAttribute
        : ActionMethodSelectorAttribute
    {
        private readonly string _name;

        public ButtonClickedAttribute(string name)
        {
            _name = name;
        }
 
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var request = controllerContext.RequestContext.HttpContext.Request;
            var value = request.Form[_name];
            return value != null;
        }
    }
}
