<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<SearchListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Converters"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models"%>
<%@ Import Namespace="LinkMe.Domain.Location"%>
<%@ Import Namespace="LinkMe.Domain.Location.Queries"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Unity"%>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Search"%>

<div class="area search">
    <div class="newsearch">
        <div class="icon"></div>
        <a href="<%= Html.RouteRefUrl(SearchRoutes.Search) %>">New search</a>
    </div>
    <div class="divider"></div>
    <div class="keywords collapsed">
        <div class="titlebar">
            <div class="icon"></div>
            <div class="title">Change keywords</div>
            <div class="arrow"></div>
        </div>
        <div class="content">
            <%= Html.MultilineTextBoxField(Model.Criteria, JobAdSearchCriteriaKeys.Keywords, c => c.GetKeywords()).WithLabel("Keywords").WithAttribute("data-watermark", "e.g. job title, skills").WithCssClass("autoexpand") %>
            <%= Html.MultilineTextBoxField(Model.Criteria, JobAdSearchCriteriaKeys.AdTitle, c => c.AdTitle).WithLabel("Job title").WithAttribute("data-watermark", "Within the job title").WithCssClass("autoexpand") %>
            <%= Html.TextBoxField(Model.Criteria, JobAdSearchCriteriaKeys.AdvertiserName, c => c.AdvertiserName).WithLabel("Advertiser").WithAttribute("data-watermark", "Within the advertiser name") %>
            <%= Html.CheckBoxField("RetainFilters", true).WithLabelOnRight("Retain current filter settings") %>
            <div class="button search a"></div>
            <div class="searchtips">Search tips</div>
        </div>
    </div>
    <div class="divider"></div>
    <div class="location collapsed">
        <div class="titlebar">
            <div class="icon"></div>
            <div class="title">Change location</div>
            <div class="arrow"></div>
        </div>
        <div class="content">
            <%= Html.TextBoxField(Model.Criteria, JobAdSearchCriteriaKeys.Location, c => c.Location).WithLabel("Location").WithAttribute("data-watermark", "e.g. Melbourne, VIC, 3000") %>
            <%= Html.DistanceField(Model.Criteria, JobAdSearchCriteriaKeys.Distance, c => c.Distance ?? Model.AncillaryData.DefaultDistance, Model.AncillaryData.Distances).WithLabel("Within").WithCssPrefix("Distance").WithAttribute("size", "9")%>
            <div class="radius">
                <span class="title">radius of</span>
                <span class="radiusof"><%= Model.Criteria.Location %></span>
            </div>
            <%= Html.CountryField(Model.Criteria, JobAdSearchCriteriaKeys.CountryId, c => c.Location == null ? Model.AncillaryData.DefaultCountry.Id : c.Location.Country.Id, Model.AncillaryData.Countries).WithLabel("Country").WithCssPrefix("Country").WithAttribute("size", "12") %>
            <%= Html.CheckBoxField("RetainFilters", true).WithLabelOnRight("Retain current filter settings") %>
            <div class="button search a"></div>
            <div class="searchtips">Search tips</div>
        </div>
    </div>
</div>
<% Html.RenderPartial("MyFavouriteJobs", Model); %>
<div class="area filter expanded">
    <div class="titlebar">
        <div class="icon"></div>
        <div class="title">Refine and filter results</div>
        <div class="arrow"></div>
    </div>
    <div class="content">
        <div class="divider withbg"></div>
        <div class="row savesearch">
            <div class="icon"></div>
            <div class="title">Save as a favourite search</div>
        </div>
        <div class="row emailalert">
            <div class="icon"></div>
            <div class="title">Email me similar jobs</div>
        </div>
        <div class="divider"></div>
        <div class="section jobtypes expanded">
            <div class="titlebar">
                <div class="checkbox checked"></div>
                <div class="title">Full-time, part-time etc.</div>
                <div class="arrow"></div>
            </div>
            <div class="content">
                <% foreach (var jt in new List<JobTypes> { JobTypes.FullTime, JobTypes.PartTime, JobTypes.Contract, JobTypes.Temp, JobTypes.JobShare }) { %>
                    <div class="jobtype <%= jt %>">
                        <div class="icon <%= (Model.Criteria.JobTypes & jt) == jt ? "checked" : "" %>"></div>
                        <div class="count">(<%= Model.Results.JobTypeHits[jt.ToString()] %>)</div>
                    </div>
                <% } %>
            </div>
        </div>
        <div class="divider"></div>
        <% var salaryFilterActivated = (Model.Criteria.Salary != null && (Model.Criteria.Salary.LowerBound != null || Model.Criteria.Salary.UpperBound != null)) || Model.Criteria.ExcludeNoSalary; %>
        <div class="section salary <%= salaryFilterActivated ? "expanded" : "collapsed" %>">
            <div class="titlebar">
                <div class="checkbox <%= salaryFilterActivated ? "checked" : "" %>"></div>
                <div class="title">Salary<span>(per annum)</span></div>
                <div class="arrow"></div>
            </div>
            <div class="content">
                <div class="salary-control">
		            <div class="salary-range">
			            <div class="left-range">$<%= Model.AncillaryData.MinSalary %></div>
			            <div class="right-range">$<%= Model.AncillaryData.MaxSalary.ToString("N0") %>+</div>
		            </div>
		            <div class="salary-slider-bg"></div>
		            <div class="salary-slider" minsalary="<%= Model.AncillaryData.MinSalary %>" maxsalary="<%= Model.AncillaryData.MaxSalary %>" stepsalary="<%= Model.AncillaryData.StepSalary %>"></div>
		            <div class="salary-slider-ruler"></div>
		            <div class="salary-desc"><center>Any salary</center></div>
		            <%= Html.TextBoxField(Model.Criteria, JobAdSearchCriteriaKeys.SalaryLowerBound, c => (c.Salary == null || c.Salary.LowerBound == null) ? Model.AncillaryData.MinSalary.ToString() : ((int)c.Salary.LowerBound.Value).ToString())%>
		            <%= Html.TextBoxField(Model.Criteria, JobAdSearchCriteriaKeys.SalaryUpperBound, c => (c.Salary == null || c.Salary.UpperBound == null) ? Model.AncillaryData.MaxSalary.ToString() : ((int)c.Salary.UpperBound.Value).ToString())%>
                </div>
                <%= Html.CheckBoxField(Model.Criteria, JobAdSearchCriteriaKeys.IncludeNoSalary, c => !c.ExcludeNoSalary).WithLabelOnRight("Include jobs without a specified salary") %>
            </div>
        </div>
        <div class="divider"></div>
        <% var recencyFilterActivated = Model.Criteria.Recency != null && Model.Criteria.Recency.Value.Days != Model.AncillaryData.DefaultRecency; %>
        <div class="section dateposted <%= recencyFilterActivated ? "expanded" : "collapsed" %>">
            <div class="titlebar">
                <div class="checkbox <%= recencyFilterActivated ? "checked" : "" %>"></div>
                <div class="title">Date posted</div>
                <div class="arrow"></div>
            </div>
            <div class="content">
                <div class="recency-control">
		            <div class="recency-range">
			            <div class="left-range"><%= Model.AncillaryData.Recencies[0].Description %></div>
			            <div class="right-range">60+ days</div>
		            </div>
		            <div class="recency-slider-bg"></div>
		            <div class="recency-slider" minrecency="0" maxrecency="<%= Model.AncillaryData.Recencies.Count - 1 %>" steprecency="1" defaultrecency="<%= Model.AncillaryData.DefaultRecency %>" recencies="<%= "[" + string.Join(",", (from r in Model.AncillaryData.Recencies select "{days: " + r.Days + ", label: '" + (r.Days == 60 ? "60+ days" : (r.Days == 30 ? "30 days" : r.Description)) + "'}").ToArray()) + "]"%>"></div>
		            <div class="recency-slider-ruler"></div>
		            <div class="recency-desc"><center></center></div>
		            <%= Html.TextBoxField(Model.Criteria, JobAdSearchCriteriaKeys.Recency, c => c.Recency == null ? Model.AncillaryData.DefaultRecency.ToString() : c.Recency.Value.Days.ToString()) %>
                </div>
            </div>
        </div>
        <div class="divider"></div>
        <% var distanceFilterActivated = Model.Criteria.Location != null && !(Model.Criteria.Location.IsCountry && Model.Criteria.Location.Country.Equals(Model.AncillaryData.DefaultCountry)); %>
        <div class="section distance <%= Model.Criteria.Location == null || Model.Criteria.Location.ToString().Equals(string.Empty) ? "emptyloc" : "" %> <%= distanceFilterActivated ? "expanded" : "collapsed" %>">
            <div class="titlebar">
                <div class="checkbox <%= distanceFilterActivated ? "checked" : "" %>"></div>
                <div class="title">Distance</div>
                <div class="arrow"></div>
            </div>
            <div class="content">
                <div class="emptyloc">
                    <div class="text">No location specified. <span>Enter a location</span> to filter jobs by distance</div>
                </div>
                <div class="withloc">
                    <div class="from">From <span class="location"><%= Model.Criteria.Location %></span> <span class="change">Change</span></div>
                    <div class="distance-control">
		                <div class="distance-range">
			                <div class="left-range">0 km</div>
			                <div class="right-range">1000+ km</div>
		                </div>
		                <div class="distance-slider-bg"></div>
		                <div class="distance-slider" mindistance="0" maxdistance="<%= Model.AncillaryData.Distances.Count - 1 %>" stepdistance="1" defaultdistance="<%= Model.AncillaryData.DefaultDistance %>" distances="<%= "[" + string.Join(",", (from d in Model.AncillaryData.Distances select d.ToString()).ToArray()) + "]" %>"></div>
		                <div class="distance-slider-ruler"></div>
		                <div class="distance-desc"><center></center></div>
		                <%= Html.TextBoxField(Model.Criteria, JobAdSearchCriteriaKeys.Distance, c => c.Distance == null ? Model.AncillaryData.DefaultDistance.ToString() : c.Distance.ToString()).WithId(JobAdSearchCriteriaKeys.Distance + "InFilter") %>
                    </div>
                </div>
            </div>
        </div>
        <div class="divider"></div>
        <% var industryFilterActivated = Model.Criteria.IndustryIds != null && Model.Criteria.IndustryIds.Count > 0; %>
        <div class="section industry <%= industryFilterActivated ? "expanded" : "collapsed" %>">
            <div class="titlebar">
                <div class="checkbox <%= industryFilterActivated ? "checked" : "" %>"></div>
                <div class="title">Industry</div>
                <div class="arrow"></div>
            </div>
            <div class="content">
                <div class="allindustry">
                    <div class="checkbox <%= Model.Criteria.IndustryIds == null ? "checked" : "" %>"></div>
                    <div class="title">All</div>
                    <div class="count"></div>
                </div>
                <% foreach (var i in Model.AncillaryData.Industries) { %>
                    <div class="industry" id="<%= i.Id %>">
                        <div class="checkbox <%= (Model.Criteria.IndustryIds != null && Model.Criteria.IndustryIds.Contains(i.Id)) ? "checked" : "" %>"></div>
                        <div class="title" title="<%= i.Name %>"><%= i.Name %></div>
                        <div class="count">(<%= (Model.Results.IndustryHits.ContainsKey(i.Id.ToString()) ? Model.Results.IndustryHits[i.Id.ToString()] : 0) %>)</div>
                    </div>
                <% } %>
                <div class="expander left">Show more ?</div>
                <div class="expander right">Show 10 more ?</div>
            </div>
        </div>
        <% if (CurrentMember != null) { %>
        <div class="divider"></div>
        <% var additionalFilterActivated = Model.Criteria.HasNotes != null || Model.Criteria.HasViewed != null || Model.Criteria.IsFlagged != null || Model.Criteria.HasApplied != null; %>
        <div class="section additional <%= additionalFilterActivated ? "expanded" : "collapsed" %>">
            <div class="titlebar">
                <div class="checkbox <%= additionalFilterActivated ? "checked" : "" %>"></div>
                <div class="title">Additional job filters</div>
                <div class="arrow"></div>
            </div>
            <div class="content">
                <div class="title">
                    <span>Either</span>
                    <span>Yes</span>
                    <span>No</span>
                </div>
                <div class="row notes">
                    <div class="icon"></div>
                    <div class="title">Has notes</div>
                    <input type="radio" id="HasNotesEither" name="notes" <%= Model.Criteria.HasNotes == null ? "checked='checked'" : "" %> value="" />
                    <input type="radio" id="HasNotesYes" name="notes" <%= true == Model.Criteria.HasNotes ? "checked='checked'" : "" %> value="True" />
                    <input type="radio" id="HasNotesNo" name="notes" <%= false == Model.Criteria.HasNotes ? "checked='checked'" : "" %> value="False" />
                </div>
                <div class="row viewed">
                    <div class="icon"></div>
                    <div class="title">Was viewed</div>
                    <input type="radio" id="HasViewedEither" name="viewed" <%= Model.Criteria.HasViewed == null ? "checked='checked'" : "" %> value="" />
                    <input type="radio" id="HasViewedYes" name="viewed" <%= true == Model.Criteria.HasViewed ? "checked='checked'" : "" %> value="True" />
                    <input type="radio" id="HasViewedNo" name="viewed" <%= false == Model.Criteria.HasViewed ? "checked='checked'" : "" %> value="False" />
                </div>
                <div class="row flagged">
                    <div class="icon"></div>
                    <div class="title">Is flagged</div>
                    <input type="radio" id="IsFlaggedEither" name="flagged" <%= Model.Criteria.IsFlagged == null ? "checked='checked'" : "" %> value="" />
                    <input type="radio" id="IsFlaggedYes" name="flagged" <%= true == Model.Criteria.IsFlagged ? "checked='checked'" : "" %> value="True" />
                    <input type="radio" id="IsFlaggedNo" name="flagged" <%= false == Model.Criteria.IsFlagged ? "checked='checked'" : "" %> value="False" />
                </div>
                <div class="row applied">
                    <div class="icon"></div>
                    <div class="title">Was applied for</div>
                    <input type="radio" id="HasAppliedEither" name="applied" <%= Model.Criteria.HasApplied == null ? "checked='checked'" : "" %> value="" />
                    <input type="radio" id="HasAppliedYes" name="applied" <%= true == Model.Criteria.HasApplied ? "checked='checked'" : "" %> value="True" />
                    <input type="radio" id="HasAppliedNo" name="applied" <%= false == Model.Criteria.HasApplied ? "checked='checked'" : "" %> value="False" />
                </div>
            </div>
        </div>
        <% } %>
        <div class="divider"></div>
        <div class="row reset">
            <div class="icon"></div>
            <div class="title">Reset all filters</div>
        </div>
    </div>
</div>
<% Html.RenderPartial("HiddenJobs", Model); %>