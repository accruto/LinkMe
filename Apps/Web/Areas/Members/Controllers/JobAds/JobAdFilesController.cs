using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    public class JobAdFilesController
        : ViewController
    {
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IMemberJobAdFilesQuery _memberJobAdFilesQuery;
        private readonly IFilesQuery _filesQuery;

        public JobAdFilesController(IJobAdsQuery jobAdsQuery, IMemberJobAdFilesQuery memberJobAdFilesQuery, IFilesQuery filesQuery)
        {
            _jobAdsQuery = jobAdsQuery;
            _memberJobAdFilesQuery = memberJobAdFilesQuery;
            _filesQuery = filesQuery;
        }

        public ActionResult Download(JobAdMimeType? mimeType, Guid[] jobAdIds)
        {
            try
            {
                if (jobAdIds.IsNullOrEmpty())
                    throw new NotFoundException("job ad");

                mimeType = GetMimeType(mimeType, jobAdIds);

                var jobAds = _jobAdsQuery.GetJobAds<JobAd>(jobAdIds).ToArray();

                switch (mimeType.Value)
                {
                    case JobAdMimeType.Doc:
                        return DocFile(_memberJobAdFilesQuery.GetJobAdFile(jobAds[0]));

                    default:
                        return ZipFile(_memberJobAdFilesQuery.GetJobAdFile(jobAds));
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View("Error");
        }

        public ActionResult Logo(Guid jobAdId)
        {
            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
            if (jobAd == null || jobAd.LogoId == null || !jobAd.Features.IsFlagSet(JobAdFeatures.Logo))
                return HttpNotFound();

            var fileReference = _filesQuery.GetFileReference(jobAd.LogoId.Value);
            if (fileReference == null || fileReference.FileData.FileType != FileType.CompanyLogo)
                return HttpNotFound();

            // Return the file.

            return File(_filesQuery.OpenFile(fileReference), fileReference.MediaType);
        }

        private static JobAdMimeType GetMimeType(JobAdMimeType? mimeType, ICollection<Guid> jobAdIds)
        {
            if (jobAdIds.Count > 1)
                return JobAdMimeType.Zip;
            return mimeType == null ? JobAdMimeType.Doc : mimeType.Value;
        }
    }
}
