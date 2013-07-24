using System;
using System.Web.UI;
using LinkMe.Apps.Agents.Context;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Apps.Asp.Context
{
    public class ContextualContent
        : Control, INamingContainer
    {
        private readonly IVerticalsQuery _verticalsQuery = Container.Current.Resolve<IVerticalsQuery>();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var placeHolder = GetActivePlaceHolder();
            if (placeHolder != null)
                placeHolder.Visible = true;
        }

        public object GetContent()
        {
            var placeHolder = GetActivePlaceHolder();
            return placeHolder != null ? placeHolder.GetContent() : null;
        }

        public void SetContent(object content)
        {
            var placeHolder = GetActivePlaceHolder();
            if (placeHolder != null)
                placeHolder.SetContent(content);
        }

        private ContextualPlaceHolder GetActivePlaceHolder()
        {
            Vertical vertical = null;
            string url = null;

            var verticalId = ActivityContext.Current.Vertical.Id;
            if (verticalId != null)
            {
                vertical = _verticalsQuery.GetVertical(verticalId.Value);
                if (vertical != null)
                {
                    // Use the url as identifier.

                    url = vertical.Url;
                }
            }

            // Show that vertical's content.

            ContextualPlaceHolder activePlaceHolder = null;
            ContextualPlaceHolder anyPlaceHolder = null;

            foreach (var control in Controls)
            {
                var placeHolder = control as ContextualPlaceHolder;
                if (placeHolder != null)
                {
                    placeHolder.Visible = false;
                    switch (placeHolder.Vertical)
                    {
                        case "None":

                            if (vertical == null)
                                activePlaceHolder = placeHolder;
                            break;

                        case "Any":
                            anyPlaceHolder = placeHolder;
                            break;

                        default:
                            if (string.Compare(placeHolder.Vertical, url, StringComparison.InvariantCultureIgnoreCase) == 0)
                                activePlaceHolder = placeHolder;
                            break;
                    }
                }
            }

            if (activePlaceHolder == null)
                activePlaceHolder = anyPlaceHolder;

            if (activePlaceHolder != null)
                activePlaceHolder.Visible = true;
            return activePlaceHolder;
        }
    }
}
