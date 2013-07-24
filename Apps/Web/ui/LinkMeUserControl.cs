using System;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Asp.Content;
using LinkMe.Apps.Asp.Notifications;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Context;
using LinkMe.WebControls;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Asp.Security;

namespace LinkMe.Web.UI
{
	public abstract class LinkMeUserControl
        : Apps.Asp.UI.UserControl
	{
        protected static readonly IAuthenticationManager _authenticationManager = Container.Current.Resolve<IAuthenticationManager>();

	    public string ApplicationPath
		{
			get { return (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath); }
		}

        public short TabIndexOffset
        {
            set
            {
                SetTabIndexOffset(Controls, value);
            }
        }

	    protected Guid? LoggedInUserId
        {
            get { return Context.User.Id(); }
        }

        protected RegisteredUser LoggedInUser
		{
            get { return _authenticationManager.GetUser(new HttpContextWrapper(Context)); }
		}

        protected UserType LoggedInUserRoles
        {
            get { return Context.User.UserType(); }
        }

	    protected bool IsLoggedInMember
	    {
            get { return LoggedInMemberId != null; }
	    }

        protected Member LoggedInMember
        {
            get { return (LoggedInUser as Member); }
        }

        protected Employer LoggedInEmployer
        {
            get { return (LoggedInUser as Employer); }
        }
        
        protected Administrator LoggedInAdministrator
		{
			get { return (LoggedInUser as Administrator); }
		}

	    protected AnonymousUserContext AnonymousUserContext
	    {
	        get { return new AnonymousUserContext(HttpContext.Current); }
	    }

		protected LinkMePage LinkMePage
		{
			get
			{
				var page = Page as LinkMePage;

				if (page == null)
				{
					Debug.Assert(Page != null, "Page != null");
					throw new ApplicationException(string.Format("The parent page of control '{0}' is"
						+ " '{1}', which does not derive from '{2}'.", GetType().BaseType.FullName,
						Page.GetType().BaseType.FullName, typeof(LinkMePage).Name));
				}

				return page;
			}
		}

        protected NavigationSiteMapNode CurrentSiteMapNode
        {
            get { return LinkMePage.CurrentSiteMapNode; }
        }

		protected HtmlForm ParentForm
		{
			get { return ControlUtils.GetParentForm(this, true); }
		}

        protected override StateBag ViewState
        {
            get { throw new NotSupportedException("ViewState is disabled."); }
        }

	    protected Guid? LoggedInMemberId
	    {
            get { return LoggedInMember != null ? LoggedInMember.Id : (Guid?) null; }
	    }

        protected void AddStyleSheetReference(StyleSheetReference reference)
        {
            LinkMePage.AddStyleSheetReference(reference);
        }

        protected void AddJavaScriptReference(JavaScriptReference reference)
        {
            LinkMePage.AddJavaScriptReference(reference);
        }

        protected ReadOnlyUrl SetNotification(ReadOnlyUrl url, NotificationType notificationType, string message)
        {
            return LinkMePage.SetNotification(url, notificationType, message);
        }

        protected void RedirectWithNotification(ReadOnlyUrl url, NotificationType notificationType, string message)
        {
            LinkMePage.RedirectWithNotification(url, notificationType, message);
        }

        protected ReadOnlyUrl GetClientUrl()
        {
            return LinkMePage.GetClientUrl();
        }

	    protected ILocationQuery LocationQuery
	    {
            get { return Container.Current.Resolve<ILocationQuery>(); }
	    }

        private static void SetTabIndexOffset(ControlCollection controls, short value)
        {
            foreach (Control control in controls)
            {
                var webControl = control as WebControl;
                if (webControl != null && webControl.TabIndex != 0)
                {
                    webControl.TabIndex += value;
                }
                else
                {
                    SetTabIndexOffset(control.Controls, value);
                }
            }
        }
    }
}
