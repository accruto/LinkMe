using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace LinkMe.Domain.Resources.Data
{
    internal interface IResourceEntity
    {
        Guid id { get; set; }
        Guid resourceSubcategoryId { get; set; }
        string title { get; set; }
        string text { get; set; }
        DateTime createdTime { get; set; }
        string shortUrl { get; set; }
    }

    internal partial class ResourceArticleEntity
        : IResourceEntity
    {
    }

    internal partial class ResourceVideoEntity
        : IResourceEntity
    {
        string IResourceEntity.text
        {
            get { return transcript; }
            set { transcript = value; }
        }
    }

    internal partial class ResourceAnsweredQuestionEntity
        : IResourceEntity
    {
    }

    internal partial class FrequentlyAskedQuestionEntity
        : IResourceEntity
    {
        public Guid resourceSubcategoryId
        {
            get { return faqSubcategoryId; }
            set { faqSubcategoryId = value; }
        }

        public string shortUrl { get; set; }
    }

    internal static class Mappings
    {
        #region Subcategory

        internal static Subcategory Map(this ResourceSubcategoryEntity entity)
        {
            return new Subcategory
            {
                Id = entity.id,
                Name = entity.displayname,
            };
        }

        internal static Subcategory Map(this FAQSubcategoryEntity entity)
        {
            return new Subcategory
            {
                Id = entity.id,
                Name = entity.displayName,
            };
        }

        #endregion

        #region Category

        internal static Category Map(this FAQCategoryEntity entity)
        {
            return new Category
            {
                Id = entity.id,
                Name = entity.displayName,
                DisplayOrder = entity.displayOrder,
                Subcategories = (from s in entity.FAQSubcategoryEntities orderby s.displayOrder select s.Map()).ToList(),
            };
        }

        internal static Category Map(this ResourceCategoryEntity entity)
        {
            return new Category
            {
                Id = entity.id,
                Name = entity.displayname,
                DisplayOrder = entity.displayorder,
                Subcategories = (from s in entity.ResourceSubcategoryEntities select s.Map()).ToList(),
            };
        }

        #endregion

        #region Article

        internal static Article Map(this ResourceArticleEntity entity)
        {
            return Map<Article>(entity);
        }

        internal static FeaturedResource Map(this ResourceFeaturedArticleEntity entity)
        {
            return new FeaturedResource
            {
                Id = entity.id,
                CssClass = entity.cssClass,
                FeaturedResourceType = (FeaturedResourceType) entity.featuredType,
                ResourceId = entity.resourceArticleId,
            };
        }

        #endregion

        #region Video

        internal static Video Map(this ResourceVideoEntity entity)
        {
            var video = Map<Video>(entity);
            video.ExternalVideoId = entity.externalVideoId;
            return video;
        }

        #endregion

        #region QnA

        internal static QnA Map(this ResourceAnsweredQuestionEntity entity)
        {
            return Map<QnA>(entity);
        }

        #endregion

        #region Question

        internal static Question Map(this ResourceQuestionEntity entity)
        {
            return new Question
            {
                Id = entity.id,
                CategoryId = entity.resourceCategoryId,
                Text = entity.text,
                CreatedTime = entity.createdTime,
                AskerId = entity.askerId,
            };
        }

        internal static ResourceQuestionEntity Map(this Question question)
        {
            return new ResourceQuestionEntity
            {
                id = question.Id,
                resourceCategoryId = question.CategoryId,
                text = question.Text,
                createdTime = question.CreatedTime,
                askerId = question.AskerId,
            };
        }

        #endregion

        #region ResourceViewing

        internal static ResourceViewing Map(this ResourceViewingEntity entity)
        {
            return new ResourceViewing
            {
                Id = entity.id,
                UserId = entity.userId,
                ResourceId = entity.resourceId,
                ResourceType = (ResourceType)entity.resourceType,
                Time = entity.time
            };
        }

        internal static ResourceViewingEntity Map(this ResourceViewing viewing)
        {
            return new ResourceViewingEntity
            {
                id = viewing.Id,
                userId = viewing.UserId,
                resourceId = viewing.ResourceId,
                resourceType = (byte) viewing.ResourceType,
                time = viewing.Time,
            };
        }

        internal static ResourceRating Map(this ResourceArticleUserRatingEntity entity)
        {
            return new ResourceRating
            {
                Id = entity.id,
                ResourceId = entity.resourceArticleId,
                UserId = entity.userId,
                Rating = entity.rating,
                LastUpdatedTime = entity.lastUpdatedTime,
                ResourceType = (ResourceType) entity.resourceType,
            };
        }

        internal static ResourceArticleUserRatingEntity Map(this ResourceRating rating)
        {
            var entity = new ResourceArticleUserRatingEntity
            {
                id = rating.Id,
                resourceArticleId = rating.ResourceId,
                userId = rating.UserId,
                resourceType = (byte) rating.ResourceType,
            };
            rating.MapTo(entity);
            return entity;
        }

        internal static void MapTo(this ResourceRating rating, ResourceArticleUserRatingEntity entity)
        {
            entity.rating = rating.Rating;
            entity.lastUpdatedTime = rating.LastUpdatedTime;
        }

        #endregion

        #region Polls

        internal static Poll Map(this ResourcePollEntity entity)
        {
            return new Poll
            {
                Id = entity.id,
                Name = entity.name,
                Question = entity.question,
                IsActive = entity.active,
                Answers = (from e in entity.ResourcePollAnswerEntities orderby e.answerNumber select e.Map()).ToList(),
            };
        }

        private static PollAnswer Map(this ResourcePollAnswerEntity entity)
        {
            return new PollAnswer
            {
                Id = entity.id,
                Order = entity.answerNumber,
                Text = entity.text,
            };
        }

        internal static ResourcePollEntity Map(this Poll poll)
        {
            return new ResourcePollEntity
            {
                id = poll.Id,
                name = poll.Name,
                question = poll.Question,
                active = poll.IsActive,
                ResourcePollAnswerEntities = poll.Answers.Map(),
            };
        }

        private static EntitySet<ResourcePollAnswerEntity> Map(this IEnumerable<PollAnswer> answers)
        {
            var set = new EntitySet<ResourcePollAnswerEntity>();
            set.AddRange(from a in answers select a.Map());
            return set;
        }

        private static ResourcePollAnswerEntity Map(this PollAnswer answer)
        {
            return new ResourcePollAnswerEntity
            {
                id = answer.Id,
                answerNumber = (byte) answer.Order,
                text = answer.Text,
            };
        }

        internal static PollAnswerVote Map(this ResourcePollAnswerVoteEntity entity)
        {
            return new PollAnswerVote
            {
                Id = entity.id,
                CreatedTime = entity.createdTime,
                AnswerId = entity.resourcePollAnswerId,
                UserId = entity.userid,
            };
        }

        internal static ResourcePollAnswerVoteEntity Map(this PollAnswerVote pollAnswerVote)
        {
            return new ResourcePollAnswerVoteEntity
            {
                id = pollAnswerVote.Id,
                resourcePollAnswerId = pollAnswerVote.AnswerId,
                userid = pollAnswerVote.UserId,
                createdTime = pollAnswerVote.CreatedTime,
            };
        }

        #endregion

        #region FAQ

        internal static Faq Map(this FrequentlyAskedQuestionEntity entity)
        {
            var faq = Map<Faq>(entity);
            faq.HelpfulNo = entity.helpfulNo;
            faq.HelpfulYes = entity.helpfulYes;
            faq.Keywords = entity.keywords;
            return faq;
        }

        internal static FrequentlyAskedQuestionEntity Map(this Faq faq)
        {
            var entity = new FrequentlyAskedQuestionEntity { id = faq.Id };
            faq.MapTo(entity);
            return entity;
        }

        internal static void MapTo(this Faq faq, FrequentlyAskedQuestionEntity entity)
        {
            entity.faqSubcategoryId = faq.SubcategoryId;
            entity.title = faq.Title;
            entity.text = faq.Text;
            entity.createdTime = faq.CreatedTime;
            entity.helpfulYes = faq.HelpfulYes;
            entity.helpfulNo = faq.HelpfulNo;
            entity.keywords = faq.Keywords;
        }

        #endregion

        private static TResource Map<TResource>(this IResourceEntity entity)
            where TResource : Resource, new()
        {
            return new TResource
            {
                Id = entity.id,
                SubcategoryId = entity.resourceSubcategoryId,
                Title = entity.title,
                Text = entity.text,
                CreatedTime = entity.createdTime,
                ShortUrl = entity.shortUrl,
            };
        }
    }
}
