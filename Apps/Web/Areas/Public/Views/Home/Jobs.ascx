<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Home.Jobs" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Converters"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Home"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<div class="homepage-section find-jobs">
    <script type="text/javascript">
        var searchUrl = "<%= SearchUrl %>?CountryId=1";
        var apiPartialMatchesUrl = "<%= PartialMatchesUrl %>" + "?countryId=1&location=";

        var jobTypes = new Array(<%= GetJobTypes() %>);
        var allJobTypes = <%= (int)JobTypes.All %>;
    </script>
    <div class="section-top">
        <div class="section-tab section-tab-selected" id="search-tab"></div>
        <a class="section-tab" id="industry-tab" target="_blank" href="<%= Html.RouteRefUrl(JobAdsRoutes.BrowseJobAds) %>"></a>
        <a class="section-tab" id="location-tab" target="_blank" href="<%= Html.RouteRefUrl(JobAdsRoutes.BrowseJobAds) %>"></a>
    </div>
    
    <div id="search-tab-content" class="section-tab-content">
        <div class="section-bg">
            <div class="homepage-content-holder">                    
                <div class="homepage-title">Find jobs</div>
                <div class="homepage-content">
                    <div class="homepage-field_holder">
                        <div class="homepage-field_label">Keywords</div>
                        <div class="text-holder"><input class="homepage-field find-jobs_text-field js_watermarked search-submit" type="text" name="Keywords" id="Keywords" data-watermark="e.g. job title, skills" MaxLength="265" tabIndex="21" /></div>
                    </div>  
                    <div class="homepage-field_holder">
                        <div class="homepage-field_label">Location</div>
                        <div class="text-holder"><input autocomplete="off" class="homepage-field find-jobs_text-field js_watermarked location_autocomplete_textbox autocomplete_textbox search-submit" type="text" name="Location" id="Location" data-watermark="e.g. Melbourne, 3000" tabIndex="22" /></div>
                    </div>
                    <div class="expanded-search_holder expanded-search" style="display:none;">
                        <div class="divider"></div>
                        <div class="expanded-search_content">
                            <div class="homepage-field_holder">
                                <div class="homepage-field_label">Job Type</div>
                                <div class="checkbox-field_holder">
                                    <div class="checkbox-holder">
                                        <input type="checkbox" name="FullTime" id="FullTime" value="true" checked="checked" class="homepage-checkbox" /><span>Full time</span>
                                    </div>
                                    <div class="checkbox-holder">
                                        <input type="checkbox" name="PartTime" id="PartTime" value="true" checked="checked" class="homepage-checkbox" /><span>Part time</span>
                                    </div>
                                    <div class="checkbox-holder">
                                        <input type="checkbox" name="Contract" id="Contract" value="true" checked="checked" class="homepage-checkbox" /><span>Contract</span>
                                    </div>
                                    <div class="checkbox-holder">
                                        <input type="checkbox" name="Temp" id="Temp" value="true" checked="checked" class="homepage-checkbox" /><span>Temp</span>
                                    </div>
                                    <div class="checkbox-holder">
                                        <input type="checkbox" name="JobShare" id="JobShare" value="true" checked="checked" class="homepage-checkbox" /><span>Job share</span>
                                    </div>
                                </div>
                            </div>  
                            <div class="homepage-field_holder slider_holder">
                                <div class="homepage-field_label">Desired Salary <span class="italized-label">(per annum)</span></div>
                                <div class="checkbox-holder">
                                    <input type="checkbox" name="IncludeNoSalary" id="IncludeNoSalary" value="true" checked="checked" class="homepage-checkbox" /><span>Include jobs without a salary</span>
                                </div>
                                <div class="slider-field_holder">
                                    <div class="inputs_for_sliders">
                                        <input type="text" name="SalaryLowerBound" id="SalaryLowerBound" value="0" />
                                        <input type="text" name="SalaryUpperBound" id="SalaryUpperBound" value="250000" />
                                    </div>
		                            <div class="slider-limits">
			                            <div class="minrange"></div>
			                            <div class="maxrange"></div>
		                            </div>
		                            <div class="slider-holder">
			                            <div id="salary-range"></div>
			                            <div class="ruler50"> &nbsp; </div>
		                            </div>
		                            <div>
			                            <center><div class="range"></div></center>
		                            </div>
		                        </div>			                    
                            </div>
                        </div>
                    </div>
                    <div class="search-button_holder">
                        <div class="expand-link more-search-link js_expand-search" onclick="javascript:expandSearch(this);" tabIndex="24">MORE OPTIONS</div>
                        <div class="homepage-button search-button" id="search" onclick="javascript:searchJobs(searchUrl, jobTypes, allJobTypes, <%= Model.MinSalary %>, <%= Model.MaxSalary %>);" tabIndex="23"></div>                    
                    </div>
                    <div class="advanced-search-button_holder expanded-search" style="display:none;">
                        <div class="homepage-button advanced-search-button" onclick="javascript:loadPage('<%= SearchUrl %>');" tabIndex="25" ></div>                    
                    </div>
                </div>       
            </div>
        </div>
        <div class="shaded-jobs-bottom">
            <div class="shaded-box-content">
                <div class="title">Jobs added this week:&nbsp;<div class="digit"><%= Model.FeaturedStatistics.CreatedJobAds.ToString("N0") %></div></div>
                <div class="sub-content" id="jobad-ticker-items">
                    <ul>
<%  foreach (var jobAd in Model.FeaturedJobAds)
    { %>                    
                        <li class="link-holder"><a href="<%= jobAd.Url %>" class="ticker-item" title="<%= jobAd.Title %>"><%= jobAd.Title %></a></li>
<%  } %>
                    </ul>
                </div>
            </div>
        </div>
    </div> 
    <div id="industry-tab-content" class="section-tab-content">
        <div class="section-bg">
            <form action="<%= SearchRoutes.Search.GenerateUrl() %>" method="post" id="browseByIndustry">
                <%= Html.IndustriesField(new { IndustryIds = new List<Guid>() }, JobAdSearchCriteriaKeys.IndustryIds, c => c.IndustryIds, Model.Industries).WithId("IndustriesIndustry").WithLabel("Select an industry")%>
                <div class="row one"></div>
                <div class="row two"></div>
                <div class="row three"></div>
                <div class="row four"></div>
                <div class="row five"></div>
                <div class="row six"></div>
                <div class="row seven"></div>
                <div class="row eight"></div>
                <div class="row nine"></div>
                <div class="row ten"></div>
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
                    <%= Html.CountryField(new { i = Model.DefaultCountry.Id }, JobAdSearchCriteriaKeys.CountryId, c => c.i, Model.Countries).WithId(JobAdSearchCriteriaKeys.CountryId + "Industry").WithLabel("Country").WithCssPrefix("Country").WithAttribute("size", "12") %>
                    <input type="submit" id="searchIndustry" name="search" class="button search" value="" />
                </div>
            </form>
        </div>
        <div class="section-bottom"></div>
    </div>
    <div id="location-tab-content" class="section-tab-content">
        <div class="section-bg">
            <div class="map_field field">
                <label>Select a location</label>
                <div class="aus-map">
    <%      foreach (var subdivision in Model.CountrySubdivisions)
    {
    if (subdivision.Name == "Australian Capital Territory") continue;
    %>	                    
                    <div class="state" state="<%= subdivision.Name %>"></div>
    <%      } %>
                    <div class="region" region="Australian Capital Territory"></div>
    <%      foreach (var region in Model.Regions)
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
			        <span>Anywhere in <%= Model.DefaultCountry.Name %></span>
		        </div>
            </div>
            <form action="<%= SearchRoutes.Search.GenerateUrl() %>" method="post" id="browseByLocation">
                <%= Html.IndustriesField(new { IndustryIds = new List<Guid>() }, JobAdSearchCriteriaKeys.IndustryIds, c => c.IndustryIds, Model.Industries).WithId("IndustriesLocation").WithLabel("Now select an industry in ").WithCssPrefix("Industry")%>
                <input type="hidden" id="LocationLocation" name="Location" />
                <input type="submit" id="searchLocation" name="search" class="button search" value="" />
            </form>
        </div>
        <div class="section-bottom"></div>
    </div>
    <script type="text/javascript">
        (function($) {
            /* Initilization for Salary slider */
            initializeSalary(
                null,
                null,
                <%= Model.MinSalary.ToString() %>,
                <%= Model.MaxSalary.ToString() %>,
                <%= Model.StepSalary.ToString() %>);

        })(jQuery);
    </script>
</div>