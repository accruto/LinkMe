using System;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class RequestContentRemovalLink : LinkMeUserControl
    {
        private string _text = "Report";
        private string cssClass = string.Empty;

        private Guid _id;

        public void Display(Guid id)
        {
            _id = id;
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public string CssClass
        {
            get { return cssClass; }
            set { cssClass = value; }
        }

        public string LoginRedirectUrl { get; set; }

        protected string GetActionJs()
        {
            if (string.IsNullOrEmpty(LoginRedirectUrl))
                return string.Format("userContent.ShowRemovalRequestPopup('{0}');", _id);

            return string.Format("window.location = '{0}';", LoginRedirectUrl);
        }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            AddJavaScriptReference(JavaScripts.OverlayPopup);
            AddJavaScriptReference(JavaScripts.UserContent);
        }
    }
}