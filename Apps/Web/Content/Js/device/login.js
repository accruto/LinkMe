(function($) {
	var currentRequest;
	
	$(document).ready(function() {
		organiseFields($("#mainbody"));
		$("#mainbody .login .button.login").click(function() {
			if (currentRequest) currentRequest.abort();
			var requestData = {};

			requestData["LoginId"] = $("#mainbody .login #LoginId").val();
			requestData["Password"] = $("#mainbody .login #Password").val();
			requestData["RememberMe"] = $("#mainbody .login #RememberMe").is(":checked");
			currentRequest = $.post($(this).attr("data-url"),
				requestData,
				function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
						showErrInfo($("#mainbody .login"));
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
										window.location.href = $("#mainbody .login .button.login").attr("data-returnurl");
									} else {
									}
									currentRequest = null;
								},
								dataType : "json"
							});							
						} else {
							window.location.href = $("#mainbody .login .button.login").attr("data-returnurl");
						}						
					} else {
						showErrInfo($("#mainbody .login"), data.Errors);
					}
					currentRequest = null;
				}
			).error(function(error) {
				var data = $.parseJSON(error.responseText);
				if (!data.Success) showErrInfo($("#mainbody .login"), data.Errors);
			});
		});
	});
})(jQuery);