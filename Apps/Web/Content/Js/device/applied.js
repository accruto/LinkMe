(function($) {
	$(document).ready(function() {
		$(".applied .suggestedjobs .row .titleline .icon.jobtype").each(function() {
			getJobType($(this));
		});
	});
	
	getJobType = function(icon) {
        //primary job type and sub job types
		var jobTypesInAds = icon.attr("jobtypes").replace(/ /gi, "").split(",");
		var jobTyepsInCriteria = "FullTime,PartTime,Contract,Temp,JobShare".replace(/ /gi, "").split(",");
		var primaryTypeArray = $.map(jobTyepsInCriteria, function(item) {
			return $.inArray(item, jobTypesInAds) < 0 ? null : item;
		});
		var primaryType = "None";
		if (primaryTypeArray.length > 0) primaryType = primaryTypeArray[0];
		icon.attr("class", "icon jobtype " + primaryType);
	}
})(jQuery);