using System;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Framework.Content;
using LinkMe.Framework.Content.ContentItems;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.UI.Controls.Employers
{
    public partial class CommunityCandidateImage : LinkMeUserControl
    {
        private Guid? _verticalId;

        private static readonly IVerticalsQuery _verticalsQuery = Container.Current.Resolve<IVerticalsQuery>();
        private static readonly IContentEngine _contentEngine = Container.Current.Resolve<IContentEngine>();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Use the vertical rather then the community because it is subject to being enabled/disabled
            // which determines whether the image shopuld be shown or not.

            if (_verticalId != null)
            {
                var vertical = _verticalsQuery.GetVertical(_verticalId.Value);
                if (vertical != null)
                    cvCandidateImage.Item = _contentEngine.GetContentItem<ImageContentItem>("Candidate logo", vertical.Id);
            }
        }

        public void Initialise(Guid? communityId)
        {
            _verticalId = communityId;
        }
    }
}