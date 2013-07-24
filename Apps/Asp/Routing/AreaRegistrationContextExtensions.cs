using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Routing;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Routing
{
    public delegate TResult ControllerFunc<in T1, in T2, in T3, in T4, in T5, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    public delegate TResult ControllerFunc<in T1, in T2, in T3, in T4, in T5, in T6, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

    public static class AreaRegistrationContextExtensions
    {
        private static readonly Regex KeysRegex = new Regex("{[0-9a-zA-Z]+}", RegexOptions.Compiled);
        private const string VersionFormat = "v{0}";

        public static void RegisterArea<T>(this RouteCollection routes)
            where T : AreaRegistration, new()
        {
            var registration = new T();
            var context = new AreaRegistrationContext(registration.AreaName, routes, null);
            context.Namespaces.Add(registration.GetType().Namespace + ".*");
            registration.RegisterArea(context);
        }

        public static RouteReference MapAreaRoute<TController>(this AreaRegistrationContext context, string routeUrl, Expression<Func<TController, Func<ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute(true, routeUrl, action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1>(this AreaRegistrationContext context, string routeUrl, Expression<Func<TController, Func<T1, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute(true, routeUrl, action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1, T2>(this AreaRegistrationContext context, string routeUrl, Expression<Func<TController, Func<T1, T2, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute(true, routeUrl, action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1, T2, T3>(this AreaRegistrationContext context, string routeUrl, Expression<Func<TController, Func<T1, T2, T3, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute(true, routeUrl, action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1, T2, T3, T4>(this AreaRegistrationContext context, string routeUrl, Expression<Func<TController, Func<T1, T2, T3, T4, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute(true, routeUrl, action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1, T2, T3, T4, T5>(this AreaRegistrationContext context, string routeUrl, Expression<Func<TController, ControllerFunc<T1, T2, T3, T4, T5, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute(true, routeUrl, action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1, T2, T3, T4, T5, T6>(this AreaRegistrationContext context, string routeUrl, Expression<Func<TController, ControllerFunc<T1, T2, T3, T4, T5, T6, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute(true, routeUrl, action, constraints);
        }

        public static RouteReference MapAreaRoute<TController>(this AreaRegistrationContext context, bool nameRoute, string routeUrl, Expression<Func<TController, Func<ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute<TController>(nameRoute, routeUrl, (LambdaExpression) action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1>(this AreaRegistrationContext context, bool nameRoute, string routeUrl, Expression<Func<TController, Func<T1, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute<TController>(nameRoute, routeUrl, action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1, T2>(this AreaRegistrationContext context, bool nameRoute, string routeUrl, Expression<Func<TController, Func<T1, T2, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute<TController>(nameRoute, routeUrl, action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1, T2, T3>(this AreaRegistrationContext context, bool nameRoute, string routeUrl, Expression<Func<TController, Func<T1, T2, T3, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute<TController>(nameRoute, routeUrl, action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1, T2, T3, T4>(this AreaRegistrationContext context, bool nameRoute, string routeUrl, Expression<Func<TController, Func<T1, T2, T3, T4, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute<TController>(nameRoute, routeUrl, action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1, T2, T3, T4, T5>(this AreaRegistrationContext context, bool nameRoute, string routeUrl, Expression<Func<TController, ControllerFunc<T1, T2, T3, T4, T5, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute<TController>(nameRoute, routeUrl, action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1, T2, T3, T4, T5, T6>(this AreaRegistrationContext context, bool nameRoute, string routeUrl, Expression<Func<TController, ControllerFunc<T1, T2, T3, T4, T5, T6, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute<TController>(nameRoute, routeUrl, action, constraints);
        }

        public static RouteReference MapAreaRoute<TController>(this AreaRegistrationContext context, int version, string routeUrl, Expression<Func<TController, Func<ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute<TController>(true, GetRouteUrl(version, routeUrl), (LambdaExpression) action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1>(this AreaRegistrationContext context, int version, string routeUrl, Expression<Func<TController, Func<T1, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute<TController>(true, GetRouteUrl(version, routeUrl), action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1, T2>(this AreaRegistrationContext context, int version, string routeUrl, Expression<Func<TController, Func<T1, T2, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute<TController>(true, GetRouteUrl(version, routeUrl), action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1, T2, T3>(this AreaRegistrationContext context, int version, string routeUrl, Expression<Func<TController, Func<T1, T2, T3, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute<TController>(true, GetRouteUrl(version, routeUrl), action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1, T2, T3, T4>(this AreaRegistrationContext context, int version, string routeUrl, Expression<Func<TController, Func<T1, T2, T3, T4, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute<TController>(true, GetRouteUrl(version, routeUrl), action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1, T2, T3, T4, T5>(this AreaRegistrationContext context, int version, string routeUrl, Expression<Func<TController, ControllerFunc<T1, T2, T3, T4, T5, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute<TController>(true, GetRouteUrl(version, routeUrl), action, constraints);
        }

        public static RouteReference MapAreaRoute<TController, T1, T2, T3, T4, T5, T6>(this AreaRegistrationContext context, int version, string routeUrl, Expression<Func<TController, ControllerFunc<T1, T2, T3, T4, T5, T6, ActionResult>>> action, params IRouteConstraint[] constraints)
        {
            return context.MapAreaRoute<TController>(true, GetRouteUrl(version, routeUrl), action, constraints);
        }

        public static Route MapRedirectRoute(this AreaRegistrationContext context, string routeUrl, RouteReference redirectRoute, object routeValues, params IRouteConstraint[] constraints)
        {
            var routeValueDictionary = routeValues == null ? null : new RouteValueDictionary(routeValues);

            var route = new Route(GetMapRouteUrl(routeUrl), new RedirectRouteHandler(redirectRoute, routeValueDictionary, true))
            {
                Constraints = CreateConstraints(false, routeValueDictionary, constraints)
            };
            context.Routes.Add(route);
            return route;
        }

        public static Route MapRedirectRoute(this AreaRegistrationContext context, string routeUrl, RouteReference redirectRoute, params IRouteConstraint[] constraints)
        {
            return context.MapRedirectRoute(routeUrl, redirectRoute, null, constraints);
        }

        public static Route MapRedirectUrl(this AreaRegistrationContext context, string routeUrl, string redirectUrl, params IRouteConstraint[] constraints)
        {
            var route = new Route(GetMapRouteUrl(routeUrl), new RedirectUrlHandler(redirectUrl, true))
            {
                Constraints = CreateConstraints(false, null, constraints)
            };
            context.Routes.Add(route);
            return route;
        }

        public static Route MapJsonNotFoundRoute(this AreaRegistrationContext context, string routeUrl)
        {
            var route = new Route(GetMapRouteUrl(routeUrl), new JsonNotFoundRouteHandler());
            context.Routes.Add(route);
            return route;
        }

        private static string GetMapRouteUrl(string routeUrl)
        {
            // If it is a catch all let it through.
            // (The word 'catchall' isn't strictly required, it could be anything.
            // If somone wants to add another word at some stage then this will need to be updated.)

            if (routeUrl.EndsWith("/{*catchall}"))
                return routeUrl;

            // If it is not an ASP.NET page add the default to it.

            return routeUrl.EndsWith(".aspx")
                ? routeUrl
                : routeUrl.AddUrlSegments("default.aspx");
        }

        private static string GetRouteUrl(int version, string routeUrl)
        {
            return string.Format(VersionFormat, version).AddUrlSegments(routeUrl);
        }

        private static RouteReference MapAreaRoute<TController>(this AreaRegistrationContext context, bool nameRoute, string routeUrl, LambdaExpression action, IList<IRouteConstraint> constraints)
        {
            var methodInfo = GetMethodInfo(action);
            var actionName = methodInfo.Name;
            var ensureHttps = GetEnsureHttps<TController>(methodInfo);
            var isMobile = GetIsMobile<TController>();
            var routeName = nameRoute ? string.Join(".", new[] { typeof(TController).FullName, actionName }) : null;

            var defaults = new RouteValueDictionary
            {
                { "controller", GetControllerName<TController>() },
                { "action", actionName }
            };

            var route = new AreaRoute(routeName, ensureHttps, routeUrl, new MvcRouteHandler())
            {
                Defaults = defaults,
                Constraints = CreateConstraints(routeUrl, methodInfo, isMobile, constraints),
                DataTokens = new RouteValueDictionary()
            };

            context.Routes.Add(routeName, route);

            // DataTokens.

            route.DataTokens["area"] = context.AreaName;
            route.DataTokens["Namespaces"] = new[] { typeof(TController).Namespace };
            route.DataTokens["UseNamespaceFallback"] = false;

            return route.Reference; 
        }

        private static bool? GetEnsureHttps<TController>(ICustomAttributeProvider methodInfo)
        {
            // Look for the attribute on the method and then the class.

            var attributes = methodInfo.GetCustomAttributes(typeof(EnsureHttpsAttribute), false);
            if (attributes.Length > 0)
                return true;

            attributes = methodInfo.GetCustomAttributes(typeof(EnsureHttpAttribute), false);
            if (attributes.Length > 0)
                return false;

            attributes = typeof(TController).GetCustomAttributes(typeof(EnsureHttpsAttribute), true);
            if (attributes.Length > 0)
                return true;

            attributes = typeof(TController).GetCustomAttributes(typeof(EnsureHttpAttribute), false);
            if (attributes.Length > 0)
                return false;

            return null;
        }

        private static bool GetIsMobile<TController>()
        {
            var controllerName = typeof(TController).Name;
            return controllerName.EndsWith("MobileController");
        }

        private static string GetControllerName<TController>()
        {
            var controllerName = typeof(TController).Name;
            if (controllerName.EndsWith("Controller"))
                controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);
            return controllerName;
        }

        private static MethodInfo GetMethodInfo(LambdaExpression expression)
        {
            // Follow the expression.

            var body = (UnaryExpression)expression.Body;
            var operand = (MethodCallExpression)body.Operand;
            var argument = (ConstantExpression)operand.Arguments[2];
            return (MethodInfo)argument.Value;
        }

        private static RouteValueDictionary CreateConstraints(bool isMobile, ICollection<KeyValuePair<string, object>> routeValues, IList<IRouteConstraint> routeConstraints)
        {
            if (!isMobile && routeValues == null && (routeConstraints == null || routeConstraints.Count == 0))
                return null;

            var constraints = new RouteValueDictionary();

            if (isMobile)
                constraints.Add("IsMobile", new MobileConstraint());

            if (routeValues != null && routeValues.Count > 0)
            {
                foreach (var routeValue in routeValues.Where(v => v.Value is RedirectQueryString && ((RedirectQueryString)v.Value).IsRequired))
                    constraints.Add(string.Format("redirect{0}", routeValue.Key), new QueryStringConstraint(routeValue.Key));
            }

            for (var index = 0; index < routeConstraints.Count; ++index)
                constraints.Add(string.Format("constraint{0}", index), routeConstraints[index]);

            return constraints;
        }

        private static RouteValueDictionary CreateConstraints(string routeUrl, MethodInfo methodInfo, bool isMobile, IList<IRouteConstraint> routeConstraints)
        {
            var constraints = CreateConstraints(isMobile, null, routeConstraints);

            // Only generate constraints for those parameters in the route.

            var keys = GetConstraintKeys(routeUrl);
            foreach (var parameterInfo in methodInfo.GetParameters())
            {
                if (keys.Contains(parameterInfo.Name))
                {
                    var constraint = CreateConstraint(parameterInfo.ParameterType);
                    if (constraint != null)
                    {
                        if (constraints == null)
                            constraints = new RouteValueDictionary();
                        constraints.Add(parameterInfo.Name, constraint);
                    }
                }
            }

            return constraints;
        }

        private static IRouteConstraint CreateConstraint(Type type)
        {
            if (type == typeof(int) || type == typeof(int?))
                return new IntConstraint();
            if (type == typeof(Guid) || type == typeof(Guid?))
                return new GuidConstraint();
            if (type.IsEnum)
                return new EnumConstraint(type);
            return null;
        }

        private static IList<string> GetConstraintKeys(string routeUrl)
        {
            var keys = new List<string>();
            var match = KeysRegex.Match(routeUrl);
            while (match.Success)
            {
                keys.Add(match.Groups[0].Value.Substring(1, match.Groups[0].Value.Length - 2));
                match = match.NextMatch();
            }

            return keys;
        }
    }
}
