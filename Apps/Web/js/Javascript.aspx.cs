using System;
using System.Reflection;
using LinkMe.Apps.Asp.Content;
using LinkMe.Domain.Contacts;
using LinkMe.Environment;
using LinkMe.Web.UI;

namespace LinkMe.Web.Js
{
    public partial class Javascript
        : LinkMePage
    {
        protected override UserType[] AuthorizedUserTypes
        {
            get { return null; }
        }

        protected override bool GetRequiresActivation()
        {
            return false;
        }

        protected override UserType GetActiveUserType()
        {
            return UserType.Member;
        }

        protected static ContentUrl _contentUrl = new ContentUrl(StaticEnvironment.GetFileVersion(Assembly.GetExecutingAssembly()), "~/content/");
    }
}
