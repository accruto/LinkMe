using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.UI
{
    public class SecondaryForm
        : HtmlContainerControl
    {
        private string action = "";
        private string enctype = "";
        private string method = "post";
        private string target = "";

        // Methods

        protected override void OnInit(EventArgs e)
        {
            if (!(base.Page is Page))
                throw new Exception("SecondaryForm must be used in a LinkMe.Apps.Asp.UI.Page");
            base.OnInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            // Validators are not supported.

            CheckValidators(this);
        }

        protected override void RenderAttributes(HtmlTextWriter writer)
        {
            if (ID == null)
                ID = ClientID;
            base.RenderAttributes(writer);

            // Add form attributes.

            writer.WriteAttribute("name", ClientID);
            writer.WriteAttribute("method", Method);
            if (string.IsNullOrEmpty(Action))
                writer.WriteAttribute("action", GetClientUrl().ToString(), true);
            else
                writer.WriteAttribute("action", ResolveUrl(Action), true);

            if (!string.IsNullOrEmpty(EncType))
                writer.WriteAttribute("enctype", EncType);

            if (!string.IsNullOrEmpty(Target))
                writer.WriteAttribute("target", Target);
        }

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();

            // Render the children as normal.

            Page.InServerForm = true;
            base.RenderChildren(new HtmlTextWriter(new StringWriter(sb)));
            Page.InServerForm = false;

            // Replace any postback rendered by children with own postback function.

            bool doPostBack = sb.ToString().Contains("__doPostBack");
            sb.Replace("__doPostBack", "doPostBack_" + ClientID);
            writer.WriteLine();
            writer.WriteLine("<input type='hidden' id='LinkMe.SecondaryForm' name='LinkMe.SecondaryForm' value='" + ClientID + "' />");
            if (doPostBack)
            {
                writer.WriteLine("<input type='hidden' name='__EVENTTARGET' value='' />");
                writer.WriteLine("<input type='hidden' name='__EVENTARGUMENT' value='' />");
                writer.WriteLine("<script type='text/javascript'>");
                writer.WriteLine("<!--");
                writer.WriteLine("\tfunction doPostBack_" + ClientID + "(eventTarget, eventArgument) {");
                writer.WriteLine("\t\tvar secondaryForm = document." + ClientID + ";");
                writer.WriteLine("\t\tsecondaryForm.__EVENTTARGET.value = eventTarget;");
                writer.WriteLine("\t\tsecondaryForm.__EVENTARGUMENT.value = eventArgument;");
                writer.WriteLine("\t\tsecondaryForm.submit();");
                writer.WriteLine("\t}");
                writer.WriteLine("//-->");
                writer.WriteLine("</script>");
            }
            writer.Write(sb.ToString());
        }

        public string Action
        {
            get { return action; }
            set { action = value; }
        }

        [Browsable(false)]
        public override bool EnableViewState
        {
            get { return false; }
        }

        public string EncType
        {
            get { return enctype; }
            set { enctype = value; }
        }

        public bool IsPostBack
        {
            get
            {
                if (Context == null)
                    return false;
                else
                    return Context.Request.Form["LinkMe.SecondaryForm"] == ClientID;
            }
        }

        public string Method
        {
            get { return method; }
            set { method = value; }
        }

        [Browsable(false)]
        public override string TagName
        {
            get { return "form"; }
        }

        public string Target
        {
            get { return target; }
            set { target = value; }
        }

        private static void CheckValidators(Control control)
        {
            foreach (Control child in control.Controls)
            {
                if (child is BaseValidator)
                    throw new Exception("Validator Controls not supported in a SecondaryForm.");
                CheckValidators(child);
            }
        }

        private new Page Page
        {
            get { return (Page)base.Page; }
        }

        private ReadOnlyUrl GetClientUrl()
        {
            return Page.GetClientUrl();
        }
    }
}