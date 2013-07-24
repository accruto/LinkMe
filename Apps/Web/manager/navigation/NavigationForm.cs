using System;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Apps.Asp.Navigation;
using Page = LinkMe.Apps.Asp.UI.Page;

namespace LinkMe.Web.Manager.Navigation
{
    public class NavigationForm : HtmlForm
    {
        private static readonly MethodInfo _registerDefaultButtonScript = GetRegisterDefaultButtonScript();

        private string name;
        private string id;
        private string actionUrl;

        public override string Name
        {
            get { return string.IsNullOrEmpty(name) ? base.Name : name; }
            set { name = value; }
        }

        public override string ID
        {
            get { return string.IsNullOrEmpty(id) ? base.ID : id; }
            set { id = value; }
        }

        public override string ClientID
        {
            get { return string.IsNullOrEmpty(id) ? base.ClientID : id; }
        }

        public override string UniqueID
        {
            get { return string.IsNullOrEmpty(id) ? base.UniqueID : id; }
        }

        public string ActionUrl
        {
            get { return actionUrl; }
            set { actionUrl = value; }
        }

        protected override void RenderAttributes(HtmlTextWriter writer)
        {
            // name and method

            writer.WriteAttribute("name", Name);
            Attributes.Remove("name");
            writer.WriteAttribute("method", Method);
            Attributes.Remove("method");

            // Action.

            if (string.IsNullOrEmpty(actionUrl))
                writer.WriteAttribute("action", GetClientUrl().PathAndQuery);
            else 
                writer.WriteAttribute("action", actionUrl);
            Attributes.Remove("action");

            // Let the rest of the attributes render themselves.

            Attributes.Render(writer);

            // ID

            if (ClientID != null)
            {
                writer.WriteAttribute("id", ClientID);
            }

            RegisterDefaultButton(writer);
        }

        private new Page Page
        {
            get { return (Page)base.Page; }
        }

        private ReadOnlyUrl GetClientUrl()
        {
            return Page.GetClientUrl();
        }

        private void RegisterDefaultButton(HtmlTextWriter writer)
        {
            // Copied from the base HtmlForm.RenderAttributes() method.

            if (string.IsNullOrEmpty(DefaultButton))
                return;

            HttpRequest request;
            try
            {
                request = Page.Request;
            }
            catch (HttpException)
            {
                return;
            }

            var browser = request.Browser;
            if (browser.EcmaScriptVersion.Major == 0 || browser.W3CDomVersion.Major == 0)
                return;

            Control button = FindControl(DefaultButton);
            if (button == null)
            {
                if (DefaultButton.IndexOfAny(new[] { '$', ':' }) != -1)
                {
                    button = Page.FindControl(DefaultButton);
                }
            }

            if (button == null)
            {
                throw new InvalidOperationException(string.Format("Failed to find default button '{0}' on form '{1}'.",
                    DefaultButton, UniqueID));
            }

            if (!(button is IButtonControl))
            {
                throw new InvalidOperationException(string.Format("The default button, '{0}', on form '{1}' does"
                    + " not derived from IButtonControl.", DefaultButton, UniqueID));
            }

            // Now the big hack - use Reflection to call the internal RegisterDefaultButtonScript() method.

            _registerDefaultButtonScript.Invoke(Page.ClientScript, new object[] { button, writer, false });
        }

        private static MethodInfo GetRegisterDefaultButtonScript()
        {
            var method = typeof(ClientScriptManager).GetMethod("RegisterDefaultButtonScript",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null,
                new[] { typeof(Control), typeof(HtmlTextWriter), typeof(bool) }, null);

            if (method == null)
            {
                throw new ApplicationException("Failed to find the ClientScriptManager.RegisterDefaultButtonScript()"
                    + " internal method using Reflection.");
            }

            return method;
        }
    }
}
