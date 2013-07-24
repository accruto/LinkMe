(function (linkme, $, undefined) {

    linkme.employers = linkme.employers || {};

    linkme.employers.account = (function () {

        return {

            ready: function (options) {
                $("#Location").autocomplete(options.urls.partialMatchesUrl);
            }

        };

    } ());

} (window.linkme = window.linkme || {}, jQuery));

