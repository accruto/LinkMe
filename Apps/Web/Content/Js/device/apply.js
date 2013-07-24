(function($) {
	var currentRequest = null;

	$(document).ready(function() {
		organiseFields($("#mainbody"));
		var fileName = $("#mainbody .apply .mostrecentresume").text();
		if (fileName != "") $("#mainbody .apply .radiobuttons_field label[for='LastUsed']").append("<span class='blue'>(" + fileName + ")</span>");
		$("#mainbody .apply .checkbox_control > label").html("Send me&nbsp;<span class='blue'>(" + $("#mainbody .apply #SendMeConfirmation").attr("data-email") + ")</span>&nbsp;a confirmation email");
		$("#mainbody .apply .managedinternally .button.apply, #mainbody .apply .managedinternally .button.apply").click(function() {
			if (currentRequest) currentRequest.abort();
			var requestData = {};

			var url;
			var resumeSource = $("#mainbody .apply .radiobutton.checked").val();
			if (resumeSource == "LastUsed") {
				url = $("#mainbody .apply .button.apply").attr("data-applywithlastusedresumeurl");
			} else {
				url = $("#mainbody .apply .button.apply").attr("data-applywithprofileurl");
			}

			currentRequest = $.post(url,
				requestData,
				function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
						showErrInfo($("#mainbody .apply"));
					} else if (data.Success) {
						if ($("#mainbody .apply .managedexternally").length > 0) {
							var href = $("#mainbody .apply .managedexternally .button.apply").attr("href");
							if (href != null) {
								if (href.indexOf("?") > 0)
									href = href + "&applicationId=" + data.Id;
								else
									href = href + "?applicationId=" + data.Id;
								$("#mainbody .apply .managedexternally .button.apply").attr("href", href);
							}
						}
						var appliedUrl = $("#mainbody .apply .button.apply").attr("data-appliedurl");
						if (appliedUrl.indexOf("?") > 0) appliedUrl = appliedUrl + "&applicationId=" + data.Id;
						else appliedUrl = appliedUrl + "?applicationId=" + data.Id;
						window.location.href = appliedUrl;
					} else {
						showErrInfo($("#mainbody .apply"));
					}
					currentRequest = null;
				}).error(function(error) {
					showErrInfo($("#mainbody .apply"));
				});
		});
		$("#mainbody .apply .appliedforexternally .button.apply").click(function() {
            $.ajax({
                type: "POST",
                url: $("#mainbody .apply .appliedforexternally .button.apply").attr("data-externallyappliedurl").replace(/~@~/gi, "/"),
                data: null,
                async: false,
                dataType: "json",
                contentType: "application/json",
                success: function(data, textStatus, xmlHttpRequest) {
                },
                error: function(error) {
                }
            });
            window.location = $("#mainbody .apply .appliedforexternally .button.apply").attr("data-appliedpageurl");
		});
		$("#mainbody .apply .button.save").click(function() {
			if ($(this).hasClass("saved")) return;
			var jobAdIds = new Array();
			jobAdIds.push($(this).attr("id"));
			var url = $(this).attr("data-url");
			var icon = $(this);
			var requestData = {};
			var row = $(this).closest(".row");
			requestData["jobAdIds"] = jobAdIds;
			if (currentRequest) currentRequest.abort();
			currentRequest = $.ajax({
				type : "POST",
				url : url,
				data : requestData,
				success : function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
					} else if (data.Success) {
						icon.addClass("saved").text("JOB SAVED");
					} else {
					}
					currentRequest = null;
				},
				dataType : "json"
			});
		});
	});
})(jQuery);