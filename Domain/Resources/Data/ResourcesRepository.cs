using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Domain.Resources.Data
{
    public class ResourcesRepository
        : Repository, IResourcesRepository
    {
        private static readonly DataLoadOptions ResourceCategoryLoadOptions = DataOptions.CreateLoadOptions<ResourceCategoryEntity>(c => c.ResourceSubcategoryEntities);
        private static readonly DataLoadOptions FaqCategoryLoadOptions = DataOptions.CreateLoadOptions<FAQCategoryEntity>(c => c.FAQSubcategoryEntities);
        private static readonly DataLoadOptions PollLoadOptions = DataOptions.CreateLoadOptions<ResourcePollEntity>(c => c.ResourcePollAnswerEntities);

        // Resource categories.

        private static readonly Func<ResourcesDataContext, IQueryable<Category>> GetResourceCategoriesQuery
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => from c in dc.ResourceCategoryEntities
                   select c.Map());

        private static readonly Func<ResourcesDataContext, Guid, Category> GetResourceCategoryQuery
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid id)
                => (from c in dc.ResourceCategoryEntities
                    where c.id == id
                    select c.Map()).SingleOrDefault());

        private static readonly Func<ResourcesDataContext, Guid, Article> GetArticle
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid id)
                => (from c in dc.ResourceArticleEntities
                    where c.id == id
                    select c.Map()).SingleOrDefault());

        private static readonly Func<ResourcesDataContext, IQueryable<Article>> GetArticles
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => from c in dc.ResourceArticleEntities
                   select c.Map());

        private static readonly Func<ResourcesDataContext, string, IQueryable<Article>> GetFilteredArticles
            = CompiledQuery.Compile((ResourcesDataContext dc, string ids)
                => from c in dc.ResourceArticleEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on c.id equals i.value
                   select c.Map());

        private static readonly Func<ResourcesDataContext, IQueryable<FeaturedResource>> GetFeaturedArticles
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => from c in dc.ResourceFeaturedArticleEntities
                   join a in dc.ResourceArticleEntities on c.resourceArticleId equals a.id
                   select c.Map());

        private static readonly Func<ResourcesDataContext, IQueryable<FeaturedResource>> GetFeaturedVideos
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => from c in dc.ResourceFeaturedArticleEntities
                   join a in dc.ResourceVideoEntities on c.resourceArticleId equals a.id
                   select c.Map());

        private static readonly Func<ResourcesDataContext, IQueryable<FeaturedResource>> GetFeaturedQnAs
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => from c in dc.ResourceFeaturedArticleEntities
                   join a in dc.ResourceAnsweredQuestionEntities on c.resourceArticleId equals a.id
                   select c.Map());

        private static readonly Func<ResourcesDataContext, Guid, Video> GetVideo
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid id)
                => (from c in dc.ResourceVideoEntities
                    where c.id == id
                    select c.Map()).SingleOrDefault());

        private static readonly Func<ResourcesDataContext, IQueryable<Video>> GetVideos
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => from c in dc.ResourceVideoEntities
                   select c.Map());

        private static readonly Func<ResourcesDataContext, string, IQueryable<Video>> GetFilteredVideos
            = CompiledQuery.Compile((ResourcesDataContext dc, string ids)
                => from c in dc.ResourceVideoEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on c.id equals i.value
                   select c.Map());

        private static readonly Func<ResourcesDataContext, Guid, QnA> GetQnA
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid id)
                => (from c in dc.ResourceAnsweredQuestionEntities
                    where c.id == id
                    select c.Map()).SingleOrDefault());

        private static readonly Func<ResourcesDataContext, IQueryable<QnA>> GetQnAs
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => from c in dc.ResourceAnsweredQuestionEntities
                   select c.Map());

        private static readonly Func<ResourcesDataContext, string, IQueryable<QnA>> GetFilteredQnAs
            = CompiledQuery.Compile((ResourcesDataContext dc, string ids)
                => from c in dc.ResourceAnsweredQuestionEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on c.id equals i.value
                   select c.Map());

        private static readonly Func<ResourcesDataContext, Guid, Guid, ResourceArticleUserRatingEntity> GetResourceArticleUserRatingEntity
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid articleId, Guid userId)
                => (from l in dc.ResourceArticleUserRatingEntities
                    where l.resourceArticleId == articleId
                    && l.userId == userId
                    select l).SingleOrDefault());

        private static readonly Func<ResourcesDataContext, Guid, string, IQueryable<Tuple<Guid, ResourceRatingSummary>>> GetRatingSummaries
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid userId, string resourceIds)
                => from r in dc.ResourceArticleUserRatingEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, resourceIds) on r.resourceArticleId equals i.value
                   group r by r.resourceArticleId into g
                   select Tuple.Create(
                        g.Key,
                        new ResourceRatingSummary
                        {
                            RatingCount = g.Count(),
                            AverageRating = ((double)g.Sum(x => x.rating)) / g.Count(),
                            UserRating = (from x in g where x.userId == userId select (byte?)x.rating).SingleOrDefault()
                        }));

        private static readonly Func<ResourcesDataContext, Article> GetTopRatedArticle
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => (from a in dc.ResourceArticleEntities
                    where a.id == (from r in dc.ResourceArticleUserRatingEntities
                                   where r.resourceType == (byte) ResourceType.Article
                                   group r by r.resourceArticleId into grp
                                   select new { resourceArticleId = grp.Key, averageRating = grp.Average(ar => ar.rating) } into avg
                                   orderby avg.averageRating descending
                                   select avg.resourceArticleId).First()
                    select a.Map()).SingleOrDefault());

        private static readonly Func<ResourcesDataContext, Video> GetTopRatedVideo
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => (from v in dc.ResourceVideoEntities
                    where v.id == (from r in dc.ResourceArticleUserRatingEntities
                                   where r.resourceType == (byte)ResourceType.Video
                                   group r by r.resourceArticleId into grp
                                   select new { resourceArticleId = grp.Key, averageRating = grp.Average(ar => ar.rating) } into avg
                                   orderby avg.averageRating descending
                                   select avg.resourceArticleId).First()
                    select v.Map()).SingleOrDefault());

        private static readonly Func<ResourcesDataContext, QnA> GetTopRatedQnA
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => (from a in dc.ResourceAnsweredQuestionEntities
                    where a.id == (from r in dc.ResourceArticleUserRatingEntities
                                   where r.resourceType == (byte)ResourceType.QnA
                                   group r by r.resourceArticleId into grp
                                   select new { resourceArticleId = grp.Key, averageRating = grp.Average(ar => ar.rating) } into avg
                                   orderby avg.averageRating descending
                                   select avg.resourceArticleId).First()
                    select a.Map()).SingleOrDefault());

        private static readonly Func<ResourcesDataContext, Guid, int, IQueryable<Article>> GetRecentlyViewedArticlesQuery
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid userId, int count)
                => (from x in
                        (
                            from a in dc.ResourceViewingEntities
                            where a.resourceType == (byte)ResourceType.Article
                            && a.userId == userId
                            group a by a.resourceId into g
                            select new { g.Key, Time = g.Max(a => a.time) }
                        )
                    orderby x.Time descending
                    select (from a in dc.ResourceArticleEntities where a.id == x.Key select a.Map()).Single()).Take(count));

        private static readonly Func<ResourcesDataContext, Guid, int, IQueryable<Video>> GetRecentlyViewedVideosQuery
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid userId, int count)
                => (from x in
                        (
                            from a in dc.ResourceViewingEntities
                            where a.resourceType == (byte)ResourceType.Video
                            && a.userId == userId
                            group a by a.resourceId into g
                            select new { g.Key, Time = g.Max(a => a.time) }
                        )
                    orderby x.Time descending
                    select (from v in dc.ResourceVideoEntities where v.id == x.Key select v.Map()).Single()).Take(count));

        private static readonly Func<ResourcesDataContext, Guid, int, IQueryable<QnA>> GetRecentlyViewedQnAsQuery
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid userId, int count)
                => (from x in
                        (
                            from a in dc.ResourceViewingEntities
                            where a.resourceType == (byte)ResourceType.QnA
                            && a.userId == userId
                            group a by a.resourceId into g
                            select new { g.Key, Time = g.Max(a => a.time) }
                        )
                    orderby x.Time descending
                    select (from a in dc.ResourceAnsweredQuestionEntities where a.id == x.Key select a.Map()).Single()).Take(count));

        private static readonly Func<ResourcesDataContext, string, IQueryable<Tuple<Guid, DateTime>>> GetLastViewedTimes
            = CompiledQuery.Compile((ResourcesDataContext dc, string resourceIds)
                => (from x in
                        (
                            from a in dc.ResourceViewingEntities
                            join i in dc.SplitGuids(SplitList<Guid>.Delimiter, resourceIds) on a.resourceId equals i.value
                            group a by a.resourceId into g
                            select new { g.Key, Time = g.Max(a => a.time) }
                        )
                    select Tuple.Create(x.Key, x.Time)));

        private static readonly Func<ResourcesDataContext, Article> GetTopViewedArticle
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => (from a in dc.ResourceArticleEntities
                    where a.id == (from v in dc.ResourceViewingEntities
                                   where v.resourceType == (byte)ResourceType.Article
                                   group v by v.resourceId into g
                                   select new { Id = g.Key, Count = g.Count() } into vc
                                   orderby vc.Count descending
                                   select vc.Id).First()
                    select a.Map()).SingleOrDefault());

        private static readonly Func<ResourcesDataContext, Video> GetTopViewedVideo
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => (from a in dc.ResourceVideoEntities
                    where a.id == (from v in dc.ResourceViewingEntities
                                   where v.resourceType == (byte)ResourceType.Video
                                   group v by v.resourceId into g
                                   select new { Id = g.Key, Count = g.Count() } into vc
                                   orderby vc.Count descending
                                   select vc.Id).First()
                    select a.Map()).SingleOrDefault());

        private static readonly Func<ResourcesDataContext, QnA> GetTopViewedQnA
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => (from a in dc.ResourceAnsweredQuestionEntities
                    where a.id == (from v in dc.ResourceViewingEntities
                                   where v.resourceType == (byte)ResourceType.QnA
                                   group v by v.resourceId into g
                                   select new { Id = g.Key, Count = g.Count() } into vc
                                   orderby vc.Count descending
                                   select vc.Id).First()
                    select a.Map()).SingleOrDefault());

        private static readonly Func<ResourcesDataContext, Guid, int> GetViewingCount
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid resourceId)
                => (from v in dc.ResourceViewingEntities
                    where v.resourceId == resourceId
                    select v).Count());

        private static readonly Func<ResourcesDataContext, string, IQueryable<Tuple<Guid, int>>> GetViewingCounts
            = CompiledQuery.Compile((ResourcesDataContext dc, string resourceIds)
                => from v in dc.ResourceViewingEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, resourceIds) on v.resourceId equals i.value
                   group v by v.resourceId into g
                   select Tuple.Create(g.Key, g.Count()));

        private static readonly Func<ResourcesDataContext, Guid, Guid, int, IQueryable<Article>> GetRelatedArticles
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid subcategoryId, Guid userId, int count)
                => (from c in dc.ResourceArticleEntities
                    where c.resourceSubcategoryId == subcategoryId
                    orderby
                        dc.ResourceViewingEntities.Any(v => v.resourceId == c.id && v.userId == userId && v.resourceType == (byte)ResourceType.Article),
                        dc.Random()
                    select c.Map()).Take(count));

        private static readonly Func<ResourcesDataContext, Guid, Guid, int, IQueryable<Video>> GetRelatedVideos
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid subcategoryId, Guid userId, int count)
                => (from c in dc.ResourceVideoEntities
                    where c.resourceSubcategoryId == subcategoryId
                    orderby
                        dc.ResourceViewingEntities.Any(v => v.resourceId == c.id && v.userId == userId && v.resourceType == (byte)ResourceType.Video),
                        dc.Random()
                    select c.Map()).Take(count));

        private static readonly Func<ResourcesDataContext, Guid, Guid, int, IQueryable<QnA>> GetRelatedQnAs
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid subcategoryId, Guid userId, int count)
                => (from q in dc.ResourceAnsweredQuestionEntities
                    where q.resourceSubcategoryId == subcategoryId
                    orderby
                        dc.ResourceViewingEntities.Any(v => v.resourceId == q.id && v.userId == userId && v.resourceType == (byte)ResourceType.QnA),
                        dc.Random()
                    select q.Map()).Take(count));

        private static readonly Func<ResourcesDataContext, string, Poll> GetPollQuery
            = CompiledQuery.Compile((ResourcesDataContext dc, string name)
                => (from p in dc.ResourcePollEntities
                    where p.name == name
                    select p.Map()).SingleOrDefault());

        private static readonly Func<ResourcesDataContext, Poll> GetActivePollQuery
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => (from p in dc.ResourcePollEntities
                    where p.active
                    select p.Map()).SingleOrDefault());

        private static readonly Func<ResourcesDataContext, Guid, Guid, bool> HasVoted
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid userId, Guid answerId)
                => (from p in dc.ResourcePollEntities
                    join a in dc.ResourcePollAnswerEntities on p.id equals a.pollId
                    join v in dc.ResourcePollAnswerVoteEntities on a.id equals v.resourcePollAnswerId
                    where v.userid == userId
                    && p.id == (from o in dc.ResourcePollAnswerEntities where o.id == answerId select a.pollId).Single()
                    select v).Any());

        private static readonly Func<ResourcesDataContext, Guid, IQueryable<Tuple<Guid, int>>> GetPollAnswerVotes
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid pollId)
                => from a in dc.ResourcePollAnswerEntities
                   where a.pollId == pollId
                   select Tuple.Create(a.id, (from v in dc.ResourcePollAnswerVoteEntities where v.resourcePollAnswerId == a.id select v).Count()));

        // Faq

        private static readonly Func<ResourcesDataContext, IQueryable<Category>> GetFaqCategoriesQuery
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => from c in dc.FAQCategoryEntities
                   select c.Map());

        private static readonly Func<ResourcesDataContext, UserType, Category> GetFaqCategoryQuery
            = CompiledQuery.Compile((ResourcesDataContext dc, UserType userType)
                => (from c in dc.FAQCategoryEntities
                    where c.userType == (byte)userType
                    select c.Map()).SingleOrDefault());

        private static readonly Func<ResourcesDataContext, Guid, Subcategory> GetFaqSubcategory
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid id)
                => (from s in dc.FAQSubcategoryEntities
                    where s.id == id
                    select s.Map()).SingleOrDefault());

        private static readonly Func<ResourcesDataContext, Guid, FrequentlyAskedQuestionEntity> GetFaqEntity
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid id)
                => (from f in dc.FrequentlyAskedQuestionEntities
                    where f.id == id
                    select f).SingleOrDefault());

        private static readonly Func<ResourcesDataContext, Guid, Faq> GetFaq
            = CompiledQuery.Compile((ResourcesDataContext dc, Guid id)
                => (from f in dc.FrequentlyAskedQuestionEntities
                    where f.id == id
                    select f.Map()).SingleOrDefault());

        private static readonly Func<ResourcesDataContext, IQueryable<Faq>> GetFaqs
           = CompiledQuery.Compile((ResourcesDataContext dc)
               => from f in dc.FrequentlyAskedQuestionEntities
                  select f.Map());

        private static readonly Func<ResourcesDataContext, string, IQueryable<Faq>> GetFilteredFaqs
           = CompiledQuery.Compile((ResourcesDataContext dc, string ids)
               => from c in dc.FrequentlyAskedQuestionEntities
                  join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on c.id equals i.value
                  select c.Map());

        private static readonly Func<ResourcesDataContext, Guid, int, IQueryable<Faq>> GetHelpfulFaqs
           = CompiledQuery.Compile((ResourcesDataContext dc, Guid categoryId, int count)
               => (from c in dc.FrequentlyAskedQuestionEntities
                   join s in dc.FAQSubcategoryEntities on c.faqSubcategoryId equals s.id
                   where s.faqCategoryId == categoryId
                   orderby (c.helpfulYes - c.helpfulNo) descending
                   select c.Map()).Take(count));

        public ResourcesRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        IList<Category> IResourcesRepository.GetResourceCategories()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetResourceCategories(dc).ToList();
            }
        }

        Category IResourcesRepository.GetResourceCategory(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetResourceCategory(dc, id);
            }
        }

        Article IResourcesRepository.GetArticle(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetArticle(dc, id);
            }
        }

        IList<Article> IResourcesRepository.GetArticles()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetArticles(dc).ToList();
            }
        }

        Video IResourcesRepository.GetVideo(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetVideo(dc, id);
            }
        }

        IList<Video> IResourcesRepository.GetVideos()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetVideos(dc).ToList();
            }
        }

        QnA IResourcesRepository.GetQnA(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetQnA(dc, id);
            }
        }

        IList<QnA> IResourcesRepository.GetQnAs()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetQnAs(dc).ToList();
            }
        }

        IList<Article> IResourcesRepository.GetArticles(IEnumerable<Guid> articleIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFilteredArticles(dc, new SplitList<Guid>(articleIds).ToString()).ToList();
            }
        }

        IList<Video> IResourcesRepository.GetVideos(IEnumerable<Guid> videoIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFilteredVideos(dc, new SplitList<Guid>(videoIds).ToString()).ToList();
            }
        }

        IList<Video> IResourcesRepository.GetRecentlyViewedVideos(Guid userId, int count)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRecentlyViewedVideosQuery(dc, userId, count).ToList();
            }
        }

        IList<QnA> IResourcesRepository.GetQnAs(IEnumerable<Guid> ids)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFilteredQnAs(dc, new SplitList<Guid>(ids).ToString()).ToList();
            }
        }

        IList<QnA> IResourcesRepository.GetRecentlyViewedQnAs(Guid userId, int count)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRecentlyViewedQnAsQuery(dc, userId, count).ToList();
            }
        }

        IDictionary<Guid, DateTime> IResourcesRepository.GetLastViewedTimes(IEnumerable<Guid> resourceIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetLastViewedTimes(dc, new SplitList<Guid>(resourceIds).ToString()).ToDictionary(x => x.Item1, x => x.Item2);
            }
        }

        IList<FeaturedResource> IResourcesRepository.GetFeaturedArticles()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFeaturedArticles(dc).ToList();
            }
        }

        IList<FeaturedResource> IResourcesRepository.GetFeaturedVideos()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFeaturedVideos(dc).ToList();
            }
        }

        IList<FeaturedResource> IResourcesRepository.GetFeaturedQnAs()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFeaturedQnAs(dc).ToList();
            }
        }

        IList<Article> IResourcesRepository.GetRelatedArticles(Guid subcategoryId, Guid userId, int count)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRelatedArticles(dc, subcategoryId, userId, count).ToList();
            }
        }

        IList<Video> IResourcesRepository.GetRelatedVideos(Guid subcategoryId, Guid userId, int count)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRelatedVideos(dc, subcategoryId, userId, count).ToList();
            }
        }

        IList<QnA> IResourcesRepository.GetRelatedQnAs(Guid subcategoryId, Guid userId, int count)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRelatedQnAs(dc, subcategoryId, userId, count).ToList();
            }
        }

        Article IResourcesRepository.GetTopRatedArticle()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetTopRatedArticle(dc);
            }
        }

        Video IResourcesRepository.GetTopRatedVideo()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetTopRatedVideo(dc);
            }
        }

        QnA IResourcesRepository.GetTopRatedQnA()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetTopRatedQnA(dc);
            }
        }

        int IResourcesRepository.GetViewingCount(Guid resourceId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetViewingCount(dc, resourceId);
            }
        }

        IDictionary<Guid, int> IResourcesRepository.GetViewingCounts(IEnumerable<Guid> resourceIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var ids = resourceIds.ToArray();
                var counts = GetViewingCounts(dc, new SplitList<Guid>(ids).ToString()).ToDictionary(x => x.Item1, x => x.Item2);
                return (from i in ids
                        select new { Id = i, Count = counts.ContainsKey(i) ? counts[i] : 0}).ToDictionary(x => x.Id, x => x.Count);
            }
        }

        Article IResourcesRepository.GetTopViewedArticle()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetTopViewedArticle(dc);
            }
        }

        Video IResourcesRepository.GetTopViewedVideo()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetTopViewedVideo(dc);
            }
        }

        QnA IResourcesRepository.GetTopViewedQnA()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetTopViewedQnA(dc);
            }
        }

        IDictionary<Guid, ResourceRatingSummary> IResourcesRepository.GetRatingSummaries(Guid userId, IEnumerable<Guid> resourceIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var ids = resourceIds.ToArray();
                var ratings = GetRatingSummaries(dc, userId, new SplitList<Guid>(ids).ToString()).ToDictionary(x => x.Item1, x => x.Item2);
                return (from i in ids
                        select new
                        {
                            Id = i,
                            Rating = ratings.ContainsKey(i) ? ratings[i] : new ResourceRatingSummary { RatingCount = 0, AverageRating = 0, UserRating = 0 },
                        }).ToDictionary(x => x.Id, x => x.Rating);
            }
        }

        IList<Article> IResourcesRepository.GetRecentlyViewedArticles(Guid userId, int count)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRecentlyViewedArticlesQuery(dc, userId, count).ToList();
            }
        }

        void IResourcesRepository.UpdateResourceRating(ResourceRating rating)
        {
            using (var dc = CreateContext())
            {
                var entity = GetResourceArticleUserRatingEntity(dc, rating.ResourceId, rating.UserId);
                if (entity == null)
                    dc.ResourceArticleUserRatingEntities.InsertOnSubmit(rating.Map());
                else
                    rating.MapTo(entity);

                dc.SubmitChanges();
            }
        }

        void IResourcesRepository.CreateResourceViewing(ResourceViewing resourceViewing)
        {
            using (var dc = CreateContext())
            {
                dc.ResourceViewingEntities.InsertOnSubmit(resourceViewing.Map());
                dc.SubmitChanges();
            }
        }

        void IResourcesRepository.CreateQuestion(Question question)
        {
            using (var dc = CreateContext())
            {
                dc.ResourceQuestionEntities.InsertOnSubmit(question.Map());
                dc.SubmitChanges();
            }
        }

        IList<Category> IResourcesRepository.GetFaqCategories()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFaqCategories(dc).ToList();
            }
        }

        Category IResourcesRepository.GetFaqCategory(UserType userType)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFaqCategory(dc, userType);
            }
        }

        Subcategory IResourcesRepository.GetFaqSubcategory(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFaqSubcategory(dc, id);
            }
        }

        Faq IResourcesRepository.GetFaq(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFaq(dc, id);
            }
        }

        IList<Faq> IResourcesRepository.GetFaqs()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFaqs(dc).ToList();
            }
        }

        IList<Faq> IResourcesRepository.GetFaqs(IEnumerable<Guid> ids)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFilteredFaqs(dc, new SplitList<Guid>(ids).ToString()).ToList();
            }
        }

        IList<Faq> IResourcesRepository.GetHelpfulFaqs(Guid categoryId, int count)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetHelpfulFaqs(dc, categoryId, count).ToList();
            }
        }

        void IResourcesRepository.MarkHelpful(Guid faqId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetFaqEntity(dc, faqId);
                if (entity != null)
                {
                    entity.helpfulYes += 1;
                    dc.SubmitChanges();
                }
            }
        }

        void IResourcesRepository.MarkNotHelpful(Guid faqId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetFaqEntity(dc, faqId);
                if (entity != null)
                {
                    entity.helpfulNo += 1;
                    dc.SubmitChanges();
                }
            }
        }

        void IResourcesRepository.CreatePoll(Poll poll)
        {
            using (var dc = CreateContext())
            {
                dc.ResourcePollEntities.InsertOnSubmit(poll.Map());
                dc.SubmitChanges();
            }
        }

        Poll IResourcesRepository.GetPoll(string name)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetPoll(dc, name);
            }
        }

        Poll IResourcesRepository.GetActivePoll()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetActivePoll(dc);
            }
        }

        void IResourcesRepository.CreatePollAnswerVote(PollAnswerVote vote)
        {
            using (var dc = CreateContext())
            {
                // Don't add if the user has already voted in this poll.

                if (!HasVoted(dc, vote.UserId, vote.AnswerId))
                {
                    dc.ResourcePollAnswerVoteEntities.InsertOnSubmit(vote.Map());
                    dc.SubmitChanges();
                }
            }
        }

        IDictionary<Guid, int> IResourcesRepository.GetPollAnswerVotes(Guid pollId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetPollAnswerVotes(dc, pollId).ToDictionary(x => x.Item1, x => x.Item2);
            }
        }

        private static IEnumerable<Category> GetResourceCategories(ResourcesDataContext dc)
        {
            dc.LoadOptions = ResourceCategoryLoadOptions;
            return GetResourceCategoriesQuery(dc);
        }

        private static Category GetResourceCategory(ResourcesDataContext dc, Guid id)
        {
            dc.LoadOptions = ResourceCategoryLoadOptions;
            return GetResourceCategoryQuery(dc, id);
        }

        private static IEnumerable<Category> GetFaqCategories(ResourcesDataContext dc)
        {
            dc.LoadOptions = FaqCategoryLoadOptions;
            return GetFaqCategoriesQuery(dc);
        }

        private static Category GetFaqCategory(ResourcesDataContext dc, UserType userType)
        {
            dc.LoadOptions = FaqCategoryLoadOptions;
            return GetFaqCategoryQuery(dc, userType);
        }

        private static Poll GetPoll(ResourcesDataContext dc, string name)
        {
            dc.LoadOptions = PollLoadOptions;
            return GetPollQuery(dc, name);
        }

        private static Poll GetActivePoll(ResourcesDataContext dc)
        {
            dc.LoadOptions = PollLoadOptions;
            return GetActivePollQuery(dc);
        }

        private ResourcesDataContext CreateContext()
        {
            return CreateContext(c => new ResourcesDataContext(c));
        }
    }
}