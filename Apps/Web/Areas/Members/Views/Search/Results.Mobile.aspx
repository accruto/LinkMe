<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<SearchListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search" %>
<%@ Import Namespace="LinkMe.Framework.Utility" %>
<%@ Import Namespace="LinkMe.Query.JobAds" %>
<%@ Import Namespace="LinkMe.Web.Areas.Api.Routes" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Converters"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Search"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.DeviceResults) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.RenderScripts(ScriptBundles.Site) %>
        <%= Html.RenderScripts(ScriptBundles.DeviceResults) %>
    </mvc:RegisterJavaScripts>
    
    <script type="text/javascript">
        $("meta[name='viewport']").attr("content", "width=device-width,initial-scale=1,user-scalable=no");
        var apiPartialMatchesUrl = "<%= Html.RouteRefUrl(LocationRoutes.PartialMatches) %>" + "?countryId=<%= Model.AncillaryData.DefaultCountry.Id %>&location=";
        var apiLocationClosestUrl = "<%= Html.RouteRefUrl(LocationRoutes.ClosestLocation) %>" + "?countryId=<%= Model.AncillaryData.DefaultCountry.Id %>&";
        var industries = <%= "[" + string.Join(",", (from i in Model.Industries select "{Id: '" + i.Id + "', Name: '" + i.Name + "'}").ToArray()) + "]"%>;
        var jobTypes = { FullTime: 1, PartTime: 2, Contract: 4, Temp: 8, JobShare: 16 };
        var criteria = {}, resetCriteria = {};

<%  if (!string.IsNullOrEmpty(Model.Criteria.GetKeywords()))
    { %>
        criteria["<%= JobAdSearchCriteriaKeys.Keywords %>"] = "<%= Model.Criteria.GetKeywords().Replace("\"", "\\\"") %>";
        resetCriteria["<%= JobAdSearchCriteriaKeys.Keywords %>"] = "<%= Model.Criteria.GetKeywords().Replace("\"", "\\\"") %>";
<%  }
    if (Model.Criteria.Location != null)
    { %>
        criteria["<%= JobAdSearchCriteriaKeys.Location %>"] = "<%= Model.Criteria.Location %>";
        criteria["<%= JobAdSearchCriteriaKeys.Distance %>"] = "<%= Model.Criteria.Distance == 0 ? Model.AncillaryData.DefaultDistance : Model.Criteria.Distance %>";
        criteria["<%= JobAdSearchCriteriaKeys.CountryId %>"] = "<%= Model.Criteria.Location == null ? Model.AncillaryData.DefaultCountry.Id : Model.Criteria.Location.Country.Id %>";
        resetCriteria["<%= JobAdSearchCriteriaKeys.Location %>"] = "<%= Model.Criteria.Location %>";
        resetCriteria["<%= JobAdSearchCriteriaKeys.Distance %>"] = "<%= Model.Criteria.Distance == 0 ? Model.AncillaryData.DefaultDistance : Model.Criteria.Distance %>";
        resetCriteria["<%= JobAdSearchCriteriaKeys.CountryId %>"] = "<%= Model.Criteria.Location == null ? Model.AncillaryData.DefaultCountry.Id : Model.Criteria.Location.Country.Id %>";
<%  }
    if (Model.Criteria.JobTypes != JobTypes.All)
    { %>
        criteria["<%= JobAdSearchCriteriaKeys.JobTypes %>"] = "<%= Model.Criteria.JobTypes %>";
        resetCriteria["<%= JobAdSearchCriteriaKeys.JobTypes %>"] = "<%= Model.Criteria.JobTypes %>";
<%  }
    if (Model.Criteria.Salary != null)
    { %>
        criteria["<%= JobAdSearchCriteriaKeys.SalaryRate %>"] = "<%= Model.Criteria.Salary.Rate %>";
        resetCriteria["<%= JobAdSearchCriteriaKeys.SalaryRate %>"] = "<%= Model.Criteria.Salary.Rate %>";
<%  }
    if (Model.Criteria.Salary != null && Model.Criteria.Salary.LowerBound != null)
    { %>
        criteria["<%= JobAdSearchCriteriaKeys.SalaryLowerBound %>"] = "<%= ((int)Model.Criteria.Salary.LowerBound.Value) %>";
        resetCriteria["<%= JobAdSearchCriteriaKeys.SalaryLowerBound %>"] = "<%= ((int)Model.Criteria.Salary.LowerBound.Value) %>";
<%  }
    if (Model.Criteria.Salary != null && Model.Criteria.Salary.UpperBound != null)
    { %>
        criteria["<%= JobAdSearchCriteriaKeys.SalaryUpperBound %>"] = "<%= ((int)Model.Criteria.Salary.UpperBound.Value) %>";
        resetCriteria["<%= JobAdSearchCriteriaKeys.SalaryUpperBound %>"] = "<%= ((int)Model.Criteria.Salary.UpperBound.Value) %>";
<%  }
    if (Model.Criteria.ExcludeNoSalary)
    { %>
        criteria["<%= JobAdSearchCriteriaKeys.IncludeNoSalary %>"] = "<%= !Model.Criteria.ExcludeNoSalary %>";
        resetCriteria["<%= JobAdSearchCriteriaKeys.IncludeNoSalary %>"] = "<%= !Model.Criteria.ExcludeNoSalary %>";
<%  }
    if (Model.Criteria.Recency != null)
    { %>
        criteria["<%= JobAdSearchCriteriaKeys.Recency %>"] = "<%= Model.Criteria.Recency == null ? Model.AncillaryData.DefaultRecency : Model.Criteria.Recency.Value.Days %>";
        resetCriteria["<%= JobAdSearchCriteriaKeys.Recency %>"] = "<%= Model.Criteria.Recency == null ? Model.AncillaryData.DefaultRecency : Model.Criteria.Recency.Value.Days %>";
<%  }
    if (!Model.Criteria.IndustryIds.IsNullOrEmpty())
    { %>
        criteria["<%= JobAdSearchCriteriaKeys.IndustryIds %>"] = <%= "[" + string.Join(",", (from id in Model.Criteria.IndustryIds select "\"" + id + "\"").ToArray()) + "]" %>;
        resetCriteria["<%= JobAdSearchCriteriaKeys.IndustryIds %>"] = <%= "[" + string.Join(",", (from id in Model.Criteria.IndustryIds select "\"" + id + "\"").ToArray()) + "]" %>;
<%  }
    if (ViewData.ModelState.ContainsKey(ModelStateKeys.Confirmation) && ViewData.ModelState[ModelStateKeys.Confirmation].Errors.Count > 0 && "SHOW SAVED NOTIFICATION".Equals(ViewData.ModelState[ModelStateKeys.Confirmation].Errors[0].ErrorMessage))
    { %>
        var showSavedNotification = true;
<%  }
    else
    { %>
        var showSavedNotification = false;
<%  }
    if (ViewData.ModelState.ContainsKey(ModelStateKeys.Confirmation) && ViewData.ModelState[ModelStateKeys.Confirmation].Errors.Count > 0 && "SHOW ADDED NOTIFICATION".Equals(ViewData.ModelState[ModelStateKeys.Confirmation].Errors[0].ErrorMessage))
    { %>
        var showAddedNotification = true;
<%  }
    else
    { %>
        var showAddedNotification = false;
<%  } %>

        (function($) {
            $(document).ready(function() {
                linkme.members.jobads.api.setUrls({
                    addJobAdsToMobileFolder: "<%= JobAdsRoutes.ApiAddJobAdsToMobileFolder.GenerateUrl() %>",
                    removeJobAdsFromMobileFolder: "<%= JobAdsRoutes.ApiRemoveJobAdsFromMobileFolder.GenerateUrl() %>"
                });
            });
        })(jQuery);

    </script>
</asp:Content>


<asp:Content ContentPlaceHolderID="Body" runat="server">
    <div class="notification saved">
        <div class="title">Your search has been saved.</div>
        <div class="desc">You can find your favourite searches here</div>
    </div>
    <div class="notification added">
        <div class="title">Your job has been saved.</div>
        <div class="desc">You can find your saved jobs here</div>
    </div>
    <div class="results" data-apiurl="<%= Html.MungeUrl(Html.RouteRefUrl(LinkMe.Web.Areas.Members.Routes.SearchRoutes.ApiSearch)) %>">
        <% Html.RenderPartial("JobAdList", Model); %>
        <% Html.RenderPartial("EmptyList", Model); %>
    </div>
    <div class="buttons">
        <div class="wrapper">
            <div class="showmorejobs" data-currentpage="<%= Model.Presentation.Pagination.Page %>"><div class="loading"></div>Show more jobs</div>
        </div>
        <div class="wrapper">
            <%= Html.RouteRefLink(Model.IsSavedSearch ? "SEARCH SAVED" : "SAVE AS FAVOURITE SEARCH", LinkMe.Web.Areas.Members.Routes.SearchRoutes.SaveSearch, null, new { @class = "saveasfavourite " + (Model.IsSavedSearch ? "issaved" : "") }) %>
        </div>
    </div>
    <div class="clearer"></div>
    <% Html.RenderPartial("GoogleHorizontalAds"); %>
    <div id="filterbar">
        <div class="divider one"></div>
        <div class="divider two"></div>
        <div class="container">
            <div class="wrapper">
                <div class="button filter"><div class="icon"></div>Filter</div>
            </div>
            <div class="wrapper">
                <div class="button sort" data-sortorder='{<%= string.Join(",", (from kv in (from s in Model.SortOrders where !s.Equals(JobAdSortOrder.Flagged) select s).ToDictionary(s => s, s => s.GetDisplayText()) select "\"" + kv.Key + "\":\"" + kv.Value + "\"").ToArray()) %>}' data-current="<%= Model.Criteria.SortCriteria.SortOrder %>"><div class="icon"></div>Sort</div>
            </div>
        </div>
        <div class="divider three"></div>
    </div>
    <div id="filter">
        <header>
            <div class="divider one"></div>
            <div class="divider two"></div>
            <div class="divider three"></div>
            <div class="container">
                <div class="wrapper">
                    <div class="button reset"></div>
                </div>
                <div class="wrapper title">
                    <div class="title">
                        <div class="icon filter"></div>
                        Filters
                    </div>
                </div>
                <div class="wrapper">
                    <div class="button back"></div>
                </div>
            </div>
            <div class="divider four"></div>
        </header>
        <div class="tabs">
            <div class="tab Distance active"><div class="wrapper"><div class="icon"></div>Distance</div></div>
            <div class="tab Salary"><div class="wrapper"><div class="icon"></div>Salary</div></div>
            <div class="tab JobType"><div class="wrapper"><div class="icon"></div>JobType</div></div>
            <div class="tab Recency"><div class="wrapper"><div class="icon"></div>Date</div></div>
            <div class="tab Industry"><div class="wrapper"><div class="icon"></div>Industry</div></div>
        </div>
        <div class="tabcontent">
            <div class="content Distance active">
                <div class="title">Distance</div>
                <%= Html.TextBoxField(Model.Criteria, JobAdSearchCriteriaKeys.Location, c => c.Location).WithAttribute("watermark", "e.g. Melbourne, VIC").WithCssPrefix("location").WithAttribute("autocomplete", "off")%>
                <div class="field"><div class="control"><div class="checkbox mylocation"></div><div class="title">Use my current location</div></div></div>
                <div class="desc field"><span class="red"><%= Model.AncillaryData.DefaultDistance %> km</span>&nbsp;from&nbsp;<span class="location"><%= Model.Criteria.Location %></span></div>
                <div class="field distanceslider_field">
                    <div class="control">
                        <div class="sliderbg"></div>
                        <div class="distanceslider"></div>
                    </div>
                </div>
                <div class="field distancerange_field">
                    <div class="control">
                        <div class="distancerange" data-mindistance="0" data-maxdistance="<%= Model.AncillaryData.Distances.Count - 1 %>" data-stepdistance="1" data-defaultdistance="<%= Model.Criteria.Distance ?? Model.AncillaryData.DefaultDistance %>" data-distances="<%= "[" + string.Join(",", (from d in Model.AncillaryData.Distances select d).ToArray()) + "]" %>">
                            <div class="left">0 km</div>
                            <div class="right">1,000+ km</div>
                        </div>
                        <%= Html.TextBoxField(Model.Criteria, JobAdSearchCriteriaKeys.Distance, c => c.Distance ?? Model.AncillaryData.DefaultDistance) %>
                    </div>
                </div>
            </div>
            <div class="content Salary">
                <%= Html.RadioButtonsField(Model.Criteria, JobAdSearchCriteriaKeys.SalaryRate, c => c.Salary == null ? SalaryRate.Year : c.Salary.Rate).With(new[] { SalaryRate.Year, SalaryRate.Hour })
                    .WithId(SalaryRate.Year, "SalaryRateYear").WithId(SalaryRate.Hour, "SalaryRateHour").WithCssPrefix("salaryrate")
                    .WithLabel("").WithLabel(SalaryRate.Year, " ").WithLabel(SalaryRate.Hour, " ") %>
                <div class="title">Salary&nbsp;<span class="salaryrate"><%= Model.Criteria.Salary == null || Model.Criteria.Salary.Rate == SalaryRate.Year ? "(per annum)" : "(per hour)" %></span></div>
                <div class="field">
                    <div class="control">
                        <div class="salarydesc"></div>
                    </div>
                </div>
                <div class="field salaryslider_field">
                    <div class="control">
                        <div class="sliderbg"></div>
                        <div class="salaryslider"></div>
                    </div>
                </div>
                <div class="field salaryrange_field">
                    <div class="control">
                        <div class="salaryrange" data-range='{ "SalaryRate<%= SalaryRate.Year %>" : { "MinSalary" : "<%= Model.AncillaryData.MinSalary %>", "MaxSalary" : "<%= Model.AncillaryData.MaxSalary %>", "StepSalary" : "<%= Model.AncillaryData.StepSalary %>" }, "SalaryRate<%= SalaryRate.Hour %>" : { "MinSalary" : "<%= Model.AncillaryData.MinHourlySalary %>", "MaxSalary" : "<%= Model.AncillaryData.MaxHourlySalary %>", "StepSalary" : "<%= Model.AncillaryData.StepHourlySalary %>" } }'>
                            <div class="left"></div>
                            <div class="right"></div>
                        </div>
		                <%= Html.TextBoxField(Model.Criteria, JobAdSearchCriteriaKeys.SalaryLowerBound, c => (c.Salary == null || c.Salary.LowerBound == null) ? Model.AncillaryData.MinSalary : ((int)c.Salary.LowerBound.Value))%>
		                <%= Html.TextBoxField(Model.Criteria, JobAdSearchCriteriaKeys.SalaryUpperBound, c => (c.Salary == null || c.Salary.UpperBound == null) ? Model.AncillaryData.MaxSalary : ((int)c.Salary.UpperBound.Value))%>
                    </div>
                </div>
                <%= Html.CheckBoxField(Model.Criteria, JobAdSearchCriteriaKeys.IncludeNoSalary, c => !c.ExcludeNoSalary).WithLabelOnRight("Include jobs without a specified salary") %>
            </div>
            <div class="content JobType">
                <div class="title">Job-type</div>
                <% foreach (var jt in new List<JobTypes> { JobTypes.FullTime, JobTypes.PartTime, JobTypes.Contract, JobTypes.Temp, JobTypes.JobShare }) { %>
                    <div class="wrapper <%= jt %>"><div class="jobtype <%= (Model.Criteria.JobTypes & jt) == jt ? "checked" : "" %>"></div></div>
                <% } %>
            </div>
            <div class="content Recency">
                <div class="title">Date posted</div>
                <div class="field">
                    <div class="control">
                        <div class="recencydesc"></div>
                    </div>
                </div>
                <div class="field recencyslider_field">
                    <div class="control">
                        <div class="sliderbg"></div>
                        <div class="recencyslider"></div>
                    </div>
                </div>
                <div class="field recencyrange_field">
                    <div class="control">
                        <div class="recencyrange" data-minrecency="0" data-maxrecency="<%= Model.AncillaryData.Recencies.Count - 1 %>" data-steprecency="1" data-defaultrecency="<%= Model.AncillaryData.DefaultRecency %>" data-recencies="<%= "[" + string.Join(",", (from r in Model.AncillaryData.Recencies select "{days: " + r.Days + ", label: '" + (r.Days == 60 ? "60+ days" : (r.Days == 30 ? "30 days" : r.Description)) + "'}").ToArray()) + "]"%>">
                            <div class="left"><%= Model.AncillaryData.Recencies[0].Description %></div>
                            <div class="right">60+ days</div>
                        </div>
                        <%= Html.TextBoxField(Model.Criteria, JobAdSearchCriteriaKeys.Recency, c => c.Recency == null ? Model.AncillaryData.DefaultRecency : c.Recency.Value.Days) %>
                    </div>
                </div>
            </div>
            <div class="content Industry">
                <div class="title">Industry</div>
                <div class="field">
                    <div class="control">
                        <div class="button choose">Choose industries<div class="cell"><div class="rightarrow"></div></div></div>
                    </div>
                </div>
            </div>
        </div>
        <div id="AllIndustriesList">
            <div class="allindustries">
                <div class="cell icon"><div class="checkbox <%= Model.Criteria.IndustryIds.IsNullOrEmpty() ? "checked" : "" %>"></div></div>
                <div class="cell text"><div class="title">All industries</div></div>
            </div>
            <% foreach (var i in Model.AncillaryData.Industries) { %>
                <div class="industry" id="<%= i.Id %>">
                    <div class="cell icon"><div class="checkbox <%= (Model.Criteria.IndustryIds != null && Model.Criteria.IndustryIds.Contains(i.Id)) ? "checked" : "" %>"></div></div>
                    <div class="cell text"><div class="title"><%= i.Name %>&nbsp;<span class="count">(<%= (Model.Results.IndustryHits.ContainsKey(i.Id.ToString()) ? Model.Results.IndustryHits[i.Id.ToString()] : 0) %>)</span></div></div>
                </div>
            <% } %>
        </div>
    </div>
</asp:Content>