/*
 * Truncates the text in the selected element(s) to fit their width. Truncated 
 * text is made to end with an ellipsis ("...").
 * 
 * Usage:
 * $(selector).ellipsis();
 * $(selector).ellipsis({setTitleIfTruncated: false});
 *
 * Only the selected elements with their style set to "overflow: hidden" will
 * be affected.
 *
 * Limitations:
 *
 * - Elements with children will be treated as if their contents are plaintext.
 * - Does not auto-update on container resize nor content resize.
 */

(function($) {

    $.fn.ellipsis = function(customArguments) {
        var p = {
            setTitleAttrIfTruncated: true
        }

        $.extend(p, customArguments);

        var s = document.documentElement.style;
        var nativeTextOverflow = 'textOverflow' in s || 'OTextOverflow' in s;

        //if (!nativeTextOverflow) {

        return this.each(function() {
            var el = $(this);

            if (el.css("overflow") == "hidden") {

                // Is text-overflow: ellipsis native to the browser (e.g. IE)? Apply that.
                if (nativeTextOverflow) {
                    el.attr("title", el.html());
                    el.css("text-overflow", "ellipsis");
                    return;
                }

                // Otherwise, we have work to do (e.g. Firefox).
                if (!el.data("originalText"))
                    el.data("originalText", el.html());

                var originalText = el.data("originalText");

                var w = el.width();

                var t = $(el.get(0).cloneNode(true)).css({
                    'position': 'absolute',
                    'width': 'auto',
                    'overflow': 'visible',
                    'max-width': 'inherit',
                    'display': 'none'
                });
                el.after(t);

                var text = originalText;
                var lessOrMore = true;        // true: less;  false: more
                var increment = text.length;
                var textLength = text.length;

                if (p.setTitleAttrIfTruncated)
                    el.attr("title", "");

                if (t.width() > el.width()) {

                    // O(logn) where N is length of text
                    while (text.length > 0 && increment >= 1) {
                        lessOrMore = t.width() > el.width();
                        increment = increment - 1;
                        textLength += (lessOrMore ? -1 : 1) * increment;
                        text = originalText.substr(0, textLength);
                        t.html(text + "...");
                    }
                    el.html(t.html());
                    if (p.setTitleAttrIfTruncated)
                        el.attr("title", originalText);
                }

                t.remove();
            }
        });
    }

    $.fn.snippetEllipsis = function(customArguments) {

        var p = {
            setTitleAttrIfTruncated: true
        }

        $.extend(p, customArguments);

        var s = document.documentElement.style;
        var nativeTextOverflow = 'textOverflow' in s || 'OTextOverflow' in s;

        //if (!nativeTextOverflow) {

        return this.each(function() {
            var el = $(this);

            if (el.css("overflow") == "hidden") {

                // Is text-overflow: ellipsis native to the browser (e.g. IE)? Apply that.
                if (nativeTextOverflow) {
                    el.css("text-overflow", "ellipsis");
                    return;
                }

                // Otherwise, we have work to do (e.g. Firefox).
                if (!el.data("originalText"))
                    el.data("originalText", el.html());

                var originalText = el.data("originalText");

                var w = el.width();

                var t = $(el.get(0).cloneNode(true)).css({
                    'position': 'absolute',
                    'width': 'auto',
                    'overflow': 'visible',
                    'max-width': 'inherit',
                    'display': 'none'
                });
                el.after(t);

                var text = originalText;
                var lessOrMore = true;        // true: less;  false: more
                var increment = text.length;
                var textLength = text.length;

                if (p.setTitleAttrIfTruncated)
                    el.attr("title", "");
                /*if (t.width() > el.width()) {
                // O(logn) where N is length of text
                while (text.length > 0 && increment >= 1) {
                lessOrMore = t.width() > el.width();
                increment = increment - 1;
                textLength += (lessOrMore ? -1 : 1) * increment;
                text = originalText.substr(0, textLength);
                t.html(text + "...");
                }
                el.html(t.html());
                if (p.setTitleAttrIfTruncated)
                el.attr("title", originalText);
                }*/
                if (text.length > 150) {
                    text = originalText.substr(0, 150);
                    t.html(text + "...");
                    el.html(t.html());
                    if (p.setTitleAttrIfTruncated)
                        el.attr("title", originalText);
                }

                t.remove();
            }
        });
    }

    $.fn.customEllipsis = function(customArguments) {

        var p = {
            setTitleAttrIfTruncated: true
        }

        $.extend(p, customArguments);

        var s = document.documentElement.style;
        var nativeTextOverflow = 'textOverflow' in s || 'OTextOverflow' in s;

        if (!nativeTextOverflow) {

            return this.each(function() {
                var el = $(this);

                if (el.css("overflow") == "hidden") {

                    // Is text-overflow: ellipsis native to the browser (e.g. IE)? Apply that.
                    if (nativeTextOverflow) {
                        el.css("text-overflow", "ellipsis");
                        return;
                    }

                    // Otherwise, we have work to do (e.g. Firefox).
                    if (!el.data("originalText"))
                        el.data("originalText", el.html());

                    var originalText = el.data("originalText");

                    //var w = el.width();

                    var t = $(el.get(0).cloneNode(true)).css({
                        'position': 'absolute',
                        'width': 'auto',
                        'overflow': 'visible',
                        'max-width': 'inherit',
                        'display': 'none'
                    });
                    el.after(t);

                    var text = originalText;
                    var lessOrMore = true;        // true: less;  false: more
                    var increment = text.length;
                    var textLength = text.length;

                    //if (p.setTitleAttrIfTruncated)
                    el.attr("title", "");
                    /*if (t.width() > el.width()) {
                    // O(logn) where N is length of text
                    while (text.length > 0 && increment >= 1) {
                    lessOrMore = t.width() > el.width();
                    increment = increment - 1;
                    textLength += (lessOrMore ? -1 : 1) * increment;
                    text = originalText.substr(0, textLength);
                    t.html(text + "...");
                    }
                    el.html(t.html());*/
                    if (p.setTitleAttrIfTruncated)
                        el.attr("title", originalText);
                    //}
                    if (customArguments == null) {
                        customArguments = 23;
                    }
                    if (text.length > customArguments) {
                        text = originalText.substr(0, customArguments);
                        t.html(text + "...");
                        el.html(t.html());
                        //if (p.setTitleAttrIfTruncated)
                        el.attr("title", originalText);
                    }

                    t.remove();
                }
            });
        }
    }

    $.fn.skillEllipsis = function() {

        return this.each(function() {

            var el = $(this);

            // Otherwise, we have work to do (e.g. Firefox).
            if (!el.data("originalText"))
                el.data("originalText", el.html());

            var originalText = el.data("originalText");

            var text = originalText;
            var textLength = text.length;

            el.attr("title", "");

            if (textLength > 180) {
                text = originalText.substr(0, 180);
                el.html(text + "...");
                el.attr("title", originalText);
            }
        });
    }

    $.fn.industriesEllipsis = function() {

        return this.each(function() {

            var el = $(this);

            // Otherwise, we have work to do (e.g. Firefox).
            if (!el.data("originalText"))
                el.data("originalText", el.html());

            var originalText = el.data("originalText").replace(/&amp;/g, "&");

            var text = originalText;
            var textLength = text.length;

            el.attr("title", "");

            if (textLength > 20) {
                text = originalText.substr(0, 20);
                el.html(text + "...");
                el.attr("title", originalText);
            }
        });
    }

})(jQuery);