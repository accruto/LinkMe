using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Xml;
using IXmlSerializable=System.Xml.Serialization.IXmlSerializable;

namespace LinkMe.Apps.Services.JobAds
{
    [XmlRoot(ElementName = "JobAd")]
    public class JobAdElement
        : IXmlSerializable
    {
        private const SalaryRate XmlPersistenceRangeType = SalaryRate.Year;
        private const char BulletPointSeparator = '\n';

        public string EmployerCompanyName { get; set; }
        public ContactDetails ContactDetails { get; set; }
        public string ExternalReferenceId { get; set; }
        public JobAdStatus? Status { get; set; }
        public string ExternalApplyUrl { get; set; }
        public string CssFilename { get; set; }

        public string Title { get; set; }
        public string PositionTitle { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public JobTypes JobTypes { get; set; }
        public string Location { get; set; }
        public string Postcode { get; set; }
        public Salary Salary { get; set; }
        public string PackageDetails { get; set; }
        public bool ResidencyRequired { get; set; }
        public IList<string> Industries { get; set; }

        public string[] BulletPoints
        {
            get { return _bulletPoints; }
            set
            {
                if (value == null || value.Length == 0)
                {
                    _bulletPoints = null;
                }
                else
                {
                    _bulletPoints = value;

                    // Strip the separator character from the start and end of each bullet point and if it appears
                    // somewhere in the middle of the string just replace it with a space.

                    TextUtil.TrimAndReplaceCharInArray(BulletPointSeparator, _bulletPoints);
                }
            }
        }

        private string[] _bulletPoints;

        #region XML serialization

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            ReadXmlAttributes(reader);
            ReadXmlElements(reader);
            reader.ReadEndElement();
        }

	    void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            WriteXmlAttributes(writer);
            WriteXmlElements(writer);
        }

        protected virtual void ReadXmlAttributes(XmlReader reader)
        {
            ExternalReferenceId = reader.GetAttribute("externalReferenceId");

            Status = null;
            var status = reader.GetAttribute("status");
            if (!string.IsNullOrEmpty(status))
            {
                try
                {
                    Status = (JobAdStatus)Enum.Parse(typeof(JobAdStatus), status);

                    // Only support Draft or Open.

                    if (Status != JobAdStatus.Draft && Status != JobAdStatus.Open)
                        Status = JobAdStatus.Draft;
                }
                catch (Exception)
                {
                    // Ignore any errors.
                }
            }
        }

        protected virtual void ReadXmlElements(XmlReader reader)
        {
            reader.ReadStartElement();

            Title = reader.ReadElementString("Title").NullIfEmpty();
            PositionTitle = reader.ReadOptionalElementString("PositionTitle").NullIfEmpty();

            if (reader.IsStartElement("BulletPoints"))
            {
                reader.ReadStartElement("BulletPoints");
                BulletPoints = reader.ReadStringList("BulletPoint").ToArray();
                reader.ReadEndElement("BulletPoints");
            }
            else
            {
                BulletPoints = null;
            }

            Summary = reader.ReadOptionalElementString("Summary").NullIfEmpty();
            Content = reader.ReadElementString("Content").NullIfEmpty();
            EmployerCompanyName = reader.ReadOptionalElementString("EmployerCompanyName").NullIfEmpty();
            JobTypes = reader.ReadElementString("JobTypes").DeserializeEnum<JobTypes>();
            Location = reader.ReadOptionalElementString("Location").NullIfEmpty();

            if (reader.IsStartElement("Salary"))
            {
                Salary = new Salary();
                ReadXml(reader, Salary);
            }
            else
            {
                Salary = null;
            }

            PackageDetails = reader.ReadOptionalElementString("PackageDetails").NullIfEmpty();
            Postcode = reader.ReadOptionalElementString("Postcode").NullIfEmpty();
            ResidencyRequired = XmlConvert.ToBoolean(reader.ReadElementString("ResidencyRequired"));

            ContactDetails = new ContactDetails();
            var readPerson = ReadContactPersonXml(reader, ContactDetails);
            var readDetails = ReadContactDetailsXml(reader, ContactDetails);
            if (!readPerson && !readDetails)
                ContactDetails = null;

            Industries = new List<string>();
            reader.ReadStartElement("Industries");
            while (reader.IsStartElement("Industry"))
            {
                var name = reader.ReadElementString();
                if (!string.IsNullOrEmpty(name))
                    Industries.Add(name);
            }
            if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == "Industries")
                reader.ReadEndElement("Industries");

            ExternalApplyUrl = reader.ReadOptionalElementString("ExternalApplyUrl").NullIfEmpty();
            CssFilename = reader.ReadOptionalElementString("CssFilename").NullIfEmpty();
        }

        private static void ReadXml(XmlReader reader, Salary salary)
        {
            // Read values into temporary locals, so that if an exception is thrown the object is not left
            // in an inconsistent state (eg. lower bound was changed, but upper was not).

            decimal? lBound = null;
            decimal? uBound = null;

            var readingAttribute = reader.MoveToFirstAttribute();

            while (readingAttribute)
            {
                switch (reader.LocalName)
                {
                    case "minAmount":
                        lBound = ParseUtil.ParseUserInputDecimal(reader.Value, "minValue salary attribute");
                        break;

                    case "maxAmount":
                        uBound = ParseUtil.ParseUserInputDecimal(reader.Value, "maxAmount salary attribute");
                        break;

                    default:
                        throw new ApplicationException("Unexpected attribute '" + reader.LocalName + "' was found on the Salary element.");
                }

                readingAttribute = reader.MoveToNextAttribute();
            }

            reader.MoveToContent();
            reader.Read();

            salary.LowerBound = lBound;
            salary.UpperBound = uBound;
            salary.Rate = XmlPersistenceRangeType;
            salary.Currency = Currency.AUD;
        }

        private static bool ReadContactPersonXml(XmlReader reader, ContactDetails contactDetails)
        {
            if (!reader.IsStartElement("ContactPerson"))
                return false;

            contactDetails.FirstName = reader.GetAttribute("firstName");
            contactDetails.LastName = reader.GetAttribute("lastName");

            reader.ReadStartElement();
            return true;
        }

        private static bool ReadContactDetailsXml(XmlReader reader, ContactDetails contactDetails)
        {
            if (!reader.IsStartElement("ContactDetails"))
                return false;

            contactDetails.EmailAddress = reader.GetAttribute("email");
            contactDetails.FaxNumber = reader.GetAttribute("faxNumber");
            contactDetails.PhoneNumber = reader.GetAttribute("phoneNumber");

            reader.ReadStartElement();
            return true;
        }

	    protected virtual void WriteXmlAttributes(XmlWriter writer)
	    {
	        writer.WriteNonEmptyAttributeString("externalReferenceId", ExternalReferenceId);
            if (Status != null)
                writer.WriteNonEmptyAttributeString("status", Status.Value.ToString());
	    }

	    protected virtual void WriteXmlElements(XmlWriter writer)
	    {
	        writer.WriteElementString("Title", Title);
            writer.WriteNonEmptyElementString("PositionTitle", PositionTitle);

	        if (!BulletPoints.IsNullOrEmpty())
	        {
	            writer.WriteStartElement("BulletPoints");
	            foreach (string bulletPoint in BulletPoints)
	            {
	                writer.WriteElementString("BulletPoint", bulletPoint);
	            }
	            writer.WriteEndElement(); // </BulletPoints>
	        }

	        writer.WriteNonEmptyElementString("Summary", Summary);
	        writer.WriteElementString("Content", Content);
	        writer.WriteNonEmptyElementString("EmployerCompanyName", EmployerCompanyName);
	        writer.WriteElementString("JobTypes", JobTypes.SerializeEnum());
	        writer.WriteNonEmptyElementString("Location", Location);

	        if (Salary != null && !Salary.IsEmpty)
	        {
	            writer.WriteStartElement("Salary");
	            WriteXml(writer, Salary);
	            writer.WriteEndElement(); // </Salary>
	        }

	        writer.WriteNonEmptyElementString("PackageDetails", PackageDetails);
	        writer.WriteNonEmptyElementString("Postcode", Postcode);
	        writer.WriteElementString("ResidencyRequired", XmlConvert.ToString(ResidencyRequired));

	        // This is the whole reason for custom XML seialization: ContactDetails class writing an
	        // OPTIONAL ContactPerson element.

            if (ContactDetails != null)
            {
                WriteContactPersonXml(writer, ContactDetails);
                WriteContactDetailsXml(writer, ContactDetails);
            }

	        writer.WriteStartElement("Industries");
            if (Industries != null)
            {
                foreach (var industry in Industries)
                {
                    writer.WriteStartElement("Industry");
                    writer.WriteString(industry);
                    writer.WriteEndElement(); // </Industry>
                }
            }
	        writer.WriteEndElement(); // </Industries>

	        writer.WriteNonEmptyElementString("ExternalApplyUrl", ExternalApplyUrl);
	        writer.WriteNonEmptyElementString("CssFilename", CssFilename);
	    }

        private static void WriteXml(XmlWriter writer, Salary salary)
        {
            // Convert to yearly before serializing.

            var yearly = salary.ToRate(XmlPersistenceRangeType);
            if (yearly.HasLowerBound)
                writer.WriteAttributeString("minAmount", yearly.LowerBound.ToString());
            if (yearly.HasUpperBound)
                writer.WriteAttributeString("maxAmount", yearly.UpperBound.ToString());
        }

        private static void WriteContactPersonXml(XmlWriter writer, ContactDetails contactDetails)
        {
            if (string.IsNullOrEmpty(contactDetails.FirstName) || string.IsNullOrEmpty(contactDetails.LastName))
                return;

            writer.WriteStartElement("ContactPerson");

            writer.WriteNonEmptyAttributeString("firstName", contactDetails.FirstName);
            writer.WriteNonEmptyAttributeString("lastName", contactDetails.LastName);

            writer.WriteEndElement(); // </ContactPerson>
        }

        private static void WriteContactDetailsXml(XmlWriter writer, ContactDetails contactDetails)
        {
            writer.WriteStartElement("ContactDetails");

            // Email is mandatory according to the XML schema. It's not mandatory in the database,
            // but in practice it's only null for CareerOne job ads, which we don't send in the job ad feed
            // anyway, so it all works out OK.

            writer.WriteNonEmptyAttributeString("email", contactDetails.EmailAddress);
            writer.WriteNonEmptyAttributeString("faxNumber", contactDetails.FaxNumber);
            writer.WriteNonEmptyAttributeString("phoneNumber", contactDetails.PhoneNumber);

            writer.WriteEndElement(); // </ContactDetails>
        }

        #endregion
    }
}
