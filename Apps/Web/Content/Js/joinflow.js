(function($) {

    initScriptForStep = function(step, urls) {
        organiseFields();

        $(".step.Join:not(.disabled)").click(function() {
            window.location = urls.Join;
        });

        $(".step.PersonalDetails:not(.disabled)").click(function() {
            window.location = urls.PersonalDetails;
        });

        $(".step.JobDetails:not(.disabled)").click(function() {
            window.location = urls.JobDetails;
        });

        $(".step.Activate:not(.disabled)").click(function() {
            window.location = urls.Activate;
        });

        switch (step) {
            case "Join":
                $(".upload-resume").click(function() {
                    if (!$(this).find(".radiobutton").hasClass("checked")) {
                        $(".radiobutton, .manually-create").toggleClass("checked");
						$(".step-content").toggleClass("upload manually");
						$(".createbutton").hide();
					}
                });
                $(".manually-create").click(function() {
                    if (!$(this).find(".radiobutton").hasClass("checked")) {
                        $(".radiobutton, .manually-create").toggleClass("checked");
						$(".step-content").toggleClass("upload manually");
						$(".createbutton").show();
					}
                });
				$(".createbutton").click(function() {
                    $("#ParsedResumeId").val("");
                    $("#JoinForm").submit();
				});
                $(".browse-button input").change(function() {
                    var fakeFilepath = $(this).val();
                    var index = fakeFilepath.lastIndexOf("\\");
                    var filename = fakeFilepath.substring(index + 1);
					$(".upload-button").show();
                    $("#ResumePath").val(filename).removeClass("mask");
					$(".nofileselected").hide();
                });
                var fileUploadData;
                updateProgressBar = function(percent) {
                    if (percent < 3 || percent > 98) $(".upload-layer .progress-bar-right").hide();
                    else $(".upload-layer .progress-bar-right").show();
                    $(".upload-layer .progress-bar").width(percent + "%");
                    $(".upload-layer .percent").text(percent + "%");
                }
                showErrorMsg = function(errorMsg) {
                    $(".upload-layer .prompt-text").text("");
                    $(".button-holder").addClass("error").appendTo(".errorMsg").show();
                    $(".errorMsg").show();
                    $(".errorMsg span").text(errorMsg);
                    $(".next-step").text("Reload my resume");
                }
                updateProgressBar(0);
                $("#fileupload").fileupload({
                    url: "api/resumes/upload",
                    type: "POST",
                    dataType: "json",
                    namespace: "linkme.resume.upload",
                    fileInput: $(".browse-button input[type='file']"),
                    replaceFileInput: false,
                    limitMultiFileUploads: 1,
                    add: function(e, data) {
                        fileUploadData = data;
                    },
                    progress: function(e, data) {
                        var percent = parseInt(data.loaded / data.total * 100, 10);
                        updateProgressBar(percent);
                    },
                    done: function(e, data) {
                        if (data.result.Success) {
                            $("#FileReferenceId").val(data.result.Id);
                            updateProgressBar(100);
                            $(".upload-layer .auto-extraction").show();
                            //ajax call parsing
                            $.post("api/resumes/parse", {
                                fileReferenceId: data.result.Id
                            }, function(data) {
								// data.Success = true;
								// data.Id = "da97b42b-105d-465f-be2c-00820cbd99de";
                                if (data.Success) {
                                    //show ok button
                                    $(".upload-layer .prompt-text").text("Your resume has been uploaded and details have been extracted.");
                                    $(".upload-layer .auto-extraction").hide();
                                    $(".upload-layer .check-icon").show();
                                    $(".upload-layer .button-holder").show();
									$(".upload-layer .button-holder .next-step").text("Take me to the next step");
                                    $("#ParsedResumeId").val(data.Id);
                                } else {
                                    showErrorMsg(data.Errors[0].Message);
                                }
                            }).error(function(error) {
								var data = $.parseJSON(error.responseText);
								if (!data.Success) showErrorMsg(data.Errors[0].Message);
							});
                        }
                    },
                    fail: function(e, data) {
                    },
                    start: function(e, data) {
                        updateProgressBar(0);
                    },
                    stop: function(e, data) {
                    }
                });
                $(".upload-button").click(function() {
					if (!fileUploadData || fileUploadData.files.length == 0) {
                        $(".nofileselected").show();
                        return;
					}
                    $(".upload-layer").dialog({
                        height: 564,
                        modal: true,
                        width: 742,
                        closeOnEscape: false,
                        resizable: false,
                        position: "center",
                        dialogClass: "upload-layer-dialog"
                    });
                    $(".upload-layer .check-icon").hide();
                    $(".upload-layer .button-holder").hide();
                    $(".upload-layer .filename").text(fileUploadData.files[0].name);
                    var fileSize = fileUploadData.files[0].size;
                    var fileType = fileUploadData.files[0].type;
                    if ($.browser.msie) {
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
                        showErrorMsg("Your resume size is too big. Please try another one");
                        return;
                    }
                    if (!(fileType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileType == "application/msword" || fileType == "application/rtf" || fileType == "application/pdf" || fileType == "text/plain" || fileType == "text/html")) {
						showErrorMsg("Your resume format is not allowed. Please try another one");
                        return;
                    }
                    $("upload-layer .prompt-text").text("Please wait while we upload your resume ...");
                    fileUploadData.submit();
                });
                $(".auto-extraction .icon").attr("src", "data:image/gif;base64,R0lGODlhIAAgALMAAP%2F%2F%2F7Ozs%2Fv7%2B9bW1uHh4fLy8rq6uoGBgTQ0NAEBARsbG8TExJeXl%2F39%2FVRUVAAAACH%2FC05FVFNDQVBFMi4wAwEAAAAh%2BQQFBQAAACwAAAAAIAAgAAAE5xDISSlLrOrNp0pKNRCdFhxVolJLEJQUoSgOpSYT4RowNSsvyW1icA16k8MMMRkCBjskBTFDAZyuAEkqCfxIQ2hgQRFvAQEEIjNxVDW6XNE4YagRjuBCwe60smQUDnd4Rz1ZAQZnFAGDd0hihh12CEE9kjAEVlycXIg7BAsMB6SlnJ87paqbSKiKoqusnbMdmDC2tXQlkUhziYtyWTxIfy6BE8WJt5YEvpJivxNaGmLHT0VnOgGYf0dZXS7APdpB309RnHOG5gDqXGLDaC457D1zZ%2FV%2FnmOM82XiHQjYKhKP1oZmADdEAAAh%2BQQFBQAAACwAAAAAGAAXAAAEchDISasKNeuJFKoHs4mUYlJIkmjIV54Soypsa0wmLSnqoTEtBw52mG0AjhYpBxioEqRNy8V0qFzNw%2BGGwlJki4lBqx1IBgjMkRIghwjrzcDti2%2FGh7D9qN774wQGAYOEfwCChIV%2FgYmDho%2BQkZKTR3p7EQAh%2BQQFBQAAACwBAAAAHQAOAAAEchDISWdANesNHHJZwE2DUSEo5SjKKB2HOKGYFLD1CB%2FDnEoIlkti2PlyuKGEATMBaAACSyGbEDYD4zN1YIEmh0SCQQgYehNmTNNaKsQJXmBuuEYPi9ECAU%2FUFnNzeUp9VBQEBoFOLmFxWHNoQw6RWEocEQAh%2BQQFBQAAACwHAAAAGQARAAAEaRDICdZZNOvNDsvfBhBDdpwZgohBgE3nQaki0AYEjEqOGmqDlkEnAzBUjhrA0CoBYhLVSkm4SaAAWkahCFAWTU0A4RxzFWJnzXFWJJWb9pTihRu5dvghl%2B%2F7NQmBggo%2FfYKHCX8AiAmEEQAh%2BQQFBQAAACwOAAAAEgAYAAAEZXCwAaq9ODAMDOUAI17McYDhWA3mCYpb1RooXBktmsbt944BU6zCQCBQiwPB4jAihiCK86irTB20qvWp7Xq%2FFYV4TNWNz4oqWoEIgL0HX%2FeQSLi69boCikTkE2VVDAp5d1p0CW4RACH5BAUFAAAALA4AAAASAB4AAASAkBgCqr3YBIMXvkEIMsxXhcFFpiZqBaTXisBClibgAnd%2BijYGq2I4HAamwXBgNHJ8BEbzgPNNjz7LwpnFDLvgLGJMdnw%2F5DRCrHaE3xbKm6FQwOt1xDnpwCvcJgcJMgEIeCYOCQlrF4YmBIoJVV2CCXZvCooHbwGRcAiKcmFUJhEAIfkEBQUAAAAsDwABABEAHwAABHsQyAkGoRivELInnOFlBjeM1BCiFBdcbMUtKQdTN0CUJru5NJQrYMh5VIFTTKJcOj2HqJQRhEqvqGuU%2Buw6AwgEwxkOO55lxIihoDjKY8pBoThPxmpAYi%2BhKzoeewkTdHkZghMIdCOIhIuHfBMOjxiNLR4KCW1ODAlxSxEAIfkEBQUAAAAsCAAOABgAEgAABGwQyEkrCDgbYvvMoOF5ILaNaIoGKroch9hacD3MFMHUBzMHiBtgwJMBFolDB4GoGGBCACKRcAAUWAmzOWJQExysQsJgWj0KqvKalTiYPhp1LBFTtp10Is6mT5gdVFx1bRN8FTsVCAqDOB9%2BKhEAIfkEBQUAAAAsAgASAB0ADgAABHgQyEmrBePS4bQdQZBdR5IcHmWEgUFQgWKaKbWwwSIhc4LonsXhBSCsQoOSScGQDJiWwOHQnAxWBIYJNXEoFCiEWDI9jCzESey7GwMM5doEwW4jJoypQQ743u1WcTV0CgFzbhJ5XClfHYd%2FEwZnHoYVDgiOfHKQNREAIfkEBQUAAAAsAAAPABkAEQAABGeQqUQruDjrW3vaYCZ5X2ie6EkcKaooTAsi7ytnTq046BBsNcTvItz4AotMwKZBIC6H6CVAJaCcT0CUBTgaTg5nTCu9GKiDEMPJg5YBBOpwlnVzLwtqyKnZagZWahoMB2M3GgsHSRsRACH5BAUFAAAALAEACAARABgAAARcMKR0gL34npkUyyCAcAmyhBijkGi2UW02VHFt33iu7yiDIDaD4%2FerEYGDlu%2FnuBAOJ9Dvc2EcDgFAYIuaXS3bbOh6MIC5IAP5Eh5fk2exC4tpgwZyiyFgvhEMBBEAIfkEBQUAAAAsAAACAA4AHQAABHMQyAnYoViSlFDGXBJ808Ep5KRwV8qEg%2BpRCOeoioKMwJK0Ekcu54h9AoghKgXIMZgAApQZcCCu2Ax2O6NUud2pmJcyHA4L0uDM%2FljYDCnGfGakJQE5YH0wUBYBAUYfBIFkHwaBgxkDgX5lgXpHAXcpBIsRADs%3D");
                $(".upload-layer .ok-button").click(function() {
                    if ($(this).parent().hasClass("error")) {
                        $(".upload-layer .prompt-text").text("Please wait while we upload your resume ...");
                        $(this).parent().removeClass("error").appendTo(".uploading-and-parsing").hide();
                        $(".errorMsg").hide();
                        $(this).prev().text("Take me to the next step");
                        $(".upload-layer").dialog("close");
                    } else $("#JoinForm").submit();
                });
                $(".find-out-more").click(function() {
                    $(".find-out-more-layer").dialog({
                        modal: true,
                        closeOnEscape: true,
                        resizable: false,
                        width: 575,
                        position: "center",
                        dialogClass: "find-out-more-layer-dialog",
                        buttons: [{
                            text: "Close",
                            click: function() {
                                $(this).dialog("close");
                            }
						}]
					});
				});
				$(".upload-tips").click(function() {
					$(".upload-tips-layer").dialog({
						modal: true,
						closeOnEscape: true,
						resizable: false,
						width: 800,
						position: "center",
						title: "Resume tips",
						dialogClass: "upload-tips-layer-dialog",
						buttons: [{
							text: "Close",
							click: function() {
								$(this).dialog("close");
							}
						}]
					});
				});
				$(".helpful-tips").click(function(event) {
					event.stopPropagation();
				});
				break;
			case "PersonalDetails":
				$(".contact .edit").hover(function() {
					$(".contact").addClass("hover");
				}, function() {
					$(".contact").removeClass("hover");
				}).click(function() {
					$(".contact").addClass("edit-mode");
				});
				$(".contact .content, .contact .footer").hover(function() {
					$(".contact").addClass("hover");
				}, function() {
					$(".contact").removeClass("hover");
				}).click(function() {
					$(".contact").addClass("edit-mode");
				});
				cancelButtonClick = function() {
					$(".contact").removeClass("edit-mode");
					$(".edit-mode .field input[type='text']").each(function() {
						$(this).val($(this).attr("reset-value"));
					});
					$("#CountryId option").removeAttr("selected");
					$("#CountryId option[value='" + $("#CountryId").attr("reset-value") + "']").attr("selected", "selected");
					$("#CountryId").prev().val($("#CountryId option:selected").text());
					$(".edit-mode .radiobutton[value='" + $("#PrimaryPhoneType").val() + "']").click();
					$(".validation-error-msg").hide();
					$.each(["FirstName", "LastName", "CountryId", "Location", "EmailAddress", "PhoneNumber"], function() {
						$("#" + this).closest(".control").removeClass("error");
						$(".help-area[helpfor='" + this + "']").removeClass("error");
					});
					$(".savefirst").hide();
				}
				$(".contact .cancel").hover(function() {
					$(".contact").toggleClass("cancel-hover");
				}, function() {
					$(".contact").toggleClass("cancel-hover");
				}).click(function() {
					cancelButtonClick();
				});
				$("#Location").autocomplete(apiPartialMatchesUrl);
				if ($(".location .text").html() == "") {
					$(".location").addClass("empty");
					$(".location .text").html("<div>Please input your location</div>");
				}
				$("#CountryId").parent().dropdown().parent().find(".dropdown-item").click(function() {
					var url = $(this).text() == "" ? "" : apiPartialMatchesUrl.substring(0, apiPartialMatchesUrl.indexOf("countryId=") + 10) + $("#CountryId option:contains('" + $(this).text() + "')").val() + "&location=";
					$("#Location").setOptions({
						url : url
					});
				});
				$("#EmailAddress").closest(".field").append("<div class='username-callout'></div>");
				var MobileRadio = $(".radiobutton[value='Mobile']").parent();
				MobileRadio.prependTo(MobileRadio.parent());
				$(".availability-icon, .availability-desc-outer").appendTo(".availability_field");
				$(".availability_control").hide();
				$(".availability-icon").hover(function() {
					$(this).addClass("hover");
					$(".availability-desc").show();
					if ($(this).hasClass("immediately-available"))
						$(".availability-desc").text("I can start work right away");
					if ($(this).hasClass("actively-looking"))
						$(".availability-desc").text("I'm looking for a new role but can't start right away");
					if ($(this).hasClass("not-looking-but-happy-to-talk"))
						$(".availability-desc").text("I'd like to hear about interesting jobs");
					if ($(this).hasClass("not-looking"))
						$(".availability-desc").text("Don't contact me about job opportunities");
				}, function() {
					$(this).removeClass("hover");
					$(".availability-desc").hide();
				}).mousedown(function() {
					$(this).addClass("mouse-down");
				}).mouseup(function() {
					$(this).removeClass("mouse-down");
				}).click(function() {
					if ($(this).hasClass("checked")) {
						$(this).removeClass("checked");
						$(".radiobutton[value='Unspecified']").click();
					} else {
						$(".availability-icon").removeClass("checked");
						$(this).addClass("checked");
						$(".radiobutton[value='" + $(this).attr("value") + "']").click();
					}
				});
				$(".save-button").click(function(event) {
					var validator = $("#PersonalDetailsForm").validate();
					var errorList = new Array();
					if (!validator.element("#FirstName")) errorList.push(validator.errorList[0]);
					if (!validator.element("#LastName")) errorList.push(validator.errorList[0]);
					if (!validator.element("#Location")) errorList.push(validator.errorList[0]);
					if (!validator.element("#EmailAddress")) errorList.push(validator.errorList[0]);
					if (!validator.element("#PhoneNumber")) errorList.push(validator.errorList[0]);
					validator.errorList = errorList;
					if (validator.errorList.length > 0) showValidationErrors(validator);
					else {
						$(".contact").removeClass("edit-mode");
						$(".edit-mode .field input[type='text']").each(function() {
							$(this).attr("reset-value", $(this).val());
						});
						$("#CountryId").attr("reset-value", $("#CountryId option:selected").val());
						$("#PrimaryPhoneType").val($(".edit-mode .radiobutton.checked").attr("value"));
						$(".contact .view-mode .name .text").text($("#FirstName").val() + " " + $("#LastName").val());
						$(".contact .view-mode .location").removeClass("empty").find(".text").html("<span>" + $("#CountryId option:selected").text() + "</span><br /><span>" + $("#Location").val() + "</span>");
						$(".contact .view-mode .primary-email .username-callout").prev().html($("#EmailAddress").val());
						$(".contact .view-mode .primary-phone .text").html("<span>" + $("#PhoneNumber").val() + "</span><span>(" + $("#PrimaryPhoneType").val() + ")</span>");
						$(".validation-error-msg").hide();
						$.each(["FirstName", "LastName", "CountryId", "Location", "EmailAddress", "PhoneNumber"], function() {
							$("#" + this).closest(".control").removeClass("error");
							$(".help-area[helpfor='" + this + "']").removeClass("error");
						});
						event.stopPropagation();
					}
					$(".savefirst").hide();
				});
				$(".cancel-button").click(function(event) {
					cancelButtonClick();
					event.stopPropagation();
				});
				$(".radiobutton[value='SalaryRateYear']").click(function() {
					initializeSalary(0, 0, 250000, 5000);
				});
				$(".radiobutton[value='SalaryRateHour']").click(function() {
					initializeSalary(0, 0, 125, 5);
				});
				$(".checkbox.ResumeVisibility").click(function() {
					if ($(this).hasClass("checked")) $(".allow-to-see-my .checkbox").removeClass("disabled").addClass("checked");
					else $(".allow-to-see-my .checkbox").addClass("disabled").removeClass("checked");
				});
				$(".checkbox_control label[for='AcceptTerms']").html("I accept the <a class='t-and-c-link' target='_blank' href='" + termsUrl + "'>terms and conditions</a>");
				$(".confirm-button, .next-button").click(function() {
					if ($(".contact").hasClass("edit-mode")) {
						$(".savefirst").show();
						return;
					}
					$("#PersonalDetailsForm").submit();
				});

				$("#AimeMemberStatus").parent().dropdown();
									   
				//help-icon
				addHelpIcon(["CountryId", "Location", "PhoneNumber", "Password", "ConfirmPassword", "AvailableNow", "ResumeVisibility", "AimeMemberStatus", "FinsiaMemberId"]);
				$(".help-icon[helpfor='AvailableNow']").insertBefore(".availability-desc-outer");
				$("label[for='Location']").append("<span class='desc'>e.g. Melbourne, VIC, 3000</span>");
				$("label[for='EmailAddress']").append("<span class='desc'>Your email address is your username login ID.</span><span class='desc red more-space'>LinkMe will never disclose your email address to employers.</span>");
				//if all contact fields are not null, click cancel
				if ($("#FirstName").val() == "" || $("#LastName").val() == "" || $("#CountryId").val() == "" || $("#Location").val() == "" || $("#EmailAddress").val() == "" || $("#PhoneNumber").val() == "")
					$(".contact .edit").click();
				//validator
				// $.validator.addMethod("matchpwd", function(value, element, param) {
					// return $(element).val() == $("#" + param).val();
				// });
				// $.validator.addMethod("empty", function(value, element, param) {
					// if ($(element).attr("id") == "Unspecified") return $(element).is(":not(:checked)") != "checked";
					// else return $(element).is(":not(:checked)") != "checked";
				// });
				if ($.browser.msie && $.browser.version.indexOf("7") == 0) {
					$(".other-details .t-and-c label[for='AcceptTerms']").attr("style", "width:200px;").closest(".field").attr("style", "margin-top:-12px;");
					$("#joinflow-content .checkbox_field.field label[for='NotSalaryVisibility']").attr("style", "font-weight:normal;");
				}
				//init page data or default value
				var currentPhoneType = $("input[name='PhoneNumberType']:checked").attr("id");
				if (currentPhoneType) $(".radiobutton[value='" + currentPhoneType + "']").click();
				else $(".radiobutton[value='Mobile']").click();
				var currentAvailabilityId = $(".availability_control").find("input:checked").attr("id");
				if (currentAvailabilityId) $(".availability-icon[value='" + currentAvailabilityId + "']").addClass("checked");
				else $("#Unspecified").click();
				var currentSalaryRateType = $("input[name='SalaryRate']:checked").attr("id");
				var currentSalary = $("#SalaryLowerBound").val();
				if (currentSalary) {
					if (currentSalaryRateType == "SalaryRateYear") initializeSalary(currentSalary, 0, 250000, 5000);
					else initializeSalary(currentSalary, 0, 125, 5);
					$(".salary-slider").slider("value", currentSalary);
				} else {
					if (currentSalaryRateType) $(".radiobutton[value='" + currentSalaryRateType + "']").click();
					else $(".radiobutton[value='SalaryRateYear']").click();
				}
				//special error msg
				if ($("#Unspecified").hasClass("input-validation-error")) {
					$("<div class='availability-error'></div>").insertBefore(".availability-icon[value='AvailableNow']");
					$(".help-area[helpfor='AvailableNow']").addClass("error");
				}
				if ($("#Location").closest(".control").hasClass("error"))
					$("#Location").closest(".field").find("help-area").addClass("error");
				if ($("#SalaryLowerBound").hasClass("input-validation-error")) {
					$("<div class='salary-error'></div>").insertBefore(".total-remuneration");
					$(".help-area[helpfor='DesiredSalary']").addClass("error");
				}
				break;
			case "JobDetails":
				$("#RecentSeniority").find("option[value='None']").appendTo("#RecentSeniority");
				$("#RecentSeniority").parent().dropdown();
				$("#RecentProfession").parent().dropdown();
				$("#IndustryIds").attr("size", "6").attr("maxcount", "5").parent().listbox();
				$("#HighestEducationLevel").find("option[value='None']").appendTo("#HighestEducationLevel");
				$("#HighestEducationLevel").parent().dropdown();
				$(".desiredjobtype_control").hide();
				$(".jobtype-icon").appendTo(".desiredjobtype_field");
				$(".jobtype-icon").hover(function() {
					$(this).addClass("hover");
				}, function() {
					$(this).removeClass("hover");
				}).mousedown(function() {
					$(this).addClass("mouse-down");
				}).mouseup(function() {
					$(this).removeClass("mouse-down");
				}).click(function() {
					if ($(this).hasClass("checked")) {
						$(this).removeClass("checked");
						$("#" + $(this).attr("value")).removeAttr("checked");
					} else {
						$(this).addClass("checked");
						$("#" + $(this).attr("value")).attr("checked", "checked");
					}
				});
                //special error msg
                if ($("#Male").hasClass("input-validation-error")) {
                    $("<div class='gender-error'></div>").insertBefore(".radiobutton[value='Male']");
                }
                if ($("#DateOfBirth").hasClass("input-validation-error")) {
                    $("<div class='date-of-birth-error'></div>").insertBefore(".help-area[helpfor='DateOfBirthYear']");
                }
                $(".radiobutton[value^='RelocationPreference']").click(function () {
					if ($(this).attr("value") == "RelocationPreferenceNo") $("#WhereToRelocate").hide();
					else if ($(this).attr("value") == "RelocationPreferenceYes" || $(this).attr("value") == "RelocationPreferenceWouldConsider") $("#WhereToRelocate").show();
				});
				$("#CountryId").parent().dropdown().closest(".field").find(".dropdown-item:contains('Australia')").click();
				$("#CountryId").parent().closest(".field").find(".dropdown-item").click(function() {
					if ($(this).text() == "Australia") {
						$(".aus-map").show();
						$("#AnyWhereInAus").show();
					} else {
						$(".aus-map").hide();
						$("#AnyWhereInAus").hide();
						$("#RelocationLocation").parent()[0].addItem("Anywhere in " + $(this).text());
					}
				});
				//$("img[usemap='#mapAus']").maphilight().prev().width("400px").height("400px").css("width", "400px").css("height", "400px");
				$("#RelocationCountryIds option").each(function() {
					$(this).text("Anywhere in " + $(this).text());
				});
				$("#RelocationLocation").parent().shoppinglist();
				$("map[name='mapAus'] area").hover(function() {
					$(".aus-map .state[state='" + $(this).attr("areafor") + "'], .aus-map .region[region='" + $(this).attr("areafor") + "']").addClass("hover");
					$(".aus-map .hint[hintfor='" + $(this).attr("areafor") + "']").show();
				}, function() {
					$(".aus-map .state[state='" + $(this).attr("areafor") + "'], .aus-map .region[region='" + $(this).attr("areafor") + "']").removeClass("hover");
					$(".aus-map .hint[hintfor='" + $(this).attr("areafor") + "']").hide();
				}).click(function() {
					var currentStateOrRegion = $($(".aus-map .state[state='" + $(this).attr("areafor") + "'], .aus-map .region[region='" + $(this).attr("areafor") + "']")[0]);
					var text = currentStateOrRegion.hasClass("state") ? currentStateOrRegion.attr("state") : currentStateOrRegion.attr("region");
					var shoppinglist = $("#RelocationLocation").parent()[0];
					currentStateOrRegion.toggleClass("active");
					/*logic: 1.AnyWhere unchecked:+/- current item;
					2.AnyWhere checked: - region/state in shopping cart, -AnyWhere in shopping cart, +All regions/states except current
					*/
					if ($("#AnyWhereInAus .checkbox").hasClass("checked")) {
						$("#AnyWhereInAus .checkbox").removeClass("checked");
						$(".shoppinglist-item:contains('Anywhere in Australia')", shoppinglist).remove();
						$("option:contains('Anywhere in Australia')", shoppinglist).removeAttr("selected");
						$(".shoppinglist-item:contains('" + text + "')", shoppinglist).remove();
						$("option:contains('" + text + "')", shoppinglist).removeAttr("selected");
						$(".aus-map .state.active").each(function() {
							shoppinglist.addItem($(this).attr("state"));
						});
						$(".aus-map .region.active").each(function() {
							if ($(this).attr("region") == "Australian Capital Territory") {
								shoppinglist.addItem("Australian Capital Territory");
								shoppinglist.addItem("Canberra");
							} else shoppinglist.addItem($(this).attr("region"));
						});
					} else {
						if (currentStateOrRegion.hasClass("active")) {
							if (text == "Australian Capital Territory") {
								shoppinglist.addItem(text);
								shoppinglist.addItem("Canberra");
							} else shoppinglist.addItem(text);
						} else {
							if (text == "Australian Capital Territory") {
								shoppinglist.deleteItem(text);
								shoppinglist.deleteItem("Canberra");
							} else shoppinglist.deleteItem(text);
						}
					}
					//when we select the state and region one by one and when we click the last state or region (which cause all states and regions are selected), the Item “Anywhere in Australia” will automatically be added to shopping list for consistency.
					if ($(".aus-map .state:not(.active), .aus-map .region[region!='Canberra']:not(.active)").length == 0)
						$("#AnyWhereInAus").click();
				});
				$("#AnyWhereInAus").click(function() {
					if ($(".checkbox", this).hasClass("checked")) {
						$(".checkbox", this).removeClass("checked");
						$(".aus-map .state, .aus-map .region").removeClass("active");
						$("#RelocationLocation").parent()[0].deleteItem("Anywhere in Australia");
						$(".shoppinglist-item", $("#RelocationLocation").parent()).each(function() {
							var text = $("span", this).text();
							$(".aus-map .state[state='" + text + "'], .aus-map .region[region='" + text + "']").addClass("active");
							if (text == "Australian Capital Territory") $(".aus-map .region[region='Australian Capital Territory']").addClass("active");
						});
					} else {
						$(".checkbox", this).addClass("checked");
						$("#RelocationLocation").parent()[0].addItem("Anywhere in Australia");
						$(".aus-map .state, .aus-map .region").addClass("active");
					}
				});
				var MobileRadio = $(".radiobutton[value='Mobile']").parent();
				MobileRadio.prependTo(MobileRadio.parent());
				$("#NotApplicable").parent().appendTo(".Visa_control");
				$("#Unspecified").parent().hide();
				var field = $("#DateOfBirthMonth").closest(".field");
				$("<div class='dropdown_control DOBMonth_control control'><div class='textbox-bg'><input type='text' readonly='readonly'></div></div>").appendTo(field).find(".textbox-bg").append($("#DateOfBirthMonth"));
				$("<div class='dropdown_control DOBYear_control control'><div class='textbox-bg'><input type='text' readonly='readonly'></div></div>").appendTo(field).find(".textbox-bg").append($("#DateOfBirthYear"));
				$(".control label", field).parent().remove();
				$("#DateOfBirthMonth").parent().dropdown();
				// for (var i = new Date().getFullYear(); i >= 1900; i--)
				// $("#DateOfBirthYear").append("<option value='" + i + "'>" + i + "</option>");
				$(".DOBMonth_control .dropdown-scrollpan").before("<div class='right-edge'></div>");
				$("#DateOfBirthYear").parent().dropdown();
				$(".DOBYear_control .dropdown-scrollpan").before("<div class='right-edge'></div>");
				$("#ExternalReferralSourceId").parent().dropdown();
				$("label[for='IndustryIds']").append("<br/><span class='desc'>Select up to five.</span>");
				$("label[for='DesiredJobTitle']").append("<span class='desc'>Separate multiple titles with commas</span>");
				$("label[for='CountryId']").append("<span class='desc red'>Maps not to scale</span>");
				$("label[for='SecondaryEmailAddress']").append("<span class='desc'>(use commas to separate multiple emails)</span>");
				$(".private-info .radiobuttons_field.field > label").append("<span class='desc red'>LinkMe will never disclose your gender to employers</span>");
				$(".private-info .field:not(.radiobuttons_field) > label").append("<span class='desc red'>LinkMe will never disclose your age or date of birth to employers</span>");
				//help-icon
				addHelpIcon(["JobTitle", "JobCompany", "RecentSeniority", "RecentProfession", "IndustryIds", "HighestEducationLevel", "DesiredJobTitle", "FullTime", "TorresIslander", "RelocationPreferenceWouldConsider", "CountryId", "SecondaryEmailAddress", "SecondaryPhoneNumber", "Citizenship", "NotApplicable", "Female", "DateOfBirthYear", "SendSuggestedJobs", "ExternalReferralSourceId"]);
				$(".finish-button").click(function() {
					$("#JobDetailsForm").submit();
				});
				if ($.browser.msie && $.browser.version.indexOf("7") == 0) {
					$("label[for='Female']").attr("style", "width:217px;");
				}
				//init page data or default value
				$(".desiredjobtype_control input:checked").each(function() {
					$(".jobtype-icon[value='" + $(this).attr("id") + "']").addClass("checked");
				});
				var relocationPreferenceId = $(".relocate-section .radiobuttons_field input:checked").attr("id");
				if (relocationPreferenceId) $(".radiobutton[value='" + relocationPreferenceId + "']").click();
				$("#RelocationLocation option:selected").each(function() {
					$("#RelocationLocation").parent()[0].addItem($(this).text());
					if ($(this).parent().attr("id") == "RelocationCountryLocationIds") {
						$(".aus-map div[state='" + $(this).text() + "'], .aus-map div[region='" + $(this).text() + "']").addClass("active");
					}
					if ($(this).text() == "Anywhere in Australia") {
						$("#AnyWhereInAus .checkbox").addClass("checked");
						$(".aus-map div.state, .aus-map div.region").addClass("active");
					}
				});
				break;
			case "Activate":
				$(".view-profile-button, .complete-profile-button").click(function() {
					window.location = $(this).attr("url");
				});
				break;
		}
		$(".help-icon").hover(function() {
			var helparea = $(".help-area[helpfor='" + $(this).attr("helpfor") + "']");
			if (helparea.hasClass("stay")) return;
			var marginTop = Number($(this).css("margin-top").substring(0, $(this).css("margin-top").length - 2));
			var marginLeft = Number($(this).css("margin-left").substring(0, $(this).css("margin-left").length - 2));
			helparea.css({
				left: $(this).position().left + marginLeft + 20,
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
				helparea.css({
					left: $(this).position().left + marginLeft + 20,
					top: $(this).position().top + marginTop - 8
				});
				helparea.show().addClass("stay");
			}
		});
	}

	showValidationErrors = function(validator) {
		/*		var errCount = validator.numberOfInvalids();
		var msgSummary;
		if (errCount == 0) return;
		if (errCount == 1) msgSummary = "There is 1 error, please correct it below.";
		else msgSummary = "There are " + errCount + " errors, please correct them below.";
		$(".validation-error-msg").show();
		$(".validation-error-msg .error-summary").text(msgSummary);
		$(".validation-error-msg ul").empty();
		$.each(validator.errorList, function() {
		$("<li><div></div>" + this.message + "</li>").appendTo($(".validation-error-msg ul"));
		});						
		*/
	}

	addHelpIcon = function(helpForIDs) {
		$.each(helpForIDs, function() {
			var helpIcon = $("<div class='help-icon' helpfor='" + this + "'></div>");
			$("#" + this).closest(".field").append(helpIcon);
		});
	}

	organiseFields = function() {
		$("#joinflow-content fieldset").removeClass("forms_v2");
		$(".control select:not(.listbox, .month_dropdown, .year_dropdown)").each(function() {
			var textBg = $("<div class='textbox-bg'><input type='text' /></div>");
			var control = $(this).parent();
			$(this).appendTo(textBg);
			control.text("").append(textBg);
		});
		$(".control .textbox").each(function() {
			var textBg = $("<div class='textbox-bg'></div>");
			var control = $(this).parent();
			$(this).appendTo(textBg);
			control.text("").append(textBg);
		});
		$(".checkbox_control").each(function() {
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
		$(".checkboxes_control input[type='checkbox']").each(function() {
			var checkbox = $("<div class='checkbox'></div>");
			var id = $(this).hide().attr("id");
			checkbox.addClass(id).insertAfter(this);
			if ($(this).is(":checked")) checkbox.addClass("checked");
			checkbox.click(function(event) {
				if ($(this).hasClass("disabled")) return;
				if ($(this).hasClass("checked")) {
					$("#" + id, $(this).parent()).removeAttr("checked");
					$(this).removeClass("checked");
				} else {
					$("#" + id, $(this).parent()).attr("checked", "checked");
					$(this).addClass("checked");
				}
				event.stopPropagation();
			});
		});
		$(".radiobuttons_control").each(function() {
			var radioButtonGroup = $(this);
			$(".radio_control", this).each(function() {
				var radiobutton = $("<div class='radiobutton'></div>");
				var id = $("input[type='radio']", this).hide().attr("id");
				radiobutton.attr("value", id).prependTo(this);
				if ($("input[type='radio']", this).attr("checked") == "checked")
					radiobutton.addClass("checked");
				radiobutton.click(function(event) {
					$(".radiobutton", radioButtonGroup).removeClass("checked");
					$(this).addClass("checked");
					$("#" + id).attr("checked", "checked");
					event.stopPropagation();
				});
			});
		});
		$(".compulsory_field").each(function() {
			var mandatory = $("<div class='mandatory'></div>");
			mandatory.insertAfter($("> label", this));
		});
	}

	toFormattedDigits = function(nValue) {
		var s = nValue.toString();
		for (j = 3; j < 15; j += 4) {  // insert commas
			if ((s.length > j) && (s.length >= 4)) {
				s = s.substr(0, s.length - j) + ',' + s.substr(s.length - j);
			}
		}
		return (s);
	}

	initializeSalary = function(lowerBound, minSalary, maxSalary, stepSalary) {

		setSalarySliderLabel = function(lowerBound) {
			var label = "";

			if (lowerBound == minSalary) label = "Select a minimum salary";
			else if (lowerBound == maxSalary) label = "$" + toFormattedDigits(lowerBound) + "+ " + $(".salary-section .radiobutton.checked").parent().find("label").text();
			else label = "$" + toFormattedDigits(lowerBound) + " " + $(".salary-section .radiobutton.checked").parent().find("label").text(); ;

			$(".salary-desc center").html(label);
			//set background position
			var percentage = lowerBound / maxSalary;
			var bgPosX = 0 - 1354;
			var max = 100;
			$(".salary-slider .ui-slider-range").css({
				"background-position" : (bgPosX - 276 * percentage) + "px -321px",
				"left" : (percentage * 100) + "%",
				"width" : (max - percentage * 100) + "%"
			});
			if (lowerBound == minSalary) $("#SalaryLowerBound").val("");
			else $("#SalaryLowerBound").val(lowerBound);
		}

		$(".salary-slider").slider({
			range: "max",
			min: minSalary,
			max: maxSalary,
			step: stepSalary,
			value: minSalary,
			slide: function(event, ui) {
				setSalarySliderLabel(ui.value);
			}
		});

		$(".salary-range .left-range").html('$' + toFormattedDigits(minSalary));
		$(".salary-range .right-range").html('$' + toFormattedDigits(maxSalary) + '+');
		setSalarySliderLabel(lowerBound);
	}

})(jQuery);

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
				if (scrollpan.css("display") == "none") {
					$(".dropdown-scrollpan").hide();
					var parentWidth = dd.parent().width();
					if ($(".right-edge", dd.parent()).length > 0) parentWidth += 3;
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
			if (type == "listbox") item = $("<div class='" + type + "-item'><div class='checkbox'></div>" + $(this).text() + "</div>");
			item.click(function (e) {
				if (type == "dropdown") {
					scrollpan.hide();
					var index = $("." + type + "-item", $(this).parent()).index(this);
					$("select option:nth-child(" + (index + 1) + ")", parent).attr("selected", "selected");
					$("input[type='text']", parent).val($(this).text());
				}
				if (type == "listbox") {
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
			scrollbarWrap.append(scrollbar);
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
			});
			scrollbarWrap.height(size * itemHeight - 23);
			scrollbar.height(size * itemHeight - handleHeight - 23);
			items.width(parent.width() - scrollbarWidth - 1);
			scrollbarWrap.mousedown(function(e) {
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

(function ($) {
	$.fn.shoppinglist = function() {
		return this.each(function() {
			var sl = $(this);
			
			sl.addClass("shoppinglist").find("select").hide();
			
			this.addItem = function(text) {
				var option = $("option:contains('" + text + "')", sl);
				if ($(".shoppinglist-item[key='" + option.val() + "']").length > 0) return;
				var item = $("<div class='shoppinglist-item' key='" + option.val() + "'></div>");
				var textSpan = $("<span>" + text + "</span>");
				var deleteIcon = $("<div class='delete-icon'></div>");
				
				option.attr("selected", "selected");
				item.append(textSpan).append(deleteIcon);
				sl.prepend(item);
				
				deleteIcon.click(function() {
					sl[0].deleteItem(text);
					if (text == "Anywhere in Australia") $("#AnyWhereInAus").click();
					else if (text.indexOf("Anywhere") == 0) {
						//when you remove an non-Australia country from shopping list and that country is currently selected (appear in dropdown list, say China in this case), then the dropdown should automatically be switched to Australia (as default)
						if (text.indexOf($("#CountryId").prev().val()) > 0) {
							$("#CountryId").closest(".field").find(".dropdown-item:contains('Australia')").click();
						}
					} else if (text == "Australian Capital Territory") $("map[name='mapAus'] area[areafor='Australian Capital Territory']").click();
					else $("map[name='mapAus'] area[areafor='" + text + "']").click();
				});
			};
			
			this.deleteItem = function (text) {
				var option = $("option:contains('" + text + "')", sl);
				$(".shoppinglist-item:contains('" + text + "')", sl).remove();
				option.removeAttr("selected");				
			};
		});
	};
})(jQuery);