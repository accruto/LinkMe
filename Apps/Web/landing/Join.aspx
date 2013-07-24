<%@ Page Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="Join.aspx.cs" Inherits="LinkMe.Web.Landing.Join" MasterPageFile="~/master/SiteMasterPage.master" %>
<%@ Register TagPrefix="uc" TagName="Join" Src="~/landing/controls/Join.ascx" %>
<%@ Register TagPrefix="nv" Namespace="LinkMe.Web.Manager.Navigation" Assembly="LinkMe.Web" %>

<asp:Content ContentPlaceHolderID="Body" runat="server">

    <div id="main">
    
        <div id="content">
            <p> <strong>Don't you hate it</strong>
                when you hear that your dream job has just been filled
                and everyone else knew that it was up for grabs, but you?
            </p>
            
            <p>
                LinkMe is used by thousands of recruiters and employers to find the right people for the job.
                <strong>Join now!</strong>
            </p>
            
        </div>
        
        <div class="joinform_control self-clearing">
            <div class="section">
                <div class="join_bg">
                    <div class="section-title">
                        <h1>Join Now - It's Free!</h1>
                    </div>
                    <div class="section-content">
                        <nv:NavigationForm id="LinkMeJoinForm" runat="server">
                            <asp:CustomValidator id="valDuplicateEmail" EnableClientScript="false" runat="server" Display="Dynamic" />
                            <uc:Join id="ucJoin" runat="server" />
                            <div class="join_button_control button_control control">
                                <asp:Button id="btnJoin" Text="Join Now!" CssClass="join_button button" runat="server" />
                            </div>
                        </nv:NavigationForm>
                        <div class="privacy">
                            You control how visible your details are so you can be as private or
                            as public as you like.
                        </div>
                    </div>
                </div>
                <div class="section-footer"></div>
            </div>
        </div>
        
    </div>
    
    <div id="footer">
     	<div id="footer_image"></div>
    </div>
    
    <script language="javascript" type="text/javascript"><!--
        // Pure CSS version of hide button-text won't work due to clearing technique being used.
        document.getElementById("<% =btnJoin.ClientID %>").value = "";
    //--></script>

</asp:Content>

