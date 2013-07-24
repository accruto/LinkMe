(function ($) {

    toFormattedDigits = function (nValue) {
        var s = nValue.toString();
        for (j = 3; j < 15; j += 4) {  // insert commas
            if (s.length > j) {
                s = s.substr(0, s.length - j) + ',' + s.substr(s.length - j);
            }
        }
        return (s);
    }

    $.fn.replaceWithFormattedDigits = function () {
        var s = $(this).text();
        $(this).text(toFormattedDigits(s));
    }

    updateIncludeInternationalState = function (includeRelocating) {
        if (includeRelocating) {
            $("#IncludeInternational").removeAttr('disabled');
        }
        else {
            //$("#IncludeInternational").attr('checked', 'checked');
            $("#IncludeInternational").attr('disabled', 'disabled');
        }
    }

    updateIncludeInternationalFilterState = function (includeRelocating) {
        if (includeRelocating) {
            $("#IncludeInternationalFilter").removeAttr('disabled');
        }
        else {
            //$("#IncludeInternationalFilter").attr('checked', 'checked');
            $("#IncludeInternationalFilter").attr('disabled', 'disabled');
        }
    }

    $(document).ready(function () {

        /* For keeping sections open on user-input values for Keywords & Location advanced search fields */

        showAdvancedSection = function (arg) {
            var args = arg.split(",");
            for (var i = 0; i < args.length; i++) {
                $("div.js_" + args[i] + "_advanced").find(".options_text").find(".down-icon").closest(".options_text").slice(0, 1).hide();
                $("div.js_" + args[i] + "_advanced").find(".options_text").find(".up-icon").closest(".options_text").slice(0, 1).show();
                $("div.js_" + args[i] + "_advanced").find(".options_contents").first().show();
                // if expand advanced keywords automatically, need to assign all keywords with keywords
                if (args[i] == "keywords" && $("#Keywords").attr("is-split") == "false" && $("#Keywords").val() != $("#Keywords").attr("data-watermark"))
                    $("#AllKeywords").val($("#Keywords").val());
            }
        }

        $(".js_options-holder").find(':input').filter(function () {
            //add this filter for filtering two input boxes which are automatically added by new dropdown on new Employer Homepage 1.5
            if ($(this).parent().hasClass("dropdown")) return false;
            else return true;
        }).each(function () {
            var keywordsSection = false;
            var jobTitleSection = false;
            var locationSection = false;
            switch (this.type) { /* Checking for Defaults */
                case 'text':
                    {
                        if (!(this.value == '')) {
                            if ((this.name == "AllKeywords") || (this.name == "ExactPhrase") || (this.name == "AnyKeywords") || (this.name == "WithoutKeywords")) {
                                keywordsSection = true;
                            } else {
                                jobTitleSection = true;
                            }
                        }
                        break;
                    }
                case 'radio':
                    {
                        if (this.name == "JobTitlesToSearch" && this.checked) {
                            if (!(this.value == "RecentJobs")) {
                                jobTitleSection = true;
                            }
                        }
                        if (this.name == "IncludeSimilarNames" && this.checked) {
                            if (!(this.value == "False")) {
                                keywordsSection = true;
                            }
                        }
                        break;
                    }
                case 'select-one':
                    {
                        if (this.name == "Distance") {
                            if (!(this.value == 50)) {
                                locationSection = true;
                            }
                        }
                        if (this.name == "CountryId") {
                            if (!($(this).children('option:selected').val() == 1)) {
                                locationSection = true;
                            }
                        }
                        break;
                    }
                case 'checkbox':
                    {
                        if (this.name == "IncludeRelocating" && this.checked) {
                            locationSection = true;
                        }
                        break;
                    }
            }

            if (jobTitleSection) {
                showAdvancedSection("keywords,jobtitle");
                $("#Keywords").attr('disabled', 'disabled');
            } else if (keywordsSection) {
                showAdvancedSection("keywords");
                $("#Keywords").attr('disabled', 'disabled');
            }
            if (locationSection) {
                showAdvancedSection("location");
            }
        });

        function addParamSeparators(url, methodCall) {
            if (url == methodCall) {
                url = url + "?";
            } else {
                url = url + "&";
            }
            return url;
        }

        function hideKeywordsOptions() {
            var anyKeywordsArray = new Array();

            var value = $("#AnyKeywords1").val();
            if (!(value == "" || value == null)) {
                anyKeywordsArray.push(value);
            }

            value = $("#AnyKeywords2").val();
            if (!(value == "" || value == null)) {
                anyKeywordsArray.push(value);
            }

            value = $("#AnyKeywords3").val();
            if (!(value == "" || value == null)) {
                anyKeywordsArray.push(value);
            }

            var url = apiCombineKeywordsUrl;

            value = $("#AllKeywords").val();
            if (!(value == "" || value == null)) {
                url = addParamSeparators(url, apiCombineKeywordsUrl) + "AllKeywords=" + encodeURIComponent(value);
            }

            value = $("#ExactPhrase").val();
            if (!(value == "" || value == null)) {
                url = addParamSeparators(url, apiCombineKeywordsUrl) + "ExactPhrase=" + encodeURIComponent(value);
            }

            var anyKeywords = anyKeywordsArray.length == 0 ? "" : anyKeywordsArray.join(" ");
            if (!(anyKeywords == "" || anyKeywords == null)) {
                url = addParamSeparators(url, apiCombineKeywordsUrl) + "AnyKeywords=" + encodeURIComponent(anyKeywords);
            }

            value = $("#WithoutKeywords").val();
            if (!((value == "") || (value == null))) {
                url = addParamSeparators(url, apiCombineKeywordsUrl) + "WithoutKeywords=" + encodeURIComponent(value);
            }

            // Set the Keywords by combining all others.

            $("#Keywords").removeAttr('disabled');

            $.ajax({
                type: "POST",
                dataType: "json",
                url: url,
                async: false,
                success: function (data) {
                    if (!(data == null)) {
                        if (!(data.Keywords == "" || data.Keywords == null)) {
                            $("#Keywords").val(data.Keywords);
                            $("#Keywords").removeClass("text-label");
                        }
                    }
                }
            });

            $("#AllKeywords").val('');
            $("#ExactPhrase").val('');
            $("#AnyKeywords1").val('');
            $("#AnyKeywords2").val('');
            $("#AnyKeywords3").val('');
            $("#WithoutKeywords").val('');
        }

        function showKeywordsOptions() {

            var value = $("#Keywords").val();

            // If the Keywords has a value then split it.

            if (!($("#Keywords").hasClass("text-label") || value == '' || value == $("#Keywords").attr("data-watermark") || value == null)) {
                var url = apiSplitKeywordsUrl + "?Keywords=" + encodeURIComponent(value);
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: url,
                    success: function (data) {
                        if (!(data == null)) {
                            if (!(data.AllKeywords == null)) {
                                $("#AllKeywords").val(data.AllKeywords);
                            }
                            if (!(data.ExactPhrase == null)) {
                                $("#ExactPhrase").val(data.ExactPhrase);
                            }
                            if (!(data.AnyKeywords == null)) {
                                var anyKeywordsArray = data.AnyKeywords.split(" ");
                                var restOfAnyKeywords = "";
                                for (var i = 0; i < anyKeywordsArray.length; i++) {
                                    if (i < 2) {
                                        $("#AnyKeywords" + (i + 1)).val(anyKeywordsArray[i]);
                                    }
                                    else {
                                        if (restOfAnyKeywords == "")
                                            restOfAnyKeywords = anyKeywordsArray[i];
                                        else
                                            restOfAnyKeywords = restOfAnyKeywords + " " + anyKeywordsArray[i];
                                    }
                                }
                                if (!(restOfAnyKeywords == "")) {
                                    $("#AnyKeywords3").val(restOfAnyKeywords);
                                }
                            }
                            if (!(data.WithoutKeywords == null)) {
                                $("#WithoutKeywords").val(data.WithoutKeywords);
                            }
                        }
                    }
                });
            }

            $("#Keywords").attr('disabled', 'disabled');
        }

        /* For toggling between options */
        $(".js_options-holder").find("a.toggleOptions").click(function () {
            if (($(this).find("span.up-icon").closest("div.options_text").css("display") == "block")) {

                /* Advanced Keywords search closed */

                if ($(this).closest(".js_options-holder").hasClass("js_keywords_advanced")) {
                    hideKeywordsOptions();
                }
            }
            else { /* Advanced Keywords search opened */
                if ($(this).closest(".js_options-holder").hasClass("js_keywords_advanced")) {
                    showKeywordsOptions();
                }
                if ($(this).closest(".overlay").size() == 0) {
                    $(".keyword-location-search_ascx").css("height", "auto");
                }
            }
            $(this).closest(".js_options-holder").find(".options_text").slice(0, 2).toggle();
            $(this).closest(".js_options-holder").find(".options_contents").first().toggle();
            if ($(this).closest(".overlay").size() == 0) {
                if ($(".keyword-location-search_ascx").height() < 200) {
                    $(".keyword-location-search_ascx").css("height", "200px");
                }
            }
            return false;
        });

        /* Resetting options */
        $(".js_options-holder").find("a.resetOptions").click(function () {
            var contents = $(this).closest(".js_options-holder").find(".options_contents").first();
            if ($(this).parents(".js_options-holder").hasClass("js_keywords_advanced")) {
                contents.find('input[type="text"]').val('');
                contents.find('input[name=IncludeSimilarNames]:checked').removeAttr('checked');
                contents.find('input[name=IncludeSimilarNames]:eq(0)').attr('checked', 'checked');
                contents.find('input[name=JobTitlesToSearch]:checked').removeAttr('checked');
                contents.find('input[name=JobTitlesToSearch]:eq(0)').attr('checked', 'checked');
            } else {
                contents.find('select[name=Distance] option:selected').removeAttr('selected');
                contents.find('select[name=Distance]').children('option').eq(3).attr('selected', 'selected');
                contents.find('select[name=CountryId] option:selected').removeAttr('selected');
                contents.find('select[name=CountryId]').children('option').eq(1).attr('selected', 'selected');
                contents.find('input[name=IncludeRelocating]').removeAttr('disabled').removeAttr('checked');
                contents.find('input[name=IncludeInternational]').attr('disabled', 'disabled').removeAttr('checked', 'checked');
            }
        });

        /* Search textboxes */
        $('input[type="text"].js_watermarked').each(function () {
            if (this.value == '') {
                this.value = $(this).attr('data-watermark');
                $(this).addClass('text-label');
            }
            $(this).focus(function () {
                if (($(this).hasClass('text-label')) || (this.value == $(this).attr('data-watermark'))) {
                    this.value = '';
                    $(this).removeClass('text-label');
                }
            });
            $(this).blur(function () {
                if (this.value == '') {
                    this.value = $(this).attr('data-watermark');
                    $(this).addClass('text-label');
                }
            });
            $(this).hover(function () {
                if (($(this).hasClass('text-label')) && (!(this.value == $(this).attr('data-watermark')))) {
                    $(this).removeClass('text-label');
                }
            });
            $(this).focus(function () {
                if ($(this).hasClass('errorable')) {
                    if ($(this).closest(".field").find(".error-container").css("display") == "block") {
                        $(this).closest(".field").find(".error-container").hide();
                    }
                }
            });
        });
        /* For location textbox */
        $('input.location_textbox').each(function () {
            if (this.value == '') {
                this.value = $(this).attr('data-watermark');
                $(this).addClass('text-label');
                /* Updating Distance Slider view */
                $(".loc_label").text('');
                $(".spec_loc").hide();
                $(".no_loc").show();
            }
        });

        /* Exclude International Candidates conditional check */
        $("#IncludeRelocating").click(function () {
            updateIncludeInternationalState($(this).is(":checked"));
        });
        $("#IncludeRelocatingFilter").click(function () {
            updateIncludeInternationalState($(this).is(":checked"));
            updateIncludeInternationalFilterState($(this).is(":checked"));
        });

        /* On closing Keywords Location overlay */
        $(".key_loc-overlay .overlay .overlay-title").find("span.close-icon")
					.click(function () {
					    /* reset values on close */
					    $(this).closest(".overlay").find('input[type="text"]').each(function () {
					        this.value = $(this).attr('data-reset-value');
					    });
					    /* Handling special case for Location */
					    if ($("#Location").attr('data-reset-value') == '') {
					        $("#Location").val($("#Location").attr('data-watermark'));
					        $("#Location").addClass('text-label');
					        /* Updating Distance Slider view */
					        $(".loc_label").text('');
					        $(".spec_loc").hide();
					        $(".no_loc").show();
					    }
					    /* Resetting radio button value */
					    var jobsResetValue = $("input[name=JobsToSearchResetHidden]").val();
					    $(this).closest(".overlay").find('input[name=JobsToSearch]:checked').removeAttr('checked');
					    $(this).closest(".overlay").find('input[name=JobsToSearch]').val([jobsResetValue]);
					    /* Resetting dropdown values */
					    var distanceResetValue = $(this).closest(".overlay").find('select[name=Distance]').attr('data-reset-value');
					    $(this).closest(".overlay").find('select[name=Distance] option:selected').removeAttr('selected');
					    $(this).closest(".overlay").find('select[name=Distance]').val(distanceResetValue).attr('selected', 'selected');
					    var countryResetValue = $(this).closest(".overlay").find('select[name=CountryId]').attr('data-reset-value');
					    $(this).closest(".overlay").find('select[name=CountryId] option:selected').removeAttr('selected');
					    $(this).closest(".overlay").find('select[name=CountryId]').val(countryResetValue).attr('selected', 'selected');
					    /* Resetting checkboxes */
					    $(this).closest(".overlay").find('input[type="checkbox"]').each(function () {
					        $(this).removeAttr("checked");
					        if (($(this).attr('data-reset-value') == "true") || ($(this).attr('data-reset-value') == "True")) {
					            $(this).attr("checked", "checked");
					        }
					        if (this.name == "IncludeInternational") {
					            if ($("#IncludeRelocating").is(":checked")) {
					                $(this).removeAttr("disabled");
					            } else {
					                $(this).attr("disabled", "disabled");
					            }
					        }
					    });
					    /* resetting "Retain current filter settings" */
					    $("#retain-filters").attr("checked", "checked");
					    return false;
					});


        resetFilterValues = function (sectionClassName) {
            //alert(sectionClassName);
            switch (sectionClassName) {
                default:
                    {
                        if (!(sectionClassName == "all")) { break; }
                    }
                case 'candidate-status_section':
                    {
                        $("#candidateStatusCheckboxes").find("input[type=checkbox]").attr("checked", "checked");
                        $("#candidateStatusCheckboxes").find("input[type=checkbox]").val(true);
                        $("#candidateStatusCheckboxes").find("input[type=checkbox]").each(function () {
                            this.refresh();
                            if ($("." + $(this).attr("id") + "_pushcheck").hasClass("disabled_pushcheck")) {
                                $(this).removeAttr("checked", "checked");
                                $(this).val(false);
                            }
                        });
                        $("#candidateStatusCheckboxes").find("input[type=hidden]").val(false);
                        if (!(sectionClassName == "all")) { break; }
                    }
                case 'job-type_section':
                    {
                        $("#jobTypeCheckboxes").find("input[type=checkbox]").attr("checked", "checked");
                        $("#jobTypeCheckboxes").find("input[type=checkbox]").val(true);
                        $("#jobTypeCheckboxes").find("input[type=checkbox]").each(function () {
                            this.refresh();
                            if ($("." + $(this).attr("id") + "_pushcheck").hasClass("disabled_pushcheck")) {
                                $(this).removeAttr("checked", "checked");
                                $(this).val(false);
                            }
                        });
                        $("#jobTypeCheckboxes").find("input[type=hidden]").val(false);
                        if (!(sectionClassName == "all")) { break; }
                    }
                case 'desired-salary_section':
                    {
                        setDefaultSalarySliderValues();
                        $("#ExcludeNoSalary").removeAttr("checked");
                        $("#ExcludeNoSalary").val(false);
                        if (!(sectionClassName == "all")) { break; }
                    }
                case 'resume-recency_section':
                    {
                        setDefaultRecencySliderValue();
                        if (!(sectionClassName == "all")) { break; }
                    }
                case 'distance_section':
                    {
                        setDefaultDistanceSliderValue();
                        $("#Distance").val($("#Distance").attr("data-default-value"));
                        $("#CountryId").val($("#CountryId").attr("data-default-value"));
                        $("#IncludeRelocating").removeAttr("checked");
                        $("#IncludeRelocatingFilter").removeAttr("checked");
                        //$("#IncludeInternational").attr("checked", "checked");
                        //$("#IncludeInternationalFilter").attr("checked", "checked");
                        updateIncludeInternationalState(false);
                        updateIncludeInternationalFilterState(false);
                        if (!(sectionClassName == "all")) { break; }
                    }
                case 'employer_section':
                    {
                        $("#CompanyKeywords").val($("#CompanyKeywords").attr('data-watermark'));
                        $("#CompanyKeywords").addClass('text-label');
                        if (!(sectionClassName == "all")) { break; }
                    }
                case 'education_section':
                    {
                        $("#EducationKeywords").val($("#EducationKeywords").attr('data-watermark'));
                        $("#EducationKeywords").addClass('text-label');
                        if (!(sectionClassName == "all")) { break; }
                    }
                case 'industry_section':
                    {
                        makeInitialToggleCheckboxes($(".js_toggle_checkboxes"), true);
                        $(".industry_section .js_toggle_checkboxes").toggleCheckboxes();
                        if (!(sectionClassName == "all")) { break; }
                    }
                case 'filter-by-activity_section':
                    {
                        var section = $(".filter-by-activity_section");
                        section.find(".filter-activity-either").attr("checked", "checked");
                        section.find(".filter-activity-yes").removeAttr("checked");
                        section.find(".filter-activity-no").removeAttr("checked");
                        if (!(sectionClassName == "all")) { break; }
                    }
                case 'indigenous-status_section':
                    {
                        $(".indigenous-status_section").find("input[type=checkbox]").removeAttr("checked");
                        if (!(sectionClassName == "all")) { break; }
                    }
                case 'visa-status_section':
                    {
                        $(".visa-status_section").find("input[type=checkbox]").removeAttr("checked");
                        if (!(sectionClassName == "all")) { break; }
                    }
                case 'communities_section':
                    {
                        $(".communities_section").find('select[name=CommunityId] option:selected').removeAttr('selected');
                        $(".communities_section").find('select[name=CommunityId]').val("").attr('selected', 'selected');
                        if (!(sectionClassName == "all")) { break; }
                    }
                    //default: break;
            }
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            /* Collapsing all filters */
            $(".refine_section").find(".section-collapsible-content").find(".section-collapsible-title-active").each(function () {
                var sectionClassName = $(this).parent("div").attr("class").split(' ')[0];
                deactivateSection(sectionClassName);
            });
            $(".refine_section").find(".section-collapsible-content").find(".section-collapsible-title")
				.addClass("section-collapsible-title-default").removeClass("section-collapsible-title-active")
				.find("> .section-icon").addClass("section-icon-down").removeClass("section-icon-up")
				.end().next().hide();
            if (!($("#Location").hasClass("text-label")) || !($("#Location").val() == "")) {
                activateSection("distance_section");
                openSection("distance_section");
            }
            updateResults(false);
        }

        //register event for candidate's name input
        $("#CandidateName").click(function () {
            if ($(this).attr("readonly") == true) {
                showSBNOverlay($(this).attr("overlay_action"));
            }
        });

    });

    openSection = function (sectionName) {
        /* Collapsing all sub sections (except the first sub section) */
        $("." + sectionName).find("div.section-collapsible-title")/*.removeClass("section-collapsible-title-default").addClass("section-collapsible-title-active")*/
			.find("> .section-icon").removeClass("section-icon-down").addClass("section-icon-up")
			.end().next().show();
    }

    /* Activating filters */

    isSectionActivated = function (sectionName) {
        return $("." + sectionName).find(".section-icon-clear-chk").is(":checked");
    }

    activateSection = function (sectionName) {
        var sectionIcon = $("." + sectionName).find(".section-icon-clear-chk");
        sectionIcon.attr("checked", "checked");
        sectionIcon.val(true);
        sectionIcon.each(function () {
            this.refresh();
        });
        $("." + sectionName).find("div.section-collapsible-title").removeClass("section-collapsible-title-default").addClass("section-collapsible-title-active");
    }

    /* Deactivating filters */
    deactivateSection = function (sectionName) {
        var sectionIcon = $("." + sectionName).find(".section-icon-clear-chk");
        sectionIcon.removeAttr("checked");
        sectionIcon.val(false);
        sectionIcon.each(function () {
            this.refresh();
        });
        $("." + sectionName).find("div.section-collapsible-title").removeClass("section-collapsible-title-active").addClass("section-collapsible-title-default");
    }

    // Typically, this plugin is applied to checkboxes which activate/deactivate each filter
    $.fn.toggleActivationStatus = function () {
        $(this).click(function () {
            var sectionClassName = $(this).parent("div").attr("class").split(' ')[0];
            if (sectionClassName == "candidate-status_section" || sectionClassName == "job-type_section") {
                return false;
            }
            if (sectionClassName == "distance_section" && $(".distance_section .spec_loc:visible").length == 0)
                return false;
            if ($(this).closest(".section").find(".filter_pushcheck").hasClass("pushcheck-checked")) {
                $(this).closest(".section").find(".section-collapsible-title").addClass("section-collapsible-title-active").removeClass("section-collapsible-title-default");
            }
            else {
                $(this).closest(".section").find(".section-collapsible-title").removeClass("section-collapsible-title-active").addClass("section-collapsible-title-default");
            }
            if (sectionClassName == "employer_section" && ($("#CompanyKeywords").val() == "" || $("#CompanyKeywords").val() == $("#CompanyKeywords").attr("data-watermark")) && $(this).closest(".section").find(".section-collapsible-title").hasClass("section-collapsible-title-active"))
                return true;
            if (sectionClassName == "education_section" && ($("#EducationKeywords").val() == "" || $("#EducationKeywords").val() == $("#EducationKeywords").attr("data-watermark")) && $(this).closest(".section").find(".section-collapsible-title").hasClass("section-collapsible-title-active"))
                return true;
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            updateResults(false);
        });
    }

    /* Clearing all filters */
    resetAllFilterValues = function () {
        resetFilterValues("all");

        activateSection("candidate-status_section");
        openSection("candidate-status_section");
        activateSection("job-type_section");
    }

    /* Keeping sections open for active filters */

    $.fn.openSectionOnLoad = function (minSalary, maxSalary) {
        var anySectionOpenFlag = false;

        // job-type_section

        $(".job-type_section").find("input[type=checkbox]").each(function () {
            if (!($(this).hasClass("section-icon-clear-chk"))) {
                if (!($(this).is(":checked"))) {
                    openSection("job-type_section");
                    anySectionOpenFlag = true;
                }
            }
        });

        // desired-salary_section

        if ($("#SalaryLowerBound").val() != minSalary || $("#SalaryUpperBound").val() != maxSalary) {
            openSection("desired-salary_section");
            activateSection("desired-salary_section");
            anySectionOpenFlag = true;
        }

        // resume-recency_section

        if ($("#Recency").val() != "") {
            openSection("resume-recency_section");
            activateSection("resume-recency_section");
            anySectionOpenFlag = true;
        }

        // distance_section

        if (!(($("#Location").val() == "") && ($("#Distance").val() == 50))) {
            openSection("distance_section");
            activateSection("distance_section");
            anySectionOpenFlag = true;
        }

        // employer_section

        if (!(($("#CompanyKeywords").hasClass("text-label")) || ($("#CompanyKeywords").val() == ""))) {
            openSection("employer_section");
            activateSection("employer_section");
            anySectionOpenFlag = true;
        }

        // education_section

        if (!(($("#EducationKeywords").hasClass("text-label")) || ($("#EducationKeywords").val() == ""))) {
            openSection("education_section");
            activateSection("education_section");
            anySectionOpenFlag = true;
        }

        // industry_section

        $(".industry_section").find(".children-toggle-checkbox-holder input[type=checkbox]").each(function () {
            if (!($(this).hasClass("section-icon-clear-chk"))) {
                if ($(this).is(":checked")) {
                    openSection("industry_section");
                    activateSection("industry_section");
                    anySectionOpenFlag = true;
                }
            }
        });

        // filter-by-activity_section

        $(".filter-by-activity_section").find("input[type=radio]").each(function () {
            if (!(($(this).attr("id") == "InFolderEither") || ($(this).attr("id") == "HasNotesEither") || ($(this).attr("id") == "HasViewedEither") || ($(this).attr("id") == "IsFlaggedEither") || ($(this).attr("id") == "IsUnlockedEither"))) {
                if ($(this).is(":checked")) {
                    openSection("filter-by-activity_section");
                    activateSection("filter-by-activity_section");
                    anySectionOpenFlag = true;
                }
            }
        });

        // indigenous-status_section

        if ($(".indigenous-status_section").find("#EthnicStatus").is(":checked")) {
            openSection("indigenous-status_section");
            activateSection("indigenous-status_section");
            anySectionOpenFlag = true;
        }

        // visa-status_section

        if ($(".visa-status_section").find("#VisaStatus").is(":checked")) {
            openSection("visa-status_section");
            activateSection("visa-status_section");
            anySectionOpenFlag = true;
        }

        // communities_section

        if (!($("#CommunityId").val() == "")) {
            openSection("communities_section");
            activateSection("communities_section");
            anySectionOpenFlag = true;
        }

        // candidate-status_section. This section remains open by default if none of the other filters are applicable.
        // Modified by Gary on 07/06/2011, this section should be open by default whatever the other filters applied or not.
        openSection("candidate-status_section");
        activateSection("candidate-status_section");
        // $(".candidate-status_section").find("input[type=checkbox]").each(function() {
        // if (!($(this).hasClass("section-icon-clear-chk"))) {
        // if ((!($(this).is(":checked"))) || (!(anySectionOpenFlag))) {
        // openSection("candidate-status_section");
        // activateSection("candidate-status_section");
        // }
        // }
        // });

        // Activating Job Hunter Status section on load.

        $(".candidate-status_section").find(".section-icon-clear-chk").attr("checked", "checked");
        $(".candidate-status_section").find(".section-icon-clear-chk").val(true);
        $(".candidate-status_section").find(".section-icon-clear-chk").each(function () {
            this.refresh();
        });
        $(".candidate-status_section").find("div.section-collapsible-title").removeClass("section-collapsible-title-default").addClass("section-collapsible-title-active");

        // Activating Job Type section on load.

        $(".job-type_section").find(".section-icon-clear-chk").attr("checked", "checked");
        $(".job-type_section").find(".section-icon-clear-chk").val(true);
        $(".job-type_section").find(".section-icon-clear-chk").each(function () {
            this.refresh();
        });
        $(".job-type_section").find("div.section-collapsible-title").removeClass("section-collapsible-title-default").addClass("section-collapsible-title-active");
    }

    // Synonyms filter
    $.fn.synonymsFilter = function () {
        $(this).click(function () {
            $(this).toggleClass("synonyms");
            $("#IncludeSynonyms").val($(this).hasClass("synonyms"));
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            updateResults(false);
        });
    }

    /* Creating Update results section */
    $.fn.initializeResultsCount = function (candidateContext) {
        $(this).find(".save-search").find("a").click(function () {
            showSaveSearchResultsOverlay(candidateContext.selectedCandidateIds.length == 0 ? candidateContext.candidateIds : candidateContext.selectedCandidateIds, candidateContext);
        });
    }

    /* Updating Results Count */
    $.fn.updateResultsCount = function (candidateContext) {
        var count = candidateContext.selectedCandidateIds.length == 0 ? candidateContext.candidateIds.length : candidateContext.selectedCandidateIds.length;
        if (count == 0) {
            $(this).hide();
        }
        else {
            $(this).show();
            $(this).find(".results-count_save-search_header").find(".results-count").html($(".pagination-results-holder").html());
            var saveSearch = $(this).find(".results-count_save-search_header").find(".save-search");
            saveSearch.find(".total-count").text(count);
            saveSearch.find(".total-suffix").text(count == 1 ? "" : "s");
        }
        return true;
    }

})(jQuery);