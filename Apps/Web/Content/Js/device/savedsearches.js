(function($) {
	var currentRequest = null;

	$(document).ready(function() {
		$("#mainbody .savedsearches .row").click(function(event) {
			var requestData = {};
			if ($(event.target).hasClass("alert")) {
				var currentStatus = "";
				var source = $(event.target);
				if (source.hasClass("active")) currentStatus = "active";
				var url = currentStatus == "active" ? source.attr("data-deleteurl").replace(/~@~/gi, "/") : source.attr("data-createurl").replace(/~@~/gi, "/");
				if (currentStatus == "active") requestData["searchId"] = source.closest(".row").attr("id");
				else requestData["id"] = source.closest(".row").attr("id");
				if (currentRequest) currentRequest.abort();
				currentRequest = $.ajax({
					type : "POST",
					url : url,
					data : requestData,
					success : function(data, textStatus, xmlHttpRequest) {
						if (data == "") {
						} else if (data.Success) {
							if (currentStatus == "active") {
								source.removeClass("active");
							} else {
								source.addClass("active");
							}
						} else {
						}
						currentRequest = null;
					},
					dataType : "json"
				});
				return false;
			} else if ($(event.target).hasClass("delete")) {
				var row = $(event.target).closest(".row");
				requestData["searchId"] = row.attr("id");
				if (currentRequest) currentRequest.abort();
				currentRequest = $.ajax({
					type : "POST",
					url : $(event.target).attr("data-url").replace(/~@~/gi, "/"),
					data : requestData,
					success : function(data, textStatus, xmlHttpRequest) {
						if (data == "") {
						} else if (data.Success) {
							row.remove();
						} else {
						}
						currentRequest = null;
					},
					dataType : "json"
				});
				return false;
			} else {
			}
		});
	});
})(jQuery);