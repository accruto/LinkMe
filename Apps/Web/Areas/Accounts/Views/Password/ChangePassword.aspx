<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<ChangePasswordModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Agents.Security"%>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Models"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.Block.Page)%>
        <%= Html.RenderStyles(StyleBundles.Block.Forms)%>
        <%= Html.StyleSheet(StyleSheets.Block.NoLeftSideBar) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Change my password</h1>
    </div>
    
    <div class="section">
        <div class="section-content">
            
            <div class="form passwords">
                
<%  using (Html.RenderForm(Context.GetClientUrl()))
    {
        using (Html.RenderFieldSet(FieldSetOptions.LabelsOnLeft))
        {
            if (Model.MustChange)
            { %>
            <p>A password has been generated on your behalf. Please create a new password that is easier to remember.</p>
<%          } %>
            
<%          if (Model.IsAdministrator)
            { %>
            <p>
                The password must be at least <%= Constants.AdministratorPasswordMinLength%> characters long and must contain
                at least one upper-case letter, one lower-case letter and one digit.
            </p>
<%          }
            else
            { %>
            <p>
                Passwords need to be between <%= Constants.PasswordMinLength%> and
                <%= Constants.PasswordMaxLength%> characters long.
            </p>
<%          } %>
            

            <%= Html.PasswordField(Model, m => m.Password).WithLabel("Current password").WithIsRequired()%>
            <%= Html.PasswordField(Model, m => m.NewPassword).WithLabel("New password").WithIsRequired()%>
            <%= Html.PasswordField(Model, m => m.ConfirmNewPassword).WithLabel("Confirm new password").WithIsRequired()%>
<%          if (Model.MustChange)
            { %>
	        <%= Html.ButtonsField(new SaveButton())%>
<%          }
            else
            { %>
	        <%= Html.ButtonsField(new SaveButton()).Add(new CancelButton())%>
<%          }
        }
    } %>

            </div>
        </div>
    </div>

</asp:Content>

