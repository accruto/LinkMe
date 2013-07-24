<%@ Page language="c#" Codebehind="HtmlInputFileTestPage.aspx.cs" AutoEventWireup="True" Inherits="NUnitAspTestPages.HtmlTester.HtmlInputFileTestPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HtmlInputFileTestPage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<INPUT id="file" type="file" name="file" runat="server" />
			<input id="plaintext" type="text" runat="server" />
			<asp:LinkButton id="submit" runat="server">Submit</asp:LinkButton>
			<asp:Label id="uploadResult" runat="server"></asp:Label>
		</form>
	</body>
</HTML>
