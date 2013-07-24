using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Web.Content;
using LinkMe.Web.Service;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class WillingnessToRelocate : LinkMeUserControl
    {
        private Candidate _candidate;
        private bool _isEditable = true;
        private bool _labelsOnLeft;
        private bool _renderFieldsetElement = true;

        //This id is used for dividinng items in lists, like --Countries--, --Regions--, --Suburbs--
        protected const int EmptyID = -1;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStyleSheetReference(StyleSheets.WillingnessToRelocate);

            rptrCounties.DataSource = LocationQuery.GetCountries();
            rptrCounties.DataBind();
        }

        public void Display(Candidate candidate)
        {
            _candidate = candidate;
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                SetDetailsFromMemberProfile();
            }
            else
            {
                //Render list on postback depending on the value of hidden field
                RenderTargetList(GetSelectedLocations(_candidate));
            }
            
            RenderEditorPanel();
        }

        private void SetDetailsFromMemberProfile()
        {
            if (_candidate == null)
                return;
            
            UserRelocationPreference = _candidate.RelocationPreference;
            if (_candidate.RelocationPreference != RelocationPreference.No)
            {
                hiddSelectedLocalities.Value = GetStructuredLocations.GetRecords(_candidate.RelocationLocations);
                RenderTargetList(_candidate.RelocationLocations);
            }
        }

        private void RenderTargetList(IEnumerable<LocationReference> locations)
        {
            foreach (var location in locations)
            {
                string record = GetStructuredLocations.GetRecord(null, location.NamedLocation, true, true);
                string display = GetStructuredLocations.GetRecord(null, location.NamedLocation, false, false);
                selTarget.Items.Add(new ListItem(display, record));
            }
        }

        private void RenderEditorPanel()
        {
            if (!_isEditable)
                return;

            bool doShow = UserRelocationPreference != RelocationPreference.No;
            divRelocationArea.Style["display"] = doShow ? "" : "none";

            rptrCounties.DataSource = LocationQuery.GetCountries();
            rptrCounties.DataBind();
        }

        public IList<LocationReference> GetSelectedLocations(Candidate candidate)
        {
            return GetSelectedLocations(candidate, hiddSelectedLocalities.Value); 
        }

        public static IList<LocationReference> GetSelectedLocations(Candidate candidate, string value)
        {
            var locations = new List<LocationReference>();
            if (string.IsNullOrEmpty(value))
                return locations;

            // Parse the records for the named locations.

            IList<NamedLocation> namedLocations = GetStructuredLocations.ParseRecords(value);
            foreach (NamedLocation namedLocation in namedLocations)
                locations.Add(new LocationReference(namedLocation));

            return locations;
        }

        public RelocationPreference UserRelocationPreference
        {
            get
            {
                if (rbYesWilling.Checked)
                    return RelocationPreference.Yes;

                if (rbWouldConsider.Checked)
                    return RelocationPreference.WouldConsider;

                if (rbNotWilling.Checked)
                    return RelocationPreference.No;

                // Bug 8282: No idea how this happens, but sometimes when the "Mark as current" LinkButton
                // is clicked none of the radio buttons are checked! The default is "No", so just return that.

                return RelocationPreference.No;
            }
            set
            {
                switch (value)
                {
                    case RelocationPreference.No:
                        rbNotWilling.Checked = true;
                        rbYesWilling.Checked = false;
                        rbWouldConsider.Checked = false;
                        break;

                    case RelocationPreference.Yes:
                        rbYesWilling.Checked = true;
                        rbNotWilling.Checked = false;
                        rbWouldConsider.Checked = false;
                        break;

                    case RelocationPreference.WouldConsider:
                        rbWouldConsider.Checked = true;
                        rbYesWilling.Checked = false;
                        rbNotWilling.Checked = false;
                        break;

                    default:
                        throw new ApplicationException("Unexpected value of RelocationPreference: " + value);
                }
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (_candidate != null && IsEditable && UserRelocationPreference != RelocationPreference.No)
                args.IsValid = true;

        }

        public string RadioButtonNoClientID
        {
            get { return rbNotWilling.ClientID; }
        }

        public string JSObjectName()
        {
            return ClientID + "wtr";
        }

        public void Update(Candidate candidate)
        {
            candidate.RelocationPreference = UserRelocationPreference;
            if (candidate.RelocationPreference != RelocationPreference.No)
                candidate.RelocationLocations = GetSelectedLocations(candidate);
        }

        public bool IsEditable
        {
            get { return _isEditable; }
            set { _isEditable = value; }
        }

        public bool LabelsOnLeft
        {
            get { return _labelsOnLeft; }
            set { _labelsOnLeft = value; }
        }

        public bool RenderFieldsetElement
        {
            get { return _renderFieldsetElement; }
            set
            {
                _renderFieldsetElement = value;
                phFieldsetOpeningTag.Visible = _renderFieldsetElement;
                phFieldsetClosingTag.Visible = _renderFieldsetElement;
                phFieldset2OpeningTag.Visible = _renderFieldsetElement;
                phFieldset2ClosingTag.Visible = _renderFieldsetElement;
            }
        }
    }
}
