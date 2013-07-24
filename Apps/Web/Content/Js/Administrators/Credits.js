(function (linkme, $, undefined) {

    linkme.administrators = linkme.administrators || {};

    linkme.administrators.credits = (function () {

        return {

            ready: function (options) {

                $(".deallocate").click(function () {

                    var tr = $(this).closest("tr");
                    var allocationId = tr.attr("id");

                    linkme.api.post(
                        options.urls.apiDeallocateUrl,
                        {
                            allocationId: allocationId
                        },
                        function () {
                            $(".status-column", tr).text("Deallocated");
                            $(".deallocate-column", tr).html("");
                        },
                        function (errors) {
                            linkme.validation.showErrors(errors);
                        });
                });
            }

        };

    } ());

} (window.linkme = window.linkme || {}, jQuery));

