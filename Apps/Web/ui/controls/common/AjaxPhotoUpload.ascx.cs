using System;
using System.Diagnostics;
using System.Web;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class AjaxPhotoUpload : LinkMeUserControl
    {
        /// <summary>
        /// The method that receives the posted file and saves it. If saved successfully, it must return
        /// the new URL of the photo to display, otherwise null.
        /// </summary>
        /// <param name="postedFile">The posted file to save.</param>
        /// <returns>The new URL of the photo to display, if saved successfully, otherwise null.</returns>
        public delegate ReadOnlyUrl PhotoUploadProcessor(HttpPostedFile postedFile);
        public delegate Url PhotoDeleteProcessor();

        private const string failureMessage = "Upload failed.";
        private const string successMessage = "Uploaded OK!";
        public const string DeleteParam = "delete";

        private string fileUploadErrorMessage;
        private ReadOnlyUrl targetUploadUrl;
        private ReadOnlyUrl targetDeleteUrl;
        private string targetFormFileInputName;
        private PhotoUploadProcessor uploadProcessor;
        private PhotoDeleteProcessor deleteProcessor;

        protected string FileUploadErrorMessage
        {
            get { return fileUploadErrorMessage; }
        }

        protected ReadOnlyUrl TargetUploadUrl
        {
            get { return targetUploadUrl; }
        }

        public ReadOnlyUrl TargetDeleteUrl
        {
            get { return targetDeleteUrl; }
        }

        protected string TargetFormFileInputName
        {
            get { return targetFormFileInputName; }
        }

        /// <summary>
        /// Call this method in the parent's OnLoad method to display a photo without the ability to edit it.
        /// </summary>
        /// <param name="photoFileUrl">The URL of the photo to display. May be null to display nothing.</param>
        public void DisplayPhotoReadOnly(ReadOnlyUrl photoFileUrl)
        {
            SetPhotoUrl(photoFileUrl);
        }

        /// <summary>
        /// Call this method in the parent's OnLoad method to display a photo and allow uploading another one
        /// to replace it.
        /// </summary>
        /// <param name="photoFileUrl">The URL of the photo to display. May be null to display nothing.</param>
        /// <param name="formTargetUrl">The URL of the page that client-side script should post the form to.
        /// This page should process the uploaded photo and internally call the same method as
        /// <paramref name="uploadProcessor" /> on it.</param>
        /// <param name="targetFormFileInputName">The name of the file input element on the target page
        /// to which the photo should be posted.</param>
        /// <param name="uploadProcessor">The method to be used to process the photo on a postback if the
        /// browser doesn't support posting inside an iframe. This should be the same method that the
        /// target page calls so that the behaviour is consistent between browsers. Currently only used for
        /// Safari.</param>
        public void DisplayPhotoForEditing(ReadOnlyUrl photoFileUrl, ReadOnlyUrl formTargetUrl, string targetFormFileInputName,
            PhotoUploadProcessor uploadProcessor, PhotoDeleteProcessor deleteProcessor)
        {
            if (formTargetUrl == null)
                throw new ArgumentException("The form target URL must be specified.", "formTargetUrl");
            if (string.IsNullOrEmpty(targetFormFileInputName))
            {
                throw new ArgumentException("The target form <input> name must be specified.",
                    "targetFormFileInputName");
            }
            if (uploadProcessor == null)
                throw new ArgumentNullException("uploadProcessor");

            targetUploadUrl = formTargetUrl;
            targetDeleteUrl = formTargetUrl.Clone(new QueryString(DeleteParam, "true"));

            this.targetFormFileInputName = targetFormFileInputName;
            this.uploadProcessor = uploadProcessor;
            this.deleteProcessor = deleteProcessor;

            SetPhotoUrl(photoFileUrl);

            // Never mind that phUploadForm has been moved out of this control - this still works.
            phEdit.Visible = phUploadForm.Visible = true;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStyleSheetReference(StyleSheets.UploadPhoto);

            // Move the upload form to the parent page (at the same level as the parent form).

            phUploadForm.Parent.Controls.Remove(phUploadForm);
            LinkMePage.AddNonFormContent(phUploadForm);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            // This part is only needed for Safari, which cannot do POST through IFrames.
            if (IsPostBack)
            {
                //This is upload file handler
                if(filPhotoUpload.PostedFile != null 
                   && !string.IsNullOrEmpty(filPhotoUpload.PostedFile.FileName) 
                   && filPhotoUpload.PostedFile.ContentLength > 0)
                {
                    Debug.Assert(uploadProcessor != null, "uploadProcessor != null");
                    var photoUrl = uploadProcessor(filPhotoUpload.PostedFile);

                    fileUploadErrorMessage = (photoUrl == null ? failureMessage : successMessage);
                    if (photoUrl != null)
                        imgPhoto.ImageUrl = photoUrl.ToString();
                }
                
                if(inputDelete.Value == DeleteParam)
                {
                    Debug.Assert(uploadProcessor != null, "uploadProcessor != null");
                    Url photoUrl = deleteProcessor();

                    fileUploadErrorMessage = (photoUrl == null ? failureMessage : successMessage);
                    if (photoUrl != null)
                        imgPhoto.ImageUrl = photoUrl.ToString();
                }
                
            }
            
            if(LoggedInMember != null)
                divRemovePhoto.Style["display"] = LoggedInMember.PhotoId == null ? "none" : "";
        }

        private void SetPhotoUrl(ReadOnlyUrl photoFileUrl)
        {
            if (photoFileUrl != null)
            {
                imgPhoto.ImageUrl = photoFileUrl.ToString();
                
                // MF (2009-02-19: Forcing image sizing is pointless and ugly; everywhere which
                // has AjaxPhotoUpload hides the whole thing until page onload fires.
                //
                /*imgPhoto.Height = Member.ProfilePhotoMaxSize.Height;
                imgPhoto.Width = Member.ProfilePhotoMaxSize.Width;*/


                divPhotoContainer.Visible = true;
            }
        }
    }
}