using System.Text;
using System.Web;

namespace LinkMe.WebControls
{
	internal class LinkMeValidatorDetails
	{
		private string errorImage = "ui/images/universal/error.png";
		private string infoImage = "ui/img/info.gif";
		private string designModeText = "*";

		public string InfoImage
		{
			get { return infoImage; }
			set { infoImage = value; }
		}

		public string Visible
		{
			get
			{
				if (ErrorImage == "")
					return ("style='visibility:hidden'");
				return "";
			}
		}

		public bool DesignMode
		{
			get { return (HttpContext.Current == null); }
		}

		public string ErrorImage
		{
			get { return errorImage; }
			set { errorImage = value; }
		}

		public string DesignModeText
		{
			get { return designModeText; }
			set { designModeText = value; }
		}

		public string Text
		{
			get
			{
				StringBuilder text = new StringBuilder();
				if (DesignMode)
				{
					text.Append(DesignModeText);
				}
				else
				{
					string applicationPath = GetApplicationPath();
					text.Append("<img align=absbottom src=\"" + applicationPath);
					if (!ErrorImage.StartsWith("/"))
					{
						text.Append("/");
					}
					text.Append(ErrorImage + "\" " + Visible + " />");
				}
				return text.ToString();
			}
		}

		public string TextError
		{
			get
			{
				StringBuilder text = new StringBuilder();
				if (DesignMode)
				{
					text.Append(DesignModeText);
				}
				else
				{
					string applicationPath = GetApplicationPath();
					text.Append(applicationPath);
					if (!ErrorImage.StartsWith("/"))
					{
						text.Append("/");
					}
					text.Append(ErrorImage);
				}
				return text.ToString();
			}
		}

		public string TextInfo
		{
			get
			{
				StringBuilder text = new StringBuilder();
				if (DesignMode)
				{
					text.Append(DesignModeText);
				}
				else
				{
					string applicationPath = GetApplicationPath();
					text.Append(applicationPath);
					if (!InfoImage.StartsWith("/"))
					{
						text.Append("/");
					}
					text.Append(InfoImage);
				}
				return text.ToString();
			}
		}

		private static string GetApplicationPath()
		{
            string path = HttpContext.Current.Request.ApplicationPath;
		    return path == "/" ? "" : path;
		}
	}
}