<%@ Page language="c#" Inherits="NUnitAspTestPages.FormVariableTestPage" Codebehind="FormVariableTestPage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FormVariableTestPage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form method="post" runat="server">
			<asp:CheckBox ID="checkbox" Runat="server" Checked="True"></asp:CheckBox>
			<asp:Button ID="submit" Runat="server" Text="Submit"></asp:Button>
		</form>
	</body>
</HTML>
