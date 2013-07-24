using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XmlSerializer = System.Xml.Serialization.XmlSerializer;

namespace LinkMe.Apps.Services.Test.JobAds
{
    [TestClass]
    public class XmlSerializationTests
    {
        private const int MaxTitleLength = 200;
        private XmlSerializer _serializer;
        private XmlSchema _schema;

        [TestInitialize]
        public void TestInitialize()
        {
            _serializer = new XmlSerializer(typeof(JobAdElement), null, new Type[] {}, null, Constants.XmlSerializationNamespace);
            _schema = GetSchema();
        }

        [TestMethod]
        public void TestRoundTripJobAd()
        {
            // Empty job ad.

            var jobAd = new JobAdElement();

            TryJobAd(jobAd, false);

            // Some simple properties.

            jobAd.Title = "Product Manager - Electronics Industry";
            jobAd.Summary = "- Leading technology provider\n- Offices all around the world\n"
                + "- Over $150 million dollar revenue";
            jobAd.Content = "Required knowledge and experience:\n* Strong communication and interpersonal"
                + " skills\n* General industry experience\n* In-depth product knowledge preferable";
            jobAd.EmployerCompanyName = "Great Employer";
            jobAd.ExternalReferenceId = "PM0123";
            jobAd.Location = "Sydney NSW 2000";
            jobAd.Postcode = "2000";

            jobAd.PackageDetails = "Competitive salary";
            jobAd.PositionTitle = "Product Manager";
            jobAd.ResidencyRequired = true;

            TryJobAd(jobAd, false);

            // Child objects.

            jobAd.ContactDetails = new ContactDetails { FirstName = "Job", LastName = "Poster", EmailAddress = "jposter@linkme.test", SecondaryEmailAddresses = null, FaxNumber = "02 12345678", PhoneNumber = "02 98765432" };
            jobAd.Salary = new Salary {LowerBound = 100000, UpperBound = 120000, Rate = SalaryRate.Year, Currency = Currency.AUD};

            TryJobAd(jobAd, false);

            // Arrays and lists.

            jobAd.BulletPoints = new[] { "Career opportunities", "City location" };

            jobAd.Industries = new List<string> {"Accounting", "Other"};

            TryJobAd(jobAd, false);

            // Enums.

            jobAd.JobTypes = JobTypes.PartTime;

            TryJobAd(jobAd, true); // The job ad is now complete - validate against the schema.

            jobAd.JobTypes = JobTypes.PartTime | JobTypes.Contract | JobTypes.Temp;

            TryJobAd(jobAd, true);

            // Too many bullet points.

            jobAd.BulletPoints = new[] { "a", "b", "c", "d" };
            try
            {
                TryJobAd(jobAd, true);
                Assert.Fail("Job ad with invalid bullet points should have failed schema validation.");
            }
            catch (XmlSchemaException ex)
            {
                Assert.IsTrue(ex.Message.IndexOf("invalid child element 'BulletPoint'") != -1);
            }
            jobAd.BulletPoints = null;

            // Invalid postcode.

            jobAd.Postcode = "123a";
            try
            {
                TryJobAd(jobAd, true);
                Assert.Fail("Job ad with invalid postcode should have failed schema validation.");
            }
            catch (XmlSchemaException ex)
            {
                Assert.IsTrue(ex.Message.IndexOf(":Postcode") != -1);
            }
            jobAd.Postcode = "3000";

            // Missing PositionTitle.

            jobAd.PositionTitle = null;
            try
            {
                TryJobAd(jobAd, true);
                Assert.Fail("Job ad with null position title should have failed schema validation.");
            }
            catch (XmlSchemaException ex)
            {
                Assert.IsTrue(ex.Message.IndexOf("expected: 'PositionTitle' in namespace '" + Constants.XmlSerializationNamespace + "'") != -1);
            }

            jobAd.PositionTitle = "";
            try
            {
                TryJobAd(jobAd, true);
                Assert.Fail("Job ad with empty position title should have failed schema validation.");
            }
            catch (XmlSchemaException ex)
            {
                Assert.IsTrue(ex.Message.IndexOf("expected: 'PositionTitle' in namespace '" + Constants.XmlSerializationNamespace + "'") != -1);
            }
            jobAd.PositionTitle = "Product Manager";

            // Title too long.

            jobAd.Title = new string('x', MaxTitleLength + 1);
            try
            {
                TryJobAd(jobAd, true);
                Assert.Fail("Job ad with long title should have failed schema validation.");
            }
            catch (XmlSchemaException ex)
            {
                Assert.IsTrue(ex.Message.IndexOf(":Title") != -1);
            }
            jobAd.Title = "Product Manager - Electronics Industry";

            // No email address.

            jobAd.ContactDetails.EmailAddress = null;
            try
            {
                TryJobAd(jobAd, true);
                Assert.Fail("Job ad with null contact email should have failed schema validation.");
            }
            catch (XmlSchemaException ex)
            {
                Assert.IsTrue(ex.Message.IndexOf("'email' is missing") != -1);
            }

            // Invalid email address.

            jobAd.ContactDetails.EmailAddress = "no email";
            try
            {
                TryJobAd(jobAd, true);
                Assert.Fail("Job ad with invalid contact email should have failed schema validation.");
            }
            catch (XmlSchemaException ex)
            {
                Assert.IsTrue(ex.Message.IndexOf("'email' attribute") != -1);
            }
            jobAd.ContactDetails.EmailAddress = "valid@email.address";

            // Unrecognised industry

            jobAd.Industries.Add("Some unknown industry");
            TryJobAd(jobAd, true);
        }

        private void TryJobAd(JobAdElement jobAd, bool validateAgainstSchema)
        {
            var stream = new MemoryStream();

            // Serialize.

            try
            {
                _serializer.Serialize(stream, jobAd);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Failed to serialize job ad '{0}'.",
                    jobAd.Title), ex);
            }

            // Write to stdout.

#if DEBUG
            stream.Position = 0;
            string xml = new StreamReader(stream).ReadToEnd();
            Console.WriteLine("\r\nJob Ad XML:\r\n" + xml);
#endif

            // Validate against the schema.

            if (validateAgainstSchema)
            {
                stream.Position = 0;
                stream.ValidateXml(_schema);
            }

            // Deserialize and compare against the original.

            stream.Position = 0;

            JobAdElement deserialized;
            try
            {
                deserialized = (JobAdElement)_serializer.Deserialize(stream);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Failed to deserialize job ad '{0}'.",
                    jobAd.Title), ex);
            }

            CompareJobAds(jobAd, deserialized);
        }

        private static void CompareJobAds(JobAdElement original, JobAdElement deserialized)
        {
            // BrandingLogo is ignored
            Assert.IsTrue(original.BulletPoints.NullableSequenceEqual(deserialized.BulletPoints));

            if (original.ContactDetails == null)
            {
                Assert.IsNull(deserialized.ContactDetails);
            }
            else
            {
                Assert.IsNotNull(deserialized.ContactDetails);
                Assert.AreEqual(original.ContactDetails.FirstName, deserialized.ContactDetails.FirstName);
                Assert.AreEqual(original.ContactDetails.LastName, deserialized.ContactDetails.LastName);
                Assert.AreEqual(original.ContactDetails.EmailAddress, deserialized.ContactDetails.EmailAddress);
                Assert.AreEqual(original.ContactDetails.FaxNumber, deserialized.ContactDetails.FaxNumber);
                Assert.AreEqual(original.ContactDetails.PhoneNumber, deserialized.ContactDetails.PhoneNumber);
            }

            Assert.AreEqual(original.Content, deserialized.Content);
            // CreatedTime is ignored //Assert.AreEqual(original.CreatedTime, deserialized.CreatedTime);
            Assert.AreEqual(original.EmployerCompanyName, deserialized.EmployerCompanyName);
            // ExpiryTime is ignored //Assert.AreEqual(original.ExpiryTime, deserialized.ExpiryTime);
            Assert.AreEqual(original.ExternalReferenceId, deserialized.ExternalReferenceId);
            Assert.AreEqual(original.Status, deserialized.Status);

            if (original.Industries == null)
            {
                Assert.IsTrue(deserialized.Industries == null || deserialized.Industries.Count == 0);
            }
            else
            {
                Assert.IsNotNull(deserialized.Industries);
                Assert.AreEqual(original.Industries.Count, deserialized.Industries.Count);

                // Different order for industries is acceptable.

                for (int i = 0; i < original.Industries.Count; i++)
                {
                    var oIndustry = original.Industries[i];
                    var dIndustry = FindIndustry(deserialized.Industries, oIndustry);
                    Assert.IsNotNull(dIndustry);
                    Assert.AreEqual(oIndustry, dIndustry);
                }
            }

            // Id is ignored
            // InternalReferenceId is ignored
            // JobPoster is ignored
            Assert.AreEqual(original.JobTypes, deserialized.JobTypes);
            // LastUpdatedTimeis ignored //Assert.AreEqual(original.LastUpdatedTime, deserialized.LastUpdatedTime);
            Assert.AreEqual(original.Location, deserialized.Location);
            Assert.AreEqual(original.Postcode, deserialized.Postcode);
            Assert.AreEqual(original.PackageDetails, deserialized.PackageDetails);
            Assert.AreEqual(original.PositionTitle, deserialized.PositionTitle);

            // Publishers is ignored
            Assert.AreEqual(original.ResidencyRequired, deserialized.ResidencyRequired);
            Assert.AreEqual(original.Salary, deserialized.Salary);
            // Status is ignored
            Assert.AreEqual(original.Summary, deserialized.Summary);
            Assert.AreEqual(original.Title, deserialized.Title);
        }

        private static string FindIndustry(IList<string> toSearch, string toFind)
        {
            for (int i = 0; i < toSearch.Count; i++)
            {
                var industry = toSearch[i];
                if (industry == toFind)
                    return industry;
            }

            return null;
        }

        private static XmlSchema GetSchema()
        {
            // Create a document schema that contains a JobAd as the document element.

            var testSchema = new XmlSchema {TargetNamespace = Constants.XmlSerializationNamespace};

            var include = new XmlSchemaInclude {Schema = Schemas.JobAd};
            testSchema.Includes.Add(include);

            var rootElement = new XmlSchemaElement
            {
                Name = "JobAd",
                SchemaTypeName = new XmlQualifiedName("JobAd", Constants.XmlSerializationNamespace)
            };
            testSchema.Items.Add(rootElement);
            return testSchema;
        }
    }
}