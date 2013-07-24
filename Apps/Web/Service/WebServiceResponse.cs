using System;
using System.IO;
using System.Xml;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Xml;
using LinkMe.Web.Configuration;

namespace LinkMe.Web.Service
{
	public abstract class WebServiceResponse
	{
		private readonly WebServiceReturnCode _returnCode;
		private readonly string[] _errors;

		protected WebServiceResponse(WebServiceReturnCode returnCode, string[] errors)
		{
			_returnCode = returnCode;
			_errors = errors;
		}

		protected WebServiceResponse(Exception ex, string additionalError)
		{
			if (ex == null)
				throw new ArgumentNullException("ex");

			_returnCode = WebServiceReturnCode.Failure;

			_errors = additionalError == null ? new[] { MiscUtils.GetExceptionMessageTree(ex) } : new[] { MiscUtils.GetExceptionMessageTree(ex), additionalError };
		}

		public WebServiceReturnCode ReturnCode
		{
			get { return _returnCode; }
		}

		public string[] Errors
		{
			get { return _errors; }
		}

		public void WriteXml(Stream output)
		{
			if (output == null)
				throw new ArgumentNullException("output");

            var xmlWriter = new XmlTextWriter(output, XmlExtensions.DefaultEncoding) {Formatting = Formatting.Indented};
		    WriteXml(xmlWriter);
			xmlWriter.Flush();
		}

		protected abstract void WriteXml(XmlTextWriter xmlWriter);

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

		protected void WriteReturnCode(XmlTextWriter xmlWriter)
		{
			xmlWriter.WriteElementString("ReturnCode", ReturnCode.ToString());
		}

		protected void WriteErrors(XmlTextWriter xmlWriter)
		{
			if (Errors.IsNullOrEmpty())
				return;

			xmlWriter.WriteStartElement("Errors");

			foreach (string error in Errors)
			{
				xmlWriter.WriteElementString("Error", error);
			}

			xmlWriter.WriteEndElement(); // </Errors>
		}
	}
}
