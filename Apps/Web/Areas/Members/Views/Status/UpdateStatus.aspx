<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<LinkMe.Web.Areas.Members.Models.Profiles.StatusModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Domain" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>
<%@ Import Namespace="LinkMe.Web.Html" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.Block.Page)%>
        <%= Html.RenderStyles(StyleBundles.Block.Forms)%>
        <%= Html.RenderStyles(StyleBundles.Block.Members.Profiles)%>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.RenderScripts(ScriptBundles.Members.Profiles) %>
    </mvc:RegisterJavaScripts>
</asp:Content>    

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-title">
        <h1>Update my status</h1>
    </div>

    <div class="section">
        <div class="section-content">
            <p>
                Keep your work status up to date so employers know if you're looking for work or not.
            </p>
            <p>
                By ensuring it is up to date,
                you increase your chances of being found and contacted by employers.
            </p>

            <div class="form profiles">
<%  using (Html.RenderRouteForm(ProfilesRoutes.UpdateStatus))
    {
        using (Html.RenderFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
            <%= Html.RadioButtonsField(Model, m => m.Status)
                    .Without(CandidateStatus.Unspecified)
                    .WithIsHidden() %>

                <div class="field">
                    <div class="control">
                        <div class="status-icon available-now js-status" value="AvailableNow"></div>
                        <div class="status-icon actively-looking js-status" value="ActivelyLooking"></div>
                        <div class="status-icon open-to-offers js-status" value="OpenToOffers"></div>
                        <div class="status-icon not-looking js-status" value="NotLooking"></div>
                    </div>
                </div>

                <div class="field">
                    <div class="helptext example_helptext">
                        Please note that if you indicate that you are Immediately Available
                        or Actively Looking, your status will need to be confirmed regularly.
                    </div>
                </div>

            <%= Html.ButtonsField(new SaveButton()) %>
<%      }
    } %>
            </div>
        </div>        
    </div>

</asp:Content>

