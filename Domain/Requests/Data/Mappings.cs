using System;

namespace LinkMe.Domain.Requests.Data
{
    public interface IUserToUserRequestEntity
    {
        Guid id { get; set; }
        string messageText { get; set; }
        byte status { get; set; }
        DateTime? firstSentTime { get; set; }
        DateTime? lastSentTime { get; set; }
        DateTime? actionedTime { get; set; }
    }

    public static class Mappings
    {
        public static T MapTo<T>(this Request request)
            where T : IUserToUserRequestEntity, new()
        {
            var t = new T { id = request.Id };
            request.MapTo(t);
            return t;
        }

        public static void MapTo(this Request request, IUserToUserRequestEntity entity)
        {
            entity.messageText = request.Text;
            entity.status = (byte)request.Status;
            entity.firstSentTime = request.FirstSentTime;
            entity.lastSentTime = request.LastSentTime;
            entity.actionedTime = request.ActionedTime;
        }

        public static T MapTo<T>(this IUserToUserRequestEntity entity)
            where T : Request, new()
        {
            var request = new T();
            entity.MapTo(request);
            return request;
        }

        public static void MapTo(this IUserToUserRequestEntity entity, Request request)
        {
            request.Id = entity.id;
            request.Text = entity.messageText;
            request.Status = (RequestStatus) entity.status;
            request.FirstSentTime = entity.firstSentTime;
            request.LastSentTime = entity.lastSentTime;
            request.ActionedTime = entity.actionedTime;
        }
    }
}
