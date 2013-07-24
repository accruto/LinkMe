<%@ Page language="c#" Inherits="NUnit.Extensions.Asp.Test.AspTester.DataGridTestPage" Codebehind="DataGridTestPage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AspDataGridTestPage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="AspDataGridTestPage" method="post" runat="server">
			<asp:datagrid id="dataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="false" OnItemCommand="dataGrid1_ItemCommand" OnSortCommand="dataGrid1_Sort">
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:LinkButton id="link1" CommandName="click" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "RowNumber")%>' Runat="server">Link</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid>
			<br>
			<asp:datagrid id="dataGrid2" runat="server" AutoGenerateColumns="false">
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:LinkButton id="link2" onclick="link2_Clicked" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "RowNumber")%>' Runat="server">Link</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid>
			<p>The last row clicked was: [<asp:Label ID="clickResult" Runat="server"></asp:Label>]</p>
			<p>The last header clicked was: [<asp:Label ID="headerResult" Runat="server"></asp:Label>]</p>
		</form>
	</body>
</HTML>
