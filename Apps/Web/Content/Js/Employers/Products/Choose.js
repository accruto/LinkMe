(function (linkme, $, undefined) {

    linkme.employers = linkme.employers || {};
    linkme.employers.products = linkme.employers.products || {};

    linkme.employers.products.choose = (function () {

        var _settings = {
            urls: {},
            products: []
        };

        var _getProduct = function (id) {

            var product = null;

            $.each(_settings.products, function (index, value) {
                if (value.id == id) {
                    product = value;
                    return false;
                }
            });

            return product;
        };

        var _productChanged = function () {

            var product = _getProduct($("#ContactProductId").val());
            $("#product-cost-price").html(product.pricePerCredit);
            $("#product-cost-total-price").html(product.price);

            // Update the order details.

            linkme.api.getHtml(
                _settings.urls.prepareCompactOrderUrl,
                { productId: $('#ContactProductId').val() },
                function (html) {
                    $('#order-compact-details').html('<div id="order-compact-details">' + html + '</div>');
                });
        };

        return {

            ready: function (options) {

                // Settings.

                $.extend(_settings, options);

                // Features.

                $("#features-hide-details").click(function () {
                    $("#more-features").hide();
                    $("#less-features").show();
                });

                $("#features-more-details").click(function () {
                    $("#more-features").show();
                    $("#less-features").hide();
                });

                // Product change.

                $("#ContactProductId").change(_productChanged);
                $("#ContactProductId").change();
            }

        };

    } ());

} (window.linkme = window.linkme || {}, jQuery));

