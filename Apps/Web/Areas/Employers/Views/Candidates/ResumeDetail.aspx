<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Blank.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<ViewCandidateModel>" %>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>
<%@ Import Namespace="LinkMe.Web.Content"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.ResumeDetail) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% var view = Model.View; %>

<div class="resume-detail-aspx">
    <div class="resume_holder result-container" data-memberid="<%= view.Id %>">
        <div id="<%= view.Id %>">
            <div class="resume-details_holder">
                <div class="resume-details">
                    <div class="tabs-container summary-container" id="Summary-details">
                        <div class="resume-header-bg"></div>
                        <div class="resume-bg">
                            <div class="tabs-inner-container">   
                                <% Html.RenderPartial("ResumeSummary", Model); %>
                            </div>
                        </div>
                    </div>
                    <% Html.RenderPartial("ResumeContent", Model); %>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
