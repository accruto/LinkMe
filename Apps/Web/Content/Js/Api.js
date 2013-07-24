(function (linkme, $, undefined) {

    linkme.api = (function () {

        var _parseQueryString = function (queryString) {

            var parameters = {};

            // Does not handle multiple values.

            var splitQueryString = queryString.split('&');
            for (var index = 0; index < splitQueryString.length; index++) {
                var parameter = splitQueryString[index].split('=');
                var value = decodeURIComponent(parameter[1]);
                if (value)
                    parameters[parameter[0]] = value;
            }

            return parameters;
        };

        var _getQueryString = function (parameters) {
            return $.param(parameters);
        };

        var _getUrl = function (baseUrl, parameters) {
            var queryString = _getQueryString(parameters);
            return queryString ? baseUrl + "?" + queryString : baseUrl;
        };

        var _getHtml = function (url, parameters, async, onSuccess, onError, onComplete) {

            if (parameters) {
                url = _getUrl(url, parameters);
            }

            $.ajax({
                type: "GET",
                url: url,
                async: async,
                dataType: "html",
                success: function (data) {
                    if (onSuccess)
                        onSuccess(data);
                },
                error: function () {
                    if (onError)
                        onError();
                },
                complete: function () {
                    if (onComplete)
                        onComplete();
                }
            });
        };

        return {

            parseQueryString: function (queryString) {
                return _parseQueryString(queryString);
            },

            getUrl: function (baseUrl, parameters) {
                return _getUrl(baseUrl, parameters);
            },

            getHtml: function (url, parameters, onSuccess, onError, onComplete) {
                _getHtml(url, parameters, true, onSuccess, onError, onComplete);
            },

            getSyncHtml: function (url, parameters, onSuccess, onError, onComplete) {
                _getHtml(url, parameters, false, onSuccess, onError, onComplete);
            },

            post: function (url, data, onSuccess, onError, onComplete) {

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
                    error: function (jqXhr, textStatus, errorThrown) {

                        // Only report an error for certain textStatus values.

                        success = true;
                        if (textStatus) {

                            // Ignore "abort".

                            if (textStatus == "timeout" || textStatus == "error" || textStatus == "parseerror") {

                                success = false;

                                // Try to parse the response.

                                var errorResponse = $.parseJSON(jqXhr.responseText);
                                if (!errorResponse.Success) {
                                    if (errorResponse.Errors && $.isArray(errorResponse.Errors)) {
                                        errors = errorResponse.Errors;
                                    }
                                }
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
            },

            checkHash: function (location, getUrl) {

                if (location && location.hash && getUrl) {

                    var hash = location.hash;
                    if (hash.length > 0 && hash[0] == '#')
                        hash = hash.substr(1);

                    var url = getUrl(hash);
                    if (url)
                        location.href = url;
                }

            }

        };

    } ());

} (window.linkme = window.linkme || {}, jQuery));

 

 
