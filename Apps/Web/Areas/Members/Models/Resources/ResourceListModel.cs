using System;
using System.Collections.Generic;
using LinkMe.Domain.Resources;
using LinkMe.Query.Search.Resources;

namespace LinkMe.Web.Areas.Members.Models.Resources
{
    public abstract class ResourceModel
    {
        public ResourcesPresentationModel Presentation { get; set; }
        public ResourceSearchCriteria Criteria { get; set; }

        public IList<Category> Categories { get; set; }

        public Article TopRatedArticle { get; set; }
        public QnA TopViewedQnA { get; set; }
        public IList<Resource> RelatedItems { get; set; }
        public IList<Resource> RecentItems { get; set; }

        public IDictionary<Guid, int> Viewings { get; set; }
        public IDictionary<Guid, ResourceRatingSummary> Ratings { get; set; }
        public IDictionary<Guid, int> Comments { get; set; }
        public IDictionary<Guid, DateTime> LastViewedTimes { get; set; }

        public int ResumePercentComplete { get; set; }
        public bool HasResume { get; set; }
    }

    public class ResourceModel<TResource>
        : ResourceModel
    {
        public TResource Resource { get; set; }
    }

    public class ResourceListResultsModel<TResource>
        where TResource : Resource
    {
        public IList<Guid> ResourceIds { get; set; }
        public IDictionary<Guid, TResource> Resources { get; set; }
    }

    public abstract class ResourceListModel
        : ResourceModel
    {
        public IDictionary<ResourceType, int> TotalResources { get; set; }
    }

    public class ResourceListModel<TResource>
        : ResourceListModel
        where TResource : Resource
    {
        public ResourceListResultsModel<TResource> Results { get; set; }
    }

    public class ResourceItemModel<TResource>
        where TResource : Resource
    {
        public ResourceListModel<TResource> List { get; set; }
        public TResource Item { get; set; }
    }
}
