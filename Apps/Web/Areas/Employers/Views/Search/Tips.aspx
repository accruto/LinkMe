<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Views"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.EmployerLoggedInFrontPage) %>
        <%= Html.StyleSheet(StyleSheets.Folders) %>
        <%= Html.StyleSheet(StyleSheets.BlockLists) %>
        <%= Html.StyleSheet(StyleSheets.Search) %>
        <%= Html.StyleSheet(StyleSheets.SearchResults) %>
        <%= Html.StyleSheet(StyleSheets.JQueryWidgets) %>
        <%= Html.StyleSheet(StyleSheets.JQueryCustom) %>
        <%= Html.StyleSheet(StyleSheets.Search) %>
        <%= Html.StyleSheet(StyleSheets.Folders) %>
        <%= Html.StyleSheet(StyleSheets.FlagLists) %>
        <%= Html.StyleSheet(StyleSheets.BlockLists) %>
        <%= Html.StyleSheet(StyleSheets.Error) %>
        <%= Html.StyleSheet(StyleSheets.Overlay) %>
        <%= Html.StyleSheet(StyleSheets.JQueryAutocomplete) %>
        <%= Html.StyleSheet(StyleSheets.JQuerySlider) %>
        <%= Html.StyleSheet(StyleSheets.JQueryTabs) %>
        <%= Html.StyleSheet(StyleSheets.JQueryDragDrop) %>
        <%= Html.StyleSheet(StyleSheets.Pagination) %>
        <%= Html.StyleSheet(StyleSheets.Notes) %>
        <%= Html.StyleSheet(StyleSheets.JQueryFileUploadUi) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Employer site</li>
        <li><%= Html.RouteRefLink("New candidate search", SearchRoutes.Search)%></li>
        <li class="current-breadcrumb">Search tips</li>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

<div class="search-tips_ascx">
    <div class="heading">Search tips</div>
    <ul>
        <li>
            <span class="title">Rule #1: Just search!</span>
            <div class="content">
                <div>Don't worry about using the "ideal" combination of keywords to find the candidate you're looking for. You can change keywords, refine and filter your results quickly on the search results page, so start with just one or two keywords and see how you go.</div>
                <div>This also means you'll never miss out on candidates by making your initial search too narrow.</div>
            </div>
        </li>
        <li>
            <span class="title">Four easy steps to finding candidates</span>
            <div class="content">
                <div>We know that everyone has their own approach to finding candidates; however, our experience shows that there are four key steps to the search process:</div>
                <div>
                    <ol>
                        <li><strong>Create a folder</strong> - call it something unique and easily identifiable with the role you're looking for.</li>
                        <li><strong>Do multiple searches and refinements</strong> - more detail and suggestions can be found below.</li>
                        <li><strong>Save suitable candidates</strong> you find to the folder - you can review them later. Feel free to add Notes to candidate profiles as you go, which you can share with your colleagues. You can also "flag" interesting candidates to collate them into your temporary "flagged" folder.</li>
                        <li><strong>Contact candidates</strong> - we suggest you try to phone candidates first, and then via email. Either way, candidates are waiting to hear from you, so don't delay.</li>
                    </ol>
                </div>
            </div>
        </li>
        <li>
            <span class="title">Too many results?</span>
            <div class="content">
                <div><strong>Use the refine and filter sidebar:</strong> You can narrow down your search results further using various criteria, including: Desired salary; Job hunter status (e.g. Immediately available); Preferred job type (e.g. Full time, part time); Employer name(s); Industry; Availability, and others.</div>
                <div><strong>Add more keywords:</strong> If you're getting too many candidates in your search results, you can add more keywords to your search.  We suggest you add one or two at a time and see how you go. Think about which words you would expect to see in the resume of a person who would suit the role. You could add skills, qualifications, and/or specialised areas of expertise.</div>
                <div>For example: Keywords: project manager enterprise software<br />Every result will contain the words "project", "manager", "enterprise", and "software".</div>
                <div><strong>Use "quotes":</strong> You can also use "quotes" to force LinkMe to look out for a phrase (or type these words into the "Exact phrase" box). This will narrow your results as only resumes with your exact phrase will be found.</div>
                <div>For example: Keywords: "project manager" will only return results with the exact phrase "project manager".</div>
                <div><strong>Use the "job title" keywords field:</strong> Sometimes the context of a word can make a big difference to the quality of your results. If you've tried searching for a property developer, you've probably found many software or game developers. To focus your search, you need to see "property developer" in the candidate's job title.</div>
                <div>For example: Keywords: All these words in their job title: property developer<br />Every result will contain "property" and "developer" somewhere among all their job titles.</div>
                <div><strong>Exclude results with "NOT":</strong> You can exclude results with certain words: use NOT<br />
Use the special keyword NOT to exclude resumes with a certain keyword or phrase in them.</div>
                <div>For example: Keywords: accountant MYOB NOT audit<br />Every result will contain the words "accountant" and "MYOB". None will contain "audit".</div>
                <div><strong>Turn off synonyms:</strong> The LinkMe search automatically matches for synonyms (for example "CEO", "Chief Executive Officer", "Chief Executive", "MD", and "Managing Director" all count as exactly the same thing).  If you find your results include inappropriate matches, you can eliminate any synonyms by putting an equals sign in front of the keyword or phrase in question.</div>
                <div>For example: Keywords: =BDM<br />Every result will contain the word "BDM". The automatically generated synonym "Business Development Manager" will now not be included in the search criteria.</div>
            </div>
        </li>
        <li>
            <span class="title">Too few results?</span>
            <div class="content">
                <div><strong>Remove criteria:</strong> If you're getting too few results, you should broaden your search by gradually removing search criteria. For example, use fewer keywords; broaden your search distance to include other locations; or increase your salary band.</div>           
                <div>We suggest that you first <strong>remove criteria from the job title keyword fields</strong>. It can sometimes be quite tricky to match candidate job titles.</div>
                <div><strong>Make words optional with "OR":</strong> You can use also use the special term "OR" in the keywords fields to widen your net and get more results. (Alternatively, you can achieve the same result by using the "One or more of these words" fields).</div>
                <div>For example: Keywords: "Web designer" OR "graphic designer"<br />Every resume will contain the phrase "web designer" or "graphic designer".</div>
            </div>
        </li>
        <li>
            <span class="title">Don't miss out!</span>
            <div class="content">
                <div>The search results you see today are unlikely to be the same next week: hundreds of new candidates join LinkMe every day, change their details, or update their employment status. These changes will all affect your search results.</div>
                <div>You can ensure you never miss any new, relevant candidates using <strong>Saved searches</strong> and <strong>Email alerts</strong>. These can be set up easily after you do a Search.</div>
                <div><strong>Saved searches</strong> allow you to quickly repeat your favourite searches later. You can also select "<strong>Email me updates</strong>" for a Saved search to have LinkMe automatically conduct the search on your behalf every day and email you with just the newest or most recently updated results.</div>
                <div><strong>Contact candidates before anyone else:</strong> We encourage you to also reach out and <strong>contact "passive" candidates</strong> (i.e. those who are not looking but happy to talk). Who knows - they might be ready to move, or they might be persuaded to move for a new opportunity. You can grab them before anyone else even knows they're on the job market.</div>
            </div>
        </li>
    </ul>
    <div class="tips-link">For PDF version of Search Tips, please <a href="javascript:void(0);" class="js_openSearchTipsPDF">click here</a>.</div>
</div>
<script language="javascript">
    (function($) {

        /* Opening PDF Version */
        $(".js_openSearchTipsPDF").click(function() {
            window.open('<%= new ReadOnlyApplicationUrl("~/resources/employer/search/SearchTips.pdf") %>', "Search Tips");
        });

    })(jQuery);
</script>
</asp:Content>

