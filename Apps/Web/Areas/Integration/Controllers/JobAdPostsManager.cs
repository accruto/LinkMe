using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Apps.Services.JobAds.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Integration.Controllers
{
    public class JobAdPostsManager
        : JobAdManager, IJobAdPostsManager
    {
        private readonly IEmployerJobAdsCommand _employerJobAdsCommand;
        private readonly IJobAdsCommand _jobAdsCommand;
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IJobAdIntegrationQuery _jobAdIntegrationQuery;
        private readonly IExternalJobAdsCommand _externalJobAdsCommand;

        public JobAdPostsManager(IEmployerJobAdsCommand employerJobAdsCommand, IJobAdsCommand jobAdsCommand, IJobAdsQuery jobAdsQuery, IJobAdIntegrationQuery jobAdIntegrationQuery, IExternalJobAdsCommand externalJobAdsCommand)
        {
            _employerJobAdsCommand = employerJobAdsCommand;
            _jobAdsCommand = jobAdsCommand;
            _jobAdsQuery = jobAdsQuery;
            _jobAdIntegrationQuery = jobAdIntegrationQuery;
            _externalJobAdsCommand = externalJobAdsCommand;
        }

        CloseReport IJobAdPostsManager.CloseJobAds(Guid integratorUserId, IEmployer jobPoster, IEnumerable<string> externalReferenceIds)
        {
            var report = new CloseReport();

            // Read the existing job ads for this integrator/job poster combination.

            var existingJobAdIds = GetExistingJobAdIds(integratorUserId, jobPoster.Id);

            // Find which of the specified job ad external IDs exist and close those.

            var externalReferenceIdList = externalReferenceIds.ToList();
            var jobAdIds = FilterJobAdIds(externalReferenceIdList, existingJobAdIds, report);
            if (jobAdIds.Count > 0)
            {
                var jobAds = _jobAdsQuery.GetJobAds<JobAd>(jobAdIds);
                foreach (var jobAd in jobAds)
                {
                    if (_jobAdsCommand.CanBeClosed(jobAd))
                        _jobAdsCommand.CloseJobAd(jobAd);
                }
            }

            // Fill in the report.

            report.Closed = jobAdIds.Count;
            report.Failed = externalReferenceIdList.Count - jobAdIds.Count;

            return report;
        }

        PostReport IJobAdPostsManager.PostJobAds(IntegratorUser integratorUser, IEmployer jobPoster, IEmployer integratorJobPoster, IEnumerable<JobAd> jobAds, bool closeAllOtherJobAds)
        {
            // Read the jobads.

            var report = new PostReport();

            // Process them.

            jobPoster = jobPoster ?? integratorJobPoster;

            foreach (var jobAd in jobAds)
                ProcessJobAd(integratorUser.Id, jobPoster, jobAd, report);

            if (!closeAllOtherJobAds)
                return report;

            var jobAdIds = _jobAdIntegrationQuery.GetJobAdIds(integratorUser.Id, jobPoster.Id);
            var closeJobAdIds = jobAdIds.Except(report.ProcessedJobAdIds).ToList();

            var closeJobAds = _jobAdsQuery.GetJobAds<JobAd>(closeJobAdIds);
            foreach (var closeJobAd in closeJobAds)
                _jobAdsCommand.CloseJobAd(closeJobAd);
            report.Closed += closeJobAdIds.Count();

            return report;
        }

        private IDictionary<string, List<Guid>> GetExistingJobAdIds(Guid integratorUserId, Guid jobPosterId)
        {
            return (from j in _jobAdsQuery.GetJobAds<JobAdEntry>(_jobAdIntegrationQuery.GetJobAdIds(integratorUserId, jobPosterId))
                    where !string.IsNullOrEmpty(j.Integration.ExternalReferenceId)
                    group j by j.Integration.ExternalReferenceId into g
                    select new {g.Key, Ids = g.Select(i => i.Id).ToList()}).ToDictionary(r => r.Key, r => r.Ids);
        }

        private static List<Guid> FilterJobAdIds(IEnumerable<string> externalReferenceIds, IDictionary<string, List<Guid>> existingJobAdIds, Report report)
        {
            var ids = new List<Guid>();
            foreach (var externalReferenceId in externalReferenceIds)
            {
                List<Guid> idsToAdd;
                if (existingJobAdIds.TryGetValue(externalReferenceId, out idsToAdd))
                    ids.AddRange(idsToAdd);
                else
                    report.Errors.Add(string.Format("There is no job ad with external reference ID '{0}' for the specified employer/integrator combination.", externalReferenceId));
            }

            return ids;
        }

        private void ProcessJobAd(Guid integratorUserId, IEmployer jobPoster, JobAd jobAd, PostReport report)
        {
            jobAd.Integration.IntegratorUserId = integratorUserId;

            // Check whether it already exists.

            if (string.IsNullOrEmpty(jobAd.Integration.ExternalReferenceId))
            {
                // Nothing to check against so just create a new job ad.

                CreateJobAd(jobPoster, jobAd, report);
            }
            else
            {
                // Look for an existing job ad.

                var existingJobAd = GetExistingJobAd(jobPoster.Id, jobAd);
                if (existingJobAd != null)
                {
                    // Update it.

                    UpdateJobAd(jobPoster, jobAd, existingJobAd, report);
                }
                else
                {
                    // Create it.

                    CreateJobAd(jobPoster, jobAd, report);
                }
            }
        }

        private JobAd GetExistingJobAd(Guid jobPosterId, JobAdEntry jobAd)
        {
            var existingJobAd = _externalJobAdsCommand.GetExistingJobAd(jobAd.Integration.IntegratorUserId.Value, jobPosterId, jobAd.Integration.ExternalReferenceId);
            if (existingJobAd == null)
                return null;
            
            // Must be either a draft or open job ad.

            switch (jobAd.Status)
            {
                case JobAdStatus.Draft:

                    switch (existingJobAd.Status)
                    {
                        case JobAdStatus.Draft:
                            return existingJobAd;

                        case JobAdStatus.Open:

                            // Want to turn an open job ad into a draft one.  Close the original so a new draft version can be created.

                            _jobAdsCommand.CloseJobAd(existingJobAd);
                            return null;

                        default:
                            return null;
                    }

                case JobAdStatus.Open:

                    switch (existingJobAd.Status)
                    {
                        case JobAdStatus.Draft:
                        case JobAdStatus.Open:
                        case JobAdStatus.Closed:
                            return existingJobAd;

                        default:
                            return null;
                    }

                default:
                    return null;
            }
        }

        private void CreateJobAd(IEmployer jobPoster, JobAd jobAd, PostReport report)
        {
            try
            {
                if (!CleanJobAd(jobAd, report))
                    return;
                
                // Need to check whether the same job ad has come in through any other integrator.

                if (!_externalJobAdsCommand.CanCreateJobAd(jobAd))
                    return;
                
                // Create the job ad.

                var status = jobAd.Status;
                jobAd.Features = _jobAdIntegrationQuery.GetDefaultFeatures();
                jobAd.FeatureBoost = _jobAdIntegrationQuery.GetDefaultFeatureBoost();
                _employerJobAdsCommand.CreateJobAd(jobPoster, jobAd);

                report.ProcessedJobAdIds.Add(jobAd.Id);

                // Only open those that require opening.

                if (status == JobAdStatus.Open)
                    _employerJobAdsCommand.OpenJobAd(jobPoster, jobAd, false);

                report.Posted++;
            }
            catch (Exception ex)
            {
                AddErrors(ex, report);
                report.Failed++;
            }
        }

        private void UpdateJobAd(IEmployer jobPoster, JobAd jobAd, JobAd existingJobAd, PostReport report)
        {
            try
            {
                report.ProcessedJobAdIds.Add(existingJobAd.Id);

                if (CleanJobAd(jobAd, report))
                {
                    CopyTo(jobAd, existingJobAd);
                    _employerJobAdsCommand.UpdateJobAd(jobPoster, existingJobAd);

                    // Make sure the job is open if needed.

                    if (jobAd.Status == JobAdStatus.Open)
                        _employerJobAdsCommand.OpenJobAd(jobPoster, existingJobAd, false);

                    report.Updated++;
                }
            }
            catch (UserException ex)
            {
                AddErrors(ex, report);
                report.Failed++;
            }
        }

        private static void CopyTo(JobAd jobAd, JobAd existingJobAd)
        {
            // Copy all the properties that are set by the caller when posting a job ad and leave the rest.

            existingJobAd.Title = jobAd.Title;
            existingJobAd.Integration.ExternalApplyUrl = jobAd.Integration.ExternalApplyUrl;
            existingJobAd.Integration.ExternalReferenceId = jobAd.Integration.ExternalReferenceId;
            existingJobAd.Integration.IntegratorReferenceId = jobAd.Integration.IntegratorReferenceId;
            existingJobAd.Integration.JobBoardId = jobAd.Integration.JobBoardId;
            existingJobAd.Integration.ApplicationRequirements = jobAd.Integration.ApplicationRequirements == null
                ? null
                : new ApplicationRequirements
                {
                    IncludeResume = jobAd.Integration.ApplicationRequirements.IncludeResume,
                    IncludeCoverLetter = jobAd.Integration.ApplicationRequirements.IncludeCoverLetter,
                    Questions = Copy(jobAd.Integration.ApplicationRequirements.Questions),
                };

            if (jobAd.ExpiryTime != null)
                existingJobAd.ExpiryTime = jobAd.ExpiryTime;

            existingJobAd.Description.CompanyName = jobAd.Description.CompanyName;
            existingJobAd.Description.BulletPoints = jobAd.Description.BulletPoints == null ? null : MiscUtils.Clone(jobAd.Description.BulletPoints.ToArray());
            existingJobAd.Description.Content = jobAd.Description.Content;
            existingJobAd.Description.JobTypes = jobAd.Description.JobTypes;
            existingJobAd.Description.Package = jobAd.Description.Package;
            existingJobAd.Description.PositionTitle = jobAd.Description.PositionTitle;
            existingJobAd.Description.ResidencyRequired = jobAd.Description.ResidencyRequired;
            existingJobAd.Description.Salary = jobAd.Description.Salary == null ? null : jobAd.Description.Salary.Clone();
            existingJobAd.Description.Summary = jobAd.Description.Summary;

            existingJobAd.Description.Location = jobAd.Description.Location == null ? null : jobAd.Description.Location.Clone();
            existingJobAd.Description.Industries = jobAd.Description.Industries == null ? null : new List<Industry>(jobAd.Description.Industries);

            existingJobAd.ContactDetails = jobAd.ContactDetails == null
                ? null
                : new ContactDetails
                {
                    FirstName = jobAd.ContactDetails.FirstName,
                    LastName = jobAd.ContactDetails.LastName,
                    CompanyName = jobAd.ContactDetails.CompanyName,
                    EmailAddress = jobAd.ContactDetails.EmailAddress,
                    FaxNumber = jobAd.ContactDetails.FaxNumber,
                    PhoneNumber = jobAd.ContactDetails.PhoneNumber
                };
        }

        private static IList<ApplicationQuestion> Copy(IEnumerable<ApplicationQuestion> questions)
        {
            if (questions == null)
                return null;
            return (from q in questions select Copy(q)).ToList();
        }

        private static ApplicationQuestion Copy(ApplicationQuestion question)
        {
            var multipleChoiceQuestion = question as MultipleChoiceQuestion;
            if (multipleChoiceQuestion != null)
            {
                return new MultipleChoiceQuestion
                {
                    Id = multipleChoiceQuestion.Id,
                    FormatId = multipleChoiceQuestion.FormatId,
                    Text = multipleChoiceQuestion.Text,
                    IsRequired = multipleChoiceQuestion.IsRequired,
                    Answers = Copy(multipleChoiceQuestion.Answers),
                };
            }

            return new TextQuestion
            {
                Id = question.Id,
                FormatId = question.FormatId,
                Text = question.Text,
                IsRequired = question.IsRequired,
            };
        }

        private static IList<MultipleChoiceQuestionAnswer> Copy(IEnumerable<MultipleChoiceQuestionAnswer> answers)
        {
            if (answers == null)
                return null;
            return (from a in answers select Copy(a)).ToList();
        }

        private static MultipleChoiceQuestionAnswer Copy(MultipleChoiceQuestionAnswer answer)
        {
            return new MultipleChoiceQuestionAnswer
            {
                Value = answer.Value,
                Text = answer.Text,
            };
        }

        private static void AddErrors(Exception ex, Report report)
        {
            var formatter = (IErrorHandler)new StandardErrorHandler();
            var exception = ex as ValidationErrorsException;
            if (exception != null)
            {
                foreach (var error in exception.Errors)
                    report.Errors.Add(formatter.FormatErrorMessage(error));
            }
            else
            {
                var userException = ex as UserException;
                report.Errors.Add(userException != null ? formatter.FormatErrorMessage(userException) : ex.Message);
            }
        }

        private static bool CleanJobAd(JobAd jobAd, PostReport report)
        {
            if (jobAd == null)
                throw new ArgumentNullException("jobAd");

            // Strip marquees, blinking text, etc.

            try
            {
                jobAd.Description.Summary = jobAd.Description.Summary.FixExtraTextAndTrim(true, false);
                jobAd.Description.Content = jobAd.Description.Content.FixExtraTextAndTrim(false, false);
            }
            catch (Exception)
            {
                var error = string.Format("Failed to strip extra text from job ad '{0}'" + " (external reference ID '{1}').", jobAd.Title, jobAd.Integration.ExternalReferenceId);
                report.Errors.Add(error);
                report.Failed++;
                return false;
            }

            return true; // OK
        }
    }
}
