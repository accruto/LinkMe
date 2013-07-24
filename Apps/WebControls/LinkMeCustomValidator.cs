using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkMe.WebControls
{
	/// <summary>
	/// Summary description for LinkMeRadioButtonValidator.
	/// </summary>
	[DefaultProperty("Text"),
		ToolboxData("<{0}:LinkMeCustomValidator runat=server></{0}:LinkMeCustomValidator>")]
	public class LinkMeCustomValidator : CustomValidator, INamingContainer
	{
		private LinkMeValidatorDetails valDetails = new LinkMeValidatorDetails();

		[Browsable(false)]
		public override string Text
		{
			get { return valDetails.Text; }
			set
			{
			}
		}

		[Bindable(false), Category("Appearance")]
		public string ErrorImage
		{
			get { return valDetails.ErrorImage; }
			set { valDetails.ErrorImage = value; }
		}

		[Bindable(false), Category("Appearance")]
		public string DesignModeText
		{
			get { return valDetails.DesignModeText; }
			set { valDetails.DesignModeText = value; }
		}

	}
}