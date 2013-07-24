#region Copyright (c) 2002, 2003 Brian Knowles, Jim Shore
/********************************************************************************************************************
'
' Copyright (c) 2002, 2003 Brian Knowles, Jim Shore
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

namespace NUnit.Extensions.Asp.Test
{
	public abstract class NUnitAspTestCase : WebFormTestCase
	{
		protected readonly string BasePath;
		protected readonly static string BaseUrl;

		private DateTime startTime;

        static NUnitAspTestCase()
        {
            BaseUrl = ConfigurationSettings.AppSettings["BaseUrl"];
            if (string.IsNullOrEmpty(BaseUrl))
                throw new ApplicationException("The BaseUrl application setting is not set.");
        }

        protected NUnitAspTestCase()
        {
            BasePath = ConfigurationSettings.AppSettings["BasePath"];
            if (string.IsNullOrEmpty(BasePath))
                throw new ApplicationException("The BasePath application setting is not set.");
        }

	    protected override void SetUp()
		{
			base.SetUp();
			startTime = DateTime.Now;
		}

		protected override void TearDown()
		{
			TimeSpan elapsedTime = DateTime.Now - startTime;
			TimeSpan overheadTime = elapsedTime - Browser.ElapsedServerTime;
			base.TearDown();
		}
	}
}
