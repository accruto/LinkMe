(function (linkme, $, undefined) {

    linkme.employers = linkme.employers || {};
    linkme.employers.orders = linkme.employers.orders || {};

    linkme.employers.orders.order = (function () {

        var _settings = {
            urls: {},
            productIds: []
        };

        var _prepareOrder = function () {

            linkme.api.getHtml(
                _settings.urls.prepareOrderUrl,
                {
                    productIds: _settings.productIds,
                    useDiscount: $('#UseDiscount').is(':checked'),
                    couponId: $('#CouponId').val(),
                    creditCardType: $('#CardType').val()
                },
                function (html) {
                    $('#order-details').html('<div id="order-details">' + html + '</div>');
                });
        };

        var _applyCoupon = function () {

            // Clear out existing values.

            linkme.validation.clearErrors();
            $('#CouponId').val("");

            // Validate that the coupon is good.
            
            linkme.api.post(
                _settings.urls.apiCouponUrl,
                {
                    code: $('#CouponCode').val(),
                    productIds: _settings.productIds
                },
                function (response) {
                    
                    // Coupon is good so update the order.

                    $('#CouponId').val(response.Id);
                    _prepareOrder();
                },
                function (errors) {
                    
                    // Coupon is not good.

                    linkme.validation.showErrors(errors);
                });
        };

        return {

            ready: function (options) {

                // Settings.

                $.extend(_settings, options);
                
                // Apply a coupon.

                $('#apply').click(function (e) {
                    _applyCoupon();
                    e.preventDefault();
                    return false;
                });

                // Changing the credit card can result in the order being changed.
                
                $('#CardType').change(_prepareOrder);
                $('#CardType').keyup(_prepareOrder);
                $('#UseDiscount').click(_prepareOrder);
            }

        };

    } ());

} (window.linkme = window.linkme || {}, jQuery));

