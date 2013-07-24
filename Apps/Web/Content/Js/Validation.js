(function (linkme, $, undefined) {

    linkme.validation = (function () {

        return {
            
            showErrors: function(errors) {
                $("#error-summary").show();
                var messages = $("#error-message").find("ul");
                messages.empty();
                for (var i = 0; i < errors.length; i++) {
                    messages.append("<li>" + errors[i].Message + "</li>");
                }
            },

            clearErrors: function () {
                $("#error-summary").hide();
                var messages = $("#error-message").find("ul");
                messages.empty();
            }
        };

    } ());

} (window.linkme = window.linkme || {}, jQuery));

