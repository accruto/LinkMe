<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Domain.Contacts.Member>" %>
<%@ Import Namespace="LinkMe.Apps.Agents" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing" %>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Routes" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSheet" runat="server">
    <style type="text/css">
        #mainbody .title {
	        font-size : 2.25em;
	        font-weight : Bold;
	        color : #464646;
	        line-height : 1.2em;
	        margin : 0 0.3333333333333333em 0.5em;
        }
        p {
            font-size: 1.75em;
            color: #464646;
            line-height: 1.2em;
            margin : 0 0.3333333333333333em 2em;
        }
        .resend {
            color: #307EEC;
            cursor: pointer;
        }
        a {
            float: none;
            color: #307EEC;
            text-decoration: none;
        }
        form {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="JavaScript" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".resend").click(function () {
                $("form").submit();
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <div class="title">Account not yet activated</div>
    <p>An activation email was sent to <b><%= Model.EmailAddresses[0].Address %></b>.</p>
	<p>If you didn't get the email, we can try to <span class="resend">re-send the activation email</span>.</p>
    <form method="POST">
    	<input type="submit" class="resend-activation-email-button" id="ResendActivationEmail" value="" name="" />
    </form>
	<p>Is that email address incorrect? <%= Html.RouteRefLink("Change my email address", AccountsRoutes.ChangeEmail, new { returnUrl = AccountsRoutes.NotActivated.GenerateUrl() }, new { id = "ChangeEmailAddress" }) %></p>
	<p>
	    If you are experiencing other problems trying to access
	    our site, please contact our Member Support Hotline on <%= Constants.PhoneNumbers.FreecallHtml %>
	    or <%= Constants.PhoneNumbers.InternationalHtml %>.
	</p>
</asp:Content>