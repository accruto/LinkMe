using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Files;

namespace LinkMe.Framework.Communications
{
    [Serializable]
    public class CommunicationResource
    {
        private readonly string _id;
        private readonly Stream _contentStream;
        private readonly string _mediaType;

        public CommunicationResource(string id, Stream contentStream, string mediaType)
        {
            _id = id;
            _contentStream = contentStream;
            _mediaType = mediaType;
        }

        public string Id
        {
            get { return _id; }
        }

        public Stream ContentStream
        {
            get { return _contentStream; }
        }

        public string MediaType
        {
            get { return _mediaType; }
        }
    }

    [Serializable]
    public class CommunicationResources
        : IEnumerable<CommunicationResource>
    {
        private readonly IList<CommunicationResource> _resources = new List<CommunicationResource>();

        public void Add(CommunicationResource resource)
        {
            _resources.Add(resource);
        }

        public int Count
        {
            get { return _resources.Count; }
        }

        public CommunicationResource this[string id]
        {
            get
            {
                foreach (var resource in _resources)
                {
                    if (resource.Id == id)
                        return resource;
                }

                return null;
            }
        }

        IEnumerator<CommunicationResource> IEnumerable<CommunicationResource>.GetEnumerator()
        {
            return _resources.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _resources.GetEnumerator();
        }
    }

    [Serializable]
    public class CommunicationView
    {
        private readonly string _body;
        private readonly string _mimeType;
        private readonly CommunicationResources _resources;

        public CommunicationView(string body, string mimeType, CommunicationResources resources)
        {
            _body = body;
            _mimeType = mimeType;
            _resources = resources;
        }

        public CommunicationView(string body, string mimeType)
            : this(body, mimeType, null)
        {
        }

        public string Body
        {
            get { return _body; }
        }

        public string MimeType
        {
            get { return _mimeType; }
        }

        public CommunicationResources Resources
        {
            get { return _resources; }
        }
    }

    public abstract class CommunicationAttachment
    {
    }

    public class FileAttachment
        : CommunicationAttachment
    {
        private readonly string _fileName;
        private readonly string _mediaType;

        public FileAttachment(string fileName, string mediaType)
        {
            _fileName = fileName;
            _mediaType = mediaType;
        }

        public string FileName
        {
            get { return _fileName; }
        }

        public string MediaType
        {
            get { return _mediaType; }
        }
    }

    public class ContentAttachment
        : CommunicationAttachment
    {
        private readonly Stream _contentStream;
        private readonly string _name;
        private readonly string _mediaType;

        public ContentAttachment(Stream contentStream, string name, string mediaType)
        {
            _contentStream = contentStream;
            _name = name;
            _mediaType = mediaType;
        }

        public Stream ContentStream
        {
            get { return _contentStream; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string MediaType
        {
            get { return _mediaType; }
        }
    }

    public sealed class Communication
        : IDisposable, IInstrumentable
    {
        private IList<CommunicationAttachment> _attachments;
        private TempFileCollection _tempFileAttachments;

        public Guid Id { get; set; }
        public string Definition { get; set; }
        public Guid? AffiliateId { get; set; }

        public ICommunicationRecipient From { get; set; }
        public ICommunicationRecipient To { get; set; }
        public IList<ICommunicationRecipient> Copy { get; set; }
        public IList<ICommunicationRecipient> BlindCopy { get; set; }
        public ICommunicationRecipient Return { get; set; }

        public string Subject { get; set; }
        public IList<CommunicationView> Views { get; set; }

        public IList<CommunicationAttachment> Attachments
        {
            get { return _attachments; }
        }

        public void AddAttachments(IList<CommunicationAttachment> attachments)
        {
            if (_attachments == null)
                _attachments = new List<CommunicationAttachment>();
            foreach (var attachment in attachments)
                _attachments.Add(attachment);
        }

        public void AddAttachments(TempFileCollection tempFileAttachments)
        {
            _tempFileAttachments = tempFileAttachments;
            if (_attachments == null)
                _attachments = new List<CommunicationAttachment>();
            foreach (var attachment in _tempFileAttachments.FilePaths)
                _attachments.Add(new FileAttachment(attachment, MediaType.GetMediaTypeFromExtension(Path.GetExtension(attachment), MediaType.Text)));
        }

        ~Communication()
        {
            Dispose(false);
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_tempFileAttachments != null)
                {
                    _tempFileAttachments.Dispose();
                    _tempFileAttachments = null;
                }
            }
        }

        void IInstrumentable.Write(IInstrumentationWriter writer)
        {
            writer.Write("Id", Id);
            writer.Write("Definition", Definition);
            writer.Write("AffiliateId", AffiliateId);
            writer.Write("To", To);
            writer.Write("From", From);
        }
    }
}