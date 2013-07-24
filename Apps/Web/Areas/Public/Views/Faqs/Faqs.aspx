<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<FaqsModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing" %>
<%@ Import namespace="LinkMe.Web.Areas.Public.Models.Faqs" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.Block.Page)%>
        <%= Html.RenderStyles(StyleBundles.JQueryCustom) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Support.Faqs)%>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom) %>
        <%= Html.RenderScripts(ScriptBundles.Site) %>
        <%= Html.RenderScripts(ScriptBundles.Support.Faqs)%>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="PageSubHeader" runat="server">
    <% Html.RenderPartial("SubHeader"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="faqs">
        <div id="looking">I am looking for FAQs...</div>
        <div class="categories">
            <div class="topbar"></div>
            <div class="bg">
                <div class="candidates">
                    <div class="icon"></div>
                    <a class="button forcandidates" href="<%= Model.CandidatesSubcategory.GenerateUrl() %>"></a>
                    <ul>
                        <li>Creating and editing your profile</li>
                        <li>Applying for jobs</li>
                        <li>Newsletter and email alerts</li>
                    </ul>
                </div>
                <div class="employers">
                    <div class="icon"></div>
                    <a class="button foremployers" href="<%= Model.EmployersSubcategory.GenerateUrl() %>"></a>
                    <ul>
                        <li>Searching and contacting candidates</li>
                        <li>Saved searches and email alerts</li>
                        <li>Job boards</li>
                    </ul>
                </div>
                <div class="onlinesecurity">
                    <div class="icon"></div>
                    <a class="button onlinesecurity" href="<%= Model.SecuritySubcategory.GenerateUrl() %>"></a>
                    <ul>
                        <li>Job and employment scams</li>
                        <li>Phising scams</li>
                        <li>Security advice</li>
                    </ul>
                </div>
            </div>
            <div class="bottombar"></div>
        </div>
        <div id="reportissues">
            <span><a class="link contactus" href="javascript:void(0)">Report</a>a site issue</span>
            <div class="icon"></div>
        </div>
        <div id="top5c">
            <div class="title">Top 5 FAQs for candidates</div>
<%  for (var index = 0; index < Model.TopCandidateFaqs.Count && index < 5; index++)
    { %>
                <div class="faqitem item<%= index %>">
                    <div class="icon"></div>
                    <a href="<%= Model.TopCandidateFaqs[index].GenerateUrl(Model.Categories) %>" class="title"><%= Model.TopCandidateFaqs[index].Title %></a>
                </div>
<%  } %>
        </div>
        <div id="top5e">
            <div class="title">Top 5 FAQs for employers</div>
<%  for (var index = 0; index < Model.TopEmployerFaqs.Count && index < 5; index++)
    { %>
                <div class="faqitem item<%= index %>">
                    <div class="icon"></div>
                    <a href="<%= Model.TopEmployerFaqs[index].GenerateUrl(Model.Categories) %>" class="title"><%= Model.TopEmployerFaqs[index].Title %></a>
                </div>
<%  } %>
        </div>
    </div>
    
    <script type="text/javascript">
        $(document).ready(function () {
            linkme.support.contactus.init({
                urls: {
                    partialContactUs: '<%= SupportRoutes.ContactUsPartial.GenerateUrl() %>',
                    apiSendContactUs: '<%= SupportRoutes.ApiSendContactUs.GenerateUrl() %>'
                },
            });

            $(".link.contactus").contactUs();
        });
    </script>

</asp:Content>