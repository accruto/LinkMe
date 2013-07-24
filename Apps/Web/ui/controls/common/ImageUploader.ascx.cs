using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Utility;
using LinkMe.Web.Content;
using InvalidOperationException=System.InvalidOperationException;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class ImageUploader : LinkMeUserControl
    {
        private string imageDescription;

        public string ImageUrl
        {
            get { return imgExisting.ImageUrl; }
            set { imgExisting.ImageUrl = value; }
        }

        public string ImageDescription
        {
            get { return imageDescription; }
            set { imageDescription = value.ToLower(); }
        }

        public Stream ImageFileContent
        {
            get { return fileAddLogo.FileContent; }
        }

        public bool HasImage
        {
            get { return fileAddLogo.HasFile; }
        }

        public bool Removed
        {
            get { return ImageRemoved.Value == "removed"; }
        }

        public string FileExtension
        {
            get
            {
                string fileName = fileAddLogo.FileName;
                return fileName.Substring(fileName.LastIndexOf('.') + 1);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            phExistingImage.Visible = (!string.IsNullOrEmpty(ImageUrl));
        }

        public HttpPostedFile PostedFile
        {
            get
            {
                if (!HasImage)
                {
                    throw new InvalidOperationException(
                        "There is no image. Check ImageUploader.HasImage before calling ImageUploader.Submit().");
                }

                string[] extArray = {".jpg", ".gif", ".png"};
                var validExtensions = new List<string>(extArray);
                string ext = fileAddLogo.FileName.Substring(fileAddLogo.FileName.LastIndexOf('.'));

                if (!validExtensions.Contains(ext.ToLower()))
                {
                    throw new UserException(
                        "Your image is not in an accepted format. Please use a JPG, GIF or PNG file.");
                }

                return fileAddLogo.PostedFile;

            }
        }

        protected static string HideIf(bool condition)
        {
            if (condition)
                return "display: none;";

            return string.Empty;
        }
    }
}