(function (linkme, $, undefined) {

    linkme.support = linkme.support || {};

    linkme.support.faqs = (function () {

        var _settings = {
            urls: {}
        };

        var _getCategoryCssClass = function (categoryName) {
            return categoryName.toLowerCase().replace(" ", "-");
        };

        var _getPartialFaqUrl = function (id, keywords) {
            var params = { id: id };
            if (keywords)
                params.keywords = keywords;
            return linkme.api.getUrl(_settings.urls.partialFaqUrl, params);
        };

        var _getPartialSubCategoryUrl = function (id) {
            return linkme.api.getUrl(_settings.urls.partialSubcategoryUrl, { subcategoryId: id });
        };

        var _getPartialSearchUrl = function (categoryId, keywords) {
            return linkme.api.getUrl(_settings.urls.partialSearchUrl, { categoryId: categoryId, keywords: keywords });
        };

        var _updateBreadCrumbs = function (criteria) {

            // Hide them all to start with.

            var $breadcrumbs = $(".breadcrumbs");
            $breadcrumbs.find(".keywords, .subcategory, .faqitem").hide();

            // Populate based on what is supplied.

            if (criteria.keywords) {
                $breadcrumbs.find(".keywords").show();
                $breadcrumbs.find(".keywords .red").text(criteria.keywords);
            }
            else if (criteria.subcategory && criteria.subcategory.name) {
                $breadcrumbs.find(".subcategory").show().text(criteria.subcategory.name);
            }

            if (criteria.faq && criteria.faq.title) {
                $breadcrumbs.find(".faqitem").show().text(criteria.faq.title);
            }
        };

        var _updateHash = function (criteria) {

            var hash = "#";

            if (criteria.faq && criteria.faq.id) {

                // Faq.

                hash += "faqId=" + criteria.faq.id;
                if (criteria.keywords)
                    hash += "&keywords=" + encodeURIComponent(criteria.keywords);
                else
                    hash += "&subcategoryId=" + criteria.subcategory.id;
            }
            else if (criteria.keywords) {

                // Search.

                hash += "keywords=" + encodeURIComponent(criteria.keywords)
                    + "&categoryId=" + criteria.category.id;
            }
            else {

                // Subcategory.

                hash += "subcategoryId=" + criteria.subcategory.id;
            }

            window.location.hash = hash;
        };

        var _updateFaqResults = function (criteria) {

            var $faq = $("#results .faq");

            // Scroll to the top.

            $('html, body').animate({ scrollTop: 0 }, 'slow');

            // Update the height of the title.

            if ($faq.find(".titlebar .title").height() > 20)
                $faq.find(".titlebar").addClass("twolines");
            else
                $faq.find(".titlebar").removeClass("twolines");

            // Back.

            $faq.find(".back").click(function () {
                if (criteria.keywords) {
                    _getSearchResults({
                        category: {
                            id: criteria.category.id
                        },
                        keywords: criteria.keywords
                    });
                }
                else {
                    _getSubcategoryResults({
                        subcategory: {
                            id: criteria.subcategory.id,
                            name: criteria.subcategory.name
                        }
                    });
                }

                return false;
            });

            // Helpful.

            $faq.find(".button.yes").click(function () {
                linkme.api.post(_settings.urls.apiMarkHelpful, { id: criteria.faq.id });
                $faq.find(".helpful").toggle();
                $faq.find(".answer").toggle();
                return false;
            });

            // Not helpful.

            $faq.find(".button.no").click(function () {
                linkme.api.post(_settings.urls.apiMarkNotHelpful, { id: criteria.faq.id });
                $faq.find(".helpful").toggle();
                $faq.find(".answer").toggle();
                return false;
            });

            // Contact us link.

            $(".link.contactus", $faq).contactUs();

            // Update breadcrumbs.

            _updateBreadCrumbs(criteria);
        };

        var _getFaqResults = function (criteria) {

            linkme.api.getHtml(
                _getPartialFaqUrl(criteria.faq.id, criteria.keywords),
                null,
                function (html) {
                    $("#results .bg").html(html);
                    _updateFaqResults(criteria);
                    _updateHash(criteria);
                });
        };

        var _updateSubcategoryResults = function (criteria) {

            // Scroll to the top.

            $('html, body').animate({ scrollTop: 0 }, 'slow');

            // Clicking an item updates the results.

            $("#results .subcategory-faqlist .faqitem").click(function () {

                var $this = $(this);
                _getFaqResults({
                    subcategory: {
                        id: criteria.subcategory.id,
                        name: criteria.subcategory.name
                    },
                    faq: {
                        id: $this.attr("faqid"),
                        title: $this.text()
                    }
                });

                return false;
            });

            // Update the heights of each item.

            $(".subcategory-faqlist .faqitem").each(function () {
                if ($(this).height() > 20)
                    $(this).css({ "height": "40px", "padding-top": "12px" });
                else
                    $(this).css({ "height": "33px", "padding-top": "19px" });
            });

            // Update the breadcrumbs.

            _updateBreadCrumbs(criteria);
        };

        var _getSubcategoryResults = function (criteria) {

            linkme.api.getHtml(
                _getPartialSubCategoryUrl(criteria.subcategory.id),
                null,
                function (html) {
                    $("#results .bg").html(html);
                    _updateSubcategoryResults(criteria);
                    _updateHash(criteria);
                });
        };

        var _updateSearchResults = function (criteria) {

            // Scroll to the top.

            $('html, body').animate({ scrollTop: 0 }, 'slow');

            // Clicking an item updates the results.

            $("#results .search-faqlist .faqitem").click(function () {

                var $this = $(this);
                _getFaqResults({
                    category: {
                        id: criteria.category.id
                    },
                    keywords: criteria.keywords,
                    faq: {
                        id: $this.attr("faqid"),
                        title: $this.find(".title").text()
                    }
                });

                return false;
            });

            // Update the heights of each item.

            $("#results .search-faqlist .faqitem").each(function () {
                if ($(this).find(".title").height() > 20) {
                    $(this).find(".title").css({ "margin-top": "10px", "margin-bottom": "0px" });
                }
                else {
                    $(this).find(".title").css({ "margin-top": "18px", "margin-bottom": "8px" });
                }
            });

            // Update the breadcrumbs.

            _updateBreadCrumbs(criteria);
        };

        var _getSearchResults = function (criteria) {

            linkme.api.getHtml(
                _getPartialSearchUrl(criteria.category.id, criteria.keywords),
                null,
                function (html) {
                    $("#results .bg").html(html);
                    _updateSearchResults(criteria);
                    _updateHash(criteria);
                });
        };

        var _initResults = function (criteria) {

            // Update the results depending upon what is in the results.

            if (criteria.faq && criteria.faq.title) {
                _updateFaqResults(criteria);
            }
            else if (criteria.keywords) {
                _updateSearchResults(criteria);
            }
            else {
                _updateSubcategoryResults(criteria);
            }

        };

        var _initLeftBar = function () {

            $("#leftbar .subcategory").click(function () {

                // Update the left bar.

                var $this = $(this);
                $this.parent().find(".subcategory").removeClass("current");
                $this.addClass("current");

                // Remove any keywords.

                $("#Keywords").val("").blur();

                // Update contact us.

                var id = $this.attr("subcategoryid");
                linkme.support.contactus.options({
                    subcategoryId: id
                });

                // Clicking a sub category updates the results.

                _getSubcategoryResults({
                    subcategory: {
                        id: id,
                        name: $this.text()
                    }
                });

                return false;
            });

        };

        var _search = function (categoryId, keywords) {

            // Update the left bar.

            $("#leftbar .subcategory.current").removeClass("current");
            linkme.support.contactus.options({
                subcategoryId: null
            });

            // Update the results.

            _getSearchResults({
                category: {
                    id: categoryId
                },
                keywords: keywords
            });
        };

        var _initSearchBar = function () {

            $("#searchbar").initFields();

            // Create the CategoryId dropdown.

            var $categoryId = $("#CategoryId");

            $categoryId.find("option[value='']").attr("disabled", "disabled");
            $categoryId.parent().dropdown();

            // Add icons.

            $categoryId.parent().prepend("<div class='icon " + _getCategoryCssClass($categoryId.find("option:selected").text()) + "'></div>");

            var items = $categoryId.parent().parent().find(".dropdown-item:not(.disabled)");
            items.prepend("<div class='icon'></div>").each(function () {
                $(this).addClass(_getCategoryCssClass($(this).text()));
            });

            $categoryId.change(function () {

                // Change the icon.

                var categoryName = $(this).find("option:selected").text();
                var cssClass = _getCategoryCssClass(categoryName);
                $(this).parent().find(".icon").attr("class", "icon " + cssClass);

                // Update the left bar.

                $("#leftbar .subcategories").hide();
                $("#leftbar .subcategories." + cssClass).show();

                // Update contact us.

                linkme.support.contactus.options({
                    userType: categoryName == "Employers" ? "Employer" : "Candidate"
                });

                // Select the first sub-category.

                $("#leftbar .subcategories." + cssClass + " .subcategory").slice(0, 1).removeClass("current").click();
            });

            items.click(function () {
                $categoryId.change();
                return false;
            });

            // Keywords.

            $("#Keywords").before("<div class='icon'></div>");

            // Respond to changes in keywords by doing a search.

            $("#Keywords").keyup(function () {

                var $this = $(this);
                var keywords = $this.val();
                if (keywords.length < 2)
                    return;

                _search($("#CategoryId").val(), keywords);
            });

            $("#searchbar .button.search").click(function () {
                var keywords = $("#Keywords").realVal();
                if (keywords) {
                    _search($("#CategoryId").val(), keywords);
                }

                return false;
            });
        };

        return {

            init: function (options) {

                // Settings.

                $.extend(_settings, options);

                // Initialize everything.

                _initResults(options.criteria);
                _initLeftBar();
                _initSearchBar();

                $(".link.contactus").contactUs();
            },

            getHashUrl: function (hash) {
                
                var parameters = linkme.api.parseQueryString(hash);
                if (parameters.faqId || parameters.keywords || parameters.subcategoryId) {
                    return linkme.api.getUrl(_settings.urls.hashUrl, parameters);
                }

                return null;
            }

        };

    } ());

} (window.linkme = window.linkme || {}, jQuery));

(function ($) {

    $(window).load(function () {
        linkme.api.checkHash(window.location, linkme.support.faqs.getHashUrl);
    });

})(jQuery);