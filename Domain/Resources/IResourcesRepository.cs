using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Resources
{
    public interface IResourcesRepository
    {
        Category GetResourceCategory(Guid id);
        IList<Category> GetResourceCategories();

        void CreateResourceViewing(ResourceViewing viewing);
        void UpdateResourceRating(ResourceRating rating);

        Article GetArticle(Guid id);
        IList<Article> GetArticles();
        IList<Article> GetArticles(IEnumerable<Guid> ids);

        Video GetVideo(Guid id);
        IList<Video> GetVideos();
        IList<Video> GetVideos(IEnumerable<Guid> ids);

        QnA GetQnA(Guid id);
        IList<QnA> GetQnAs();
        IList<QnA> GetQnAs(IEnumerable<Guid> ids);

        int GetViewingCount(Guid resourceId);
        IDictionary<Guid, int> GetViewingCounts(IEnumerable<Guid> resourceIds);

        IList<Article> GetRecentlyViewedArticles(Guid userId, int count);
        IList<Video> GetRecentlyViewedVideos(Guid userId, int count);
        IList<QnA> GetRecentlyViewedQnAs(Guid userId, int count);

        IDictionary<Guid, DateTime> GetLastViewedTimes(IEnumerable<Guid> resourceIds);

        Article GetTopViewedArticle();
        Video GetTopViewedVideo();
        QnA GetTopViewedQnA();

        Article GetTopRatedArticle();
        Video GetTopRatedVideo();
        QnA GetTopRatedQnA();
        IDictionary<Guid, ResourceRatingSummary> GetRatingSummaries(Guid userId, IEnumerable<Guid> resourceIds);

        IList<Article> GetRelatedArticles(Guid subcategoryId, Guid userId, int count);
        IList<Video> GetRelatedVideos(Guid subcategoryId, Guid userId, int count);
        IList<QnA> GetRelatedQnAs(Guid subcategoryId, Guid userId, int count);

        IList<FeaturedResource> GetFeaturedArticles();
        IList<FeaturedResource> GetFeaturedVideos();
        IList<FeaturedResource> GetFeaturedQnAs();

        IList<Category> GetFaqCategories();
        Category GetFaqCategory(UserType userType);
        Subcategory GetFaqSubcategory(Guid id);

        void MarkHelpful(Guid faqId);
        void MarkNotHelpful(Guid faqId);
        Faq GetFaq(Guid id);
        IList<Faq> GetFaqs();
        IList<Faq> GetFaqs(IEnumerable<Guid> faqIds);
        IList<Faq> GetHelpfulFaqs(Guid categoryId, int count);

        void CreateQuestion(Question question);

        void CreatePoll(Poll poll);
        void CreatePollAnswerVote(PollAnswerVote vote);
        Poll GetPoll(string name);
        Poll GetActivePoll();
        IDictionary<Guid, int> GetPollAnswerVotes(Guid answerId);
    }
}
