(function (linkme, $, undefined) {

    linkme.fields = (function () {

        return {
            
            resetValidation: function(context) {

                context = context || $("body");

                var $success = $(".validation-success", context);
                $success.hide();
                $success.find(".prompt").text("");

                var $errors = $(".validation-errors", context);
                $errors.hide();
                $errors.find(".prompt").text("");

                $(".validation-errors", context).hide();
                $(".control", context).removeClass("error");
                $(".field .validation-error", context).remove();
            },

            showValidationSuccess: function (context, message) {

                context = context || $("body");

                // Show success and hide errors.
                
                var $success = $(".validation-success", context);
                $success.show();
                if (message)
                    $success.find(".prompt").text(message);
                $(".validation-errors", context).hide();

                // Remove all errors on each field.
                
                $(".control", context).removeClass("error");
                $(".field .validation-error", context).remove();
            },

            showValidationErrors: function (context, errors, message) {

                context = context || $("body");
                message = message || "Please review the errors below";
                
                // Show errors and hide success.
                
                var $errors = $(".validation-errors", context);
                $errors.show();
                $errors.find(".prompt").text(message);
                $(".validation-success", context).hide();
                
                // Set errors on each field if needed.

                $(".field .validation-error", context).remove();
                $.each(errors, function () {
                    $("#" + this.Key, context).closest(".control").addClass("error").closest(".field").find(".help-area").addClass("error").end().append("<span class='validation-error' errorfor='" + this.Key + "'>" + this.Message + "</span>");
                });
                $(".validation-errors ul", context).text("");
                $.each(errors, function () {
                    $(".validation-errors ul", context).append("<li><div class='listicon'></div><div class='text'>" + this.Message + "</div></li>");
                });
            }

        };
    } ());

} (window.linkme = window.linkme || {}, jQuery));

(function ($) {

    var _createScrollPan = function(parent, type) {

        // Add UI elements.

        var items = $("<div class='" + type + "-items'></div>");
        var scrollpan = $("<div class='" + type + "-scrollpan'></div>");
        scrollpan.append(items);
        scrollpan.width(parent.width() + "px");
        parent.append(scrollpan);

        // Add item for each option.

        $.each($("select option", parent), function() {

            var item = type == "listbox"
                ? $("<div class='" + type + "-item'><div class='checkbox'></div>" + $(this).text() + "</div>")
                : $("<div class='" + type + "-item'>" + $(this).text() + "</div>");

            if ($(this).attr("disabled") == "disabled")
                item.addClass("disabled");

            item.click(function(e) {

                if (item.hasClass("disabled"))
                    return;

                // Turn click on item into update to underlying input element.

                if (type == "dropdown") {
                    scrollpan.hide();
                    var index = $("." + type + "-item", $(this).parent()).index(this);
                    $("select option", parent).removeAttr("selected");
                    $("select option:nth-child(" + (index + 1) + ")", parent).attr("selected", "selected");
                    $("input[type='text']", parent).val($(this).text());
                }
                if (type == "listbox") {
                    if ($(this).hasClass("selected")) {

                        // Turn on.

                        $(this).closest(".listbox").find("select option:contains('" + $(this).text() + "')").removeAttr("selected");
                        $(this).removeClass("selected");
                    } else {

                        // Turn off.

                        if ($("." + type + "-item.selected", parent).length == $("select", parent).attr("maxcount")) {
                            return;
                        } else {
                            $(this).closest(".listbox").find("select option:contains('" + $(this).text() + "')").attr("selected", "selected");
                            $(this).addClass("selected");
                        }
                    }
                }

                e.stopPropagation();
            });

            // Add the item to the list.

            items.append(item);
            if ($(this).attr("selected"))
                item.click();
        });

        // Scroll pan and scroll bar.

        var size = $("select", parent).attr("size");
        var length = $("." + type + "-item", items).length;
        var itemHeight = 26;
        scrollpan.height(size * itemHeight + 1);

        if (length > size) {

            var topArrow = $("<div class='" + type + "-scrollbar-toparrow-border'></div><div class='" + type + "-scrollbar-toparrow'></div>");
            var bottomArrow = $("<div class='" + type + "-scrollbar-bottomarrow-border'></div><div class='" + type + "-scrollbar-bottomarrow'></div>");
            var scrollbarWrap = $("<div class='" + type + "-scrollbar-wrap'></div>");
            var scrollbar = $("<div class='" + type + "-scrollbar'></div>");
            var diff = (length - size) * itemHeight;
            var handleHeight = 49;
            var minValue = 0;
            var maxValue = 100;
            var scrollbarWidth = 12;

            scrollbarWrap.append(scrollbar);
            scrollpan.append(topArrow).append(scrollbarWrap).append(bottomArrow);
            scrollbar.slider({
                orientation: "vertical",
                min: minValue,
                max: maxValue,
                value: maxValue,
                slide: function(event, ui) {
                    var topVal = -((maxValue - ui.value) * diff / 100);
                    items.css("top", topVal);
                },
                change: function(event, ui) {
                    var topVal = -((maxValue - ui.value) * diff / 100);
                    items.css("top", topVal);
                }
            });
            scrollbarWrap.height(size * itemHeight - 23);
            scrollbar.height(size * itemHeight - handleHeight - 23);
            items.width(parent.width() - scrollbarWidth - 1);
            scrollbarWrap.mousedown(function(e) {
                if ($(e.srcElement).hasClass("ui-slider-handle")) return;
                scrollbar.slider("value", 100 - (e.offsetY / scrollbarWrap.height() * 100));
            });
            $("." + type + "-scrollbar-toparrow, ." + type + "-scrollbar-toparrow-border", scrollpan).click(function() {
                scrollbar.slider("value", scrollbar.slider("value") + 10);
            });
            $("." + type + "-scrollbar-bottomarrow, ." + type + "-scrollbar-bottomarrow-border", scrollpan).click(function() {
                scrollbar.slider("value", scrollbar.slider("value") - 10);
            });
            if (type == "listbox")
                parent.height(scrollpan.height());
        }
    };

    $.fn.initFields = function () {

        var context = $(this);

        // Text box.

        $(".control .textbox:not(.password_textbox, .multiline_textbox, .autoexpand)", context).each(function () {

            var $this = $(this);
            var control = $this.parent();

            // Add UI elements.

            var bg = $("<div class='bg'></div>");
            $this.appendTo(bg);
            control.append(bg);
            bg.before("<div class='leftbar'></div>").after("<div class='rightbar'></div>");

            // Set water mark.

            var watermark = $this.attr("data-watermark");
            if ($this.val() == "" && watermark)
                $this.val(watermark).addClass("mask");
            
            $.fn.extend({
				realVal : function() {
				    var val = $(this).val();
				    var watermark = $(this).attr("data-watermark");
					return watermark && val == watermark ? "" : val;
				}
			});

        }).focus(function () {

            var $this = $(this);
            if ($this.attr("readonly") == "readonly")
                return;

            // Remove the watermark and make the control active.

            if ($this.val() == $this.attr("data-watermark"))
                $this.val("").removeClass("mask");
            $this.closest(".control").addClass("active");

        }).blur(function () {
            
            var $this = $(this);

            // Put the watermark back if needed and make the control inactive.

            var watermark = $this.attr("data-watermark");
            if ($this.val() == "" && watermark)
                $this.val(watermark).addClass("mask");
            $this.closest(".control").removeClass("active");

        });
        
        // Compulsory fields.

		$(".compulsory_field", context).each(function() {
			var mandatory = $("<div class='mandatory'></div>");
			mandatory.insertAfter($("> label", this));
		});
        
        // Drop down.

        $(".control select:not(.listbox, .month_dropdown, .year_dropdown)", context).each(function () {

            var $this = $(this);
            var control = $(this).parent();

            // Add UI elements.

            var bg = $("<div class='bg'><input type='text' /></div>");
            $this.appendTo(bg);
            control.text("").append(bg);
            bg.before("<div class='leftbar'></div>").after("<div class='rightbar'></div>");
        });
    };

    $.fn.dropdown = function () {
        return this.each(function () {
            
            var dropdown = $(this);
            dropdown.addClass("dropdown");

            // Append vertical line and an arrow to simulate down arrow.
            
            dropdown.after("<div class='dropdown-arrow'></div>").after("<div class='dropdown-vline'></div>");

            // Make the textbox readonly and hide the select.
            
            $("input[type='text']", dropdown).attr("readonly", "readonly");
            $("select", dropdown).hide();
            
            // Create the scroll pan.
            
            _createScrollPan(dropdown.parent(), "dropdown");
            
            var scrollpan = $(".dropdown-scrollpan", dropdown.parent());
            scrollpan.hide().bind("mousedownoutside", function (event) {
                if ($(event.target).closest(".dropdown_control").length > 0)
                    return;
                $(this).hide();
                event.stopPropagation();
            });
            
            // Opening and closing of drop down.

            dropdown.click(function (event) {
                if (scrollpan.css("display") == "none") {
                    $(".dropdown-scrollpan").hide();
                    var parentWidth = dropdown.parent().width();
                    scrollpan.width(parentWidth);
                    if ($.browser.msie && $.browser.version.indexOf("7") == 0)
                        scrollpan.css("margin-left", "-" + parentWidth + "px");
                    if (dropdown.find("option").length > dropdown.find("select").attr("size"))
                        scrollpan.find(".dropdown-items").width(parentWidth - 12 - 1); //12 = scrollbarWidth, see below
                    else
                        scrollpan.find(".dropdown-items").width(parentWidth);
                    scrollpan.show().css("z-index", "1000");
                }
                else {
                    scrollpan.hide().css("z-index", "-1");
                }
                event.stopPropagation();
            });
            $(".dropdown-vline, .dropdown-arrow", dropdown.parent()).click(function () {
                dropdown.click();
            });
        });
    };

})(jQuery);
