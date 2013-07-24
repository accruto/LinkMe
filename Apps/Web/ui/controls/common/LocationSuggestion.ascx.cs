using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI.WebControls;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility;
using LinkMe.Utility.Utilities;

namespace LinkMe.Web.UI.Controls.Common
{
    /// <summary>
    /// This control works in two modes: if you set LinkTemplate it will provide hyperlinks for the chosen
    /// location, otherwise choosing a location will do a postback, which raises the LocationChosen event.
    /// </summary>
	public partial class LocationSuggestion : LinkMeUserControl
	{
        public event LocationChosenEventHandler LocationChosen;

        private string _message = string.Empty;
        private string _linkParameter;

		public string Message
		{
			get { return _message; }
			set { _message = value; }
		}

	    protected string LinkParameter
	    {
            get { return _linkParameter; }
	    }

        /// <summary>
        /// Call this overload to display location suggestions as GET links. The template parameter specifies the
        /// query string parameter to populate with the chosen location.
        /// </summary>
        public void DisplaySuggestions(string linkParameter, IList<NamedLocation> suggestions)
        {
            if (string.IsNullOrEmpty(linkParameter))
                throw new ArgumentException("The template link parameter must be specified.", "linkParameter");

            _linkParameter = linkParameter;

            SetSuggestions(suggestions);
        }

        /// <summary>
        /// Call this overload to display location suggestions as POST links. The LocationChosen event handler
        /// is called when a location is chosen.
        /// </summary>
        public void DisplaySuggestions(IList<NamedLocation> suggestions)
        {
            if (LocationChosen == null)
            {
                throw new ApplicationException("You must attach an event handler to the LocationChosen event before"
                    + " calling this overload of DisplaySuggestions().");
            }

            SetSuggestions(suggestions);

            // Store in a hidden field so they can be reloaded on postback.
            hidSuggestionIds.Value = IdUtil.GetIdList<NamedLocation, int>(",", suggestions);
        }

        private void SetSuggestions(IList<NamedLocation> suggestions)
        {
            rptLocations.DataSource = suggestions;
            DataBind();
        }

        protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

            rptLocations.ItemCommand += rptLocalities_ItemCommand;
            rptLocations.ItemDataBound += rptLocalities_ItemDataBound;
		}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack)
            {
                // Now that we don't use viewstate anymore it's up to us to manually save the data between the initial
                // page load and the postback in the rare case that this is needed.
                LoadSavedLocations();
            }
        }

		protected virtual void OnLocationChosen(LocationChosenEventArgs e)
		{
			if (LocationChosen != null)
			{
				LocationChosen(this, e);
			}
		}

        private void rptLocalities_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;

            var namedLocation = (NamedLocation)e.Item.DataItem;
            string displayName = namedLocation.ToString();

            if (string.IsNullOrEmpty(LinkParameter))
            {
                var btnLocation = (LinkButton) e.Item.FindControl("btnLocation");
                Debug.Assert(btnLocation != null, "btnLocation != null");

                btnLocation.CommandArgument = namedLocation.Id.ToString();
                btnLocation.Text = displayName;
            }
            else
            {
                var lnkLocation = (HyperLink)e.Item.FindControl("lnkLocation");
                Debug.Assert(lnkLocation != null, "lnkLocation != null");

                var url = GetClientUrl().AsNonReadOnly();
                url.QueryString[LinkParameter] = namedLocation.ToString();
                lnkLocation.NavigateUrl = url.ToString();
                lnkLocation.Text = displayName;
            }
        }

		private void rptLocalities_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
            NamedLocation location = LocationQuery.GetNamedLocation(int.Parse(e.CommandArgument.ToString()));
            OnLocationChosen(new LocationChosenEventArgs(location));
		}

        private void LoadSavedLocations()
        {
            // Load the locations from the hidden field.

            int[] locationIds = StringUtils.ParseArrayFromString<int>(",", hidSuggestionIds.Value);
            if (!locationIds.IsNullOrEmpty())
            {
                var locations = new NamedLocation[locationIds.Length];
                for (int i = 0; i < locations.Length; i++)
                {
                    locations[i] = LocationQuery.GetNamedLocation(locationIds[i], true);
                }

                rptLocations.DataSource = locations;
                DataBind();
            }
        }
    }

	public delegate void LocationChosenEventHandler(object sender, LocationChosenEventArgs e);

	public class LocationChosenEventArgs : EventArgs
	{
        private readonly NamedLocation _location;

        internal LocationChosenEventArgs(NamedLocation location)
		{
            _location = location;
		}

        public NamedLocation Location
		{
            get { return _location; }
		}
	}
}
