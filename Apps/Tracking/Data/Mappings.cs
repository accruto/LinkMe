using System;
using LinkMe.Framework.Instrumentation.Message;
using LinkMe.Framework.Utility.Event;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Tracking.Data
{
    internal static class Mappings
    {
        public static void MapTo(this SecurityDetail detail, TrackingRequestEntity entity)
        {
            Guid? userId = null;
            try
            {
                userId = new Guid(detail.UserName);
            }
            catch (Exception)
            {
            }

            entity.userId = userId;
        }

        public static void MapTo(this HttpDetail detail, TrackingRequestEntity entity)
        {
            if (detail.Request != null)
                detail.Request.MapTo(entity);
            if (detail.Session != null)
                detail.Session.MapTo(entity);
        }

        public static void MapTo(this ProcessDetail detail, TrackingRequestEntity entity)
        {
            entity.serverHostName = detail.MachineName.Substring(0, Math.Min(detail.MachineName.Length, 128));
            entity.serverProcessId = detail.ProcessId;
        }

        private static void MapTo(this HttpRequestDetail detail, TrackingRequestEntity entity)
        {
            if (!string.IsNullOrEmpty(detail.Url))
            {
                var url = new Url(detail.Url);
                var queryString = url.QueryString.ToString();
                entity.queryString = queryString.Substring(0, Math.Min(queryString.Length, 1024));
                url.QueryString.Clear();
                entity.url = url.AbsoluteUri.Substring(0, Math.Min(url.AbsoluteUri.Length, 1024));
            }

            if (!string.IsNullOrEmpty(detail.UrlReferrer))
            {
                var url = new Url(detail.UrlReferrer);
                var queryString = url.QueryString.ToString();
                entity.referrerQueryString = queryString.Substring(0, Math.Min(queryString.Length, 1024));
                url.QueryString.Clear();
                entity.referrerUrl = url.AbsoluteUri.Substring(0, Math.Min(url.AbsoluteUri.Length, 1024));
            }

            if (detail.User != null)
                detail.User.MapTo(entity);
        }

        private static void MapTo(this HttpRequestUserDetail detail, TrackingRequestEntity entity)
        {
            entity.clientHostName = detail.HostName.Substring(0, Math.Min(detail.HostName.Length, 128));
            entity.clientHostAddress = detail.HostAddress.Substring(0, Math.Min(detail.HostAddress.Length, 128));
            entity.clientAgent = detail.Agent.Substring(0, Math.Min(detail.Agent.Length, 128));
        }

        private static void MapTo(this HttpSessionDetail detail, TrackingRequestEntity entity)
        {
            entity.sessionId = detail.Id.Substring(0, Math.Min(detail.Id.Length, 128));
        }
    }
}