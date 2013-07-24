(function ($) {
    $(document).ready(function () {
        if ((".jobad .navigation").length > 0) shortenCriteria();

        $(".jobad .bg .titleline .icon.jobtype, .jobad .similarjobs .row .titleline .icon.jobtype").each(function () {
            getJobType($(this));
        });

        $("#mainbody .jobad .button.saved").click(function (event) {
            event.stopPropagation();

            var jobAdIds = [$(this).attr("id")];
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
                    linkme.members.jobads.api.addJobAdsToMobileFolder(jobAdIds, function() {
                        icon.addClass("active");
                    });
                }
            }
        });

        if (showAddedNotification) {
            $(".notification.added").show().delay(10000).queue(function () {
                $(this).hide();
            });
        }
    });

    getJobType = function (icon) {
        //primary job type and sub job types
        var jobTypesInAds = icon.attr("jobtypes").replace(/ /gi, "").split(",");
        var jobTyepsInCriteria = (criteria["JobTypes"] == undefined ? "FullTime,PartTime,Contract,Temp,JobShare" : criteria["JobTypes"]).replace(/ /gi, "").split(",");
        var primaryTypeArray = $.map(jobTyepsInCriteria, function (item) {
            return $.inArray(item, jobTypesInAds) < 0 ? null : item;
        });
        var primaryType = "None";
        if (primaryTypeArray.length > 0) primaryType = primaryTypeArray[0];
        icon.attr("class", "icon jobtype " + primaryType);
    }

    shortenCriteria = function () {
        var title = $(".jobad .navigation .title")
        var criteriahtml = title.find("span.criteriahtml");
        criteriahtml.html(criteriahtml.text());
        var hidePart = $("<span class='hidepart'></span>");
        var moreOrLessHolder = $("<span class='moreorlessholder'><span class='ellipsis'>... </span><span class='moreorlesstext'>more</span></span>");
        var lineHeight = parseFloat(title.css("line-height"));
        if (title.height() > lineHeight * 1) {
            hidePart.hide().appendTo(title);
            moreOrLessHolder.appendTo(title);
            while (title.height() > lineHeight * 1) {
                var text = criteriahtml.text();
                if (text == "") break;
                var ch = text.charAt(text.length - 1);
                hidePart.text(ch + hidePart.text());
                criteriahtml.text(text.substring(0, text.length - 1));
            }
            hidePart.hide();
            title.find(".moreorlessholder").click(function () {
                if ($(this).prev(":hidden").length > 0) {
                    $(this).find(".ellipsis").text(" ");
                    $(this).find(".moreorlesstext").text("less");
                    $(this).prev().toggle();
                } else {
                    $(this).find(".ellipsis").text("... ");
                    $(this).find(".moreorlesstext").text("more");
                    $(this).prev().toggle();
                }
            });
        }
    }
})(jQuery);