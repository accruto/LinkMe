using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using LinkMe.Apps.Presentation.Domain.Roles.Resumes;
using LinkMe.Apps.Presentation.Domain.Users.Members;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Presentation.Domain.Roles.Candidates
{
    public class ResumeFilesQuery
        : IResumeFilesQuery
    {
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery;

        private const string ResumeTemplateXml = "<ResDoc><contact></contact><resume></resume></ResDoc>";
        private const string CurrentDateText = "current";
        private const string DateTextFormat = "MMMM yyyy";
        private const string RtfLinebreak = "\\par ";
        private const string FileExtension = ".doc";
        private static readonly XslCompiledTransform XslTransform;
        private static readonly string ShortLine;
        private static readonly string LongLine;
        private static readonly string HeaderLogo;

        static ResumeFilesQuery()
		{
            var ns = typeof(ResumeFilesQuery).Namespace + ".Resources";
            using (var stream = Assembly.GetAssembly(typeof(ResumeFilesQuery)).GetManifestResourceStream(ns + ".transform-resume-rtf.xsl"))
            {
                if (stream == null)
                    throw new ApplicationException("Cannot load the transform-resume-rtf.xsl resource.");
                var xmlTextReader = new XmlTextReader(stream);
                var resolver = new XmlUrlResolver();
                XslTransform = new XslCompiledTransform();
                XslTransform.Load(xmlTextReader, null, resolver);
            }

            ShortLine = GetResource(ns + ".line_short.rtf");
            LongLine = GetResource(ns + ".line_long.rtf");
            HeaderLogo = GetResource(ns + ".logo_tiny.rtf");
		}

        public ResumeFilesQuery(IEmployerMemberViewsQuery employerMemberViewsQuery)
        {
            _employerMemberViewsQuery = employerMemberViewsQuery;
        }

        ResumeFile IResumeFilesQuery.GetResumeFile(RegisteredUser recipient, Member member, Candidate candidate, Resume resume)
        {
            if (candidate == null)
                throw new ArgumentNullException("candidate");
            if (recipient == null)
                throw new ArgumentNullException("recipient");

            EmployerMemberView view;
            var employer = recipient as Employer;
            if (employer != null)
            {
                view = _employerMemberViewsQuery.GetEmployerMemberView(employer, member);
            }
            else if (recipient is Member)
            {
                if (recipient.Id != member.Id)
                    throw new ApplicationException("Downloading the resume of another member is not supported.");
                view = new EmployerMemberView(member, candidate, resume, null, ProfessionalContactDegree.Self, false, false);
            }
            else if (recipient is Administrator)
            {
                view = new EmployerMemberView(member, candidate, resume, null, ProfessionalContactDegree.Self, false, false);
            }
            else
                throw new ApplicationException("Unexpected type of recipient: " + recipient.GetType().FullName);

            return ConvertResume(view, resume);
        }

        ResumeFile IResumeFilesQuery.GetResumeFile(EmployerMemberView view, Resume resume)
        {
            return ConvertResume(view, resume);
        }

        private static ResumeFile ConvertResume(EmployerMemberView view, Resume resume)
        {
            // Use an empty resume if none provided.

            if (resume == null)
                resume = new Resume();

            var resumeXml = Serialize(resume);

            var xsltArgumentList = new XsltArgumentList();
            xsltArgumentList.AddParam("headerLogo", "", HeaderLogo);
            xsltArgumentList.AddParam("shortLine", "", ShortLine);
            xsltArgumentList.AddParam("longLine", "", LongLine);
            xsltArgumentList.AddParam("dateTimeNow", "", DateTime.Now.ToShortDateString());

            if (!string.IsNullOrEmpty(view.FullName))
                xsltArgumentList.AddParam("networkerName", "", view.FullName);

            // Use the first current job.

            var currentJobTitles = resume.GetCurrentJobTitles();
            var currentJobTitle = currentJobTitles == null || currentJobTitles.Length == 0
                ? string.Empty
                : currentJobTitles[0];

            if (!string.IsNullOrEmpty(currentJobTitle))
                xsltArgumentList.AddParam("currentJobTitle", "", currentJobTitle);
            var currentIndustry = view.GetIndustriesDisplayText(", ");
            if (!string.IsNullOrEmpty(currentIndustry))
                xsltArgumentList.AddParam("currentIndustry", "", currentIndustry);

            xsltArgumentList.AddParam("updatedDate", "", resume.LastUpdatedTime.ToString("d MMMM yyyy"));

            var industries = view.GetIndustriesDisplayText(RtfLinebreak);
            if (!string.IsNullOrEmpty(industries))
                xsltArgumentList.AddParam("industries", "", industries);

            var location = view.Address.Location.ToString();
            if (!string.IsNullOrEmpty(location))
                xsltArgumentList.AddParam("location", "", location);

            var state = view.Address.Location.CountrySubdivision;
            if (state != null)
            {
                if (state.ShortName != null)
                    xsltArgumentList.AddParam("state", "", state.ShortName);
                xsltArgumentList.AddParam("country", "", state.Country.Name);
            }

            var phoneNumber = GetPhoneNumber(view, PhoneNumberType.Mobile);
            if (phoneNumber != null)
                xsltArgumentList.AddParam("mobilePhoneNumber", "", phoneNumber.Number);
            phoneNumber = GetPhoneNumber(view, PhoneNumberType.Work);
            if (phoneNumber != null)
                xsltArgumentList.AddParam("workPhoneNumber", "", phoneNumber.Number);
            phoneNumber = GetPhoneNumber(view, PhoneNumberType.Home);
            if (phoneNumber != null)
                xsltArgumentList.AddParam("homePhoneNumber", "", phoneNumber.Number);

            if (!string.IsNullOrEmpty(view.DesiredJobTitle))
                xsltArgumentList.AddParam("desiredJobTitle", "", view.DesiredJobTitle);
            xsltArgumentList.AddParam("desiredJobTypes", "", view.DesiredJobTypes.GetDesiredClauseDisplayText());
            if (view.DesiredSalary != null)
                xsltArgumentList.AddParam("desiredSalary", "", view.DesiredSalary.GetDisplayText());

            var showRealName = !string.IsNullOrEmpty(view.FullName);

            xsltArgumentList.AddParam("showRealName", "", showRealName);
            xsltArgumentList.AddParam("showPhoneNumbers", "", view.PhoneNumbers != null && view.PhoneNumbers.Count > 0 && view.PhoneNumbers.All(p => !string.IsNullOrEmpty(p.Number)));
            xsltArgumentList.AddParam("showProfilePhoto", "", view.PhotoId != null);
            xsltArgumentList.AddParam("showRecentEmployers", "", true);
            xsltArgumentList.AddParam("showCommunities", "", view.AffiliateId != null);

            return new ResumeFile
            {
                Contents = Transform(resumeXml, xsltArgumentList),
                FileName = FileSystem.GetValidFileName(view.GetDisplayText(view, view.Resume, true)) + FileExtension
            };
        }

        private static PhoneNumber GetPhoneNumber(IMember view, PhoneNumberType type)
        {
            if (view.PhoneNumbers == null)
                return null;
            var phoneNumber = (from p in view.PhoneNumbers where p.Type == type && !string.IsNullOrEmpty(p.Number) select p).FirstOrDefault();
            return phoneNumber == null || string.IsNullOrEmpty(phoneNumber.Number) ? null : phoneNumber;
        }

        private static string GetResource(string resourceName)
        {
            using (var stream = Assembly.GetAssembly(typeof(ResumeFilesQuery)).GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new ApplicationException("Cannot load the " + resourceName + " resource.");
                return new StreamReader(stream, Encoding.UTF8).ReadToEnd();
            }
        }

        private static string Transform(string resumeXml, XsltArgumentList argList)
        {
            if (resumeXml == null)
                resumeXml = ResumeTemplateXml;

            // Escape special characters.

            resumeXml = TextUtil.EscapeRtfText(resumeXml);

            var xmlReader = new XmlTextReader(new StringReader(resumeXml));
            var resolver = new XmlUrlResolver();
            var stringWriter = new StringWriter();
            var xmlWriter = new XmlTextWriter(stringWriter);

            XslTransform.Transform(xmlReader, argList, xmlWriter, resolver);

            return stringWriter.ToString().TrimStart();
        }

        private static string Serialize(Resume resume)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(ResumeTemplateXml);
            if (resume != null)
                Serialize(resume, xmlDoc);
            return xmlDoc.OuterXml;
        }

        private static XmlNode Append(XmlNode parentNode, string name)
        {
            var newNode = parentNode.OwnerDocument.CreateElement(name);
            parentNode.AppendChild(newNode);
            return newNode;
        }

        private static void Append(XmlNode parentNode, string name, string value)
        {
            if (string.IsNullOrEmpty(value))
                return;
            var newNode = parentNode.OwnerDocument.CreateElement(name);
            newNode.AppendChild(parentNode.OwnerDocument.CreateCDataSection(value));
            parentNode.AppendChild(newNode);
        }

        private static void AppendAttribute(XmlNode node, string name, string value)
        {
            var attribute = node.OwnerDocument.CreateAttribute(name);
            attribute.Value = value;
            node.Attributes.Append(attribute);
        }

        private static string ToString(PartialDate? dt)
        {
            return dt == null
                ? CurrentDateText
                : dt.Value.ToString(DateTextFormat);
        }

        private static void Serialize(Resume resume, XmlNode xmlDoc)
        {
            var resumeNode = xmlDoc.SelectSingleNode("//resume");
            Append(resumeNode, "objective", resume.Objective);
            Append(resumeNode, "summary", resume.Summary);
            Append(resumeNode, "other", resume.Other);
            Append(resumeNode, "professional", resume.Professional);
            Append(resumeNode, "skills", resume.Skills);
            Append(resumeNode, resume.Citizenship, resume.Affiliations, resume.Interests, resume.Referees);

            Append(resumeNode, resume.Jobs);
            Append(resumeNode, resume.Schools);

            Append(resumeNode, resume.Courses, "education", "courses");
            Append(resumeNode, resume.Awards, "statements", "honors");
        }

        private static void Append(XmlNode resumeNode, string citizenship, string affiliations, string interests, string references)
        {
            var statementsNode = resumeNode.OwnerDocument.CreateElement("statements");
            Append(statementsNode, "citizenship", citizenship);
            Append(statementsNode, "affiliations", affiliations);
            Append(statementsNode, "activities", interests);
            Append(statementsNode, "references", references);
            if (statementsNode.HasChildNodes)
                resumeNode.AppendChild(statementsNode);
        }

        private static void Append(XmlNode resumeNode, ICollection<Job> jobs)
        {
            if (jobs == null || jobs.Count == 0)
                return;

            var experienceNode = resumeNode.OwnerDocument.CreateElement("experience");
            foreach (var job in jobs)
                Append(experienceNode, job);

            if (experienceNode.HasChildNodes)
                resumeNode.AppendChild(experienceNode);
        }

        private static void Append(XmlNode experienceNode, IJob job)
        {
            var jobNode = Append(experienceNode, "job");

            if (job.Id != Guid.Empty)
                AppendAttribute(jobNode, "guid", job.Id.ToString("n"));

            Append(jobNode, "title", job.Title);
            Append(jobNode, "description", job.Description);
            Append(jobNode, "employer", job.Company);

            if (job.Dates != null)
            {
                var daterangeNode = Append(jobNode, "daterange");
                if (job.Dates.Start != null)
                    Append(daterangeNode, "start", ToString(job.Dates.Start));
                Append(daterangeNode, "end", ToString(job.Dates.End));
            }
        }

        private static void Append(XmlNode resumeNode, ICollection<School> schools)
        {
            if (schools == null || schools.Count == 0)
                return;

            var educationNode = resumeNode.OwnerDocument.CreateElement("education");
            foreach (var school in schools)
                Append(educationNode, school);

            if (educationNode.HasChildNodes)
                resumeNode.AppendChild(educationNode);
        }

        private static void Append(XmlNode educationNode, ISchool school)
        {
            var schoolNode = Append(educationNode, "school");

            if (school.Id != Guid.Empty)
                AppendAttribute(schoolNode, "guid", school.Id.ToString("n"));

            if (school.CompletionDate != null)
            {
                var daterangeNode = Append(schoolNode, "daterange");
                Append(daterangeNode, "end", ToString(school.CompletionDate.End));
            }

            Append(schoolNode, "institution", school.Institution);
            Append(schoolNode, "degree", school.Degree);
            Append(schoolNode, "major", school.Major);
            Append(schoolNode, "description", school.Description);

            var addressNode = Append(schoolNode, "address");
            Append(addressNode, "city", school.City);
            Append(addressNode, "country", school.Country);
        }

        private static void Append(XmlNode parentNode, ICollection<string> items, string listName, string itemName)
        {
            if (items != null && items.Count > 0)
            {
                var listNode = parentNode.OwnerDocument.CreateElement(listName);
                foreach (var textItem in items)
                    Append(listNode, itemName, textItem);
                parentNode.AppendChild(listNode);
            }
        }
    }
}
