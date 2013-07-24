using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace LinkMe.WebControls
{
    public class ExplicitClientIdHtmlControl
        : HtmlContainerControl
    {
        private string id;
        private string tag;
        private string cssClass;

        public ExplicitClientIdHtmlControl()
        {
            tag = "span";
        }

        public ExplicitClientIdHtmlControl(string tag)
        {
            this.tag = tag;
        }

        public override string TagName
        {
            get { return tag; }
        }

        public string Tag
        {
            get { return tag; }
            set { tag = value; }
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

        public string CssClass
        {
            get { return cssClass; }
            set { cssClass = value; }
        }

        protected override void RenderAttributes(HtmlTextWriter writer)
        {
            Attributes.Render(writer);

            // ID

            if (ClientID != null)
                writer.WriteAttribute("id", ClientID);

            if (!string.IsNullOrEmpty(CssClass))
                writer.WriteAttribute("class", CssClass);
        }
    }
}
