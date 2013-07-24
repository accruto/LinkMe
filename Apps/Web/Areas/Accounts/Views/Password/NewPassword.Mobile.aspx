<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Accounts.Models.NewPasswordModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes" %>

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
            margin : 0 0.4285714285714286em 2em;
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
        #error-message li {
            list-style: none;
	        font-size : 1.75em;
	        font-weight : Bold;
	        color : #CC6B6B;
	        line-height : 1.2em;
	        margin : 0 0.4285714285714286em 0.5em;        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="JavaScript" runat="server">
    <script type="text/javascript">
        $("document").ready(function () {
            organiseFields($("#mainbody"));
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <div class="title">Request a new password</div>
    <% Html.RenderPartial("ValidationSummary"); %>
    <p>Enter your email address and a new password will be emailed to you.</p>
    <form method="POST">
        <%= Html.TextBoxField(Model, m => m.LoginId).WithLabel("Email address").WithAttribute("autocapitalize", "off").WithAttribute("autocorrect", "off").WithAttribute("type", "email")%>
        <div class="buttons">
            <div class="wrapper">
                <input type="submit" name="Send" value="SEND"/>
            </div>
            <div class="wrapper">
                <%= Html.RouteRefLink("CANCEL", HomeRoutes.Home, null, new { @class = "button" }) %>
            </div>
        </div>
    </form>
    
</asp:Content>