<%@ Page language="c#" Inherits="NUnitAspTestPages.AspTester.CheckBoxTestPage" Codebehind="CheckBoxTestPage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CheckBoxTestPage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0" />
		<meta name="CODE_LANGUAGE" Content="C#" />
		<meta name="vs_defaultClientScript" content="JavaScript" />
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
	</HEAD>
	<body>
		<form method="post" runat="server">
			<asp:CheckBox id="checkBox" runat="server" Text="Test me"></asp:CheckBox>
			<br />
			<asp:CheckBox id="disabled" Enabled="False" runat="server" Text="I'm disabled"></asp:CheckBox>
			<br />
			<asp:CheckBox ID="noText" runat="server"></asp:CheckBox><span>not part of 
				checkbox...</span>
			<br />
			<asp:CheckBox ID="formattedText" Runat="server" Text="<b>bold!</b>"></asp:CheckBox>
			<br />
			<asp:LinkButton ID="submit" Runat="server">Submit</asp:LinkButton>
		</form>
	</body>
</HTML>
