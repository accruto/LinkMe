(function (linkme, $, undefined) {

    linkme.employers = linkme.employers || {};
    linkme.employers.jobads = linkme.employers.jobads || {};

    linkme.employers.jobads.jobad = (function () {

        var _settings = {
            urls: {}
        };

        return {

            ready: function (options) {

                // Settings.

                $.extend(_settings, options);

                // Auto complete on Location.

                $("#Location").autocomplete(_settings.urls.apiLocationPartialMatchesUrl);

                // Salary validations.

                $.fn.validateSalary = function () {
                    return this.each(function () {
                        $(this).keydown(function (event) {

                            // Allow: backspace, delete, tab, escape, and enter

                            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13) {
                                return;
                            }

                            // Allow: Ctrl+A

                            if (event.keyCode == 65 && event.ctrlKey === true) {
                                return;
                            }

                            // Allow: home, end, left, right.

                            if (event.keyCode >= 35 && event.keyCode <= 39) {
                                return;
                            }

                            // If not a number stop the keypress.

                            if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                                event.preventDefault();
                            }
                        });
                    });
                };

                $("#SalaryLowerBound").validateSalary();
                $("#SalaryUpperBound").validateSalary();

                // Logo.
                
                $(".js-delete-logo").click(function () {

                    // Remove the file reference.

                    $("#LogoId").val("");

                    // Update the UI.

                    $("#display-logo").hide();
                    $("#upload-logo").show();
                    $("#upload-logo-help").show();
                });
            }

        };

    } ());

} (window.linkme = window.linkme || {}, jQuery));

