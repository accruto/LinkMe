/*
 *  jQuery Ellipsis
 *  Mnigrele/Emateu: 8 abril 2011. Aportes de Ekupelian
 *  No copyright
 *
 */
(function($){

    $.fn.ellipsis = function(conf) {
        return this.each(function() {
            setup($(this), conf);
        });
    };

    function setup(element, conf) {

        conf = $.extend({
            lines: 2
        }, conf || {} );

        var lineHeight = parseInt(element.css("line-height"), 10);
		//set default line height to fontsize + 2px if it is not set in css
		if (isNaN(lineHeight)) lineHeight = parseInt(element.css("font-size"), 10) + 2;
		var height = element.height();

        if (height < lineHeight * conf.lines) return false;

        var divCloned = $("<div>").css({
            "font-size" : element.css("font-size"),
            "font-weight" : element.css("font-weight"),
			"font-family" : element.css("font-family"),
			"text-align" : element.css("text-align"),
            "line-height" : element.css("line-height"),
            "position" : "absolute",
			"width" : element.width(),
            "top" : "99px"//"-99999px"
        }).appendTo("body");

		var content = element.text(), length = content.length;
		var leftPos = 0, middlePos = length, rightPos = length;
		
		do {
			divCloned.text(content.substring(0, middlePos));
			if (divCloned.height() > height) rightPos = middlePos;
			else leftPos = middlePos;
			middlePos = Math.floor(leftPos + (rightPos - leftPos) / 2);
		} while (!(leftPos == rightPos || leftPos == rightPos - 1));
		
		if (leftPos == 0) element.text("");
		else if (leftPos == 1) element.text(".");
		else if (leftPos == 2) element.text("..");
		else if (leftPos == length) element.text(content);
		else element.text(content.substring(0, leftPos - 3) + "...");
		element.attr("title", content);
        divCloned.remove();
		return true;
    }

}(jQuery));