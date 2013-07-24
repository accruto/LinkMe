(function (linkme, $, undefined) {

    linkme.employers = linkme.employers || {};
    linkme.employers.jobads = linkme.employers.jobads || {};

    linkme.employers.jobads.preview = (function () {

        var _settings = {
            urls: {},
            featurePacks: {}
        };

        var _updateFeaturePack = function (logoUrl, featurePack) {

            // Logo.

            if (logoUrl && featurePack.showLogo) {
                $("#jobad-view .summary").addClass("with-logo");
                $("#jobad-view .logo").attr("src", logoUrl);
                $("#jobad-view .logo-item").show();
            }
            else {
                $("#jobad-view .summary").removeClass("with-logo");
                $("#jobad-view .logo-item").hide();
                $("#jobad-view .logo").attr("src", "");
            }

            // Highlight.

            if (featurePack.isHighlighted) {
                $(".jobad-list-view").addClass("featured");
            }
            else {
                $(".jobad-list-view").removeClass("featured");
            }

            // Expiry.

            $(".expiry-days").text(featurePack.expiryDays);
            $(".expiry-date").text(featurePack.expiryDate);

        };

        var _selectFeaturePack = function (id) {

            switch (id) {
                case "feature-pack-1":
                    $("#FeaturePack1").attr("checked", "checked");
                    _updateFeaturePack(_settings.urls.logoUrl, _settings.featurePacks.featurePack1);
                    break;

                case "feature-pack-2":
                    $("#FeaturePack2").attr("checked", "checked");
                    _updateFeaturePack(_settings.urls.logoUrl, _settings.featurePacks.featurePack2);
                    break;

                default:
                    $("#BaseFeaturePack").attr("checked", "checked");
                    _updateFeaturePack(_settings.urls.logoUrl, _settings.featurePacks.baseFeaturePack);
                    break;
            }
        };

        return {

            ready: function (options) {

                // Settings.

                $.extend(_settings, options);

                // Feature packs.

                var selected = _settings.featurePacks.baseFeaturePack.isSelected
                    ? "#base-feature-pack"
                    : _settings.featurePacks.featurePack1.isSelected
                        ? "#feature-pack-1"
                        : "#feature-pack-2";

                $("#base-feature-pack,#feature-pack-1,#feature-pack-2").largeRadioButtons(
                    $(selected),
                    function (button) {
                        _selectFeaturePack(button.attr("id"));
                    });
            }

        };

    } ());

} (window.linkme = window.linkme || {}, jQuery));

