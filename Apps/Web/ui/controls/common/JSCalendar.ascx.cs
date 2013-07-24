using System;
using LinkMe.Framework.Utility;
using LinkMe.Utility.Utilities;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class JSCalendar : LinkMeUserControl
    {
        private DateTime? _date = null;
        private bool _hideOtherCalendars = true;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override void  OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            AddJavaScriptReference(JavaScripts.Scriptaculous);
            AddStyleSheetReference(StyleSheets.Scal);
            if (IsPostBack && !String.IsNullOrEmpty(txtDate.Value))
                    Date = ParseUtil.ParseUserInputDateTime(txtDate.Value, "calendar date");
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            
            txtDate.Value = Date.Date.ToString("yyyy-MM-dd");
        }

        public DateTime Date
        {
            get
            {
                if (_date == null)
                    _date = DateTime.Now;
                return (DateTime) _date;
            }
            set { _date = value;  }
        }

        protected bool IsDateSet
        {
            get { return _date != null;  }
        }

        public bool HideOtherCalendars
        {
            get { return _hideOtherCalendars; }
            set { _hideOtherCalendars = value; }
        }

        private string _onDateSelectedJavascriptCallback;

        public string OnDateSelectedJavascriptCallback
        {
            get { return _onDateSelectedJavascriptCallback; }
            set { _onDateSelectedJavascriptCallback = value; }
        }

        public string InstanceId
        {
            get { return ClientID + "_calendar"; }
        }


    }

}