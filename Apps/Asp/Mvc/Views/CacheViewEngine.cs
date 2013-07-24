using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using Microsoft.Practices.Unity;

namespace LinkMe.Apps.Asp.Mvc.Views
{
    public abstract class CacheViewEngine
        : ContainerViewEngine
    {
        private const string CacheKeyFormat = ":ViewCacheEntry:{0}:{1}:{2}:{3}:{4}:";
        private const string CacheKeyPrefixMaster = "Master";
        private const string CacheKeyPrefixPartial = "Partial";
        private const string CacheKeyPrefixView = "View";
        private const string DoesNotExist = "DoesNotExist";

        protected struct FindViewName
        {
            public string Name { get; set; }
            public bool Use { get; set; }
        }

        private class ViewLocation
        {
            protected readonly string VirtualPathFormatString;

            public ViewLocation(string virtualPathFormatString)
            {
                VirtualPathFormatString = virtualPathFormatString;
            }

            public virtual string Format(string viewName, string controllerName, string areaName)
            {
                return string.Format(CultureInfo.InvariantCulture, VirtualPathFormatString, viewName, controllerName);
            }
        }

        private class AreaAwareViewLocation
            : ViewLocation
        {
            public AreaAwareViewLocation(string virtualPathFormatString)
                : base(virtualPathFormatString)
            {
            }

            public override string Format(string viewName, string controllerName, string areaName)
            {
                return string.Format(CultureInfo.InvariantCulture, VirtualPathFormatString, viewName, controllerName, areaName);
            }
        }

        protected CacheViewEngine(IUnityContainer container)
            : base(container)
        {
            // Always cache.

            ViewLocationCache = new DefaultViewLocationCache(new TimeSpan(1, 0, 0, 0));
        }

        protected abstract IList<FindViewName> GetFindViewNames(ControllerContext controllerContext, string viewName);

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            var findViewNames = GetFindViewNames(controllerContext, viewName);
            if (findViewNames != null)
            {
                foreach (var findViewName in findViewNames)
                {
                    var result = GetViewResult(controllerContext, findViewName.Name, masterName, useCache);
                    if (result != null)
                    {
                        if (findViewName.Use)
                            return GetViewEngineResult(result);
                        break;
                    }
                }
            }

            return GetViewEngineResult(GetViewEngineResult(controllerContext, viewName, masterName, useCache));
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            var findViewNames = GetFindViewNames(controllerContext, partialViewName);
            if (findViewNames != null)
            {
                foreach (var findViewName in findViewNames)
                {
                    var result = GetPartialViewResult(controllerContext, findViewName.Name, useCache);
                    if (result != null)
                    {
                        if (findViewName.Use)
                            return GetViewEngineResult(result);
                        break;
                    }
                }
            }

            return GetViewEngineResult(GetPartialViewEngineResult(controllerContext, partialViewName, useCache));
        }

        private ViewEngineResult GetPartialViewResult(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            var result = GetPartialViewEngineResult(controllerContext, partialViewName, useCache);
            if (result != null && result.View != null)
                return result;
            return null;
        }

        private ViewEngineResult GetViewResult(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            var result = GetViewEngineResult(controllerContext, viewName, masterName, useCache);
            if (result != null && result.View != null)
                return result;
            return null;
        }

        private ViewEngineResult GetPartialViewEngineResult(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            IList<string> searchedLocations;
            var controllerName = GetControllerName(controllerContext);
            var partialPath = GetPath(controllerContext, PartialViewLocationFormats, AreaPartialViewLocationFormats, partialViewName, controllerName, CacheKeyPrefixPartial, useCache, out searchedLocations);

            return string.IsNullOrEmpty(partialPath)
                ? new ViewEngineResult(searchedLocations)
                : new ViewEngineResult(CreatePartialView(controllerContext, partialPath), this);
        }

        private ViewEngineResult GetViewEngineResult(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            IList<string> viewLocationsSearched;
            IList<string> masterLocationsSearched;

            var controllerName = GetControllerName(controllerContext);
            var viewPath = GetPath(controllerContext, ViewLocationFormats, AreaViewLocationFormats, viewName, controllerName, CacheKeyPrefixView, useCache, out viewLocationsSearched);
            var masterPath = GetPath(controllerContext, MasterLocationFormats, AreaMasterLocationFormats, masterName, controllerName, CacheKeyPrefixMaster, useCache, out masterLocationsSearched);

            return string.IsNullOrEmpty(viewPath) || (string.IsNullOrEmpty(masterPath) && !string.IsNullOrEmpty(masterName))
                ? new ViewEngineResult(viewLocationsSearched.Union(masterLocationsSearched))
                : new ViewEngineResult(CreateView(controllerContext, viewPath, masterPath), this);
        }

        private string GetPath(ControllerContext controllerContext, IEnumerable<string> locations, string[] areaLocations, string name, string controllerName, string cacheKeyPrefix, bool useCache, out IList<string> searchedLocations)
        {
            searchedLocations = new List<string>();

            if (string.IsNullOrEmpty(name))
                return string.Empty;

            var areaName = GetAreaName(controllerContext.RouteData);
            var usingAreas = !string.IsNullOrEmpty(areaName);
            var viewLocations = GetViewLocations(locations, usingAreas ? areaLocations : null);

            var isSpecificPath = IsSpecificPath(name);
            var cacheKey = CreateCacheKey(cacheKeyPrefix, name, isSpecificPath ? string.Empty : controllerName, areaName);

            // Check the cache using the first display mode, i.e. the most specific.

            var displayMode = DisplayModeProvider.GetAvailableDisplayModesForContext(controllerContext.HttpContext, controllerContext.DisplayMode).First();
            if (useCache)
            {
                // Look in the cache.

                var cachedLocation = ViewLocationCache.GetViewLocation(controllerContext.HttpContext, AppendDisplayModeToCacheKey(cacheKey, displayMode.DisplayModeId));
                if (cachedLocation != null)
                {
                    // The location has been found for this display mode meaning it has been resolved either way.

                    if (controllerContext.DisplayMode == null)
                        controllerContext.DisplayMode = displayMode;
                    return cachedLocation == DoesNotExist ? string.Empty : cachedLocation;
                }
            }

            return isSpecificPath
                ? GetPathFromSpecificName(controllerContext, name, cacheKey, out searchedLocations)
                : GetPathFromGeneralName(controllerContext, viewLocations, displayMode, name, controllerName, areaName, cacheKey, out searchedLocations);
        }

        private static bool IsSpecificPath(string name)
        {
            var c = name[0];
            return c == '~' || c == '/';
        }

        private static string GetControllerName(ControllerContext controllerContext)
        {
            var name = controllerContext.RouteData.GetRequiredString("controller");

            if (name.EndsWith("Mobile"))
                return name.Substring(0, name.Length - "Mobile".Length);
            if (name.EndsWith("Web"))
                return name.Substring(0, name.Length - "Web".Length);
            
            return name;
        }

        private static IEnumerable<ViewLocation> GetViewLocations(IEnumerable<string> viewLocationFormats, IEnumerable<string> areaViewLocationFormats)
        {
            var allLocations = new List<ViewLocation>();

            if (areaViewLocationFormats != null)
                allLocations.AddRange(areaViewLocationFormats.Select(areaViewLocationFormat => new AreaAwareViewLocation(areaViewLocationFormat)));

            if (viewLocationFormats != null)
                allLocations.AddRange(viewLocationFormats.Select(viewLocationFormat => new ViewLocation(viewLocationFormat)));

            return allLocations;
        }

        private static string GetAreaName(RouteBase route)
        {
            var routeWithArea = route as IRouteWithArea;
            if (routeWithArea != null)
                return routeWithArea.Area;

            var castRoute = route as Route;
            if (castRoute != null && castRoute.DataTokens != null)
                return castRoute.DataTokens["area"] as string;

            return null;
        }

        private static string GetAreaName(RouteData routeData)
        {
            object area;
            if (routeData.DataTokens.TryGetValue("area", out area))
                return area as string;
            return GetAreaName(routeData.Route);
        }

        private string CreateCacheKey(string prefix, string name, string controllerName, string areaName)
        {
            return string.Format(CultureInfo.InvariantCulture, CacheKeyFormat, GetType().AssemblyQualifiedName, prefix, name, controllerName, areaName);
        }

        private static string AppendDisplayModeToCacheKey(string cacheKey, string displayMode)
        {
            // key format is ":ViewCacheEntry:{cacheType}:{prefix}:{name}:{controllerName}:{areaName}:"
            // so append "{displayMode}:" to the key
            return cacheKey + displayMode + ":";
        }

        private string GetPathFromGeneralName(ControllerContext controllerContext, IEnumerable<ViewLocation> locations, IDisplayMode displayMode, string name, string controllerName, string areaName, string cacheKey, out IList<string> searchedLocations)
        {
            searchedLocations = new List<string>();

            // Iterate through the locations.

            foreach (var location in locations)
            {
                var virtualPath = location.Format(name, controllerName, areaName);

                var virtualPathDisplayInfo = DisplayModeProvider.GetDisplayInfoForVirtualPath(virtualPath, controllerContext.HttpContext, path => FileExists(controllerContext, path), controllerContext.DisplayMode);
                if (virtualPathDisplayInfo != null)
                {
                    // Found a path for a view for a display mode.  Cache it against the most specific display mode.

                    var resolvedVirtualPath = virtualPathDisplayInfo.FilePath;
                    ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, AppendDisplayModeToCacheKey(cacheKey, displayMode.DisplayModeId), resolvedVirtualPath);

                    if (controllerContext.DisplayMode == null)
                        controllerContext.DisplayMode = virtualPathDisplayInfo.DisplayMode;

                    searchedLocations.Clear();
                    return resolvedVirtualPath;
                }

                searchedLocations.Add(virtualPath);
            }

            // Even though it does not exist add it to the cache so that all subsequent lookups don't have to check again.

            ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, AppendDisplayModeToCacheKey(cacheKey, displayMode.DisplayModeId), DoesNotExist);
            return string.Empty;
        }

        private string GetPathFromSpecificName(ControllerContext controllerContext, string name, string cacheKey, out IList<string> searchedLocations)
        {
            var result = name;

            if (!FileExists(controllerContext, name))
            {
                result = string.Empty;
                searchedLocations = new[] { name };
            }
            else
            {
                searchedLocations = new List<string>();
            }

            ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, result);
            return result;
        }
    }
}
