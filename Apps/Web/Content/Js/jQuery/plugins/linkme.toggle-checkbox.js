/*  MF: note that this is NOT a general purpose plugin (set)!

    This is *very* specific to the Industry filter for Filter.aspx and
    will not work anywhere else without a fair amount of work.

    If you need a more general-use checkbox hierarchy, you can use
    CheckboxesHierarchyBehaviour.js (written for Prototype - it needs
    porting to jQuery at some point...)
*/

function makeInitialToggleCheckboxes(obj, checkFlag) {
    (function($) {
        /* All Checkboxes being selected on initial page load (very first time). If checkFlag is true, child checkboxes will be deselected */
        if (checkFlag) {
            $(obj).find(".children-toggle-checkbox-holder input[type='checkbox']").each(function() {
                $(this).removeAttr("checked");
            });
            $(obj).find(".parent-toggle-checkbox-holder input[type='checkbox']").each(function() {
                $(this).attr("checked", "checked");
            });
        } else {
            /* If any child checkboxes are to be checked on load (clicking on Back Link), parent childbox is unchecked */
            var AnyChildChecked = false;
            $(obj).closest(".toggle-checkboxes").find(".children-toggle-checkbox-holder input[type='checkbox']").each(function() {
                /*if (!($(this).is(':checked'))) {
                AnyChildUnchecked = true;
                }*/
                if ($(this).is(':checked')) {
                    AnyChildChecked = true;
                }
            });
            if (AnyChildChecked) {
                $(obj).find(".parent-toggle-checkbox-holder input[type='checkbox']").each(function() {
                    $(this).removeAttr("checked");
                });
            } else {
                $(obj).find(".children-toggle-checkbox-holder input[type='checkbox']").each(function() {
                    //$(this).attr("checked", "checked");
                    $(this).removeAttr("checked");
                });
                $(obj).find(".parent-toggle-checkbox-holder input[type='checkbox']").each(function() {
                    $(this).attr("checked", "checked");
                });
            }

        }
    })(jQuery);
}    

function makeInitialCollapsibleDisplay(obj, startIndex, stepIncrement) {
    (function($) {
        if ($(obj).hasClass("industries_section")) {
            $(obj).find(".link-holder").remove();
            var noOfIndustries = $(obj).find(".children-toggle-checkbox-holder .child-holder:not('.disabled-industry')").length;

            /* Displaying only <startIndex> number of child textboxes on page load */
            if (noOfIndustries > startIndex) {
                $(obj).find(".children-toggle-checkbox-holder .child-holder:not('.disabled-industry')").slice(startIndex).hide();
                $(obj).append('<div class="link-holder"><a href="javascript:void(0)" class="left-link"><span class="left-link"><small>Show more</small></span><span class="arrow-icon icon-down"/></a><a href="javascript:void(0)" class="right-link"></a></div>');
            }
        }
    })(jQuery);
}

(function($) {

    var noOfIndustries = 0;
    

    $.fn.toggleCheckboxes = function() {
        
        /* Toggling Child checkbox */
        $(this).find(".children-toggle-checkbox-holder input[type='checkbox']").click(function() {
            var AnyChildChecked = false;
            $(this).closest(".toggle-checkboxes").find(".children-toggle-checkbox-holder input[type='checkbox']").each(function() {
                if ($(this).is(':checked')) {
                    AnyChildChecked = true;
                }
            });
            $(this).closest(".toggle-checkboxes").find(".parent-toggle-checkbox-holder input[type='checkbox']").each(function() {
                if (AnyChildChecked) {
                    $(this).removeAttr("checked");
                } else {
                    $(this).attr("checked", "checked");
                }
            });
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            updateResults(false);
        });
        /* Toggling Parent checkbox */
        $(this).find(".parent-toggle-checkbox-holder input[type='checkbox']").click(function() {
            if ($(this).is(':checked')) {
                $(this).closest(".toggle-checkboxes").find(".children-toggle-checkbox-holder input[type='checkbox']").each(function() {
                    //$(this).attr("checked", "checked");
                    $(this).removeAttr("checked");
                });
            } else {
                var AnyChildChecked = false;
                $(this).closest(".toggle-checkboxes").find(".children-toggle-checkbox-holder input[type='checkbox']").each(function() {
                    //$(this).removeAttr("checked");                    
                    if ($(this).is(':checked')) {
                        AnyChildChecked = true;
                    }
                });
                if (!(AnyChildChecked)) {
                    $(this).attr("checked", "checked");
                }
            }
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            updateResults(false);
        });
    }

    $.fn.makeCollapsibleDisplay = function(startIndex, stepIncrement) {
        if ($(this).hasClass("industries_section")) {
			$(this).find("a.left-link").click(function() {
				var text = $(this).text();
				if (text.indexOf("Show more") == 0)
					$(this).text("Show 10 more ▼").next().show().parent().parent().find(".child-holder").slice(5, 10).show();
				else if (text.indexOf("10") > 0)
					$(this).text("Show 9 more ▼").next().show().parent().parent().find(".child-holder").slice(10, 20).show();
				else
					$(this).hide().next().show().parent().parent().find(".child-holder").show();
			}).text("Show more ▼");
			$(this).find("a.right-link").click(function() {
				$(this).hide().prev().text("Show more ▼").show().parent().parent().find(".child-holder").slice(5).hide();
			}).text("Show less ▲").hide();
        }
    }

})(jQuery);