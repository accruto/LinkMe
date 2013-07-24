using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkMe.WebControls
{
	[DefaultProperty("Text"), ToolboxData("<{0}:LinkMeCheckboxListValidator runat=server></{0}:LinkMeCheckboxListValidator>")]
	public class LinkMeCheckboxListValidator: BaseValidator 
	{
		private LinkMeValidatorDetails valDetails = new LinkMeValidatorDetails();
		private ListControl listctrl;
 

		[Browsable(false)]
		public override string Text
		{
			get { return valDetails.Text; }
			set{}
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

		
		public void RequiredFieldValidatorForCheckBoxLists()
		{
			base.EnableClientScript = false;
		}
 
		protected override bool ControlPropertiesValid()
		{
			Control ctrl = FindControl(ControlToValidate);
       
			if(ctrl != null) 
			{
				listctrl = (ListControl) ctrl;
				return (listctrl != null);   
			}
			else 
				return false;  // raise exception
		}
 
		protected override bool EvaluateIsValid()
		{     
			return listctrl.SelectedIndex != -1;
		}
	}
}
