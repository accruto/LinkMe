<%@ Page language="c#" Inherits="NUnitAspTestPages.AspTester.ImageButtonTestPage" Codebehind="ImageButtonTestPage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ImageButtonTestPage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form method="post" runat="server">
			<asp:ImageButton id="ImageButton1" runat="server"></asp:ImageButton>
			<br /><asp:ImageButton id="Disabled" Enabled="False" runat="server" />
			<br /><asp:Label id="clickResult" runat="server">Label</asp:Label>
		</form>
	</body>
</HTML>
