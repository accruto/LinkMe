using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Anonymous.Queries;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Web.Areas.Integration.Routes;

namespace LinkMe.Web.Areas.Integration.Controllers
{
    public class JobAdApplicationsManager
        : JobAdManager, IJobAdApplicationsManager
    {
        private readonly IJobAdsCommand _jobAdsCommand;
        private readonly IApplicationsCommand _applicationsCommand;
        private readonly IMemberApplicationsQuery _memberApplicationsQuery;
        private readonly IMembersQuery _membersQuery;
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly IResumesQuery _resumesQuery;
        private readonly IEmployerResumeFilesQuery _employerResumeFilesQuery;
        private readonly IAnonymousUsersQuery _anonymousUsersQuery;
        private readonly IFilesQuery _filesQuery;
        private readonly ILocationQuery _locationQuery;

        public JobAdApplicationsManager(IJobAdsCommand jobAdsCommand, IApplicationsCommand applicationsCommand, IMemberApplicationsQuery memberApplicationsQuery, IMembersQuery membersQuery, ICandidatesQuery candidatesQuery, IResumesQuery resumesQuery, IEmployerResumeFilesQuery employerResumeFilesQuery, IAnonymousUsersQuery anonymousUsersQuery, IFilesQuery filesQuery, ILocationQuery locationQuery)
        {
            _jobAdsCommand = jobAdsCommand;
            _applicationsCommand = applicationsCommand;
            _memberApplicationsQuery = memberApplicationsQuery;
            _membersQuery = membersQuery;
            _candidatesQuery = candidatesQuery;
            _resumesQuery = resumesQuery;
            _employerResumeFilesQuery = employerResumeFilesQuery;
            _anonymousUsersQuery = anonymousUsersQuery;
            _filesQuery = filesQuery;
            _locationQuery = locationQuery;
        }

        string IJobAdApplicationsManager.GetJobApplication(IntegratorUser integratorUser, Guid applicationId, List<string> errors)
        {
            var application = _memberApplicationsQuery.GetInternalApplication(applicationId);
            if (application == null)
            {
                errors.Add("There is no job application with ID '" + applicationId + "'.");
                return null;
            }
            
            var jobAd = _jobAdsCommand.GetJobAd<JobAdEntry>(application.PositionId);
            if (jobAd.Integration.IntegratorUserId != integratorUser.Id)
            {
                errors.Add(string.Format("Job application {0:b} is for a job not posted by the current integrator user, '{1}'.", application.Id, integratorUser.LoginId));
                return null;
            }

            // Determine whether it is a member or anonymous user application.

            var member = _membersQuery.GetMember(application.ApplicantId);
            if (member != null)
                return GetJobApplication(application, member);

            var contact = _anonymousUsersQuery.GetContact(application.ApplicantId);
            if (contact != null)
                return GetJobApplication(application, contact);

            errors.Add("There is no job application with ID '" + applicationId + "'.");
            return null;
        }

        string IJobAdApplicationsManager.SetApplicationStatuses(IntegratorUser integratorUser, string requestXml, List<string> errors)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(requestXml);
            var xmlNsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            xmlNsMgr.AddNamespace(XmlNamespacePrefix, XmlNamespace);
            var xmlNode = xmlDoc.SelectSingleNode("/" + XmlNamespacePrefix + ":SetJobApplicationStatusRequest", xmlNsMgr);

            var applicationIds = GetApplicationIds(xmlNode, xmlNsMgr, errors);

            // Set them.

            return SetApplicationStatuses(integratorUser, applicationIds, errors);
        }

        DocFile IJobAdApplicationsManager.GetResume(Member member)
        {
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

            var view = new EmployerMemberView(member, candidate, resume, null, ProfessionalContactDegree.Applicant, false, false);
            return _employerResumeFilesQuery.GetResumeFile(view);
        }

        private string GetJobApplication(InternalApplication application, Member member)
        {
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = application.ResumeId == null || candidate.ResumeId == null
                ? null
                : _resumesQuery.GetResume(candidate.ResumeId.Value);

            var file = application.ResumeFileId != null
                ? _filesQuery.GetFileReference(application.ResumeFileId.Value)
                : null;

            return GetResponseXml(member, resume, file);
        }

        private string GetJobApplication(InternalApplication application, ICommunicationRecipient contact)
        {
            var file = application.ResumeFileId != null
                ? _filesQuery.GetFileReference(application.ResumeFileId.Value)
                : null;

            // Create a member to represent the contact.

            var member = new Member
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = contact.EmailAddress, IsVerified = true } },
            };

            var resume = new Resume();

            return GetResponseXml(member, resume, file);
        }

        private string SetApplicationStatuses(IntegratorUser integratorUser, ICollection<Guid> applicationsIds, ICollection<string> errors)
        {
            var applications = _memberApplicationsQuery.GetApplications(applicationsIds);

            var updated = 0;
            var toUpdate = new List<Application>();

            foreach (var applicationId in applicationsIds)
            {
                var application = (from a in applications where a.Id == applicationId select a).SingleOrDefault();
                if (application == null)
                {
                    errors.Add(string.Format("There is no job application with ID {0:b}.", applicationId));
                }
                else
                {
                    var jobAd = _jobAdsCommand.GetJobAd<JobAdEntry>(application.PositionId);
                    if (jobAd.Integration.IntegratorUserId != integratorUser.Id)
                    {
                        errors.Add(string.Format("Job application {0:b} is for a job not posted by the current integrator user, '{1}'.", application.Id, integratorUser));
                    }
                    else
                    {
                        if (application is InternalApplication)
                            SetJobApplicationStatus((InternalApplication)application, toUpdate, ref updated);
                    }
                }
            }

            // Save the updated applications.

            if (toUpdate.Count > 0)
            {
                foreach (var application in toUpdate)
                    _applicationsCommand.UpdateApplication(application);
            }

            return GetResponseXml(applicationsIds.Count, updated);
        }

        private string GetResponseXml(Member member, Resume resume, FileReference resumeFile)
        {
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                using (var xmlWriter = new XmlTextWriter(writer))
                {
                    xmlWriter.WriteStartElement("JobApplication");

                    WriteApplicantDetails(xmlWriter, member);
                    if (resumeFile != null)
                        WriteResumeDetails(xmlWriter, resumeFile);
                    else if (resume != null)
                        WriteResumeDetails(xmlWriter, member);

                    xmlWriter.WriteEndElement();
                }
            }

            return sb.ToString();
        }

        private void WriteApplicantDetails(XmlWriter xmlWriter, IMember applicant)
        {
            xmlWriter.WriteStartElement("Applicant");

            xmlWriter.WriteAttributeString("firstName", applicant.FirstName);
            xmlWriter.WriteAttributeString("lastName", applicant.LastName);
            xmlWriter.WriteAttributeString("email", applicant.GetBestEmailAddress().Address);
            var phoneNumber = applicant.GetBestPhoneNumber();
            if (phoneNumber != null)
                xmlWriter.WriteAttributeString("contactPhoneNumber", phoneNumber.Number);
            WriteApplicantAddress(xmlWriter, applicant);

            xmlWriter.WriteEndElement();
        }

        private void WriteApplicantAddress(XmlWriter xmlWriter, IMember applicant)
        {
            // At this stage only write Australian addresses.

            if (applicant.Address == null || applicant.Address.Location == null || applicant.Address.Location.IsCountry || applicant.Address.Location.CountrySubdivision.Country.Id != _locationQuery.GetCountry("Australia").Id)
                return;

            var postcode = applicant.Address.Location.Postcode;
            var state = applicant.Address.Location.CountrySubdivision.ShortName;
            if (postcode == null && state == null)
                return;

            xmlWriter.WriteStartElement("Address");
            if (state != null)
                xmlWriter.WriteAttributeString("state", state);
            if (postcode != null)
                xmlWriter.WriteAttributeString("postcode", postcode);
            xmlWriter.WriteEndElement();
        }

        private static void WriteResumeDetails(XmlWriter xmlWriter, FileReference resumeFile)
        {
            if (resumeFile == null)
                return;
            xmlWriter.WriteStartElement("Resume");
            if (string.IsNullOrEmpty(resumeFile.FileName))
                xmlWriter.WriteAttributeString("uri", JobAdsRoutes.ResumeFile.GenerateUrl(new { fileId = resumeFile.Id }).AbsoluteUri);
            else
                xmlWriter.WriteAttributeString("uri", JobAdsRoutes.ResumeFileName.GenerateUrl(new { fileId = resumeFile.Id, fileName = resumeFile.FileName }).AbsoluteUri);
            xmlWriter.WriteAttributeString("name", resumeFile.FileName);
            xmlWriter.WriteEndElement();
        }

        private static void WriteResumeDetails(XmlWriter xmlWriter, ICommunicationRecipient applicant)
        {
            xmlWriter.WriteStartElement("Resume");
            xmlWriter.WriteAttributeString("uri", JobAdsRoutes.Resume.GenerateUrl(new { candidateId = applicant.Id.ToString("n") }).AbsoluteUri);
            xmlWriter.WriteAttributeString("name", GetResumeFileName(applicant.FirstName, applicant.LastName));
            xmlWriter.WriteEndElement();
        }

        private static string GetResumeFileName(string firstName, string lastName)
        {
            return string.Format("{0}_{1}_LinkMeCV.doc", lastName, firstName);
        }

        private static string GetResponseXml(int total, int updated)
        {
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                using (var xmlWriter = new XmlTextWriter(writer))
                {
                    xmlWriter.WriteStartElement("JobApplications");
                    xmlWriter.WriteAttributeString("updated", updated.ToString());
                    xmlWriter.WriteAttributeString("failed", (total - updated).ToString());
                    xmlWriter.WriteEndElement();
                }
            }

            return sb.ToString();
        }

        private static void SetJobApplicationStatus(InternalApplication application, ICollection<Application> toUpdate, ref int updated)
        {
            // OK, this job application status can be updated.

            if (application.IsPending)
            {
                application.IsPending = false;
                toUpdate.Add(application);
                updated++;

            }
            else
            {
                updated++; // Already submitted, OK.
            }
        }

        private static IList<Guid> GetApplicationIds(XmlNode xmlNode, XmlNamespaceManager xmlNsMgr, ICollection<string> errors)
        {
            var applicationIds = new List<Guid>();

            var nodes = xmlNode.SelectNodes(XmlNamespacePrefix + ":JobApplications/" + XmlNamespacePrefix + ":JobApplication", xmlNsMgr);
            if (nodes != null)
            {
                foreach (XmlNode xmlApplicationNode in nodes)
                {
                    var applicationId = GetApplicationId(xmlApplicationNode, errors);
                    if (applicationId != null)
                        applicationIds.Add(applicationId.Value);
                }
            }

            return applicationIds;
        }

        private static Guid? GetApplicationId(XmlNode node, ICollection<string> errors)
        {
            var status = GetStatus(node.Attributes["status"], errors);
            if (status == null)
                return null;

            var id = GetId(node.Attributes["id"]);
            var uriId = GetUriId(node.Attributes["uri"], errors);

            if (id != null && uriId != null)
                errors.Add("Both an ID and a URI were specified for a job application.");
            if (id == null && uriId != null)
                id = uriId;

            if (id == null)
            {
                errors.Add("No 'uri' attribute was specified for a JobApplication.");
                return null;
            }

            return id;
        }

        private static string GetStatus(XmlNode attribute, ICollection<string> errors)
        {
            if (attribute == null || attribute.Value == null)
            {
                errors.Add("No 'status' attribute was specified for  JobApplication.");
                return null;
            }

            if (attribute.Value != "Submitted")
            {
                errors.Add("The value of the 'status' attribute, '" + attribute.Value + "', is not a valid application status.");
                return null;
            }

            return attribute.Value;
        }

        private static Guid? GetId(string value)
        {
            try
            {
                return new Guid(value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static Guid? GetId(XmlNode attribute)
        {
            return attribute == null ? null : GetId(attribute.Value);
        }

        private static Guid? GetUriId(XmlNode attribute, ICollection<string> errors)
        {
            if (attribute == null)
                return null;

            var value = attribute.Value.TrimEnd('/');

            // Take the bit after the last '/' - that should be the ID.

            var index = value.LastIndexOf('/');
            if (index == -1 || index == value.Length - 1)
            {
                errors.Add("The URI does not contain an ID.");
                return null;
            }

            return GetId(value.Substring(index + 1, value.Length - index - 1));
        }
    }
}
