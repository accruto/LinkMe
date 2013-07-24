#region Copyright (c) 2002-2004, Brian Knowles, Jim Shore
/********************************************************************************************************************
'
' Copyright (c) 2002-2004 Brian Knowles, Jim Shore
' Originally written by David Paxson.  Copyright assigned to Brian Knowles and Jim Shore
' on the nunitasp-devl@lists.sourceforge.net mailing list on 28 Aug 2002.
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
' THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
'
'*******************************************************************************************************************/
#endregion

using System;
using System.Xml;

namespace NUnit.Extensions.Asp.AspTester
{
	/// <summary>
	/// Tester for System.Web.UI.WebControls.ValidationSummary
	/// </summary>
	public class ValidationSummaryTester : AspControlTester
	{
		/// <summary>
		/// Create the tester and link it to an ASP.NET control.
		/// </summary>
		/// <param name="aspId">The ID of the control to test (look in the page's ASP.NET source code for the ID).</param>
		/// <param name="container">A tester for the control's container.  (In the page's ASP.NET
		/// source code, look for the tag that the control is nested in.  That's probably the
		/// control's container.  Use CurrentWebForm if the control is just nested in the form tag.)</param>
		public ValidationSummaryTester(string aspId, Tester container) 
		: base(aspId, container)
		{
		}

		/// <summary>
		/// The messages in the validation summary.
		/// </summary>
		public string[] Messages
		{
			get
			{
				if (Element.SelectSingleNode(".//ul") != null)
				{
					return ReadBulletedMessages();
				}
				else
				{
					return ReadListMessages();
				}
			}
		}

        public override bool Visible
        {
            get
            {
                HtmlTag tag = Tag;

                // EP 02/01/07: Only consider a summary "visible" if it has content.
                return tag.Visible && tag.Body.Trim().Length > 0;
            }
        }

        private string[] ReadBulletedMessages()
		{
			XmlNodeList nodes = Element.SelectNodes(".//ul/li");
			string[] messages = new string[nodes.Count];
			for (int i = 0; i < nodes.Count; i++)
			{
				messages[i] = nodes[i].InnerXml;
			}
			return messages;
		}

		private string[] ReadListMessages() 
		{
            // Go to the innermost <div> or <font>

            XmlNode node = Element;

            while (true)
            {
                XmlNode child = node.SelectSingleNode(".//div");
                if (child == null)
                {
                    child = node.SelectSingleNode(".//font");
                }

                if (child == null)
                    break;

                node = child;
            }

			string delim = "<br />";
			string inner = node.InnerXml.Trim();

            return inner.Replace(delim, "|").Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
