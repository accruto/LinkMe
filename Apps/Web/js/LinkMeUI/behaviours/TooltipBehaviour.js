LinkMeUI.TooltipBehaviour = Class.create({

    initialize: function(params) {
        this.params = params;

        if (params.tooltipStyle = LinkMeUI.TooltipBehaviour.STYLE_CALLOUT) {
            params.createTooltipFunction = LinkMeUI.TooltipBehaviour.createCalloutTooltip;
            params.positionTooltipFunction = LinkMeUI.TooltipBehaviour.positionCalloutTooltip;
            params.setTooltipTextFunction = LinkMeUI.TooltipBehaviour.setCalloutTooltipText;
        }

        params.elementToBehave.observe('mouseover',
            LinkMeUI.TooltipBehaviour.mouseOver.bindAsEventListener(
                params.elementToBehave,
                params.elementToBehave.attributes["id"].value,
                params.elementToBehave.attributes["id"].value + "_tip",
                params.tooltipText,
                params.createTooltipFunction,
                params.positionTooltipFunction,
                params.setTooltipTextFunction
            )
        );

        params.elementToBehave.observe('mouseout',
            LinkMeUI.TooltipBehaviour.mouseOut.bindAsEventListener(
                params.elementToBehave,
                params.elementToBehave.attributes["id"].value,
                params.elementToBehave.attributes["id"].value + "_tip"
            )
        );
    }
});



LinkMeUI.TooltipBehaviour.STYLE_PLAIN = "";
LinkMeUI.TooltipBehaviour.STYLE_CALLOUT = "Callout style";



LinkMeUI.TooltipBehaviour.isIE6 =
   document.all &&
   (navigator.userAgent.toLowerCase().indexOf("msie 6.") != -1);
          
LinkMeUI.TooltipBehaviour.isIE =
   document.all &&
   (navigator.userAgent.toLowerCase().indexOf("msie") != -1);
          
           
LinkMeUI.TooltipBehaviour.makeWindowed = function(p_div) {
    if (this.isIE6) {
        var opacify = function() {
            var estBorderWidth = (p_div.offsetWidth - p_div.clientWidth) / 2;
            var estBorderHeight = (p_div.offsetHeight - p_div.clientHeight) / 2;
            var html =
                  "<iframe src=\"BLOCKED SCRIPT'&lt;html&gt;&lt;/html&gt;';\" " +
                  "style=\"position: absolute; display: block; " +
                  "z-index: -1; top: -" + estBorderHeight + "px; left: -" + estBorderWidth + "px;" +
                  "width: " + p_div.offsetWidth + "px; height: " + p_div.offsetHeight + "px;" +
                  "filter: mask(); background: none; \"></iframe>";
            if (p_div) p_div.innerHTML += html;

            // force refresh of div
            
            var olddisplay = p_div.style.display;
            p_div.style.display = 'none';
            p_div.style.display = olddisplay;
        };
        opacify.delay(0.00001);
    };
}



LinkMeUI.TooltipBehaviour.createAndShowTooltip = function(e, tooltipInvokerId, tooltipElementId, tooltipText, withMouseInOutEvents,
    createTooltipFunction,
    positionTooltipFunction,
    setTooltipTextFunction
) {
    // Use existing tooltipElement for this "TooltipInvoker" (TooltipIcon, button etc.), if any
    var tooltipElement = $(tooltipElementId);
    var tooltipInvoker = $(tooltipInvokerId);

    if (tooltipElement == null) {
        var tooltipElement;
        if (createTooltipFunction) {
            tooltipElement = createTooltipFunction.call(this, e, tooltipInvoker, tooltipElementId);
        } else {
            tooltipElement = $(document.createElement('div'));
            tooltipElement.setAttribute('id', tooltipElementId);
            tooltipElement.addClassName('tooltip');
        }
    }

    if (setTooltipTextFunction)
        setTooltipTextFunction.call(this, tooltipElement, tooltipText);
    else
        tooltipElement.update(tooltipText);

    tooltipElement.hide();

    if ($(tooltipElementId) == null)
        document.body.appendChild(tooltipElement);

    if (positionTooltipFunction)
        positionTooltipFunction.call(
            this,
            tooltipElement,
            tooltipInvoker,
            { x: Event.pointerX(e), y: Event.pointerY(e) }
        );
    else
        tooltipElement.setStyle({ 'position': 'absolute',
            'left': (Event.pointerX(e) + 1) + 'px',
            'top': (Event.pointerY(e) + 1) + 'px'
        });


    if (!LinkMeUI.TooltipBehaviour.isIE)
        Effect.Appear(tooltipElement, { duration: 0.25 });
    else {
        tooltipElement.show();
        LinkMeUI.TooltipBehaviour.makeWindowed(tooltipElement);  // For IE6 - guarantee div to be above SELECT elements
    }

    Event.stop(e);


    if (withMouseInOutEvents) {
        tooltipElement.mouseOver = function(e) {
            LinkMeUI.TooltipBehaviour.mouseOver(e, tooltipInvokerId, tooltipElementId, tooltipText);
        };
        tooltipElement.mouseOut = function(e) {
            LinkMeUI.TooltipBehaviour.mouseOut(e, tooltipInvokerId, tooltipElementId);
        };
        tooltipElement.observe('mouseover', tooltipElement.mouseOver);
        tooltipElement.observe('mouseout', tooltipElement.mouseOut);
    } else {
        tooltipElement.mouseClick = function(e) {
            LinkMeUI.TooltipBehaviour.hideTooltipById(tooltipElementId);
        }
        document.observe('click', tooltipElement.mouseClick);
        tooltipElement.observe('click', tooltipElement.mouseClick);
    }
}



LinkMeUI.TooltipBehaviour.hideTooltipById = function(tooltipElementId) {
    var tooltipElement = $(tooltipElementId)
    if (!tooltipElement.visible())
        return;

    if (!LinkMeUI.TooltipBehaviour.isIE)
        Effect.Fade(tooltipElement, { duration: 0.25 })
    else
        tooltipElement.hide();

    if (tooltipElement.mouseClick) {
        tooltipElement.stopObserving('click', tooltipElement.mouseClick);
        document.stopObserving('click', tooltipElement.mouseClick);
    }
    if (tooltipElement.mouseOver) {
        tooltipElement.stopObserving('mouseover', tooltipElement.mouseOver);
        tooltipElement.mouseOver = null;
    }
    if (tooltipElement.mouseOut) {
        tooltipElement.stopObserving('mouseout', tooltipElement.mouseOut);
        tooltipElement.mouseOut = null;
    }
}



LinkMeUI.TooltipBehaviour.createCalloutTooltip = function(e, tooltipInvoker, tooltipElementId) {
    var calloutDiv = $(document.createElement('div'));
    var calloutHead = $(document.createElement('div'));
    var calloutBody = $(document.createElement('div'));
    var calloutFoot = $(document.createElement('div'));
    var calloutTail = $(document.createElement('div'));
    calloutDiv.className = "callout-tooltip";
    calloutHead.className = "callout-tooltip-head";
    calloutBody.className = "callout-tooltip-body";
    calloutFoot.className = "callout-tooltip-foot";
    calloutTail.className = "callout-tooltip-tail";
    calloutDiv.appendChild(calloutHead);
    calloutDiv.appendChild(calloutBody);
    calloutDiv.appendChild(calloutFoot);
    calloutDiv.appendChild(calloutTail);
    calloutDiv.setAttribute('id', tooltipElementId);
    return calloutDiv;
}

LinkMeUI.TooltipBehaviour.positionCalloutTooltip = function(tooltipElement, tooltipInvoker, mousePos) {
    tooltipElement = $(tooltipElement);

    var invokerPos = tooltipInvoker.cumulativeOffset();

    tooltipElement.setStyle({ 'position': 'absolute',
        'left': (invokerPos.left) + 'px',
        'top': (invokerPos.top + tooltipInvoker.offsetHeight) + 'px'
    });

    var offscreenOffset = 0;
    var tooltipMarginLeft = parseInt(tooltipElement.getStyle('margin-left').split("px")[0]);
    var tooltipMarginRight = parseInt(tooltipElement.getStyle('margin-right').split("px")[0]);
    var tooltipWidth = tooltipElement.getDimensions().width;
    var tooltipRight = invokerPos.left - tooltipMarginLeft + tooltipWidth;
    var viewportRight = document.viewport.getWidth() + document.viewport.getScrollOffsets().left;
    var tailBasePositionLeft = tooltipInvoker.offsetWidth / 2;

    // Is offscreen? Offset (will be negative).
    if (tooltipRight > viewportRight) {
        offscreenOffset = viewportRight - tooltipRight;
        if (-offscreenOffset > (tooltipWidth - tailBasePositionLeft + Math.min(tooltipMarginLeft * 2, 0) + Math.min(tooltipMarginRight * 2, 0)))
            offscreenOffset = -(tooltipWidth - tailBasePositionLeft + Math.min(tooltipMarginLeft * 2, 0) + Math.min(tooltipMarginRight * 2, 0));
        tooltipElement.setStyle({ 'left': (invokerPos.left + offscreenOffset) + 'px' });
    }

    tailElement = tooltipElement.down(".callout-tooltip-tail");
    tailElement.setStyle({ 'position': 'absolute',
        'left': (tailBasePositionLeft - offscreenOffset) + 'px'
    });
}

LinkMeUI.TooltipBehaviour.setCalloutTooltipText = function(tooltipElement, tooltipText) {
    tooltipElement.down(".callout-tooltip-body").update(tooltipText);
        
    // Ensure IE6 correctly spaces elements
    if (LinkMeUI.TooltipBehaviour.isIE6)
        tooltipElement.down(".callout-tooltip-body").down().addClassName("ie_firstchild");
}



/* Use this to manually add Tooltip behaviour (typically, in an OnMouseOver handler) */
LinkMeUI.TooltipBehaviour.mouseOver = function(e, tooltipInvokerId, tooltipElementId, tooltipText, createTooltipFunction, positionTooltipFunction, setTooltipTextFunction) {
    var timeoutsHolder = $(tooltipInvokerId);

    if (timeoutsHolder.hideTooltipByIdTimeout)
        window.clearTimeout(timeoutsHolder.hideTooltipByIdTimeout);

    if (window.event) {
        var eventToPass = {};
        for (var i in e) eventToPass[i] = e[i];
    } else {
        eventToPass = e;
    }

    if (!$(tooltipElementId) || !$(tooltipElementId).visible())
        timeoutsHolder.showTooltipTimeout =
            LinkMeUI.TooltipBehaviour.createAndShowTooltip.delay(0.3, eventToPass, tooltipInvokerId, tooltipElementId, tooltipText, true, createTooltipFunction, positionTooltipFunction, setTooltipTextFunction);
}



/* Use this to manually add Tooltip behaviour (typically, in an OnMouseOut handler) */
LinkMeUI.TooltipBehaviour.mouseOut = function(e, tooltipInvokerId, tooltipElementId) {
    var timeoutsHolder = $(tooltipInvokerId);

    if (timeoutsHolder.showTooltipTimeout)
        window.clearTimeout(timeoutsHolder.showTooltipTimeout);

    if ($(tooltipElementId) && $(tooltipElementId).visible())
        timeoutsHolder.hideTooltipByIdTimeout =
            LinkMeUI.TooltipBehaviour.hideTooltipById.delay(0.3, tooltipElementId);
}



/* Use this to manually add Tooltip behaviour (typically, in an OnClick handler) */
LinkMeUI.TooltipBehaviour.toggle = function(e, tooltipInvokerId, tooltipElementId, tooltipText, createTooltipFunction, positionTooltipFunction, setTooltipTextFunction) {
    // This is the toggle-off part of the behaviour
    var tooltipElement = $(tooltipElementId);
    if (tooltipElement && tooltipElement.visible()) {
        LinkMeUI.TooltipBehaviour.hideTooltipById(tooltipElementId);
        return;
    }

    LinkMeUI.TooltipBehaviour.createAndShowTooltip(e, tooltipInvokerId, tooltipElementId, tooltipText, false, createTooltipFunction, positionTooltipFunction, setTooltipTextFunction);
}
