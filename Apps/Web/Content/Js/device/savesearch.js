(function($) {
	var currentRequest = null;

	$(document).ready(function() {
		organiseFields($("#mainbody"));
		if (errorMsg.length > 0) showErrInfo($("#mainbody .savesearch"), errorMsg);
		// $("#mainbody .savesearch .button.save").click(function() {
            // var requestData = {};
            // requestData["name"] = $("#mainbody .savesearch #Name").val();
            // requestData["createAlert"] = $("#mainbody .savesearch #CreateEmailAlert:checked").length > 0;
            // currentRequest = $.post($(this).attr("data-url"),
				// requestData,
				// function(data, textStatus, xmlHttpRequest) {
					// if (data == "") {
						// showErrInfo($("#mainbody .savesearch"));
					// } else if (data.Success) {
						// window.location.href = $("#mainbody .savesearch .button.save").attr("data-returnurl");
					// } else {
						// showErrInfo($("#mainbody .savesearch"), data.Errors);
					// }
					// currentRequest = null;
				// }
			// ).error(function(error) {
				// var data = $.parseJSON(error.responseText);
				// if (!data.Success) showErrInfo($("#mainbody .savesearch"), data.Errors);
			// });
		// });
	});
})(jQuery);