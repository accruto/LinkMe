(function ($) {

    $(document).ready(function () {

        // Initial value.

        if ($("#AvailableNow").is(":checked")) {
            $(".js-status[value='AvailableNow']").addClass("checked");
        }
        else if ($("#ActivelyLooking").is(":checked")) {
            $(".js-status[value='ActivelyLooking']").addClass("checked");
        }
        else if ($("#OpenToOffers").is(":checked")) {
            $(".js-status[value='OpenToOffers']").addClass("checked");
        }
        else if ($("#NotLooking").is(":checked")) {
            $(".js-status[value='NotLooking']").addClass("checked");
        }
        
        // Events.

        $(".js-status").hover(function () {
            $(this).addClass("hover");
        }, function () {
            $(this).removeClass("hover");
        }).mousedown(function () {
            $(this).addClass("mouse-down");
        }).mouseup(function () {
            $(this).removeClass("mouse-down");
        }).click(function () {
            if (!$(this).hasClass("checked")) {

                // Make sure this is the only checked icon.

                $(".js-status").removeClass("checked");
                $(this).addClass("checked");

                // Update the corresponding input control.

                $("#" + $(this).attr("value")).click();
            }
        });

    });
})(jQuery);