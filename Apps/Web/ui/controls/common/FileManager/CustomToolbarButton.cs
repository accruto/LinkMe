using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.UI.Controls.Common.FileManager
{
    public sealed class CustomToolbarButton
    {
        private Url imageUrl;
        private string text;
        private string commandName;
        private string commandArgument;
        private string onClientClick;
        private bool performPostBack = true;

        public Url ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }

        public string Text
        {
            get { return text ?? string.Empty; }
            set { text = value; }
        }

        public string CommandName
        {
            get { return commandName ?? string.Empty; }
            set { commandName = value; }
        }

        public string CommandArgument
        {
            get { return commandArgument ?? string.Empty; }
            set { commandArgument = value; }
        }

        public string OnClientClick
        {
            get { return onClientClick ?? string.Empty; }
            set { onClientClick = value; }
        }

        public bool PerformPostBack
        {
            get { return performPostBack; }
            set { performPostBack = value; }
        }
    }
}
