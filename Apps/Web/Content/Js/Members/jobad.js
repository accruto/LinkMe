(function($) {
    var currentRequest = null;
    var fileUploadData, fileUploadErrors = false;

    $(document).ready(function() {
        organiseFields();
		$(".main-content .titlebar .subtypes .icon:not(.hide)").slice(0, 1).css("margin-left", ((88 - 17 * $(".main-content .titlebar .subtypes .icon:not(.hide)").length) / 2) + "px");
        //$(".ribbons .job-title").text("Apply for: Project Manager - Retail/Supply chain/POS software solutions")
        //$(".ribbons .job-title").attr("title", $(".ribbons .job-title").text());
        if ($(".ribbons .job-title").width() > 250)
            $(".ribbons .job-title").css({
                "width": "257px",
                "margin-top": "9px",
                "height": "44px"
            }).ellipsis();
        else
            $(".ribbons .job-title").css({
                "width": "257px",
                "margin-top": "18px"
            });
        resizeAfterRibbon();
        $(".rightside .topbuttons .backbutton").click(function() {
            window.location = $(this).attr("url");
        });
        $(".careeroneapply .applybutton").click(function() {
            $.ajax({
                type: "POST",
                url: externallyAppliedUrl.unMungeUrl(),
                data: null,
                async: false,
                dataType: "json",
                contentType: "application/json",
                success: function(data, textStatus, xmlHttpRequest) {
                },
                error: function(error) {
                }
            });
            window.location = appliedPageUrl;
        });
        //browser buttons
        $(".browse-button input").change(function() {
            var fakeFilepath = $(this).val();
            var index = fakeFilepath.lastIndexOf("\\");
            var filename = fakeFilepath.substring(index + 1);
            var context = $(this).closest(".resume_field");
            $("#Resume", context).val(filename);
            $(".resume_control", context).removeClass("error").find("err-msg").remove();
            $("#FileReferenceId", context).val("");
        });
        //loggedinuserapply only
        if ($(".loggedinuserapply").length > 0) {
            initForLoggedInUserApplyArea();
        }

        //init overlays
        if ($(".CallToActionOverlays").attr("overlayfor") == "Occasional" || $(".CallToActionOverlays").attr("overlayfor") == "Casual" || $(".managedexternallyapply").length > 0)
            initOverlays();

        //AppliedForExternally (CareerOne) only
        if ($(".careeroneapply").length > 0 && ($(".CallToActionOverlays").attr("overlayfor") == "Occasional" || $(".CallToActionOverlays").attr("overlayfor") == "Casual")) {
            //show relevant overlay according to different CallToActionOverlays type
            $(".CallToActionOverlays ." + $(".CallToActionOverlays").attr("overlayfor")).dialog({
                modal: true,
                width: 751,
                closeOnEscape: false,
                resizable: false,
                position: [parseInt(($("body").width() - 751) / 2), 200],
                dialogClass: $(".CallToActionOverlays").attr("overlayfor") + "-dialog"
            }).find(".close-icon").click(function() {
                $(this).closest(".Occasional, .Casual").dialog("close");
            });
            //dialog left and right edge gradient effect
            if ($("." + $(".CallToActionOverlays").attr("overlayfor") + " .left-edge .vline, ." + $(".CallToActionOverlays").attr("overlayfor") + " .right-edge .vline").length > 0) {
                if ($.browser.msie && $.browser.version.indexOf("7") == 0) {
                    if ($(".CallToActionOverlays").attr("overlayfor") == "Occasional")
                        $("." + $(".CallToActionOverlays").attr("overlayfor") + " .left-edge .vline, ." + $(".CallToActionOverlays").attr("overlayfor") + " .right-edge .vline").height(403).gradienter();
                    else
                        $("." + $(".CallToActionOverlays").attr("overlayfor") + " .left-edge .vline, ." + $(".CallToActionOverlays").attr("overlayfor") + " .right-edge .vline").height(650).gradienter();
                } else {
                    $("." + $(".CallToActionOverlays").attr("overlayfor") + " .left-edge .vline, ." + $(".CallToActionOverlays").attr("overlayfor") + " .right-edge .vline").height($("." + $(".CallToActionOverlays").attr("overlayfor") + " > .bg").height()).gradienter();
                }
            }
            //shorten search criteria
            shortenSearchCriteria();
        }

        //ManagedExternallyApply (ATS) only
        if ($(".managedexternallyapply").length > 0) {
            $(".managedexternallyapply .loginarea > .loginbutton, .managedexternallyapply .loginarea .joinnow .link").click(function() {
                $(".managedexternallyapply .loginarea .loginfields, .managedexternallyapply .loginarea > .loginbutton, .managedexternallyapply .joinarea, .applyarea.ManagedExternally .popupsetting").toggle();
            });
            $(".managedexternallyapply .loginarea .loginfields > .loginbutton").click(function() {
                if (currentRequest) currentRequest.abort();
                var requestData = {};

                requestData["LoginId"] = $(".managedexternallyapply .loginarea .loginfields #LoginId").val() == $(".managedexternallyapply .loginarea .loginfields #LoginId").attr("data-watermark") ? "" : $(".managedexternallyapply .loginarea .loginfields #LoginId").val();
                requestData["Password"] = $(".managedexternallyapply .loginarea .loginfields #Password").val();
                currentRequest = $.post($(".managedexternallyapply .loginarea .loginfields > .loginbutton").attr("loginurl"),
					requestData,
					function(data, textStatus, xmlHttpRequest) {
					    if (data == "") {
					        showErrInfo("ManagedExternallyLogin", $(".managedexternallyapply .loginarea"));
					    } else if (data.Success) {
					        //load LoggedInUserApply Area
					        if (currentRequest) currentRequest.abort();
					        currentRequest = $.post($(".managedexternallyapply .loginarea .loginfields > .loginbutton").attr("applyareaurl"),
								null,
								function(data, textStatus, xmlHttpRequest) {
								    if (data == "") {
								        showErrInfo("ManagedExternallyLogin", $(".managedexternallyapply .loginarea"));
								    } else {
								        $(".managedexternallyapply").after(data).remove();
								        organiseFields($(".loggedinuserapply"));
								        initForLoggedInUserApplyArea();
								        resizeAfterRibbon();
								    }
								    currentRequest = null;
								}
							).error(function(error) {
							    var data = $.parseJSON(error.responseText);
							    if (!data.Success) showErrInfo("ManagedExternallyLogin", $(".managedexternallyapply .loginarea"), data.Errors);
							});
					    } else {
					        showErrInfo("ManagedExternallyLogin", $(".managedexternallyapply .loginarea"), data.Errors);
					    }
					    currentRequest = null;
					}
				).error(function(error) {
				    var data = $.parseJSON(error.responseText);
				    if (!data.Success) showErrInfo("ManagedExternallyLogin", $(".managedexternallyapply .loginarea"), data.Errors);
				});
            });
            $(".managedexternallyapply .joinarea label[for='EmailAddress']").append("<span class='username'> (this is your LinkMe username)</span>");
            $(".managedexternallyapply .joinarea .CreateProfile_field .CreateProfile_control label[for='CreateProfile']").append("<span class='learnmore'>Learn more</span>");
            $(".managedexternallyapply .joinarea .TAndC_field .TAndC_control label[for='TAndC']").append("<a class='tandc' target='_blank' href='" + $(".managedexternallyapply .joinarea .TAndC_field .TAndC_control #TAndC").attr("url") + "'>terms and conditions</a>");
            $(".managedexternallyapply .joinarea .checkbox.CreateProfile").click(function() {
                $(".managedexternallyapply .joinarea .passwordsection, .managedexternallyapply .joinarea .username").toggle();
                $(".managedexternallyapply .joinarea .apply-button").toggleClass("externalexpressapply applybutton");
                resizeAfterRibbon();
            });
            initFileUpload("ManagedExternallyJoin", $(".managedexternallyapply"));
            $(".managedexternallyapply .joinarea .apply-button").text("").click(function() {
                var hasErrors = false, applicationId, profileCompletePercentage;
                //------ !!!!! important, must be synchronous here, we need to join/upload first then apply ------
                if ($(this).hasClass("externalexpressapply")) {
                    //create profile and apply, first step - join
                    if ($(this).attr("joinsucc") != "true") {
                        if (currentRequest) currentRequest.abort();
                        currentRequest = $.ajax({
                            type: "POST",
                            url: $(this).attr("joinurl"),
                            async: false,
                            data: JSON.stringify({
                                EmailAddress: $(".managedexternallyapply #EmailAddress").val() == $(".managedexternallyapply #EmailAddress").attr("data-watermark") ? "" : $(".managedexternallyapply #EmailAddress").val(),
                                FirstName: $(".managedexternallyapply #FirstName").val() == $(".managedexternallyapply #FirstName").attr("data-watermark") ? "" : $(".managedexternallyapply #FirstName").val(),
                                LastName: $(".managedexternallyapply #LastName").val() == $(".managedexternallyapply #LastName").attr("data-watermark") ? "" : $(".managedexternallyapply #LastName").val(),
                                JoinPassword: $(".managedexternallyapply #JoinPassword").val(),
                                JoinConfirmPassword: $(".managedexternallyapply #JoinConfirmPassword").val(),
                                acceptTerms: $(".managedexternallyapply #TAndC").is(":checked")
                            }),
                            dataType: "json",
                            contentType: "application/json",
                            success: function(data, textStatus, xmlHttpRequest) {
                                if (data == "") {
                                    showErrInfo("ManagedExternallyJoin", $(".managedexternallyapply .joinarea"));
                                    hasErrors = true;
                                } else if (data.Success) {
                                    //succ, continue to next steps
                                    $(".managedexternallyapply .joinarea .apply-button").attr("joinsucc", "true");
                                } else {
                                    showErrInfo("ManagedExternallyJoin", $(".managedexternallyapply .joinarea"), data.Errors);
                                    hasErrors = true;
                                }
                                currentRequest = null;
                            },
                            error: function(error) {
                                var data = $.parseJSON(error.responseText);
                                if (!data.Success) showErrInfo("ManagedExternallyJoin", $(".managedexternallyapply .joinarea"), data.Errors);
                                hasErrors = true;
                            }
                        });
                    }
                } else {
                    //not create profile, only apply
                }
                //second step - upload
                if (!hasErrors) uploadResume("ManagedExternallyJoin", $(".managedexternallyapply .joinarea"));
                if (fileUploadErrors) hasErrors = true;
                //third step - parse
                if (!hasErrors && $(this).hasClass("externalexpressapply") && !$.browser.msie) {
                    if ($(this).attr("parsesucc") != "true")
						hasErrors = parseResume("ManagedExternallyJoin", $(".managedexternallyapply .joinarea"));
                }
				//fourth step - apply
				if (!$.browser.msie) hasErrors = externalApply();
				return !hasErrors;
            });
        }

        //ManagedInternallyApply (LinkMe) only
        if ($(".managedinternallyapply").length > 0) {
            $(".managedinternallyapply label[for='LoginId']").append("<span class='username'> (this is your LinkMe username)</span>");
            $(".managedinternallyapply .joinandloginsection .TAndC_field .TAndC_control label[for='TAndC']").append("<a class='tandc' target='_blank' href='" + $(".managedinternallyapply .joinandloginsection .TAndC_field .TAndC_control #TAndC").attr("url") + "'>terms and conditions</a>");
            $(".managedinternallyapply .joinandloginsection .radiobuttons_field .radiobutton").click(function() {
                if ($(this).val() == "SignMeUp") {
                    $(".managedinternallyapply #FirstName").closest(".field").show();
                    $(".managedinternallyapply #LastName").closest(".field").show();
                    $(".managedinternallyapply #JoinPassword").closest(".field").show();
                    $(".managedinternallyapply #JoinConfirmPassword").closest(".field").show();
                    $(".managedinternallyapply .forgotpassword").hide();
                    $(".managedinternallyapply .resume_field").show();
                    $(".managedinternallyapply .TAndC_field").show();
					$(".managedinternallyapply #CoverLetter").closest(".field").show();
                    $(".managedinternallyapply .apply-button").addClass("expressapply").removeClass("loginandapply");
                } else {
                    $(".managedinternallyapply #FirstName").closest(".field").hide();
                    $(".managedinternallyapply #LastName").closest(".field").hide();
                    $(".managedinternallyapply #JoinPassword").closest(".field").show();
                    $(".managedinternallyapply #JoinConfirmPassword").closest(".field").hide();
                    $(".managedinternallyapply .forgotpassword").show();
                    $(".managedinternallyapply .resume_field").hide();
					$(".managedinternallyapply #CoverLetter").closest(".field").hide();
                    $(".managedinternallyapply .TAndC_field").hide();
                    $(".managedinternallyapply .apply-button").addClass("loginandapply").removeClass("expressapply");
                }
                resizeAfterRibbon();
            });
            $(".managedinternallyapply .joinandloginsection .field").show();
            $(".managedinternallyapply .forgotpassword").hide();
            $(".managedinternallyapply .joinandloginsection .apply-button").text("").click(function() {
                if ($(this).hasClass("loginandapply")) {
                    if (currentRequest) currentRequest.abort();
                    var requestData = {};

                    requestData["LoginId"] = $(".managedinternallyapply #LoginId").val() == $(".managedinternallyapply #LoginId").attr("data-watermark") ? "" : $(".managedinternallyapply #LoginId").val();
                    requestData["Password"] = $(".managedinternallyapply #JoinPassword").val();
                    currentRequest = $.post($(".managedinternallyapply .loginandapply").attr("loginurl"),
						requestData,
						function(data, textStatus, xmlHttpRequest) {
						    if (data == "") {
						        showErrInfo("ManagedInternallyLogin", $(".managedinternallyapply"));
						    } else if (data.Success) {
						        //load LoggedInUserApply Area
						        if (currentRequest) currentRequest.abort();
						        currentRequest = $.post($(".managedinternallyapply .loginandapply").attr("applyareaurl"),
									null,
									function(data, textStatus, xmlHttpRequest) {
									    if (data == "") {
									        showErrInfo("ManagedInternallyLogin", $(".managedinternallyapply"));
									    } else {
									        $(".managedinternallyapply").after(data).remove();
									        organiseFields($(".loggedinuserapply"));
									        initForLoggedInUserApplyArea();
									        resizeAfterRibbon();
									    }
									    currentRequest = null;
									}
								).error(function(error) {
								    var data = $.parseJSON(error.responseText);
								    if (!data.Success) showErrInfo("ManagedInternallyLogin", $(".managedinternallyapply"), data.Errors);
								});
						    } else {
						        showErrInfo("ManagedInternallyLogin", $(".managedinternallyapply"), data.Errors);
						    }
						    currentRequest = null;
						}
					).error(function(error) {
					    var data = $.parseJSON(error.responseText);
					    if (!data.Success) showErrInfo("ManagedInternallyLogin", $(".managedinternallyapply"), data.Errors);
					});
                } else {
                    //join and express apply
                    if (currentRequest) currentRequest.abort();
                    var requestData = {};

                    requestData["EmailAddress"] = $(".managedinternallyapply #LoginId").val() == $(".managedinternallyapply #LoginId").attr("data-watermark") ? "" : $(".managedinternallyapply #LoginId").val();
                    requestData["FirstName"] = $(".managedinternallyapply #FirstName").val() == $(".managedinternallyapply #FirstName").attr("data-watermark") ? "" : $(".managedinternallyapply #FirstName").val();
                    requestData["LastName"] = $(".managedinternallyapply #LastName").val() == $(".managedinternallyapply #LastName").attr("data-watermark") ? "" : $(".managedinternallyapply #LastName").val();
                    requestData["JoinPassword"] = $(".managedinternallyapply #JoinPassword").val();
                    requestData["JoinConfirmPassword"] = $(".managedinternallyapply #JoinConfirmPassword").val();
                    requestData["acceptTerms"] = $(".managedinternallyapply #TAndC").is(":checked");
                    currentRequest = $.post($(".managedinternallyapply .expressapply").attr("joinurl"),
						requestData,
						function(data, textStatus, xmlHttpRequest) {
						    if (data == "") {
						        showErrInfo("ManagedInternallyJoin", $(".managedinternallyapply"));
						    } else if (data.Success) {
						        uploadResume("ManagedInternallyJoin", $(".managedinternallyapply"));
						    } else {
						        showErrInfo("ManagedInternallyJoin", $(".managedinternallyapply"), data.Errors);
						    }
						    currentRequest = null;
						}
					).error(function(error) {
					    var data = $.parseJSON(error.responseText);
					    if (!data.Success) showErrInfo("ManagedInternallyJoin", $(".managedinternallyapply"), data.Errors);
					});
                }
            });
            initFileUpload("ManagedInternallyJoin", $(".managedinternallyapply"));
        }

        //applied page only
        if ($(".appliedpage").length > 0 && $(".CompleteProfile").length > 0) {
            var percent = $(".CompleteProfile .progressbar .text").text();
            showCompleteProfileDialog(percent);
        }

        //closed job only
        $(".main-content.closed .togglecontent").find(".togglelink, .togglebutton").click(function() {
            $(this).parent().prev().toggle();
            $(this).parent().find(".togglelink, .togglebutton").toggleClass("collapse expanded");
            $(this).parent().find(".togglelink").text($(this).hasClass("collapse") ? "Show job description" : "Hide job description");
        });

        //call viewed api to track viewing, not for applied page
        if ($(".applyarea").length > 0) {
            if (currentRequest) currentRequest.abort();
            currentRequest = $.post($(".CallToActionOverlays").attr("viewedurl").unMungeUrl(),
				null,
				function(data, textStatus, xmlHttpRequest) {
				    currentRequest = null;
				}
			);
        }
		
		//JobAdQuestions Page
		if ($(".jobadquestions").length > 0) {
			$(".jobadquestions fieldset").removeClass("forms_v2");
			$(".field .control .dropdown").parent().dropdown();
		}

        $(".learnmore").click(function() {
            $(".learnmoredialog").dialog({
                modal: true,
                width: 741,
                height: 501,
                closeOnEscape: true,
                resizable: false,
                position: [parseInt(($("body").width() - 751) / 2), 200],
                dialogClass: "learnmore-dialog"
            }).find(".okbutton").click(function() {
                $(".learnmoredialog").dialog("close");
            });
            event.stopPropagation();
        });
		
		//action icons
		if (!($(".jobadquestions").length > 0 || $(".appliedpage").length > 0)) loadAllNotes();
		$(".main-content .titlebar:not(.notloggedin) .notes .text").click(function() {
			if ($(".menupan.note").is(":visible")) {
				$(".menupan.note").slideUp(500);
			} else {
				$(".menupan.note").attr("action", "single");
				loadNotes($(".rightside .topbuttons .actions").attr("jobadid"));
				$(".menupan.note").slideDown(500);
			}
		});
		$(".menupan.note .add .text").click(function() {
			var menupan = $(this).closest(".menupan");
			var newRow = menupan.find(".row.empty").clone(true).removeClass("empty").attr("id", "").insertAfter(menupan.find(".add"));
			newRow.find(".title, .content").hide().end().append(menupan.find("#Notes").val("").end().find(".editarea").show());
		});
		$(".menupan.note .buttons .text.edit").click(function() {
			var row = $(this).closest(".row");
			row.closest(".menupan").find(".editarea").appendTo(row).show().find("#Notes").val(row.find(".content").text());
			row.find(".title, .content").hide();
		});
		$(".menupan.note .buttons .text.delete").click(function() {
			var row = $(this).closest(".row");
			var noteId = row.attr("id");
			var menupan = row.closest(".menupan");
			row.find(".editarea").appendTo(menupan);
			var notesholder = $(".main-content .titlebar .notes");
			var url = notesholder.attr("apideletenoteurl").unMungeUrl().replace(/00000000-0000-0000-0000-000000000000/gi, noteId);
			currentRequest = $.post(url,
				null,
				function(data, textStatus, xmlHttpRequest) {
					currentRequest = null;
				}
			);
			var notes = notesholder.data("notes");
			notes = $.grep(notes, function(element, index) {
				return element.Id == noteId ? null : element;
			});
			notesholder.find(".count").text("(" + notes.length + ")").end().data("notes", notes);
			row.remove();
		});
		$(".menupan.note .button.save").click(function() {
			var notesholder = $(".main-content .titlebar .notes");
			var menupan = $(this).closest(".menupan");
			var jobAdIds = new Array();
			jobAdIds.push($(".rightside .topbuttons .actions").attr("jobadid"));
			var requestData = {}, url;
			var row = $(this).closest(".row");
			requestData["text"] = row.find("#Notes").val();
			//new note
			if (!row.attr("id") || row.attr("id") == "") {
				url = notesholder.attr("apinewnoteurl").unMungeUrl();
				requestData["jobAdIds"] = jobAdIds;
			} else { //editing note
				url = notesholder.attr("apieditnoteurl").unMungeUrl().replace(/00000000-0000-0000-0000-000000000000/gi, row.attr("id"));
			}
			currentRequest = $.post(url,
				requestData,
				function(data, textStatus, xmlHttpRequest) {
					row.find(".title").show().end().find(".title .date").text(getNoteUpdateTime(new Date())).end().find(".content").text(requestData["text"]).show().end().find(".editarea").hide();
					var notes = notesholder.data("notes");
					if (!row.attr("id") || row.attr("id") == "") {
						notes.unshift({ Id : data.Id, Text : requestData["text"], UpdateTime : new Date() });
					} else notes = $.map(notes, function(item) {
						if (item.Id == row.attr("id")) item.Text = requestData["text"];
						return item;
					});
					notesholder.find(".count").text("(" + notes.length + ")").end().data("notes", notes);
					if (!row.attr("id") || row.attr("id") == "") row.attr("id", data.Id);
					currentRequest = null;
				}
			);
		});
		$(".menupan.note .button.cancel").click(function() {
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
		});
		$(".rightside .topbuttons .actions:not(.notloggedin) .icon.folder").click(function() {
            if ($(this).hasClass("selected")) {
                $(this).closest(".topbuttons").find(".menupan.folder").slideUp(500);
                $(this).removeClass("selected");
            } else {
                $(this).closest(".topbuttons").find(".menupan.folder").slideDown(500).end().find(".emaillayer").hide().end().find(".actions .icon").removeClass("selected");
                $(this).addClass("selected");
            }
		});
		$(".rightside .menuitem.folder").click(function() {
			var folderId = $(this).attr("folderid");
			var menupan = $(this).closest(".menupan");
			var jobAdId = menupan.closest(".topbuttons").find(".actions").attr("jobadid"), jobAdIds = new Array();
			jobAdIds.push(jobAdId);
			var requestData = {};
			requestData["jobAdIds"] = jobAdIds;
            currentRequest = $.post($(this).attr("url").unMungeUrl(),
				requestData,
				function(data, textStatus, xmlHttpRequest) {
					$(".menupan.folder").slideUp(500).closest(".topbuttons").find(".actions .icon.folder").removeClass("selected");
					$(".rightside .results #" + jobAdId).removeClass("hover");
				    currentRequest = null;
				}
			);
		});
		$(".rightside .topbuttons .actions .icon.print").click(function() {
			window.print();
		});
        $(".rightside .topbuttons .actions .icon.email").click(function() {
            $(".emaillayer .validationerror").hide();
            $(".emaillayer .succ-info").hide();
            $(this).closest(".topbuttons").find(".emaillayer .control").removeClass("error").closest(".field").find(".error-msg").remove();
            if ($(this).hasClass("selected")) {
                $(this).closest(".topbuttons").find(".emaillayer").slideUp(500);
                $(this).removeClass("selected");
            } else {
                $(this).closest(".topbuttons").find(".emaillayer").slideDown(500).end().find(".menupan.folder").hide().end().find(".actions .icon").removeClass("selected");
                $(".rightside .emaillayer .sendbutton").removeAttr("sending");
                $(this).addClass("selected");
            }
        });
        $("#FriendsName").closest(".field").append("<div class='help'>(use commas to separate multiple names)</div>");
        $("#FriendsEmailAddress").closest(".field").append("<div class='help'>(use commas to separate multiple emails)</div>");
        $(".rightside .emaillayer .sendbutton").click(function() {
            if ($(this).attr("sending") == "true") return false;

            $(this).attr("sending", "true");
            if (currentRequest)
                currentRequest.abort();

            var toNamesValue = $("#ToNames").val() == $("#ToNames").attr("data-watermark") ? "" : $("#ToNames").val();
            var toEmailAddressesValue = $("#ToEmailAddresses").val() == $("#ToEmailAddresses").attr("data-watermark") ? "" : $("#ToEmailAddresses").val();
            var toNames = toNamesValue.split(",");
            var toEmailAddresses = toEmailAddressesValue.split(",");
            if (toNames.length != toEmailAddresses.length) {
                showErrInfo("notmatch", $(".emaillayer"), null, "The numbers (" + toNames.length + ") of your friend's name(s) doesn't match the numbers (" + toEmailAddresses.length + ") of your friend's email address(es).");
                return false;
            }
            var tos = new Array();
            if (toNamesValue != "")
                $.each(toNames, function(index, item) {
                    var pair = {};
                    pair["ToName"] = item;
                    pair["ToEmailAddress"] = toEmailAddresses[index];
                    tos.push(pair);
                });

            currentRequest = $.ajax({
                type: "POST",
                url: $(this).attr("url").unMungeUrl(),
                data: JSON.stringify({
                    FromName: $("#FromName").val(),
                    FromEmailAddress: $("#FromEmailAddress").val(),
                    Tos: tos,
					JobAdIds : $(".rightside .topbuttons .actions").attr("jobadid")
                }),
                dataType: "json",
                contentType: "application/json",
                success: function(data, textStatus, xmlHttpRequest) {
                    if (data == "") {
                        showErrInfo("send", $(".emaillayer"));
                    } else if (data.Success) {
                        showSuccInfo("send", $(".emaillayer"), "");
                    } else {
                        showErrInfo("send", $(".emaillayer"), data.Errors);
                    }
                    currentRequest = null;
                },
                error: function(error) {
                    var data = $.parseJSON(error.responseText);
                    if (!data.Success) showErrInfo("send", $(".emaillayer"), data.Errors);
                }
            });
            return false;
        });
        $(".rightside .emaillayer .cancelbutton").click(function() {
            $(this).closest(".emaillayer").slideUp(500).parent().find(".actions .icon.email").removeClass("selected");
        });
		$(".rightside .topbuttons .actions .icon.download").click(function() {
			var select = $("<select name='jobAdIds' multiple='multiple'></select>");
			select.append("<option value='" + $(".rightside .topbuttons .actions").attr("jobadid") + "' selected='selected'></option>");
			var form = $("<form action='" + $(this).attr("url").unMungeUrl() + "' method='post'></form>");
			form.append(select).appendTo($("body")).submit().remove();
		});
		$(".rightside .topbuttons .actions:not(.notloggedin) .icon.hide").click(function() {
			var jobAdIds = new Array();
			jobAdIds.push($(this).closest(".actions").attr("jobadid"));
			hide("hide", jobAdIds);
		});
		
		$(".suggestedjob").click(function() {
			window.location = $(this).attr("url");
		});
    });

	//fourth step -- apply (for ManagedExternallyApply)
	externalApply = function() {
		var hasErrors = false;
		//fourth step - apply
		//do something here to get the applicationId
		var fileReferenceId = $(".managedexternallyapply #FileReferenceId").val();
		var applyurl = null;
		if ($(".managedexternallyapply .apply-button").hasClass("externalexpressapply")) {
			if (fileReferenceId == "")
				applyurl = $(".managedexternallyapply .apply-button").attr("applywithprofileurl");
			else
				applyurl = $(".managedexternallyapply .apply-button").attr("applywithuploadedresumeurl");
		}
		else {
			applyurl = $(".managedexternallyapply .apply-button").attr("apiapplyurl");
		}
		currentRequest = $.ajax({
			type: "POST",
			url: applyurl,
			async: false,
			data: JSON.stringify({
				fileReferenceId: fileReferenceId,
				useForProfile: true,
				FirstName: $(".managedexternallyapply #FirstName").val(),
				LastName: $(".managedexternallyapply #LastName").val(),
				EmailAddress: $(".managedexternallyapply #EmailAddress").val()
			}),
			dataType: "json",
			contentType: "application/json",
			success: function(data, textStatus, xmlHttpRequest) {
				if (data == "") {
					showErrInfo("ManagedExternallyJoin", $(".managedexternallyapply .joinarea"));
					hasErrors = true;
				} else if (data.Success) {
					applicationId = data.Id;
				} else {
					showErrInfo("ManagedExternallyJoin", $(".managedexternallyapply .joinarea"), data.Errors);
					hasErrors = true;
				}
				currentRequest = null;
			},
			error: function(error) {
				var data = $.parseJSON(error.responseText);
				if (!data.Success) showErrInfo("ManagedExternallyJoin", $(".managedexternallyapply .joinarea"), data.Errors);
				hasErrors = true;
			}
		});
		if (hasErrors) return hasErrors;
		else {
			var href = $(".managedexternallyapply .apply-button").attr("href");
			if (href != null) {
				if (href.indexOf("?") > 0)
					href = href + "&applicationId=" + applicationId;
				else
					href = href + "?applicationId=" + applicationId;
				$(".managedexternallyapply .apply-button").attr("href", href);
			}
			var appliedurl = $(".managedexternallyapply .apply-button").attr("appliedurl");
			if (appliedurl.indexOf("?") > 0)
				appliedurl = appliedurl + "&applicationId=" + applicationId;
			else
				appliedurl = appliedurl + "?applicationId=" + applicationId;
			window.location = appliedurl;
		}
	}

	hide = function(type, jobAdIds) {
		var requestData = {}, url;
		if (type == "hide" || type == "unhide") requestData["jobAdIds"] = jobAdIds;
		if (type == "hide") url = $(".rightside .topbuttons .actions .icon.hide").attr("apiblockjobadsurl").unMungeUrl();
		else if (type == "unhide") url = apiUnblockJobAdsUrl;
		else url = apiUnflagAllJobAdsUrl;
		currentRequest = $.post(url,
			requestData,
			function(data, textStatus, xmlHttpRequest) {
				currentRequest = null;
			}
		);
	}
	
	getNoteUpdateTime = function(date) {
		var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
		return date.getDate() + "-" + months[date.getMonth()] + "-" + date.getFullYear()
	}
	
	loadNotes = function(jobAdId) {
		//load notes from db if need
		var notesholder = $(".main-content .titlebar .notes");
		if (notesholder.attr("notesloaded") != "true") {
			currentRequest = $.ajax({
				type : "POST",
				url : apiNotesUrl,
				async : false,
				data: JSON.stringify({
					jobAdId : jobAdId
				}),
				dataType: "json",
				contentType: "application/json",
				success: function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
					} else if (data.Success) {
						notesholder.data("notes", data.Notes);
						$.each(data.Notes, function(index, element) {
							element.UpdateTime = new Date(parseInt(element.UpdatedTime.substring(6)));
						});
						notesholder.attr("notesloaded", "true");
						notesholder.find(".count").text("(" + data.Notes.length + ")");
					} else {
					}
				},
				error: function(error) {
				}
			});
		}
		var menupan = $(".menupan.note");
		menupan.find(".editarea").appendTo(menupan);
		menupan.find(".row:not(.empty)").remove();
		$.each(notesholder.data("notes"), function(index, element) {
			var newRow = menupan.find(".row.empty").clone(true).removeClass("empty");
			newRow.attr("id", element.Id);
			newRow.find(".title .date").text(getNoteUpdateTime(element.UpdateTime)).end().find(".content").text(element.Text).end().appendTo(menupan);
		});
	}
	
	loadAllNotes = function() {
		if ($(".main-content .titlebar").hasClass("notloggedin")) return;
		//load all notes from db
		var jobAdId = $(".rightside .topbuttons .actions").attr("jobadid");
		var notesholder = $(".main-content .titlebar .notes");
		if (notesholder.attr("notesloaded") != "true") {
			currentRequest = $.ajax({
				type : "POST",
				url : notesholder.attr("apinotesurl").unMungeUrl(),
				async : false,
				data: JSON.stringify({
					jobAdId : jobAdId
				}),
				dataType: "json",
				contentType: "application/json",
				success: function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
					} else if (data.Success) {
						notesholder.data("notes", data.Notes);
						$.each(data.Notes, function(index, element) {
							element.UpdateTime = new Date(parseInt(element.UpdatedTime.substring(6)));
						});
						notesholder.attr("notesloaded", "true");
						notesholder.find(".count").text("(" + data.Notes.length + ")");
					} else {
					}
				},
				error: function(error) {
				}
			});
		}
	}

    initOverlays = function() {
        $(".Occasional .left-side .field .checkbox_control label[for='CreateProfile']").append("<span class='learnmore'>Learn more</span>");
        $(".checkbox.AlreadyHaveProfile").click(function() {
            $(this).closest(".left-side").find(".withoutprofile, .withprofile").toggle();
        });
        $(".Occasional .left-side .loginandsendmejobsbyemail, .Occasional .left-side .sendmejobsbyemail").click(function() {
            var hasErrors = false;
            if ($(this).hasClass("loginandsendmejobsbyemail")) {
                //login first then create email alert
                if (currentRequest) currentRequest.abort();
                currentRequest = $.ajax({
                    type: "POST",
                    url: $(this).attr("loginurl"),
                    async: false,
                    data: JSON.stringify({
                        LoginId: $(".Occasional #Username").val() == $(".Occasional #Username").attr("data-watermark") ? "" : $(".Occasional #Username").val(),
                        Password: $(".Occasional #Password").val()
                    }),
                    dataType: "json",
                    contentType: "application/json",
                    succ: function(data, textStatus, xmlHttpRequest) {
                        if (data == "") {
                            showErrInfo("OccasionalLogin", $(".Occasional"));
                            hasErrors = true;
                        } else if (data.Success) {

                        } else if (data.Errors.Code != "104") {
                            showErrInfo("OccasionalLogin", $(".Occasional"), data.Errors);
                            hasErrors = true;
                        }
                        currentRequest = null;
                    },
                    error: function(error) {
                        var data = $.parseJSON(error.responseText);
                        if (!data.Success) showErrInfo("OccasionalLogin", $(".Occasional"), data.Errors);
                        hasErrors = true;
                    }
                });
                if (!hasErrors) {
                    if (currentRequest) currentRequest.abort();
                    currentRequest = $.ajax({
                        type: "POST",
                        url: $(this).attr("saveurl"),
                        async: false,
                        dataType: "json",
                        contentType: "application/json",
                        success: function(data, textStatus, xmlHttpRequest) {
                            if (data == "") {
                                showErrInfo("OccasionalJoin", $(".Occasional"));
                                hasErrors = true;
                            } else if (data.Success) {
                            } else {
                                showErrInfo("OccasionalJoin", $(".Occasional"), data.Errors);
                                hasErrors = true;
                            }
                            currentRequest = null;
                        },
                        error: function(error) {
                            var data = $.parseJSON(error.responseText);
                            if (!data.Success) showErrInfo("OccasionalJoin", $(".Occasional"), data.Errors);
                            hasErrors = true;
                        }
                    });
                }
            } else {
                //create email alert for anonymous user				
                if ($(this).attr("savesucc") != "true") {
                    if (currentRequest) currentRequest.abort();
                    currentRequest = $.ajax({
                        type: "POST",
                        url: $(this).attr("saveurl"),
                        async: false,
                        data: JSON.stringify({
                            EmailAddress: $(".Occasional #EmailAddress").val() == $(".Occasional #EmailAddress").attr("data-watermark") ? "" : $(".Occasional #EmailAddress").val(),
                            FirstName: $(".Occasional #FirstName").val() == $(".Occasional #FirstName").attr("data-watermark") ? "" : $(".Occasional #FirstName").val(),
                            LastName: $(".Occasional #LastName").val() == $(".Occasional #LastName").attr("data-watermark") ? "" : $(".Occasional #LastName").val()
                        }),
                        dataType: "json",
                        contentType: "application/json",
                        success: function(data, textStatus, xmlHttpRequest) {
                            if (data == "") {
                                showErrInfo("OccasionalJoin", $(".Occasional"));
                                hasErrors = true;
                            } else if (data.Success) {
                                //succ, continue to next steps
                                $(".Occasional .sendmejobsbyemail").attr("savesucc", "true");
                            } else {
                                showErrInfo("OccasionalJoin", $(".Occasional"), data.Errors);
                                hasErrors = true;
                            }
                            currentRequest = null;
                        },
                        error: function(error) {
                            var data = $.parseJSON(error.responseText);
                            if (!data.Success) showErrInfo("OccasionalJoin", $(".Occasional"), data.Errors);
                            hasErrors = true;
                        }
                    });
                }
            }
            if (!hasErrors) alertCreated();
        });
        $(".Casual .left-side .field > label[for='TAndC']").text("Terms and conditions");
        $(".Casual .left-side .field .checkbox_control label[for='TAndC']").append("<a class='tandc' target='_blank' href='" + $("#TAndC").attr("url") + "'>terms and conditions</a>");
        $(".Casual .createmylinkmeprofile").click(function() {
            //sync create profile
            var hasErrors = false, applicationId;
            //------ !!!!! important, must be synchronous here, we need to join/upload first then apply ------
            //create profile and apply, first step - join
            if ($(this).attr("joinsucc") != "true") {
                if (currentRequest) currentRequest.abort();
                currentRequest = $.ajax({
                    type: "POST",
                    url: $(this).attr("joinurl"),
                    async: false,
                    data: JSON.stringify({
                        EmailAddress: $(".Casual #JoinEmailAddress").val() == $(".Casual #JoinEmailAddress").attr("data-watermark") ? "" : $(".Casual #JoinEmailAddress").val(),
                        FirstName: $(".Casual #JoinFirstName").val() == $(".Casual #JoinFirstName").attr("data-watermark") ? "" : $(".Casual #JoinFirstName").val(),
                        LastName: $(".Casual #JoinLastName").val() == $(".Casual #JoinLastName").attr("data-watermark") ? "" : $(".Casual #JoinLastName").val(),
                        JoinPassword: $(".Casual #JoinPassword").val(),
                        JoinConfirmPassword: $(".Casual #JoinConfirmPassword").val(),
                        acceptTerms: $(".Casual #TAndC").is(":checked")
                    }),
                    dataType: "json",
                    contentType: "application/json",
                    success: function(data, textStatus, xmlHttpRequest) {
                        if (data == "") {
                            showErrInfo("Casual", $(".Casual"));
                            hasErrors = true;
                        } else if (data.Success) {
                            //succ, continue to next steps
                            $(".Casual .createmylinkmeprofile").attr("joinsucc", "true");
                        } else {
                            showErrInfo("Casual", $(".Casual"), data.Errors);
                            hasErrors = true;
                        }
                        currentRequest = null;
                    },
                    error: function(error) {
                        var data = $.parseJSON(error.responseText);
                        if (!data.Success) showErrInfo("Casual", $(".Casual"), data.Errors);
                        hasErrors = true;
                    }
                });
            }
            //second step - upload
            if (!hasErrors) uploadResume("Casual", $(".Casual"));
            if (fileUploadErrors) hasErrors = true;
            //third step - parse
            if (!hasErrors && !$.browser.msie) {
                if ($(this).attr("parsesucc") != "true") {
                    parseResume("Casual", $(".Casual"));
                }
            }
        });
        initFileUpload("Casual", $(".Casual"));
    }

    parseResume = function(type, context) {
		var hasErrors;
        var parseUrl = "";
        if (type == "Casual") parseUrl = $(".createmylinkmeprofile", context).attr("parseurl")
		if (type == "ManagedExternallyJoin") parseUrl = $(".apply-button", context).attr("parseurl");
        if (currentRequest) currentRequest.abort();
        currentRequest = $.ajax({
            type: "POST",
            url: parseUrl,
            async: false,
            data: JSON.stringify({
                fileReferenceId: $("#FileReferenceId", context).val()
            }),
            dataType: "json",
            contentType: "application/json",
            success: function(data, textStatus, xmlHttpRequest) {
                if (data == "") {
                    showErrInfo(type, context);
                    hasErrors = true;
                } else if (data.Success) {
                    //succ, continue to next step
                    if (type == "Casual") {
						var profileCompletePercentage = data.Profile.PercentComplete;
                        $(".Casual .createmylinkmeprofile").attr("parsesucc", "true");
                        $(".Casual").dialog("close");
                        showCompleteProfileDialog(profileCompletePercentage);
                    }
					if (type == "ManagedExternallyJoin") hasErrors = externalApply();
                } else {
                    showErrInfo(type, context, data.Errors);
                    hasErrors = true;
                }
                currentRequest = null;
            },
            error: function(error) {
                var data = $.parseJSON(error.responseText);
                if (!data.Success) showErrInfo(type, context, data.Errors);
                hasErrors = true;
            }
        });
		return hasErrors;
    }

    initFileUpload = function(type, context) {
        var asyncOption = true;
        if (type == "ManagedExternallyJoin" || type == "Casual") asyncOption = false;
        //file browse
        $("#resumeupload", context).fileupload({
            url: $("#resumeupload", context).attr("url"),
            type: "POST",
            dataType: "json",
            namespace: "linkme." + type + ".resume.upload",
            fileInput: $("#resumeupload .browse-button input[type='file']", context),
            replaceFileInput: false,
            limitMultiFileUploads: 1,
            async: asyncOption,
            add: function(e, data) {
                fileUploadData = data;
            },
            progress: function(e, data) {
            },
            done: function(e, data) {
                if (data.result.Success) {
                    fileUploadErrors = false;
                    $("#FileReferenceId", context).val(data.result.Id);
                    if (type == "LoggedInUserApply") loggedInUserApply();
                    if (type == "ManagedInternallyJoin") managedInternallyExpressApply();
                    if ((type == "Casual" || (type == "ManagedExternallyJoin" && $(".apply-button").hasClass("externalexpressapply"))) && $.browser.msie) parseResume(type, context);
					if (type == "ManagedExternallyJoin" && $(".apply-button").hasClass("applybutton")) externalApply();
                }
            },
            fail: function(e, data) {
                fileUploadErrors = true;
                showErrInfo(type, context, data);
            },
            start: function(e, data) {
            },
            stop: function(e, data) {
            }
        });
    }

    initForLoggedInUserApplyArea = function() {
        $(".loggedinuserapply #Uploaded").closest(".radio_control").after($(".loggedinuserapply .uploadsection"));
        $(".loggedinuserapply label[for='Profile']").append($(".loggedinuserapply .reviewprofile"));
        $(".loggedinuserapply label[for='LastUsed']").append($(".loggedinuserapply .lastusedresumefile"));
        if ($(".loggedinuserapply .radiobutton[value='Uploaded']").hasClass("checked")) $(".loggedinuserapply .uploadsection").show();
        else $(".loggedinuserapply .uploadsection").hide();
        resizeAfterRibbon();
        $(".loggedinuserapply .radiobutton").click(function() {
            if ($(this).hasClass("disabled")) return;
            if ($(this).val() == "Uploaded") $(".loggedinuserapply .uploadsection").show();
            else $(".loggedinuserapply .uploadsection").hide();
            resizeAfterRibbon();
        });
        $("#CoverLetter").attr("charsPerLine", "35");
        $(".loggedinuserapply .expressapply").click(function() {
            if ($(".loggedinuserapply .radiobutton.checked").val() == "Uploaded") {
                //overwrite?
                if ($(".loggedinuserapply .checkbox.Overwrite").hasClass("checked")) {
                    $(".overwrite-overlay").dialog({
                        modal: true,
                        width: 560,
                        closeOnEscape: false,
                        resizable: false,
                        dialogClass: "overwrite-overlay-dialog",
                        title: "Overwrite LinkMe resume"
                    }).find(".yes, .cancel").unbind("click").click(function() {
                        $(".overwrite-overlay").dialog("close");
                        if ($(this).hasClass("yes")) uploadResume("LoggedInUserApply", $(".loggedinuserapply"));
                    });
                } else uploadResume("LoggedInUserApply", $(".loggedinuserapply"));
            } else loggedInUserApply();
        });
        $(".loggedinuserapply .externalexpressapply").text("").click(function() {
            window.location = $(this).attr("appliedurl");
        });
        initFileUpload("LoggedInUserApply", $(".loggedinuserapply"));
    }

    uploadResume = function(type, context) {
        //upload if FileReferenceId is null
        if ($("#FileReferenceId", context).val() == "") {
            if (!(fileUploadData && fileUploadData.files.length > 0)) {
                showErrInfo(type, context, [{
                    "Key": "Resume",
                    "Message": "You should browse a resume file from your computer."
				}]);
				fileUploadErrors = true;
				return;
			}
			var fileSize = fileUploadData.files[0].size;
			var fileType = fileUploadData.files[0].type;
			if ($.browser.msie || fileType == "") {
				var filename = fileUploadData.files[0].name;
				var ext = filename.substring(filename.lastIndexOf("."));
				switch (ext) {
					case ".doc":
						fileType = "application/msword";
						break;
					case ".docx":
						fileType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
						break;
					case ".rtf":
						fileType = "application/rtf";
						break;
					case ".pdf":
						fileType = "application/pdf";
						break;
					case ".txt":
						fileType = "text/plain";
						break;
					case ".htm":
					case ".html":
						fileType = "text/html";
						break;
				}
			}
			if (fileSize > 2097152) {
				showErrInfo(type, context, [{
					"Key": "Resume",
					"Message": "Your resume is too big. Please try another one. 2MB max size."
				}]);
				fileUploadErrors = true;
				return;
			}
			if (!(fileType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileType == "application/msword" || fileType == "application/rtf" || fileType == "application/pdf" || fileType == "text/plain" || fileType == "text/html")) {
				showErrInfo(type, context, [{
					"Key": "Resume",
					"Message": "The uploaded file type is not supported. Please convert to one of these formats: MS Word, RTF, DOC, DOCX, Text or HTML. 2MB max size."
				}]);
				fileUploadErrors = true;
				return;
			}
			fileUploadData.submit();
		} else {
			if (type == "LoggedInUserApply") loggedInUserApply();
			else if (type == "ManagedInternallyJoin") managedInternallyExpressApply();
		}
	}

	loggedInUserApply = function() {
		if (currentRequest) currentRequest.abort();
		var requestData = {};

		var url;
		var resumeSource = $(".loggedinuserapply .radiobutton.checked").val();
		if (resumeSource == "LastUsed") {
			url = $(".loggedinuserapply .expressapply").attr("applywithlastusedresumeurl");
		}
		else if (resumeSource == "Uploaded") {
			url = $(".loggedinuserapply .expressapply").attr("applywithuploadedresumeurl");
		}
		else {
			url = $(".loggedinuserapply .expressapply").attr("applywithprofileurl");
		}

		requestData["coverLetterText"] = $(".loggedinuserapply #CoverLetter").val();
		if (resumeSource == "Uploaded") {
			requestData["fileReferenceId"] = $(".loggedinuserapply #FileReferenceId").val();
			requestData["useForProfile"] = $(".loggedinuserapply .checkbox.Overwrite").hasClass("checked");
		}
		currentRequest = $.post(url,
			requestData,
			function(data, textStatus, xmlHttpRequest) {
				if (data == "") {
					showErrInfo("LoggedInUserApply", $(".loggedinuserapply"));
					return;
				} else if (data.Success) {
					var appliedUrl = $(".loggedinuserapply .expressapply").attr("appliedurl");
					if (appliedUrl.indexOf("?") > 0) appliedUrl = appliedUrl + "&applicationId=" + data.Id;
					else appliedUrl = appliedUrl + "?applicationId=" + data.Id;
					window.location.href = appliedUrl;
				} else {
					showErrInfo("LoggedInUserApply", $(".loggedinuserapply"), data.Errors);
				}
				currentRequest = null;
			}).error(function(error) {
				var data = $.parseJSON(error.responseText);
				if (!data.Success) showErrInfo("LoggedInUserApply", $(".loggedinuserapply"), data.Errors);
			});
	}

	managedInternallyExpressApply = function() {
		if (currentRequest) currentRequest.abort();
		var requestData = {};

		var resumeSource = "Uploaded";
		var url = $(".managedinternallyapply .expressapply").attr("applywithuploadedresumeurl");

		requestData["coverLetterText"] = $(".managedinternallyapply #CoverLetter").val();;
		requestData["fileReferenceId"] = $(".managedinternallyapply #FileReferenceId").val();
		requestData["useForProfile"] = true;

		currentRequest = $.post(url,
			requestData,
			function(data, textStatus, xmlHttpRequest) {
				if (data == "") {
					showErrInfo("ManagedInternallyJoin", $(".managedinternallyapply"));
					return;
				} else if (data.Success) {
					var url = $(".managedinternallyapply .expressapply").attr("appliedurl");
					if (url.indexOf("?") > 0) url = url + "&showOverlay=true";
					else url = url + "?showOverlay=true";
					window.location.href = url;
				} else {
					showErrInfo("ManagedInternallyJoin", $(".loggedinuserapply"), data.Errors);
				}
				currentRequest = null;
			}
		).error(function(error) {
			var data = $.parseJSON(error.responseText);
			if (!data.Success) showErrInfo("ManagedInternallyJoin", $(".managedinternallyapply"), data.Errors);
		});
	}

	showCompleteProfileDialog = function(profilePercent) {
		$(".CompleteProfile").dialog({
			modal: true,
			width: 751,
			closeOnEscape: false,
			resizable: false,
			position: [parseInt(($("body").width() - 751) / 2), 200],
			dialogClass: "CompleteProfile-dialog"
		}).find(".close-icon").click(function() {
			$(this).closest(".CompleteProfile").dialog("close");
		});
		if ($(".CompleteProfile .left-edge .vline, .CompleteProfile .right-edge .vline").length > 0)
			$(".CompleteProfile .left-edge .vline, .CompleteProfile .right-edge .vline").height($(".CompleteProfile > .bg").height()).gradienter();
		$(".CompleteProfile > .bg > .divider").height(height);
		var width = Math.round($(".CompleteProfile .progressbar .bg").width() * profilePercent / 100);
		$(".CompleteProfile .progressbar .fg").width(width);
		$(".CompleteProfile .progressbar .text").text(profilePercent + "%");
	}

	alertCreated = function() {
		if ($(".checkbox.AlreadyHaveProfile").hasClass("checked") || $("#CreateProfile:checked").length == 0) {
			//CreateProfile unchecked, prompt
			$(".Occasional .alertcreated .alertaddress").text($(".checkbox.AlreadyHaveProfile").hasClass("checked") ? $("#Username").val() : $("#EmailAddress").val());
			$(".Occasional .AlreadyHaveProfile_field, .Occasional .CreateProfile_field").hide();
			//show prompt
			$(".Occasional .alertcreated").show();
			$(".Occasional .validationerror").hide();
			$(".Occasional .control").removeClass("error");
			$(".Occasional .field .error-msg").remove();
			//disable input
			$(".Occasional .left-side .field").addClass("read-only_field").find("input[type='text'], input[type='password']").attr("readonly", "readonly");
			//show button
			$(".sendmejobsbyemail, .loginandsendmejobsbyemail, .seemorejobslikethis").toggle();
		} else {
			//CreateProfile checked, go to join (the dialog is the same with Casual
			$(".Occasional").dialog("close");
			$("#JoinFirstName").val($("#FirstName").val()).attr("readonly", "readonly").closest(".field").addClass("read-only_field");
			$("#JoinLastName").val($("#LastName").val()).attr("readonly", "readonly").closest(".field").addClass("read-only_field");
			$("#JoinEmailAddress").val($("#EmailAddress").val()).attr("readonly", "readonly").closest(".field").addClass("read-only_field");
			$(".Casual .alertcreated").show().find(".alertaddress").text($("#EmailAddress").val());
			$(".Casual").dialog({
				modal: true,
				width: 751,
				closeOnEscape: false,
				resizable: false,
				position: [parseInt(($("body").width() - 751) / 2), 200],
				dialogClass: "Casual-dialog"
			}).find(".close-icon").click(function() {
				$(this).closest(".Casual").dialog("close");
			});
			$(".Casual .left-edge .vline, .Casual .right-edge .vline").height($(".Casual > .bg").height()).gradienter();
		}
	}

	shortenSearchCriteria = function() {
		var hidePart = $("<span class='hidepart'></span>");
		var moreOrLessHolder = $("<span class='moreorlessholder'><span class='ellipsis'>... </span><span class='moreorlesstext'>more</span> <span class='arrow'>▼</span></span>");
		if ($(".search-criteria").height() > 45) {
			hidePart.appendTo($(".search-criteria"));
			moreOrLessHolder.appendTo($(".search-criteria"));
			while ($(".search-criteria").height() > 45) hidePart.prev().appendTo(hidePart);
			hidePart.hide();
			$(".search-criteria .moreorlessholder").click(function() {
				if ($(this).prev(":hidden").length > 0) {
					$(this).find(".ellipsis").text(" ");
					$(this).find(".moreorlesstext").text(" hide");
					$(this).find(".arrow").text("▲");
					$(this).prev().toggle();
				} else {
					$(this).find(".ellipsis").text("... ");
					$(this).find(".moreorlesstext").text("more");
					$(this).find(".arrow").text("▼");
					$(this).prev().toggle();
				}
			});
		}
	}

	showSuccInfo = function(type, context, extra_msg) {
		var message = "";
		if (type == "send") message = "Email has already been sent to your friends.";
		$(".succ-info", context).text(message);
		$(".succ-info", context).show();
		$(".validationerror", context).hide();
		$(".control", context).removeClass("error");
		$(".field .error-msg", context).remove();
		if (type == "send") {
			$(".rightside .emaillayer").delay(4000).slideUp(500).parent().find(".actions .icon.email").removeClass("selected");
		}
	}

	showErrInfo = function(type, context, errObj, extMessage) {
		var message = "Please review the errors below";
		if (type == "notmatch") message = extMessage;
		if (type == "send") message = "You need to correct some errors before your email can be sent. Please review the fields and try again."
		$(".validationerror .prompt", context).text(message);
		$(".validationerror", context).show();
		$(".succ-info", context).hide();
		if (type == "send" || type == "LoggedInUserApply" || type == "ManagedExternallyLogin" || type == "ManagedInternallyLogin" || type == "ManagedInternallyJoin" || type == "ManagedExternallyJoin" || type == "Casual" || type == "OccasionalLogin" || type == "OccasionalJoin") {
			$(".field .error-msg", context).remove();
			$(".control, .help-area, .usernamearrow", context).removeClass("error");
			$(".rightside .emaillayer .sendbutton").removeAttr("sending");
			$.each(errObj, function() {
				if (this["Key"] == "Tos") {
					$("#ToNames", context).closest(".control").addClass("error").closest(".field").append("<span class='error-msg' error-for='ToNames'>Your friend's name is required.</span>");
					$("#ToEmailAddresses", context).closest(".control").addClass("error").closest(".field").append("<span class='error-msg' error-for='ToEmailAddresses'>" + this["Message"] + "</span>");
				} else if (this["Key"] == "ToName")
					$("#ToNames", context).closest(".control").addClass("error").closest(".field").append("<span class='error-msg' error-for='ToNames'>" + this["Message"] + "</span>");
				else if (this["Key"] == "ToEmailAddress")
					$("#ToEmailAddresses", context).closest(".control").addClass("error").closest(".field").append("<span class='error-msg' error-for='ToEmailAddresses'>" + this["Message"] + "</span>");
				else if (this["Key"] == "EmailAddress") {
					$("#LoginId", context).closest(".control").addClass("error").closest(".field").append("<span class='error-msg' error-for='LoginId'>" + this["Message"] + "</span>");
					$("#JoinEmailAddress", context).closest(".control").addClass("error").closest(".field").append("<span class='error-msg' error-for='JoinEmailAddress'>" + this["Message"] + "</span>");
					$("#EmailAddress", context).closest(".control").addClass("error").closest(".field").append("<span class='error-msg' error-for='EmailAddress'>" + this["Message"] + "</span>");
					$(".usernamearrow").addClass("error");
				} else if (this["Key"] == "FirstName") {
					$("#FirstName", context).closest(".control").addClass("error").closest(".field").append("<span class='error-msg' error-for='FirstName'>" + this["Message"] + "</span>");
					$("#JoinFirstName", context).closest(".control").addClass("error").closest(".field").append("<span class='error-msg' error-for='JoinFirstName'>" + this["Message"] + "</span>");
				} else if (this["Key"] == "LastName") {
					$("#LastName", context).closest(".control").addClass("error").closest(".field").append("<span class='error-msg' error-for='LastName'>" + this["Message"] + "</span>");
					$("#JoinLastName", context).closest(".control").addClass("error").closest(".field").append("<span class='error-msg' error-for='JoinLastName'>" + this["Message"] + "</span>");
				} else if (this["Key"] == "LoginId") {
					$("#Username", context).closest(".control").addClass("error").closest(".field").append("<span class='error-msg' error-for='Username'>" + this["Message"] + "</span>");
					$("#LoginId", context).closest(".control").addClass("error").closest(".field").append("<span class='error-msg' error-for='LoginId'>Your email is required.</span>");
				} else if (this["Key"] == "AcceptTerms") {
					$("#TAndC", context).closest(".control").addClass("error").closest(".field").append("<span class='error-msg' error-for='TAndC'>" + this["Message"] + "</span>");
				} else
					$("#" + this["Key"], context).closest(".control").addClass("error").closest(".field").find(".help-area").addClass("error").end().append("<span class='error-msg' error-for='" + this["Key"] + "'>" + this["Message"] + "</span>");
			});
		}
		$(".validationerror ul", context).text("");
		$.each(errObj, function() {
			if (this["Key"] == "Tos") {
				$(".validationerror ul", context).append("<li><div class='listicon'></div><div class='text'>Your friend's name is required.</div></li>");
				$(".validationerror ul", context).append("<li><div class='listicon'></div><div class='text'>" + this["Message"] + "</div></li>");
			} else if (this["Key"] == "LoginId")
				$(".validationerror ul", context).append("<li><div class='listicon'></div><div class='text'>Your email is required.</div></li>");
			else
				$(".validationerror ul", context).append("<li><div class='listicon'></div><div class='text'>" + this["Message"] + "</div></li>");
		});
		resizeAfterRibbon();
	}

	resizeAfterRibbon = function() {
		$(".afterribbon > .divider").height(0).height($(".afterribbon").height() + 13); //reset to 0 first then + 13, this divider has margin-top = 10, margin-bottom = -3, so add 13 here.
		if ($(".applyarea").hasClass("CareerOne")) {
			var margin = ($(".applyarea.CareerOne").height() - $(".applyarea.CareerOne .careeroneapply").height() - 50) / 2;
			$(".applyarea.CareerOne .careeroneapply").css({
				//"margin-top" : margin + "px",
				"margin-bottom": margin + "px"
			});
		}
	}

	organiseFields = function(context) {
		if (!context) context = $("body");
		$(".control select:not(.listbox, .month_dropdown, .year_dropdown)", context).each(function() {
			var textBg = $("<div class='textbox-bg'><input type='text' /></div>");
			var control = $(this).parent();
			$(this).appendTo(textBg);
			control.text("").append(textBg);
			textBg.before("<div class='textbox-left'></div>").after("<div class='textbox-right'></div>");
		});
		$(".control .textbox:not(.password_textbox, .multiline_textbox)", context).each(function() {
			var textBg = $("<div class='textbox-bg'></div>");
			var control = $(this).parent();
			$(this).appendTo(textBg);
			control.append(textBg);
			textBg.before("<div class='textbox-left'></div>").after("<div class='textbox-right'></div>");
			if ($(this).val() == "" && $(this).attr("data-watermark") != "") $(this).val($(this).attr("data-watermark"));
		}).focus(function() {
			if ($(this).attr("readonly") == "readonly") return;
			if ($(this).val() == $(this).attr("data-watermark")) $(this).val("");
		}).blur(function() {
			if ($(this).val() == "") $(this).val($(this).attr("data-watermark"));
		});
		$(".control .multiline_textbox", context).each(function() {
			var textBg = $("<div class='textarea-bg'></div>");
			var textarea = $(this);
			var control = textarea.parent();
			var height = parseInt(textarea.css("height"));
			textarea.appendTo(textBg);
			var topArrow = $("<div class='top'><div class='arrow'></div></div>"), bottomArrow = $("<div class='bottom'><div class='arrow'></div></div>"), scrollbarWrap = $("<div class='wrap'></div>"), scrollbar = $("<div class='scrollbar'></div>"), scrollbarOuter = $("<div class='outer'></div>");
			var topBarHeight = 4, bottomBarHeight = 4, arrowHeight = 16, handleHeight = 48;
			scrollbarOuter.append(topArrow).append(scrollbarWrap).append(bottomArrow);
			textBg.append(scrollbarOuter);
			scrollbarWrap.append(scrollbar).height(height + topBarHeight + bottomBarHeight - (arrowHeight * 2));
			scrollbar.height(height + topBarHeight + bottomBarHeight - (arrowHeight * 2) - handleHeight);
			//init textarea event
			change = function() {
				// console.log("clientHeight = " + this.clientHeight);
				// console.log("scrollHeight = " + this.scrollHeight);
				// console.log("scrollTop = " + this.scrollTop);
				if (this.scrollHeight > this.clientHeight) scrollbarOuter.show();
				else scrollbarOuter.hide();
				scrollbar.slider("option", "max", this.scrollHeight - this.clientHeight);
				scrollbar.slider("value", scrollbar.slider("option", "max") - this.scrollTop);
				// console.log("maxValue = " + scrollbar.slider("option", "max"));
				// console.log("scrollValue = " + scrollbar.slider("value"));
			}
			textarea.change(change).keyup(change).keydown(change).keypress(change);
			//init slider
			var minValue = 0, maxValue = 0;
			var scrollpan = textarea.closest(".textarea-bg");
			var scrollpanHeight = scrollpan.height();
			scrollbar.slider({
				orientation: "vertical",
				min: minValue,
				max: maxValue,
				value: maxValue,
				slide: function(event, ui) {
					textarea[0].scrollTop = scrollbar.slider("option", "max") - ui.value;
				},
				change: function(event, ui) {
					textarea[0].scrollTop = scrollbar.slider("option", "max") - ui.value;
				}
			}).removeClass("ui-widget-content");
			var scrollbarWrap = scrollpan.find(".wrap");
			topArrow.click(function() {
				scrollbar.slider("value", scrollbar.slider("option", "max") - (scrollbar.slider("value") + 10));
			});
			bottomArrow.click(function() {
				scrollbar.slider("value", scrollbar.slider("option", "max") - (scrollbar.slider("value") - 10));
			});
			control.text("").append(textBg);
			textBg.before("<div class='textarea-top'></div>").after("<div class='textarea-bottom'></div>");
		});
		$(".control .textbox.password_textbox", context).each(function() {
			var id = $(this).attr("id");
			var fakePwd = $("<input type='text' class='textbox' id='Fake" + id + "' data-watermark='" + $(this).attr("data-watermark") + "' value='" + $(this).attr("data-watermark") + "'>");
			var textBg = $("<div class='textbox-bg'></div>");
			var control = $(this).hide().parent();
			textBg.append($(this)).append(fakePwd);
			control.append(textBg);
			textBg.before("<div class='textbox-left'></div>").after("<div class='textbox-right'></div>");
			fakePwd.show().focus(function() {
				if ($(this).attr("readonly") == "readonly") return;
				if ($(this).val() == $(this).attr("data-watermark")) {
					$("#" + id).show().focus();
					$(this).hide();
				}
			});
		}).blur(function() {
			if ($(this).val() == "") {
				$(this).hide();
				$("#Fake" + $(this).attr("id")).show().val($("#Fake" + $(this).attr("id")).attr("data-watermark"));
			}
		});
		$(".checkbox_control", context).each(function() {
			var checkbox = $("<div class='checkbox'></div>");
			var id = $("input[type='checkbox']", this).hide().attr("id");
			checkbox.addClass(id).prependTo(this);
			if ($("input[type='checkbox']", this).is(":checked")) checkbox.addClass("checked");
			checkbox.click(function(event) {
				if ($(this).hasClass("disabled")) return;
				if ($(this).hasClass("checked")) {
					$("input[type='checkbox']", $(this).parent()).removeAttr("checked");
					$(this).removeClass("checked");
				} else {
					$("input[type='checkbox']", $(this).parent()).attr("checked", "checked");
					$(this).addClass("checked");
				}
				event.stopPropagation();
			});
		});
		$(".radiobuttons_control", context).each(function() {
			var radioButtonGroup = $(this);
			$(".radio_control", this).each(function() {
				var radiobutton = $("<div class='radiobutton'></div>");
				var id = $("input[type='radio']", this).hide().attr("id");
				radiobutton.attr("value", id).prependTo(this);
				if ($("input[type='radio']", this).is(":checked"))
					radiobutton.addClass("checked");
				if ($("input[type='radio']", this).attr("disabled") == "disabled")
					radiobutton.addClass("disabled");
				radiobutton.click(function(event) {
					if ($(this).hasClass("disabled")) return;
					$(".radiobutton", radioButtonGroup).removeClass("checked");
					$(this).addClass("checked");
					$("input.radio", radioButtonGroup).removeAttr("checked");
					$("#" + id, radioButtonGroup).attr("checked", "checked");
					event.stopPropagation();
				});
			});
		});
		$(".compulsory_field", context).each(function() {
			var mandatory = $("<div class='mandatory'></div>");
			mandatory.insertAfter($("> label", this));
		});
		//help icon, all help icons need to be added before this function called
		$(".help-icon", context).hover(function() {
			var helparea = $(".help-area[helpfor='" + $(this).attr("helpfor") + "']");
			if (helparea.hasClass("stay")) return;
			var marginTop = Number($(this).css("margin-top").substring(0, $(this).css("margin-top").length - 2));
			var marginLeft = Number($(this).css("margin-left").substring(0, $(this).css("margin-left").length - 2));
			if ($.browser.mozilla)
				helparea.css({
					left: $(this).position().left + marginLeft + 14,
					top: $(this).position().top + marginTop - 8
				});
			else
				helparea.css({
					left: $(this).position().left + marginLeft + 10,
					top: $(this).position().top + marginTop - 8
				});
			helparea.show();
		}, function() {
			var helparea = $(".help-area[helpfor='" + $(this).attr("helpfor") + "']");
			if (helparea.hasClass("stay")) return;
			helparea.hide();
		}).click(function() {
			var helparea = $(".help-area[helpfor='" + $(this).attr("helpfor") + "']");
			if (helparea.hasClass("stay")) helparea.hide().removeClass("stay");
			else {
				$(".help-area").hide().removeClass("stay");
				var marginTop = Number($(this).css("margin-top").substring(0, $(this).css("margin-top").length - 2));
				var marginLeft = Number($(this).css("margin-left").substring(0, $(this).css("margin-left").length - 2));
				if ($.browser.mozilla)
					helparea.css({
						left: $(this).position().left + marginLeft + 14,
						top: $(this).position().top + marginTop - 8
					});
				else
					helparea.css({
						left: $(this).position().left + marginLeft + 10,
						top: $(this).position().top + marginTop - 8
					});
				helparea.show().addClass("stay");
			}
		});
		//hide all link's text
		$(".alljobsbutton, .seemorejobslikethis, .completemyprofilenow, .applybutton", context).text("");
	}

	String.prototype.unMungeUrl = function() {
		return this.replace(/~@~/gi, "/");
	}
})(jQuery);

/*
 *  jQuery Ellipsis
 *  Mnigrele/Emateu: 8 abril 2011. Aportes de Ekupelian
 *  No copyright
 *
 */

(function($){

    $.fn.ellipsis = function(conf) {
        return this.each(function() {
			$(this).attr("title", $(this).text());
            setup($(this), conf);
        });
    };

    function setup(element, conf) {

        conf = $.extend({
            lines: 2
        }, conf || {} );

        var lineHeight = parseInt(element.css("line-height"), 10);
		//set default line height to fontsize + 2px if it is not set in css
		if (isNaN(lineHeight)) lineHeight = parseInt(element.css("font-size"), 10) + 2;
		var height = element.height();

        if (height < lineHeight * conf.lines) return false;

        var divCloned = $("<div>").css({
            "font-size" : element.css("font-size"),
            "font-weight" : element.css("font-weight"),
			"font-family" : element.css("font-family"),
			"text-align" : element.css("text-align"),
            "line-height" : element.css("line-height"),
            "position" : "absolute",
			"width" : element.width(),
            "top" : "99px"//"-99999px"
        }).appendTo("body");

		var content = element.text(), length = content.length;
		var leftPos = 0, middlePos = length, rightPos = length;
		
		do {
			divCloned.text(content.substring(0, middlePos));
			if (divCloned.height() > height) rightPos = middlePos;
			else leftPos = middlePos;
			middlePos = Math.floor(leftPos + (rightPos - leftPos) / 2);
		} while (!(leftPos == rightPos || leftPos == rightPos - 1));
		
		if (leftPos == 0) element.text("");
		else if (leftPos == 1) element.text(".");
		else if (leftPos == 2) element.text("..");
		else if (leftPos == length) element.text(content);
		else element.text(content.substring(0, leftPos - 3) + "...");
		element.attr("title", content);
        divCloned.remove();
		return true;
    }

}(jQuery));


/**
 * jqBarGraph - jQuery plugin
 * @version: 1.0 (2010/09/11)
 * @requires jQuery v1.2.2 or later 
 * @author Ivan Lazarevic
 * Examples and documentation at: http://www.workshop.rs/projects/gradienter
 * 
 * Dual licensed under the MIT and GPL licenses:
 *   http://www.opensource.org/licenses/mit-license.php
 *   http://www.gnu.org/licenses/gpl.html
 * 
 * @param color_start: 'FFFFFF' // first color in gradient
 * @param color_end: '000000' // last color in gradient
 * @param prop: 'background-color' // on which CSS property should apply gradient

 * @example  $('div').gradienter({ color_start: '0099FF', color_end: 'F3F3F3', prob: 'color' });  
  
**/

(function($) {
	var opts = new Array;
	var count = 0;
	var defaults = {	
		color_start: 'FFFFFF',
		color_end: '000000',
		prop: 'background-color'
	};
	
	$.fn.Gradienter = $.fn.gradienter = function(options) {
        return this.each(function() {
			var d = new Date();
			init(this, options);
        });
    };
	
	init = function(el, options) {
		if (!options) options = {};
		if ($(el).attr("colour_start") != "") options["color_start"] = $(el).attr("colour_start");
		if ($(el).attr("colour_end") != "") options["color_end"] = $(el).attr("colour_end");

		opts[count] = $.extend({}, defaults, options);
		
		var r = g = b = 0;
		
		
		r = sr = parseInt(opts[count].color_start.substring(0,2),16);
		g = sg = parseInt(opts[count].color_start.substring(2,4),16);
		b = sb = parseInt(opts[count].color_start.substring(4,6),16);

		er = parseInt(opts[count].color_end.substring(0,2),16);
		eg = parseInt(opts[count].color_end.substring(2,4),16);
		eb = parseInt(opts[count].color_end.substring(4,6),16);
	
		//$(el).css(opts[count].prop , 'rgb('+sr+','+sg+','+sb+')');			
	
		//add pixels to elements
		var arr = new Array();
		var steps = $(el).height() - 1;
		var funcPrefix = ":rgba(", funcSuffix = ",0.6);";
		if ($.browser.msie && ($.browser.version.indexOf("7") == 0 || $.browser.version.indexOf("8") == 0)) {
			funcPrefix = ":rgb(";
			funcSuffix = ");";
		}
		for (var i = 0; i < steps + 1; i++) {
			r = sr + parseInt((er - sr) * i / steps);
			g = sg + parseInt((eg - sg) * i / steps);
			b = sb + parseInt((eb - sb) * i / steps);
			var style = opts[count].prop + funcPrefix + r + "," + g + "," + b + funcSuffix;
			arr[i] = "<div style='" + style + "'></div>"; 
		}
		$(el).html(arr.join(""));

		count++;
	};
})(jQuery);


/*
    http://www.JSON.org/json2.js
    2011-10-19

    Public Domain.

    NO WARRANTY EXPRESSED OR IMPLIED. USE AT YOUR OWN RISK.

    See http://www.JSON.org/js.html


    This code should be minified before deployment.
    See http://javascript.crockford.com/jsmin.html

    USE YOUR OWN COPY. IT IS EXTREMELY UNWISE TO LOAD CODE FROM SERVERS YOU DO
    NOT CONTROL.


    This file creates a global JSON object containing two methods: stringify
    and parse.

        JSON.stringify(value, replacer, space)
            value       any JavaScript value, usually an object or array.

            replacer    an optional parameter that determines how object
                        values are stringified for objects. It can be a
                        function or an array of strings.

            space       an optional parameter that specifies the indentation
                        of nested structures. If it is omitted, the text will
                        be packed without extra whitespace. If it is a number,
                        it will specify the number of spaces to indent at each
                        level. If it is a string (such as '\t' or '&nbsp;'),
                        it contains the characters used to indent at each level.

            This method produces a JSON text from a JavaScript value.

            When an object value is found, if the object contains a toJSON
            method, its toJSON method will be called and the result will be
            stringified. A toJSON method does not serialize: it returns the
            value represented by the name/value pair that should be serialized,
            or undefined if nothing should be serialized. The toJSON method
            will be passed the key associated with the value, and this will be
            bound to the value

            For example, this would serialize Dates as ISO strings.

                Date.prototype.toJSON = function (key) {
                    function f(n) {
                        // Format integers to have at least two digits.
                        return n < 10 ? '0' + n : n;
                    }

                    return this.getUTCFullYear()   + '-' +
                         f(this.getUTCMonth() + 1) + '-' +
                         f(this.getUTCDate())      + 'T' +
                         f(this.getUTCHours())     + ':' +
                         f(this.getUTCMinutes())   + ':' +
                         f(this.getUTCSeconds())   + 'Z';
                };

            You can provide an optional replacer method. It will be passed the
            key and value of each member, with this bound to the containing
            object. The value that is returned from your method will be
            serialized. If your method returns undefined, then the member will
            be excluded from the serialization.

            If the replacer parameter is an array of strings, then it will be
            used to select the members to be serialized. It filters the results
            such that only members with keys listed in the replacer array are
            stringified.

            Values that do not have JSON representations, such as undefined or
            functions, will not be serialized. Such values in objects will be
            dropped; in arrays they will be replaced with null. You can use
            a replacer function to replace those with JSON values.
            JSON.stringify(undefined) returns undefined.

            The optional space parameter produces a stringification of the
            value that is filled with line breaks and indentation to make it
            easier to read.

            If the space parameter is a non-empty string, then that string will
            be used for indentation. If the space parameter is a number, then
            the indentation will be that many spaces.

            Example:

            text = JSON.stringify(['e', {pluribus: 'unum'}]);
            // text is '["e",{"pluribus":"unum"}]'


            text = JSON.stringify(['e', {pluribus: 'unum'}], null, '\t');
            // text is '[\n\t"e",\n\t{\n\t\t"pluribus": "unum"\n\t}\n]'

            text = JSON.stringify([new Date()], function (key, value) {
                return this[key] instanceof Date ?
                    'Date(' + this[key] + ')' : value;
            });
            // text is '["Date(---current time---)"]'


        JSON.parse(text, reviver)
            This method parses a JSON text to produce an object or array.
            It can throw a SyntaxError exception.

            The optional reviver parameter is a function that can filter and
            transform the results. It receives each of the keys and values,
            and its return value is used instead of the original value.
            If it returns what it received, then the structure is not modified.
            If it returns undefined then the member is deleted.

            Example:

            // Parse the text. Values that look like ISO date strings will
            // be converted to Date objects.

            myData = JSON.parse(text, function (key, value) {
                var a;
                if (typeof value === 'string') {
                    a =
/^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2}(?:\.\d*)?)Z$/.exec(value);
                    if (a) {
                        return new Date(Date.UTC(+a[1], +a[2] - 1, +a[3], +a[4],
                            +a[5], +a[6]));
                    }
                }
                return value;
            });

            myData = JSON.parse('["Date(09/09/2001)"]', function (key, value) {
                var d;
                if (typeof value === 'string' &&
                        value.slice(0, 5) === 'Date(' &&
                        value.slice(-1) === ')') {
                    d = new Date(value.slice(5, -1));
                    if (d) {
                        return d;
                    }
                }
                return value;
            });


    This is a reference implementation. You are free to copy, modify, or
    redistribute.
*/

/*jslint evil: true, regexp: true */

/*members "", "\b", "\t", "\n", "\f", "\r", "\"", JSON, "\\", apply,
    call, charCodeAt, getUTCDate, getUTCFullYear, getUTCHours,
    getUTCMinutes, getUTCMonth, getUTCSeconds, hasOwnProperty, join,
    lastIndex, length, parse, prototype, push, replace, slice, stringify,
    test, toJSON, toString, valueOf
*/

if ($.browser.msie && $.browser.version.indexOf("7") == 0) {

	// Create a JSON object only if one does not already exist. We create the
	// methods in a closure to avoid creating global variables.

	var JSON;
	if (!JSON) {
		JSON = {};
	}

	(function () {
		'use strict';

		function f(n) {
			// Format integers to have at least two digits.
			return n < 10 ? '0' + n : n;
		}

		if (typeof Date.prototype.toJSON !== 'function') {

			Date.prototype.toJSON = function (key) {

				return isFinite(this.valueOf())
					? this.getUTCFullYear()     + '-' +
						f(this.getUTCMonth() + 1) + '-' +
						f(this.getUTCDate())      + 'T' +
						f(this.getUTCHours())     + ':' +
						f(this.getUTCMinutes())   + ':' +
						f(this.getUTCSeconds())   + 'Z'
					: null;
			};

			String.prototype.toJSON      =
				Number.prototype.toJSON  =
				Boolean.prototype.toJSON = function (key) {
					return this.valueOf();
				};
		}

		var cx = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,
			escapable = /[\\\"\x00-\x1f\x7f-\x9f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,
			gap,
			indent,
			meta = {    // table of character substitutions
				'\b': '\\b',
				'\t': '\\t',
				'\n': '\\n',
				'\f': '\\f',
				'\r': '\\r',
				'"' : '\\"',
				'\\': '\\\\'
			},
			rep;


		function quote(string) {

	// If the string contains no control characters, no quote characters, and no
	// backslash characters, then we can safely slap some quotes around it.
	// Otherwise we must also replace the offending characters with safe escape
	// sequences.

			escapable.lastIndex = 0;
			return escapable.test(string) ? '"' + string.replace(escapable, function (a) {
				var c = meta[a];
				return typeof c === 'string'
					? c
					: '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
			}) + '"' : '"' + string + '"';
		}


		function str(key, holder) {

	// Produce a string from holder[key].

			var i,          // The loop counter.
				k,          // The member key.
				v,          // The member value.
				length,
				mind = gap,
				partial,
				value = holder[key];

	// If the value has a toJSON method, call it to obtain a replacement value.

			if (value && typeof value === 'object' &&
					typeof value.toJSON === 'function') {
				value = value.toJSON(key);
			}

	// If we were called with a replacer function, then call the replacer to
	// obtain a replacement value.

			if (typeof rep === 'function') {
				value = rep.call(holder, key, value);
			}

	// What happens next depends on the value's type.

			switch (typeof value) {
			case 'string':
				return quote(value);

			case 'number':

	// JSON numbers must be finite. Encode non-finite numbers as null.

				return isFinite(value) ? String(value) : 'null';

			case 'boolean':
			case 'null':

	// If the value is a boolean or null, convert it to a string. Note:
	// typeof null does not produce 'null'. The case is included here in
	// the remote chance that this gets fixed someday.

				return String(value);

	// If the type is 'object', we might be dealing with an object or an array or
	// null.

			case 'object':

	// Due to a specification blunder in ECMAScript, typeof null is 'object',
	// so watch out for that case.

				if (!value) {
					return 'null';
				}

	// Make an array to hold the partial results of stringifying this object value.

				gap += indent;
				partial = [];

	// Is the value an array?

				if (Object.prototype.toString.apply(value) === '[object Array]') {

	// The value is an array. Stringify every element. Use null as a placeholder
	// for non-JSON values.

					length = value.length;
					for (i = 0; i < length; i += 1) {
						partial[i] = str(i, value) || 'null';
					}

	// Join all of the elements together, separated with commas, and wrap them in
	// brackets.

					v = partial.length === 0
						? '[]'
						: gap
						? '[\n' + gap + partial.join(',\n' + gap) + '\n' + mind + ']'
						: '[' + partial.join(',') + ']';
					gap = mind;
					return v;
				}

	// If the replacer is an array, use it to select the members to be stringified.

				if (rep && typeof rep === 'object') {
					length = rep.length;
					for (i = 0; i < length; i += 1) {
						if (typeof rep[i] === 'string') {
							k = rep[i];
							v = str(k, value);
							if (v) {
								partial.push(quote(k) + (gap ? ': ' : ':') + v);
							}
						}
					}
				} else {

	// Otherwise, iterate through all of the keys in the object.

					for (k in value) {
						if (Object.prototype.hasOwnProperty.call(value, k)) {
							v = str(k, value);
							if (v) {
								partial.push(quote(k) + (gap ? ': ' : ':') + v);
							}
						}
					}
				}

	// Join all of the member texts together, separated with commas,
	// and wrap them in braces.

				v = partial.length === 0
					? '{}'
					: gap
					? '{\n' + gap + partial.join(',\n' + gap) + '\n' + mind + '}'
					: '{' + partial.join(',') + '}';
				gap = mind;
				return v;
			}
		}

	// If the JSON object does not yet have a stringify method, give it one.

		if (typeof JSON.stringify !== 'function') {
			JSON.stringify = function (value, replacer, space) {

	// The stringify method takes a value and an optional replacer, and an optional
	// space parameter, and returns a JSON text. The replacer can be a function
	// that can replace values, or an array of strings that will select the keys.
	// A default replacer method can be provided. Use of the space parameter can
	// produce text that is more easily readable.

				var i;
				gap = '';
				indent = '';

	// If the space parameter is a number, make an indent string containing that
	// many spaces.

				if (typeof space === 'number') {
					for (i = 0; i < space; i += 1) {
						indent += ' ';
					}

	// If the space parameter is a string, it will be used as the indent string.

				} else if (typeof space === 'string') {
					indent = space;
				}

	// If there is a replacer, it must be a function or an array.
	// Otherwise, throw an error.

				rep = replacer;
				if (replacer && typeof replacer !== 'function' &&
						(typeof replacer !== 'object' ||
						typeof replacer.length !== 'number')) {
					throw new Error('JSON.stringify');
				}

	// Make a fake root object containing our value under the key of ''.
	// Return the result of stringifying the value.

				return str('', {'': value});
			};
		}


	// If the JSON object does not yet have a parse method, give it one.

		if (typeof JSON.parse !== 'function') {
			JSON.parse = function (text, reviver) {

	// The parse method takes a text and an optional reviver function, and returns
	// a JavaScript value if the text is a valid JSON text.

				var j;

				function walk(holder, key) {

	// The walk method is used to recursively walk the resulting structure so
	// that modifications can be made.

					var k, v, value = holder[key];
					if (value && typeof value === 'object') {
						for (k in value) {
							if (Object.prototype.hasOwnProperty.call(value, k)) {
								v = walk(value, k);
								if (v !== undefined) {
									value[k] = v;
								} else {
									delete value[k];
								}
							}
						}
					}
					return reviver.call(holder, key, value);
				}


	// Parsing happens in four stages. In the first stage, we replace certain
	// Unicode characters with escape sequences. JavaScript handles many characters
	// incorrectly, either silently deleting them, or treating them as line endings.

				text = String(text);
				cx.lastIndex = 0;
				if (cx.test(text)) {
					text = text.replace(cx, function (a) {
						return '\\u' +
							('0000' + a.charCodeAt(0).toString(16)).slice(-4);
					});
				}

	// In the second stage, we run the text against regular expressions that look
	// for non-JSON patterns. We are especially concerned with '()' and 'new'
	// because they can cause invocation, and '=' because it can cause mutation.
	// But just to be safe, we want to reject all unexpected forms.

	// We split the second stage into 4 regexp operations in order to work around
	// crippling inefficiencies in IE's and Safari's regexp engines. First we
	// replace the JSON backslash pairs with '@' (a non-JSON character). Second, we
	// replace all simple value tokens with ']' characters. Third, we delete all
	// open brackets that follow a colon or comma or that begin the text. Finally,
	// we look to see that the remaining characters are only whitespace or ']' or
	// ',' or ':' or '{' or '}'. If that is so, then the text is safe for eval.

				if (/^[\],:{}\s]*$/
						.test(text.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, '@')
							.replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']')
							.replace(/(?:^|:|,)(?:\s*\[)+/g, ''))) {

	// In the third stage we use the eval function to compile the text into a
	// JavaScript structure. The '{' operator is subject to a syntactic ambiguity
	// in JavaScript: it can begin a block or an object literal. We wrap the text
	// in parens to eliminate the ambiguity.

					j = eval('(' + text + ')');

	// In the optional fourth stage, we recursively walk the new structure, passing
	// each name/value pair to a reviver function for possible transformation.

					return typeof reviver === 'function'
						? walk({'': j}, '')
						: j;
				}

	// If the text is not JSON parseable, then a SyntaxError is thrown.

				throw new SyntaxError('JSON.parse');
			};
		}
	}());

}

(function ($) {
	$.fn.dropdown = function() {
		return this.each(function() {
			var dd = $(this);

			dd.addClass("dropdown");
			
			//append vertical line and an arrow to simulate down arrow
			dd.after("<div class='dropdown-arrow'></div>").after("<div class='dropdown-vline'></div>");
			
			//readonly textbox, hide select box
			$("input[type='text']", dd).attr("readonly", "readonly");
			$("select", dd).hide();
			createScrollPan(dd.parent(), "dropdown");
			var scrollpan = $(".dropdown-scrollpan", dd.parent());
			scrollpan.hide();
			
			dd.click(function (event) {
				if ($(this).closest(".control").hasClass("disabled")) return false;
				if (scrollpan.css("display") == "none") {
					$(".dropdown-scrollpan").hide();
					var parentWidth = dd.parent().width();
					if (dd.parent().hasClass("DOBYear_control") || dd.parent().hasClass("DOBMonth_control") || dd.parent().hasClass("DOBYear_control") || dd.parent().hasClass("StartDateYear_control") || dd.parent().hasClass("StartDateMonth_control") || dd.parent().hasClass("EndDateMonth_control") || dd.parent().hasClass("EndDateMonth_control"))
						parentWidth -= 24;
					scrollpan.width(parentWidth);
					if ($.browser.msie && $.browser.version.indexOf("7") == 0)
						scrollpan.css("margin-left", "-" + parentWidth + "px");
					if (dd.find("option").length > dd.find("select").attr("size"))
						scrollpan.find(".dropdown-items").width(parentWidth - 12 - 1); //12 = scrollbarWidth, see below
					else
						scrollpan.find(".dropdown-items").width(parentWidth);
					scrollpan.show().css("z-index", "1000");
				} else {
					scrollpan.hide().css("z-index", "-1");
				}
				event.stopPropagation();
			});
			$(".dropdown-vline, .dropdown-arrow", dd.parent()).click(function() {
				dd.click();
			});
		});
	};
	
	$.fn.listbox = function() {
		return this.each(function() {
			var lb = $(this);
			lb.addClass("listbox");
			
			$("select", lb).hide();
			createScrollPan(lb, "listbox");
		});
	};
	
	//type = "dropdown" or "listbox"
	createScrollPan = function(parent, type) {
		//items
		var items = $("<div class='" + type + "-items'></div>");
		var scrollpan = $("<div class='" + type + "-scrollpan'></div>");
		scrollpan.append(items);
		scrollpan.width(parent.width() + "px");
		parent.append(scrollpan);
		$.each($("select option", parent), function() {
			var item = $("<div class='" + type + "-item'>" + $(this).text() + "</div>");
			item.click(function (e) {
				if (type == "dropdown") {
					scrollpan.hide();
					var index = $("." + type + "-item", $(this).parent()).index(this);
					$("select option", parent).removeAttr("selected");
					$("select option:nth-child(" + (index + 1) + ")", parent).attr("selected", "selected");
					$("input[type='text']", parent).val($(this).text());
				}
				if (type == "listbox") {
					if (e.ctrlKey) {
						if ($(this).hasClass("selected")) {
							//turn on
							$(this).closest(".listbox").find("select option:contains('" + $(this).text() + "')").removeAttr("selected");
							$(this).removeClass("selected");
						} else {
							//turn off
							if ($("." + type + "-item.selected", parent).length == $("select", parent).attr("maxcount")) return;
							else {
								$(this).closest(".listbox").find("select option:contains('" + $(this).text() + "')").attr("selected", "selected");
								$(this).addClass("selected");
							}
						}
					} else {
						$("." + type + "-item", parent).removeClass("selected");
						$(this).closest(".listbox").find("select option").removeAttr("selected");
						$(this).addClass("selected");
						$(this).closest(".listbox").find("select option:contains('" + $(this).text() + "')").attr("selected", "selected");
					}
				}
				e.stopPropagation();
			});
			items.append(item);
			if ($(this).attr("selected")) item.click();
		});
		
		//scroll pan and scroll bar
		var size = $("select", parent).attr("size");
		var length = $("." + type + "-item", items).length;
		var itemHeight = 26;
		scrollpan.height(size * itemHeight + 1);
		if (length > size) {
			var topArrow = $("<div class='" + type + "-scrollbar-toparrow-border'></div><div class='" + type + "-scrollbar-toparrow'></div>");
			var bottomArrow = $("<div class='" + type + "-scrollbar-bottomarrow-border'></div><div class='" + type + "-scrollbar-bottomarrow'></div>");
			var scrollbarWrap = $("<div class='" + type + "-scrollbar-wrap'></div>");
			var scrollbar = $("<div class='" + type + "-scrollbar'></div>");
			var diff = (length - size) * itemHeight;
			var portion = diff / items.height();
			var handleHeight = 48;
			var minValue = 0;
			var maxValue = 100;
			var scrollbarWidth = 12;
			scrollpan.append(topArrow).append(scrollbarWrap).append(bottomArrow);
			scrollbar.slider({
				orientation: "vertical",
				min: minValue,
				max: maxValue,
				value: maxValue,
				slide: function(event, ui) {
					var topVal = -((maxValue - ui.value) * diff / 100);
					items.css("top", topVal);
				},
				change: function(event, ui) {
					var topVal = -((maxValue - ui.value) * diff / 100);
					items.css("top", topVal);
				}
			}).removeClass("ui-widget-content").height(size * itemHeight - handleHeight - 22);
			items.width(parent.width() - scrollbarWidth - 1);
			scrollbarWrap.append(scrollbar).height(size * itemHeight - 22).mousedown(function(e) {
				if ($(e.srcElement).hasClass("ui-slider-handle")) return;
				scrollbar.slider("value", 100 - (e.offsetY / scrollbarWrap.height() * 100));
			});
			$("." + type + "-scrollbar-toparrow, ." + type + "-scrollbar-toparrow-border", scrollpan).click(function() {
				scrollbar.slider("value", scrollbar.slider("value") + 10);
			});
			$("." + type + "-scrollbar-bottomarrow, ." + type + "-scrollbar-bottomarrow-border", scrollpan).click(function() {
				scrollbar.slider("value", scrollbar.slider("value") - 10);
			});
			if (type == "listbox") parent.height(scrollpan.height());
		}
	};
})(jQuery);