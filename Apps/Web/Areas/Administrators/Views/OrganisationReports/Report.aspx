<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="LinkMe.Web.Areas.Administrators.Views.OrganisationReports.Report" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Agents.Reports.Employers"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<asp:Content id="StyleSheet" ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.JQueryUiAll) %>
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Administrators.Page) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryUiCore) %>
        <%= Html.JavaScript(JavaScripts.JQueryUiDatePicker) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div class="section">
	    <div class="section-body">
		    <ul class="plain-actions actions">
                <li><%= Html.RouteRefLink(Model.Organisation.FullName, OrganisationsRoutes.Edit, new { id = Model.Organisation.Id }) %></li>
                <li><%= Html.RouteRefLink("Credits", OrganisationsRoutes.Credits, new { id = Model.Organisation.Id })%></li>
                <li><%= Html.RouteRefLink("Employers", OrganisationsRoutes.Employers, new { id = Model.Organisation.Id })%></li>
                <li><%= Html.RouteRefLink("Reports", OrganisationsRoutes.Reports, new { id = Model.Organisation.Id })%></li>
                <li><%= Html.RouteRefLink("Communications", OrganisationsRoutes.Communications, new { id = Model.Organisation.Id })%></li>
		    </ul>
	    </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1><%= Model.Report.Name %></h1>
    </div>
    
    <div class="form">

<%  using (Html.RenderRouteForm(OrganisationsRoutes.Report, new { id = Model.Organisation.Id, type = Model.Report.Type }, "ParametersForm"))
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                
                <div class="section-title">
                    <h1>Parameters</h1>
                </div>
            
                <p><%= Model.Report.Description %></p>
                
                <% if (Model.Report is CandidateCareReport)
                        Html.RenderPartial("CandidateCareParameters", Model.Report as CandidateCareReport); %>
                <% if (Model.Report is ResumeSearchActivityReport)
                        Html.RenderPartial("ResumeSearchActivityParameters", Model.Report as ResumeSearchActivityReport); %>
                <% if (Model.Report is JobBoardActivityReport)
                        Html.RenderPartial("JobBoardActivityParameters", Model.Report as JobBoardActivityReport); %>
                                  
                <%= Html.CheckBoxesField(Model.Report)
                    .WithVertical()
                    .Add("Account manager", r => r.SendToAccountManager, GetAccountManagerHtml().ToString())
                    .Add("Client", r => r.SendToClient, GetClientHtml().ToString())
                    .WithLabel("Automatically send to") %>
                               
                <%= Html.ButtonsField(new SaveButton()) %>
                               
            </div>
            <div class="section-foot"></div>
        </div>
<%      }
    } %>
                
<%  using (Html.RenderRouteForm(OrganisationsRoutes.RunReport, new { id = Model.Organisation.Id, type = Model.Report.Type }, "RunReportForm"))
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                
                <div class="section-title">
                    <h1>Manual run</h1>
                </div>
            
                <p>You can manually run this report for any period (the results will not be emailed to anyone).</p>

                <%= Html.CheckBoxField(Model, m => m.IncludeCredits)
                    .WithLabelOnRight("Include credits in report") %>
                           
                <%= Html.DateField(Model, m => m.StartDate)
                    .WithLabel("Start date")
                    .WithButton(Images.Block.Calendar)
                    .WithFormat("dd/MM/yyyy")
                    .WithIsRequired() %>
                <%= Html.DateField(Model, m => m.EndDate)
                    .WithLabel("End date")
                    .WithButton(Images.Block.Calendar)
                    .WithFormat("dd/MM/yyyy")
                    .WithIsRequired() %>
                                    
                <%= Model.Report.ReportAsFile ? Html.ButtonsField(new DownloadButton(), new DownloadPdfButton()) : Html.ButtonsField(new SearchButton()) %>
            </div>
            <div class="section-foot"></div>
        </div>
<%      }
    } %>
       
    </div>
    
</asp:Content>

