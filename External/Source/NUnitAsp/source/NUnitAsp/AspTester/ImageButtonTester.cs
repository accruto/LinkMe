#region Copyright (c) 2003-2004, Brian Knowles, Jim Shore
/********************************************************************************************************************
'
' Copyright (c) 2003-2004, Brian Knowles, Jim Shore
' Originally by Clifton F. Vaughn; copyright transferred on nunitasp-devl mailing list, May 2003
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

namespace NUnit.Extensions.Asp.AspTester
{
	/// <summary>
	/// Tester for System.Web.UI.WebControls.ImageButton
	/// </summary>
	public class ImageButtonTester : AspControlTester
	{
		/// <summary>
		/// Create the tester and link it to an ASP.NET control.
		/// </summary>
		/// <param name="aspId">The ID of the control to test (look in the page's ASP.NET source code for the ID).</param>
		/// <param name="container">A tester for the control's container.  (In the page's ASP.NET
		/// source code, look for the tag that the control is nested in.  That's probably the
		/// control's container.  Use CurrentWebForm if the control is just nested in the form tag.)</param>
		public ImageButtonTester(string aspId, Tester container) : base(aspId, container)
		{
		}

		/// <summary>
		/// Click the button in a particular location.
		/// <param name="x">The 'x' coordinate of the simulated mouse click.</param>
		/// <param name="y">The 'y' coordinate of the simulated mouse click.</param>
		/// </summary>
		public void Click(int x, int y)
		{
			string name = Tag.Attribute("name");
			EnterInputValue(name + ".x", x.ToString());
			EnterInputValue(name + ".y", y.ToString());
			Submit();
		}

		/// <summary>
		/// Click the button at no particular location. Same as Click(0, 0).
		/// </summary>
		public void Click()
		{
			Click(0,0);
		}
	}
}