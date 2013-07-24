using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Management.Areas.Communications.Controllers;
using LinkMe.Apps.Management.Areas.Communications.Models;

namespace LinkMe.Apps.Management.Areas.Communications
{
    public class CommunicationsAreaRegistration
        : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Communications"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapAreaRoute<CacheController>("communications/cache/clear", c => c.Clear);

            context.MapAreaRoute<MembersController, CommunicationsContext>("communications/definitions/membernewsletteremail", c => c.Newsletter);
            context.MapAreaRoute<MembersController, CommunicationsContext>("communications/definitions/reengagementemail", c => c.Reengagement);
            context.MapAreaRoute<MembersController, CommunicationsContext>("communications/definitions/edmemail", c => c.Edm);
            context.MapAreaRoute<MembersController, Guid>("communications/members/photo/{id}", c => c.Photo);

            context.MapAreaRoute<EmployersController, CommunicationsContext>("communications/definitions/employernewsletteremail", c => c.Newsletter);
            context.MapAreaRoute<EmployersController, CommunicationsContext>("communications/definitions/ioslaunchemail", c => c.IosLaunch);
        }
    }
}
