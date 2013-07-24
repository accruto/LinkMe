using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Content.Templates;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Files;

namespace LinkMe.Apps.Agents.Communications.Emails
{
    public abstract class TemplateEmail
    {
        protected enum ServicesType
        {
            Member,
            Client
        }

        private IList<CommunicationAttachment> _attachments;
        private TemplateProperties _properties;

        private readonly Guid _id = Guid.NewGuid();

        protected TemplateEmail(ICommunicationUser to, ICommunicationUser from)
        {
            To = to;
            From = from;
        }

        public Guid Id
        {
            get { return _id; }
        }

        public ICommunicationUser To { get; set; }
        public ICommunicationUser From { get; set; }
        public ICommunicationUser Return { get; set; }

        public virtual IList<ICommunicationUser> Copy
        {
            get { return null; }
        }

        public virtual IList<ICommunicationUser> BlindCopy
        {
            get { return null; }
        }

        public virtual Guid? AffiliateId
        {
            get { return null; }
        }

        public virtual string Definition
        {
            get { return GetType().Name; }
        }

        public virtual string Category
        {
            get { return null; }
        }

        public abstract bool RequiresActivation { get; }

        public TemplateProperties Properties
        {
            get
            {
                if (_properties == null)
                {
                    _properties = new TemplateProperties();
                    AddProperties(_properties);
                }

                return _properties;
            }
        }

        protected virtual void AddProperties(TemplateProperties properties)
        {
            properties.Add("Id", Id);
            properties.Add("Definition", Definition);
            properties.Add("To", To, typeof(ICommunicationUser));
            properties.Add("From", From, typeof(ICommunicationUser));
        }

        public virtual TempFileCollection GetTempFileAttachments()
        {
            return null;
        }

        public virtual IList<CommunicationAttachment> GetAttachments()
        {
            return _attachments;
        }

        public void AddAttachments(IList<CommunicationAttachment> attachments)
        {
            if (attachments == null)
                return;

            if (_attachments == null)
                _attachments = new List<CommunicationAttachment>();
            foreach (var attachment in attachments)
                _attachments.Add(attachment);
        }

        public void AddAttachments(IList<string> fileAttachments)
        {
            if (_attachments == null)
                _attachments = new List<CommunicationAttachment>();
            foreach (var attachment in fileAttachments)
                _attachments.Add((new FileAttachment(attachment, MediaType.GetMediaTypeFromExtension(Path.GetExtension(attachment), MediaType.Text))));
        }

        public Communication CreateCommunication(CopyItem copyItem)
        {
            var communication = new Communication
            {
                Id = Id,
                Definition = Definition,
                AffiliateId = AffiliateId,
                From = From,
                To = To,
                Copy = Copy == null ? null : Copy.Cast<ICommunicationRecipient>().ToList(),
                BlindCopy = BlindCopy == null ? null : BlindCopy.Cast<ICommunicationRecipient>().ToList(),
                Return = Return,
                Subject = copyItem.Text,
                Views = new List<CommunicationView>()
            };

            foreach (var viewItem in copyItem.ViewItems)
            {
                if (viewItem.ResourceItems.Count == 0)
                    communication.Views.Add(new CommunicationView(viewItem.Text, viewItem.MimeType));
                else
                    communication.Views.Add(new CommunicationView(viewItem.Text, viewItem.MimeType, GetCommunicationResources(viewItem.ResourceItems)));
            }

            // Look for temporary files.

            var tempFiles = GetTempFileAttachments();
            if (tempFiles != null)
                communication.AddAttachments(tempFiles);

            // Look for permanent files.

            var attachments = GetAttachments();
            if (attachments != null)
                communication.AddAttachments(attachments);

            return communication;
        }

        private static CommunicationResources GetCommunicationResources(IEnumerable<ResourceItem> resourceItems)
        {
            var resources = new CommunicationResources();
            foreach (var item in resourceItems)
                resources.Add(new CommunicationResource(item.Id, item.ContentStream, item.MediaType));
            return resources;
        }
    }
}