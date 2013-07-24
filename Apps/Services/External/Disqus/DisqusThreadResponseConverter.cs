using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using LinkMe.Apps.Presentation.Converters;

namespace LinkMe.Apps.Services.External.Disqus
{
    public class DisqusThreadResponseConverter
        : Converter<DisqusThreadResponse>
    {
        public override void Convert(DisqusThreadResponse obj, ISetValues values)
        {
            throw new NotImplementedException();
        }

        public override DisqusThreadResponse Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new DisqusThreadResponse
            {
                Category = values.GetIntValue("category").Value,
                Reactions = values.GetIntValue("reactions").Value,
                CanModerate = values.GetBooleanValue("canModerate").Value,
                Author = values.GetIntValue("author").Value,
                Forum = values.GetStringValue("forum"),
                Title = values.GetStringValue("title"),
                Dislikes = values.GetIntValue("dislikes").Value,
                Identifiers = values.GetGuidArrayValue("identifiers"),
                UserScore = values.GetIntValue("userScore").Value,
                CreatedAt = values.GetDateTimeValue("createdAt").Value,
                Slug = values.GetStringValue("slug"),
                IsClosed = values.GetBooleanValue("isClosed").Value,
                Posts = values.GetIntValue("posts").Value,
                UserSubscription = values.GetBooleanValue("userSubscription").Value,
                Link = values.GetStringValue("link"),
                Likes = values.GetIntValue("likes").Value,
                CanPost = values.GetBooleanValue("canPost").Value,
                Id = values.GetIntValue("id").Value,
                IsDeleted = values.GetBooleanValue("isDeleted").Value,
            };
        }
    }

    public class DisqusThreadJsonModelConverter
        : Converter<DisqusThreadJsonModel>
    {
        public override void Convert(DisqusThreadJsonModel obj, ISetValues values)
        {
            throw new NotImplementedException();
        }

        public override DisqusThreadJsonModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new DisqusThreadJsonModel
            {
                Code = values.GetIntValue("code").Value,
                Response = values.GetChildValue<DisqusThreadResponse>("response"),
            };
        }
    }

    public class DisqusThreadResponseJavaScriptConverter
    : JavaScriptConverter
    {
        private static readonly Type[] Types = new[] { typeof(DisqusThreadResponse), typeof(DisqusThreadJsonModel) };

        public override object Deserialize(IDictionary<string, object> values, Type type, JavaScriptSerializer serializer)
        {
            if (type == typeof(DisqusThreadResponse))
                return new DisqusThreadResponseConverter().Deconvert(new JavaScriptValues(values, serializer), null);
            if (type == typeof(DisqusThreadJsonModel))
                return new DisqusThreadJsonModelConverter().Deconvert(new JavaScriptValues(values, serializer), null);

            return null;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var values = new Dictionary<string, object>();
            if (obj is DisqusThreadResponse)
                new DisqusThreadResponseConverter().Convert((DisqusThreadResponse)obj, new JavaScriptValues(values, serializer));
            if (obj is DisqusThreadJsonModel)
                new DisqusThreadJsonModelConverter().Convert((DisqusThreadJsonModel)obj, new JavaScriptValues(values, serializer));

            return values;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return Types; }
        }
    }
}