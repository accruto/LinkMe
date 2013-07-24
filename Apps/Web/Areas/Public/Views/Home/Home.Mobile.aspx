<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<HomeModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Api.Routes" %>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Converters"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Home"%>
<%@ Import Namespace="LinkMe.Web.Content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.DeviceSearch) %>
    </mvc:RegisterStyleSheets>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.RenderScripts(ScriptBundles.DeviceSearch)%>
    </mvc:RegisterJavaScripts>
    
    <script type="text/javascript">
        var apiPartialMatchesUrl = "<%= Html.RouteRefUrl(LocationRoutes.PartialMatches) %>" + "?countryId=<%= Model.Reference.DefaultCountry.Id %>&location=";
        var apiLocationClosestUrl = "<%= Html.RouteRefUrl(LocationRoutes.ClosestLocation) %>" + "?countryId=<%= Model.Reference.DefaultCountry.Id %>&";
    </script>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <form action="<%= LinkMe.Web.Areas.Members.Routes.SearchRoutes.Search.GenerateUrl() %>" method="post" id="search">
        <%= Html.TextBoxField(JobAdSearchCriteriaKeys.Keywords, string.Empty).WithAttribute("watermark", "e.g. job title, skills") %>
        <%= Html.TextBoxField(JobAdSearchCriteriaKeys.Location, string.Empty).WithAttribute("watermark", "e.g. Melbourne, VIC").WithCssPrefix("location").WithAttribute("autocomplete", "off") %>
        <div class="mylocation"></div>
        <%= Html.CountryField(new { i = Model.Reference.DefaultCountry.Id }, JobAdSearchCriteriaKeys.CountryId, c => c.i, from c in Model.Reference.Countries where c.Id == Model.Reference.DefaultCountry.Id select c).WithCssPrefix("Country") %>
        <div class="salarydesc"></div>
        <%= Html.RadioButtonsField(new { sr = SalaryRate.None }, JobAdSearchCriteriaKeys.SalaryRate, c => c.sr).With(new[] { SalaryRate.Year, SalaryRate.Hour })
            .WithId(SalaryRate.Year, "SalaryRateYear").WithId(SalaryRate.Hour, "SalaryRateHour").WithCssPrefix("salaryrate")
            .WithLabel("").WithLabel(SalaryRate.Year, " ").WithLabel(SalaryRate.Hour, " ") %>
        <div class="field salaryslider_field">
            <div class="control">
                <div class="sliderbg"></div>
                <div class="salaryslider"></div>
            </div>
        </div>
        <div class="field salaryrange_field">
            <div class="control">
                <div class="salaryrange" data-range='{ "SalaryRate<%= SalaryRate.Year %>" : { "MinSalary" : "<%= Model.Reference.MinSalary %>", "MaxSalary" : "<%= Model.Reference.MaxSalary %>", "StepSalary" : "<%= Model.Reference.StepSalary %>" }, "SalaryRate<%= SalaryRate.Hour %>" : { "MinSalary" : "<%= Model.Reference.MinHourlySalary %>", "MaxSalary" : "<%= Model.Reference.MaxHourlySalary %>", "StepSalary" : "<%= Model.Reference.StepHourlySalary %>" } }'>
                    <div class="left"></div>
                    <div class="right"></div>
                </div>
                <%= Html.Hidden(JobAdSearchCriteriaKeys.SalaryLowerBound)%>
                <%= Html.Hidden(JobAdSearchCriteriaKeys.SalaryUpperBound)%>
            </div>
        </div>
        <%= Html.ButtonsField().Add(new SubmitButton("SEARCH")) %>
    </form>

    <div class="clearer"></div>

    <% Html.RenderPartial("GoogleHorizontalAds"); %>

</asp:Content>