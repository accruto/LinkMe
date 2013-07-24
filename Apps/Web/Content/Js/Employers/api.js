(function ($) {

    api = {

        getArrayQueryString: function (name, ids) {
            var queryString = "";
            for (var i = 0; i < ids.length; i++) {
                if (i > 0) {
                    queryString = queryString + "&";
                }
                queryString = queryString + name + "=" + ids[i];
            }

            return queryString;
        },

        call: function (url, data, onSuccess, onError, onComplete) {

            var success = false;
            var successResponse = null;
            var errors = new Array();

            $.ajax({
                type: "POST",
                dataType: "json",
                url: url,
                data: data,
                success: function (response) {

                    // Deal with the numerous ways this can fail.

                    if (response == null) {
                        return;
                    }

                    if (response.Success) {
                        success = true;
                        successResponse = response;
                        return;
                    }

                    if (response.Errors != null && response.Errors.length > 0) {
                        $.each(response.Errors, function () {
                            errors.push(this["Message"]);
                        });
                        return;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {

                    // Only report an error for certain textStatus values.

                    success = true;
                    if (textStatus) {

                        // Ignore "abort".

                        if (textStatus == "timeout" || textStatus == "error" || textStatus == "parseerror") {
                            success = false;
                        }
                    }
                },
                complete: function () {

                    // Callback.

                    if (success) {
                        if (onSuccess != null)
                            onSuccess(successResponse);
                        if (onComplete != null)
                            onComplete();
                    }
                    else {
                        if (errors.length > 0) {
                            if (onError != null) {
                                if (onError(errors)) {
                                    if (onComplete != null)
                                        onComplete();
                                }
                            } else {
                                alert(errors[0]);
                                if (onComplete != null)
                                    onComplete();
                            }
                        }
                    }
                }
            });
        }

    }

})(jQuery);