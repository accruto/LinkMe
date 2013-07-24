/* ----------------------------
 * Desktop-style menu component
 * ---------------------------- 
 *
 * Dependencies:
 * - jQuery
 * - Align With 1.0.2
 *
 *
 * Default behaviour is as follows:
 * 
 * Main arguments:
 * 
 * - this: Provided by jQuery.
 *         An element containing <li>'s. Those <li>'s
 *         might have <ul><li> ... </ul> within them, i.e. a nested list.
 *         
 * - containerElement: The desktop menu is inserted after all children of
 *   this element.
 *
 *
 *
 * Example usage:
 * $("#ulActions").desktopMenu({
 *    menuIdPrefix:             "myMenu",
 *    absorbHyperlinks:         true,
 *    contextFunction: function() { return this.parents(".search-result").attr("data-memberid") },
 *    containerElement:         $("#search-results"),
 *    togglerClass:             "candidate-menu-toggler",
 *    togglerHoverClass:        "candidate-menu-toggler-hover",
 *    togglerDownClass:         "candidate-menu-toggler-down",
 *    togglerDownHoverClass:    "candidate-menu-toggler-down-hover"
 * });
 *
 *
 *
 * Output:
 * 
 * A container with a deep clone of the menuSourceRootElement element, where:
 * - All <ul> and <li>'s are replaced by <div>'s
 * - Mouse interaction with it behaves like a desktop application menu
 * - Nested lists become submenus
 * - Hyperlinks become normal text
 *      - The onClick handler of the first link in each <li> is triggered by the related <div>
 *      - A <li> with a hyperlink AND a <ul> inside becomes a submenu AND a triggerable menu item
 *
 * It is added after all children of containerElement.
 *
 *
 * 
 * 
 */
(function($) {
    if ($.LinkMeUI === undefined) {
        $.LinkMeUI = {};
    }

    $.getScript(LinkMeUI.ContentPath + "js/jquery/plugins/jquery.ba-outside-events.min.js");

    desktopMenuFunctions = {

        disableTextSelection: function(element) {
            element = $(element);
            if (element.length > 0) {
                var rawElement = element.get(0);
                if (typeof rawElement.onselectstart != "undefined") { // IE route
                    rawElement.onselectstart = function() { return false; };
                }
                element.mousedown(function() {
                    return false; // Other than IE
                });
            }
        },

        item_updateAppearance: function(p) {
            var menuItem = $(this);
            menuItem.toggleClass("menu-item-hover", menuItem.data("hover"));
            menuItem.toggleClass("menu-item-submenu", menuItem.data("has-submenu"));
            menuItem.toggleClass("menu-item-submenu-hover", menuItem.data("hover") && menuItem.data("has-submenu"));
        },

        item_toggleMenuUnderItem: function(p, menuItem, dontToggleAlwaysShow) {
            menuItem = $(menuItem);
            var containingPanel = menuItem.data("containingPanel");
            var openChildPanel = containingPanel.data("openChildPanel");
            var childPanel = menuItem.data("childPanel");
            var submenuButton = menuItem.data("submenuButton");
            var panelAlreadyOpen = openChildPanel == childPanel;

            // No toggle off allowed for already open panel
            if (panelAlreadyOpen && dontToggleAlwaysShow) {
                return;
            }

            // Close currently open panel and its children
            if (openChildPanel) {
                desktopMenuFunctions.item_closeMenuUnderItem(p, openChildPanel.data("parentItem"));
            }

            if (panelAlreadyOpen) {
                return;
            }

            // Maybe... maybe this menuItem has no submenu. Don't throw an error, just abort.
            if (!childPanel) {
                return;
            }

            var toggler = p.rootElement.data("openedByToggler");
            desktopMenuFunctions.enablePanelMenuItems(p, childPanel, p.contextFunction.call(toggler));

            containingPanel.data("openChildPanel", childPanel);
            if (submenuButton) {
                submenuButton.data("opened", true);
                desktopMenuFunctions.submenuButton_updateAppearance.call(submenuButton, p);
            }
            childPanel.show();

            // Quick hack to align the menu correctly on the first click.
            var whatToAlignWith = submenuButton ? submenuButton : menuItem;
            for (var i = 1; i <= 2; i++) {
                childPanel.alignWith(whatToAlignWith, "tltr", { appendTo: p.rootElement });
            }
        },

        item_closeMenuUnderItem: function(p, menuItem) {
            var menuPanelUnderItem = menuItem.data("childPanel");
            var containingPanel = menuItem.data("containingPanel");
            var submenuButton = menuItem.data("submenuButton");

            containingPanel.data("openChildPanel", null);
            if (submenuButton) {
                submenuButton.data("opened", false);
                desktopMenuFunctions.submenuButton_updateAppearance.call(submenuButton, p);
            }
            menuPanelUnderItem.hide();

            var openChildPanel = menuPanelUnderItem.data("openChildPanel");
            if (!openChildPanel) {
                return;
            }

            desktopMenuFunctions.item_closeMenuUnderItem(p, openChildPanel.data("parentItem"));
        },

        openMenu: function(p, openedByToggler) {
            var toggler = $(openedByToggler);
            p.rootElement.data("opened", true);
            p.rootElement.data("openedByToggler", toggler);
            toggler.data("down", true);
            desktopMenuFunctions.toggler_updateAppearance.call(toggler);
            desktopMenuFunctions.enablePanelMenuItems(p, p.rootPanel, p.contextFunction.call(toggler));
            p.rootPanel.css("visibility", "hidden");
            p.rootPanel.show();
            setTimeout(function() {
                if (p.rootPanelMustBeWiderThanToggler) {
                    var togglerOuterWidth = toggler.outerWidth();
                    var togglerInnerWidth = toggler.width();
                    var menuPanelOuterWidth = p.rootPanel.outerWidth();
                    var firstItem = $(".menu-item:first", p.rootPanel);
                    var IE = false;
					/*@cc_on
					IE = true;
					@*/
                    var itemInnerWidth;
                    // IE7 stuffs up the layout brutally... much simpler to not support
                    // this cosmetic nicety in IE at all.
                    //if (!IE) {
                        itemInnerWidth = firstItem.width();
                        $(".menu-item", p.rootPanel).css("min-width", itemInnerWidth - (menuPanelOuterWidth - togglerOuterWidth) + "px");
                    //}
                }
                p.rootPanel.css("visibility", "visible");
                p.rootElement.bind("mousedownoutside", function(event) {
                    desktopMenuFunctions.closeMenu(p);
                });
            }, 1);
        },

        closeMenu: function(p) {
            var toggler = p.rootElement.data("openedByToggler");
            p.rootElement.data("opened", false);
            toggler.data("down", false);
            p.rootPanel.hide();
            desktopMenuFunctions.toggler_updateAppearance.call(toggler);
            if (p.rootPanel.data("openChildPanel")) {
                desktopMenuFunctions.item_closeMenuUnderItem(p, p.rootPanel.data("openChildPanel").data("parentItem"));
            }
            p.rootElement.unbind("mousedownoutside");
        },

        enablePanelMenuItems: function(p, panel, context) {
            var innerPanel = panel.find(".menu-panel-inner");
            $(".menu-item", innerPanel).each(function() {
                var menuItem = $(this);
                var enableMenuItem = p.updateMenuItemFunction(menuItem, context, p.updateMenuItemContext);
                if (enableMenuItem != null) {
                    if (enableMenuItem) {
                        desktopMenuFunctions.enableMenuItem(p, menuItem);
                    }
                    else {
                        desktopMenuFunctions.disableMenuItem(p, menuItem);
                    }
                }
            });
        },

        enableMenuItem: function(p, menuItem) {
            menuItem.removeClass("menu-item-disabled");
        },

        disableMenuItem: function(p, menuItem) {
            menuItem.addClass("menu-item-disabled");
        },

        submenuButton_updateAppearance: function(p) {
            var submenuButton = $(this);
            submenuButton.toggleClass("menu-submenu-button-hover", submenuButton.data("hover"));
            submenuButton.toggleClass("menu-submenu-button-opened", submenuButton.data("opened"));
            submenuButton.toggleClass("menu-submenu-button-opened-hover", submenuButton.data("hover") && submenuButton.data("opened"));
        },

        submenuButton_onClick: function(event) {
            event.stopPropagation();
        },

        submenuButton_onMouseDown: function(event) {
            if (event.which != 1) {
                return;
            }

            var p = event.data;
            var containingItem = $(this).data("containingItem");
            desktopMenuFunctions.item_toggleMenuUnderItem(p, containingItem);

            if (p.mouseDelayTimer) {
                clearTimeout(p.mouseDelayTimer);
            }
        },

        itemWithSubmenu_onMouseDown: function(event) {
            if (event.which != 1) {
                return;
            }

            var p = event.data;
            desktopMenuFunctions.item_toggleMenuUnderItem(p, this);

            if (p.mouseDelayTimer) {
                clearTimeout(p.mouseDelayTimer);
            }
        },

        submenuButton_onMouseEnter: function(event) {
            var p = event.data;
            var submenuButton = $(this);
            var containingItem = submenuButton.data("containingItem");

            submenuButton.data("hover", true);
            desktopMenuFunctions.submenuButton_updateAppearance.call(this, event.data);

            if (p.mouseDelayTimer) {
                clearTimeout(p.mouseDelayTimer);
            }

            p.mouseDelayTimer = setTimeout(function() {
                desktopMenuFunctions.item_toggleMenuUnderItem(p, containingItem, true);
            }, p.onSubmenuButtonMouseDelay);
        },

        submenuButton_onMouseLeave: function(event) {
            var p = event.data;
            $(this).data("hover", false);
            desktopMenuFunctions.submenuButton_updateAppearance.call(this, p);

            if (p.mouseDelayTimer) {
                clearTimeout(p.mouseDelayTimer);
            }
        },

        item_onMouseEnter: function(event) {
            var p = event.data;
            var menuItem = $(this);
            menuItem.data("hover", !menuItem.hasClass("menu-item-disabled"));
            desktopMenuFunctions.item_updateAppearance.call(menuItem, p);

            submenuButton = menuItem.data("submenuButton");
            if (submenuButton && submenuButton.data("hover")) {
                return;
            }

            if (p.mouseDelayTimer) {
                clearTimeout(p.mouseDelayTimer);
            }

            p.mouseDelayTimer = setTimeout(function() {
                desktopMenuFunctions.item_toggleMenuUnderItem(p, menuItem, true);
            }, p.hasSubmenuButtonMouseDelay);
        },

        item_onMouseLeave: function(event) {
            var p = event.data;
            $(this).data("hover", false);
            desktopMenuFunctions.item_updateAppearance.call(this, p);

            if (p.mouseDelayTimer) {
                clearTimeout(p.mouseDelayTimer);
            }
        },

        // Copy menuItem's class/content/click handler into the parent
        item_absorbIntoParent: function(parentItem) {
            var menuItem = $(this);
            parentItem.unbind("click");
            parentItem.attr("class", menuItem.attr("class"));
            parentItem.click(function() {
                menuItem.click();
            });

            // If present, "shortText" data in the menuItem overrides content replacement.

            var content = menuItem.data("shortText");
            content = content ? content : menuItem.find(".desktop-menu-item-content:first").html();

            var container = parentItem.find("small:first");
            container = container.length != 0 ? container : parentItem.find(".desktop-menu-item-content:first");

            container.html(content);

            parentItem.removeClass("menu-item-hover");
            parentItem.removeClass("menu-item-submenu");
            parentItem.removeClass("menu-item-submenu-hover");
        },

        desktopMenu: function(parentItem, p) {

            var menuPanel = $(this).find("> .menu-panel");
            var menuItemContainer = menuPanel.find(".menu-panel-inner");

            if (parentItem) {
                menuPanel.data("parentItem", parentItem);
            }
            else {
                p.rootPanel = menuPanel;
            }

            // Iterate through direct menu items but not deeper.

            $(".menu-item", menuItemContainer).not($(".menu-item .menu-item", menuItemContainer)).each(function() {

                var menuItem = $(this);
                menuItem.data("shortText", menuItem.attr("data-item-shorttext"));
                menuItem.data("containingPanel", menuPanel);
                menuItem.data("has-submenu", false);

                menuItem.click(function() {
                    var menuItem = $(this);
                    if (!(menuItem.hasClass("menu-item-disabled"))) {
                        desktopMenuFunctions.closeMenu(p);
                        menuItem.trigger("action-invoked");
                    }
                });

                // Absorb right now if this item is the default one.

                if (menuItem.hasClass("js_default-clicked-child") && parentItem && parentItem.hasClass("js_absorb-clicked-child")) {
                    desktopMenuFunctions.item_absorbIntoParent.call(menuItem, parentItem);
                }

                // Basic events.

                menuItem.bind('mouseenter', p, desktopMenuFunctions.item_onMouseEnter);
                menuItem.bind('mouseleave', p, desktopMenuFunctions.item_onMouseLeave);

                // Recurse to sub menu.

                var submenu = menuItem.find(".desktop-menu-submenu");
                if (submenu.get(0)) {

                    var submenuButton = menuItem.find(".menu-submenu-button");
                    if (submenuButton.get(0)) {
                        submenuButton.data("containingItem", menuItem);
                        submenuButton.data("opened", false);

                        submenuButton.bind("click", p, desktopMenuFunctions.submenuButton_onClick);
                        submenuButton.bind("mousedown", p, desktopMenuFunctions.submenuButton_onMouseDown);
                        submenuButton.bind("mouseenter", p, desktopMenuFunctions.submenuButton_onMouseEnter);
                        submenuButton.bind("mouseleave", p, desktopMenuFunctions.submenuButton_onMouseLeave);

                        menuItem.data("submenuButton", submenuButton);
                    }

                    var submenuIndicator = menuItem.find(".menu-submenu-indicator");
                    if (submenuIndicator.get(0)) {
                        menuItem.bind("mousedown", p, desktopMenuFunctions.itemWithSubmenu_onMouseDown);
                    }

                    // Recurse.

                    menuItem.data("has-submenu", true);
                    menuItem.data("childPanel", desktopMenuFunctions.desktopMenu.call(submenu, menuItem, p));
                }
            });

            return menuPanel;
        },

        toggler_updateAppearance: function() {
            var toggler = $(this);
            var p = toggler.data("menuTogglerParameters");
            toggler.toggleClass(p.togglerHoverClass, toggler.data("hover"));
            toggler.toggleClass(p.togglerDownClass, toggler.data("down"));
            toggler.toggleClass(p.togglerDownHoverClass, toggler.data("hover") && toggler.data("down"));
        },

        makeMenuToggler: function(p) {
            var toggler = $(this);

            p.rootElement.data("opened", false);
            toggler.data("hover", false);
            toggler.data("down", false);
            toggler.addClass(p.togglerClass);

            toggler.mouseenter(function(event) {
                toggler.data("hover", true);
                desktopMenuFunctions.toggler_updateAppearance.call(toggler);
            });

            toggler.mouseleave(function(event) {
                toggler.data("hover", false);
                desktopMenuFunctions.toggler_updateAppearance.call(toggler);
            });

            $("a, input", toggler).each(function() {
                $(this).mousedown(function(event) {
                    event.stopPropagation();
                });
            });

            toggler.mousedown(function(event) {
                var toggler = $(this);

                // Left mouse-button only
                if (event.which != 1) {
                    return;
                }

                var openIt = !p.rootElement.data("opened");

                if (openIt) {
                    desktopMenuFunctions.openMenu(p, toggler);
                    // Quick hack to align the menu correctly on the first click.
                    for (var i = 1; i <= 2; i++) {
                        p.rootElement.alignWith(toggler, "tlbl", { appendTo: p.containerElement });
                    }
                }
                else {
                    // This would be handled by mousedownoutside, but toggler.mousedown returns
                    // false (to disable text-selection) and thus mousedownoutside doesn't receive any event.
                    desktopMenuFunctions.closeMenu(p);
                }
            });
        }
    };

    $.fn.desktopMenu = function(customArguments) {
        var p = {
            containerElement: document,
            subitemTextSelector: "small",
            onSubmenuButtonMouseDelay: 400,  // Submenu opening delay on submenu button hover
            hasSubmenuButtonMouseDelay: 400, // Submenu opening delay on item hover, when that item has a submenu button
            contextFunction: function() { return null; },
            updateMenuItemFunction: function() { return null; }
        };

        $.extend(p, customArguments);

        p.containerElement = $(p.containerElement);
        p.rootElement = $(this);
        p.rootElement.data("desktopMenuParameters", p);
        desktopMenuFunctions.disableTextSelection(p.rootElement);
        desktopMenuFunctions.desktopMenu.call(p.rootElement, null, p);

        return p.rootElement;
    };

    $.fn.menuToggler = function(menuRootElement, customArguments) {
        var p = {
            togglerClass: "toggler",
            togglerHoverClass: "toggler-hover",
            togglerDownClass: "toggler-down",
            togglerDownHoverClass: "toggler-down-hover",
            rootPanelMustBeWiderThanToggler: true	// Not supported for IE (too complicated to get right, at this stage)
        };

        $(this).data("menuTogglerParameters", p);

        $.extend(p,
			customArguments,
			{ rootElement: menuRootElement },
			menuRootElement.data("desktopMenuParameters")
		);

        $(this).each(function() {
            desktopMenuFunctions.makeMenuToggler.call(this, p);

            // Leave reference to  parameters from this toggler element
            $(this).data("menuTogglerParameters", p);
        });
    };
})(jQuery);