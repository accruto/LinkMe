using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Services.External.Disqus;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Query.Resources;
using LinkMe.Query.Search.Resources;
using LinkMe.Query.Search.Resources.Commands;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Models.Resources;
using LinkMe.Web.Areas.Members.Routes;
using LinkMe.Web.Context;

namespace LinkMe.Web.Areas.Members.Controllers.Resources
{
    public class ResourcesController
        : MembersController
    {
        private readonly IExecuteResourceSearchCommand _executeResourceSearchCommand;
        private readonly IPollsQuery _pollsQuery;
        private readonly IResumesQuery _resumesQuery;
        private readonly IMemberStatusQuery _memberStatusQuery;
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly IResourcesQuery _resourcesQuery;
        private readonly IDisqusQuery _disqusQuery;
        private const int DefaultItemsPerPage = 5;
        private const int RecentItemsCount = 5;
        private const int RelatedItemsCount = 5;

        public ResourcesController(IResumesQuery resumesQuery, ICandidatesQuery candidatesQuery, IMemberStatusQuery memberStatusQuery, IResourcesQuery resourcesQuery, IPollsQuery pollsQuery, IExecuteResourceSearchCommand executeResourceSearchCommand, IDisqusQuery disqusQuery)
        {
            _resumesQuery = resumesQuery;
            _candidatesQuery = candidatesQuery;
            _memberStatusQuery = memberStatusQuery;
            _resourcesQuery = resourcesQuery;
            _disqusQuery = disqusQuery;
            _executeResourceSearchCommand = executeResourceSearchCommand;
            _pollsQuery = pollsQuery;
        }

        public ActionResult Resources()
        {
            // Gather all the pieces.

            var poll = _pollsQuery.GetActivePoll();

            var featuredArticles = _resourcesQuery.GetFeaturedArticles();
            var articles = _resourcesQuery.GetArticles(from a in featuredArticles select a.ResourceId);

            var featuredVideo = _resourcesQuery.GetFeaturedVideos().Single();
            var video = _resourcesQuery.GetVideo(featuredVideo.ResourceId);

            var featuredQnA = _resourcesQuery.GetFeaturedQnAs().Single();
            var qna = _resourcesQuery.GetQnA(featuredQnA.ResourceId);

            return View(new ResourcesModel
            {
                Categories = _resourcesQuery.GetCategories(),
                FeaturedArticles = featuredArticles.Select(r => new FeaturedArticleModel { FeaturedResource = r, Article = articles.Single(a => a.Id == r.ResourceId) }).ToList(),
                FeaturedVideo = video,
                FeaturedQnA = qna,
                FeaturedQnAViews = qna != null ? _resourcesQuery.GetViewingCount(qna.Id) : 0,
                FeaturedQnAComments = qna != null ? _disqusQuery.GetCommentCount(qna.Id) ?? 0 : 0,
                ActivePoll = GetPollModel(poll),
            });
        }

        public ActionResult Articles(ResourceSearchCriteria criteria, ResourcesPresentationModel presentation)
        {
            return View("Articles", Resources(criteria, presentation, i => _resourcesQuery.GetArticles(i), u => _resourcesQuery.GetRecentlyViewedArticles(u, RecentItemsCount)));
        }

        public ActionResult Videos(ResourceSearchCriteria criteria, ResourcesPresentationModel presentation)
        {
            return View("Videos", Resources(criteria, presentation, i => _resourcesQuery.GetVideos(i), u => _resourcesQuery.GetRecentlyViewedVideos(u, RecentItemsCount)));
        }

        public ActionResult QnAs(ResourceSearchCriteria criteria, ResourcesPresentationModel presentation)
        {
            return View("QnAs", Resources(criteria, presentation, i => _resourcesQuery.GetQnAs(i), u => _resourcesQuery.GetRecentlyViewedQnAs(u, RecentItemsCount)));
        }

        public ActionResult CategoryArticles(ResourceSearchCriteria criteria, ResourcesPresentationModel presentation)
        {
            return Articles(criteria, presentation);
        }

        public ActionResult CategoryVideos(ResourceSearchCriteria criteria, ResourcesPresentationModel presentation)
        {
            return Videos(criteria, presentation);
        }

        public ActionResult CategoryQnAs(ResourceSearchCriteria criteria, ResourcesPresentationModel presentation)
        {
            return QnAs(criteria, presentation);
        }

        public ActionResult PartialArticles(ResourceSearchCriteria criteria, ResourcesPresentationModel presentation)
        {
            return PartialView("PartialArticles", PartialResources(criteria, presentation, i => _resourcesQuery.GetArticles(i), u => _resourcesQuery.GetRecentlyViewedArticles(u, RecentItemsCount)));
        }

        public ActionResult PartialVideos(ResourceSearchCriteria criteria, ResourcesPresentationModel presentation)
        {
            return PartialView("PartialVideos", PartialResources(criteria, presentation, i => _resourcesQuery.GetVideos(i), u => _resourcesQuery.GetRecentlyViewedVideos(u, RecentItemsCount)));
        }

        public ActionResult PartialQnAs(ResourceSearchCriteria criteria, ResourcesPresentationModel presentation)
        {
            return PartialView("PartialQnAs", PartialResources(criteria, presentation, i => _resourcesQuery.GetQnAs(i), u => _resourcesQuery.GetRecentlyViewedQnAs(u, RecentItemsCount)));
        }

        public ActionResult RecentArticles()
        {
            return View(RecentResources(u => _resourcesQuery.GetRecentlyViewedArticles(u, RecentItemsCount)));
        }

        public ActionResult RecentVideos()
        {
            return View(RecentResources(u => _resourcesQuery.GetRecentlyViewedVideos(u, RecentItemsCount)));
        }

        public ActionResult RecentQnAs()
        {
            return View(RecentResources(u => _resourcesQuery.GetRecentlyViewedQnAs(u, RecentItemsCount)));
        }

        public ActionResult Search(ResourceSearchCriteria criteria)
        {
            switch (criteria.ResourceType)
            {
                case ResourceType.Video:
                    return Videos(criteria, new ResourcesPresentationModel());

                case ResourceType.QnA:
                    return QnAs(criteria, new ResourcesPresentationModel());

                default:
                    return Articles(criteria, new ResourcesPresentationModel());
            }
        }

        public ActionResult PartialCurrent()
        {
            var currentSearch = MemberContext.CurrentResourcesSearch;

            var criteria = currentSearch != null && currentSearch.Criteria != null
                ? MemberContext.CurrentResourcesSearch.Criteria
                : new ResourceSearchCriteria { ResourceType = GetResourceType<Article>() };

            var presentation = currentSearch != null
                ? new ResourcesPresentationModel { Pagination = currentSearch.Pagination }
                : new ResourcesPresentationModel();

            switch (criteria.ResourceType)
            {
                case ResourceType.QnA:
                    return PartialQnAs(criteria, presentation);

                case ResourceType.Video:
                    return PartialVideos(criteria, presentation);

                default:
                    return PartialArticles(criteria, presentation);
            }
        }

        public ActionResult PartialRecentArticles(ResourcesPresentationModel presentation)
        {
            return PartialView(PartialRecentResources(presentation, u => _resourcesQuery.GetRecentlyViewedArticles(u, RecentItemsCount)));
        }

        public ActionResult PartialRecentVideos(ResourcesPresentationModel presentation)
        {
            return PartialView(PartialRecentResources(presentation, u => _resourcesQuery.GetRecentlyViewedVideos(u, RecentItemsCount)));
        }

        public ActionResult PartialRecentQnAs(ResourcesPresentationModel presentation)
        {
            return PartialView(PartialRecentResources(presentation, u => _resourcesQuery.GetRecentlyViewedQnAs(u, RecentItemsCount)));
        }

        public ActionResult Article(Guid id)
        {
            var model = GetModel(id, CurrentUser.Id, i => _resourcesQuery.GetArticle(i), u => _resourcesQuery.GetRecentlyViewedArticles(u, RecentItemsCount));
            if (model == null)
                return NotFound("article", "id", id);

            // Check url.

            var result = EnsureUrl(model.Resource.GenerateUrl(model.Categories));
            return result ?? View(model);
        }

        public ActionResult Video(Guid id)
        {
            var model = GetModel(id, CurrentUser.Id, i => _resourcesQuery.GetVideo(i), u => _resourcesQuery.GetRecentlyViewedVideos(u, RecentItemsCount));
            if (model == null)
                return NotFound("video", "id", id);

            // Check url.

            var result = EnsureUrl(model.Resource.GenerateUrl(model.Categories));
            return result ?? View(model);
        }

        public ActionResult QnA(Guid id)
        {
            var model = GetModel(id, CurrentUser.Id, i => _resourcesQuery.GetQnA(i), u => _resourcesQuery.GetRecentlyViewedQnAs(u, RecentItemsCount));
            if (model == null)
                return NotFound("question", "id", id);

            // Check url.

            var result = EnsureUrl(model.Resource.GenerateUrl(model.Categories));
            return result ?? View(model);
        }

        public ActionResult PartialArticle(Guid id)
        {
            var model = GetPartialResourceModel(id, CurrentUser.Id, _resourcesQuery.GetCategories(), i => _resourcesQuery.GetArticle(i), u => _resourcesQuery.GetRecentlyViewedArticles(u, RecentItemsCount));
            return model == null
                ? NotFound("article", "id", id)
                : PartialView(model);
        }

        public ActionResult PartialVideo(Guid id)
        {
            var model = GetPartialResourceModel(id, CurrentUser.Id, _resourcesQuery.GetCategories(), i => _resourcesQuery.GetVideo(i), u => _resourcesQuery.GetRecentlyViewedVideos(u, RecentItemsCount));
            return model == null
                ? NotFound("video", "id", id)
                : PartialView(model);
        }

        public ActionResult PartialQnA(Guid id)
        {
            var model = GetPartialResourceModel(id, CurrentUser.Id, _resourcesQuery.GetCategories(), i => _resourcesQuery.GetQnA(i), u => _resourcesQuery.GetRecentlyViewedQnAs(u, RecentItemsCount));
            return model == null
                ? NotFound("question", "id", id)
                : PartialView(model);
        }

        private ResourceSearchCriteria GetCurrentCriteria<TResource>()
            where TResource : Resource
        {
            var currentSearch = MemberContext.CurrentResourcesSearch;

            // Return a default criteria if there is no current search available.

            return currentSearch != null && currentSearch.Criteria != null
                ? MemberContext.CurrentResourcesSearch.Criteria
                : new ResourceSearchCriteria { ResourceType = GetResourceType<TResource>() };
        }

        private static ResourceType GetResourceType<TResource>()
            where TResource : Resource
        {
            return typeof(TResource) == typeof(QnA)
                ? ResourceType.QnA
                : typeof(TResource) == typeof(Video)
                    ? ResourceType.Video
                    : ResourceType.Article;
        }

        private IDictionary<Guid, int> GetViewings<TResource>(ResourceListModel<TResource> model)
            where TResource : Resource
        {
            return GetViewings(model.Results.ResourceIds, model);
        }

        private IDictionary<Guid, int> GetViewings<TResource>(ResourceModel<TResource> model)
            where TResource : Resource
        {
            return GetViewings(new[] { model.Resource.Id }, model);
        }

        private IDictionary<Guid, int> GetViewings(IEnumerable<Guid> resourceIds, ResourceModel model)
        {
            if (model.RelatedItems != null)
                resourceIds = resourceIds.Concat(model.RelatedItems.Select(i => i.Id));
            if (model.RecentItems != null)
                resourceIds = resourceIds.Concat(model.RecentItems.Select(i => i.Id));
            if (model.TopRatedArticle != null)
                resourceIds = resourceIds.Concat(new[] { model.TopRatedArticle.Id });
            if (model.TopViewedQnA != null)
                resourceIds = resourceIds.Concat(new[] { model.TopViewedQnA.Id });

            var distinctResourceIds = resourceIds.Distinct().ToList();
            var viewings = _resourcesQuery.GetViewingCounts(distinctResourceIds);

            // Ensure that all ids are represented.

            return (from i in distinctResourceIds
                    let v = viewings.ContainsKey(i) ? viewings[i] : 0
                    select new { i, v }).ToDictionary(x => x.i, x => x.v);
        }

        private IDictionary<Guid, DateTime> GetLastViewedTimes(ResourceModel model)
        {
            // Only need for recent items.

            var resourceIds = model.RecentItems != null
                ? model.RecentItems.Select(i => i.Id)
                : new Guid[0];

            return _resourcesQuery.GetLastViewedTimes(resourceIds).ToDictionary(x => x.Key, x => x.Value);
        }

        private IDictionary<Guid, ResourceRatingSummary> GetRatings<TResource>(Guid userId, ResourceListModel<TResource> model)
            where TResource : Resource
        {
            return GetRatings<TResource>(userId, model.Results.ResourceIds, model);
        }

        private IDictionary<Guid, ResourceRatingSummary> GetRatings<TResource>(Guid userId, ResourceModel<TResource> model)
            where TResource : Resource
        {
            return GetRatings<TResource>(userId, new[] { model.Resource.Id }, model);
        }

        private IDictionary<Guid, ResourceRatingSummary> GetRatings<TResource>(Guid userId, IEnumerable<Guid> ids, ResourceModel model)
            where TResource : Resource
        {
            // Only need ratings for articles.

            var resourceIds = typeof(TResource) == typeof(Article)
                ? ids
                : new Guid[0];

            if (model.RelatedItems != null)
                resourceIds = resourceIds.Concat(model.RelatedItems.OfType<Article>().Select(i => i.Id));

            if (typeof(TResource) == typeof(Article) && model.RecentItems != null)
                resourceIds = resourceIds.Concat(model.RecentItems.Select(i => i.Id));

            if (model.TopRatedArticle != null)
                resourceIds = resourceIds.Concat(new[] { model.TopRatedArticle.Id });

            var distinctResourceIds = resourceIds.Distinct().ToList();
            var ratings = _resourcesQuery.GetRatingSummaries(userId, distinctResourceIds);

            // Ensure that all ids are repesented.

            return (from i in distinctResourceIds
                    let r = ratings.ContainsKey(i) ? ratings[i] : new ResourceRatingSummary()
                    select new { i, r }).ToDictionary(x => x.i, x => x.r);
        }

        private IDictionary<Guid, int> GetComments<TResource>(ResourceListModel<TResource> model)
            where TResource : Resource
        {
            return GetComments<TResource>(model.Results.ResourceIds, model);
        }

        private IDictionary<Guid, int> GetComments<TResource>(ResourceModel<TResource> model)
            where TResource : Resource
        {
            return GetComments<TResource>(new[] { model.Resource.Id }, model);
        }

        private IDictionary<Guid, int> GetComments<TResource>(IEnumerable<Guid> ids, ResourceModel model)
            where TResource : Resource
        {
            // Only need comments for qnas.

            var resourceIds = typeof(TResource) == typeof(QnA)
                ? ids
                : new Guid[0];

            if (model.RelatedItems != null)
                resourceIds = resourceIds.Concat(model.RelatedItems.OfType<QnA>().Select(i => i.Id));

            if (typeof(TResource) == typeof(QnA) && model.RecentItems != null)
                resourceIds = resourceIds.Concat(model.RecentItems.Select(i => i.Id));

            if (model.TopViewedQnA != null)
                resourceIds = resourceIds.Concat(new[] { model.TopViewedQnA.Id });

            var distinctResourceIds = resourceIds.Distinct().ToList();

            // Ensure that all ids are repesented.

            return (from i in distinctResourceIds
                    let c = _disqusQuery.GetCommentCount(i) ?? 0
                    select new { i, c }).ToDictionary(x => x.i, x => x.c);
        }

        private static bool HasResume(ICandidate candidate)
        {
            return candidate != null && candidate.ResumeId == null;
        }

        private static ResourcesPresentationModel PreparePresentationModel(ResourcesPresentationModel model)
        {
            // Ensure that the pagination values are always set.

            if (model == null)
                model = new ResourcesPresentationModel();
            if (model.Pagination == null)
                model.Pagination = new Pagination();
            if (model.Pagination.Page == null)
                model.Pagination.Page = 1;
            if (model.Pagination.Items == null)
                model.Pagination.Items = DefaultItemsPerPage;
            model.ItemsPerPage = Reference.ItemsPerPage;
            model.DefaultItemsPerPage = DefaultItemsPerPage;

            return model;
        }

        private ResourceListModel<TResource> Resources<TResource>(ResourceSearchCriteria criteria, ResourcesPresentationModel presentation, Func<IList<Guid>, IList<TResource>> getResources, Func<Guid, IList<TResource>> getRecentlyViewedResources)
            where TResource : Resource
        {
            // Save the current search.

            criteria.ResourceType = GetResourceType<TResource>();
            criteria.SortCriteria = new ResourceSearchSortCriteria { SortOrder = ResourceSortOrder.CreatedTime, ReverseSortOrder = false };
            presentation = PreparePresentationModel(presentation);
            MemberContext.CurrentResourcesSearch = new ResourcesSearchNavigation(criteria, presentation);
            
            // Search.

            var user = CurrentUser;
            var categories = _resourcesQuery.GetCategories();
            var model = Search(user.Id, criteria, presentation, categories, getResources, getRecentlyViewedResources);

            // Set the rest of the data for the view.

            return GetResourceListModel(user, model);
        }

        private ResourceListModel<TResource> PartialResources<TResource>(ResourceSearchCriteria criteria, ResourcesPresentationModel presentation, Func<IList<Guid>, IList<TResource>> getResources, Func<Guid, IList<TResource>> getRecentlyViewedResources)
            where TResource : Resource
        {
            // Save the current search.

            criteria.ResourceType = GetResourceType<TResource>();
            criteria.SortCriteria = new ResourceSearchSortCriteria { SortOrder = ResourceSortOrder.CreatedTime, ReverseSortOrder = false };
            presentation = PreparePresentationModel(presentation);
            MemberContext.CurrentResourcesSearch = new ResourcesSearchNavigation(criteria, presentation);

            // Search.

            var user = CurrentUser;
            var categories = _resourcesQuery.GetCategories();
            var model = Search(user.Id, criteria, presentation, categories, getResources, getRecentlyViewedResources);

            // Set the data for all resources in the model.

            return GetPartialResourceListModel(user, model);
        }

        private ResourceListModel<TResource> RecentResources<TResource>(Func<Guid, IList<TResource>> getRecentlyViewedResources)
            where TResource : Resource
        {
            var presentation = PreparePresentationModel(new ResourcesPresentationModel());
            var user = CurrentUser;

            // Get the resources.

            var model = GetRecentlyViewedResources(user.Id, presentation, getRecentlyViewedResources);

            // Set the rest of the data for the view.

            return GetResourceListModel(user, model);
        }

        private ResourceListModel<TResource> PartialRecentResources<TResource>(ResourcesPresentationModel presentation, Func<Guid, IList<TResource>> getRecentlyViewedResources)
            where TResource : Resource
        {
            presentation = PreparePresentationModel(presentation);
            var user = CurrentUser;

            // Get the resources.

            var model = GetRecentlyViewedResources(user.Id, presentation, getRecentlyViewedResources);

            // Set the data for all resources in the model.

            return GetPartialResourceListModel(user, model);
        }

        private ResourceListModel<TResource> Search<TResource>(Guid userId, ResourceSearchCriteria criteria, ResourcesPresentationModel presentation, IList<Category> categories, Func<IList<Guid>, IList<TResource>> getResources, Func<Guid, IList<TResource>> getRecentlyViewedResources)
            where TResource : Resource
        {
            // Search.

            var execution = _executeResourceSearchCommand.Search(criteria, presentation.Pagination.ToRange());

            return new ResourceListModel<TResource>
            {
                Categories = categories,
                Presentation = presentation,
                Criteria = execution.Criteria,
                TotalResources = execution.Results.ResourceTypeHits.ToDictionary(h => h.Key, h => h.Value),
                Results = new ResourceListResultsModel<TResource>
                {
                    ResourceIds = execution.Results.ResourceIds,
                    Resources = getResources(execution.Results.ResourceIds).ToDictionary(r => r.Id, r => r),
                },
                RecentItems = getRecentlyViewedResources(userId).Cast<Resource>().ToList(),
            };
        }

        private ResourceListModel<TResource> GetRecentlyViewedResources<TResource>(Guid userId, ResourcesPresentationModel presentation, Func<Guid, IList<TResource>> getRecentlyViewedResources)
            where TResource : Resource
        {
            var results = getRecentlyViewedResources(userId);

            return new ResourceListModel<TResource>
            {
                Categories = _resourcesQuery.GetCategories(),
                Presentation = presentation,
                Criteria = new ResourceSearchCriteria { ResourceType = GetResourceType<TResource>() },
                Results = new ResourceListResultsModel<TResource>
                {
                    ResourceIds = results.Select(r => r.Id).ToList(),
                    Resources = results.ToDictionary(r => r.Id, r => r),
                },
            };
        }

        private ResourceListModel<TResource> GetResourceListModel<TResource>(IHasId<Guid> user, ResourceListModel<TResource> model)
            where TResource : Resource
        {
            if (user is Member)
            {
                var candidate = _candidatesQuery.GetCandidate(user.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
                model.ResumePercentComplete = _memberStatusQuery.GetPercentComplete(CurrentMember, candidate, resume);
                model.HasResume = HasResume(candidate);
            }
            else
            {
                model.ResumePercentComplete = 0;
                model.HasResume = false;
            }

            model.TopRatedArticle = _resourcesQuery.GetTopRatedArticle();
            model.TopViewedQnA = _resourcesQuery.GetTopViewedQnA();

            // Set the data for all resources in the model.

            model.Viewings = GetViewings(model);
            model.LastViewedTimes = GetLastViewedTimes(model);
            model.Ratings = GetRatings(user.Id, model);
            model.Comments = GetComments(model);
            return model;
        }

        private ResourceListModel<TResource> GetPartialResourceListModel<TResource>(IHasId<Guid> user, ResourceListModel<TResource> model)
            where TResource : Resource
        {
            // Set the data for all resources in the model.

            model.Viewings = GetViewings(model);
            model.LastViewedTimes = GetLastViewedTimes(model);
            model.Ratings = GetRatings(user.Id, model);
            model.Comments = GetComments(model);
            return model;
        }

        private PollModel GetPollModel(Poll poll)
        {
            return new PollModel
            {
                Poll = poll,
                Votes = poll == null ? null : _pollsQuery.GetPollAnswerVotes(poll.Id)
            };
        }

        private ResourceModel<TResource> GetModel<TResource>(Guid resourceId, Guid userId, Func<Guid, TResource> getResource, Func<Guid, IList<TResource>> getRecentlyViewedResources)
            where TResource : Resource
        {
            // Get the model.

            var categories = _resourcesQuery.GetCategories();
            var model = GetResourceModel(resourceId, userId, categories, getResource, getRecentlyViewedResources);
            if (model == null)
                return null;

            // Set the rest of the data for the view.

            if (CurrentMember == null)
            {
                model.ResumePercentComplete = 0;
                model.HasResume = false;
            }
            else
            {
                var candidate = _candidatesQuery.GetCandidate(CurrentMember.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
                model.ResumePercentComplete = _memberStatusQuery.GetPercentComplete(CurrentMember, candidate, resume);
                model.HasResume = HasResume(candidate);
            }

            model.Categories = categories;
            model.TopRatedArticle = _resourcesQuery.GetTopRatedArticle();
            model.TopViewedQnA = _resourcesQuery.GetTopViewedQnA();

            // Set the data for all resources in the model.

            model.Viewings = GetViewings(model);
            model.LastViewedTimes = GetLastViewedTimes(model);
            model.Ratings = GetRatings(userId, model);
            model.Comments = GetComments(model);
            return model;
        }

        private ResourceModel<TResource> GetPartialResourceModel<TResource>(Guid resourceId, Guid userId, IList<Category> categories, Func<Guid, TResource> getResource, Func<Guid, IList<TResource>> getRecentlyViewedResources)
            where TResource : Resource
        {
            // Get the model.

            var model = GetResourceModel(resourceId, userId, categories, getResource, getRecentlyViewedResources);
            if (model == null)
                return null;

            // Set the data for all resources in the model.

            model.Viewings = GetViewings(model);
            model.LastViewedTimes = GetLastViewedTimes(model);
            model.Ratings = GetRatings(userId, model);
            model.Comments = GetComments(model);
            return model;
        }

        private ResourceModel<TResource> GetResourceModel<TResource>(Guid resourceId, Guid userId, IList<Category> categories, Func<Guid, TResource> getResource, Func<Guid, IList<TResource>> getRecentlyViewedResources)
            where TResource : Resource
        {
            // Get the resource.

            var resource = getResource(resourceId);
            if (resource == null)
                return null;

            // Get the current criteria.

            var criteria = GetCurrentCriteria<TResource>();
            if (criteria.CategoryId == null)
            {
                criteria.SubcategoryId = resource.SubcategoryId;
                criteria.CategoryId = categories.GetCategoryBySubcategory(resource.SubcategoryId).Id;
            }

            // Populate the model.

            return new ResourceModel<TResource>
            {
                Categories = categories,
                Presentation = PreparePresentationModel(new ResourcesPresentationModel()),
                Criteria = criteria,
                Resource = resource,
                RecentItems = getRecentlyViewedResources(userId).Cast<Resource>().ToList(),
                RelatedItems = GetRelatedItems(resource, userId)
            };
        }

        private IList<Resource> GetRelatedItems<TResource>(TResource resource, Guid userId)
            where TResource : Resource
        {
            // Display up to 3 of the current type and 1 of each other.
            // If there are insufficient of any type make up numbers with articles.
            // Fetch one more than the required number in case the current item is fetched

            var articles = _resourcesQuery.GetRelatedArticles(resource.SubcategoryId, userId, RelatedItemsCount + 1);
            var videos = _resourcesQuery.GetRelatedVideos(resource.SubcategoryId, userId, RelatedItemsCount + 1);
            var qnas = _resourcesQuery.GetRelatedQnAs(resource.SubcategoryId, userId, RelatedItemsCount + 1);

            var relatedItems = new List<Resource>();

            if (resource is QnA)
            {
                relatedItems = relatedItems.Concat(from q in qnas where q.Id != resource.Id select q).Take(3).ToList();
                relatedItems = relatedItems.Concat(videos.Take(1)).ToList();
                relatedItems = relatedItems.Concat(articles.Take(RelatedItemsCount - relatedItems.Count)).ToList();
            }
            else if (resource is Video)
            {
                relatedItems = relatedItems.Concat(from v in videos where v.Id != resource.Id select v).Take(3).ToList();
                relatedItems = relatedItems.Concat(qnas.Take(1)).ToList();
                relatedItems = relatedItems.Concat(articles.Take(RelatedItemsCount - relatedItems.Count)).ToList();
            }
            else if (resource is Article)
            {
                var relatedQnas = qnas.Take(1).ToList();
                var relatedVideos = videos.Take(1).ToList();

                var articlesCount = RelatedItemsCount - relatedQnas.Count - relatedVideos.Count;
                relatedItems = relatedItems.Concat(from a in articles where a.Id != resource.Id select a).Take(articlesCount).Concat(relatedQnas).Concat(relatedVideos).ToList();
            }

            return relatedItems;
        }
    }
}