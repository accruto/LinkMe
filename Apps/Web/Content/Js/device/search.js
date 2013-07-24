(function($) {
	$(document).ready(function() {
		organiseFields($("#mainbody"));
		$(".salaryrate_field .radiobutton[value='SalaryRateYear']").click();
		$("form#search").submit(function() {
			$(this).find("#Location").val($("#Location").realValue());
		});
	});
})(jQuery);