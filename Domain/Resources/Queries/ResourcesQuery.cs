using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Resources.Queries
{
    public class ResourcesQuery
        : IResourcesQuery
    {
        private readonly IResourcesRepository _repository;

        public ResourcesQuery(IResourcesRepository repository)
        {
            _repository = repository;
        }

        IList<Category> IResourcesQuery.GetCategories()
        {
            return _repository.GetResourceCategories();
        }

        Category IResourcesQuery.GetCategory(Guid id)
        {
            return _repository.GetResourceCategory(id);
        }

        Article IResourcesQuery.GetArticle(Guid id)
        {
            return _repository.GetArticle(id);
        }

        IList<Article> IResourcesQuery.GetArticles()
        {
            return _repository.GetArticles();
        }

        IList<Article> IResourcesQuery.GetArticles(IEnumerable<Guid> ids)
        {
            return _repository.GetArticles(ids);
        }

        Video IResourcesQuery.GetVideo(Guid id)
        {
            return _repository.GetVideo(id);
        }

        IList<Video> IResourcesQuery.GetVideos()
        {
            return _repository.GetVideos();
        }

        IList<Video> IResourcesQuery.GetVideos(IEnumerable<Guid> ids)
        {
            return _repository.GetVideos(ids);
        }

        QnA IResourcesQuery.GetQnA(Guid id)
        {
            return _repository.GetQnA(id);
        }

        IList<QnA> IResourcesQuery.GetQnAs()
        {
            return _repository.GetQnAs();
        }

        IList<QnA> IResourcesQuery.GetQnAs(IEnumerable<Guid> ids)
        {
            return _repository.GetQnAs(ids);
        }

        IList<Article> IResourcesQuery.GetRelatedArticles(Guid subcategoryId, Guid userId, int count)
        {
            return _repository.GetRelatedArticles(subcategoryId, userId, count);
        }

        IList<Video> IResourcesQuery.GetRelatedVideos(Guid subcategoryId, Guid userId, int count)
        {
            return _repository.GetRelatedVideos(subcategoryId, userId, count);
        }

        IList<QnA> IResourcesQuery.GetRelatedQnAs(Guid subcategoryId, Guid userId, int count)
        {
            return _repository.GetRelatedQnAs(subcategoryId, userId, count);
        }

        IList<FeaturedResource> IResourcesQuery.GetFeaturedArticles()
        {
            return _repository.GetFeaturedArticles();
        }

        IList<FeaturedResource> IResourcesQuery.GetFeaturedVideos()
        {
            return _repository.GetFeaturedVideos();
        }

        IList<FeaturedResource> IResourcesQuery.GetFeaturedQnAs()
        {
            return _repository.GetFeaturedQnAs();
        }

        int IResourcesQuery.GetViewingCount(Guid resourceId)
        {
            return _repository.GetViewingCount(resourceId);
        }

        IDictionary<Guid, int> IResourcesQuery.GetViewingCounts(IEnumerable<Guid> resourceIds)
        {
            return _repository.GetViewingCounts(resourceIds);
        }

        IList<Article> IResourcesQuery.GetRecentlyViewedArticles(Guid userId, int count)
        {
            return _repository.GetRecentlyViewedArticles(userId, count);
        }

        IList<Video> IResourcesQuery.GetRecentlyViewedVideos(Guid userId, int count)
        {
            return _repository.GetRecentlyViewedVideos(userId, count);
        }

        IList<QnA> IResourcesQuery.GetRecentlyViewedQnAs(Guid userId, int count)
        {
            return _repository.GetRecentlyViewedQnAs(userId, count);
        }

        IDictionary<Guid, DateTime> IResourcesQuery.GetLastViewedTimes(IEnumerable<Guid> resourceIds)
        {
            return _repository.GetLastViewedTimes(resourceIds);
        }

        Article IResourcesQuery.GetTopViewedArticle()
        {
            return _repository.GetTopViewedArticle();
        }

        Video IResourcesQuery.GetTopViewedVideo()
        {
            return _repository.GetTopViewedVideo();
        }

        QnA IResourcesQuery.GetTopViewedQnA()
        {
            return _repository.GetTopViewedQnA();
        }

        IDictionary<Guid, ResourceRatingSummary> IResourcesQuery.GetRatingSummaries(Guid userId, IEnumerable<Guid> resourceIds)
        {
            return _repository.GetRatingSummaries(userId, resourceIds);
        }

        Article IResourcesQuery.GetTopRatedArticle()
        {
            return _repository.GetTopRatedArticle();
        }

        Video IResourcesQuery.GetTopRatedVideo()
        {
            return _repository.GetTopRatedVideo();
        }

        QnA IResourcesQuery.GetTopRatedQnA()
        {
            return _repository.GetTopRatedQnA();
        }
    }
}
