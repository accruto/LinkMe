<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Home.Jobs" %>
<%@ Import Namespace="LinkMe.Domain"%>

<script type="text/javascript">
    var searchUrl = "<%= SearchUrl %>";
    var apiPartialMatchesUrl = "<%= PartialMatchesUrl %>" + "?countryId=1&location=";

    var jobTypes = new Array(<%= GetJobTypes() %>);
    var allJobTypes = <%= (int)JobTypes.All %>;
</script>
<div class="jobsearch_ascx forms_v2">
    <fieldset>
        <div class="keywords_field field textbox_field">
            <label>Keywords</label>
            <div class="control textbox_control">
                <input id="Keywords" name="Keywords" class="textbox search-submit" maxlength="265" />
            </div>
        </div>
        <div class="location_field field textbox_field autocomplete_textbox_field">
            <label>Location</label>
            <div class="control textbox_control autocomplete_textbox_control">
                <input id="Location" name="Location" class="textbox search-submit" maxlength="265" />
            </div>
        </div>
        <div class="field">
            <div class="search-and-links_control control">
                <input type="button" class="search_button button" id="search" onclick="javascript:searchJobs(searchUrl, jobTypes, allJobTypes, <%= Model.MinSalary %>, <%= Model.MaxSalary %>);" />
                <ul class="action-list">
                    <li><a id="lnkAdvancedSearch" href="<%= SearchUrl %>" title="Advanced Job Search">Advanced job search</a></li>
                </ul>
            </div>
        </div>
    </fieldset>
</div>
