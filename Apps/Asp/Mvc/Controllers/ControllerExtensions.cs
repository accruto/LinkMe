using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Apps.Asp.Mvc.Controllers
{
    public static class ControllerExtensions
    {
        public static void ExecuteController<TController, T1, T2, T3>(this HttpContextBase httpContext, Expression<Func<TController, Func<T1, T2, T3, ActionResult>>> action, T1 p1, T2 p2, T3 p3)
            where TController : IController
        {
            ExecuteController<TController>(httpContext, action, p1, p2, p3);
        }

        public static void ExecuteController<TController, T1, T2>(this HttpContextBase httpContext, Expression<Func<TController, Func<T1, T2, ActionResult>>> action, T1 p1, T2 p2)
            where TController : IController
        {
            ExecuteController<TController>(httpContext, action, p1, p2);
        }

        public static void ExecuteController<TController, T1>(this HttpContextBase httpContext, Expression<Func<TController, Func<T1, ActionResult>>> action, T1 p1)
            where TController : IController
        {
            ExecuteController<TController>(httpContext, action, p1);
        }

        public static void ExecuteController<TController>(this HttpContextBase httpContext, Expression<Func<TController, Func<ActionResult>>> action)
            where TController : IController
        {
            ExecuteController<TController>(httpContext, action, new object[0]);
        }

        public static void ExecuteController<TController, T1, T2, T3>(this HttpContext httpContext, Expression<Func<TController, Func<T1, T2, T3, ActionResult>>> action, T1 p1, T2 p2, T3 p3)
            where TController : IController
        {
            new HttpContextWrapper(httpContext).ExecuteController(action, p1, p2, p3);
        }

        public static void ExecuteController<TController, T1, T2>(this HttpContext httpContext, Expression<Func<TController, Func<T1, T2, ActionResult>>> action, T1 p1, T2 p2)
            where TController : IController
        {
            new HttpContextWrapper(httpContext).ExecuteController(action, p1, p2);
        }

        public static void ExecuteController<TController, T1>(this HttpContext httpContext, Expression<Func<TController, Func<T1, ActionResult>>> action, T1 p1)
            where TController : IController
        {
            new HttpContextWrapper(httpContext).ExecuteController(action, p1);
        }

        public static void ExecuteController<TController>(this HttpContext httpContext, Expression<Func<TController, Func<ActionResult>>> action)
            where TController : IController
        {
            new HttpContextWrapper(httpContext).ExecuteController(action);
        }

        private static void ExecuteController<TController>(HttpContextBase httpContext, LambdaExpression expression, params object[] parameters)
            where TController : IController
        {
            var controllerName = GetControllerName<TController>();
            var methodInfo = GetMethodInfo(expression);
            var parameterNames = GetParameterNames(methodInfo);

            IController errorController = Container.Current.Resolve<TController>();
            var routeData = new RouteData();
            routeData.Values.Add("controller", controllerName);
            routeData.Values.Add("action", GetActionName(methodInfo));
            for (var index = 0; index < parameters.Length; ++index)
                routeData.Values.Add(parameterNames[index], parameters[index]);
            errorController.Execute(new RequestContext(httpContext, routeData));
        }

        private static string GetControllerName<TController>()
        {
            var name = typeof(TController).Name;
            return name.EndsWith("Controller")
                ? name.Substring(0, name.Length - "Controller".Length)
                : name;
        }

        private static string GetActionName(MethodInfo methodInfo)
        {
            return methodInfo.Name;
        }

        private static string[] GetParameterNames(MethodInfo methodInfo)
        {
            return (from p in methodInfo.GetParameters() select p.Name).ToArray();
        }

        private static MethodInfo GetMethodInfo(LambdaExpression expression)
        {
            var body = (UnaryExpression)expression.Body;
            var operand = (MethodCallExpression)body.Operand;
            var argument = (ConstantExpression)operand.Arguments[2];
            return (MethodInfo)argument.Value;
        }
    }
}