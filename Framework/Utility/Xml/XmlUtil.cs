using System.IO;
using System.Text;
using System.Xml;


namespace LinkMe.Framework.Utility.Xml
{
	/// <summary>
	/// Provides static methods for manipulating XML.
	/// </summary>
	public sealed class XmlUtil
	{
		/// <summary>
		/// The characters treated as whitespace in XML.
		/// </summary>
		public static readonly string WhitespaceChars = "\t\n\r ";

		private XmlUtil()
		{
		}

		/// <summary>
		/// Determines whether two strings represent the same XML fragement. Formatting and
		/// non-significant whitespace is ignored.
		/// </summary>
		/// <param name="one">The first XML string to compare.</param>
		/// <param name="two">The second XML string to compare.</param>
		/// <returns>True if the strings represent the same XML, otherwise false.</returns>
		public static bool XmlStringsEqual(string one, string two)
		{
			if (one == two)
				return true; // Do a simple string comparison first.

			if (one == null || two == null)
				return false;

			// For the moment use an inefficient method - re-write both XML documents with the
			// same options and then compare strings. A better implementation is needed - defect 48954.

			try
			{
				XmlTextReader readerOne = new XmlTextReader(new StringReader(one));
				readerOne.WhitespaceHandling = WhitespaceHandling.Significant;

				XmlTextReader readerTwo = new XmlTextReader(new StringReader(two));
				readerTwo.WhitespaceHandling = WhitespaceHandling.Significant;

				StringBuilder sbOne = new StringBuilder();
				XmlTextWriter writerOne = new XmlTextWriter(new StringWriter(sbOne));
				writerOne.Formatting = Formatting.None;

				StringBuilder sbTwo = new StringBuilder();
				XmlTextWriter writerTwo = new XmlTextWriter(new StringWriter(sbTwo));
				writerTwo.Formatting = Formatting.None;

				writerOne.WriteNode(readerOne, true);
				writerTwo.WriteNode(readerTwo, true);

				return (sbOne.ToString() == sbTwo.ToString());
			}
			catch (XmlException)
			{
				// At least one of the strings is not valid XML. Since string comparison failed
				// (at the beginning of the method) the strings are not equal.

				return false;
			}
		}
	}
}
