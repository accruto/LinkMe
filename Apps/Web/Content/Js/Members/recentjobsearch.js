(function($) {
    var currentRequest = null;

	$(document).ready(function() {
		//tabs
		$(".tabs .tab").click(function() {
			if ($(this).hasClass("active")) return false;
			var index = $(".tabs .tab").index($(this));
			$.ajax({
				type : "GET",
				url : $(this).attr("url").unMungeUrl(),
				success : function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
					} else if (data.Success) {
					} else {
						$(".tabcontent .tab:eq(" + index + ")").find(".row, .pagination-container, .text.empty").remove();
						$(".tabcontent .tab:eq(" + index + ")").append(data);
						$(".tabs .tab.active, .tabcontent .tab.active, .tabs .tab:eq(" + index + "), .tabcontent .tab:eq(" + index + ")").toggleClass("active");
						if ($(".tabs .tab.active").hasClass("Recent"))
							$("#subheader .header, #subheader .breadcrumbs li:eq(2)").text("My recent searches");
						else $("#subheader .header, #subheader .breadcrumbs li:eq(2)").text("My favourite searches");
						updateResult();
					}
					currentRequest = null;
				},
				dataType : "html"
			});
			return false;
		});
		//prompt
		$(".prompt .icon.close").click(function() {
			$(this).closest(".prompt").hide();
		});
		updateResult();
		//fields
		organiseFields();
		//overlays
		$(".overlay.rename").find(".button.cancel").click(function() {
			$(this).closest(".overlay.rename").dialog("close");
		});
		$(".overlay.rename").find(".button.rename").click(function() {
			var requestData = {};
			requestData["searchId"] = $(this).attr("searchId");
			requestData["newName"] = $(".overlay.rename #NewName").realValue();
			if (currentRequest) currentRequest.abort();
			currentRequest = $.ajax({
				type : "POST",
				url : $(this).attr("url").unMungeUrl(),
				data : requestData,
				success : function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
					} else if (data.Success) {
						$(".tabcontent .tab.Favourite .row#" + requestData["searchId"] + " .title span").text(requestData["newName"]);
						$(".overlay.rename").dialog("close");
					} else {
					}
					currentRequest = null;
				},
				dataType : "json"
			});
		});
		$(".overlay.delete").find(".button.cancel").click(function() {
			$(this).closest(".overlay.delete").dialog("close");
		});
		$(".overlay.delete").find(".button.delete").click(function() {
			deleteSearch($(this).attr("searchId"));
		});
	});
	
	updateResult = function() {
		//pagination style
		$(".tabcontent .active .pagination-holder").css("margin-left", ((937 - $(".tabcontent .active .pagination-holder").width()) / 2) + "px");
		//pagination event
		$(".pagination-holder a").click(function() {
			$.ajax({
				type : "GET",
				url : $(this).attr("href"),
				success : function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
					} else if (data.Success) {
					} else {
						$(".tabcontent .tab.active").find(".row, .pagination-container, .text.empty").remove();
						$(".tabcontent .tab.active").append(data);
						updateResult();
					}
					currentRequest = null;
				},
				dataType : "html"
			});
			return false;
		});
		//alert and divider style
		$(".tabcontent .Recent .row .icon.alert").each(function(inex, element) {
			var height = $(element).parent().height();
			$(element).css("margin-top", ((height - 25) / 2) + "px");
			$(element).parent().find(".line").css("height", height + "px");
		}).click(function() {
			if ($(this).hasClass("active")) {
				toggleAlert($(this), "active");
			} else {
				toggleAlert($(this), "");
			}
		});
		$(".tabcontent .Favourite .row").each(function(inex, element) {
			var height = $(element).height();
			$(element).find(".icon.alert").css("margin-top", ((height - 25) / 2) + "px").click(function() {
				if ($(this).hasClass("active")) {
					toggleAlert($(this), "active");
				} else {
					toggleAlert($(this), "");
				}
			});
			$(element).find(".line").css("height", (height + 2) + "px");
			$(element).find(".icon.action").css("margin-top", ((height - 34) / 2) + "px");
			$(element).hover(function() {
				$(this).find(".line").css("height", (height - 4) + "px");
			}, function() {
				$(this).find(".line").css("height", (height + 2) + "px");
			});
		});
		$(".tabcontent .Favourite .row .icon.action.rename").click(function() {
			$(".overlay.rename").dialog({
				modal: true,
				width: 740,
				closeOnEscape: false,
				resizable: false,
				dialogClass: "rename-dialog"
			});
			$(".overlay.rename").closest(".ui-dialog").find(".ui-dialog-title").html("Rename the '<span title='" + $(this).closest(".row").find(".title").text() + "'>" + $(this).closest(".row").find(".title").text() + "</span>' search");
			$(".overlay.rename .button.rename").attr("searchId", $(this).closest(".row").attr("id"));
		});
		$(".tabcontent .Favourite .row .icon.action.delete").click(function() {
			$(".overlay.delete").dialog({
				modal: true,
				width: 740,
				closeOnEscape: false,
				resizable: false,
				dialogClass: "delete-dialog",
				title : "Delete favourite search?"
			});
			$(".overlay.delete .text.name span").text($(this).closest(".row").find(".title").text()).attr("title", $(this).closest(".row").find(".title").text());
			$(".overlay.delete .button.delete").attr("searchId", $(this).closest(".row").attr("id"));
		});
	}
	
	deleteSearch = function(searchId) {
		var requestData = {};
		var row = $(".tabcontent .Favourite .row#" + searchId);
		requestData["searchId"] = row.attr("id");
		if (currentRequest) currentRequest.abort();
		currentRequest = $.ajax({
			type : "POST",
			url : $(".overlay.delete .button.delete").attr("url").unMungeUrl(),
			data : requestData,
			success : function(data, textStatus, xmlHttpRequest) {
				if (data == "") {
				} else if (data.Success) {
					row.remove();
					$(".overlay.delete").dialog("close");
				} else {
				}
				currentRequest = null;
			},
			dataType : "json"
		});
	}
	
	toggleAlert = function(source, currentStatus) {
		var name = source.parent().find(".title").text();
		var url = currentStatus == "active" ? source.attr("deleteurl").unMungeUrl() : source.attr("createurl").unMungeUrl();
		var requestData = {};
		if (currentStatus == "active") requestData["searchId"] = source.attr("alertid");
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
						showSuccMsg("alert", name);
						source.addClass("active");
					}
				} else {
				}
				currentRequest = null;
			},
			dataType : "json"
		});
	}
	
    showSuccMsg = function(type, message) {
        if (type == "alert") {
			$(".prompt.success .text span").text(message).attr("title", message).closest(".prompt").show().delay(5000).fadeOut("slow");
		}
    }	
})(jQuery);