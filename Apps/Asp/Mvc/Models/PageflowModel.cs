using System;
using System.Collections.Generic;
using LinkMe.Apps.Pageflows;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Mvc.Models
{
    public class StepsModel
    {
        public IList<IPageflowStep> Steps { get; set; }
        public int CurrentStepIndex { get; set; }
    }

    public class PageflowModel
    {
        public Guid InstanceId { get; set; }
        public StepsModel Steps { get; set; }

        public ReadOnlyUrl GetUrl(ReadOnlyUrl clientUrl)
        {
            var instanceId = GetInstanceId(clientUrl);
            if (instanceId == null || instanceId.Value != InstanceId)
            {
                var url = clientUrl.AsNonReadOnly();
                url.QueryString.Remove("instanceId");
                url.QueryString["instanceId"] = InstanceId.ToString();
                return url;
            }

            return clientUrl;
        }

        private static Guid? GetInstanceId(ReadOnlyUrl url)
        {
            var value = url.QueryString["instanceId"];
            if (string.IsNullOrEmpty(value))
                return null;
            Guid instanceId;
            if (Guid.TryParse(value, out instanceId))
                return instanceId;
            return null;
        }
    }
}
