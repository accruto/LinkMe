(function($) {
    makeTabsFunctions = {
        tabs_updateAppearance: function(p) {
            var tabsContainer = $(this);

            $(p.tabSelector, tabsContainer).each(function() {
                var tab = $(this);
                var isSelected = tab.data("selected");
                $(this).toggleClass(p.selectedClass, isSelected);
                var tabContent = tab.find(p.tabContentSelector + ".ui-tabcontent");
                if (tabContent)
                    tabContent.html(isSelected ? tab.data("TabLongText") : tab.data("TabShortText"));
            });
        },

        tabs_selectTab: function(p, target) {
            var tabsContainer = $(this);
            target = $(target);

            // "selected" flag = is this tab now selected
            $(p.tabSelector, tabsContainer).each(function() {
                $(this).data("selected", $(this).get(0) == target.get(0));
            });

            makeTabsFunctions.tabs_updateAppearance.call(tabsContainer, p);
        }
    };

    $.fn.makeTabs = function(customArguments) {
        var p = {
            absorbHyperlinks: true,
            hyperlinkReplacementTag: "span",
            hyperlinkOrReplacementClassesToAdd: null,
            shortTextAttribute: "data-tab-shorttext",
            tabSelector: "li",
            tabContentSelector: "span",
            tabContentClassesToAdd: null,
            defaultSelectedFlagClass: "js_default-selected-tab",
            selectedClass: "tabs-selected"
        }
        $.extend(p, customArguments);

        var tabsContainer = $(this);

        $(p.tabSelector, tabsContainer).each(function() {
            var tab = $(this);
            var tabContent = tab.find(p.tabContentSelector);

            tabContent.addClass("ui-tabcontent");
            tabContent.addClass(p.tabContentClassesToAdd);

            // Store 
            if (tabContent) {
                tab.data("TabLongText", tabContent.html());
                tab.data("TabShortText", tabContent.attr(p.shortTextAttribute));
            }
        });

        /* 'Promote' hyperlinks to plaintext */
        if (p.absorbHyperlinks) {
            $(p.tabSelector, tabsContainer).each(function() {
                var tab = $(this);
                var firstTabHyperlink = $("a:first", tab);

                // Absorb class(es)
                if (firstTabHyperlink.attr("class"))
                    tab.addClass(firstTabHyperlink.attr("class"));

                // Absorb onClick
                tab.click(function(event) {
                    firstTabHyperlink.trigger("click");
                });

                // "Flatten" all links into plain-text
                $("a", tab).each(function() {
                    var tabHyperlink = $(this);

                    // Intended to mean "replace link with plaintext", but jQuery.replaceWith() requires
                    // a wrapping element for the replacement.
                    if (p.hyperlinkReplacementTag) {
                        var replacement = $(document.createElement(p.hyperlinkReplacementTag));
                        if (p.hyperlinkOrReplacementClassesToAdd)
                            replacement.addClass(p.hyperlinkOrReplacementClassesToAdd);
                        replacement.html(tabHyperlink.html());
                        tabHyperlink.after(replacement);
                    } else
                        tabHyperlink.after(tabHyperlink.html());
                    tabHyperlink.hide();
                    tabsContainer.after(tabHyperlink); // "spit out" links to stop tab.click being triggered by $("a").click ... :-S
                });
            });
        } else {
            $(p.tabSelector + " a", tabsContainer).addClass(p.hyperlinkOrReplacementClassesToAdd);
        }

        /* onClick: Set CSS classes to indicate which tab is selected */
        tabsContainer.find(p.tabSelector).click(function() {
            makeTabsFunctions.tabs_selectTab.call(tabsContainer, p, $(this));
        });

        /* Apply to selected item on page-load */
        makeTabsFunctions.tabs_selectTab.call(tabsContainer, p, $("." + p.defaultSelectedFlagClass + ":first", tabsContainer));
    }
})(jQuery);