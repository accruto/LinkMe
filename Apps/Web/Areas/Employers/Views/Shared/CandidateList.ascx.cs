using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Web.Areas.Employers.Models;
using System;

namespace LinkMe.Web.Areas.Employers.Views.Shared
{
    public class CandidateList
        : ViewUserControl<CandidateListModel>
    {
        protected string GetCanContact(CanContactStatus status)
        {
            return GetCandidateContext(from id in Model.Results.CandidateIds
                                       where Model.Results.Views[id].CanContact() == status
                                       select id);
        }

        protected string GetCannotContact()
        {
            return GetCandidateContext(from id in Model.Results.CandidateIds
                                       let canContact = Model.Results.Views[id].CanContact()
                                       where canContact == CanContactStatus.No || canContact == CanContactStatus.YesIfHadCredit
                                       select id);
        }

        protected string GetCanContactByPhone(CanContactStatus status)
        {
            return GetCandidateContext(from id in Model.Results.CandidateIds
                                       where Model.Results.Views[id].CanContactByPhone() == status
                                       select id);
        }

        protected string GetCannotContactByPhone()
        {
            return GetCandidateContext(from id in Model.Results.CandidateIds
                                       let canContact = Model.Results.Views[id].CanContactByPhone()
                                       where canContact == CanContactStatus.No || canContact == CanContactStatus.YesIfHadCredit
                                       select id);
        }

        protected string GetCanAccessResume(CanContactStatus status)
        {
            return GetCandidateContext(from id in Model.Results.CandidateIds
                                       where Model.Results.Views[id].CanAccessResume() == status
                                       select id);
        }

        protected string GetCannotAccessResume()
        {
            return GetCandidateContext(from id in Model.Results.CandidateIds
                                       let canAccess = Model.Results.Views[id].CanAccessResume()
                                       where canAccess == CanContactStatus.No || canAccess == CanContactStatus.YesIfHadCredit
                                       select id);
        }

        protected string GetCandidateContext(IEnumerable<Guid> ids)
        {
            if (ids.IsNullOrEmpty())
                return "";
            return "\"" + string.Join("\",\"", (from i in ids select i.ToString()).ToArray()) + "\"";
        }
    }
}
