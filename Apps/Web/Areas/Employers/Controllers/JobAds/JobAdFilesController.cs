using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Queries;

namespace LinkMe.Web.Areas.Employers.Controllers.JobAds
{
    public class JobAdFilesController
        : ViewController
    {
        private readonly IFilesQuery _filesQuery;

        public JobAdFilesController(IFilesQuery filesQuery)
        {
            _filesQuery = filesQuery;
        }

        public ActionResult Logo(Guid fileId)
        {
            // Look for the file.
            // No checks are made etc to determine whether the current user uploaded the logo.

            var fileReference = _filesQuery.GetFileReference(fileId);
            if (fileReference == null || fileReference.FileData.FileType != FileType.CompanyLogo)
                return HttpNotFound();

            // Return the file.

            return File(_filesQuery.OpenFile(fileReference), fileReference.MediaType);
        }
    }
}
