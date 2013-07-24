<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Accounts.Models.ChangeEmailModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models" %>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Routes" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSheet" runat="server">
    <style type="text/css">
        #mainbody > .title {
	        font-size : 2.25em;
	        font-weight : Bold;
	        color : #464646;
	        line-height : 1.2em;
	        margin : 0 0.3333333333333333em 0.5em;
        }
        #mainbody .textbox_field .textbox_control .watermark .title {
	        width : 100%;
        }
        p {
            font-size: 1.75em;
            color: #464646;
            line-height: 1.2em;
            margin : 0 0.3333333333333333em 2em;
        }
        p.gray {
            color: #8894A6;
        }
        a {
            float: none;
            color: #307EEC;
            text-decoration: none;
        }
        .buttons .wrapper {
            width: 50%;
        }
        #mainbody .button {
	        font-size : 2.25em;
	        font-weight : bold;
	        color : white;
	        width : 100%;
	        height : 2.2222222222222222em;
	        border-radius : 0.3333333333333333em;
	        text-shadow : 0 -0.0555555555555555em 0 rgba(0,0,0,0.4);
	        background-image : -webkit-gradient(linear, center top, center bottom, from(#8CBB0E), to(#437904));
	        background-image : -webkit-linear-gradient(top, #8CBB0E, #437904);
	        background-image : -moz-linear-gradient(top, #8CBB0E, #437904);
	        background-image : -o-linear-gradient(top, #8CBB0E, #437904);
	        background-image : -ms-linear-gradient(top, #8CBB0E, #437904);
	        background-image : linear-gradient(to bottom, #8CBB0E, #437904);
	        -webkit-box-shadow : 0 0.0555555555555555em 0 rgba(0,0,0,0.2), inset 0 0 1.1111111111111111em 0.1666666666666666em rgba(0,0,0,0.4);
	        -moz-box-shadow : 0 0.0555555555555555em 0 rgba(0,0,0,0.2), inset 0 0 1.1111111111111111em 0.1666666666666666em rgba(0,0,0,0.4);
	        box-shadow: 0 0.0555555555555555em 0 rgba(0,0,0,0.2), inset 0 0 1.1111111111111111em 0.1666666666666666em rgba(0,0,0,0.4);
	        border : 0.0833333333333333em solid #437904;
	        float : left;
	        text-align : center;
	        line-height : 2.25em;
	        cursor : pointer;
	        -webkit-box-sizing : border-box;
	        -moz-box-sizing : border-box;
	        -o-box-sizing : border-box;
	        box-sizing : border-box;
        }
        #mainbody .textbox_field .textbox_control .icon.tip {
	        width : 2.375em;
	        height : 2.4375em;
	        background-size : 100% 100%;
	        background-image : url("../content/images/device/IconCorrect.png");
	        float : left;
	        margin-left : 30.625em;
	        margin-top : -3.6875em;
	        display : none;
	        position : relative;
	        z-index : 10;
        }
        #mainbody .textbox_field .textbox_control.error .icon.tip {
	        display : block;
	        background-image : url("../content/images/device/IconIncorrect.png");
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="JavaScript" runat="server">
    <script type="text/javascript">
        $("document").ready(function () {
            organiseFields($("#mainbody"));
            <% if (!ViewData.ModelState.IsValid && ViewData.ModelState.GetErrorMessages().Length > 0) { %>
            $("#EmailAddress").closest(".control").addClass("error").removeClass("success");
            <% } %>
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <div class="title">Change my email address</div>
    <form method="POST">
        <p>Your email address is currently <strong><%= Model.EmailAddress %></strong></p>
        <%= Html.TextBoxField(Model, m => m.EmailAddress).WithLabel("New email address").WithAttribute("autocapitalize", "off").WithAttribute("autocorrect", "off").WithAttribute("type", "email") %>
        <p>Note: This new email address will become your new username. Please remember to use your new email address next time you log in.</p>
        <p class="gray">Used in case your primary email address isn't working.</p>
        <%= Html.TextBoxField(Model, m => m.SecondaryEmailAddress).WithLabel("Secondary email address").WithAttribute("autocapitalize", "off").WithAttribute("autocorrect", "off").WithAttribute("type", "email")%>
        <div class="buttons">
            <div class="wrapper">
                <input type="submit" name="Send" value="SEND"/>
            </div>
            <div class="wrapper">
                <%= Html.RouteRefLink("CANCEL", AccountsRoutes.NotActivated, null, new { @class = "button" }) %>
            </div>
        </div>
    </form>
</asp:Content>