using System;
using System.Web.Mvc;
using LinkMe.Apps.Api.Areas.Employers.Controllers.Candidates;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Routing;

namespace LinkMe.Apps.Api.Areas.Employers.Routes
{
    public class CandidatesRoutes
    {
        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapAreaRoute<CandidatesApiController, Guid>(1, "employers/candidates/{candidateId}", c => c.Candidate);
            context.MapAreaRoute<CandidatesApiController, Guid>(1, "employers/candidates/{candidateId}/unlock", c => c.Unlock);
            context.MapAreaRoute<CandidatesApiController, Guid>(1, "employers/candidates/{candidateId}/phonenumbers", c => c.PhoneNumbers);

            context.MapAreaRoute<FoldersApiController, Pagination>(1, "employers/candidates/folders/mobile", c => c.MobileCandidates);
            context.MapAreaRoute<FoldersApiController, Guid>(1, "employers/candidates/folders/mobile/{candidateId}", c => c.MobileCandidate);
        }
    }
}
