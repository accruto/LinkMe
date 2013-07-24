<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<MemberSearchSortModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Query.Members"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Query.Search.Members"%>

<div class="sort_holder">
    <span id="sortOrderText" class="sort_text"></span>
    <span class="sort_fields forms_v2">
        <%= Html.MemberSortOrderField(Model.Criteria)
            .WithLabel("")
            .With(Model.SortOrders) %>
    </span>        
</div>

<script language="javascript" type="text/javascript">

    function setSortOrderText() {
        (function($) {

            var sortOrder = $("#SortOrder").val();
            var isAscending = $(".ascending").hasClass("selected");

            var text;
            var sortHolderClass = "sort_holder";
            switch (sortOrder) {
                case "DateUpdated":
                    text = isAscending ? "Oldest to most recent" : "Most recent to oldest";
                    sortHolderClass = sortHolderClass + " sort_date";
                    break;

                case "Distance":
                    text = isAscending ? "Furthest to nearest" : "Nearest to furthest";
                    sortHolderClass = sortHolderClass + " sort_distance";
                    break;

                case "Salary":
                    text = isAscending ? "Lowest to highest" : "Highest to lowest";
                    sortHolderClass = sortHolderClass + " sort_salary";
                    break;

                case "Flagged":
                    text = isAscending ? "Flagged at bottom" : "Flagged at top";
                    sortHolderClass = sortHolderClass + " sort_flagged";
                    break;

                case "Availability":
                    text = isAscending ? "Least to most" : "Most to least";
                    sortHolderClass = sortHolderClass + " sort_availability";
                    break;

                case "FirstName":
                    text = isAscending ? "Z-A" : "A-Z";
                    sortHolderClass = sortHolderClass + " sort_name";
                    break;

                case "Relevance":
                default:
                    text = isAscending ? "Least to most" : "Most to least";
                    sortHolderClass = sortHolderClass + " sort_relevance";
                    break;
            }

            sortHolderClass = sortHolderClass + (isAscending ? "-asc" : "-desc");

            $("#sortOrderText").text(text);
            $(".sort_holder").attr("class", sortHolderClass);

			if ($.browser.msie) {
				if ($.browser.version.indexOf("7") == 0) {
					$(".sort_holder").addClass("IE7");
				}
			}

        })(jQuery);
    }
    
    (function($) {

        // Updating results.

        $("#SortOrder").change(function() {
            setSortOrderText();
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            updateResults(false);
        });

        $(".sort_fields").find("input[type=radio]").each(function() {
            if ($(this).val() == "SortOrderIsAscending") {

                if ($(this).is(":checked")) {
                    $(".ascending").addClass("selected").addClass("asc_selected");
                    $(".descending").removeClass("selected").removeClass("dsc_selected");
                }
                else {
                    $(".descending").addClass("selected").addClass("dsc_selected");
                    $(".ascending").removeClass("selected").removeClass("asc_selected");
                }

                $(".ascending").click(function() {
                    if ($(this).hasClass("selected")) {
                        return false;
                    }
                    else {
                        $(this).addClass("selected").addClass("asc_selected");
                        $(".descending").removeClass("selected").removeClass("dsc_selected");
                        $("#SortOrderIsAscending").attr("checked", "checked");
                        setSortOrderText();
                        $('html, body').animate({ scrollTop: 0 }, 'slow');
                        updateResults(false);
                    }
                });
            }

            if ($(this).val() == "SortOrderIsDescending") {

                $(".descending").click(function() {
                    if ($(this).hasClass("selected")) {
                        return false;
                    }
                    else {
                        $(this).addClass("selected").addClass("dsc_selected");
                        $(".ascending").removeClass("selected").removeClass("asc_selected");
                        $("#SortOrderIsDescending").attr("checked", "checked");
                        setSortOrderText();
                        $('html, body').animate({ scrollTop: 0 }, 'slow');
                        updateResults(false);
                    }
                });
            }
        });

        setSortOrderText();

        $(document).ready(function() {
            if ($.browser.msie) {
                if ($.browser.version.indexOf("7") == 0) {
                    $(".sort_holder").addClass("IE7");
                    $("#container").addClass("IE7");
                }
            }
        });

    })(jQuery);

</script>
