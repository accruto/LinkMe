using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.WebControls
{
    /// <summary>
    /// Button control that submits the form as a GET request, rather than a POST. Only the values of controls
    /// listed in the Inputs property are submitted (not all the controls in the containing form). If JavaScript
    /// is disabled on the client the form is submitted as a POST, but the server then return a redirect to the
    /// same GET URL, so the only downside is an extra round-trip.
    /// </summary>
    public class SubmitAsGetButton : Button
    {
        #region Nested types

        private struct FormField
        {
            public readonly string Name;
            public readonly Control Input;
            public readonly string ClientScript;
            public readonly object ExtraData;

            internal FormField(string name, Control input, string clientScript, object extraData)
            {
                Name = name;
                Input = input;
                ClientScript = clientScript;
                ExtraData = extraData;
            }
        }

        #endregion

        private readonly List<FormField> _fields = new List<FormField>();
        private bool _isDefaultButton = true;

        public void AddInput(string name, bool value)
        {
            AddInputInternal(name, null, null, value.ToString());
        }

        public void AddInput(string name, TextBox input)
        {
            AddInputInternal(name, input, "encodeURIComponent({0}.value)", null);
        }

        public void AddInput(string name, CheckBox input)
        {
            AddInputInternal(name, input, "{0}.checked", null);
        }

        public void AddInput(string name, RadioButtonList input)
        {
            AddInputInternal(name, input, "GetRadioListValue({0})", null);
        }

        public void AddInput(string name, ListBox input, bool asFlags)
        {
            AddInputInternal(name, input, (asFlags ? "GetListBoxFlagsValue({0})" : "GetListBoxValues({0})"), asFlags);
        }

        public void AddInput(string name, DropDownList input)
        {
            AddInputInternal(name, input, "GetDropDownListValue({0})", null);
        }

        public void AddInput(string name, CheckBoxList input)
        {
            AddInputInternal(name, input, "GetCheckBoxListValue({0})", null);
        }

        public void AddInput(string name, HiddenField input)
        {
            AddInputInternal(name, input, "{0}.value", null);
        }

        private void AddInputInternal(string name, Control input, string clientScriptFormat, object extraData)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("The name must be specified.", "name");
            if (input != null)
            {
                Debug.Assert(!string.IsNullOrEmpty(clientScriptFormat), "!string.IsNullOrEmpty(clientScriptFormat)");
                _fields.Add(new FormField(name, input, string.Format(clientScriptFormat, "$('" + input.ClientID + "')"), extraData));
            }
            else
            {
                _fields.Add(new FormField(name, null, "'" + extraData + "'", extraData));
            }
        }

        public override string OnClientClick
        {
            get { return GetSubmitScript(base.OnClientClick); }
        }

        public bool IsDefaultButton
        {
            get { return _isDefaultButton; }
            set { _isDefaultButton = value; }
        }

        #region Static methods

        private static string GetControlValue(Control input, object extraData)
        {
            var textBox = input as TextBox;
            if (textBox != null)
                return textBox.Text;

            var checkBox = input as CheckBox;
            if (checkBox != null)
                return (checkBox.Checked ? "true" : "false");

            var radioList = input as RadioButtonList;
            if (radioList != null)
                return radioList.SelectedValue;

            var listBox = input as ListBox;
            if (listBox != null)
            {
                return (bool) extraData
                    ? ControlUtils.SelectedListItemValuesToInt(listBox.Items).ToString()
                    : ControlUtils.ConcatenateSelectedListItemValues(listBox.Items);
            }

            var dropDownList = input as DropDownList;
            if (dropDownList != null)
                return dropDownList.SelectedItem.Value;

            var checkBoxList = input as CheckBoxList;
            if (checkBoxList != null)
            {
                ListItemCollection boxes = checkBoxList.Items;

                int postedJobValues = 0;

                for (int i = 0; i < boxes.Count; i++)
                {
                    // The enum stores things in power of two,
                    // so for each item we get to, we take its relative position
                    // as a power of two
                    if (boxes[i].Selected)
                        postedJobValues += (int)Math.Pow(2, i);
                }

                return postedJobValues.ToString();
            }

            var hiddenField = input as HiddenField;
            if (hiddenField != null)
                return hiddenField.Value;

            throw new ArgumentException("Unsupported type of input control: " + input.GetType().FullName, "input");
        }

        private static string GetClientNameValuePair(FormField field)
        {
            if(field.Input != null && !field.Input.Visible)
                return String.Empty;
            return "'" + HttpUtility.UrlEncode(field.Name) + "', " + field.ClientScript;
        }

        #endregion

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (CausesValidation && !Page.IsValid)
                return;

            // Redirect to the form submission URL with all the form values, except for empty ones.

            var parameters = new string[_fields.Count * 2];
            for (int i = 0, j = 0; i < _fields.Count; i++, j += 2)
            {
                FormField field = _fields[i];
                parameters[j] = field.Name;
                parameters[j + 1] = field.Input != null
                    ? GetControlValue(field.Input, field.ExtraData).NullIfEmpty()
                    : field.ExtraData.ToString();
            }

            var url = new ReadOnlyApplicationUrl(GetFormUrl(), new QueryString(parameters));
            Page.Response.Redirect(url.ToString());
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // For the onclick script to be executed in IE 6 when the users presses Enter inside the form
            // the button needs to be the default button. (In Firefox it doesn't.)

            if (_isDefaultButton)
            {
                HtmlForm parentForm = ControlUtils.GetParentForm(this, true);
                if (string.IsNullOrEmpty(parentForm.DefaultButton))
                {
                    parentForm.DefaultButton = UniqueID;
                }
                else
                {
                    if (parentForm.DefaultButton != UniqueID)
                    {
                        throw new ApplicationException(string.Format("Form '{0}' contains a {1} '{2}', but the default"
                            + " button has been set to '{3}'. This would prevent the onclick JavaScript from running"
                            + " when the user submits the form by pressing Enter in IE.",
                            parentForm.ClientID, GetType().Name, UniqueID, parentForm.DefaultButton));
                    }
                }
            }
        }

#if DEBUG

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (_isDefaultButton)
            {
                HtmlForm parentForm = ControlUtils.GetParentForm(this, true);
                if (parentForm.DefaultButton != UniqueID)
                {
                    throw new ApplicationException(string.Format("Form '{0}' contains a {1} '{2}', which is not set"
                        + " as the default button (probably because another control overwrote the DefaultButton"
                        + " property). This would prevent the onclick JavaScript from running when the user submits"
                        + " the form by pressing Enter in IE.", parentForm.UniqueID, GetType().Name, UniqueID));
                }
            }
            
        }
#endif
        protected override void Render(HtmlTextWriter writer)
        {
            string appPath = Page.Request.ApplicationPath;
            writer.WriteLine("<script type=\"text/javascript\" src=\"" 
                + (appPath == "/" ? "" : appPath)
                + "/js/SubmitAsGetButton.js\"></script>");
            writer.WriteLine("\n<script type=\"text/javascript\">function {0}(){{\n{1}}}</script>\n",
                GetClientHandleFunctionName(), GetClientOnClickHandleFunctionText());
            base.Render(writer);
        }

        private string GetClientOnClickHandleFunctionText()
        {
            var sb = new StringBuilder();

            string formUrl = GetFormUrl();
            sb.Append("\nif (typeof(WebForm_OnSubmit) != 'function' || WebForm_OnSubmit()) {\n"
                    + "\nvar loc = GetButtonSubmitUrl('");
            // ResolveClientUrl must be called on the page, not the control, as that gives the wrong path.
            sb.Append(HttpUtility.UrlPathEncode(Page.ResolveClientUrl(formUrl)));
            sb.Append("', [");

            if (_fields.Count > 0)
            {
                sb.Append(GetClientNameValuePair(_fields[0]));
                for (int i = 1; i < _fields.Count; i++)
                {
                    string str = GetClientNameValuePair(_fields[i]);
                    if (!String.IsNullOrEmpty(str))
                        sb.AppendFormat(", {0}\n", str);
                }
            }

            sb.Append("]); \n");
            sb.Append("document.location = loc;\n");
            sb.Append("}\n return false;");

            return sb.ToString();
        }

        private string GetClientHandleFunctionName()
        {
            return string.Format("SubmitAsGetOnClick_{0}", ClientID);
        }

        private string GetSubmitScript(string firstScript)
        {
            if (Page == null)
                return firstScript;

            if (_fields.IsNullOrEmpty())
            {
                throw new InvalidOperationException("AddInput must be called at least once to initialise the '"
                    + ID + "' " + GetType().Name + ".");
            }

            var sb = new StringBuilder(firstScript);
            if (sb.Length > 0)
            {
                if (!firstScript.EndsWith(";"))
                {
                    sb.Append(";");
                }
                sb.Append(' ');
            }
            
            sb.Append("return ");
            sb.Append(GetClientHandleFunctionName());
            sb.Append("();");
            
            return sb.ToString();
        }

        private string GetFormUrl()
        {
            var url = PostBackUrl;
            if (!string.IsNullOrEmpty(url))
                return url;

            Uri requestUrl = Page.Request.Url;
            Debug.Assert(requestUrl.Segments.Length > 0, "requestUrl.Segments.Length > 0");

            return requestUrl.AbsolutePath;
        }
    }
}
