<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Employers.Views.Shared.KeywordLocationSearch" %>
<%@ Import Namespace="LinkMe.Web.Areas.Api.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Query.Members"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Domain"%>

<script language="javascript">
    var apiPartialMatchesUrl = "<%= Html.RouteRefUrl(LocationRoutes.PartialMatches) %>" + "?countryId=1&location=";
    var apiResolveLocationUrl = "<%= Html.RouteRefUrl(LocationRoutes.ResolveLocation) %>" + "?countryId=1&location=";
    var apiSplitKeywordsUrl = "<%= Html.RouteRefUrl(LinkMe.Web.Areas.Api.Routes.SearchRoutes.SplitKeywords) %>";
    var apiCombineKeywordsUrl = "<%= Html.RouteRefUrl(LinkMe.Web.Areas.Api.Routes.SearchRoutes.CombineKeywords) %>";
</script>
<div class="keyword-location-search_ascx">
    <div class="main_columns columns">
        <!-- Keywords Search -->
        
<% var isSplit = Model.Criteria.GetKeywords() != Model.Criteria.AllKeywords;
   var keywords = isSplit ? "" : Model.Criteria.AllKeywords;
   var allKeywords = isSplit ? Model.Criteria.AllKeywords : "";
   var exactPhrase = isSplit ? Model.Criteria.ExactPhrase : "";
   var anyKeywords1 = isSplit ? AnyKeywords1 : "";
   var anyKeywords2 = isSplit ? AnyKeywords2 : "";
   var anyKeywords3 = isSplit ? AnyKeywords3 : "";
   var withoutKeywords = isSplit ? Model.Criteria.WithoutKeywords : ""; %>
        
        <div class="column">
            <div class="textbox_field keywords_field field">
                <label>Keywords</label>
                <div class="textbox_control control">
                    <input name="Keywords" value="<%= Html.Encode(keywords) %>" maxlength="265" id="Keywords" is-split="<%= isSplit ? "true" : "false" %>" tabindex="1" class="keywords_textbox js_watermarked errorable" type="text" data-reset-value="<%= Html.Encode(keywords) %>" data-watermark="e.g. Project Manager, MBA" />
                    <input type="hidden" name="IncludeSynonyms" id="IncludeSynonyms" value="<%= Model.Criteria.IncludeSynonyms ? "true" : "false" %>" />
                </div>
                <div class="error-container" style="display:none;">
                    <span class="error-arrow"></span>
                    <div class="error-body">
	                    <div class="error-content">
		                    <span class="error-alert-icon"></span> <span class="error-msg"> Please enter keywords to search </span>
	                    </div>
                    </div>
                </div>
            </div>
            <div class="options-holder js_options-holder js_keywords_advanced">
                <div class="options_text"><a title="More options" href="javascript:void(0);" class="toggleOptions" tabindex="2"><span class="options_label">More options</span><span class="down-icon">&nbsp;</span></a></div>
                <div class="options_text" style="display:none;"><a title="Hide options" href="javascript:void(0);" class="toggleOptions" tabindex="2"><span class="options_label">Hide options</span><span class="up-icon">&nbsp;</span></a></div>
                <div class="options_contents" style="display:none;">
                    <div class="advanced_search_box">
                        <div class="section">
                            <div class="section-content forms_v2">
								<div class="error-adv-keywords" style="display:none;">
									<div class="error-body">
										<div class="error-content">
											<span class="error-alert-icon"></span> <span class="error-msg"> Please enter keywords to search </span>
										</div>
									</div>
								</div>
                                <p class="small heavy">Find candidate resumes with...</p>
                                <fieldset>                                
                                    <div class="small field">
                                        <span class="heavy">All</span> of these words:
                                        <div class="control">
                                            <input type="text" name="AllKeywords" id="AllKeywords" maxlength="265" tabindex="3" value="<%= Html.Encode(allKeywords) %>" data-reset-value="<%= Html.Encode(allKeywords) %>" />
                                        </div>
                                    </div>
                                    <div class="small field">
                                        This <span class="heavy">exact</span> phrase:
                                        <div class="control">
                                            <input type="text" name="ExactPhrase" id="ExactPhrase" maxlength="265" tabindex="4" value="<%= Html.Encode(exactPhrase) %>" data-reset-value="<%= Html.Encode(exactPhrase) %>"/>
                                            <img src="<%= Images.Help %>" class="help js_tooltip"
                                             data-tooltip="You can do this in the Keyword field by &quotsurrounding your phrase with quotation marks&quot"/>
                                        </div>
                                    </div>
                                    <div class="small field more">
                                        <span class="heavy">One or more</span> of these words:
                                        <div class="control anykeywords-control">
                                            <input type="text" name="AnyKeywords" id="AnyKeywords1" maxlength="265" tabindex="5" value="<%= Html.Encode(anyKeywords1) %>" data-reset-value="<%= Html.Encode(anyKeywords1) %>"/>
                                            OR
                                            <input type="text" name="AnyKeywords" id="AnyKeywords2" maxlength="265" tabindex="6" value="<%= Html.Encode(anyKeywords2) %>" data-reset-value="<%= Html.Encode(anyKeywords2) %>"/>
                                            OR
                                            <input type="text" name="AnyKeywords" id="AnyKeywords3" maxlength="265" tabindex="7" value="<%= Html.Encode(anyKeywords3) %>" data-reset-value="<%= Html.Encode(anyKeywords3) %>"/>
                                            <img src="<%= Images.Help %>" class="help js_tooltip"
                                             data-tooltip="You can do this in the Keyword field by typing OR between the alternative words"/>
                                        </div>
                                    </div>
                                    <div class="small field">
                                        This candidate's name:
                                        <div class="control">
                                            <input type="text" name="CandidateName" id="CandidateName" maxlength="265" tabindex="8" value="<%= Html.Encode(Model.Criteria.Name) %>" data-reset-value="<%= Html.Encode(Model.Criteria.Name) %>" <% if (!Model.CanSearchByName) { %> readonly class="candidate_name_readonly" overlay_action="<% if (CurrentEmployer == null) { %>sbn-not-logged-in<% } else { %>sbn-limit-credit<% } %>" <% }%>  />
                                            <img src="<%= Images.Help %>" class="help js_tooltip" data-tooltip="Enter the candidate's first and last names, separated by spaces" />
                                            <span><input name="IncludeSimilarNames" id="ExactMatch" value="False" type="radio" <%= Model.Criteria.IncludeSimilarNames ? "" : "checked=\"checked\"" %> tabindex="9" /> Exact match</span>
                                            <span><input name="IncludeSimilarNames" id="CloseMatch" value="True" type="radio" <%= Model.Criteria.IncludeSimilarNames ? "checked=\"checked\"" : "" %> tabindex="10" /> Include close matches</span>
                                            <input name="IncludeSimiliarNamesResetHidden" type="hidden" value="<%= Model.Criteria.IncludeSimilarNames %>" />
                                        </div>
                                    </div>
                                </fieldset>
                                <div class="options-holder js_options-holder js_jobtitle_advanced">
                                    <div class="options_text">
                                        <a title="Filter job titles" href="javascript:void(0);" class="toggleOptions" tabindex="11"><span class="options_label">Filter job titles</span><span class="down-icon">&nbsp;</span></a>
                                    </div>
                                    <div class="options_text" style="display:none;">
                                        <a title="Hide job titles" href="javascript:void(0);" class="toggleOptions" tabindex="11"><span class="options_label">Hide job titles filter</span><span class="up-icon">&nbsp;</span></a>
                                    </div>
                                    <div class="options_contents" style="display:none;">
                                        <div class="section">
	                                        <div class="section-content forms_v2">
	                                            <fieldset>
		                                            <div class="small field">
			                                            <span class="heavy">All</span> of these words in their job titles:
			                                            <div class="control">
			                                                <input type="text" name="JobTitle" id="JobTitle" maxlength="265" tabindex="12"
			                                                    value="<%= Html.Encode(Model.Criteria.JobTitle) %>" data-reset-value="<%= Html.Encode(Model.Criteria.JobTitle) %>"/>
			                                                <span><input name="JobTitlesToSearch" class="noOfJobs" id="RecentJobs" value="RecentJobs" type="radio"
			                                                    <%= Model.Criteria.JobTitlesToSearch == JobsToSearch.RecentJobs ? "checked=\"checked\"" : "" %>
			                                                    tabindex="13"/> Last 3 jobs</span>
			                                                <span><input name="JobTitlesToSearch" class="noOfJobs" id="AllJobs" value="AllJobs" type="radio"
			                                                    <%= Model.Criteria.JobTitlesToSearch == JobsToSearch.AllJobs ? "checked=\"checked\"" : "" %>
			                                                    tabindex="14"/> All jobs</span>
			                                                    <input name="JobTitlesToSearchResetHidden" type="hidden" value="<%= Model.Criteria.JobTitlesToSearch.ToString() %>" />
			                                            </div>
		                                            </div>
		                                            <div class="small field">
			                                            <span class="heavy">All</span> of these words in their desired job titles:
			                                            <div class="control">
			                                                <input type="text" name="DesiredJobTitle" id="DesiredJobTitle" maxlength="265" tabindex="15" value="<%= Html.Encode(Model.Criteria.DesiredJobTitle) %>" data-reset-value="<%= Html.Encode(Model.Criteria.DesiredJobTitle) %>"/>
			                                            </div>
		                                            </div>
		                                        </fieldset>
	                                        </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="section">
                                    <div class="section-content forms_v2">
                                        <fieldset>
                                            <div class="field">
                                                <p class="small heavy">But don't show any candidate resumes with...</p>
                                                <div class="small">
	                                                <span class="heavy">Any</span> of these unwanted words:
	                                                <div class="control">
	                                                    <input type="text" name="WithoutKeywords" id="WithoutKeywords" maxlength="265" tabindex="16" value="<%= Html.Encode(withoutKeywords) %>" data-reset-value="<%= Html.Encode(withoutKeywords) %>"/>
	                                                    <img src="<%= Images.Help %>" class="help js_tooltip" data-tooltip="You can do this in the Keyword field by typing NOT before the word or phrase you want to exclude" />
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="reset-section">
                                    <a title="Reset options" href="javascript:void(0);" class="resetOptions"><span class="options_label">Reset options</span><span class="clear-icon">&nbsp;&nbsp;&nbsp;</span></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Location Search -->
        <div class="column">
            <div class="self-clearing">
                <div class="location_holder">
                    <div class="location_field autocomplete_textbox_field textbox_field field">
                        <label>Location</label>
                        <div class="textbox_control control">						
                            <div class="location_autocomplete_textbox_control autocomplete_textbox_control textbox_control control">
                                <input autocomplete="off" name="Location" id="Location"
                                    class="location_textbox location_autocomplete_textbox autocomplete_textbox js_watermarked errorable" type="text"
                                    value="<%= Model.Criteria.Location %>"
                                    data-reset-value="<%= Model.Criteria.Location %>" maxlength="100" tabindex="17"
                                    data-watermark="e.g. Melbourne, 3000">
                            </div>
                        </div>
                        <div class="error-container" style="display:none;">
                            <span class="error-arrow"></span>
                            <div class="error-body">
	                            <div class="error-content">
		                            <span class="error-alert-icon"></span> <span class="error-msg"> The location entered was not recognised, please re-enter. </span>
	                            </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="search_btn_holder">
                    <div class="search-and-links_field field">
                        <div class="search-and-links_control control" tabindex="21">
                            <!-- Search button -->
	                         <%= Html.ButtonsField().Add(new NewSearchButton()) %>
                        </div>
                    </div>
                    <div class="options_text search_tip">
                        <a title="Candidate Search Tips" class="js_search-tips" tabindex="22">Search tips</a>						
                    </div>
                </div>
            </div>
            <div class="location_options">
                <div class="options-holder js_options-holder js_location_advanced">
                    <div class="options_text">
                        <a title="More options" href="#" class="toggleOptions" tabindex="18"><span class="options_label">More options</span><span class="down-icon">&nbsp;</span></a>
                    </div>
                    <div class="options_text" style="display:none;">
                        <a title="Hide options" href="#" class="toggleOptions" tabindex="18"><span class="options_label">Hide options</span><span class="up-icon">&nbsp;</span></a>
                    </div>
                    <div class="options_contents" style="display:none;">
                        <div class="advanced_search_box">
                            <div class="section">
                                <div class="section-content">
                                    <p class="small heavy">Find candidates...</p>
                                    <div class="small">
                                        <%= Html.DistanceField(Model.Criteria, "Distance", c => c.Distance ?? Model.DefaultDistance, Model.Distances)
                                            .WithLabel("")
                                            .WithAttribute("data-reset-value", Model.Criteria.Distance.ToString())
                                            .WithAttribute("data-default-value", Model.DefaultDistance.ToString()) %>
                                    </div>
                                    <div class="small include-relocating-holder">
                                        <input name="IncludeRelocating" id="IncludeRelocating" type="checkbox" <% if (Model.Criteria.IncludeRelocating){ %> checked="checked" <% } %> value="true" tabindex="19" data-reset-value="<%= Model.Criteria.IncludeRelocating %>" />
                                        <input name="IncludeRelocating" type="hidden" value="false" />
                                        Include those willing to relocate here
                                    </div>
                                    <div class="small include-international-holder">
                                        <input name="IncludeInternational" id="IncludeInternational" type="checkbox" <% if (Model.Criteria.IncludeInternational){ %> checked="checked" <% } %> value="true" tabindex="20" data-reset-value="<%= Model.Criteria.IncludeInternational %>" <% if (!Model.Criteria.IncludeRelocating){ %> disabled="disabled" <% } %> />
                                        <input name="IncludeInternational" type="hidden" value="false" />
                                        Include international candidates
                                    </div>
                                    <div class="small" style="padding-top:16px;">
                                        <%= Html.CountryField(Model.Criteria, "CountryId", c => c.Location == null ? (int?)null : c.Location.Country.Id, Model.Countries)
                                            .WithText(c => c == null ? "Global" : c.Name)
                                            .WithPreText("Country: ")
                                            .WithAttribute("data-reset-value", Model.Criteria.Location == null ? null : Model.Criteria.Location.Country.Id.ToString())
                                            .WithAttribute("data-default-value", Model.DefaultCountry.Id.ToString())
                                            .WithLabel("")%>
                                    </div>
                                    <div class="reset-section">
                                        <a title="Reset options" href="javascript:void(0);" class="resetOptions"><span class="options_label">Reset options</span><span class="clear-icon">&nbsp;&nbsp;&nbsp;</span></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>        
    </div>
</div>

<script language="javascript">
(function($) {
	$(document).ready(function() {
        $(".js_tooltip").addTooltip();

        $("#Location").autocomplete(apiPartialMatchesUrl);

        $("#search").click(function() {
            var keywordsValid = false;
            var locationValid = false;

            // TODO: Watermark text breaks non-JS form submit. Move watermark text to non-form element, visually overlapping the 'containing' textbox.

            // Check for keywords

            if (($(".js_keywords_advanced").find(".options_contents").first().css("display") == "block")) {

                /* Advanced keywords is open */

                keywordsValid = !($("#AllKeywords").val() == ''
                    && $("#ExactPhrase").val() == ''
                    && $("#AnyKeywords1").val() == ''
                    && $("#AnyKeywords2").val() == ''
                    && $("#AnyKeywords3").val() == ''
                    && $("#WithoutKeywords").val() == ''
                    && $("#CandidateName").val() == ''
                    && $("#JobTitle").val() == ''
                    && $("#DesiredJobTitle").val() == '');

            }
            else {

                /* Advanced keywords is closed */

                keywordsValid = !($("#Keywords").hasClass("text-label") || $("#Keywords").val() == '' || $("#Keywords").val() == $("#Keywords").attr("data-watermark"));
            }

            // Check Location
            if ($("#Location").hasClass("text-label") || $("#Location").val() == '') {
                $("#Location").val('');
                /* Updating Distance Slider view */
                $(".loc_label").text('');
                $(".spec_loc").hide();
                $(".no_loc").show();
                locationValid = true;
            }
            else {
                var location = $("#Location").val();

                $.ajax({
                    mode: "abort",
                    type: "POST",
                    dataType: "json",
                    async: false,
                    url: apiResolveLocationUrl + encodeURIComponent(location),
                    success: function(data) {
                        locationValid = !(data.ResolvedLocation == null);
                    }
                });
            }
			
			//keywords always valid (actually there is no keywords) for suggested candidate
			if ($(".js_dist-key-loc").hasClass("loc-suggest-candidate")) keywordsValid = true;

            if (!keywordsValid) {
                if ($("#AllKeywords").closest(".js_options-holder").find(".options_contents:first:visible").length == 1)
                    $(".error-adv-keywords").show();
                else
                    $("#Keywords").closest(".field").find(".error-container").show();
            }

            if (!locationValid) {
                $("#Location").closest(".field").find(".error-container").show();
            }

            var reloadFlag = false;
            if (keywordsValid && locationValid) {

                // Case: submission is from Results.aspx (i.e. it's in the overlay on that page)
                if ($(this).closest(".overlay").size() > 0) {
                    $("#background-overlay").hide();
                    $(".key_loc-overlay").hide();
                    if (($("#Location").hasClass("text-label")) || ($("#Location").val() == '')) {
                        /* Updating Distance Slider view */
                        $(".loc_label").text('');
                        $(".spec_loc").hide();
                        $(".no_loc").show();
                        deactivateSection("distance_section");
                    }
                    else {
                        /* Updating Distance Slider view */
                        $(".no_loc").hide();
                        $(".spec_loc").show();
                        $(".loc_label").text($("#Location").val());
                        activateSection("distance_section");
                        openSection("distance_section");
                    }
                    // reflecting Distance dropdown changes in the Location search box.
                    setDistanceSliderValue($("#Distance").val());
                    /* Saving the Keywords and locations filter criteria */
                    $(this).closest(".overlay").find('input[type="text"]').each(function() {
                        $(this).attr('data-reset-value', this.value);
                    });
                    $('select[name=Distance]').attr('data-reset-value', $('select[name=Distance] option:selected').val());
                    $('select[name=CountryId]').attr('data-reset-value', $('select[name=CountryId] option:selected').val());
                    $("#IncludeRelocating").attr('data-reset-value', $("#IncludeRelocating").is(":checked"));
                    $("#IncludeInternational").attr('data-reset-value', $("#IncludeInternational").is(":checked"));
                    $("input[name=IncludeSimiliarNamesResetHidden]").val($("input[name=IncludeSimilarNames]:checked").val());
                    $("input[name=JobTitlesToSearchResetHidden]").val($('input[name=JobTitlesToSearch]:checked').val());
                    /* Updating Distance filter checkboxes (left side filters) on search submit */
                    ($("#IncludeRelocating").is(":checked")) ? $("#IncludeRelocatingFilter").attr("checked", "checked") : $("#IncludeRelocatingFilter").removeAttr("checked");
                    ($("#IncludeRelocating").is(":checked")) ? $("#IncludeInternationalFilter").removeAttr("disabled") : $("#IncludeInternationalFilter").attr("disabled", "disabled");
                    ($("#IncludeInternational").is(":checked")) ? $("#IncludeInternationalFilter").attr("checked", "checked") : $("#IncludeInternationalFilter").removeAttr("checked");

                    reloadFlag = false;

                    /* Flagging that this is a new search */
                    $("#isNewSearch").val("true");

                    /* Submitting depending on the state of "Retain current filter settings" */
                    if ($("#retain-filters").is(":checked")) {
                        $('html, body').animate({ scrollTop: 0 }, 'slow');
                        updateResults(false);    // from containing Results.aspx
                    } else {
                        resetAllFilterValues(); // updateResults() is called from inside this function
                    }
                    /* resetting "Retain current filter settings" after results fetch */
                    $("#retain-filters").attr("checked", "checked");
                }
                else {
                    reloadFlag = true;
                }
            }
            else if ($("#Location").hasClass("text-label") || $("#Location").val() == '') {

                // TODO: replace manual watermark re-setting with more generally applicable method

                $("#Location").val($("#Location").attr('data-watermark'));
                $("#Location").addClass('text-label');
                reloadFlag = false;
            }

            return reloadFlag;
        });

        if ($("#Keywords").closest(".overlay").size() > 0) {
            $("#Keywords").keypress(function(e) {
                if ((e.keyCode || e.which) == 13) {
                    $("#search").click();
                }
            });
        }

		$(".advanced_search_box").find("input").focus(function() {
			$(".error-adv-keywords:visible").hide();
		});
    });
})(jQuery);
</script>
