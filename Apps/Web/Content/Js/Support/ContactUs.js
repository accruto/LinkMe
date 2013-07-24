(function (linkme, $, undefined) {

    linkme.support = linkme.support || {};

    linkme.support.contactus = (function () {

        var _settings = {
            urls: {},
            userType: "Member",
            subcategoryId: null
        };

        var _initOverlay = function () {

            $(".overlay.contactus").initFields();
            $("#UserType").parent().dropdown();
            $("#MemberSubcategoryId").parent().dropdown();
            $("#EmployerSubcategoryId").parent().dropdown();

            // Show/hide user type specific fields.

            $("#UserType").change(function () {

                var $overlay = $(".overlay.contactus");

                if ($("#UserType option:selected").val() == "Member") {
                    $overlay.find(".formember_dropdown_field").show();
                    $overlay.find(".foremployer_dropdown_field, .phone_textbox_field").hide();
                    $overlay.find(".section.byphone").hide();
                }
                else {
                    $overlay.find(".formember_dropdown_field").hide();
                    $overlay.find(".foremployer_dropdown_field, .phone_textbox_field").show();
                    $overlay.find(".section.byphone").show();
                }
            });

            $("#UserType").closest(".control").find(".dropdown-item").click(function () {
                $("#UserType").change();
            });

            // Cancel.

            $(".overlay.contactus .button.cancel").click(function () {
                $(".overlay.contactus").dialog("close");
            });

            // Send.

            $(".overlay.contactus .button.send").click(function () {

                var userType = $('#UserType').val();

                var request = {
                    Name: $("#Name").val(),
                    From: $("#From").val(),
                    PhoneNumber: $("#PhoneNumber").val(),
                    UserType: userType,
                    SubcategoryId: userType == "Member" ? $("#MemberSubcategoryId").val() : $("#EmployerSubcategoryId").val(),
                    Message: $("#Message").val()
                };

                linkme.api.post(
                    _settings.urls.apiSendContactUs,
                    request,
                    function () {
                        linkme.fields.showValidationSuccess($(".overlay.contactus"), "Your enquiry has been sent to our support team who will respond shortly.");
                        return true;
                    },
                    function (errors) {
                        linkme.fields.showValidationErrors($(".overlay.contactus"), errors);
                        return true;
                    });
            });

        };

        return {

            init: function (options) {

                // Settings.

                $.extend(_settings, options);

                $.fn.contactUs = function () {

                    $(this).unbind("click").click(function () {

                        // If the overlay has not been retrieved yet do it now.

                        if ($(".overlay.contactus").length == 0) {
                            linkme.api.getSyncHtml(
                                _settings.urls.partialContactUs,
                                null,
                                function (html) {
                                    $("body").append(html);
                                    _initOverlay();
                                });
                        }

                        // Set the options.

                        var $overlay = $(".overlay.contactus");

                        if (_settings.userType) {

                            // UserType.

                            var text = $("#UserType option[value='" + _settings.userType + "']").text();
                            if (text)
                                $("#UserType").closest(".field").find(".dropdown-item:contains('" + text + "')").click();

                            // Subcategory.

                            var $subcategoryId = _settings.userType == "Employer" ? $("#EmployerSubcategoryId") : $("#MemberSubcategoryId");
                            text = $subcategoryId.find("option[value='" + _settings.subcategoryId + "']").text();
                            if (text)
                                $subcategoryId.closest(".field").find(".dropdown-item:contains('" + text + "')").click();
                            else
                                $subcategoryId.closest(".field").find(".dropdown-item").slice(0, 1).click();

                            // By phone.

                            if (_settings.userType == "Employer")
                                $overlay.find(".section.byphone").show();
                            else
                                $overlay.find(".section.byphone").hide();
                        }

                        linkme.fields.resetValidation($overlay);

                        // Show it.

                        $overlay.dialog({
                            modal: true,
                            width: 618,
                            closeOnEscape: false,
                            resizable: false,
                            dialogClass: "contactus-dialog"
                        });

                        return false;
                    });
                };
            },

            options: function (options) {
                $.extend(_settings, options);
            }

        };

    } ());

} (window.linkme = window.linkme || {}, jQuery));

