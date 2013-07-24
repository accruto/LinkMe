<%@ Page language="c#" Inherits="NUnit.Extensions.Asp.Test.HtmlTester.HtmlAnchorTestPage" Codebehind="HtmlAnchorTestPage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HtmlAnchorTestPage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form method="post" runat="server">
			<!-- NOTE: href attribute of this anchor is capitalized to test case insensitivity of HTML parser -->
			<a id="testLink" Href="../RedirectionTarget.aspx?a=a&amp;b=b">Click Here</a>
			<br>
			<a id="popupLink" onclick="javascript: x=(screen.availWidth/2)-250; y=(screen.availHeight/2)-200;&#13;&#10;&#9;&#9;&#9;some_Win = window.open('../RedirectionTarget.aspx','some_Popup',&#13;&#10;&#9;&#9;&#9;'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=500,height=400,top='+y+',left='+x);"
				href="#">Popup Link</a>
			<br>
			<!-- NOTE: id attribute of this anchor is capitalized to test case insensitivity of HTML parser -->
			<a ID="disabledLink" disabled href="../RedirectionTarget.aspx?a=a&amp;b=b">Disabled 
				Link</a>
			<br>
			<a id="serverLink" runat="server" onserverclick="serverLink_ServerClick">Server-Side Link</a>&nbsp;
		</form>
	</body>
</HTML>
