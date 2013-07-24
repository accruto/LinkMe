<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="System.Web.Mvc.ViewPage<CommunityModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Communities"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Administrators.Page) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div class="section">
	    <div class="section-body">
		    <ul class="plain-actions actions">
                <li><%= Html.RouteRefLink("Custodians", CommunitiesRoutes.Custodians, new { id = Model.Community.Id })%></li>
		    </ul>
	    </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Community</h1>
    </div>
    
    <div class="form">

<%  using (Html.RenderForm())
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
            
                <%= Html.TextBoxField(Model, m => m.Community.Name)
                    .WithLargestWidth()
                    .WithIsReadOnly() %>

                <%= Html.TextBoxField(Model, "IsEnabled", m => m.Vertical.IsDeleted ? "No" : "Yes")
                    .WithLabel("Enabled")
                    .WithIsReadOnly() %>
                    
<%          if (string.IsNullOrEmpty(Model.Vertical.Host))
            { %>
                <%= Html.TextBoxField(Model, m => m.Vertical.Host)
                    .WithIsReadOnly()%>
<%          }
            else
            { %>
                <%= Html.LinkField(new ReadOnlyUrl(Model.Vertical.Host), Model.Vertical.Host)
                    .WithLabel("Host") %>
<%          } %>                
                
<%          if (string.IsNullOrEmpty(Model.Vertical.SecondaryHost))
            { %>
                <%= Html.TextBoxField(Model, m => m.Vertical.SecondaryHost)
                    .WithLabel("Secondary host").WithIsReadOnly() %>
<%          }
            else
            { %>
                <%= Html.LinkField(new ReadOnlyUrl(Model.Vertical.SecondaryHost), Model.Vertical.SecondaryHost)
                    .WithLabel("Secondary host") %>
<%          } %>                
                    
<%          if (string.IsNullOrEmpty(Model.Vertical.TertiaryHost))
            { %>
                <%= Html.TextBoxField(Model, m => m.Vertical.TertiaryHost)
                    .WithLabel("Tertiary host").WithIsReadOnly() %>
<%          }
            else
            { %>
                <%= Html.LinkField(new ReadOnlyUrl(Model.Vertical.TertiaryHost), Model.Vertical.TertiaryHost)
                    .WithLabel("Tertiary host") %>
<%          } %>                
                    
<%          if (string.IsNullOrEmpty(Model.Vertical.Url))
            { %>
                <%= Html.TextBoxField(Model, m => m.Vertical.Url)
                    .WithIsReadOnly() %>
<%          }
            else
            { %>
                <%= Html.LinkField(new ReadOnlyApplicationUrl("~/" + Model.Vertical.Url), Model.Vertical.Url)
                    .WithLabel("Url") %>
<%          } %>                
                    
                <%= Html.CheckBoxesField(Model)
                    .Add("Requires external login", m => m.Vertical.RequiresExternalLogin)
                    .WithIsReadOnly() %>
                    
<%          if (Model.Vertical.RequiresExternalLogin)
            { %>
                <%= Html.LinkField(new ReadOnlyUrl(Model.Vertical.ExternalLoginUrl), Model.Vertical.ExternalLoginUrl)
                    .WithLabel("External login url") %>
                <%= Html.TextBoxField(Model, m => m.Vertical.ExternalCookieDomain)
                    .WithLabel("External cookie domain") %>
<%          } %>                    

                <%= Html.CheckBoxesField(Model)
                    .Add("Has members", m => m.Community.HasMembers)
                    .Add("Has organisations", m => m.Community.HasOrganisations)
                    .WithIsReadOnly() %>

            </div>
            <div class="section-foot"></div>
        </div>
 <%     }
    } %>

    </div>

</asp:Content>
