#region Copyright (c) 2003, Brian Knowles, Jim Shore
/********************************************************************************************************************
'
' Copyright (c) 2003, Brian Knowles, Jim Shore
' Originally by Andrew Enfield; copyright transferred on nunitasp-devl mailing list, May 2003
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
using System.Configuration;

using NUnit.Framework;
using NUnit.Extensions.Asp.HtmlTester;
using NUnit.Extensions.Asp.AspTester;

namespace NUnit.Extensions.Asp.Test.HtmlTester
{

	public class HtmlInputFileTest : NUnitAspTestCase
	{
		
		protected string TestFilePath = ConfigurationSettings.AppSettings["TestFilePath"];

		private HtmlInputFileTester fileUpload;
        private TextBoxTester plaintext;

		private LinkButtonTester submit;
		private LabelTester uploadResult;

		protected override void SetUp() 
		{
			base.SetUp();

			fileUpload = new HtmlInputFileTester("file", CurrentWebForm, true);
            plaintext = new TextBoxTester("plaintext", CurrentWebForm);

			submit = new LinkButtonTester("submit", CurrentWebForm);

			uploadResult = new LabelTester("uploadResult", CurrentWebForm );

			Browser.GetPage(BaseUrl + "HtmlTester/HtmlInputFileTestPage.aspx");
		}

		public void TestText() {
			AssertEquals("empty file box", "", fileUpload.Filename);
			fileUpload.Filename = TestFilePath;
			submit.Click();
			AssertEquals("empty file box", "", fileUpload.Filename);
			AssertVisibility(uploadResult,true);
			AssertEquals("show success", "Success",uploadResult.Text);
		}

		public void TestText_WhenEmpty() {
			AssertEquals("empty file box", "", fileUpload.Filename);
			fileUpload.Filename = "";
			submit.Click();
			AssertEquals("empty file box", "", fileUpload.Filename);
		}

        [Test]
        public void TestText_ExtendedUnicodeCharacters()
        {
            const string testString = "a\x00FFb\x0001c\xFFFCz";

            // Make sure extended characters work when a file is uploaded, because a different type of
            // form submission is used in that case (multipart instead of url-encoded).

            plaintext.Text = testString;
            fileUpload.Filename = TestFilePath;
            submit.Click();

            AssertEquals(testString, plaintext.Text);
            AssertEquals("empty file box", "", fileUpload.Filename);
        }
    }
}
