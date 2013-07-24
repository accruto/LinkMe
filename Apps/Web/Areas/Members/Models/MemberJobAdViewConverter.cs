using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Web.Areas.Members.Routes;
using LinkMe.Web.Domain.Roles.JobAds;

namespace LinkMe.Web.Areas.Members.Models
{
    public class MemberJobAdViewConverter
        : Converter<MemberJobAdView>
    {
        public override void Convert(MemberJobAdView view, ISetValues values)
        {
            if (view == null)
                return;

            values.SetValue("JobAdId", view.Id);

            if (!string.IsNullOrEmpty(view.Title))
                values.SetValue("Title", view.Title);

            values.SetValue("ContactDetails", view.ContactDetails.GetContactDetailsDisplayText() ?? "");
            values.SetValue("CreatedTime", view.CreatedTime.GetDateAgoText());
            values.SetValue("IsHighlighted", view.Features.IsFlagSet(JobAdFeatures.Highlight));
            values.SetValue("IsNew", view.IsNew());
            values.SetValue("JobAdUrl", view.GenerateJobAdUrl().ToString());

            // Description.

            values.SetValue("JobTypes", view.Description.JobTypes.ToString());
            values.SetValue("Salary", view.Description.Salary.GetJobAdDisplayText());

            if (!string.IsNullOrEmpty(view.Description.Content))
                values.SetValue("Content", view.Description.Content.GetContentDisplayText());

            if (!view.Description.BulletPoints.IsNullOrEmpty())
                values.SetArrayValue("BulletPoints", view.Description.BulletPoints);

            if (!view.Description.Industries.IsNullOrEmpty())
                values.SetArrayValue("Industries", view.Description.Industries.Select(i => i.Id).ToList());

            if (view.Description.Location != null)
                values.SetValue("Location", view.Description.Location.ToString());

            // Applicant.

            values.SetValue("HasViewed", view.Applicant.HasViewed);
            values.SetValue("HasApplied", view.Applicant.HasApplied);
            values.SetValue("IsFlagged", view.Applicant.IsFlagged);
        }

        public override MemberJobAdView Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            throw new NotImplementedException();
        }
    }

    public class MemberJobAdViewJavaScriptConverter
        : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> values, Type type, JavaScriptSerializer serializer)
        {
            if (type != typeof(MemberJobAdView))
                return null;

            return new MemberJobAdViewConverter().Deconvert(new JavaScriptValues(values, serializer), null);
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var values = new Dictionary<string, object>();
            var view = obj as MemberJobAdView;
            if (view == null)
                return values;

            new MemberJobAdViewConverter().Convert(view, new JavaScriptValues(values, serializer));
            return values;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new[] { typeof(MemberJobAdView) }; }
        }
    }
}
