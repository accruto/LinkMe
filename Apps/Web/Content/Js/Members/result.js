(function ($) {
    var currentRequest = null;
    var jobTypes = { FullTime: 1, PartTime: 2, Contract: 4, Temp: 8, JobShare: 16 };

    $(document).ready(function () {
        //get left side partial
        if ($(".leftside:empty").length == 0) initLeftSideEvents();
        else {
            currentRequest = $.ajax({
                type: "GET",
                url: $(".leftside").attr("url").unMungeUrl(),
                success: function (data, textStatus, xmlHttpRequest) {
                    if (data == "") {
                    } else if (data.Success) {
                    } else {
                        $(".leftside").append(data);
                        initLeftSideEvents();
                    }
                    currentRequest = null;
                },
                dataType: "html"
            });
        }
        initRightSideEvents();
        updateCriteria($(".rightside .results .criteriahtml").html());
        initOverlays();
        updateJobAdItemData();
        //if empty result
        updateEmptyResult();
        var margin = ($(".pagination-container").width() - $(".pagination-holder").width()) / 2;
        $(".pagination-holder").css("margin-left", margin + "px");
        //josn for IE7
        if ($.browser.msie && $.browser.version.indexOf("7") == 0) {
            var json = document.createElement('script'); json.type = 'text/javascript'; json.async = true;
            json.src = LinkMeUI.Locations.JSON;
            (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(json);
        }
        //all notes
        loadAllNotes();
    });

    initOverlays = function () {
        $(".overlay.apierror").find(".tryagain").click(function () {
            $(this).closest(".overlay.apierror").dialog("close");
            partialCall();
        }).end().find(".close").click(function () {
            $(this).closest(".overlay.apierror").dialog("close");
        });
        $(".overlay.tips").find(".icon.close").click(function () {
            $(this).closest(".overlay.tips").dialog("close");
        })
        $(".overlay.login").find(".button.cancel").click(function () {
            $(this).closest(".overlay.login").dialog("close");
        });
        $(".overlay.renamefolder").find(".button.cancel, .icon.close").click(function () {
            $(this).closest(".overlay.renamefolder").dialog("close");
        }).end().find(".button.renamefolder").click(function () {
            var dialog = $(this).closest(".overlay");
            var input = dialog.find("#NewFolderName");
            var folderId = dialog.attr("folderid");
            if (!input.val() || input.val() == "") {
                dialog.find(".prompt").show().find(".text").text("Please input a new folder name.");
                input.focus();
            } else {
                var newName = $('<div/>').text(input.val()).html();
                currentRequest = $.post(apiRenameFolderUrl.replace(/00000000-0000-0000-0000-000000000000/gi, folderId),
					{ name: newName },
					function (data, textStatus, xmlHttpRequest) {
					    $(".leftside .area.favourite #" + folderId + " .title").html(newName);
					    $(".rightside .resultheader .title").html(newName);
					    dialog.dialog("close");
					    currentRequest = null;
					}
				).error(function (response) {
				    var data = $.parseJSON(response.responseText);
				    dialog.find(".prompt").show().find(".text").text(data.Errors[0].Message);
				});
            }
        });
        $(".overlay.emptyfolder").find(".button.cancel, .icon.close").click(function () {
            $(this).closest(".overlay.emptyfolder").dialog("close");
        }).end().find(".button.emptyfolder").click(function () {
            var dialog = $(this).closest(".overlay");
            var folderId = dialog.attr("folderid");
            var isFlaggedFolder = $(".leftside .area.favourite #" + folderId).hasClass("flagged");
            var url;
            if (isFlaggedFolder)
                url = apiUnflagAllJobAdsUrl;
            else url = apiEmptyFolderUrl.replace(/00000000-0000-0000-0000-000000000000/gi, folderId);
            currentRequest = $.post(url,
				function (data, textStatus, xmlHttpRequest) {
				    $(".leftside .area.favourite #" + folderId + " .count").text("(0)");
				    dialog.dialog("close");
				    currentRequest = null;
				}
			);
        });
        $(".overlay.savesearch").find(".button.cancel, .icon.close").click(function () {
            $(this).closest(".overlay.savesearch").dialog("close");
        }).end().find(".button.savesearch").click(function () {
            var dialog = $(this).closest(".overlay");
            var requestData = {};
            requestData["name"] = dialog.find("#SearchName").val();
            requestData["createAlert"] = dialog.find("#Email:checked").length > 0;
            currentRequest = $.post(apiCreateSearchFromCurrentUrl,
				requestData,
				function (data, textStatus, xmlHttpRequest) {
				    dialog.dialog("close");
				    currentRequest = null;
				}
			);
        });
        $(".overlay.emailalert").find(".button.cancel, .icon.close").click(function () {
            $(this).closest(".overlay.emailalert").dialog("close");
        }).end().find(".button.createemailalert").click(function () {
            var dialog = $(this).closest(".overlay");
            var requestData = {};
            requestData["name"] = dialog.find("#SearchName").val();
            requestData["createAlert"] = true;
            currentRequest = $.post(apiCreateSearchFromCurrentUrl,
				requestData,
				function (data, textStatus, xmlHttpRequest) {
				    dialog.dialog("close");
				    currentRequest = null;
				}
			);
        });
    }

    getSortText = function () {
        var sortText = "";
        var sortOrder = $("#SortOrder option:selected").attr("value");
        if (sortOrder == "CreatedTime") {
            if ($(".rightside .sort .descending").hasClass("active")) sortText = "Most recent to oldest";
            else sortText = "Oldest to most recent";
        } else if (sortOrder == "Relevance") {
            if ($(".sort .descending").hasClass("active")) sortText = "Most to least";
            else sortText = "Least to most";
        } else if (sortOrder == "JobType") {
            if ($(".sort .descending").hasClass("active")) sortText = "Full time at top";
            else sortText = "Full time at bottom";
        } else if (sortOrder == "Distance") {
            if ($(".sort .descending").hasClass("active")) sortText = "Nearest to furthest";
            else sortText = "Furthest to nearest";
        } else if (sortOrder == "Salary") {
            if ($(".sort .descending").hasClass("active")) sortText = "Highest to lowest";
            else sortText = "Lowest to highest";
        } else if (sortOrder == "Flagged") {
            if ($(".sort .descending").hasClass("active")) sortText = "Flagged at top";
            else sortText = "Flagged at bottom";
        }
        return sortText;
    }

    initRightSideEvents = function () {
        //rename folder
        $(".rightside.Folder .resultheader .icon.rename").click(function () {
            var header = $(this).closest(".resultheader");
            $(".overlay.renamefolder").dialog({
                modal: true,
                width: 360,
                height: 120,
                closeOnEscape: false,
                resizable: false,
                dialogClass: "renamefolder-dialog"
            }).attr("folderid", header.find(".icon.rename").attr("folderid")).find(".titlebar .title").text("Rename '" + header.find(".title").text() + "'").end().find(".prompt").hide();
        });
        //add 10 jobs to folder
        $(".rightside:not(.notloggedin) .resultscount .addtofolder").hover(function () {
            $(this).addClass("hover");
        }, function () {
            if ($(".menupan.folder:visible").length > 0) return;
            $(this).removeClass("hover");
        }).click(function () {
            $(".menupan").hide();
            var position = $(this).position();
            $(".menupan.folder").toggle().css({
                left: position.left - 6,
                top: position.top + 30 - 9
            }).attr("action", "addall");
        });
        $(".rightside .resultscount .restoreall").click(function () {
            var jobAdIds = new Array();
            $(".rightside .results .row:not(.empty)").each(function () {
                jobAdIds.push($(this).attr("id"));
                $(this).addClass("empty");
            });
            hide("unhide", jobAdIds);
            criteria["Page"] = "1";
            criteria["Items"] = "10";
            partialCall();
        });
        //clear all flags
        $(".rightside .resultscount .clearflags .action").click(function () {
            flag("current", null);
        });
        //sort order
        $(".rightside .sort .sorttext").text(getSortText());
        $(".rightside .sort #SortOrder").change(function () {
            $(".rightside .sort .sorttext").text(getSortText());
            criteria["SortOrder"] = $(this).find("option:selected").val();
            criteria["SortOrderDirection"] = $(".rightside .sort .descending.active").length > 0 ? "SortOrderIsDescending" : "SortOrderIsAscending";
            criteria["Page"] = "1";
            criteria["Items"] = $(".rightside .pg #ItemsPerPage option:selected").val();
            partialCall();
        });
        $(".rightside .sort .ascending, .rightside .sort .descending").click(function () {
            $(".rightside .sort .ascending, .rightside .sort .descending").toggleClass("active");
            $(".rightside .sort .sorttext").text(getSortText());
            criteria["SortOrder"] = $(".rightside .sort #SortOrder option:selected").val();
            criteria["SortOrderDirection"] = $(".rightside .sort .descending.active").length > 0 ? "SortOrderIsDescending" : "SortOrderIsAscending";
            criteria["Page"] = "1";
            criteria["Items"] = $(".rightside .pg #ItemsPerPage option:selected").val();
            partialCall();
        });
        //act on selected jobs
        $(".rightside .titlebar .bulktick .checkbox").click(function () {
            $(this).toggleClass("checked");
            if ($(this).hasClass("checked")) {
                var rowCount = $(".rightside .results .row:not(.empty)").length;
                $(".rightside .titlebar .bulkaction").addClass("selected").find(".dropdown .text .count").text(rowCount).end().find(".dropdown .text .plural").text("s");
                $(".rightside .results .row:not(.empty)").addClass("selected").find(".column.tick .checkbox").addClass("checked");
                $(".menupan.bulk").find(".menuitem").removeClass("zero").find(".text .count").text(rowCount).end().find(".text .plural").text("s").end().find(".text .type").text("ZIP");
            } else {
                $(".rightside .titlebar .bulkaction").removeClass("selected").find(".dropdown .text .count").text("").end().find(".dropdown .text .plural").text("s");
                $(".rightside .results .row:not(.empty)").removeClass("selected").find(".column.tick .checkbox").removeClass("checked");
                $(".menupan.bulk").find(".menuitem").removeClass("zero").find(".text .count").text("").end().find(".text .plural").text("s").end().find(".text .type").text("ZIP");
            }
        });
        $(".rightside .titlebar .bulkaction").click(function () {
            if (!$(this).hasClass("selected")) return;
            var position = $(this).position();
            $(".menupan.bulk").toggle().css({
                left: position.left + 3,
                top: position.top + 32 - 4
            });
        });
        //menupan
        $(".menupan").bind("mousedownoutside", function (event) {
            if ($(event.target).closest(".bulkaction").length > 0) return;
            if ($(event.target).parent().hasClass("action")) return;
            if ($(event.target).closest(".addtofolder").length > 0) return;
            if ($(event.target).closest(".menupan.folder, menupan.note, menupan.email").length > 0) return;
            if ($(event.target).attr("id") == "Notes") return;
            $(this).hide();
            if ($(this).hasClass("folder") && $(this).attr("action") == "addall")
                $(".rightside .resultscount .addtofolder").removeClass("hover");
            if (($(this).hasClass("folder") || $(this).hasClass("note") || $(this).hasClass("email")) && $(this).attr("action") == "single" && $(event.target).closest(".row").length == 0)
                $(".rightside .results .row.hover").removeClass("hover");
            event.stopPropagation();
        });
        $(".menupan.bulk .menuitem.parent").hover(function () {
            if ($(this).closest(".rightside").hasClass("notloggedin") && ($(this).hasClass("note") || $(this).hasClass("folder"))) return;
            $(".menupan:not(.bulk)").hide();
            var pan = $(this).closest(".menupan"), panPosition = pan.position(), position = $(this).position(), panWidth = pan.width();
            $($(this).attr("child")).show().css({
                left: panPosition.left + position.left + panWidth + 2,
                top: panPosition.top + position.top
            }).attr("action", "bulk").attr("jobadid", "");
            if ($(this).hasClass("note")) {
                var menupan = $(".menupan.note");
                menupan.find(".editarea").appendTo(menupan)
                menupan.find(".row:not(.empty)").remove();
                var newRow = menupan.find(".row.empty").clone(true).removeClass("empty");
                menupan.find("#Notes").val("").end().find(".editarea").appendTo(newRow).show();
                newRow.attr("id", "").insertAfter(menupan.find(".add")).find(".title, .content").hide();
            }
            if ($(this).hasClass("email"))
                $(".menupan.email").find(".thisjob").text($(this).closest(".rightside").find(".results .row.selected").length == 1 ? "this job" : "these jobs");
        }, function () {
        });
        $(".menupan.bulk .menuitem:not(.parent)").hover(function () {
            $(".menupan:not(.bulk)").hide();
        }, function () {
        });
        $(".rightside:not(.notloggedin) .menupan.bulk .menuitem.hide").click(function () {
            var jobAdIds = new Array();
            $(".rightside .results .row.selected").each(function () {
                $(this).removeClass("selected hover").find(".column.tick .checkbox").removeClass("checked").end().find(".column.flag .flag").removeClass("flagged");
                jobAdIds.push($(this).attr("id"));
                if ($(this).find(".hidprompt").length > 0) $(this).addClass("hid");
                else {
                    var jobAdId = new Array();
                    jobAdId.push($(this).attr("id"));
                    var html = "<div class='hidprompt'><span class='text'>This job has been moved to your \"Hidden jobs\" folder (underneath the Filters on the left). It will not appear in any further search results unless it is reposted by an employer.</span><div class='icon'></div><div class='undo'>Undo hide</div></div>";
                    $(this).append(html).addClass("hid").find(".undo").click(function () {
                        $(this).closest(".row").removeClass("hid");
                        hide("unhide", jobAdId);
                    });
                }
            });
            $(".rightside .titlebar .bulktick .checkbox").removeClass("checked");
            $(".rightside .titlebar .bulkaction").removeClass("selected").find(".dropdown .text .count").text("").end().find(".dropdown .text .plural").text("s");
            $(".menupan.bulk").find(".menuitem").removeClass("zero").find(".text .count").text("").end().find(".text .plural").text("s").end().find(".text .type").text("ZIP");
            $(".menupan").hide();
            hide("hide", jobAdIds);
        });
        $(".rightside .menupan.bulk .menuitem.restore").click(function () {
            var jobAdIds = new Array();
            $(".rightside .results .row.selected").each(function () {
                jobAdIds.push($(this).attr("id"));
                $(this).addClass("empty");
            });
            $(".menupan").hide();
            hide("unhide", jobAdIds);
            criteria["Page"] = "1";
            criteria["Items"] = "10";
            partialCall();
        });
        $(".menupan.folder .menuitem.folder").click(function () {
            var folderId = $(this).attr("folderid");
            var menupan = $(this).closest(".menupan");
            var action = menupan.attr("action");
            var jobAdId = menupan.attr("jobadid"), jobAdIds = new Array();
            if (action == "bulk") {
                $(".rightside .results .row.selected").each(function () {
                    jobAdIds.push($(this).attr("id"));
                });
            } else if (action == "addall") {
                $(".rightside .results .row:not(.empty)").each(function () {
                    jobAdIds.push($(this).attr("id"));
                });
            } else jobAdIds.push(jobAdId);
            var requestData = {};
            requestData["jobAdIds"] = jobAdIds;
            currentRequest = $.post($(this).attr("url").unMungeUrl(),
				requestData,
				function (data, textStatus, xmlHttpRequest) {
				    $(".leftside .area.favourite #" + folderId + " .count").text("(" + data.Count + ")");
				    if (action == "bulk") {
				        $(".menupan.bulk, .menupan.folder").hide();
				    } else if (action == "addall") {
				        $(".menupan.folder").hide();
				    } else {
				        $(".menupan.folder").hide();
				        $(".rightside .results #" + jobAdId).removeClass("hover");
				    }
				    currentRequest = null;
				    //hidden jobs view need to update rightside and numbers as well
				    if ($(".rightside").hasClass("BlockList")) {
				        criteria["Page"] = "1";
				        criteria["Items"] = "10";
				        partialCall();
				    }
				}
			);
        });
        $(".menupan.folder .menuitem.removefromfolder").click(function () {
            var folderId = $(".rightside .resultheader .icon.rename").attr("folderid");
            var menupan = $(this).closest(".menupan");
            var jobAdId = menupan.attr("jobadid"), jobAdIds = new Array();
            jobAdIds.push(jobAdId);
            var requestData = {};
            requestData["jobAdIds"] = jobAdIds;
            currentRequest = $.post($(this).attr("url").unMungeUrl().replace(/00000000-0000-0000-0000-000000000000/gi, folderId),
				requestData,
				function (data, textStatus, xmlHttpRequest) {
				    $(".menupan.folder").hide();
				    currentRequest = null;
				    criteria["Page"] = "1";
				    criteria["Items"] = "10";
				    partialCall();
				}
			);
        });
        $(".menupan.bulk .menuitem.download").click(function () {
            var select = $("<select name='jobAdIds' multiple='multiple'></select>");
            $(".rightside .results .row.selected").each(function () {
                select.append("<option value='" + $(this).attr("id") + "' selected='selected'></option>");
            });
            var form = $("<form action='" + downloadUrl + "' method='post'></form>");
            form.append(select).appendTo($("body")).submit().remove();
            $(".menupan.bulk").hide();
        });
        $(".menupan.note .add .text").click(function () {
            var menupan = $(this).closest(".menupan");
            var newRow = menupan.find(".row.empty").clone(true).removeClass("empty").attr("id", "").insertAfter(menupan.find(".add"));
            newRow.find(".title, .content").hide().end().append(menupan.find("#Notes").val("").end().find(".editarea").show());
        });
        $(".menupan.note .buttons .text.edit").click(function () {
            var row = $(this).closest(".row");
            row.closest(".menupan").find(".editarea").appendTo(row).show().find("#Notes").val(row.find(".content").text());
            row.find(".title, .content").hide();
        });
        $(".menupan.note .buttons .text.delete").click(function () {
            var row = $(this).closest(".row");
            var noteId = row.attr("id");
            var menupan = row.closest(".menupan");
            row.find(".editarea").appendTo(menupan);
            var url = apiDeleteNoteUrl.replace(/00000000-0000-0000-0000-000000000000/gi, noteId);
            currentRequest = $.post(url,
				null,
				function (data, textStatus, xmlHttpRequest) {
				    currentRequest = null;
				}
			);
            var jobadRow = $(".rightside .results #" + menupan.attr("jobadid"));
            var notes = jobadRow.data("notes");
            notes = $.grep(notes, function (element, index) {
                return element.Id == noteId ? null : element;
            });
            jobadRow.find(".column.title .action .icon.note .count").text("(" + notes.length + ")").end().data("notes", notes);
            row.remove();
        });
        $(".menupan.note .button.save").click(function () {
            var menupan = $(this).closest(".menupan");
            var jobAdIds = new Array();
            if (menupan.attr("action") == "single") {
                jobAdIds.push(menupan.attr("jobadid"));
            } else {
                $.each($(".rightside .results .row.selected"), function () {
                    jobAdIds.push($(this).attr("id"));
                });
            }
            var requestData = {}, url;
            var row = $(this).closest(".row");
            requestData["text"] = row.find("#Notes").val();
            //new note
            if (!row.attr("id") || row.attr("id") == "") {
                url = apiNewNoteUrl;
                requestData["jobAdIds"] = jobAdIds;
            } else { //editing note
                url = apiEditNoteUrl.replace(/00000000-0000-0000-0000-000000000000/gi, row.attr("id"));
            }
            currentRequest = $.post(url,
				requestData,
				function (data, textStatus, xmlHttpRequest) {
				    row.find(".title").show().end().find(".title .date").text(getNoteUpdateTime(new Date())).end().find(".content").text(requestData["text"]).show().end().find(".editarea").hide();
				    if (menupan.attr("action") == "bulk") {
				        menupan.hide();
				        $(".menupan.bulk").hide();
				        $(".rightside .results .row.selected").each(function () {
				            $(this).attr("notesloaded", "false");
				        });
				        loadAllNotes();
				    } else {
				        var notes = $(".rightside .results #" + menupan.attr("jobadid")).data("notes");
				        if (!row.attr("id") || row.attr("id") == "") {
				            notes.unshift({ Id: data.Id, Text: requestData["text"], UpdateTime: new Date() });
				        } else notes = $.map(notes, function (item) {
				            if (item.Id == row.attr("id")) item.Text = requestData["text"];
				            return item;
				        });
				        $(".rightside .results #" + menupan.attr("jobadid")).find(".column.title .action .icon.note .count").text("(" + notes.length + ")").end().data("notes", notes);
				    }
				    if (!row.attr("id") || row.attr("id") == "") row.attr("id", data.Id);
				    currentRequest = null;
				}
			);
        });
        $(".menupan.note .button.cancel").click(function () {
            var row = $(this).closest(".row");
            var menupan = row.closest(".menupan");
            if (!row.attr("id") || row.attr("id") == "") {
                //new empty note
                row.find(".editarea").appendTo(menupan).hide();
                row.remove();
            } else {
                //editing note
                row.find(".title, .content").show().end().find(".editarea").hide();
            }
            if (menupan.attr("action") == "bulk") {
                menupan.hide();
                $(".menupan.bulk").hide();
            }
        });
        organiseFields($(".menupan.email"));
        $(".menupan.email #ToNames").closest(".field").append("<div class='help'>(use commas to separate multiple names)</div>");
        $(".menupan.email #ToEmailAddresses").closest(".field").append("<div class='help'>(use commas to separate multiple emails)</div>");
        $(".menupan.email .button.send").click(function () {
            var button = $(this);
            var menupan = button.closest(".menupan");

            if (button.attr("sending") == "true") return false;
            button.attr("sending", "true");
            if (currentRequest) currentRequest.abort();

            var toNamesValue = $("#ToNames").realValue();
            var toEmailAddressesValue = $("#ToEmailAddresses").realValue();
            var toNames = toNamesValue.split(",");
            var toEmailAddresses = toEmailAddressesValue.split(",");
            if (toNames.length != toEmailAddresses.length) {
                showErrInfo($(".menupan.email"), { Key: "NotMatch", Message: "The numbers (" + toNames.length + ") of your friend's name(s) doesn't match the numbers (" + toEmailAddresses.length + ") of your friend's email address(es)." });
                return false;
            }
            var tos = new Array();
            if (toNamesValue != "")
                $.each(toNames, function (index, item) {
                    var pair = {};
                    pair["ToName"] = item;
                    pair["ToEmailAddress"] = toEmailAddresses[index];
                    tos.push(pair);
                });
            var jobAdIds = new Array();
            if (menupan.attr("action") == "single")
                jobAdIds.push(menupan.attr("jobadid"));
            else
                $.each($(".rightside .results .row.selected"), function () {
                    jobAdIds.push($(this).attr("id"));
                });

            currentRequest = $.ajax({
                type: "POST",
                url: button.attr("url").unMungeUrl(),
                data: JSON.stringify({
                    FromName: $("#FromName").realValue(),
                    FromEmailAddress: $("#FromEmailAddress").realValue(),
                    Tos: tos,
                    JobAdIds: jobAdIds
                }),
                dataType: "json",
                contentType: "application/json",
                success: function (data, textStatus, xmlHttpRequest) {
                    if (data == "") {
                        showErrInfo($(".menupan.email"));
                    } else if (data.Success) {
                        menupan.find(".validationerror").hide().end().hide();
                        if (menupan.attr("action") == "bulk") $(".menupan.bulk").hide();
                    } else {
                        showErrInfo($(".menupan.email"), data.Errors);
                    }
                    button.removeAttr("sending");
                    currentRequest = null;
                },
                error: function (error) {
                    var data = $.parseJSON(error.responseText);
                    if (!data.Success) showErrInfo($(".menupan.email"), data.Errors);
                }
            });
        });
        $(".menupan.email .button.cancel").click(function () {
            var menupan = $(this).closest(".menupan");
            menupan.find(".validationerror").hide().end().hide();
            if (menupan.attr("action") == "bulk") $(".menupan.bulk").hide();
        });
        //expand all job details
        $(".rightside .sort .expandalljobs").click(function () {
            var text = $(this).find(".text").toggleClass("expand collapse");
            $(this).find(".button").toggleClass("expand collapse");
            if ($(text).hasClass("collapse")) {
                $(text).text("Collapse all jobs");
                $(".rightside .results .row .column.flag .button").removeClass("expand").addClass("collapse");
                $(".rightside .results .row .details").removeClass("collapsed").addClass("expanded");
            } else {
                $(text).text("Expand all jobs");
                $(".rightside .results .row .column.flag .button").removeClass("collapse").addClass("expand");
                $(".rightside .results .row .details").removeClass("expanded").addClass("collapsed");
            }
        });
        //pagination
        $(".rightside .pg #ItemsPerPage").change(function () {
            criteria["Items"] = $(this).find("option:selected").val();
            criteria["Page"] = 1;
            partialCall();
        });
        $(".rightside .pagination-holder a.page").click(function () {
            if ($(this).hasClass("current")) return false;
            criteria["Page"] = $(this).attr("page");
            partialCall();
            return false;
        });
        //notloggedin
        $(".rightside.notloggedin .menupan.bulk .menuitem.folder, .rightside.notloggedin .menupan.bulk .menuitem.note, .rightside.notloggedin .menupan.bulk .menuitem.hide").click(function () {
            showLogin();
        });
        //job ad item list events
        initItemListEvents();
        //google ads
        reloadGoogleAds($(".rightside .results .querystringforga").text());
    }

    reloadGoogleAds = function (query) {
        if (!query || query == "") {
            $("#gainlist").hide();
            $("#gainpagination").hide();
            $("#gainemptylist").hide();
            return;
        }
        pageOptions["query"] = query;
        if ($(".rightside .results .row:not(.empty)").length > 0) {
            $("#gainlist").hide();
            $("#gainpagination").insertBefore($(".rightside .pg .itemsperpage")).show();
            $("#gainemptylist").hide();
            new google.ads.search.Ads(pageOptions, adblock2);
        } else {
            $("#gainlist").hide();
            $("#gainpagination").hide();
            $("#gainemptylist").insertAfter($(".rightside .results .emptylist")).show();
            new google.ads.search.Ads(pageOptions, adblock3);
        }
    }

    showErrInfo = function (context, errObj) {
        var message = "Please review the errors below";
        $(".validationerror", context).show();
        $(".succ-info", context).hide();
        $(".control", context).removeClass("error");
        $(".menupan.email .button.send").removeAttr("sending");
        $.each(errObj, function () {
            if (this["Key"] == "Tos") {
                $("#ToNames", context).closest(".control").addClass("error");
                $("#ToEmailAddresses", context).closest(".control").addClass("error");
            } else if (this["Key"] == "ToName")
                $("#ToNames", context).closest(".control").addClass("error");
            else if (this["Key"] == "ToEmailAddress")
                $("#ToEmailAddresses", context).closest(".control").addClass("error");
            else if (this["Key"] == "NotMatch") {
                $("#ToNames", context).closest(".control").addClass("error");
                $("#ToEmailAddresses", context).closest(".control").addClass("error");
            } else
                $("#" + this["Key"], context).closest(".control").addClass("error");
        });
        $(".validationerror ul", context).text("");
        $.each(errObj, function () {
            if (this["Key"] == "Tos") {
                $(".validationerror ul", context).append("<li><div class='icon'></div>Your friend's name is required.</li>");
                $(".validationerror ul", context).append("<li><div class='icon'></div>" + this["Message"] + "</li>");
            } else
                $(".validationerror ul", context).append("<li><div class='icon'></div>" + this["Message"] + "</li>");
        });
    }

    initItemListEvents = function () {
        $(".rightside .results .row").hover(function () {
            $(this).addClass("hover");
        }, function () {
            if ($(".menupan.folder:visible").length > 0 && $(this).attr("id") == $(".menupan.folder").attr("jobadid")) return;
            if ($(".menupan.note:visible").length > 0 && $(this).attr("id") == $(".menupan.note").attr("jobadid")) return;
            $(this).removeClass("hover");
        });
        $(".rightside .results .row .column.tick .checkbox").click(function () {
            $(this).toggleClass("checked").closest(".row").toggleClass("selected");
            var count = $(this).closest(".results").find(".row.selected:not(.empty)").length;
            if (count > 0) {
                $(".rightside .titlebar .bulkaction").addClass("selected").find(".dropdown .text .count").text(count).end().find(".dropdown .text .plural").text(count > 1 ? "s" : "");
                $(".menupan.bulk").find(".menuitem").removeClass("zero").find(".text .count").text(count).end().find(".text .plural").text(count > 1 ? "s" : "").end().find(".text .type").text(count > 1 ? "ZIP" : "DOC");
            } else {
                $(".rightside .titlebar .bulkaction").removeClass("selected").find(".dropdown .text .count").text("").end().find(".dropdown .text .plural").text("s");
                $(".menupan.bulk").find(".menuitem").addClass("zero").find(".text .count").text(count).end().find(".text .plural").text("s").end().find(".text .type").text("ZIP");
            }
            if (count == $(".rightside .pg #ItemsPerPage option:selected").val())
                $(".rightside .titlebar .bulktick .checkbox").addClass("checked");
            else $(".rightside .titlebar .bulktick .checkbox").removeClass("checked");
        });
        $(".rightside:not(.notloggedin) .results .row .column.flag .flag").click(function () {
            $(this).toggleClass("flagged");
            var jobAdIds = new Array();
            jobAdIds.push($(this).closest(".row").attr("id"));
            flag($(this).hasClass("flagged") ? "flag" : "unflag", jobAdIds);
        });
        $(".rightside .results .row .column.flag .button").click(function () {
            $(this).toggleClass("expand collapse").closest(".bg").next().toggleClass("expanded collapsed");
        });
        $(".rightside:not(.notloggedin) .results .row .column.title .action .icon.folder").hover(function () {
            $(this).addClass("hover");
        }, function () {
            if ($(".menupan.folder:visible").length > 0 && $(this).closest(".row").attr("id") == $(".menupan.folder").attr("jobadid")) return;
            $(this).removeClass("hover");
        }).click(function () {
            $(".rightside .results .row .column.title .action .icon:not(.folder)").removeClass("hover");
            $(".menupan:not(.folder)").hide();
            var position = $(this).position();
            $(".menupan.folder").toggle().css({
                left: position.left - 1,
                top: position.top + 30 - 1
            }).attr("jobadid", $(this).closest(".row").attr("id")).attr("action", "single");
        });
        $(".rightside:not(.notloggedin) .results .row .column.title .action .icon.note").hover(function () {
            $(this).addClass("hover");
        }, function () {
            if ($(".menupan.note:visible").length > 0 && $(this).closest(".row").attr("id") == $(".menupan.note").attr("jobadid")) return;
            $(this).removeClass("hover");
        }).click(function () {
            var jobAdId = $(this).closest(".row").attr("id");
            $(".rightside .results .row .column.title .action .icon:not(.note)").removeClass("hover");
            $(".menupan:not(.note)").hide();
            var position = $(this).position();
            var menupan = $(".menupan.note");
            menupan.toggle().css({
                left: position.left - 1,
                top: position.top + 30 - 1
            }).attr("action", "single");
            if (menupan.length == 0) return;
            if (menupan.attr("jobadid") != jobAdId) {
                menupan.attr("jobadid", jobAdId);
                loadNotes(jobAdId);
            }
        });
        $(".rightside .results .row .column.title .action .icon.email").hover(function () {
            $(this).addClass("hover");
        }, function () {
            if ($(".menupan.email:visible").length > 0 && $(this).closest(".row").attr("id") == $(".menupan.email").attr("jobadid")) return;
            $(this).removeClass("hover");
        }).click(function () {
            $(".rightside .results .row .column.title .action .icon:not(.email)").removeClass("hover");
            $(".menupan:not(.email)").hide();
            var position = $(this).position();
            $(".menupan.email").toggle().css({
                left: position.left - 1,
                top: position.top + 30 - 1
            }).attr("jobadid", $(this).closest(".row").attr("id")).attr("action", "single").find(".jobtitle").text($(this).closest(".row").find(".column.title .title").text());
        });
        $(".rightside .results .row .column.title .action .icon.download").click(function () {
            $(".menupan").hide();
            var select = $("<select name='jobAdIds' multiple='multiple'></select>");
            select.append("<option value='" + $(this).closest(".row").attr("id") + "' selected='selected'></option>");
            var form = $("<form action='" + downloadUrl + "' method='post'></form>");
            form.append(select).appendTo($("body")).submit().remove();
        });
        $(".rightside:not(.notloggedin) .results .row .column.title .action .icon.hide").click(function () {
            var jobAdIds = new Array();
            var row = $(this).closest(".row");
            jobAdIds.push(row.attr("id"));
            hide("hide", jobAdIds);
            row.removeClass("selected hover").find(".column.tick .checkbox").removeClass("checked").end().find(".column.flag .flag").removeClass("flagged");
            if (row.find(".hidprompt").length > 0) row.addClass("hid");
            else {
                var html = "<div class='hidprompt'><span class='text'>This job has been moved to your \"Hidden jobs\" folder (underneath the Filters on the left). It will not appear in any further search results unless it is reposted by an employer.</span><div class='icon'></div><div class='undo'>Undo hide</div></div>";
                row.append(html).addClass("hid").find(".undo").click(function () {
                    $(this).closest(".row").removeClass("hid");
                    hide("unhide", jobAdIds);
                });
            }
        });
        $(".rightside .results .row .column.title .action .icon.restore").click(function () {
            var jobAdIds = new Array();
            var row = $(this).closest(".row");
            jobAdIds.push(row.attr("id"));
            hide("unhide", jobAdIds);
            row.removeClass("selected hover").addClass("empty");
            criteria["Page"] = "1";
            criteria["Items"] = "10";
            partialCall();
        });
        //notloggedin
        $(".rightside.notloggedin .results .row .column.title .action .icon.folder, .rightside.notloggedin .results .row .column.title .action .icon.note, .rightside.notloggedin .results .row .column.title .action .icon.hide, .rightside.notloggedin .results .row .column.flag .flag").click(function () {
            showLogin();
        });
    }

    loadNotes = function (jobAdId) {
        //load notes from db if need
        var row = $(".rightside .results #" + jobAdId);
        if (row.attr("notesloaded") != "true") {
            currentRequest = $.ajax({
                type: "POST",
                url: apiNotesUrl,
                async: false,
                data: JSON.stringify({
                    jobAdId: jobAdId
                }),
                dataType: "json",
                contentType: "application/json",
                success: function (data, textStatus, xmlHttpRequest) {
                    if (data == "") {
                    } else if (data.Success) {
                        row.data("notes", data.Notes);
                        $.each(data.Notes, function (index, element) {
                            element.UpdateTime = new Date(parseInt(element.UpdatedTime.substring(6)));
                        });
                        row.attr("notesloaded", "true");
                        row.find(".column.title .action .icon.note .count").text("(" + data.Notes.length + ")");
                    } else {
                    }
                },
                error: function (error) {
                }
            });
        }
        var menupan = $(".menupan.note");
        menupan.find(".editarea").appendTo(menupan)
        menupan.find(".row:not(.empty)").remove();
        $.each(row.data("notes"), function (index, element) {
            var newRow = menupan.find(".row.empty").clone(true).removeClass("empty");
            newRow.attr("id", element.Id);
            newRow.find(".title .date").text(getNoteUpdateTime(element.UpdateTime)).end().find(".content").text(element.Text).end().appendTo(menupan);
        });
    }

    loadAllNotes = function () {
        if ($(".rightside").hasClass("notloggedin")) return;
        //load all notes from db
        $(".rightside .results .row:not(.empty)").each(function () {
            var row = $(this);
            var jobAdId = row.attr("id");
            if (row.attr("notesloaded") != "true") {
                currentRequest = $.ajax({
                    type: "POST",
                    url: apiNotesUrl,
                    async: false,
                    data: JSON.stringify({
                        jobAdId: jobAdId
                    }),
                    dataType: "json",
                    contentType: "application/json",
                    success: function (data, textStatus, xmlHttpRequest) {
                        if (data == "") {
                        } else if (data.Success) {
                            row.data("notes", data.Notes);
                            $.each(data.Notes, function (index, element) {
                                element.UpdateTime = new Date(parseInt(element.UpdatedTime.substring(6)));
                            });
                            row.attr("notesloaded", "true");
                            row.find(".column.title .action .icon.note .count").text("(" + data.Notes.length + ")");
                        } else {
                        }
                    },
                    error: function (error) {
                    }
                });
            }
        });
    }

    getNoteUpdateTime = function (date) {
        var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        return date.getDate() + "-" + months[date.getMonth()] + "-" + date.getFullYear()
    }

    showLogin = function () {
        $(".overlay.login").dialog({
            modal: true,
            width: 360,
            height: 135,
            closeOnEscape: false,
            resizable: false,
            dialogClass: "loginprompt-dialog"
        });
    }

    flag = function (type, jobAdIds) {
        var requestData = {}, url;
        if (type == "flag" || type == "unflag") requestData["jobAdIds"] = jobAdIds;
        if (type == "flag") url = apiFlagJobAdsUrl;
        else if (type == "unflag") url = apiUnflagJobAdsUrl;
        else if (type == "current") url = apiUnflagCurrentJobAdsUrl;
        else url = apiUnflagAllJobAdsUrl;
        currentRequest = $.post(url,
			requestData,
			function (data, textStatus, xmlHttpRequest) {
			    $(".leftside .area.favourite .folder.flagged .count").text("(" + data.Count + ")");
			    if (type == "current" || type == "all")
			        $(".rightside .results .row .column.flag .flag").removeClass("flagged");
			    currentRequest = null;
			}
		);
    }

    hide = function (type, jobAdIds) {
        var requestData = {}, url;
        if (type == "hide" || type == "unhide") requestData["jobAdIds"] = jobAdIds;
        if (type == "hide") url = apiBlockJobAdsUrl;
        else if (type == "unhide") url = apiUnblockJobAdsUrl;
        else url = apiUnflagAllJobAdsUrl;
        currentRequest = $.post(url,
			requestData,
			function (data, textStatus, xmlHttpRequest) {
			    $(".leftside .area.hiddenjobs .count").text("(" + data.Count + ")");
			    currentRequest = null;
			}
		);
    }

    initLeftSideEvents = function () {
        //fields (textbox, checkbox, dropdown, autocomplete, slider
        organiseFields($(".leftside"));
        $(".leftside .area.search #Location").autocomplete(apiPartialMatchesUrl);
        $("#Distance").parent().dropdown();
        $(".leftside .area.search #CountryId").parent().dropdown().parent().find(".dropdown-item").click(function () {
            var url = $(this).text() == "" ? "" : apiPartialMatchesUrl.substring(0, apiPartialMatchesUrl.indexOf("countryId=") + 10) + $(".leftside .area.search #CountryId option:contains('" + $(this).text() + "')").val() + "&location=";
            $(".leftside .area.search #Location").setOptions({
                url: url
            });
        });
        //3 sliders in filter
        if ($(".leftside .area.filter").length > 0) {
            var ss = $(".salary-slider");
            initializeSalary(parseInt($("#SalaryLowerBound").val()), parseInt($("#SalaryUpperBound").val()), parseInt(ss.attr("minsalary")), parseInt(ss.attr("maxsalary")), parseInt(ss.attr("stepsalary")));
            var rs = $(".recency-slider");
            initializeRecency(parseInt($("#Recency").val()), parseInt(rs.attr("minrecency")), parseInt(rs.attr("maxrecency")), parseInt(rs.attr("steprecency")), eval(rs.attr("recencies")));
            var ds = $(".distance-slider");
            initializeDistance(parseInt($(".section.distance #DistanceInFilter").val()), parseInt(ds.attr("mindistance")), parseInt(ds.attr("maxdistance")), parseInt(ds.attr("stepdistance")), eval(ds.attr("distances")));
        }
        //section title
        $(".leftside .area.search .keywords .titlebar, .leftside .area.search .location .titlebar, .leftside .area.favourite .titlebar, .leftside .area.filter .titlebar").click(function () {
            $(this).parent().toggleClass("collapsed expanded");
        });
        $(".leftside .area.favourite .toggle").click(function () {
            $(this).toggleClass("collapsed expanded").text($(this).hasClass("collapsed") ? "Show all folders" : "Hide folders").prev().toggleClass("collapsed expanded");
        });
        //section title checkbox
        $(".leftside .area.filter .section .titlebar .checkbox").hover(function () {
            $(this).addClass("hover");
        }, function () {
            $(this).removeClass("hover");
        }).mousedown(function () {
            $(this).addClass("mousedown");
        }).mouseup(function () {
            $(this).removeClass("mousedown");
        }).click(function (event) {
            var section = $(this).closest(".section");
            var active = !$(this).hasClass("checked");
            if (section.hasClass("jobtypes")) {
                event.stopPropagation();
                return;
            } else if (section.hasClass("salary")) {
                if (active) {
                    criteria["SalaryLowerBound"] = $("#SalaryLowerBound").val();
                    criteria["SalaryUpperBound"] = $("#SalaryUpperBound").val();
                    criteria["IncludeNoSalary"] = $("#IncludeNoSalary:checked").length > 0;
                } else {
                    delete criteria["SalaryLowerBound"];
                    delete criteria["SalaryUpperBound"];
                    delete criteria["IncludeNoSalary"];
                }
            } else if (section.hasClass("dateposted")) {
                if (active) criteria["Recency"] = $("#Recency").val();
                else delete criteria["Recency"];
            } else if (section.hasClass("distance")) {
                if (active) {
                    criteria["Location"] = $(".leftside .area.search #Location").realValue();
                    criteria["Distance"] = $("#DistanceInFilter").val();
                    criteria["CountryId"] = $(".leftside .area.search #CountryId").val();
                } else {
                    $(".leftside .area.search .location").addClass("collapsed").removeClass("expanded");
                    delete criteria["Location"];
                    delete criteria["Distance"];
                    delete criteria["CountryId"];
                }
            } else if (section.hasClass("industry")) {
                if (active) {
                    var industry = new Array();
                    if (section.find(".allindustry .checkbox").hasClass("checked")) {
                        section.find(".industry").each(function () {
                            industry.push($(this).attr("id"));
                        });
                    } else {
                        section.find(".industry .checkbox.checked").each(function () {
                            industry.push($(this).parent().attr("id"));
                        });
                    }
                    criteria["IndustryIds"] = industry;
                } else delete criteria["IndustryIds"];
            } else if (section.hasClass("additional")) {
                if (active) {
                    var value = section.find(".notes input:checked").val();
                    if (value == "") delete criteria["HasNotes"];
                    else criteria["HasNotes"] = value;
                    value = section.find(".viewed input:checked").val();
                    if (value == "") delete criteria["HasViewed"];
                    else criteria["HasViewed"] = value;
                    value = section.find(".flagged input:checked").val();
                    if (value == "") delete criteria["IsFlagged"];
                    else criteria["IsFlagged"] = value;
                    value = section.find(".applied input:checked").val();
                    if (value == "") delete criteria["HasApplied"];
                    else criteria["HasApplied"] = value;
                } else {
                    delete criteria["HasNotes"];
                    delete criteria["HasViewed"];
                    delete criteria["IsFlagged"];
                    delete criteria["HasApplied"];
                }
            }
            applyFilter(criteria);
            $(this).toggleClass("checked");
            section.removeClass("collapsed").addClass("expanded");
            event.stopPropagation();
        });
        //change keywords
        $(".area.search .keywords .button.search").click(function () {
            if (!$(this).parent().find(".RetainFilters").hasClass("checked")) {
                criteria = getDefaultCriteria();
                applyDefultFilter();
            }
            var keywords = $(this).parent().find("#Keywords").realValue();
            if (keywords != "") criteria["Keywords"] = keywords;
            else delete criteria["Keywords"];
            var adTitle = $(this).parent().find("#AdTitle").realValue();
            if (adTitle != "") criteria["AdTitle"] = adTitle;
            else delete criteria["AdTitle"];
            var advertiser = $(this).parent().find("#Advertiser").realValue();
            if (advertiser != "") criteria["Advertiser"] = advertiser;
            else delete criteria["Advertiser"];
            applyFilter(criteria);
        });
        //change location
        $(".leftside .area.search #Location").change(function () {
            var loc = $(this).realValue();
            $(".leftside .area.search .radiusof").text(loc);
            if (!loc || loc == "") $(".leftside .area.search .radius").hide();
            else $(".leftside .area.search .radius").show();
        });
        if ($(".leftside .area.search .radiusof:empty").length == 0) $(".leftside .area.search .radius").show();
        $(".area.search .location .button.search").click(function () {
            if (!$(this).parent().find(".RetainFilters").hasClass("checked")) {
                criteria = getDefaultCriteria();
                applyDefultFilter();
            }
            var location = $(this).parent().find("#Location").realValue();
            if (location != "") {
                criteria["Location"] = location;
                criteria["Distance"] = $(this).parent().find("#Distance").val();
                criteria["CountryId"] = $(this).parent().find("#CountryId").val();
            } else {
                delete criteria["Location"];
                delete criteria["Distance"];
                delete criteria["CountryId"];
            }
            if (location == "") {
                $(".area.filter .section.distance").addClass("emptyloc collapsed").removeClass("expanded").find(".titlebar .checkbox").removeClass("checked");
            } else {
                var section = $(".area.filter .section.distance");
                section.removeClass("emptyloc collapsed").addClass("expanded").find(".titlebar .checkbox").addClass("checked");
                section.find(".from .location").text(criteria["Location"]);
                var index = 0, distances = eval(ds.attr("distances"));
                for (var i = 0; i < distances.length; i++)
                    if (distances[i] == criteria["Distance"]) {
                        index = i;
                        break;
                    }
                section.find(".distance-slider").slider("option", "value", index);
            }
            applyFilter(criteria);
        });
        $(".leftside .area.search input[type='text'], .leftside .area.search textarea").keypress(function (event) {
            if (event.keyCode == 13)
                $(this).closest(".content").find(".button.search").click();
        });
        //search tips
        $(".leftside .area.search .searchtips").click(function () {
            $(".overlay.tips").dialog({
                modal: true,
                width: 360,
                closeOnEscape: false,
                resizable: false,
                dialogClass: "tips-dialog"
            });
        });
        //folder (rename/empty)
        $(".leftside .area.favourite .folder .icon.rename").click(function () {
            var folder = $(this).closest(".folder");
            $(".overlay.renamefolder").dialog({
                modal: true,
                width: 360,
                closeOnEscape: false,
                resizable: false,
                dialogClass: "renamefolder-dialog"
            }).attr("folderid", folder.attr("id")).find(".titlebar .title").text("Rename '" + folder.find(".title").text() + "'").end().find(".prompt").hide();
        });
        $(".leftside .area.favourite .folder .icon.empty").click(function () {
            var folder = $(this).closest(".folder");
            var count = parseInt(folder.find(".count").text().replace(/\(/gi, "").replace(/\)/gi, ""));
            if (count == 0) return;
            $(".overlay.emptyfolder").dialog({
                modal: true,
                width: 360,
                closeOnEscape: false,
                resizable: false,
                dialogClass: "emptyfolder-dialog"
            }).attr("folderid", folder.attr("id")).find(".titlebar .title").text("Empty '" + folder.find(".title").text() + "' folder?").end().find(".name").text(folder.find(".title").text()).end().find(".count").text(count).end().find(".plural").text(count > 1 ? "s" : "");
        });
        //save search/email
        $(".leftside:not(.notloggedin) .area.filter .row.savesearch .title").click(function () {
            var keywords = criteria["Keywords"] ? criteria["Keywords"] : "";
            var location = criteria["Location"] ? criteria["Location"] : "";
            $(".overlay.savesearch").dialog({
                modal: true,
                width: 360,
                closeOnEscape: false,
                resizable: false,
                dialogClass: "savesearch-dialog"
            }).find(".prompt").hide().end().find("#SearchName").val(keywords + (location == "" ? "" : ", " + location));
        });
        $(".leftside:not(.notloggedin) .area.filter .row.emailalert .title").click(function () {
            $(".overlay.emailalert").dialog({
                modal: true,
                width: 360,
                closeOnEscape: false,
                resizable: false,
                dialogClass: "emailalert-dialog"
            }).find(".prompt").hide();
        });
        //job type
        $(".leftside .area.filter .section.jobtypes .jobtype .icon").hover(function () {
            $(this).addClass("hover");
        }, function () {
            $(this).removeClass("hover");
        }).mousedown(function () {
            $(this).addClass("mousedown");
        }).mouseup(function () {
            $(this).removeClass("mousedown");
        }).click(function () {
            if ($(this).parent().parent().find(".jobtype .icon.checked").length == 1 && $(this).hasClass("checked")) return;
            $(this).toggleClass("checked");
            var filterJobTypes = new Array();
            $(".area.filter .section.jobtypes .jobtype").each(function () {
                if ($(this).find(".icon").hasClass("checked"))
                    filterJobTypes.push($(this).attr("class").replace("jobtype ", ""));
            });
            criteria["JobTypes"] = filterJobTypes.join();
            applyFilter(criteria);
        });
        //salary
        $("#IncludeNoSalary").prev().click(function () {
            $(this).closest(".section").addClass("expanded").removeClass("collapsed").find(".titlebar .checkbox").addClass("checked");
            criteria["IncludeNoSalary"] = $(this).hasClass("checked");
            applyFilter(criteria);
        });
        //recency
        //distance
        $(".area.filter .section.distance .emptyloc span, .area.filter .section.distance .withloc .from .change").click(function () {
            $(".area.search .location").addClass("expanded").removeClass("collapsed").find("#Location").focus();
        });
        //industry
        $(".leftside .area.filter .section.industry .industry").slice(5).hide();
        $(".leftside .area.filter .section.industry .expander.left").click(function () {
            var text = $(this).text();
            if (text.indexOf("Show more") == 0)
                $(this).text("Show 10 more ▼").next().show().parent().find(".industry").slice(5, 10).show();
            else if (text.indexOf("10") > 0)
                $(this).text("Show 9 more ▼").next().show().parent().find(".industry").slice(10, 20).show();
            else
                $(this).hide().next().show().parent().find(".industry").show();
        }).text("Show more ▼");
        $(".leftside .area.filter .section.industry .expander.right").click(function () {
            $(this).hide().prev().text("Show more ▼").show().parent().find(".industry").slice(5).hide();
        }).text("Show less ▲");
        $(".area.filter .section.industry .allindustry .checkbox").click(function () {
            $(this).toggleClass("checked");
            $(this).closest(".section").addClass("expanded").removeClass("collapsed").find(".titlebar .checkbox").addClass("checked");
            if ($(this).hasClass("checked")) {
                $(this).parent().parent().find(".industry .checkbox").removeClass("checked");
                var industry = new Array();
                $(this).parent().parent().find(".industry").each(function () {
                    industry.push($(this).attr("id"));
                });
                criteria["IndustryIds"] = industry;
            } else {
                criteria["IndustryIds"] = "";
            }
            applyFilter(criteria);
        });
        $(".area.filter .section.industry .industry .checkbox").click(function () {
            $(this).toggleClass("checked");
            $(this).closest(".section").addClass("expanded").removeClass("collapsed").find(".titlebar .checkbox").addClass("checked");
            $(this).parent().parent().find(".allindustry .checkbox").removeClass("checked");
            if ($(this).hasClass("checked")) {
                if (criteria["IndustryIds"] && criteria["IndustryIds"].length > 0)
                    criteria["IndustryIds"].push($(this).parent().attr("id"));
                else criteria["IndustryIds"] = [$(this).parent().attr("id")];
            } else {
                var id = $(this).parent().attr("id");
                criteria["IndustryIds"] = $.map(criteria["IndustryIds"], function (element) {
                    return element == id ? null : element;
                });
            }
            applyFilter(criteria);
        });
        //additional
        $(".area.filter .section.additional input").click(function () {
            $(this).closest(".section").addClass("expanded").removeClass("collapsed").find(".titlebar .checkbox").addClass("checked");
            var value = $(this).parent().find("input:checked").val();
            switch ($(this).attr("name")) {
                case "notes":
                    if (value == "") delete criteria["HasNotes"];
                    else criteria["HasNotes"] = value;
                    break;
                case "viewed":
                    if (value == "") delete criteria["HasViewed"];
                    else criteria["HasViewed"] = value;
                    break;
                case "flagged":
                    if (value == "") delete criteria["IsFlagged"];
                    else criteria["IsFlagged"] = value;
                    break;
                case "applied":
                    if (value == "") delete criteria["HasApplied"];
                    else criteria["HasApplied"] = value;
                    break;
            }
            applyFilter(criteria);
        });
        //reset
        $(".area.filter .row.reset .title").click(function () {
            criteria = getDefaultCriteria();
            applyDefultFilter();
            var keywords = $(".leftside .area.search #Keywords").realValue();
            if (keywords != "") criteria["Keywords"] = keywords;
            var adTitle = $(".leftside .area.search #AdTitle").realValue();
            if (adTitle != "") criteria["AdTitle"] = adTitle;
            var advertiser = $(".leftside .area.search #Advertiser").realValue();
            if (advertiser != "") criteria["Advertiser"] = advertiser;
            var location = $(".leftside .area.search #Location").realValue();
            if (location != "") {
                criteria["Location"] = location;
                criteria["Distance"] = $(".leftside .area.search #Distance").val();
                criteria["CountryId"] = $(".leftside .area.search #CountryId").val();
            }
            applyFilter(criteria);
        });
        $(".leftside #apiorpartial").click(function () {
            $(this).toggleClass("checked");
        });
        //notloggedin
        $(".leftside.notloggedin .area.filter .row.savesearch .title, .leftside.notloggedin .area.filter .row.emailalert .title, .rightside.notloggedin .resultscount .action").click(function () {
            showLogin();
        });
    }

    //apply current filter with default page = 1, 10 items per page
    applyFilter = function (criteria) {
        criteria["Page"] = "1";
        criteria["Items"] = "10";
        partialCall();
    }

    //request a new search with current filter, get results, refresh page content
    partialCall = function () {
        $(".overlay.loading").dialog({
            modal: true,
            width: 150,
            height: 150,
            closeOnEscape: false,
            resizable: false,
            dialogClass: "loading-dialog"
        });
        var requestData = {};
        for (var p in criteria)
            if (criteria.hasOwnProperty(p))
                requestData[p] = criteria[p];
        if (requestData["JobTypes"]) {
            var value = 0;
            var requestTypes = requestData["JobTypes"].replace(/ /gi, "").split(",");
            $.each(requestTypes, function (index, element) {
                value += jobTypes[element];
            });
            requestData["JobTypes"] = value;
        }
        //always send Australia to backend
        if (!requestData["CountryId"] || requestData["CountryId"] == "")
            requestData["CountryId"] = $(".leftside .area.search #CountryId").val();
        var jsonReturn;
        //depend on checkbox for SearchResult, all partial calls for Folder(Flag/Folder/Hidden)
        if ($("#apiorpartial").hasClass("checked") && $(".rightside").hasClass("SearchResult")) {
            //logTime("Make API call");
            //request
            currentRequest = $.get($(".rightside").attr("apiurl").unMungeUrl(),
				requestData,
				function (data, textStatus, xmlHttpRequest) {
				    if (data == "") {
				        showAPIErrorOverlay();
				    } else if (data.Success) {
				        updateAPIResults(data);
				    } else {
				        showAPIErrorOverlay();
				    }
				    currentRequest = null;
				}
			);
        } else {
            //logTime("Make Partial Call");
            //request
            currentRequest = $.post($(".rightside").attr("partialurl").unMungeUrl(),
				requestData,
				function (data, textStatus, xmlHttpRequest) {
				    updatePartialResults(data);
				    currentRequest = null;
				}
			);
        }
        //google analytics
        _gaq.push(['_trackEvent', 'JobAdSearch', 'AjaxCall', $.param(requestData)]);
    }

    showAPIErrorOverlay = function () {
        $("body").animate({ scrollTop: 0 }, 200);
        $(".overlay.loading").dialog("close");
        $(".overlay.apierror").dialog({
            modal: true,
            width: 220,
            height: 110,
            closeOnEscape: false,
            resizable: false,
            dialogClass: "apierror-dialog"
        });
    }

    updatePartialResults = function (data) {
        $("#gainlist, #gainpagination, #gainemptylist").appendTo($(".rightside"));
        var emptylist = $(".rightside .results .emptylist");
        emptylist.appendTo($("body"));
        $(".rightside .results").html(data);
        emptylist.appendTo($(".rightside .results"));
        //logTime("Finish updating html data");
        initItemListEvents();
        //logTime("Finish init all events within job ad item list on right side");
        //update criteria & display
        updateCriteria($(".rightside .results .criteriahtml").html());
        //logTime("Finish update criteria text");
        //update results count
        updateResultsCount($(".rightside .results .hits .total").text());
        updatePagination();
        //logTime("Finish update result count & pagination");
        //update counts
        var hits = {};
        $(".rightside .results .hits .industryhits").each(function () {
            hits[$(this).attr("id")] = $(this).text();
        });
        updateIndustryHits(hits);
        hits = {};
        $(".rightside .results .hits .jobtypehits").each(function () {
            hits[$(this).attr("id")] = $(this).text();
        });
        updateJobTypeHits();
        //logTime("Finish update hits count");
        //update data within job ad item (job types)
        updateJobAdItemData();
        //logTime("Finish update job ad item data");
        //if empty result
        updateEmptyResult();
        //logTime("Finish update empty list");
        //pagination holder position
        var holder = $(".pagination-holder").css("margin-left", "0px");
        var margin = ($(".pagination-container").width() - holder.width()) / 2;
        holder.css("margin-left", margin + "px");
        //logTime("Finish update pagination holder position");
        $("body").animate({ scrollTop: 0 }, 200);
        $(".overlay.loading").dialog("close");
        //all notes
        loadAllNotes();
        //google ads
        reloadGoogleAds($(".rightside .results .querystringforga").text());
    }

    updateAPIResults = function (data) {
        updateHash(data.Hash);
        updateJsonData(data);
        //logTime("Finish updating json data");
        //update criteria & display
        updateCriteria(data.CriteriaHtml);
        //logTime("Finish update criteria text");
        //update results count
        updateResultsCount(data.TotalJobAds);
        updatePagination();
        //logTime("Finish update result count & pagination");
        //update counts
        updateIndustryHits(data.IndustryHits);
        updateJobTypeHits(data.JobTypeHits);
        //update data within job ad item (job types)
        updateJobAdItemData();
        //logTime("Finish update job ad item data");
        //if empty result
        updateEmptyResult();
        //logTime("Finish update empty list");
        //pagination holder position
        var holder = $(".pagination-holder").css("margin-left", "0px");
        var margin = ($(".pagination-container").width() - holder.width()) / 2;
        holder.css("margin-left", margin + "px");
        //logTime("Finish update pagination holder position");
        $(".overlay.loading").dialog("close");
        $("body").animate({ scrollTop: 0 }, 200);
        //all notes
        loadAllNotes();
        //google ads
        reloadGoogleAds(data.QueryStringForGa);
    }

    updateCriteria = function (criteriaHtml) {
        $(".rightside.SearchResult #results-header-text, .rightside.BrowseResult #results-header-text").html(criteriaHtml).find(".synonyms").click(function () {
            var prev = $(this).prev();
            if (prev.text() == "with synonyms") {
                prev.text("without synonyms");
                criteria["IncludeSynonyms"] = false;
            } else {
                prev.text("with synonyms");
                criteria["IncludeSynonyms"] = true;
            }
            criteria["Page"] = "1";
            criteria["Items"] = "10";
            partialCall();
        });
    }

    updateHash = function (hash) {
        if (setHash && hash) {
            window.location.hash = "#" + hash;
        }
    }

    updateEmptyResult = function () {
        var resultCount = $(".rightside .results .row:not(.empty)").length;
        if (resultCount == 0) {
            $(".rightside .resultscount, .rightside .sort, .rightside .titlebar, .rightside .pg").hide();
            $(".rightside .results .emptylist").show();
        } else {
            $(".rightside .resultscount, .rightside .sort, .rightside .titlebar, .rightside .pg").show();
            $(".rightside .results .emptylist").hide();
        }
    }

    logTime = function (text) {
        var d = new Date();
        console.log(text + " @ " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds() + "." + d.getMilliseconds());
    }

    updateResultsCount = function (total) {
        var currentPage = criteria["Page"] ? parseInt(criteria["Page"]) : 1;
        var itemsPerPage = criteria["Items"] ? criteria["Items"] : $(".rightside .pg #ItemsPerPage option:selected").val();
        var startIndex = (currentPage - 1) * itemsPerPage + 1;
        var endIndex = currentPage * itemsPerPage;
        if (endIndex > total) endIndex = total;
        //results count
        $(".rightside .resultscount .total b").text("Jobs " + startIndex + " - " + endIndex);
        $(".rightside .resultscount .total .count").text(total);
        $(".rightside .resultscount .addtofolder .action").text("Add " + (endIndex - startIndex + 1) + " jobs to a folder");
        //bottom
        $(".rightside .pg .itemsperpage .total").text("Jobs " + startIndex + " - " + endIndex + " of " + total);
        //hidden jobs count if needed
        if ($(".rightside").hasClass("BlockList"))
            $(".leftside .area.hiddenjobs .count").text("(" + $(".results .hits .total").text() + ")");

    }

    updatePagination = function () {
        var currentPage = parseInt(criteria["Page"]);
        var itemsPerPage = parseInt(criteria["Items"] ? criteria["Items"] : $(".rightside .pg #ItemsPerPage option:selected").val());
        var totalResults = parseInt($(".rightside .resultscount .total .count").text());
        var totalPages = Math.ceil(totalResults / itemsPerPage);
        var holder = $(".rightside .pagination-holder");
        var maxPages = 11, halfRange = Math.floor(maxPages / 2), firstPage, lastPage;
        if (currentPage - halfRange <= 1) {
            firstPage = 1;
            lastPage = totalPages > maxPages ? maxPages : totalPages;
        } else {
            if (currentPage + halfRange > totalPages) {
                lastPage = totalPages;
            } else {
                lastPage = currentPage + halfRange;
            }
            firstPage = currentPage - halfRange > lastPage - maxPages + 1 ? lastPage - maxPages + 1 : currentPage - halfRange;
        }

        //items per page
        $(".rightside .pg #ItemsPerPage").find("options:selected").removeAttr("selected").end().find("option[value='" + itemsPerPage + "']").attr("selected", "selected");
        //first & previous
        if (currentPage == 1) {
            holder.find("div.page.first, div.page.previous").addClass("active");
            holder.find("a.page.first, a.page.previous").removeClass("active");
        } else {
            holder.find("div.page.first, div.page.previous").removeClass("active");
            holder.find("a.page.first, a.page.previous").addClass("active");
        }
        holder.find("div.page.previous, a.page.previous").attr("page", currentPage - 1);
        //pages and current
        if (firstPage > 1) holder.find(".pagination-ellipsis.left").addClass("active");
        else holder.find(".pagination-ellipsis.left").removeClass("active");
        var pages = lastPage - firstPage + 1;
        if (holder.find(".page-numbers a.page").length == 0)
            $("<a class='page'>1</a>").insertBefore(holder.find(".pagination-ellipsis.right"));
        if (holder.find(".page-numbers div.page").length == 0)
            $("<div class='page current'>1</div>").insertBefore(holder.find(".pagination-ellipsis.right"));
        var links = holder.find(".page-numbers a.page");
        for (var i = 0; i < pages; i++) {
            var currentLink;
            if (i > links.length - 1)
                currentLink = $(links[0]).clone(true).insertBefore(holder.find(".pagination-ellipsis.right"));
            else currentLink = $(links[i]);
            $(currentLink).removeClass("current").addClass("active").attr("page", firstPage + i).text(firstPage + i);
            if (currentPage == firstPage + i)
                holder.find(".page-numbers div.page").text(currentPage).insertBefore($(currentLink).addClass("current").removeClass("active"));
        }
        for (var i = pages; i < links.length; i++)
            $(links[i]).remove();
        if (lastPage < totalPages) holder.find(".pagination-ellipsis.right").addClass("active");
        else holder.find(".pagination-ellipsis.right").removeClass("active");
        //next & last
        if (currentPage == totalPages) {
            holder.find("div.page.last, div.page.next").addClass("active");
            holder.find("a.page.last, a.page.next").removeClass("active");
        } else {
            holder.find("div.page.last, div.page.next").removeClass("active");
            holder.find("a.page.last, a.page.next").addClass("active");
        }
        holder.find("div.page.next, a.page.next").attr("page", currentPage + 1).end().find("div.page.last, a.page.last").attr("page", totalPages);
        //href (for both browseResult and searchResult)
        var baseUrl = holder.find("a.first").attr("href");
        var browseResult = $(".rightside").hasClass("BrowseResult")
        holder.find("a.page:not(.first)").each(function () {
            var page = parseInt($(this).attr("page"));
            if (browseResult) {
                if (page > 1) $(this).attr("href", baseUrl + "/" + page);
                else $(this).attr("href", baseUrl);
            } else {
                $(this).attr("href", baseUrl);
            }
        })
    }

    updateIndustryHits = function (hits) {
        $(".leftside .area.filter .section.industry .industry .count").text("(0)");
        for (var i in hits)
            if (hits.hasOwnProperty(i))
                $(".leftside .area.filter .section.industry #" + i + " .count").text("(" + hits[i] + ")");
    }

    updateJobTypeHits = function (hits) {
        for (var t in hits)
            if (hits.hasOwnProperty(t))
                $(".leftside .area.filter .section.jobtypes ." + t + " .count").text("(" + hits[t] + ")");
    }

    updateJsonData = function (data) {
        //current rows count
        var rows = $(".rightside .results .row");
        $.each(data.JobAds, function (index, jobAd) {
            var currentRow;
            if (index > rows.length - 1) {
                currentRow = $(rows[0]).clone(true).appendTo($(".rightside .results"));
            } else {
                currentRow = $(rows[index]);
            }
            currentRow.removeClass("empty hid");
            //ID
            currentRow.attr("id", jobAd.JobAdId);
            //IsNew
            if (jobAd.IsNew)
                currentRow.find(".column.tick .icon.new").addClass("active");
            else
                currentRow.find(".column.tick .icon.new").removeClass("active");
            //Featured
            if (jobAd.IsHighlighted)
                currentRow.addClass("featured");
            else
                currentRow.removeClass("featured");
            //JobTypes
            currentRow.find(".column.jobtype .types").attr("jobtypes", jobAd.JobTypes);
            //Title
            var title = currentRow.find(".column.title .title");
            title.attr("href", jobAd.JobAdUrl);
            title.attr("title", jobAd.Title);
            title.text(jobAd.Title);
            //Company
            var company = currentRow.find(".column.title .company");
            var contactDetails = typeof jobAd.ContactDetails == "undefined" ? "" : jobAd.ContactDetails
            company.attr("title", contactDetails);
            company.text(contactDetails);
            //Status (Viewed/Applied)
            if (jobAd.HasViewed)
                currentRow.find(".column.info .icon.viewed").addClass("active");
            else currentRow.find(".column.info .icon.viewed").removeClass("active");
            if (jobAd.HasApplied)
                currentRow.find(".column.info .icon.applied").addClass("active");
            else currentRow.find(".column.info .icon.applied").removeClass("active");
            //Location
            currentRow.find(".location").attr("title", jobAd.Location).text(jobAd.Location);
            //Salary
            currentRow.find(".salary").text(jobAd.Salary);
            //Date
            currentRow.find(".date").text(jobAd.CreatedTime);
            //Flag
            if (jobAd.IsFlagged)
                currentRow.find(".column.flag .flag").addClass("flagged");
            else currentRow.find(".column.flag .flag").removeClass("flagged");
            //BulletPoints
            if (jobAd.BulletPoints && $.isArray(jobAd.BulletPoints)) {
                var bp = currentRow.find(".details .bulletpoints").html("");
                $.each(jobAd.BulletPoints, function () {
                    bp.append("<li>" + this + "</li>");
                });
            }
            //industry
            if (jobAd.Industries && $.isArray(jobAd.Industries)) {
                var jobIndustries = $.map(jobAd.Industries, function (item) {
                    var index = -1;
                    for (var i = 0; i < industries.length; i++)
                        if (industries[i].Id == item) {
                            index = i;
                            break;
                        }
                    return index < 0 ? null : industries[index].Name;
                });
                currentRow.find(".details .industry .desc").text(jobIndustries.join(", "));
            }
            //Description (without ellipsis)
            currentRow.find(".details .description").text(jobAd.Content);
        });
        for (var i = data.JobAds.length; i < rows.length; i++)
            $(rows[i]).addClass("empty");
    }

    updateJobAdItemData = function () {
        //primary job type and sub job types
        $(".rightside .results .row:not(.empty) .column.jobtype").each(function () {
            var column = $(this);
            var icon = $(this).find(".icon.types");
            var jobTypesInAds = icon.attr("jobtypes").replace(/ /gi, "").split(",");
            var jobTyepsInCriteria = (criteria["JobTypes"] == undefined ? "FullTime,PartTime,Contract,Temp,JobShare" : criteria["JobTypes"]).replace(/ /gi, "").split(",");
            var primaryTypeArray = $.map(jobTyepsInCriteria, function (item) {
                return $.inArray(item, jobTypesInAds) < 0 ? null : item;
            });
            var primaryType = "None";
            if (primaryTypeArray.length > 0) primaryType = primaryTypeArray[0];
            icon.attr("class", "icon types " + primaryType);
            $(this).find(".subtype").removeClass("active");
            $.each(jobTypesInAds, function (index, element) {
                if (element == primaryType) return;
                else column.find(".subtype." + element).addClass("active");
            });
        });
        //icon title
        $(".rightside .results .row .column.info .icon.viewed:not(.active)").attr("title", "You haven't viewed this job before");
        $(".rightside .results .row .column.info .icon.applied:not(.active)").attr("title", "You haven't applied for this job yet");
        $(".rightside .results .row .column.info .icon.viewed.active").attr("title", "You have previously viewed this job");
        $(".rightside .results .row .column.info .icon.applied.active").attr("title", "You have already applied for this job");
        //description ellipsis
        //logTime("start ellipsis");
        $(".rightside .results .row .details .description").ellipsis({ lines: 4 });
        //logTime("end ellipsis");
        //summary & industry
        $(".rightside .results .row .details").each(function () {
            if ($(this).find(".summary .bulletpoints li").length == 0) $(this).find(".summary").hide();
            if ($(this).find(".industry .desc").text() == "") $(this).find(".industry").hide();
        });
    }

    //set all default values in criteria
    getDefaultCriteria = function () {
        var defaultCriteria = {};
        return defaultCriteria;
    }

    //apply default values in all filters (update new display to left side filters)
    applyDefultFilter = function () {
        var loc = $(".area.search .location #Location").realValue();
        if (loc == "") $(".area.filter .section.distance").addClass("emptyloc");
        else $(".area.filter .section.distance").removeClass("emptyloc").find(".withloc .from .location").text(loc);
        $(".area.filter .section.distance .distance-slider").slider("option", "value", parseInt($(".area.filter .section.distance .distance-slider").attr("defaultindex")));
        $(".area.search .location .dropdown-item:contains('50 km')").click();
        $(".area.filter .section.jobtypes .jobtype .icon").addClass("checked");
        $(".area.filter .section.salary .salary-slider").slider("option", "values", [parseInt($(".area.filter .section.salary .salary-slider").attr("minsalary")), parseInt($(".area.filter .section.salary .salary-slider").attr("maxsalary"))]);
        $(".area.filter .section.salary .IncludeNoSalary").addClass("checked");
        $(".area.filter .section.dateposted .recency-slider").slider("option", "value", parseInt($(".area.filter .section.dateposted .recency-slider").attr("defaultindex")));
        $(".area.filter .section.industry .allindustry .checkbox").addClass("checked");
        $(".area.filter .section.industry .industry .checkbox").removeClass("checked");
        $(".area.filter .section.additional .row").each(function () {
            $(this).find("input").removeAttr("checked").slice(0, 1).attr("checked", "checked");
        });
        $(".area.filter .section:not(.jobtypes)").removeClass("expanded").addClass("collapsed").find(".titlebar .checkbox").removeClass("checked");
    }

    toFormattedDigits = function (nValue) {
        var s = nValue.toString();
        for (j = 3; j < 15; j += 4) {  // insert commas
            if ((s.length > j) && (s.length >= 4)) {
                s = s.substr(0, s.length - j) + ',' + s.substr(s.length - j);
            }
        }
        return (s);
    }

    initializeSalary = function (lowerBound, upperBound, minSalary, maxSalary, stepSalary) {

        setSalarySliderLabel = function (lowerBound, upperBound) {
            var label = "";

            if (lowerBound == minSalary && upperBound == maxSalary) label = "Any salary";
            else if (lowerBound == minSalary) label = "up to $" + toFormattedDigits(upperBound);
            else if (upperBound == maxSalary) label = "$" + toFormattedDigits(lowerBound) + "+";
            else label = "$" + toFormattedDigits(lowerBound) + " - " + "$" + toFormattedDigits(upperBound);

            $(".salary-desc center").html(label);
            //set background position
            var percentage = lowerBound / maxSalary;
            var right = upperBound / maxSalary;
            var bgPosX = 0;
            $(".salary-slider .ui-slider-range").css({
                "background-position": (bgPosX - 211 * percentage) + "px 0px",
                "left": (percentage * 100) + "%",
                "width": ((right - percentage) * 100) + "%"
            });
        }

        $(".salary-slider").slider({
            range: true,
            min: minSalary,
            max: maxSalary,
            step: stepSalary,
            values: [lowerBound, upperBound],
            slide: function (event, ui) {
                setSalarySliderLabel(ui.values[0], ui.values[1]);
            },
            stop: function (event, ui) {
                criteria["SalaryLowerBound"] = ui.values[0];
                criteria["SalaryUpperBound"] = ui.values[1];
                applyFilter(criteria);
            },
            change: function (event, ui) {
                setSalarySliderLabel(ui.values[0], ui.values[1]);
                $(".section.salary").addClass("expanded").removeClass("collapsed").find(".titlebar .checkbox").addClass("checked");
            }
        });

        setSalarySliderLabel(lowerBound, upperBound);
        if (lowerBound == minSalary) $("#SalaryLowerBound").val("");
        else $("#SalaryLowerBound").val(lowerBound);
        if (upperBound == maxSalary) $("#SalaryUpperBound").val("");
        else $("#SalaryUpperBound").val(upperBound);
    }

    initializeRecency = function (defaultRecency, minRecency, maxRecency, stepRecency, recencies) {
        var defaultIndex;
        for (var i = 0; i < recencies.length; i++)
            if (recencies[i].days == defaultRecency) {
                defaultIndex = i;
                break;
            }
        $(".recency-slider").slider({
            range: "min",
            min: minRecency,
            max: maxRecency,
            step: stepRecency,
            value: defaultIndex,
            slide: function (event, ui) {
                $(".recency-desc center").html(recencies[ui.value].label);
            },
            stop: function (event, ui) {
                criteria["Recency"] = recencies[ui.value].days;
                applyFilter(criteria);
            },
            change: function (event, ui) {
                $(".recency-desc center").html(recencies[ui.value].label);
                $("#Recency").val(recencies[ui.value].days);
                $(".section.dateposted").addClass("expanded").removeClass("collapsed").find(".titlebar .checkbox").addClass("checked");
            }
        }).attr("defaultindex", defaultIndex);

        $(".recency-desc center").html(recencies[defaultIndex].label);
        $("#Recency").val(defaultRecency);
    }

    initializeDistance = function (defaultDistance, minDistance, maxDistance, stepDistance, distances) {
        var defaultIndex;
        for (var i = 0; i < distances.length; i++)
            if (distances[i] == defaultDistance) {
                defaultIndex = i;
                break;
            }
        $(".distance-slider").slider({
            range: "min",
            min: minDistance,
            max: maxDistance,
            step: stepDistance,
            value: defaultIndex,
            slide: function (event, ui) {
                $(".distance-desc center").html(distances[ui.value] + (ui.value == (distances.length - 1) ? "+" : "") + " km");
            },
            stop: function (event, ui) {
                criteria["Distance"] = distances[ui.value];
                applyFilter(criteria);
            },
            change: function (event, ui) {
                var text = distances[ui.value] + (ui.value == (distances.length - 1) ? "+" : "") + " km";
                $(".distance-desc center").html(text);
                $(".filter #DistanceInFilter").val(distances[ui.value]);
                $(".area.search .location .dropdown-item:contains('" + text + "')").filter(function () {
                    if ($(this).text() == text) return true;
                }).click();
                $(".section.distance").addClass("expanded").removeClass("collapsed").find(".titlebar .checkbox").addClass("checked");
            }
        }).attr("defaultindex", defaultIndex);

        $(".distance-desc center").html(distances[defaultIndex] + " km");
        $(".filter #DistanceInFilter").val(defaultDistance);
    }
})(jQuery);