    (function($) {

        Array.prototype.indexOf = function(obj) {
            for (var i = 0; i < this.length; i++) {
                if (this[i] == obj) {
                    return i;
                }
            }
            return -1;
        }

        var actionsContext = {
            jobAds: null,
            folders: null
        };

        getIntersection = function(candidateIds1, candidateIds2) {

            var intersection = new Array();
            for (var index = 0; index < candidateIds2.length; index++) {
                if (candidateIds1.indexOf(candidateIds2[index]) != -1)
                    intersection.push(candidateIds2[index]);
            }

            return intersection;
        }

        function viewCandidates(context) {
            var select = $("<select name='candidateId' multiple='multiple'></select>");
            var queryString = "";
            if (context instanceof Array) {
                for (var index = 0; index < context.length; index++) {
                    if (index > 0) {
                        queryString = queryString + "&";
                    }
                    queryString = queryString + "candidateId=" + context[index];
                    select.append("<option value='" + context[index] + "' selected='selected'></option>");
                }
            }
            else {
                queryString = "candidateId=" + context;
                select.append("<option value='" + context + "' selected='selected'></option>");
            }

            var url = candidatesUrl + "?" + queryString;
            //window.location = url;
            var form = $("<form action='" + candidatesUrl + "' method='post'></form>");
            form.append(select).appendTo($("body")).submit();
        }

        function creditAction(action, actionItem, hideCreditReminder, hideBulkCreditReminder) {
            var canWithCredit = action.data("canWithCreditCandidateIds");
            var canWithoutCredit = action.data("canWithoutCreditCandidateIds");

            if (canWithCredit.length == 0) {
                if (canWithoutCredit.length != 0) {
                    performCreditsAction(actionItem, canWithoutCredit);
                }
            }
            else {
                showCreditsOverlay(actionItem, canWithCredit, canWithoutCredit, hideCreditReminder, hideBulkCreditReminder);
            }
        }

        function sendMessageAction(action, type) {
            var canWithCredit = action.data("canWithCreditCandidateIds");
            var canWithoutCredit = action.data("canWithoutCreditCandidateIds");

            checkCanSendMessage(canWithCredit.concat(canWithoutCredit), function() {
                showSendMessageOverlay(canWithCredit, canWithoutCredit, type);
            });
        }

        function sendRejectionMessageAction(action, type) {
            var rejected = action.data("rejectedCandidateIds");
            showSendRejectionMessageOverlay(rejected, type);
        }

        function updateCountText(menuItem, count) {
            if (count > 0) {
                menuItem.find(".js_count").text(count + " ");
                menuItem.find(".js_suffix").text(count == 1 ? "" : "s");
            }
            else {
                menuItem.find(".js_count").text("");
                menuItem.find(".js_suffix").text("s");
            }
        }

        function getActionIds(menuItem, candidateIds, candidateContext) {

            var actionIds = {};

            if (candidateContext.isAnonymous && !menuItem.hasClass("view-resume")) {
                actionIds.canWithoutCredit = new Array();
                actionIds.canWithCredit = new Array();
                actionIds.cannot = candidateIds;
            }
            else if (menuItem.hasClass("js_contact-by-phone-action")) {
                actionIds.canWithoutCredit = getIntersection(candidateContext.canContactByPhoneWithoutCredit, candidateIds);
                actionIds.canWithCredit = getIntersection(candidateContext.canContactByPhoneWithCredit, candidateIds);
                actionIds.cannot = getIntersection(candidateContext.cannotContactByPhone, candidateIds);
            }
            else if (menuItem.hasClass("js_contact-action")) {
                actionIds.canWithoutCredit = getIntersection(candidateContext.canContactWithoutCredit, candidateIds);
                actionIds.canWithCredit = getIntersection(candidateContext.canContactWithCredit, candidateIds);
                actionIds.cannot = getIntersection(candidateContext.cannotContact, candidateIds);
            }
            else if (menuItem.hasClass("js_access-resume-action")) {
                actionIds.canWithoutCredit = getIntersection(candidateContext.canAccessResumeWithoutCredit, candidateIds);
                actionIds.canWithCredit = getIntersection(candidateContext.canAccessResumeWithCredit, candidateIds);
                actionIds.cannot = getIntersection(candidateContext.cannotAccessResume, candidateIds);
            }
            else if (menuItem.hasClass("js_no-action")) {
                actionIds.canWithoutCredit = new Array();
                actionIds.canWithCredit = new Array();
                actionIds.cannot = new Array();
            }
            else {
                actionIds.canWithoutCredit = candidateIds;
                actionIds.canWithCredit = new Array();
                actionIds.cannot = new Array();
            }

            if (candidateContext.rejected == null) {
                actionIds.rejected = null;
            }
            else {
                actionIds.rejected = getIntersection(candidateContext.rejected, candidateIds);
            }

            return actionIds;
        }

        function updateMenuItem(menuItem, actionIds, candidateContext) {

            var action = null;

            // Update count text.

            if (menuItem.hasClass("bulk-menu-item")) {
                var id = menuItem.attr("id");
                if (id != "") {
                    action = menuItem;
                }

                updateCountText(menuItem, actionIds.canWithoutCredit.length + actionIds.canWithCredit.length);
            }
            else {
                var id = menuItem.attr("id");
                if (id != "") {
                    action = menuItem;
                }
            }

            // Decide which ones are shown and which are hidden.

            if (action != null) {
                action.data("canWithCreditCandidateIds", actionIds.canWithCredit);
                action.data("canWithoutCreditCandidateIds", actionIds.canWithoutCredit);
                action.data("candidateIds", actionIds.canWithCredit.concat(actionIds.canWithoutCredit));
                action.data("rejectedCandidateIds", actionIds.rejected);
            }

            if (menuItem.hasClass("js_credit-action")) {
                if (candidateContext.blockListId != null) {
                    menuItem.hide();
                    return false;
                }

                return actionIds.canWithCredit.length > 0 || actionIds.canWithoutCredit.length > 0;
            }
            else if (menuItem.hasClass("add-to-folder") || menuItem.hasClass("add-to-new-folder")) {

                if (candidateContext.isAnonymous)
                    return false;

                return actionIds.canWithCredit.length > 0 || actionIds.canWithoutCredit.length > 0;
            }
            else if (menuItem.hasClass("add-to-jobad")) {

                if (candidateContext.isAnonymous)
                    return false;

                return actionIds.canWithCredit.length > 0 || actionIds.canWithoutCredit.length > 0;
            }
            else if (menuItem.hasClass("remove-from-folder")) {

                if (candidateContext.folderId == null) {
                    menuItem.hide();
                    return false;
                }

                return actionIds.canWithCredit.length > 0 || actionIds.canWithoutCredit.length > 0;
            }
            else if (menuItem.hasClass("remove-from-flaglist")) {

                if (candidateContext.flagListId == null) {
                    menuItem.hide();
                    return false;
                }

                return actionIds.canWithCredit.length > 0 || actionIds.canWithoutCredit.length > 0;
            }
            else if (menuItem.hasClass("block-from-search") || menuItem.hasClass("child-block-from-current") || menuItem.hasClass("child-block-from-all")) {

                if (candidateContext.blockListId != null || !candidateContext.isSearch) {
                    menuItem.hide();
                    return false;
                }

                return actionIds.canWithCredit.length > 0 || actionIds.canWithoutCredit.length > 0;
            }
            else if (menuItem.hasClass("block-from-all")) {
                if (candidateContext.blockListId != null || candidateContext.isSearch) {
                    menuItem.hide();
                    return false;
                }

                return actionIds.canWithCredit.length > 0 || actionIds.canWithoutCredit.length > 0;
            }
            else if (menuItem.hasClass("remove-from-blocklist")) {

                if (candidateContext.blockListId == null) {
                    menuItem.hide();
                    return false;
                }

                return actionIds.canWithCredit.length > 0 || actionIds.canWithoutCredit.length > 0;
            }

            if (menuItem.hasClass("add-note")) {
                if (candidateContext.blockListId != null) {
                    menuItem.hide();
                    menuItem.next().hide();
                    return false;
                }

                if (candidateContext.isAnonymous)
                    return false;

                return actionIds.canWithCredit.length > 0 || actionIds.canWithoutCredit.length > 0;
            }

            if (menuItem.hasClass("send-rejection-message")) {
                if (actionIds.rejected == null) {
                    menuItem.hide();
                    return false;
                }

                return actionIds.rejected.length > 0;
            }

            return actionIds.canWithCredit.length > 0 || actionIds.canWithoutCredit.length > 0;
        }

        function updateActionMenuItem(menuItem, context, candidateContext) {

            // For the actions the context is the candidateId, set by the contextFunction.

            var candidateIds = new Array(context);
            var actionIds = getActionIds(menuItem, candidateIds, candidateContext);
            return updateMenuItem(menuItem, actionIds, candidateContext);
        }

        function updateBulkActions(candidateContext) {

            // The bulk actions are updated as items are selected so directly manipulate the menu states rather then relying on desktopMenu.

            $("div.bulk-menu-item").each(function() {
                var menuItem = $(this);
                var actionIds = getActionIds(menuItem, candidateContext.selectedCandidateIds, candidateContext);
                if (updateMenuItem(menuItem, actionIds, candidateContext)) {
                    menuItem.removeClass("menu-item-disabled");
                }
                else {
                    menuItem.addClass("menu-item-disabled");
                }
            });
        }

        shortenLongFolderLists = function(folderListType) {
            if (folderListType == "Private" || folderListType == "Mobile") {
                $(".add-to-folder").parent(".menu-panel-inner").prepend("<div class='large-private-folder-list'></div>");
                ($(".add-to-folder").parent(".menu-panel-inner").find("div.Shortlist")).each(function() {
                    $(this).appendTo($(this).closest(".menu-panel-inner").find("div.large-private-folder-list"));
                });
                ($(".add-to-folder").parent(".menu-panel-inner").find("div.Private")).each(function() {
                    $(this).appendTo($(this).closest(".menu-panel-inner").find("div.large-private-folder-list"));
                });
                //move the private folders title before large
                $(".menu-item-private-folder-header").each(function() {
                    $(this).parent().find(".large-private-folder-list").before($(this));
                });
            } else {
                $(".add-to-folder").parent(".menu-panel-inner").find(".divider:eq(0)").after("<div class='large-shared-folder-list'></div>");
                ($(".add-to-folder").parent(".menu-panel-inner").find("div.Shared")).each(function() {
                    $(this).appendTo($(this).closest(".menu-panel-inner").find("div.large-shared-folder-list"));
                });
                //move the shared folders title before large
                $(".menu-item-shared-folder-header").each(function() {
                    $(this).parent().find(".large-shared-folder-list").before($(this));
                });
            }
        }

        shortenLongJobAdsLists = function(jobAdsStatus) {
            $(".add-to-jobad").parent(".menu-panel-inner").find("div.menu-item-jobads-" + jobAdsStatus + ":eq(0)").before("<div class='large-jobads-" + jobAdsStatus + "-list'></div>");
            ($(".add-to-jobad").parent(".menu-panel-inner").find("div.menu-item-jobads-" + jobAdsStatus)).each(function() {
                $(this).appendTo($(this).closest(".menu-panel-inner").find("div.large-jobads-" + jobAdsStatus + "-list"));
            });
            $(".menu-item-jobads-" + jobAdsStatus + "-header").each(function() {
                $(this).parent().find(".large-jobads-" + jobAdsStatus + "-list").before($(this));
            });
        }

        initializeActions = function(candidateContext) {
            actionsContext.candidateContext = candidateContext;
            actionsContext.folders = null;
            actionsContext.jobAds = null;
            getFolders(initializeActionsFolders);
            getJobAds(initializeActionsJobAds);
        }

        initializeActionsFolders = function(folders) {
            actionsContext.folders = folders;
            if (actionsContext.jobAds != null)
                initializeActionsAll(actionsContext.folders, actionsContext.jobAds, actionsContext.candidateContext);
        }

        initializeActionsJobAds = function(jobAds) {
            actionsContext.jobAds = jobAds;
            if (actionsContext.folders != null)
                initializeActionsAll(actionsContext.folders, actionsContext.jobAds, actionsContext.candidateContext);
        }

        initializeActionsAll = function(folders, jobAds, candidateContext) {

            var largePrivateFolders = false;
            var largeSharedFolders = false;
            var largeJobAds = false;
            var jobAdsCount = {};

            // Populate the folders.

            if (folders != null) {

                var folderMenu = $("#hlAddToFolder .menu-panel-inner");
                var bulkFolderMenu = $("#hlBulkAddToFolder .menu-panel-inner");
                var privateFolders = 0;
                var sharedFolders = 0;

                var folderDivider = $("<div class='menu-item divider'><div class='desktop-menu-item-content'></div></div>");
                var bulkFolderDivider = $("<div class='menu-item divider'><div class='desktop-menu-item-content'></div></div>");
                folderMenu.append("<div class='menu-item-private-folder-header candidate-menu-item add-to-folder-disabled'><div class='icon'></div><div class='desktop-menu-item-content'>Private folders</div></div>")
				    .append(folderDivider)
				    .append("<div class='menu-item-shared-folder-header candidate-menu-item add-to-folder-disabled'><div class='icon'></div><div class='desktop-menu-item-content'>Organisation-wide folders</div></div>");

                bulkFolderMenu.append("<div class='menu-item-private-folder-header candidate-menu-item add-to-folder-disabled'><div class='icon'></div><div class='desktop-menu-item-content'>Private folders</div></div>")
				    .append(bulkFolderDivider)
				    .append("<div class='menu-item-shared-folder-header candidate-menu-item add-to-folder-disabled'><div class='icon'></div><div class='desktop-menu-item-content'>Organisation-wide folders</div></div>");

                for (var i = 0; i < folders.length; i++) {
                    if (folders[i].Type != "Flagged") {
                        var name = folders[i].Name;
                        if (folders[i].Type == "Shortlist" && name == null) {
                            name = "My shortlist";
                        }

                        if (folders[i].Type == "Mobile") {
                            name = "My mobile favourites";
                        }

                        if (folders[i].Type != "Shared") {
                            sharedFolders++;
                            folderDivider.before("<div id='hl" + folders[i].Id + "' class='" + folders[i].Type + " menu-item candidate-menu-item add-to-folder js_dropdown-ellipsis'><div class='icon'></div><div class='desktop-menu-item-content'>" + name + "</div></div>");
                            bulkFolderDivider.before("<div id='hlBulk" + folders[i].Id + "' class='" + folders[i].Type + " menu-item bulk-menu-item add-to-folder js_dropdown-ellipsis'><div class='icon'></div><div class='desktop-menu-item-content'>" + name + "</div></div>");
                        } else {
                            privateFolders++;
                            folderMenu.append("<div id='hl" + folders[i].Id + "' class='" + folders[i].Type + " menu-item candidate-menu-item add-to-folder js_dropdown-ellipsis'><div class='icon'></div><div class='desktop-menu-item-content'>" + name + "</div></div>");
                            bulkFolderMenu.append("<div id='hlBulk" + folders[i].Id + "' class='" + folders[i].Type + " menu-item bulk-menu-item add-to-folder js_dropdown-ellipsis'><div class='icon'></div><div class='desktop-menu-item-content'>" + name + "</div></div>");
                        }
                    }
                }

                if (privateFolders + sharedFolders > 0) {
                    folderMenu.append("<div class='menu-item divider'><div class='desktop-menu-item-content'></div></div>");
                    bulkFolderMenu.append("<div class='menu-item divider'><div class='desktop-menu-item-content'></div></div>");
                }

                folderMenu.append("<div id='hlAddToNewFolder' class='menu-item candidate-menu-item add-to-new-folder js_default-clicked-child' data-item-shorttext='new folder'><div class='icon'></div><div class='desktop-menu-item-content'>New folder</div></div>");
                bulkFolderMenu.append("<div id='hlBulkAddToNewFolder' class='menu-item bulk-menu-item add-to-new-folder js_default-clicked-child' data-item-shorttext='new folder'><div class='icon'></div><div class='desktop-menu-item-content'>New folder</div></div>");

                folderMenu.find(".js_dropdown-ellipsis").customEllipsis();
                bulkFolderMenu.find(".js_dropdown-ellipsis").customEllipsis();

                if (privateFolders > 5) {
                    largePrivateFolders = true;
                }
                if (sharedFolders > 5) {
                    largeSharedFolders = true;
                }
            }

            if (jobAds != null && jobAds.length > 0) {

                var jobAdMenu = $("#hlAddToJobAd .menu-panel-inner");
                var bulkJobAdMenu = $("#hlBulkAddToJobAd .menu-panel-inner");

                var status = jobAds[0].Status;

                var jobadsDivider = $("<div class='menu-item divider'><div class='desktop-menu-item-content'></div></div>");
                var bulkJobadsDivider = $("<div class='menu-item divider'><div class='desktop-menu-item-content'></div></div>");
                jobAdMenu.append("<div class='menu-item-jobads-Open-header candidate-menu-item add-to-jobad-disabled'><div class='icon'></div><div class='desktop-menu-item-content'>Open LinkMe job ads</div></div>")
    				.append(jobadsDivider)
	    			.append("<div class='menu-item-jobads-Closed-header candidate-menu-item add-to-jobad-disabled'><div class='icon'></div><div class='desktop-menu-item-content'>Closed LinkMe job ads</div></div>");

                bulkJobAdMenu.append("<div class='menu-item-jobads-Open-header candidate-menu-item add-to-jobad-disabled'><div class='icon'></div><div class='desktop-menu-item-content'>Open LinkMe job ads</div></div>")
		    		.append(bulkJobadsDivider)
			    	.append("<div class='menu-item-jobads-Closed-header candidate-menu-item add-to-jobad-disabled'><div class='icon'></div><div class='desktop-menu-item-content'>Closed LinkMe job ads</div></div>");

                for (var i = 0; i < jobAds.length; i++) {
                    var status = jobAds[i].Status;
                    var title = jobAds[i].Title;
                    if (status == "Open") {
                        jobadsDivider.before("<div id='hl" + jobAds[i].Id + "' class='menu-item-jobads-" + status + " menu-item candidate-menu-item add-to-jobad js_dropdown-ellipsis'><div class='icon'></div><div class='desktop-menu-item-content'>" + title + "</div></div>");
                        bulkJobadsDivider.before("<div id='hlBulk" + jobAds[i].Id + "' class='menu-item-jobads-" + status + " menu-item bulk-menu-item add-to-jobad js_dropdown-ellipsis'><div class='icon'></div><div class='desktop-menu-item-content'>" + title + "</div></div>");
                    } else {
                        jobAdMenu.append("<div id='hlBulk" + jobAds[i].Id + "' class='menu-item-jobads-" + status + " menu-item candidate-menu-item add-to-jobad js_dropdown-ellipsis'><div class='icon'></div><div class='desktop-menu-item-content'>" + title + "</div></div>");
                        bulkJobAdMenu.append("<div id='hlBulk" + jobAds[i].Id + "' class='menu-item-jobads-" + status + " menu-item bulk-menu-item add-to-jobad js_dropdown-ellipsis'><div class='icon'></div><div class='desktop-menu-item-content'>" + title + "</div></div>");
                    }

                    if (!jobAdsCount.hasOwnProperty(status)) jobAdsCount[status] = 0;
                    jobAdsCount[status]++;
                }

                jobAdMenu.find(".js_dropdown-ellipsis").customEllipsis();
                bulkJobAdMenu.find(".js_dropdown-ellipsis").customEllipsis();

                for (var status in jobAdsCount)
                    if (jobAdsCount[status] > 5)
                    shortenLongJobAdsLists(status);
            }
            else {
                var addToJobAd = $("#hlAddToJobAd");
                addToJobAd.addClass("js_no-action");
                addToJobAd.find(".menu-submenu-button").remove();
                addToJobAd.find(".desktop-menu-submenu").remove();

                var bulkAddToJobAd = $("#hlBulkAddToJobAd");
                bulkAddToJobAd.addClass("js_no-action");
                bulkAddToJobAd.find(".menu-submenu-button").remove();
                bulkAddToJobAd.find(".desktop-menu-submenu").remove();
            }

            // Set up action menus.

            // Bulk

            var searchResults = $("#search-results");

            bulkActionMenuParameters = {
                menuIdPrefix: "bulk-menu",
                absorbHyperlinks: true,
                containerElement: searchResults,
                rootElementClass: "desktop-menu search-results-action-menu"
            };
            bulkActionMenuTogglerParameters = {
                togglerClass: "bulk-menu-toggler",
                togglerHoverClass: "bulk-menu-toggler-hover",
                togglerDownClass: "bulk-menu-toggler-down",
                togglerDownHoverClass: "bulk-menu-toggler-down-hover"
            };

            var bulkActionMenu = $("#bulk-desktop-menu").desktopMenu(bulkActionMenuParameters);

            searchResults.find(".js_bulk-toggler").each(function() {
                $(this).menuToggler(bulkActionMenu, bulkActionMenuTogglerParameters);
            });

            // Candidate

            actionMenuParameters = {
                menuIdPrefix: "candidate-menu",
                absorbHyperlinks: true,
                contextFunction: function() { return this.parents(".search-result").attr("data-memberid") },
                updateMenuItemFunction: updateActionMenuItem,
                updateMenuItemContext: candidateContext,
                containerElement: searchResults,
                rootElementClass: "desktop-menu search-results-action-menu"
            };
            menuTogglerParameters = {
                togglerClass: "candidate-menu-toggler",
                togglerHoverClass: "candidate-menu-toggler-hover",
                togglerDownClass: "candidate-menu-toggler-down",
                togglerDownHoverClass: "candidate-menu-toggler-down-hover"
            };

            var candidateActionMenu = $("#candidate-desktop-menu").desktopMenu(actionMenuParameters);

            searchResults.find(".js_toggler").each(function() {
                $(this).menuToggler(candidateActionMenu, menuTogglerParameters);
            });

            // Adjust the menus.

            if (largePrivateFolders) {
                shortenLongFolderLists("Private");
            }
            if (largeSharedFolders) {
                shortenLongFolderLists("Shared");
            }

            // Set up responses to actions.

            $(".candidate-menu-item").each(function() {

                var id = $(this).attr("id");

                if (id == "hlViewResume") {
                    $(this).bind("action-invoked", function() {
                        viewCandidates($(this).data("candidateIds"));
                    });
                }

                else if (id == "hlViewPhoneNumber") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            creditAction($(this), "viewPhoneNumbers", candidateContext.hideCreditReminder, candidateContext.hideBulkCreditReminder);
                        });
                    }
                }

                else if (id == "hlSendMessage") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            sendMessageAction($(this), "individual");
                        });
                    }
                }

                else if (id == "hlSendRejection") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            sendRejectionMessageAction($(this), "individual");
                        });
                    }
                }

                else if (id == "hlDownloadResume") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            creditAction($(this), "downloadResumes", candidateContext.hideCreditReminder, candidateContext.hideBulkCreditReminder);
                        });
                    }
                }

                else if (id == "hlEmailResume") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            creditAction($(this), "emailResumes", candidateContext.hideCreditReminder, candidateContext.hideBulkCreditReminder);
                        });
                    }
                }

                else if ($(this).hasClass("add-to-new-folder")) {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            return showAddFolderOverlay(false, $(this).data("candidateIds"), function() { updateFolders(candidateContext); });
                        });
                    }
                }
                else if ($(this).hasClass("add-to-folder")) {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            var id = $(this).attr("id").slice(2);
                            addCandidatesToFolder(id, $(this).data("candidateIds"));
                        });
                    }
                }
                else if (id == "hlRemoveFromFolder") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            removeCandidatesFromFolder(candidateContext.folderId, $(this).data("candidateIds"));
                        });
                    }
                }
                else if (id == "hlRemoveFromFlagList") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            removeCandidatesFromFlagList($(this).data("candidateIds"));
                        });
                    }
                }
                else if (id == "hlRestoreFromBlockList") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            removeCandidatesFromBlockList(candidateContext.blockListId, $(this).data("candidateIds"));
                        });
                    }
                }
                else if ($(this).hasClass("add-to-jobad")) {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            var id = $(this).attr("id");
                            if (id != "hlAddToJobAd") {
                                shortlistCandidatesForJobAd(id.slice(2), $(this).data("candidateIds"));
                            }
                        });
                    }
                }
                else if (id == "hlAddNote") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            var candidateIds = $(this).data("candidateIds");
                            if (candidateIds.length == 1) {
                                var notes = $("#notes-" + candidateIds[0]);
                                notes.show();
                                notes.find(".add-notes_section").show();
                                notes.find(".add-notes_section").find("#org-wide").attr("checked", "checked");
                                notes.find(".add-notes_button").hide();
                                displayAllNotes(candidateIds[0]);
                            }
                        });
                    }
                }

                else if (id == "hlChildBlockFromCurrentSearch") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            var candidateIds = $(this).data("candidateIds");
                            temporarilyBlockCandidate(candidateIds[0]);
                        });
                    }
                }
                else if (id == "hlBlockFromAllSearches" || id == "hlChildBlockFromAllSearches") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            var candidateIds = $(this).data("candidateIds");
                            permanentlyBlockCandidate(candidateIds[0], candidateContext);
                        });
                    }
                }
            });

            $(".bulk-menu-item").each(function() {

                var id = $(this).attr("id");

                if (id == "hlBulkViewResume") {
                    $(this).bind("action-invoked", function() {
                        viewCandidates($(this).data("candidateIds"));
                    });
                }

                else if (id == "hlBulkViewPhoneNumber") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            creditAction($(this), "viewPhoneNumbers", candidateContext.hideCreditReminder, candidateContext.hideBulkCreditReminder);
                        });
                    }
                }

                else if (id == "hlBulkSendMessage") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            sendMessageAction($(this), "bulk");
                        });
                    }
                }

                else if (id == "hlBulkSendRejection") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            sendRejectionMessageAction($(this), "bulk");
                        });
                    }
                }

                else if (id == "hlBulkDownloadResume") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            creditAction($(this), "downloadResumes", candidateContext.hideCreditReminder, candidateContext.hideBulkCreditReminder);
                        });
                    }
                }

                else if (id == "hlBulkEmailResume") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            creditAction($(this), "emailResumes", candidateContext.hideCreditReminder, candidateContext.hideBulkCreditReminder);
                        });
                    }
                }

                else if ($(this).hasClass("add-to-new-folder")) {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            return showAddFolderOverlay(false, $(this).data("candidateIds"), function() { updateFolders(candidateContext); });
                        });
                    }
                }
                else if ($(this).hasClass("add-to-folder")) {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            var id = $(this).attr("id").slice(6);
                            addCandidatesToFolder(id, $(this).data("candidateIds"));
                        });
                    }
                }
                else if (id == "hlBulkRemoveFromFolder") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            removeCandidatesFromFolder(candidateContext.folderId, $(this).data("candidateIds"));
                        });
                    }
                }
                else if (id == "hlBulkRemoveFromFlagList") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            removeCandidatesFromFlagList($(this).data("candidateIds"));
                        });
                    }
                }
                else if (id == "hlBulkRestoreFromBlockList") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            removeCandidatesFromBlockList(candidateContext.blockListId, $(this).data("candidateIds"));
                        });
                    }
                }
                else if ($(this).hasClass("add-to-jobad")) {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            var id = $(this).attr("id");
                            if (id != "hlBulkAddToJobAd") {
                                shortlistCandidatesForJobAd(id.slice(6), $(this).data("candidateIds"));
                            }
                        });
                    }
                }
                else if (id == "hlBulkAddNotes") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            $(".bulk-notes_holder").show();
                            $(".bulk-notes_holder").find(".add-notes_section").find("#org-wide").attr("checked", "checked");
                        });
                    }
                }
                else if (id == "hlBulkChildBlockFromCurrentSearch") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            temporarilyBlockCandidates($(this).data("candidateIds"));
                        });
                    }
                }
                else if (id == "hlBulkBlockFromAllSearches" || id == "hlBulkChildBlockFromAllSearches") {
                    if (!candidateContext.isAnonymous) {
                        $(this).bind("action-invoked", function() {
                            permanentlyBlockCandidates($(this).data("candidateIds"), candidateContext);
                        });
                    }
                }
            });

            $(".js_selectable-item").initializeItems(candidateContext);
            $(".js_select-all-checkbox").initializeAllItems(candidateContext);

            if ($.browser.msie) {
                $(".icon").addClass("IE");
                if ($.browser.version.indexOf("7") == 0) {
                    $(".menu-item-jobads-Open-header .desktop-menu-item-content").addClass("IE7");
                    $(".menu-item-jobads-Closed-header .desktop-menu-item-content").addClass("IE7");
                    $("#hlChildBlockFromCurrentSearch .icon").addClass("IE7");
                    $("#hlChildBulkBlockFromCurrentSearch .icon").addClass("IE7");
                    $("#hlChildBlockFromAllSearches .icon").addClass("IE7");
                    $("#hlChildBulkBlockFromAllSearches .icon").addClass("IE7");
                    $("#hlBulkDownloadResume .icon").addClass("IE7");
                    $("#hlBulkEmailResume .icon").addClass("IE7");
                }
            }
            if ($.browser.mozilla) $(".icon").addClass("FireFox");
        }

        initializeResumeActions = function(candidateContext) {
            actionsContext.candidateContext = candidateContext;
            actionsContext.folders = null;
            actionsContext.jobAds = null;
            getFolders(initializeResumeActionsFolders);
            getJobAds(initializeResumeActionsJobAds);
        }

        initializeResumeActionsFolders = function(folders) {
            actionsContext.folders = folders;
            if (actionsContext.jobAds != null)
                initializeResumeActionsAll(actionsContext.folders, actionsContext.jobAds, actionsContext.candidateContext);
        }

        initializeResumeActionsJobAds = function(jobAds) {
            actionsContext.jobAds = jobAds;
            if (actionsContext.folders != null)
                initializeResumeActionsAll(actionsContext.folders, actionsContext.jobAds, actionsContext.candidateContext);
        }

        initializeResumeActionsAll = function(folders, jobAds, candidateContext) {

            var largePrivateFolders = false;
            var largeSharedFolders = false;
            var largeJobAds = false;
            var jobAdsCount = {};

            // Populate the folders.

            if (!(folders == null)) {
                var foldersList = $(".folders-dropdown-list");
                var privateFolders = 0;
                var sharedFolders = 0;

                for (var i = 0; i < folders.length; i++) {
                    if (folders[i].Type == "Shortlist" || folders[i].Type == "Private" || folders[i].Type == "Shared" || folders[i].Type == "Mobile") {
                        if (i > 2 && folders[i].Type != folders[i - 1].Type) {
                            foldersList.append("<div class='divider'></div>");
                        }

                        if (folders[i].Type == "Shared")
                            sharedFolders++;
                        else
                            privateFolders++;

                        var name = folders[i].Name;
                        if (folders[i].Type == "Shortlist" && name == null) {
                            name = "My shortlist";
                        }
                        if (folders[i].Type == "Mobile") {
                            name = "My mobile favourites";
                        }
                        foldersList.append("<div class='" + folders[i].Type + " list-item' data-item-shorttext='" + name + "'><a href='javascript:void(0)' class='add-to-folder js_dropdown-ellipsis' id='hl" + folders[i].Id + "'>" + name + "</a></div>");
                    }
                }

                foldersList.append("<div class='divider'></div>");
                foldersList.append("<div class='js_default-clicked-child hlAddToNewFolder' data-item-shorttext='a new folder'><a class='add-to-new-folder' href='javascript:void(0)' id='hlAddToNewFolder'>New folder</a></div>");
                foldersList.find(".js_dropdown-ellipsis").customEllipsis(18);

                if (privateFolders > 5) {
                    largePrivateFolders = true;
                }
                if (sharedFolders > 5) {
                    largeSharedFolders = true;
                }

                // Adjust the menus.

                if (largePrivateFolders) {
                    shortenLongFolderLists("Private");
                }
                if (largeSharedFolders) {
                    shortenLongFolderLists("Shared");
                }
            }

            // Populate the job ads.

            //SAB - 23/03/11 - add divider between open and closed ads and don't show a dropdown if there are no ads
            if (!(jobAds == null) && jobAds.length > 0) {

                var jobAdsList = $(".jobads-dropdown-list");
                var status = jobAds[0].Status;

                for (var i = 0; i < jobAds.length; i++) {
                    if (status != jobAds[i].Status) {
                        //add the divider
                        jobAdsList.append("<div class='menu-item divider'><div class='desktop-menu-item-content'></div></div>");
                    }
                    jobAdsList.append("<div class='list-item' data-item-shorttext='" + jobAds[i].Title + "'><a href='javascript:void(0)' class='add-to-jobad js_dropdown-ellipsis' id='hl" + jobAds[i].Id + "'>" + jobAds[i].Title + "</a></div>");

                    status = jobAds[i].Status;
                    if (!jobAdsCount.hasOwnProperty(status)) jobAdsCount[status] = 0;
                    jobAdsCount[status]++;
                }

                jobAdsList.find(".js_dropdown-ellipsis").customEllipsis(18);

                for (var status in jobAdsCount)
                    if (jobAdsCount[status] > 5)
                    shortenLongJobAdsLists(status);
            }
            else {
                $(".add-to-jobad").parent().replaceWith("<td><div class='add-to-jobad-disabled js_action-item action-item'>Add to <span class='dynamic-text'>a job ad</span></div></td>");
            }

            var actionItems = $(".js_action-items");

            if (!candidateContext.isAnonymous) {
                actionItems.find(".send-message").click(function() {
                    showSendMessageOverlay([], candidateContext.candidateIds, "individual");
                });
                actionItems.find(".send-message-locked").click(function() {
                    showSendMessageOverlay(candidateContext.candidateIds, [], "individual");
                });

                actionItems.find(".send-rejection-message").click(function() {
                    showSendRejectionMessageOverlay([], candidateContext.candidateIds, "individual");
                });

                actionItems.find(".view-phone-numbers").click(function() {
                    viewPhoneNumbers(candidateContext.candidateIds);
                });
                actionItems.find(".view-phone-numbers-locked").click(function() {
                    showCreditsOverlay("viewPhoneNumbers", candidateContext.candidateIds, [], candidateContext.hideCreditReminder, candidateContext.hideBulkCreditReminder);
                });

                actionItems.find(".download-resume").click(function() {
                    downloadResumes(candidateContext.candidateIds);
                });
                actionItems.find(".download-resume-locked").click(function() {
                    showCreditsOverlay("downloadResumes", candidateContext.candidateIds, [], candidateContext.hideCreditReminder, candidateContext.hideBulkCreditReminder);
                });

                actionItems.find(".email-resume").click(function() {
                    emailResumes(candidateContext.candidateIds);
                });
                actionItems.find(".email-resume-locked").click(function() {
                    showCreditsOverlay("emailResumes", candidateContext.candidateIds, [], candidateContext.hideCreditReminder, candidateContext.hideBulkCreditReminder);
                });

                actionItems.find("a.add-to-folder").click(function() {
                    var id = $(this).attr("id").slice(2);
                    addCandidatesToFolder(id, candidateContext.candidateIds);
                });

                actionItems.find("a.add-to-new-folder").click(function() {
                    showAddFolderOverlay(false, candidateContext.candidateIds, function() { updateFolders(candidateContext); });
                });

                actionItems.find("a.add-to-jobad").click(function() {
                    var id = $(this).attr("id").slice(2);
                    shortlistCandidatesForJobAd(id, candidateContext.candidateIds);
                });
            }

            /* Toggling dropdowns */
            $(".js_toggleDropdown").toggleDropdown(candidateContext);
        }

        $.fn.initializeItems = function(candidateContext) {

            // Work through current selections.

            $(this).find("input[type=checkbox]").each(function() {
                var id = $(this).attr("id");
                if (candidateContext.selectedCandidateIds.indexOf(id) != -1) {
                    $(this).attr("checked", "checked");
                    $(this).closest(".js_selectable-item").addClass("selected_candidate_search-result").addClass("selected_search-result");
                    $("#bulk-action").find(".bulk-select_holder").addClass("bulk-selected");
                    $(".selected_count").text(candidateContext.selectedCandidateIds.length + " ");
                    $(".selected_suffix").text(candidateContext.selectedCandidateIds.length == 1 ? "" : "s");
                }
                else {
                    $(this).removeAttr("checked");
                }
                if ($.browser.msie && ($.browser.version.indexOf("7") == 0 || $.browser.version.indexOf("8") == 0)) {
                    $(this).bind("click", checkboxOnChange);
                } else {
                    $(this).bind("change", checkboxOnChange);
                }
            });

            updateBulkActions(candidateContext);
            if (candidateContext.blockListId == null)
                $(".js_results-count").updateResultsCount(candidateContext);
            else
                $(".js_results-count").updateBlockListResultsCount(candidateContext);

            function checkboxOnChange() {
                var id = $(this).attr("id");
                if ($(this).is(":checked")) {
                    $(this).closest(".js_selectable-item").addClass("selected_candidate_search-result").addClass("selected_search-result");
                    if (candidateContext.selectedCandidateIds.indexOf(id) == -1) {
                        candidateContext.selectedCandidateIds.push(id);
                        $("#bulk-action").find(".bulk-select_holder").addClass("bulk-selected");
                        $(".selected_count").text(candidateContext.selectedCandidateIds.length + " ");
                        $(".selected_suffix").text(candidateContext.selectedCandidateIds.length == 1 ? "" : "s");
                    }
                }
                else {
                    $(this).closest(".js_selectable-item").removeClass("selected_candidate_search-result").removeClass("selected_search-result");
                    var index = candidateContext.selectedCandidateIds.indexOf(id);
                    if (index != -1) {
                        candidateContext.selectedCandidateIds.splice(index, 1);
                    }

                    if (candidateContext.selectedCandidateIds.length == 0) {
                        $("#bulk-action").find(".bulk-select_holder").removeClass("bulk-selected");
                        $(".selected_count").text("");
                        $(".selected_suffix").text("s");
                    }
                    else {
                        $(".selected_count").text(candidateContext.selectedCandidateIds.length + " ");
                        $(".selected_suffix").text(candidateContext.selectedCandidateIds.length == 1 ? "" : "s");
                    }
                }

                updateBulkActions(candidateContext);
                if (candidateContext.blockListId == null)
                    $(".js_results-count").updateResultsCount(candidateContext);
                else
                    $(".js_results-count").updateBlockListResultsCount(candidateContext);
            }
        }

        $.fn.initializeAllItems = function(candidateContext) {

            // For the moment deslect all.

            $(this).find("input[type=checkbox]").removeAttr("checked");

            $(this).find("input[type=checkbox]").click(function() {
                if ($(this).is(":checked")) {
                    $(this).closest(".search-results_ascx").find(".js_selectable-item").find("input[type=checkbox]").each(function() {
                        $(this).attr("checked", "checked");
                        var id = $(this).attr("id");
                        if (id != "" && candidateContext.selectedCandidateIds.indexOf(id) == -1) {
                            candidateContext.selectedCandidateIds.push(id);
                        }
                    });
                    $(this).closest(".search-results_ascx").find(".js_selectable-item").addClass("selected_candidate_search-result").addClass("selected_search-result");
                    $("#bulk-action").find(".bulk-select_holder").addClass("bulk-selected");
                    $(".selected_count").text(candidateContext.selectedCandidateIds.length + " ");
                    $(".selected_suffix").text(candidateContext.selectedCandidateIds.length == 1 ? "" : "s");
                }
                else {
                    $(this).closest(".search-results_ascx").find(".js_selectable-item").find("input[type=checkbox]").each(function() {
                        $(this).removeAttr("checked");
                        var id = $(this).attr("id");
                        if (candidateContext.selectedCandidateIds.indexOf(id) != -1) {
                            candidateContext.selectedCandidateIds.splice(candidateContext.selectedCandidateIds.indexOf(id), 1);
                        }
                    });
                    $(this).closest(".search-results_ascx").find(".js_selectable-item").removeClass("selected_candidate_search-result").removeClass("selected_search-result");
                    $("#bulk-action").find(".bulk-select_holder").removeClass("bulk-selected");
                    $(".selected_count").text("");
                    $(".selected_suffix").text("s");
                }

                updateBulkActions(candidateContext);
                if (candidateContext.blockListId == null)
                    $(".js_results-count").updateResultsCount(candidateContext);
                else
                    $(".js_results-count").updateBlockListResultsCount(candidateContext);
            });
        }

    })(jQuery);