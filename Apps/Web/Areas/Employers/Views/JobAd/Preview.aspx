<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Employers.Models.JobAds.PreviewJobAdModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls" %>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds" %>
<%@ Import Namespace="LinkMe.Domain.Users.Employers.Orders.Queries" %>
<%@ Import Namespace="LinkMe.Framework.Utility" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.JobAds" %>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.NoLeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.Orders) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.JobAds.Preview) %>
<%  if (!string.IsNullOrEmpty(Model.OrganisationCssFile))
    { %>
        <%= Html.StyleSheet(new StyleSheetReference(Model.OrganisationCssFile))%>
<%  } %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.RenderScripts(ScriptBundles.Site) %>
        <%= Html.RenderScripts(ScriptBundles.Employers.JobAds.Preview) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Employer site</li>
        <li class="current-breadcrumb">Preview job ad</li>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Preview: <%= HtmlUtil.HtmlToText(Model.JobAd.Title) %></h1>
    </div>
    
<%  var showFeaturePacks = Model.Status == JobAdStatus.Draft
        &&
        (
            (CurrentRegisteredUser == null)
            ||
            (Model.JobAdCredits == 0 && string.IsNullOrEmpty(Model.OrganisationCssFile))
        );

    if (showFeaturePacks)
    {
        Html.RenderPartial("FeaturePacks");
    } %>

    <div class="section">
        <div class="section-content">

<%  if (Model.Status == JobAdStatus.Draft)
    { %>
        <p>This job ad will expire <span class="expiry-days"><%= Model.ExpiryTime.GetDaysLeftDisplayText() %></span> on <strong><span class="expiry-date"><%= string.Format("{0} {1}", Model.ExpiryTime.DayOfWeek, Model.ExpiryTime.ToShortDateString()) %></span></strong>.</p>
<%  }
    else if (Model.Status == JobAdStatus.Open)
    { %>
            <p>This job ad is currently open and will expire <span class="expiry-days"><%= Model.ExpiryTime.GetDaysLeftDisplayText() %></span> on <strong><span class="expiry-date"><%= string.Format("{0} {1}", Model.ExpiryTime.DayOfWeek, Model.ExpiryTime.ToShortDateString()) %></span></strong>.</p>
<%  }
    else if (Model.Status == JobAdStatus.Closed)
    {
        if (Model.CanBeOpened)
        { %>
            <p>This job ad is currently closed.</p>
            <p>It can however be re-opened without using another credit where it will expire <span class="expiry-days"><%= Model.ExpiryTime.GetDaysLeftDisplayText() %></span> on <strong><span class="expiry-date"><%= string.Format("{0} {1}", Model.ExpiryTime.DayOfWeek, Model.ExpiryTime.ToShortDateString()) %></span></strong>.</p>
<%      }
        else
        { %>
            <h2>Your job ad is saved as a draft.</h2>
            <p>You can repost it where it will expire <span class="expiry-days"><%= Model.ExpiryTime.GetDaysLeftDisplayText() %></span> on <strong><span class="expiry-date"><%= string.Format("{0} {1}", Model.ExpiryTime.DayOfWeek, Model.ExpiryTime.ToShortDateString()) %></span></strong>.</p>
<%      }
    } %>    

        </div>
    </div>

    <div class="section">
        <div class="section-body">
            <div class="section-content">
                <div class="section-title">
                    <h1>Search results</h1>
                </div>
            </div>
        </div>
    </div>

<%  Html.RenderPartial("JobAdListView", Model.JobAd); %>
    
    <div class="section">
        <div class="section-body">
            <div class="section-content">
                <div class="section-title">
                    <h1>Job ad</h1>
                </div>
            </div>
        </div>
    </div>

<%  Html.RenderPartial("JobAdView", Model); %>

<%  using (Html.RenderForm(Context.GetClientUrl()))
    { %>
    <div class="form">
        <div class="right-buttons-section section">
            <div class="section-body">
<%      if (Model.Status == JobAdStatus.Draft)
        {
            if (showFeaturePacks)
            { %>
                <div style="display: none">
                <%= Html.RadioButtonsField(Model, m => m.FeaturePack) %>
                </div>
<%          } %>

            <%= Html.ButtonsField(new EditButton(), new PublishButton())%>
<%      }
        else if (Model.Status == JobAdStatus.Open)
        { %>
            <%= Html.ButtonsField(new EditButton(), new DoneButton())%>                
<%      }
        else if (Model.Status == JobAdStatus.Closed)
        {
            if (Model.CanBeOpened)
            { %>
                <%= Html.ButtonsField(new EditButton(), new ReopenButton())%>                
<%          }
            else
            { %>    
                <%= Html.ButtonsField(new EditButton(), new RepostButton())%>
<%          }
        } %>
        
            </div>
        </div>
    </div>
<%  } %>

    <script type="text/javascript">
        $(document).ready(function () {
            linkme.employers.jobads.preview.ready({
                urls: {
                    logoUrl: <%= Model.JobAd.LogoId != null ? "'" + JobAdsRoutes.Logo.GenerateUrl(new { fileId = Model.JobAd.LogoId.Value }) + "'" : "null" %>
                },
                featurePacks: {
                    baseFeaturePack: {
                        isSelected: <%= Model.FeaturePack == JobAdFeaturePack.BaseFeaturePack ? "true" : "false" %>,
                        showLogo: <%= Model.Features[JobAdFeaturePack.BaseFeaturePack].ShowLogo ? "true" : "false" %>,
                        isHighlighted: <%= Model.Features[JobAdFeaturePack.BaseFeaturePack].IsHighlighted ? "true" : "false" %>,
                        expiryDays: '<%= Model.Features[JobAdFeaturePack.BaseFeaturePack].ExpiryTime.GetDaysLeftDisplayText() %>',
                        expiryDate: '<%= string.Format("{0} {1}", Model.Features[JobAdFeaturePack.BaseFeaturePack].ExpiryTime.DayOfWeek, Model.Features[JobAdFeaturePack.BaseFeaturePack].ExpiryTime.ToShortDateString()) %>'
                    },
                    featurePack1: {
                        isSelected: <%= Model.FeaturePack == JobAdFeaturePack.FeaturePack1 ? "true" : "false" %>,
                        showLogo: <%= Model.Features[JobAdFeaturePack.FeaturePack1].ShowLogo ? "true" : "false" %>,
                        isHighlighted: <%= Model.Features[JobAdFeaturePack.FeaturePack1].IsHighlighted ? "true" : "false" %>,
                        expiryDays: '<%= Model.Features[JobAdFeaturePack.FeaturePack1].ExpiryTime.GetDaysLeftDisplayText() %>',
                        expiryDate: '<%= string.Format("{0} {1}", Model.Features[JobAdFeaturePack.FeaturePack1].ExpiryTime.DayOfWeek, Model.Features[JobAdFeaturePack.FeaturePack1].ExpiryTime.ToShortDateString()) %>'
                    },
                    featurePack2: {
                        isSelected: <%= Model.FeaturePack == JobAdFeaturePack.FeaturePack2 ? "true" : "false" %>,
                        showLogo: <%= Model.Features[JobAdFeaturePack.FeaturePack2].ShowLogo ? "true" : "false" %>,
                        isHighlighted: <%= Model.Features[JobAdFeaturePack.FeaturePack2].IsHighlighted ? "true" : "false" %>,
                        expiryDays: '<%= Model.Features[JobAdFeaturePack.FeaturePack2].ExpiryTime.GetDaysLeftDisplayText() %>',
                        expiryDate: '<%= string.Format("{0} {1}", Model.Features[JobAdFeaturePack.FeaturePack2].ExpiryTime.DayOfWeek, Model.Features[JobAdFeaturePack.FeaturePack2].ExpiryTime.ToShortDateString()) %>'
                    }
                }
            });
        });
    </script>
    
</asp:Content>
