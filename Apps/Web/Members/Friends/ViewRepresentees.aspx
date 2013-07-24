<%@ Page Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="ViewRepresentees.aspx.cs" Inherits="LinkMe.Web.Members.Friends.ViewRepresentees" MasterPageFile="~/master/StandardMasterPage.master" %>
<%@ Import Namespace="LinkMe.Web.Members.Friends"%>
<%@ Import namespace="LinkMe.Common"%>
<%@ Register TagPrefix="uc" TagName="ContactsList" Src="~/ui/controls/networkers/ContactsList.ascx" %>
<%@ Register TagPrefix="uc" TagName="PagingBar" Src="~/ui/controls/common/AlphabeticalPagingBar.ascx" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    
    <div class="page-title">
        <h1>People I am representing</h1>
    </div>
    
    <div class="forms_v2">
    
        <div class="section">
            <div class="section-content">
        
                <asp:PlaceHolder id="phNoRepresenteesText" runat="server" Visible="false">
                    <div>
                        <p>
                            You are currently not representing anyone.
                        </p>
                    </div>
                    <div>
                        <ul class="action-list">
                            <li><a href="<%= GetUrlForPage<ViewRepresentative>() %>">Nominate</a> someone to represent you</li>
                        </ul>
                    </div>
                </asp:PlaceHolder>
                
                <asp:PlaceHolder ID="phRepresentees" runat="server" Visible="true">
                    <div>As the nominated representative will receive all employer enquiries on the following people's behalf.</div>
                    <h4><%= DescribeRepresenteesCount() %></h4>
                    <uc:PagingBar id="ucPagingBarTop" runat="server" />
                    <asp:PlaceHolder ID="displayRepresentees" runat="server" Visible="true">
                        <uc:ContactsList id="contactsListControl" runat="server" />
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="displayNoRepresentees" runat="server" Visible="false">
                        <div style="padding:10px;margin-top:10px;margin-bottom:10px;background-color:#EEE;">
                            You currently are not representing anyone starting with <%= ucPagingBarTop.NameStartsWith %>.
                        </div>
                    </asp:PlaceHolder>
                    <uc:PagingBar id="ucPagingBarBottom" runat="server" />
                </asp:PlaceHolder>

            </div>
        </div>

    </div>
    
</asp:Content>
