using LinkMe.Framework.Utility.Urls;
using Constants=LinkMe.Apps.Services.Constants;

namespace LinkMe.Web.Configuration
{
	public static class WebConstants
	{
		public const string WEB_SERVICE_NAMESPACE = Constants.XmlSerializationNamespace;

		public const int MAX_UPLOAD_FILE_SIZE = 4194304;
		public const string MAX_UPLOAD_FILE_SIZE_DISPLAY_TEXT = "4 MB";

        public static readonly Url ProfilePhotoPlaceholderUrl = new ApplicationUrl("~/ui/images/universal/photo-default.png");
        public static readonly Url ProfilePhotoMultiRecipientUrl = new ApplicationUrl("~/ui/images/universal/multi-recip.png");

        // The ASP.NET event target form field. Use of this field is a good indication of a major hack!
        public const string EVENT_TARGET_FIELD = "__EVENTTARGET";

        public const string CompanyCssDirectory = "~/ui/styles/recruiters/";
    }
}
