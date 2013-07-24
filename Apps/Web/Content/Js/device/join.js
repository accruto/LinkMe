(function($) {
	var currentRequest;
	
	$(document).ready(function() {
		organiseFields($("#mainbody"));
		$("#mainbody .join .button.join").click(function() {
			if (currentRequest) currentRequest.abort();
			var requestData = {};

			requestData["EmailAddress"] = $("#mainbody .join #EmailAddress").val();
			requestData["FirstName"] = $("#mainbody .join #FirstName").val();
			requestData["LastName"] = $("#mainbody .join #LastName").val();
			requestData["JoinPassword"] = $("#mainbody .join #JoinPassword").val();
			requestData["JoinConfirmPassword"] = $("#mainbody .join #JoinConfirmPassword").val();
			requestData["acceptTerms"] = true;
			currentRequest = $.post($(this).attr("data-url"),
				requestData,
				function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
						showErrInfo($("#mainbody .join"));
					} else if (data.Success) {
						if (false) { //addJobAd) {
							var requestData = {};
							requestData["jobAdIds"] = jobAdIds;
							if (currentRequest) currentRequest.abort();
							currentRequest = $.ajax({
								type : "POST",
								url : addJobAdToMobileFolderUrl,
								data : requestData,
								success : function(data, textStatus, xmlHttpRequest) {
									if (data == "") {
									} else if (data.Success) {
										window.location.href = $("#mainbody .join .button.join").attr("data-returnurl");
									} else {
									}
									currentRequest = null;
								},
								dataType : "json"
							});							
						} else {
							window.location.href = $("#mainbody .join .button.join").attr("data-returnurl");
						}						
					} else {
						showErrInfo($("#mainbody .join"), data.Errors);
					}
					currentRequest = null;
				}
			).error(function(error) {
				var data = $.parseJSON(error.responseText);
				if (!data.Success) showErrInfo($("#mainbody .join"), data.Errors);
			});
		});
	});
})(jQuery);