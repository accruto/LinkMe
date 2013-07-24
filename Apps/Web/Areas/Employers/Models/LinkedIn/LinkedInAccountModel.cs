using System.Collections.Generic;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Industries;

namespace LinkMe.Web.Areas.Employers.Models.LinkedIn
{
    public class LinkedInAccountModel
    {
        public Login Login { get; set; }
        public LinkedInJoin Join { get; set; }
        public bool AcceptTerms { get; set; }
        public IList<Industry> Industries { get; set; }
    }
}