#region Copyright (c) 2003, Brian Knowles, Jim Shore
/********************************************************************************************************************
'
' Copyright (c) 2003, Brian Knowles, Jim Shore
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
using NUnit.Framework;
using NUnit.Extensions.Asp.AspTester;

namespace NUnit.Extensions.Asp.Test.AspTester
{
	[TestFixture]
	public class RadioButtonTest : CheckBoxTest
	{
		private RadioButtonTester groupedOne;
		private RadioButtonTester groupedTwo;

		protected override void SetUp()
		{
			base.SetUp();

			// Used in base class
			CheckBox = new RadioButtonTester("radionButton", CurrentWebForm);
			DisabledCheckBox = new RadioButtonTester("disabled", CurrentWebForm);

			groupedOne = new RadioButtonTester("groupedOne", CurrentWebForm);
			groupedTwo = new RadioButtonTester("groupedTwo", CurrentWebForm);

			Submit = new LinkButtonTester("submit", CurrentWebForm);
			Browser.GetPage(BaseUrl + "/AspTester/RadioButtonTestPage.aspx");
		}

		[Test]
		[ExpectedException(typeof(RadioButtonTester.CannotUncheckException))]
		public override void TestUncheck()
		{
			CheckBox.Checked = false;
		}

		[Test]
		public void TestGroupedCheck()
		{
			AssertEquals(true, groupedOne.Checked);
			AssertEquals(false, groupedTwo.Checked);

			groupedTwo.Checked = true;
			Submit.Click();

			AssertEquals(false, groupedOne.Checked);
			AssertEquals(true, groupedTwo.Checked);
		}
	}
}
