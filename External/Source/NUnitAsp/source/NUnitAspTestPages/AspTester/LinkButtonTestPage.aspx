<%@ Page language="c#" Inherits="NUnitAspTestPages.AspTester.LinkButtonTestPage" Codebehind="LinkButtonTestPage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ButtonTestPage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="ButtonTestPage" method="post" runat="server">
			<asp:LinkButton id="button" runat="server">Link</asp:LinkButton>
			<br /><asp:LinkButton id="disabled" Enabled="False" runat="server">Disabled</asp:LinkButton>
			<br />Click result: <asp:Label id="clickResult" runat="server" />
		</form>
	</body>
</HTML>
