(function($) {
	String.prototype.unMungeUrl = function() {
		return this.replace(/~@~/gi, "/");
	}
})(jQuery);