/* ---------------------
 * LinkMe-style tooltips
 * ---------------------
 *
 * Dependencies:
 * - jQuery
 * - LinkMeUI.ApplicationPath
 * - callout-tooltip-* CSS selectors from universal-layout.css
 *
 *
 *
 * Usage:
 *
 * // Use data-tooltip attribute from each element as tooltip text
 * $(selector).addTooltip();   
 *
 * // As above, but uses fancier 'callout'-style tooltip
 * $(selector).addTooltip({tooltipStyle: $.LinkMeUI.Tooltips.Styles.CALLOUT});  
 *
 * // Use specified text as tooltip for this element
 * $("#someId").addTooltip({tooltipText: "text"});
 *
 */


(function($) {
    if ($.LinkMeUI == undefined)
        $.LinkMeUI = {};



    // Pseudo-GUID functions from http://stackoverflow.com/questions/105034/how-to-create-a-guid-uuid-in-javascript
    $.LinkMeUI.generatePseudoGuid = function() {
	    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
		    var r = Math.random()*16|0, v = c == 'x' ? r : (r&0x3|0x8);
		    return v.toString(16);
	    }).toUpperCase();
    };



	$.getScript(LinkMeUI.ContentPath + "js/jquery/plugins/jquery.later.js");


    
    $.fn.addTooltip = function(customArguments) {
        var p = {
            tooltipText:  null,
            tooltipStyle: $.LinkMeUI.Tooltips.Styles.DEFAULT
        };
        
        $.extend(p, customArguments);
        
        $(this).each( function () {
            var el = $(this);
            if (!el.attr("id"))
                el.attr("id", el.get(0).tagName.toLowerCase() + $.LinkMeUI.generatePseudoGuid());
                
            var elId = el.attr("id");
        
            if (p.tooltipStyle == $.LinkMeUI.Tooltips.Styles.CALLOUT) {
                p.createTooltipFunction = private.createCalloutTooltip;
                p.positionTooltipFunction = private.positionCalloutTooltip;
                p.setTooltipTextFunction = private.setCalloutTooltipText;
            }
            
            el.bind("mouseover", function(event) {
                $.LinkMeUI.Tooltips.mouseOver.call(el, event, elId, elId + "_tip",
                    p.tooltipText ? p.tooltipText : el.attr("data-tooltip"),
                    p.createTooltipFunction,
                    p.positionTooltipFunction,
                    p.setTooltipTextFunction
                );
            });

            el.bind("mouseout", function(event) {
                $.LinkMeUI.Tooltips.mouseOut.call(el, event, elId, elId + "_tip")
            });
        });
    };
    
    
    
    $.LinkMeUI.Tooltips = {
        Styles: {
            DEFAULT: 0,
            CALLOUT: 1
        },
        
        

        /* Use this to manually add Tooltip behaviour (typically, in an OnMouseOver handler) */
        mouseOver: function(e, tooltipInvokerId, tooltipElementId, tooltipText, createTooltipFunction, positionTooltipFunction, setTooltipTextFunction) {
            var timeoutsHolder = $("#" + tooltipInvokerId);

            if (timeoutsHolder.data("hideTooltipByIdTimeout"))
                timeoutsHolder.data("hideTooltipByIdTimeout").cancel();

            if (window.event) {
                var eventToPass = {};
                for (var i in e) eventToPass[i] = e[i];
            } else {
                eventToPass = e;
            }
            
            var tooltipElement = $("#" + tooltipElementId);

            if (!tooltipElement.get(0) || !tooltipElement.is(":visible"))
                timeoutsHolder.data("showTooltipTimeout",
                    $.later(300, this, private.createAndShowTooltip,
                        [eventToPass, tooltipInvokerId, tooltipElementId, tooltipText, true, createTooltipFunction, positionTooltipFunction, setTooltipTextFunction],
                        false
                    )
                );
                //private.createAndShowTooltip.apply(window, [eventToPass, tooltipInvokerId, tooltipElementId, tooltipText, true, createTooltipFunction, positionTooltipFunction, setTooltipTextFunction]);
        },



        /* Use this to manually add Tooltip behaviour (typically, in an OnMouseOut handler) */
        mouseOut: function(e, tooltipInvokerId, tooltipElementId) {
            var timeoutsHolder = $("#" + tooltipInvokerId);

            if (timeoutsHolder.data("showTooltipTimeout"))
                timeoutsHolder.data("showTooltipTimeout").cancel();
                
            var tooltipElement = $("#" + tooltipElementId);

            if (!tooltipElement.get(0) || tooltipElement.is(":visible"))
                timeoutsHolder.data("hideTooltipByIdTimeout",
                    $.later(300, this, private.hideTooltipById,
                        [tooltipElementId],
                        false
                    )
                );
                    //private.hideTooltipById.apply(window, [tooltipElementId]);
        },



        /* Use this to manually add Tooltip behaviour (typically, in an OnClick handler) */
        toggle: function(e, tooltipInvokerId, tooltipElementId, tooltipText, createTooltipFunction, positionTooltipFunction, setTooltipTextFunction) {
            // This is the toggle-off part of the behaviour
            var tooltipElement = $(tooltipElementId);
            if (tooltipElement && tooltipElement.is(":visible")) {
                private.hideTooltipById(tooltipElementId);
                return;
            }

            private.createAndShowTooltip(e, tooltipInvokerId, tooltipElementId, tooltipText, false, createTooltipFunction, positionTooltipFunction, setTooltipTextFunction);
        }
    };
    
    
    
    var private = {
        isIE:
            document.all && (navigator.userAgent.toLowerCase().indexOf("msie") != -1),
    
        createAndShowTooltip: function(e, tooltipInvokerId, tooltipElementId, tooltipText, withMouseInOutEvents, createTooltipFunction, positionTooltipFunction, setTooltipTextFunction) {
            var el = $(this);
            e = $.event.fix(e);
        
            // Use existing tooltipElement for this "TooltipInvoker" (TooltipIcon, button etc.), if any
            var tooltipElement = $("#" + tooltipElementId);
            var tooltipInvoker = $("#" + tooltipInvokerId);

            if (!tooltipElement.get(0)) {
                var tooltipElement;
                if (createTooltipFunction) {
                    tooltipElement = createTooltipFunction.call(el, e, tooltipInvoker, tooltipElementId);
                } else {
                    tooltipElement = $(document.createElement('div'));
                    tooltipElement.attr("id", tooltipElementId);
                    tooltipElement.attr("class", "tooltip");
                }
            }

            if (setTooltipTextFunction)
                setTooltipTextFunction.call(el, tooltipElement, tooltipText);
            else
                tooltipElement.html(tooltipText);

            tooltipElement.hide();

            if (!$("#" + tooltipElementId).get(0))
                $("body").append(tooltipElement);

            if (positionTooltipFunction)
                positionTooltipFunction.call(el,
                    tooltipElement,
                    tooltipInvoker,
                    { x: e.pageX, y: e.pageY }
                );
            else
                tooltipElement.css({
                    "position": "absolute",
                    "left":     (e.pageX + 1) + "px",
                    "top":      (e.pageY + 1) + "px",
                    "z-index":  999
                });

            if (!private.isIE)
                tooltipElement.fadeIn(250);
            else
                tooltipElement.show();

            e.stopPropagation();
            e.preventDefault();

            if (withMouseInOutEvents) {
                tooltipElement.data("mouseOverFunction", function(e) {
                    $.LinkMeUI.Tooltips.mouseOver(e, tooltipInvokerId, tooltipElementId, tooltipText);
                });
                tooltipElement.data("mouseOutFunction", function(e) {
                    $.LinkMeUI.Tooltips.mouseOut(e, tooltipInvokerId, tooltipElementId);
                });
                tooltipElement.bind("mouseover", tooltipElement.data("mouseOverFunction"));
                tooltipElement.bind("mouseout",  tooltipElement.data("mouseOutFunction"));
            } else {
                tooltipElement.data("mouseClickFunction", function(e) {
                    private.hideTooltipById(tooltipElementId);
                });
                $(document).bind("click", tooltipElement.data("mouseClickFunction"));
                tooltipElement.bind("click", tooltipElement.data("mouseClickFunction"));
            }
        },



        hideTooltipById: function(tooltipElementId) {
            var tooltipElement = $("#"+tooltipElementId);
            if (!tooltipElement.is(":visible"))
                return;

            if (!private.isIE)
                tooltipElement.fadeOut(250);
            else
                tooltipElement.hide();

            if (tooltipElement.mouseClick) {
                tooltipElement.unbind("click", tooltipElement.data("mouseClickFunction"));
                $(document).unbind("click", tooltipElement.data("mouseClickFunction"));
            }
            if (tooltipElement.mouseOver) {
                tooltipElement.unbind("mouseover", tooltipElement.data("mouseOverFunction"));
                tooltipElement.mouseOver = null;
            }
            if (tooltipElement.mouseOut) {
                tooltipElement.unbind("mouseout", tooltipElement.data("mouseOutFunction"));
                tooltipElement.mouseOut = null;
            }
        },



        createCalloutTooltip: function(e, tooltipInvoker, tooltipElementId) {
            return $(document.createElement("div"))
                 .attr("id", tooltipElementId)
                 .attr("class", "callout-tooltip")
                    .append($(document.createElement("div")).attr("class", "callout-tooltip-head"))
                    .append($(document.createElement("div")).attr("class", "callout-tooltip-body"))
                    .append($(document.createElement("div")).attr("class", "callout-tooltip-foot"))
                    .append($(document.createElement("div")).attr("class", "callout-tooltip-tail"));
        },
        
        

        positionCalloutTooltip: function(tooltipElement, tooltipInvoker, mousePos) {
            tooltipElement = $(tooltipElement);

            var invokerPos = tooltipInvoker.offset();

            tooltipElement.css({
                "position": "absolute",
                "left":     (invokerPos.left) + "px",
                "top":      (invokerPos.top + tooltipInvoker.height()) + "px",
                "z-index":  999
            });

            var offscreenOffset = 0;
            var tooltipMarginLeft = parseInt(tooltipElement.css("margin-left").split("px")[0]);
            var tooltipMarginRight = parseInt(tooltipElement.css("margin-right").split("px")[0]);
            var tooltipWidth = tooltipElement.width();
            var tooltipRight = invokerPos.left - tooltipMarginLeft + tooltipWidth;
            var viewportRight = $(window).width() + $(window).scrollLeft();
            var tailBasePositionLeft = tooltipInvoker.width() / 2;

            // Is offscreen? Offset (will be negative).
            if (tooltipRight > viewportRight) {
                offscreenOffset = viewportRight - tooltipRight;
                if (-offscreenOffset > (tooltipWidth - tailBasePositionLeft + Math.min(tooltipMarginLeft * 2, 0) + Math.min(tooltipMarginRight * 2, 0)))
                    offscreenOffset = -(tooltipWidth - tailBasePositionLeft + Math.min(tooltipMarginLeft * 2, 0) + Math.min(tooltipMarginRight * 2, 0));
                tooltipElement.css("left", (invokerPos.left + offscreenOffset) + "px" );
            }

            tailElement = tooltipElement.children(".callout-tooltip-tail");
            tailElement.css({
                "position": "absolute",
                "left":     (tailBasePositionLeft - offscreenOffset) + "px"
            });
        },



        setCalloutTooltipText: function(tooltipElement, tooltipText) {
            tooltipElement.children(".callout-tooltip-body").html(tooltipText);
        }
    };
})(jQuery);