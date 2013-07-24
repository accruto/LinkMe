using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.PublishedEvents;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Resources.Commands
{
    public class ResourcesCommand
        : IResourcesCommand
    {
        private readonly IResourcesRepository _repository;

        public ResourcesCommand(IResourcesRepository repository)
        {
            _repository = repository;
        }

        void IResourcesCommand.RateArticle(Guid userId, Guid articleId, byte rating)
        {
            RateResource(ResourceType.Article, userId, articleId, rating);
        }

        void IResourcesCommand.RateVideo(Guid userId, Guid videoId, byte rating)
        {
            RateResource(ResourceType.Video, userId, videoId, rating);
        }

        void IResourcesCommand.RateQnA(Guid userId, Guid qnaId, byte rating)
        {
            RateResource(ResourceType.QnA, userId, qnaId, rating);
        }

        void IResourcesCommand.ViewArticle(Guid userId, Guid articleId)
        {
            ViewResource(ResourceType.Article, userId, articleId);
        }

        void IResourcesCommand.ViewVideo(Guid userId, Guid videoId)
        {
            ViewResource(ResourceType.Video, userId, videoId);
        }

        void IResourcesCommand.ViewQnA(Guid userId, Guid qnaId)
        {
            ViewResource(ResourceType.QnA, userId, qnaId);
        }

        void IResourcesCommand.CreateQuestion(Question question)
        {
            question.Prepare();
            question.Validate();

            _repository.CreateQuestion(question);

            var handlers = QuestionCreated;
            if (handlers != null)
                handlers(this, new ResourceQuestionEventArgs(question));
        }

        private void RateResource(ResourceType resourceType, Guid userId, Guid resourceId, byte rating)
        {
            var resourceRating = new ResourceRating
            {
                ResourceId = resourceId,
                UserId = userId,
                Rating = rating,
                LastUpdatedTime = DateTime.Now,
                ResourceType = resourceType,
            };
            resourceRating.Prepare();
            resourceRating.Validate();
            _repository.UpdateResourceRating(resourceRating);
        }

        private void ViewResource(ResourceType resourceType, Guid userId, Guid resourceId)
        {
            var viewing = new ResourceViewing { ResourceType = resourceType, UserId = userId, ResourceId = resourceId };
            viewing.Prepare();
            viewing.Validate();
            _repository.CreateResourceViewing(viewing);
    
            var handlers = ResourceViewed;
            if (handlers != null)
                handlers(this, new ResourceEventArgs(viewing.ResourceId));
        }

        [Publishes(PublishedEvents.ResourceViewed)]
        public event EventHandler<ResourceEventArgs> ResourceViewed;

        [Publishes(PublishedEvents.QuestionCreated)]
        public event EventHandler<ResourceQuestionEventArgs> QuestionCreated;
    }
}
