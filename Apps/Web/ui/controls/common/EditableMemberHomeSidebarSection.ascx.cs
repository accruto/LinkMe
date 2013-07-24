using System;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Framework.Content;
using LinkMe.Framework.Content.ContentItems;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Web.Domain.Roles.Affiliations.Communities;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class EditableMemberHomeSidebarSection
        : LinkMeUserControl, ISectionControl
    {
        private readonly ICommunitiesQuery _communitiesQuery = Container.Current.Resolve<ICommunitiesQuery>();

        private SectionContentItem _item;
        public static readonly string ContentSectionName = "Member sidebar section";

		#region Page Actions

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            var contentEngine = Container.Current.Resolve<IContentEngine>();
            var currentCommunity = _communitiesQuery.GetCurrentCommunity();

            if(currentCommunity != null)
                _item = contentEngine.GetContentItem<SectionContentItem>(ContentSectionName, currentCommunity.Id);

            cvSidebarSection.ItemName = ContentSectionName;
            cvSidebarSection.TemplateUrl = "~/cms/ContentTemplates/SidebarSectionContentTemplate.ascx";
        }

        #endregion

        #region ISectionControl Members

        public bool ShowSection
        {
            get { return _item != null; }
        }

        public string SectionTitle
        {
            get { return _item.SectionTitle; }
        }

        #endregion
    }
}