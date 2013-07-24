(function ($) {
    $(document).ready(function () {
        initJobAdItemEvents();
    });

    initJobAdItemEvents = function () {
        //row click
        $(".row.jobad").unbind("click").click(function () {
            var url = $(this).attr("data-url");
            $(this).addClass("active").delay(500).queue(function () {
                window.location.href = url;
            });
        });
        $(".row.jobad:not(.empty)").each(function () {
            updateJobAdItemData($(this));
        });
        //save job
        $(".row.jobad .icon.saved").unbind("click").click(function (event) {
            event.stopPropagation();

            var jobAdIds = [$(this).closest(".row.jobad").attr("id")];
            var icon = $(this);

            if ($(this).hasClass("active")) {
                linkme.members.jobads.api.removeJobAdsFromMobileFolder(jobAdIds, function () {
                    icon.removeClass("active");
                });
            }
            else {
                if ($(this).hasClass("notloggedin")) {
                    window.location.href = $(this).attr("data-url");
                }
                else {
                    linkme.members.jobads.api.addJobAdsToMobileFolder(jobAdIds, function () {
                        icon.addClass("active");
                        $(".notification.added").show().delay(10000).queue(function () {
                            $(this).hide();
                        });
                    });
                }
            }
        });
    }

    updateJobAdItemData = function (row) {
        //primary job type and sub job types
        var icon = row.find(".titleline .icon.jobtype");
        var jobTypesInAds = icon.attr("jobtypes").replace(/ /gi, "").split(",");
        var jobTyepsInCriteria = (criteria == undefined || criteria["JobTypes"] == undefined ? "FullTime,PartTime,Contract,Temp,JobShare" : criteria["JobTypes"]).replace(/ /gi, "").split(",");
        var primaryTypeArray = $.map(jobTyepsInCriteria, function (item) {
            return $.inArray(item, jobTypesInAds) < 0 ? null : item;
        });
        var primaryType = "None";
        if (primaryTypeArray.length > 0) primaryType = primaryTypeArray[0];
        icon.attr("class", "icon jobtype " + primaryType);
    }
})(jQuery);