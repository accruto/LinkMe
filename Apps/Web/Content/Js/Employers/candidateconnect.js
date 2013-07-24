(function($) {
	$(document).ready(function() {
		//slides show
		if ($(".slidesshow").length > 0) {
			$(".slidesshow .balls .dot").click(function() {
				if ($(this).hasClass("active")) return;
				
				var currentIndex = $(".slidesshow .balls .dot").index($(this));
				showSlideAtNumberX(currentIndex);
				clearTimeout(slideTimeout);
				slideTimeout = setTimeout("slidesShow()", 60000);
			});
			$(".slidesshow .descs .desc").click(function() {
				if ($(this).hasClass("active")) return;
				
				var currentIndex = $(".slidesshow .descs .desc").index($(this));
				showSlideAtNumberX(currentIndex);
				clearTimeout(slideTimeout);
				slideTimeout = setTimeout("slidesShow()", 60000);
			});
			showSlideAtNumberX(0);			
			slideTimeout = setTimeout("slidesShow()", 10000);
		}
	});
	
	showSlideAtNumberX = function(index) {
		$(".slidesshow .balls .dot.active, .slidesshow .balls .dot:eq(" + index + "), .slidesshow .slide.active, .slidesshow .slide:eq(" + index + "), .slidesshow .descs .desc.active, .slidesshow .descs .desc:eq(" + index + ")").toggleClass("active");
	}
	
	slidesShow = function(type) {
		var currentIndex = $(".slidesshow .balls .dot").index($(".slidesshow .balls .dot.active"));
		if (currentIndex == $(".slidesshow .balls .dot").length - 1) currentIndex = -1;
		showSlideAtNumberX(currentIndex + 1);
		slideTimeout = setTimeout("slidesShow()", 10000);
	}
})(jQuery);
