<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<SearchEnginesModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Search"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Search engines</h1>
    </div>
    
    <div class="forms_v2">
    
<% using (Html.RenderForm())
   { %>
            
        <div class="section">
            <div class="section-content">
                <h2>Members</h2>
                <br />
    
<%      using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
            <%= Html.TextBoxField(Model.MemberSearch, s => s.TotalMembers).WithIsReadOnly().WithLabel("Total members") %>
            <%= Html.TextBoxField(Model.MemberSearch, s => s.LoginId).WithLabel("Login id").WithLargestWidth() %>
            <%= Html.TextBoxField(Model.MemberSearch, "IsMemberIndexed", s => s.IsIndexed == null ? "" : (s.IsIndexed.Value ? "yes" : "no")).WithIsReadOnly().WithLabel("Is indexed?") %>
<%      } %>

                <br />
                <h2>Job ads</h2>
                <br />
    
<%      using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
            <%= Html.TextBoxField(Model.JobAdSearch, s => s.TotalJobAds).WithIsReadOnly().WithLabel("Total job ads") %>
            <%= Html.TextBoxField(Model.JobAdSearch, s => s.JobAdId).WithLabel("Job ad id").WithLargestWidth() %>
            <%= Html.TextBoxField(Model.JobAdSearch, "IsJobAdIndexed", s => s.IsIndexed == null ? "" : (s.IsIndexed.Value ? "yes" : "no")).WithIsReadOnly().WithLabel("Is indexed?")%>
<%      } %>

                <br />
            </div>
        
<%      using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
            <%= Html.ButtonsField().Add(new SearchButton()) %>
<%      } %>

        </div>
            
<% } %>
    </div>
</asp:Content>

