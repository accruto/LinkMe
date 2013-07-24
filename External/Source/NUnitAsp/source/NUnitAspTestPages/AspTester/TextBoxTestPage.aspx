<%@ Page language="c#" Inherits="NUnitAspTestPages.AspTester.TextBoxTestPage" Codebehind="TextBoxTestPage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TextBoxTestPage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0" />
		<meta name="CODE_LANGUAGE" Content="C#" />
		<meta name="vs_defaultClientScript" content="JavaScript" />
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
	</HEAD>
	<body>
		<form method="post" runat="server">
			<asp:TextBox id="textBox" runat="server"></asp:TextBox>
			<asp:TextBox ID="multiline" Runat="server" TextMode="MultiLine">default</asp:TextBox>
			<asp:TextBox ID="disabled" Runat="server" Enabled="False"></asp:TextBox>
			<asp:Button ID="postback" Text="postback" Runat="server"></asp:Button>
		</form>
	</body>
</HTML>
