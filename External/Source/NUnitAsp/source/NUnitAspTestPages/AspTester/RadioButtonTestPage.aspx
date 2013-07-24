<%@ Page language="c#" Inherits="NUnitAspTestPages.AspTester.RadioButtonTestPage" Codebehind="RadioButtonTestPage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RadioButtonTestPage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0" />
		<meta name="CODE_LANGUAGE" Content="C#" />
		<meta name="vs_defaultClientScript" content="JavaScript" />
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
	</HEAD>
	<body>
		<form method="post" runat="server">
			<p>
				<asp:RadioButton id="radionButton" runat="server" Text="Test me" />
				<br />
				<asp:RadioButton id="disabled" Enabled="False" runat="server" Text="I'm disabled" />
				<br />
				<asp:RadioButton ID="noText" runat="server"></asp:RadioButton><span>not part of 
					radio button...</span>
				<br />
				<asp:RadioButton ID="formattedText" Runat="server" Text="<b>bold!</b>" />
			</p>
			<p>
				Radio group:
				<span style="border: solid black 1px">
					<asp:RadioButton ID="groupedOne" GroupName="Group" Checked="True" Runat="server" Text="One" />
					<asp:RadioButton ID="groupedTwo" GroupName="Group" Runat="server" Text="Two" />
				</span>
			</p>
			<p>
				<asp:LinkButton ID="submit" Runat="server">Submit</asp:LinkButton>
			</p>
		</form>
	</body>
</HTML>
