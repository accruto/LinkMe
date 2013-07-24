using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Apps.Agents.Context;
using LinkMe.Utility.Validation;
using LinkMe.Web.Content;
using LinkMe.Web.Service;
using LinkMe.Common;

namespace LinkMe.Web.UI.Controls.Common
{
    [ValidationProperty("Text")]
    public partial class Location
        : LinkMeUserControl
    {
        private const string NamedLocationDefaultMessage = "e.g. Melbourne";
        private const string AddressLocationDefaultMessage = "e.g. Armadale VIC 3143";

        private string _locationParameter = GetSuggestedLocations.LocationParameter;
        private string _countryParameter = GetSuggestedLocations.CountryParameter;
        private string _maximumSuggestionsParameter = GetSuggestedLocations.MaximumParameter;
        private string _methodParameter = GetSuggestedLocations.MethodParameter;
        private int _countryId;
        private int _maximumSuggestions = GetSuggestedLocations.DefaultMaximum;
        private ResolutionMethod _resolutionMethod = ResolutionMethod.Unspecified; // Force user code to specify
        private string _indicatorImagePath = "/ui/images/universal/loading.gif";
        private string _indicatorAltText = string.Empty; // "Working...";
        private LocationCountry _locationCountry;
        private LocationConfirmation _locationConfirmation;
        private string _exampleText = "Melbourne, 3000, Australia.";

        public enum ResolutionMethod
        {
            Unspecified,
            NamedLocation,
            AddressLocation
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStyleSheetReference(StyleSheets.Messaging);

            txtLocation.MaxLength = DomainConstants.LocationDisplayNameMaxLength;
            valTxtLocation.ErrorMessage = string.Format(ValidationErrorMessages.MAX_LENGTH_EXCEEDED_FORMAT, "suburb/state/postode", DomainConstants.LocationDisplayNameMaxLength);

            // Set the default country based on the current request.

            _countryId = ActivityContext.Current.Location.Country.Id;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            AjaxPro.Utility.RegisterTypeForAjax(typeof(ResolveLocation));

            // Construct the javascript to resolve the location when the text field is left.

            txtLocation.Attributes.Add("onblur", GetResolveLocationScript());

            // Set the country ID from the country control, if any.

            if (_locationCountry != null)
            {
                var country = _locationCountry.SelectedValue;
                if (country != null)
                    _countryId = country.Id;
            }

            if (IsPostBack)
            {
                // We need just one more check to make sure the default location isn't actually posted back

                if (txtLocation.Text == GetConfirmationDefaultMessage())
                    txtLocation.Text = String.Empty;
            }
        }

        public string Text
        {
            get { return txtLocation.Text; }
            set { txtLocation.Text = value; }
        }

        public short TabIndex
        {
            get { return txtLocation.TabIndex; }
            set { txtLocation.TabIndex = value; }
        }

        public string LocationParameter
        {
            get { return _locationParameter; }
            set { _locationParameter = value; }
        }

        public string CountryParameter
        {
            get { return _countryParameter; }
            set { _countryParameter = value; }
        }

        public ResolutionMethod Method
        {
            get { return _resolutionMethod; }
            set { _resolutionMethod = value; }
        }

        public int CountryId
        {
            get { return _countryId; }
            set { _countryId = value; }
        }

        public string MaximumSuggestionsParameter
        {
            get { return _maximumSuggestionsParameter; }
            set { _maximumSuggestionsParameter = value; }
        }

        public int MaximumSuggestions
        {
            get { return _maximumSuggestions; }
            set { _maximumSuggestions = value; }
        }

        public string MethodParameter
        {
            get { return _methodParameter; }
            set { _methodParameter = value; }
        }

        public string IndicatorImagePath
        {
            get
            {
                return _indicatorImagePath.StartsWith("/") && ApplicationPath != "/"
                    ? ApplicationPath + _indicatorImagePath
                    : _indicatorImagePath;
            }
            set
            {
                _indicatorImagePath = value;
            }
        }

        public string IndicatorAltText
        {
            get { return _indicatorAltText; }
            set { _indicatorAltText = value; }
        }

        public string CssClass
        {
            get { return txtLocation.CssClass; }
            set { txtLocation.CssClass = value; }
        }

        public string TextBoxClientID
        {
            get { return txtLocation.ClientID; }
        }

        public string OnKeyPress { get; set; }

        internal TextBox TextBox
        {
            get { return txtLocation; }
        }

        private string CountryDropDownClientID
        {
            get { return _locationCountry == null ? string.Empty : _locationCountry.DropDownClientID; }
        }

        public string ExampleText
        {
            get { return _exampleText; }
            set { _exampleText = value; }
        }

        internal void SetLocationCountry(LocationCountry value)
        {
            _locationCountry = value;
        }

        internal void SetLocationConfirmation(LocationConfirmation value)
        {
            _locationConfirmation = value;
        }

        protected internal string SetCountryScript
        {
            get { return _locationCountry == null ? string.Empty : GetSetCountryScript(); }
        }

        internal static string ConfirmUrl
        {
            get { return "javascript:toggleConfirmationDisplay('false')"; }
        }

        internal string RejectUrl
        {
            get { return "javascript:rejectLocation(" + GetElementByIdScript(TextBoxClientID) + ", " + GetElementByIdScript(ConfirmationElementId) + ", " + AutoCompleter + ", '" + GetConfirmationDefaultMessage() + "')"; }
        }

        private string AutoCompleter
        {
            get { return ClientID + "Autocompleter"; }
        }

        private string ConfirmationElementId
        {
            get { return _locationConfirmation == null ? string.Empty : _locationConfirmation.ElementId; }
        }

        public string GetConfirmationDefaultMessage()
        {
            return Method == ResolutionMethod.AddressLocation ? AddressLocationDefaultMessage : NamedLocationDefaultMessage;
        }

        private static string GetElementByIdScript(string id)
        {
            return "document.getElementById('" + id + "')";
        }

        protected string GetResolveLocationScript()
        {
            string method;
            switch (Method)
            {
                case ResolutionMethod.Unspecified:
                    throw new ApplicationException("The location resolution method must be explicitly specified.");

                case ResolutionMethod.NamedLocation:
                    method = "NamedLocation";
                    break;

                case ResolutionMethod.AddressLocation:
                    method = "AddressLocation";
                    break;

                default:
                    throw new ApplicationException("Unexpected resolution method: " + Method);
            }

            // Use setTimeout to avoid a race condition with Ajax.Autocompleter.

            var sb = new StringBuilder();
            sb.Append("setTimeout(\"resolveLocation(")
                .Append(GetElementByIdScript(TextBoxClientID))
                .Append(" , ")
                .Append(GetElementByIdScript(ConfirmationElementId))
                .Append(", ")
                .Append(AutoCompleter)
                .Append(", '")
                .Append(method)
                .Append("')\", 250);");
            return sb.ToString();
        }

        //private string GetResetConfirmationScript()
        //{
        //    string script = "setLocationConfirmationMessage(" + GetElementByIdScript(ConfirmationElementId) + ", \"\", false);";
        //    if (string.IsNullOrEmpty(onKeyPress))
        //        return "return " + script;
        //    else
        //        return script + onKeyPress;
        //}

        private string GetSetCountryScript()
        {
            return "setLocationParameter(" + AutoCompleter + ", 'country', " + GetElementByIdScript(CountryDropDownClientID) + ".value)";
        }
    }
}
