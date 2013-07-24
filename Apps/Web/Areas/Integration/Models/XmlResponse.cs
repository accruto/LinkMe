using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Xml;
using LinkMe.Web.Configuration;

namespace LinkMe.Web.Areas.Integration.Models
{
    public enum XmlReturnCode
    {
        /// <summary>
        /// The web service successfully completed all the requested work.
        /// </summary>
        Success,
        /// <summary>
        /// The entire input was processed and some errors occurred, but some work may have been completed
        /// successfully. The errors collection should contain one or more errors.
        /// </summary>
        Errors,
        /// <summary>
        /// A fatal error occurred and processing was stopped.. Most likely no work has been completed,
        /// but this cannot be guaranteed. The errors collection should contain one error.
        /// </summary>
        Failure,
    }

    public abstract class XmlResponse
    {
        private readonly XmlReturnCode _returnCode;
        private readonly string _xml;
        private readonly string[] _errors;
        private static readonly Regex XmlRegex = new Regex("[\x00-\x09]|[\x11-\x12]|[\x14-\x1f]|&#x[0-9]{2};");

        protected XmlResponse(string xml, string[] errors)
        {
            _returnCode = errors == null || errors.Length == 0 ? XmlReturnCode.Success : XmlReturnCode.Errors;
            _xml = xml == null ? null : XmlRegex.Replace(xml, " ");
            _errors = errors;
        }

        protected XmlResponse(string xml)
        {
            _returnCode = XmlReturnCode.Success;
            _xml = xml == null ? null : XmlRegex.Replace(xml, " ");
        }

        protected XmlResponse(Exception ex)
        {
            _returnCode = XmlReturnCode.Failure;
            _errors = new[] { MiscUtils.GetExceptionMessageTree(ex) };
        }

        public XmlReturnCode ReturnCode
        {
            get { return _returnCode; }
        }

        public string[] Errors
        {
            get { return _errors; }
        }

        public void WriteXml(Stream stream)
        {
            var xmlWriter = new XmlTextWriter(stream, XmlExtensions.DefaultEncoding) {Formatting = Formatting.Indented};
            WriteXml(xmlWriter);
            xmlWriter.Flush();
        }

        private void WriteXml(XmlTextWriter xmlWriter)
        {
            WriteStart(xmlWriter, RootName, "1.0");
            if (WriteReturnCode)
                xmlWriter.WriteElementString("ReturnCode", ReturnCode.ToString());
            if (ReturnCode != XmlReturnCode.Failure && _xml != null)
                xmlWriter.WriteRaw(_xml);
            WriteErrors(xmlWriter);
            WriteEnd(xmlWriter);
        }

        protected abstract string RootName { get; }
        protected abstract bool WriteReturnCode { get; }

        protected void WriteStart(XmlTextWriter xmlWriter, string name, string version)
        {
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement(name, WebConstants.WEB_SERVICE_NAMESPACE);
            xmlWriter.WriteAttributeString("version", version);
        }

        protected void WriteEnd(XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteEndDocument();
        }

        protected void WriteErrors(XmlTextWriter xmlWriter)
        {
            if (Errors.IsNullOrEmpty())
                return;

            xmlWriter.WriteStartElement("Errors");
            foreach (var error in Errors)
                xmlWriter.WriteElementString("Error", error);
            xmlWriter.WriteEndElement();
        }
    }
}
