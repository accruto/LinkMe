using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Recruiters;

namespace LinkMe.Web.Areas.Administrators.Models.Organisations
{
    public class CommunicationsModel
    {
        public Organisation Organisation { get; set; }
        public IList<Tuple<Category, Frequency?>> Categories { get; set; }
    }
}
