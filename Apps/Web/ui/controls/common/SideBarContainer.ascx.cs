using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using LinkMe.Utility.Utilities;
using LinkMe.WebControls;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class SideBarContainer : LinkMeUserControl
    {
        private class SidebarComponent
        {
            public SidebarComponent(string title, MakeControl makeControl)
            {
                _title = title;
                _makeControl = makeControl;
            }

            private readonly string _title;
            private readonly MakeControl _makeControl;

            public string Title
            {
                get { return _title; }
            }

            public MakeControl MakeControl
            {
                get { return _makeControl; }
            }
        }

        private delegate UserControl MakeControl(SideBarContainer container);
        private static readonly Dictionary<string, MakeControl> Components = BuildComponents();

        private static readonly CachingFileLoader<Dictionary<string, IList<SidebarComponent>>> SidebarMap =
            new CachingFileLoader<Dictionary<string, IList<SidebarComponent>>>("~/config/linkme.sidebars.xml", LoadSidebarMap);

        private static MakeControl Make(string ascx)
        {
            return container => (UserControl) container.LoadControl(ascx);
        }
        private static MakeControl Info(InfoControl.InfoTopic topic)
        {
            return delegate(SideBarContainer container)
                       {
                           var ctl = (InfoControl) container.LoadControl("InfoControl.ascx");
                           ctl.Topic = topic;
                           return ctl;
                       };
        }

        private static Dictionary<string, MakeControl> BuildComponents()
        {
            return new Dictionary<string, MakeControl>
            {
                {"member-notify", Make("~/ui/controls/networkers/Notifications.ascx")},
                {"member-search", Make("~/ui/controls/networkers/MiniFindFriends.ascx")},
                {"employer-subscriptions", Make("~/ui/controls/common/ViewCreditsExpiry.ascx")},
                {"employer-access", Make("~/ui/controls/employers/CandidateAccess.ascx")},
                {"member-log-in", Make("~/ui/controls/networkers/MemberSideBarLogin.ascx")},
                {"employer-log-in", Make("~/ui/controls/common/SideBarLogin.ascx")},
                {"job-ad-writing-advice", Info(InfoControl.InfoTopic.job_ad_writing_advice)},
                {"job-ad-title-advice", Info(InfoControl.InfoTopic.job_ad_title_advice)},
                {"job-ad-salary-advice", Info(InfoControl.InfoTopic.job_ad_salary_advice)},
                {"what-is-shown-to-job-hunters", Info(InfoControl.InfoTopic.what_is_shown_to_job_hunters)},
                {"employer-courtesy", Info(InfoControl.InfoTopic.employer_courtesy)},
                {"join-privacy", Info(InfoControl.InfoTopic.join_privacy)},
                {"join-details", Info(InfoControl.InfoTopic.join_details)},
                {"member-join", Info(InfoControl.InfoTopic.member_join)},
                {"employer-join", Info(InfoControl.InfoTopic.employer_join)},
                {"employer-folders", Make("~/ui/controls/employers/SideBarFolders.ascx")},
                {"employer-folder-search", Make("~/ui/controls/employers/SideBarFolderSearch.ascx")},
                {"editable-section", Make("~/ui/controls/common/EditableMemberHomeSidebarSection.ascx")},
                {"member-have-your-say", Make("~/ui/controls/networkers/MemberHaveYourSay.ascx")},
                {"adsense", Make("~/ui/controls/common/SidebarAdSense.ascx")},
                {"adsense2", Make("~/views/shared/GoogleJobsAdSense2.ascx")}
            };
        }

        public bool AllowGrabbingFocus { get; set; }

        private static Dictionary<string, IList<SidebarComponent>> LoadSidebarMap(Stream file)
        {
            var dict = new Dictionary<string, IList<SidebarComponent>>();

            var doc = new XmlDocument();
            doc.Load(file);
            foreach (XmlNode sidebarNode in doc.SelectNodes("/sidebars/sidebar"))
            {
                var sidebarComponents = new List<SidebarComponent>();
                foreach (XmlNode componentNode in sidebarNode.SelectNodes("components/component"))
                {
                    string title = null;
                    if (componentNode.Attributes["title"] != null)
                        title = componentNode.Attributes["title"].Value;

                    MakeControl makeControl;
                    try
                    {
                        makeControl = Components[componentNode.InnerText];
                    }
                    catch (KeyNotFoundException)
                    {
                        throw new ApplicationException("There is no sidebar component '"
                            + componentNode.InnerText + "' defined in SideBarContainer.BuildComponents().");
                    }

                    sidebarComponents.Add(new SidebarComponent(title, makeControl));
                }
                dict.Add(sidebarNode.Attributes["id"].InnerText, sidebarComponents);
            }

            return dict;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (CurrentSiteMapNode != null)
            {
                string activeSidebar = CurrentSiteMapNode.GetActiveValue("sidebar", LoggedInUserRoles);
                if (!string.IsNullOrEmpty(activeSidebar))
                {
                    foreach (SidebarComponent sidebarComponent in SidebarMap.Value[activeSidebar])
                    {
                        // Create the control first.

                        UserControl control = sidebarComponent.MakeControl(this);

                        // Create a placeholder with the control wrapped in 2 literals.

                        var placeHolderControl = (PlaceHolder) LoadControl(typeof(PlaceHolder), null);

                        var literalControl = (LiteralControl) LoadControl(typeof(LiteralControl), null);
                        literalControl.Text = GetHeaderText(sidebarComponent.Title);
                        placeHolderControl.Controls.Add(literalControl);

                        placeHolderControl.Controls.Add(control);

                        literalControl = (LiteralControl) LoadControl(typeof(LiteralControl), null);
                        literalControl.Text = GetFooterText();

                        placeHolderControl.Controls.Add(literalControl);
                        Controls.Add(placeHolderControl);
                    }
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // After all the controls are loaded give them a chance to indicate whether they in fact want
            // to be shown, or to update the section title.

            foreach (PlaceHolder placeHolder in Controls)
            {
                Control sideBarControl = placeHolder.Controls[1];

                // Determine whether the control supports the section control interface.
                
                var sectionControl = sideBarControl as ISectionControl;
                if (sectionControl != null)
                {
                    if (!sectionControl.ShowSection)
                    {
                        // Hide the placeholder which will hide the control.

                        placeHolder.Visible = false;
                    }
                    else 
                    {
                        string title = sectionControl.SectionTitle;
                        if (!string.IsNullOrEmpty(title))
                        {
                            // Update the header.

                            ((LiteralControl) placeHolder.Controls[0]).Text = GetHeaderText(title);
                        }
                    }
                }

                // If the control can grab focus tell it whether it's allowed to do so on this page.

                var canGrabFocus = sideBarControl as ICanGrabFocus;
                if (canGrabFocus != null)
                {
                    canGrabFocus.AllowGrabbingFocus = AllowGrabbingFocus;
                }
            }
        }

        private static string GetHeaderText(string title)
        {
            var sb = new StringBuilder();
            sb.Append("<div class=\"sidebar-section-title\"><h2>")
                .Append(title)
                .AppendLine("</h2></div>")
                .Append("<div class=\"sidebar-section-content\">");
            return sb.ToString();
        }

        private static string GetFooterText()
        {
            return "</div>";
        }
    }
}
