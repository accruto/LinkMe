using System;

namespace LinkMe.Domain.Resources.Commands
{
    public interface IResourcesCommand
    {
        void RateArticle(Guid userId, Guid articleId, byte rating);
        void RateVideo(Guid userId, Guid videoId, byte rating);
        void RateQnA(Guid userId, Guid qnaId, byte rating);

        void ViewArticle(Guid userId, Guid articleId);
        void ViewVideo(Guid userId, Guid videoId);
        void ViewQnA(Guid userId, Guid qnaId);

        void CreateQuestion(Question question);
    }
}
