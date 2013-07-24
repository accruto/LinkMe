using System;
using System.ComponentModel;
using System.Collections;
using System.Windows.Forms;
using System.Reflection;
using System.Text.RegularExpressions;
using LinkMe.Framework.Configuration;

namespace LinkMe.Framework.Type.Tools.Controls
{
	/// <summary>
	/// An extender control that can validate the value of a textbox against a regular expression.
	/// </summary>
	[
		ProvideProperty("PrimitiveType", typeof(TextBox)),
		ProvideProperty("DisplayName", typeof(TextBox)),
		ProvideProperty("Required", typeof(TextBox)),
		ProvideProperty("RegularExpression", typeof(TextBox))
	]
	public class TextboxValidator
		:	Component,
			IExtenderProvider
	{
		private const PrimitiveType m_defaultType = PrimitiveType.String;
		private const bool m_defaultRequired = false;
		private const string m_defaultDisplayName = "";
		private const string m_defaultRegEx = "";

		private class ProvidedProperties
		{
			public PrimitiveType PrimitiveType = m_defaultType;
			public bool Required = m_defaultRequired;
			public string DisplayName = m_defaultDisplayName;
			public string RegularExpression = m_defaultRegEx;
		}

		private ErrorProvider m_errorProvider = null;
		private Hashtable m_providedProperties = new Hashtable();

		private Container components = null;

		public TextboxValidator()
		{
			InitializeComponent();
		}

		public TextboxValidator(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new Container();
		}
		#endregion
	
		#region IExtenderProvider Members

		public bool CanExtend(object extendee)
		{
			// Can extend a text box.

			return extendee is TextBox;
		}

		#endregion

		[Description("The ErrorProvider control where error messages can be shown.")]
		public ErrorProvider ErrorProvider
		{
			get { return m_errorProvider; }
			set { m_errorProvider = value; }
		}

		[Browsable(false)]
		public bool IsValid
		{
			get
			{
				// Iterate over the controls looking for an error message.

				foreach ( Control control in m_providedProperties.Keys )
				{
					string errorMessage = GetErrorMessage(control);
					if ( errorMessage != null && errorMessage.Length > 0 )
						return false;
				}

				return true;
			}
		}

		public void CheckIsValid()
		{
			if ( m_errorProvider != null )
			{
				// Iterate.

				foreach ( Control control in m_providedProperties.Keys )
					m_errorProvider.SetError(control, GetErrorMessage(control));
			}
		}

		public void ClearIsValid()
		{
			if ( m_errorProvider != null )
			{
				// Iterate.

				foreach ( Control control in m_providedProperties.Keys )
					m_errorProvider.SetError(control, string.Empty);
			}
		}

		public void SelectInvalid()
		{
			// Iterate over the controls looking for an error message.

			foreach ( Control control in m_providedProperties.Keys )
			{
				string errorMessage = GetErrorMessage(control);
				if ( errorMessage != null && errorMessage.Length > 0 )
				{
					// Select the control.

					if ( control.CanSelect )
					{
						control.Select();
						if ( control is TextBox )
							((TextBox) control).SelectAll();

						return;
					}
				}
			}
		}

		[DefaultValue(m_defaultType)]
		public PrimitiveType GetPrimitiveType(Control control)
		{
			if ( m_providedProperties.Contains(control) )
				return ((ProvidedProperties) m_providedProperties[control]).PrimitiveType;
			else
				return PrimitiveType.String;
		}

		// The value must be of type "object", other WinForms designer fails to find this method when
		// generating code! See defect 44630 for details.
		public void SetPrimitiveType(Control control, object primitiveType)
		{
			GetProperties(control).PrimitiveType = (PrimitiveType)primitiveType;
		}

		[DefaultValue(m_defaultRequired)]
		public bool GetRequired(Control control)
		{
			if ( m_providedProperties.Contains(control) )
				return ((ProvidedProperties) m_providedProperties[control]).Required;
			else
				return false;
		}

		public void SetRequired(Control control, bool required)
		{
			GetProperties(control).Required = required;
		}

		[DefaultValue(m_defaultDisplayName)]
		public string GetDisplayName(Control control)
		{
			if ( m_providedProperties.Contains(control) )
				return ((ProvidedProperties) m_providedProperties[control]).DisplayName;
			else
				return string.Empty;
		}

		public void SetDisplayName(Control control, string displayName)
		{
			GetProperties(control).DisplayName = displayName == null ? string.Empty : displayName;
		}

		[DefaultValue(m_defaultRegEx)]
		public string GetRegularExpression(Control control)
		{
			if ( m_providedProperties.Contains(control) )
				return ((ProvidedProperties) m_providedProperties[control]).RegularExpression;
			else
				return string.Empty;
		}

		public void SetRegularExpression(Control control, string regularExpression)
		{
			GetProperties(control).RegularExpression = regularExpression == null ? string.Empty : regularExpression;
		}

		public void SetRegularExpression(Control control, System.Type type, string property)
		{
			// Look for a regular expression validation attribute on the property.

            PropertyInfo propertyInfo = type.GetProperty(property);
			if ( propertyInfo != null )
			{
				foreach ( ConfigurationRegexValidationAttribute attribute in propertyInfo.GetCustomAttributes(typeof(ConfigurationRegexValidationAttribute), true) )
				{
					// Set the pattern.

					SetRegularExpression(control, attribute.Pattern);
					return;
				}
			}

			SetRegularExpression(control, string.Empty);
		}

		private ProvidedProperties GetProperties(Control control)
		{
			if ( m_providedProperties.Contains(control) )
			{
				return (ProvidedProperties) m_providedProperties[control];
			}
			else
			{
				ProvidedProperties properties = new ProvidedProperties();
				m_providedProperties.Add(control, properties);

				control.Validating += new CancelEventHandler(ValidatingHandler);
                control.KeyUp += new KeyEventHandler(CheckOnKeystrokeHandler);

				return properties;
			}
		}

		private void ValidatingHandler(object sender, CancelEventArgs args)
		{
			Validate((Control) sender);
		}

		private void CheckOnKeystrokeHandler(object sender, KeyEventArgs args)
		{
			Validate((Control) sender);
		}

		public void Validate(Control control)
		{
			if ( m_errorProvider != null )
				m_errorProvider.SetError(control, GetErrorMessage(control));
		}

		private string GetErrorMessage(Control control)
		{
			// Get the properties for this control.

			ProvidedProperties properties = (ProvidedProperties) m_providedProperties[control];
			string displayName = properties.DisplayName.Length == 0 ? control.Name : properties.DisplayName;

			string value = control.Text;

			// Check against required.

			if ( value.Length == 0 )
			{
				if ( properties.Required )
					return displayName + " is required.";
				else
					return string.Empty;
			}

			// Validate that the text can be converted to the type.

			try
			{
				TypeConvert.ToType(value, properties.PrimitiveType);
			}
			catch ( Exception )
			{
				return "The text cannot be converted to the '" + properties.PrimitiveType.ToString() + "' type.";
			}

			// Validate the regular expression.

			if ( properties.RegularExpression.Length > 0 )
			{
				if ( !Regex.IsMatch(value, properties.RegularExpression) )
					return "The text is not of the correct format.";
			}

			// All tests passed.

			return string.Empty;
		}
	}
}
