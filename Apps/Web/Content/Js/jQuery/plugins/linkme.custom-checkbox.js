/*  Limitations:
    Does not respond to programmatic changes to the 'checked' attribute of the underlying checkbox.
    
    Workaround:
    .refresh() method added to each checkbox. Call after setting 'checked' programmatically.
    e.g. $("input#chkMyCheckbox").refresh();
*/

(function($) {
    $.fn.customCheckbox = function(customArguments) {
        var p = {
            tagName: "div",
            cssClass: "",
            hoverClass: "ui-hover",
            downClass: "ui-down",
            hoverDownClass: "ui-hoverdown",
            checkedClass: "ui-checked",
            checkedHoverClass: "ui-checked-hover",
            checkedDownClass: "ui-checked-down",
            checkedHoverDownClass: "ui-checked-hoverdown",
            mutuallyExclusiveStateClasses: true,
            labelTextInside: true,
            labelTagName: "span"
        };

        $.extend(p, customArguments);

        this.each(function() {
            chk = $(this);

            var originalLabel = $("[for='" + chk.attr("id") + "']");
            var rawVis = document.createElement(p.tagName);
            var vis = $(rawVis);
            var visLabel = p.labelTagName ? $(document.createElement(p.labelTagName)) : null;

            // Label can be its own element, or not
            if (p.labelTextInside)
                if (visLabel) {
                visLabel.html(originalLabel.html());
                vis.append(visLabel);
            } else {
                vis.html(originalLabel.html());
            }

            // Attach states etc. to replacement element
            vis.data("form-element", chk);
            vis.data("hover", false);
            vis.data("down", false);

            // Insert replacement element
            vis.addClass(p.cssClass);
            chk.before(vis);     // "before" preserves expected :first-child behaviour - "after" does not

            // Hide the replaced elements
            originalLabel.hide();
            chk.hide();

            // Attach the update function to replacement element
            vis.data("updateAppearanceFunction", function() {
                // this: vis
                this.data("checked", this.data("form-element").is(":checked"));
                if (p.mutuallyExclusiveStateClasses) {
                    this.toggleClass(p.hoverClass, this.data("hover") && !this.data("down") && !this.data("checked"));
                    this.toggleClass(p.downClass, !this.data("hover") && this.data("down") && !this.data("checked"));
                    this.toggleClass(p.hoverDownClass, this.data("hover") && this.data("down") && !this.data("checked"));
                    this.toggleClass(p.checkedClass, !this.data("hover") && !this.data("down") && this.data("checked"));
                    this.toggleClass(p.checkedHoverClass, this.data("hover") && !this.data("down") && this.data("checked"));
                    this.toggleClass(p.checkedDownClass, !this.data("hover") && this.data("down") && this.data("checked"));
                    this.toggleClass(p.checkedHoverDownClass, this.data("hover") && this.data("down") && this.data("checked"));
                } else {
                    this.toggleClass(p.hoverClass, this.data("hover"));
                    this.toggleClass(p.downClass, this.data("down"));
                    this.toggleClass(p.hoverDownClass, this.data("hover") && this.data("down"));
                    this.toggleClass(p.checkedClass, this.data("checked"));
                    this.toggleClass(p.checkedHoverClass, this.data("hover") && this.data("checked"));
                    this.toggleClass(p.checkedDownClass, this.data("down") && this.data("checked"));
                    this.toggleClass(p.checkedHoverDownClass, this.data("hover") && this.data("down") && this.data("checked"));
                }
            });

            vis.data("form-element").get(0).refresh = function() {
                vis.data("updateAppearanceFunction").apply(vis);
            };

            // Stop text-selection
            if (typeof rawVis.onselectstart != "undefined") // IE route
                rawVis.onselectstart = function() { return false }

            vis.mousedown(function() {
                vis.data("down", true);
                vis.data("updateAppearanceFunction").apply(vis);
                vis.data("form-element").mousedown();
                return false; // Stops text selection in browsers other than IE
            });

            vis.mouseup(function() {
                // Toggle underlying checkbox first
				if (vis.data("form-element").is(":checked")) vis.data("form-element").removeAttr("checked");
				else vis.data("form-element").attr("checked", "checked");
                vis.data("down", false);
                vis.data("updateAppearanceFunction").apply(vis);
                vis.data("form-element").mouseup();
            });

            vis.click(function() {
                vis.data("form-element").click(); // intended to call event
                if (vis.data("form-element").is(":checked")) vis.data("form-element").removeAttr("checked");
				else vis.data("form-element").attr("checked", "checked"); // intended to undo the checkbox toggling inadvertently done by the above line
                vis.data("updateAppearanceFunction").apply(vis);
            });

            // IE 'mutes' the second click event in a double-click: let's turn it back into a second click
            vis.dblclick(function() {
                var isMSIE = false;
				/*@cc_on
				isMSIE = true;
				@*/
                if (isMSIE)
                    vis.click();
            });

            vis.mouseenter(function() {
                vis.data("hover", true);
                vis.data("updateAppearanceFunction").apply(vis);
                vis.data("form-element").mouseenter();
            });

            vis.mouseleave(function() {
                vis.data("hover", false);
                vis.data("updateAppearanceFunction").apply(vis);
                vis.data("form-element").mouseleave();
            });

            vis.data("updateAppearanceFunction").apply(vis);
        });

        return this;
    }
})(jQuery);