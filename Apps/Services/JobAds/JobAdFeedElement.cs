using System;
using System.Xml;
using System.Xml.Serialization;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Apps.Services.JobAds
{
    [XmlRoot(ElementName = "JobAd")]
    public class JobAdFeedElement
        : JobAdElement
    {
        private string _viewJobAdUrl;
        private string _applyJobAdUrl;

        public string ViewJobAdUrl
        {
            get { return _viewJobAdUrl; }
            set { _viewJobAdUrl = value.ToLower(); }
        }

        public string ApplyJobAdUrl
        {
            get { return _applyJobAdUrl; }
            set { _applyJobAdUrl = value.ToLower(); }
        }

        public Guid Id { get; set; }
        public bool Featured { get; set; }
        public string RecruiterCompanyName { get; set; }

        #region XML serialization

        protected override void ReadXmlAttributes(XmlReader reader)
        {
            base.ReadXmlAttributes(reader);

            Id = XmlConvert.ToGuid(reader.GetAttribute("id"));
            ViewJobAdUrl = reader.GetAttribute("viewJobAdUrl");
            ApplyJobAdUrl = reader.GetAttribute("applyJobAdUrl");

            var featured = reader.GetAttribute("featured");
            if (featured != null)
                Featured = XmlConvert.ToBoolean(featured);
        }

        protected override void ReadXmlElements(XmlReader reader)
        {
            base.ReadXmlElements(reader);
            RecruiterCompanyName = reader.ReadOptionalElementString("RecruiterCompanyName");
        }

        protected override void WriteXmlAttributes(XmlWriter writer)
        {
            base.WriteXmlAttributes(writer);
            writer.WriteAttributeString("id", XmlConvert.ToString(Id));
            writer.WriteAttributeString("viewJobAdUrl", ViewJobAdUrl);
            writer.WriteAttributeString("applyJobAdUrl", ApplyJobAdUrl);

            if (Featured)
                writer.WriteAttributeString("featured", XmlConvert.ToString(Featured));
        }

        protected override void WriteXmlElements(XmlWriter writer)
        {
            base.WriteXmlElements(writer);
            writer.WriteNonEmptyElementString("RecruiterCompanyName", RecruiterCompanyName);
        }

        #endregion
    }
}