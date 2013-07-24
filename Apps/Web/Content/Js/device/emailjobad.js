(function($) {
	var currentRequest = null;

	$(document).ready(function() {
		organiseFields($("#mainbody"));
		$("#mainbody .emailjobad .button.send").click(function() {
			if (currentRequest) currentRequest.abort();
			var requestData = {};

            var toNamesValue = $("#ToName").val();
            var toEmailAddressesValue = $("#ToEmailAddress").val();
            var toNames = toNamesValue.split(",");
            var toEmailAddresses = toEmailAddressesValue.split(",");
            var tos = new Array();
            if (toNamesValue != "")
                $.each(toNames, function(index, item) {
                    var pair = {};
                    pair["ToName"] = item;
                    pair["ToEmailAddress"] = toEmailAddresses[index];
                    tos.push(pair);
                });
			var jobAdId = $(this).attr("id");

            currentRequest = $.ajax({
                type: "POST",
                url: $(this).attr("data-url"),
                data: JSON.stringify({
                    FromName: $("#mainbody .emailjobad #FromName").val(),
                    FromEmailAddress: $("#mainbody .emailjobad #FromEmailAddress").val(),
                    Tos: tos,
					JobAdIds : jobAdId
                }),
                dataType: "json",
                contentType: "application/json",
                success: function(data, textStatus, xmlHttpRequest) {
                    if (data == "") {
                        showErrInfo($("#mainbody .emailjobad"));
                    } else if (data.Success) {
                        window.location.href = $("#mainbody .emailjobad .button.send").attr("data-succurl");
                    } else {
						showErrInfo($("#mainbody .emailjobad"), data.Errors);
                    }
                    currentRequest = null;
                },
                error: function(error) {
                    var data = $.parseJSON(error.responseText);
                    if (!data.Success) showErrInfo($("#mainbody .emailjobad"), data.Errors);
                }
            });
		});
	});
})(jQuery);