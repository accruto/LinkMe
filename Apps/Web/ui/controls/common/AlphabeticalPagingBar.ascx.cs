using System;
using System.Web.UI;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Members.Friends;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class AlphabeticalPagingBar : UserControl
	{
		// Parameters set by the parent page.
        private string nameStartsWith;

        public string NameStartsWith
        {
            get { return nameStartsWith; }
            set { nameStartsWith = value; }
        }

        protected string GetPageLink(string name)
		{
            string styleForActiveLink = string.Empty;
            if(!String.IsNullOrEmpty(nameStartsWith))
            {
                if(name.ToLower() == nameStartsWith.ToLower())
                    styleForActiveLink = " style=\"text-decoration:none;color:black;font-weight:bold;\"";
            }
                
            return string.Format("<a href=\"{0}\"{1}>{2}</a>", GetPageUrl(name), styleForActiveLink, name);
		}

		private Url GetPageUrl(string name)
		{
            Url url = new Url(Request.Url);
            url.QueryString[ViewFriends.NameParameter] = name;
            
            return url;
		}
    }
}