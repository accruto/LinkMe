(function($) {
    $.fn.center = function() {

        var object = this;
        
        /*if(!init) {*/

        object.css('margin-top', $(window).height() / 2 - this.height() / 2);
        object.css('margin-left', $(window).width() / 2 - this.width() / 2);

        $(window).resize(function() {
            object.center();
        });

        /*} else {
			
		var marginTop = $(window).height() / 2 - this.height() / 2;
        var marginLeft = $(window).width() / 2 - this.width() / 2;
			
		marginTop = (marginTop < 0) ? 0 : marginTop;
        marginLeft = (marginLeft < 0) ? 0 : marginLeft;

		marginTop = marginTop;
        marginLeft = marginLeft;

		object.stop();
        object.animate(
        {
        marginTop: marginTop, 
        marginLeft: marginLeft
        }, 
        150, 
        'linear'
        );
		
	}*/
    }
})(jQuery);