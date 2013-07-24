<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Web.Areas.Members.Views.Status.StatusUpdated" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain" %>
<%@ Import Namespace="LinkMe.Domain" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.Block.Page)%>
        <%= Html.RenderStyles(StyleBundles.Block.Forms)%>
        <%= Html.RenderStyles(StyleBundles.Block.Members.Profiles)%>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-title">
        <h1>Thanks for <%= Model.PreviousStatus == null ? "confirming" : "updating" %> your work status</h1>
    </div>

    <div class="section status-section">
        <div class="section-content">
            <div class="status-icon-content">
                <div class="status-icon <%= GetCssIconClass(Model.NewStatus) %>"></div>
            </div>
            <div>
                <div>
                    Your work status has been <%= Model.PreviousStatus == null ? "confirmed as" : "updated to" %> '<strong><%= Model.NewStatus.GetDisplayText() %></strong>'.
                </div>

                <div>
<%  switch (Model.NewStatus)
    {
        case CandidateStatus.AvailableNow: %>
                    Employers and recruiters will see that you are interested in job opportunities.
<%          break;
            
        case CandidateStatus.ActivelyLooking: %>
                    This means that employers and recruiters will be contacting you for job opportunities.
<%          break;

        case CandidateStatus.OpenToOffers: %>
                    This means that an employer who views your resume will assume that you are not actively looking for work, but may be open to other job opportunites.
<%          break;
            
        case CandidateStatus.NotLooking: %>
                    This means that you will not appear in search results and employers and recruiters will not be contacting you for job opportunities.
<%          break;
    } %>
                </div>

<%  int? days;
    Model.ConfirmationDays.TryGetValue(Model.NewStatus, out days);
    if (days != null)
    { %>
                <div class="status-need-confirmation">
                    You will need to confirm your work status in <strong><%= days.Value %></strong> days.
                </div>
<%  } %>

            </div>
        </div>
    </div>

    <div class="section status-section">
        <div class="section-content">
            <div>
                If this is not accurate you can
                <%= Html.RouteRefLink("change your work status", ProfilesRoutes.UpdateStatus) %>
                to
<%  var otherStatuses = new[] {CandidateStatus.AvailableNow, CandidateStatus.ActivelyLooking, CandidateStatus.OpenToOffers, CandidateStatus.NotLooking}.Except(new[] {Model.NewStatus}).ToArray();
    for (var index = 0; index < otherStatuses.Length - 1; ++index)
    { %>
                '<%= otherStatuses[index].GetDisplayText() %>',
<%  } %>
                or '<%= otherStatuses[otherStatuses.Length - 1].GetDisplayText() %>'.
            </div>
        </div>
    </div>

    <div class="section status-section">
        <div class="section-content">
            <div>
                You can also
                <%= Html.RouteRefLink("update your profile", ProfilesRoutes.Profile) %>
                to better reflect your experience and qualifications.
            </div>
        </div>
    </div>

</asp:Content>

