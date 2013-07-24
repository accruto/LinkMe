(function (linkme, $, undefined) {

    linkme.administrators = (function () {

        return {

            ready: function (options) {
                $("#OrganisationName").autocomplete(options.urls.apiOrganisationsPartialMatchesUrl);
                $("#ParentFullName").autocomplete(options.urls.apiOrganisationsPartialMatchesUrl);
                $("#Location").autocomplete(options.urls.apiLocationPartialMatchesUrl);
            }

        };

    } ());

} (window.linkme = window.linkme || {}, jQuery));

