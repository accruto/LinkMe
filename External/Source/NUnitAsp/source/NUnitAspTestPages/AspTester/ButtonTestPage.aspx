<%@ Page language="c#" Inherits="NUnitAspTestPages.AspTester.ButtonTestPage" Codebehind="ButtonTestPage.aspx.cs" %>
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
		<form method="post" runat="server">
			<asp:Button id="button" text="Button" runat="server" onclick="button_Click" />
			<br>
			<asp:Button id="disabled" Enabled="False" Text="Disabled" runat="server" />
			<br>
			Click result:
			<asp:Label id="clickResult" runat="server" />
		</form>
	</body>
</HTML>
