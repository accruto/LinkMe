using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LinkMe.WebControls
{

    [ToolboxData("<{0}:Loader runat=server></{0}:Loader>")]
    [PersistChildren(true), ParseChildren(false)]

    public class Loader : WebControl
    {
        private string controlToHide;
        private string message;
        private bool displayMessage = true;

        private string ApplicationPath
		{
            get { return (Page.Request.ApplicationPath == "/" ? "" : Page.Request.ApplicationPath); }
		}

        protected override void Render(HtmlTextWriter writer)
        {
            HtmlControl hCtrl = FindControlToHide();
            if (hCtrl != null)
            {
                HideControlInHtml(hCtrl);

                string uniqueId = "divLoading" + Guid.NewGuid().ToString("n");
                //Render div
                
                if (DisplayMessage)
                    writer.Write(@"
                    <div id=""" + uniqueId + @""" style=""text-align:center; height: 250px; padding-top: 100px;"">
                        " + Message + @"<br /><br />
	                    <img src="" " + ApplicationPath + @"/ui/images/universal/loading3.gif"" alt=""Loading..."" />
                    </div>
                    ");

                //Render original contents
                base.Render(writer);

                //Hook up to window.onload
                writer.Write(@"
                <script type=""text/javascript"">
                    Event.observe(window, 'load', function() {
                        " + (DisplayMessage ? @"$('" + uniqueId + @"').hide(); " : "") + @"
                        $('" + hCtrl.ClientID + @"').show();                        
                        //This is to re-position the browser to anchor specified in the URL when
                        //loading of the contents within finishes.
                        if (document.location.hash)
                            document.location = document.location;
                        //Global handler for any post-load actions
                        if(LinkMeUI.Editor.LoaderGlobalCallback) 
                            LinkMeUI.Editor.LoaderGlobalCallback();                            
                    });
                    
                </script>
                ");
            } else {
                base.Render(writer);
            }
        }

        private static void HideControlInHtml(HtmlControl hCtrl)
        {
            StringBuilder styleB = new StringBuilder();
            //Closing last style definition, if any 
            string style = (hCtrl.Attributes["style"] ?? "").Trim();
            if (!String.IsNullOrEmpty(style))
            {
                styleB.Append(style);
                
                if(style.Substring(style.Length - 1, 1) != ";") 
                    styleB.Append("; "); 
            }

            //Hiding
            styleB.Append("display: none;");
            hCtrl.Attributes["style"] = styleB.ToString();
        }

        private HtmlControl FindControlToHide()
        {
            if(!String.IsNullOrEmpty(ControlToHide))
            {
                foreach (Control ctrl in Controls)
                {
                    HtmlControl wCtrl = ctrl as HtmlControl;
                    if(wCtrl != null && wCtrl.ID == ControlToHide)
                    {
                        return wCtrl;
                    }
                }
            }
            return null;
        }

        public string ControlToHide
        {
            get { return controlToHide; }
            set { controlToHide = value; }
        }

        public bool DisplayMessage
        {
            get { return displayMessage; }
            set { displayMessage = value; }
        }

        public string Message
        {
            get { return message ?? "Loading..."; }
            set { message = value; }
        }
    }
}
