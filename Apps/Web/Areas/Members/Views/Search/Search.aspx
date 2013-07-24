<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<SearchModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Converters"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Search"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Views" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.RenderStyles(StyleBundles.JobSearch)%>
    </mvc:RegisterStyleSheets>
</asp:Content>
<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom) %>
        <%= Html.RenderScripts(ScriptBundles.JobSearch)%>
    </mvc:RegisterJavaScripts>
    
    <script language="javascript" type="text/javascript">
        var apiPartialMatchesUrl = "<%= Html.RouteRefUrl(LinkMe.Web.Areas.Api.Routes.LocationRoutes.PartialMatches) %>" + "?countryId=1&location=";
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="PageSubHeader" runat="server">
    <div class="header">Find jobs</div>
    <ul class="breadcrumbs">
        <li>Candidate site</li>
        <li>Jobs</li>
    </ul>    
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="tabs">
        <div class="tab Search"></div>
        <a class="tab Industry" target="_blank" href="<%= Html.RouteRefUrl(JobAdsRoutes.BrowseJobAds) %>"></a>
        <a class="tab Location" target="_blank" href="<%= Html.RouteRefUrl(JobAdsRoutes.BrowseJobAds) %>"></a>
        <% if (CurrentMember != null) { %>
            <a class="icon recent" href="<%= Html.RouteRefUrl(SearchRoutes.RecentSearches) %>"></a>
        <% } %>
    </div>
    <div class="tabcontent">
        <div class="topbar"></div>
        <div class="tab Search">
            <div class="quick active">
                <div class="row one">
                    <form action="" method="post" id="quickSearch">
                        <%= Html.TextBoxField(JobAdSearchCriteriaKeys.Keywords, string.Empty).WithLabel("Keywords").WithAttribute("data-watermark", "e.g. job title, skills")%>
                        <%= Html.TextBoxField(JobAdSearchCriteriaKeys.Location, string.Empty).WithLabel("Location").WithAttribute("data-watermark", "e.g. Melbourne, VIC, 3000")%>
                        <%= Html.CountryField(Model.Criteria, JobAdSearchCriteriaKeys.CountryId, c => c.Location.Country.Id, Model.AncillaryData.Countries).WithId(JobAdSearchCriteriaKeys.CountryId + "Quick").WithLabel("Country").WithCssPrefix("Country").WithAttribute("size", "12") %>
                        <input type="submit" id="search" name="search" class="button search" value="" />
                    </form>
                </div>
                <div class="row two">
                    <div class="explaination">
                        <span class="title">All the keywords above must appear in either the job ad title or job description.</span>
                        <span class='hidepart'>
                            <br />
                            <ul>
                                <li>Use "double quotes" to denote an exact phrase<br />e.g. "financial controller"</li>
                                <li>Use OR to allow either one of several words or phrases<br />e.g. financial controller OR manager OR accountant</li>
                                <li>Use NOT to exclude words or phrases<br />e.g. "financial controller" NOT assistant</li>
                            </ul>
                        </span>
                        <span class="moreorlessholder less"><span class="ellipsis">...</span><span class="arrow">▼</span><span class="moreorlesstext">Read more</span></span>
                    </div>
                    <div class="toggle advsearch">
                        <span><b>Advanced search</b></span>
                        <div class="button expand"></div>
                    </div>
                </div>
            </div>
            <div class="advanced">
                <form action="" method="post" id="advancedSearch">
                    <div class="col one">
                        <div class="area keywords">
                            <label class="title">Keywords</label>
                            <%= Html.TextBoxField(JobAdSearchCriteriaKeys.Keywords, string.Empty).WithId(JobAdSearchCriteriaKeys.Keywords + "Advanced").WithLabel("").WithAttribute("data-watermark", "e.g. job title, skills")%>
                            <div class="explaination">
                                <span class="title">All the keywords above must appear in either the job ad title or job description.</span>
                                <br />
                                <ul>
                                    <li>Use "double quotes" to denote an exact phrase<br />e.g. "financial controller"</li>
                                    <li>Use OR to allow either one of several words or phrases<br />e.g. financial controller OR manager OR accountant</li>
                                    <li>Use NOT to exclude words or phrases<br />e.g. "financial controller" NOT assistant</li>
                                </ul>
                            </div>
                            <%= Html.TextBoxField(JobAdSearchCriteriaKeys.AdTitle, string.Empty).WithLabel("Job title").WithAttribute("data-watermark", "Within the job title")%>
                            <%= Html.TextBoxField(JobAdSearchCriteriaKeys.AdvertiserName, string.Empty).WithLabel("Advertiser").WithAttribute("data-watermark", "Within the advertiser name")%>
                        </div>
                        <div class="area salary">
                            <label class="title">Desired Salary <span>(per annum)</span></label>
                            <div class="salary-control">
					            <div class="salary-range">
						            <div class="left-range">$<%= Model.AncillaryData.MinSalary %></div>
						            <div class="right-range">$<%= Model.AncillaryData.MaxSalary.ToString("N0") %>+</div>
					            </div>
					            <div class="salary-slider-bg"></div>
					            <div class="salary-slider" minsalary="<%= Model.AncillaryData.MinSalary %>" maxsalary="<%= Model.AncillaryData.MaxSalary %>" stepsalary="<%= Model.AncillaryData.StepSalary %>"></div>
					            <div class="salary-slider-ruler"></div>
					            <div class="salary-desc"><center>Any salary</center></div>
					            <%= Html.TextBoxField(Model.Criteria, JobAdSearchCriteriaKeys.SalaryLowerBound, c => (c.Salary == null || c.Salary.LowerBound == null) ? null : ((int) c.Salary.LowerBound.Value).ToString()) %>
 					            <%= Html.TextBoxField(Model.Criteria, JobAdSearchCriteriaKeys.SalaryUpperBound, c => (c.Salary == null || c.Salary.UpperBound == null) ? null : ((int)c.Salary.UpperBound.Value).ToString())%>
                            </div>
                            <%= Html.CheckBoxField(JobAdSearchCriteriaKeys.IncludeNoSalary, true).WithLabelOnRight("Include jobs without a specified salary") %>
                       </div>
                    </div>
                    <div class="col two">
                        <div class="area location">
                            <label class="title">Location</label>
                            <%= Html.TextBoxField(JobAdSearchCriteriaKeys.Location, string.Empty).WithId(JobAdSearchCriteriaKeys.Location + "Advanced").WithLabel("").WithAttribute("data-watermark", "e.g. Melbourne, VIC, 3000") %>
                            <%= Html.DistanceField(Model.Criteria, JobAdSearchCriteriaKeys.Distance, c => c.Distance ?? Model.AncillaryData.DefaultDistance, Model.AncillaryData.Distances).WithLabel("Within").WithCssPrefix("Distance").WithAttribute("size", "9")%>
                            <div class="radius">
                                <span class="title">radius of</span>
                                <span class="radiusof"></span>
                            </div>
                            <%= Html.CountryField(Model.Criteria, JobAdSearchCriteriaKeys.CountryId, c => c.Location.Country.Id, Model.AncillaryData.Countries).WithLabel("Country").WithCssPrefix("Country").WithAttribute("size", "12") %>
                        </div>
                        <div class="area jobtype">
                            <label class="title">Job type</label>
                            <%= Html.CheckBoxesField(Model.Criteria, c => c.JobTypes).Without(JobTypes.All).Without(JobTypes.None).WithLabel(JobTypes.FullTime, "Full time").WithLabel(JobTypes.PartTime, "Part time").WithLabel(JobTypes.JobShare, "Job share") %>
                        </div>
                        <div class="area industry">
                            <label class="title">Industry</label>
                            <%= Html.IndustriesField(Model.Criteria, JobAdSearchCriteriaKeys.IndustryIds, c => c.IndustryIds, Model.AncillaryData.Industries).WithId("IndustriesAdvanced").WithLabel("")%>
                        </div>
                    </div>
                    <input type="submit" id="searchAdvanced" name="search" class="button search" value="" />
                    <div class="toggle quicksearch">
                        <span><b>Quick search</b></span>
                        <div class="button collapse"></div>
                    </div>
                </form>
            </div>
        </div>
        <div class="tab Industry">
            <form action="" method="post" id="browseByIndustry">
                <%= Html.IndustriesField(Model.Criteria, JobAdSearchCriteriaKeys.IndustryIds, c => c.IndustryIds, Model.AncillaryData.Industries).WithId("IndustriesIndustry").WithLabel("Select an industry")%>
                <div class="row one"></div>
                <div class="row two"></div>
                <div class="row three"></div>
                <div class="row four"></div>
                <div class="row five"></div>
                <div class="row six"></div>
                <div class="multicheck">
                    <div class="selectall">
                        <div class="icon"></div>
                        <div class="text">Select all</div>
                    </div>
                    <div class="divider light"></div>
                    <div class="divider"></div>
                    <div class="clearall">
                        <div class="icon"></div>
                        <div class="text">Clear all</div>
                    </div>
                </div>
                <div class="nowenter">
                    <%= Html.TextBoxField(JobAdSearchCriteriaKeys.Location, string.Empty).WithId(JobAdSearchCriteriaKeys.Location + "Industry").WithLabel("Now enter a location").WithAttribute("data-watermark", "e.g. Melbourne, VIC, 3000")%>
                    <%= Html.CountryField(Model.Criteria, JobAdSearchCriteriaKeys.CountryId, c => c.Location.Country.Id, Model.AncillaryData.Countries).WithId(JobAdSearchCriteriaKeys.CountryId + "Industry").WithLabel("Country").WithCssPrefix("Country").WithAttribute("size", "12") %>
                    <input type="submit" id="searchIndustry" name="search" class="button search" value="" />
                </div>
            </form>
       </div>
        <div class="tab Location">
            <div class="map_field field">
                <label>Select a location</label>
                <div class="aus-map">
<%      foreach (var subdivision in Model.AncillaryData.CountrySubdivisions)
{
    if (subdivision.Name == "Australian Capital Territory") continue;
    %>	                    
                    <div class="state" state="<%= subdivision.Name %>"></div>
<%      } %>
                    <div class="region" region="Australian Capital Territory"></div>
<%      foreach (var region in Model.AncillaryData.Regions)
{ %>	                    
                    <div class="region" region="<%= region.Name %>"></div>
<%      } %>
                    <div class="pin"></div>
                    <img usemap="#mapAus" src="data:image/gif;base64,R0lGODlhAQABAJH/AP///wAAAMDAwAAAACH5BAEAAAIALAAAAAABAAEAAAICVAEAOw==" />
                    <map name="mapAus" >
	                    <area shape="poly" href="javascript:void(0);" coords="171,42,156,32,126,55,120,61,121,70,115,78,99,84,81,88,61,99,53,132,72,178,80,178,81,182,81,191,73,194,69,196,68,202,80,209,89,210,103,200,126,198,171,180" areafor="Western Australia" />
	                    <area shape="poly" href="javascript:void(0);" coords="174,40,180,35,187,32,201,31,201,25,211,24,210,14,220,18,229,13,233,19,233,34,222,40,241,53,241,132,173,132" areafor="Northern Territory" />
	                    <area shape="poly" href="javascript:void(0);" coords="174,135,263,135,263,171,241,171,241,194,263,194,263,234,258,234,254,229,253,221,247,215,238,219,232,219,230,216,235,214,238,214,241,215,245,213,249,214,250,210,254,205,251,202,246,205,243,206,241,204,239,212,233,213,234,209,239,194,225,210,221,206,212,195,206,187,192,182,174,183" areafor="South Australia" />
	                    <area shape="poly" href="javascript:void(0);" coords="244,52,256,51,257,59,265,59,268,52,270,42,271,18,278,4,290,33,299,38,305,59,307,73,325,82,338,99,353,124,358,121,358,125,355,128,353,124,349,144,352,138,357,138,359,141,353,141,347,142,347,150,349,153,343,156,330,152,326,155,266,155,266,131,244,131" areafor="Queensland" />
	                    <area shape="poly" href="javascript:void(0);" coords="267,160,327,160,329,157,339,157,345,160,348,160,348,156,350,155,359,153,361,161,352,190,347,192,333,192,333,198,303,198,303,208,326,208,326,198,333,198,333,206,341,206,336,217,334,230,330,230,320,225,317,219,295,219,275,204,267,203,267,194,302,194,302,189,313,191,302,179,302,171,267,171" areafor="New South Wales" />
	                    <area shape="poly" href="javascript:void(0);" coords="266,207,275,208,295,224,301,221,318,223,321,230,333,235,331,238,318,241,307,249,302,248,295,244,296,239,302,238,306,236,297,229,292,237,296,239,285,247,271,244,266,240" areafor="Victoria" />
	                    <area shape="poly" href="javascript:void(0);" coords="287,247,292,257,301,261,304,272,305,281,311,275,312,260,315,258,316,249,320,255,320,270,315,280,309,281,302,281,297,275,273,274,273,266,293,265,291,257,285,252" areafor="Tasmania" />
	                    <area shape="poly" href="javascript:void(0);" coords="184,9,182,15,188,16,195,12,200,13,199,18,188,17,180,35,187,32,201,31,201,25,211,25,211,14,204,13,200,8,187,11" areafor="Darwin" />
	                    <area shape="poly" href="javascript:void(0);" coords="71,175,79,175,80,179,80,188,72,189,73,180" areafor="Perth" />
	                    <area shape="poly" href="javascript:void(0);" coords="300,260,303,271,304,280,310,274,311,260,306,261" areafor="Hobart" />
	                    <area shape="poly" href="javascript:void(0);" coords="297,229,306,236,302,238,292,237" areafor="Melbourne" />
	                    <area shape="poly" href="javascript:void(0);" coords="347,143,347,151,358,148,358,142,353,142" areafor="Gold Coast" />
	                    <area shape="poly" href="javascript:void(0);" coords="354,123,350,133,353,137,358,137,356,127" areafor="Brisbane" />
	                    <area shape="poly" href="javascript:void(0);" coords="337,192,336,206,343,204,349,191" areafor="Sydney" />
	                    <area shape="poly" href="javascript:void(0);" coords="304,199,326,199,326,209,304,209" areafor="Australian Capital Territory" />
	                    <area shape="poly" href="javascript:void(0);" coords="246,205,251,202,254,205,250,210,249,214,244,214,246,211" areafor="Adelaide" />
						<area shape="poly" href="javascript:void(0);" coords="109,12,109,35,159,35,159,28,164,23,159,19,159,12" areafor="Darwin" />
						<area shape="poly" href="javascript:void(0);" coords="8,171,8,194,58,194,58,187,63,182,58,178,58,171" areafor="Perth" />
						<area shape="poly" href="javascript:void(0);" coords="328,259,378,259,378,282,328,282,328,275,323,270,328,265" areafor="Hobart" />
						<area shape="poly" href="javascript:void(0);" coords="391,224,391,247,318,247,318,240,313,235,318,227,318,224" areafor="Melbourne" />
						<area shape="poly" href="javascript:void(0);" coords="443,137,443,160,370,160,370,153,365,148,370,143,370,137" areafor="Gold Coast" />
						<area shape="poly" href="javascript:void(0);" coords="430,111,430,134,370,134,370,127,365,122,370,117,370,111" areafor="Brisbane" />
						<area shape="poly" href="javascript:void(0);" coords="420,187,420,210,360,210,360,203,355,198,360,193,360,187" areafor="Sydney" />
						<area shape="poly" href="javascript:void(0);" coords="241,171,241,194,302,194,302,189,313,191,302,179,302,171" areafor="Australian Capital Territory" />
						<area shape="poly" href="javascript:void(0);" coords="164,195,164,218,233,218,233,211,238,206,233,202,233,195" areafor="Adelaide" />
                    </map>
                </div>
                <div id="AnyWhereInAus">
				    <div class="checkbox"></div>
				    <span>Anywhere in <%= Model.AncillaryData.DefaultCountry.Name %></span>
			    </div>
            </div>
            <form action="" method="post" id="browseByLocation">
                <%= Html.IndustriesField(Model.Criteria, JobAdSearchCriteriaKeys.IndustryIds, c => c.IndustryIds, Model.AncillaryData.Industries).WithId("IndustriesLocation").WithLabel("Now select an industry in ").WithCssPrefix("Industry")%>
                <input type="hidden" id="LocationLocation" name="Location" />
                <input type="submit" id="searchLocation" name="search" class="button search" value="" />
            </form>
        </div>
        <% if (CurrentMember != null) { %>
            <% Html.RenderPartial("SuggestedJobs", Model.SuggestedJobs); %>
        <% } %>
        <div class="bottombar"></div>
    </div>
</asp:Content>