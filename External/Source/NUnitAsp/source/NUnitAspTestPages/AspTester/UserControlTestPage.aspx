<%@ Page language="c#" Inherits="NUnitAspTestPages.AspTester.UserControlTestPage" Codebehind="UserControlTestPage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>UserControlTestPage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0" />
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body>
		<form method="post" runat="server">
			Nested controls:
			<asp:PlaceHolder id="placeHolder" runat="server" />
			<br />
			Click result:
			<asp:Label id="clickResult" runat="server" />
		</form>
	</body>
</html>
