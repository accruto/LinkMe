using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Presentation.Domain.Roles.JobAds
{
    public class JobAdFilesQuery
        : IJobAdFilesQuery
    {
        private const string JobAdTemplateXml = "<JobAdDoc><jobad></jobad></JobAdDoc>";
        private const string FileExtension = ".doc";
        private static readonly XslCompiledTransform XslTransform;
        private static readonly string ShortLine;
        private static readonly string LongLine;
        private static readonly string HeaderLogo;

        static JobAdFilesQuery()
		{
            var ns = typeof(JobAdFilesQuery).Namespace + ".Resources";
            using (var stream = Assembly.GetAssembly(typeof(JobAdFilesQuery)).GetManifestResourceStream(ns + ".transform-jobad-rtf.xsl"))
            {
                if (stream == null)
                    throw new ApplicationException("Cannot load the transform-jobad-rtf.xsl resource.");
                var xmlTextReader = new XmlTextReader(stream);
                var resolver = new XmlUrlResolver();
                XslTransform = new XslCompiledTransform();
                XslTransform.Load(xmlTextReader, null, resolver);
            }

            ShortLine = GetResource(ns + ".line_short.rtf");
            LongLine = GetResource(ns + ".line_long.rtf");
            HeaderLogo = GetResource(ns + ".logo_tiny.rtf");
        }

        JobAdFile IJobAdFilesQuery.GetJobAdFile(JobAd jobAd)
        {
            if (jobAd == null)
                throw new ArgumentNullException("jobAd");

            return ConvertJobAd(jobAd);
        }

        private static JobAdFile ConvertJobAd(JobAd jobAd)
        {
            var jobAdXml = Serialize(jobAd);

            var xsltArgumentList = new XsltArgumentList();
            xsltArgumentList.AddParam("headerLogo", "", HeaderLogo);
            xsltArgumentList.AddParam("shortLine", "", ShortLine);
            xsltArgumentList.AddParam("longLine", "", LongLine);
            xsltArgumentList.AddParam("dateTimeNow", "", DateTime.Now.ToShortDateString());

            return new JobAdFile
                       {
                           Contents = Transform(jobAdXml, xsltArgumentList),
                           FileName = FileSystem.GetValidFileName(jobAd.Title) + FileExtension
                       };
        }

        private static string GetResource(string resourceName)
        {
            using (var stream = Assembly.GetAssembly(typeof(JobAdFilesQuery)).GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new ApplicationException("Cannot load the " + resourceName + " resource.");
                return new StreamReader(stream, Encoding.UTF8).ReadToEnd();
            }
        }

        private static string Transform(string jobAdXml, XsltArgumentList argList)
        {
            if (jobAdXml == null)
                jobAdXml = JobAdTemplateXml;

            // Escape special characters.

            jobAdXml = TextUtil.EscapeRtfText(jobAdXml);

            var xmlReader = new XmlTextReader(new StringReader(jobAdXml));
            var resolver = new XmlUrlResolver();
            var stringWriter = new StringWriter();
            var xmlWriter = new XmlTextWriter(stringWriter);

            XslTransform.Transform(xmlReader, argList, xmlWriter, resolver);

            return stringWriter.ToString().TrimStart();
        }

        private static string Serialize(JobAd jobAd)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(JobAdTemplateXml);
            if (jobAd != null)
                Serialize(jobAd, xmlDoc);
            return xmlDoc.OuterXml;
        }

        private static void Append(XmlNode parentNode, string name, string value)
        {
            if (string.IsNullOrEmpty(value))
                return;
            var newNode = parentNode.OwnerDocument.CreateElement(name);
            newNode.AppendChild(parentNode.OwnerDocument.CreateCDataSection(value));
            parentNode.AppendChild(newNode);
        }

        private static void Serialize(JobAd jobAd, XmlNode xmlDoc)
        {
            var jobAdNode = xmlDoc.SelectSingleNode("//jobad");
            Append(jobAdNode, "title", jobAd.Title);

            var location = jobAd.Description.Location == null ? string.Empty : jobAd.Description.Location.ToString();
            if (!string.IsNullOrEmpty(location))
                Append(jobAdNode, "location", location);

            Append(jobAdNode, "jobtype", jobAd.Description.JobTypes.ToString());

            var salary = jobAd.Description.Salary == null ? string.Empty : jobAd.Description.Salary.GetDisplayText();
            if (!string.IsNullOrEmpty(salary))
                Append(jobAdNode, "salary", salary);

            var industry = jobAd.Description.Industries == null || jobAd.Description.Industries.Count == 0
                ? string.Empty
                : jobAd.Description.Industries[0].Name;
            if (!string.IsNullOrEmpty(industry))
                Append(jobAdNode, "industry", industry);

            if (!string.IsNullOrEmpty(jobAd.Integration.ExternalReferenceId))
                Append(jobAdNode, "referencenumber", jobAd.Integration.ExternalReferenceId);

            var company = jobAd.ContactDetails.GetContactDetailsDisplayText();
            if (!string.IsNullOrEmpty(company))
                Append(jobAdNode, "company", company);

            Append(jobAdNode, "content", HtmlUtil.StripHtmlTags(HtmlUtil.HtmlLineBreakToText(jobAd.Description.Content)));

            if (jobAd.Description.BulletPoints != null && jobAd.Description.BulletPoints.Count > 0)
            {
                var i = 1;
                foreach (var point in jobAd.Description.BulletPoints)
                {
                    if (string.IsNullOrEmpty(point)) continue;

                    Append(jobAdNode, string.Format("bulletpoint{0}", i), point);
                    i++;
                }
            }
        }
    }
}
