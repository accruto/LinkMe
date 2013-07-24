<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.SidebarAbsent) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-title">
        <h1>Blogs</h1>
    </div>
    
    <div class="section">
        <p>
            We're improving our site to make it easier for you to find a job.
        </p>
        <p>
            We're focusing our efforts on features
            (like <%= Html.RouteRefLink("job search", SearchRoutes.Search) %>
            and <%= Html.RouteRefLink("candidate profiles", ProfilesRoutes.Profile)%>)
            which are most successful in securing jobs for candidates.
        </p>
        <p>
            You can also read our <%= Html.RouteRefLink("Resources", ResourcesRoutes.Resources)%> section for advice.
        </p>
        <p>
            Unfortunately, this means that Blogs will no longer be available on our site.
            All content has been removed and archived.
            Please <%= Html.RouteRefLink("contact us", SupportRoutes.ContactUs)%> if you have any questions.
        </p>
    </div>

</asp:Content>

