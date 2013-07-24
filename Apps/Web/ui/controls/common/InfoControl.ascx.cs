using System.Web.UI;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class InfoControl : LinkMeUserControl
    {
        public enum InfoTopic
        {
            notimpl,
            what_is_shown_to_job_hunters,
            job_ad_title_advice,
            job_ad_writing_advice,
            job_ad_salary_advice,
            interim_member_login,
            interim_employer_login,
            employer_courtesy,
            join_privacy,
            join_details,
            member_join,
            employer_join,
        }

        private InfoTopic _topic;
        public string SidebarClass { get; set; }

        public delegate void ShowControl(InfoControl control);

        public InfoTopic Topic
        {
            get { return _topic; }
            set
            {
                if (_topic != value)
                {
                    _topic = value;
                    var topicId = _topic.ToString();
                    foreach (Control ctl in Controls)
                    {
                        ctl.Visible = (ctl.ID == topicId);
                    }
                }
            }
        }
    }
}
