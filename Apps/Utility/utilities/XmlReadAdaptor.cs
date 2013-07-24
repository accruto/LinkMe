using System.IO;
using System.Xml;

namespace LinkMe.Utility.Utilities
{
	public class XmlReadAdaptor : XmlTextReader
	{
		private const string XMLNS_NAMESPACE = "http://www.w3.org/2000/xmlns/";

		public XmlReadAdaptor(Stream input)
			: base(input)
		{
			// Skip insignificant whitespace by default.

			WhitespaceHandling = System.Xml.WhitespaceHandling.Significant;
		}

		#region Overrides

		public override bool MoveToNextAttribute()
		{
			bool haveAttribute = base.MoveToNextAttribute();

			// Skip the "xmlns" attributes - they should be of no interest to user code.

			while (haveAttribute && (LocalName == "xmlns" || NamespaceURI == XMLNS_NAMESPACE))
			{
				haveAttribute = base.MoveToNextAttribute();
			}

			return haveAttribute;
		}

		#endregion

		public void ReadStartDocument()
		{
			if (!Read())
			{
				throw new XmlException("The input does not contain an XML document.", null,
					LineNumber, LinePosition);
			}

			while (NodeType == XmlNodeType.XmlDeclaration)
			{
				Skip();
			}
		}

		public void EnsureExpectedElement(string name, string ns)
		{
			while (NodeType == XmlNodeType.Comment || NodeType == XmlNodeType.ProcessingInstruction)
			{
				Skip();
			}

			if (!IsStartElement(name, ns))
			{
				if (NodeType == XmlNodeType.Element)
				{
					throw new XmlException(string.Format("XML element '{0}:{1}' was encountered, but '{2}:{3}'"
						+ " was expected.", NamespaceURI, LocalName, ns, name), null, LineNumber, LinePosition);
				}
				else
				{
					throw new XmlException(string.Format("XML node of type {0} was encountered, but XML element"
						+ " '{1}:{2}' was expected.", NodeType, ns, name), null, LineNumber, LinePosition);
				}
			}
		}
	}
}
