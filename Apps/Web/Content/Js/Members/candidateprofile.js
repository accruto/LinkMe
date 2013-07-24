(function($) {
    $(document).ready(function() {
        $(".profile-header").after($(".breadcrumbs"));
        updateLeftSection();
        updateVisibility();
        $(".section").click(function() {
            if ($(".current", this).length > 0) return;
            if ($(".content").hasClass("edit-mode")) {
                if (checkDataUpdated()) {
                    showErrInfo("changeSection");
                    return;
                }
            }
            if (currentRequest) return;
            getProfileDetails($(".six-section").attr("profileUrl"), $(this).attr("url"), false);
            $(".fg", this).before($(".six-section .current"));
        });
        $(".succ-info .close-icon, .err-info .close-icon").click(function() {
            $(this).closest(".bg").parent().hide();
        });
        $(".resumeage .setcurrent a").click(function() {
            if (currentRequest)
                currentRequest.abort();

            currentRequest = $.post($(this).attr("url"),
				{},
				function(data, textStatus, xmlHttpRequest) {
				    if (data == "") {
				        showErrInfo("setCurrent");
				        return;
				    } else if (data.Success) {
				        $.extend(true, candidateProfileModel, data.Profile);
				        showSuccInfo("setCurrent", "");
				    } else {
				        showErrInfo("setCurrent", data.Errors);
				    }
				    currentRequest = null;
				}
			).error(function(error) {
			    var data = $.parseJSON(error.responseText);
			    if (!data.Success) showErrInfo("setCurrent", data.Errors);
			});
            return false;
        });
        $(".resumeage .help-setcurrent").click(function() {
            $(".resumeage .setcurrent-hint").show();
        });
        $(".resumeage .setcurrent-hint a").click(function() {
            $(".resumeage .setcurrent-hint").hide();
            return false;
        });
        $(".user-action .icon.view").click(function() {
            $("#preview .resume-part").each(function() {
                $(this).html("");
                getProfileDetails($(".six-section").attr("profileUrl"), $(this).attr("partType"), true, false);
            });
            $("#header, #subheader, #left-sidebar, .lastupdate, .succ-info, .err-info, #results, #footer").hide();
            $("#preview").show().find(".loading").show();
        });
        $(".user-action .icon.replace").click(function() {
            $(".upload-layer .prompt-text").text("");
            $(".upload-layer .auto-extraction").hide();
            $(".upload-layer .errorMsg").hide();
            $(".upload-layer .check-icon").hide();
            $(".upload-layer .uploading-and-parsing .button-holder").hide();
            $(".upload-layer").dialog({
                height: 357,
                modal: true,
                width: 692,
                closeOnEscape: false,
                resizable: false,
                position: "center",
                title: "Upload new resume",
                dialogClass: "upload-layer-dialog"
            });
            updateUploadProgressBar(0);
        });
        $(".user-action .icon.email").click(function() {
            if (currentRequest)
                currentRequest.abort();

            currentRequest = $.post($(this).attr("url"),
				{},
				function(data, textStatus, xmlHttpRequest) {
				    if (data == "") {
				        showErrInfo("sendResume");
				        return;
				    } else if (data.Success) {
				        showSuccInfo("sendResume", contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.EmailAddress]);
				    } else {
				        showErrInfo("sendResume", data.Errors);
				    }
				    currentRequest = null;
				}
			).error(function(error) {
			    var data = $.parseJSON(error.responseText);
			    if (!data.Success) showErrInfo("sendResume", data.Errors);
			});
        });
        $(".user-action .icon.print").click(function() {
            $("#preview .resume-part").each(function() {
                $(this).html("");
                getProfileDetails($(".six-section").attr("profileUrl"), $(this).attr("partType"), true, true);
            });
            $("#header, #subheader, #left-sidebar, .lastupdate, .succ-info, .err-info, #results, #footer").hide();
            $("#preview").show().find(".loading").show();
        });
        $(".user-action .icon.doc").click(function() {
            window.location.href = $(this).attr("url");
        });
        $(".set-visibility .text-on, .set-visibility .text-off").click(function() {
            if ($(this).hasClass("active")) return;
            if ($(".set-visibility .text-on").hasClass("active")) {
                $(".set-visibility .text-on").removeClass("active");
                $(".set-visibility .switch-icon").addClass("off").removeClass("on");
                $(".set-visibility .text-off").addClass("active");
            } else {
                $(".set-visibility .text-on").addClass("active");
                $(".set-visibility .switch-icon").addClass("on").removeClass("off");
                $(".set-visibility .text-off").removeClass("active");
            }
            saveCandidateProfileData($(".visibility .fg .save").attr("url"), "switchVisibility");
        });
        $(".set-visibility .setup-icon").click(function() {
            if ($(this).hasClass("active")) {
                $(this).removeClass("active");
                $(".visibility").hide();
            } else {
                $(this).addClass("active");
                $(".visibility").show();
            }
        });
        $(".visibility .fg .checkbox_control").each(function() {
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
        $(".visibility .fg .save").click(function() {
            saveCandidateProfileData($(this).attr("url"), "saveVisibility");
        });
        $(".visibility .fg .cancel").click(function() {
            $(".set-visibility .setup-icon").removeClass("active");
            $(".visibility").hide();
        });
        $(".section[url='contactdetails']").click();
        $(".resume-tips").click(function() {
            $(".upload-tips-layer").dialog({
                modal: true,
                closeOnEscape: true,
                resizable: false,
                width: 800,
                position: "center",
                dialogClass: "upload-tips-layer-dialog",
                buttons: [{
                    text: "Close",
                    click: function() {
                        $(this).dialog("close");
                    }
				}]
			});
		});
		$(".upload-layer .cancel, .upload-layer .uploading-and-parsing .ok-button").click(function() {
			$(".upload-layer").dialog("close");
		});
		$(".upload-layer .browse-button input").change(function() {
			var fakeFilepath = $(this).val();
			var index = fakeFilepath.lastIndexOf("\\");
			var filename = fakeFilepath.substring(index + 1);
			$("#ResumePath").val(filename);
		});
		var fileUploadData;
		$("#resumeupload").fileupload({
			url: $("#resumeupload").attr("url"),
			type: "POST",
			dataType: "json",
			namespace: "linkme.profile.resume.upload",
			fileInput: $("#resumeupload .browse-button input[type='file']"),
			replaceFileInput: false,
			limitMultiFileUploads: 1,
			add: function(e, data) {
				fileUploadData = data;
			},
			progress: function(e, data) {
				var percentage = parseInt(data.loaded / data.total * 100, 10);
				updateUploadProgressBar(percentage);
			},
			done: function(e, data) {
				if (data.result.Success) {
					updateUploadProgressBar(100);
					$(".upload-layer .auto-extraction").show();
					//ajax call parsing
					$.post($("#resumeupload").attr("parseUrl"), {
						fileReferenceId: data.result.Id
					}, function(data, textStatus, xmlHttpRequest) {
						if (data.Success) {
							//show ok button
							$(".upload-layer .prompt-text").text("Your resume has been uploaded and details have been extracted.");
							$(".upload-layer .auto-extraction").hide();
							$(".upload-layer .check-icon").show();
							$(".upload-layer .uploading-and-parsing .button-holder").show();
							$.extend(true, candidateProfileModel, data.Profile);
							updateLeftSection();
						} else {
							showUploadErrorMsg(data.Errors[0].Message);
						}
					}).error(function(error) {
						var data = $.parseJSON(error.responseText);
						if (!data.Success) showUploadErrorMsg(data.Errors[0].Message);
					});
				}
			},
			fail: function(e, data) {
			},
			start: function(e, data) {
			},
			stop: function(e, data) {
			}
		});
		$(".upload-layer .upload").click(function() {
			if (!(fileUploadData && fileUploadData.files.length > 0)) {
				showUploadErrorMsg("Your should browse a resume file from your computer.");
				return;
			}
			$(".upload-layer .filename").text(fileUploadData.files[0].name);
			$(".errorMsg").hide();
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
				showUploadErrorMsg("Your resume is too big. Please try another one");
				return;
			}
			if (!(fileType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileType == "application/msword" || fileType == "application/rtf" || fileType == "application/pdf" || fileType == "text/plain" || fileType == "text/html")) {
				showUploadErrorMsg("Your resume format is not allowed. Please try another one");
				return;
			}
			$("upload-layer .prompt-text").text("Please wait while we upload your resume ...");
			fileUploadData.submit();
		});
		$(".auto-extraction .icon, #preview .loading img").attr("src", "data:image/gif;base64,R0lGODlhIAAgALMAAP%2F%2F%2F7Ozs%2Fv7%2B9bW1uHh4fLy8rq6uoGBgTQ0NAEBARsbG8TExJeXl%2F39%2FVRUVAAAACH%2FC05FVFNDQVBFMi4wAwEAAAAh%2BQQFBQAAACwAAAAAIAAgAAAE5xDISSlLrOrNp0pKNRCdFhxVolJLEJQUoSgOpSYT4RowNSsvyW1icA16k8MMMRkCBjskBTFDAZyuAEkqCfxIQ2hgQRFvAQEEIjNxVDW6XNE4YagRjuBCwe60smQUDnd4Rz1ZAQZnFAGDd0hihh12CEE9kjAEVlycXIg7BAsMB6SlnJ87paqbSKiKoqusnbMdmDC2tXQlkUhziYtyWTxIfy6BE8WJt5YEvpJivxNaGmLHT0VnOgGYf0dZXS7APdpB309RnHOG5gDqXGLDaC457D1zZ%2FV%2FnmOM82XiHQjYKhKP1oZmADdEAAAh%2BQQFBQAAACwAAAAAGAAXAAAEchDISasKNeuJFKoHs4mUYlJIkmjIV54Soypsa0wmLSnqoTEtBw52mG0AjhYpBxioEqRNy8V0qFzNw%2BGGwlJki4lBqx1IBgjMkRIghwjrzcDti2%2FGh7D9qN774wQGAYOEfwCChIV%2FgYmDho%2BQkZKTR3p7EQAh%2BQQFBQAAACwBAAAAHQAOAAAEchDISWdANesNHHJZwE2DUSEo5SjKKB2HOKGYFLD1CB%2FDnEoIlkti2PlyuKGEATMBaAACSyGbEDYD4zN1YIEmh0SCQQgYehNmTNNaKsQJXmBuuEYPi9ECAU%2FUFnNzeUp9VBQEBoFOLmFxWHNoQw6RWEocEQAh%2BQQFBQAAACwHAAAAGQARAAAEaRDICdZZNOvNDsvfBhBDdpwZgohBgE3nQaki0AYEjEqOGmqDlkEnAzBUjhrA0CoBYhLVSkm4SaAAWkahCFAWTU0A4RxzFWJnzXFWJJWb9pTihRu5dvghl%2B%2F7NQmBggo%2FfYKHCX8AiAmEEQAh%2BQQFBQAAACwOAAAAEgAYAAAEZXCwAaq9ODAMDOUAI17McYDhWA3mCYpb1RooXBktmsbt944BU6zCQCBQiwPB4jAihiCK86irTB20qvWp7Xq%2FFYV4TNWNz4oqWoEIgL0HX%2FeQSLi69boCikTkE2VVDAp5d1p0CW4RACH5BAUFAAAALA4AAAASAB4AAASAkBgCqr3YBIMXvkEIMsxXhcFFpiZqBaTXisBClibgAnd%2BijYGq2I4HAamwXBgNHJ8BEbzgPNNjz7LwpnFDLvgLGJMdnw%2F5DRCrHaE3xbKm6FQwOt1xDnpwCvcJgcJMgEIeCYOCQlrF4YmBIoJVV2CCXZvCooHbwGRcAiKcmFUJhEAIfkEBQUAAAAsDwABABEAHwAABHsQyAkGoRivELInnOFlBjeM1BCiFBdcbMUtKQdTN0CUJru5NJQrYMh5VIFTTKJcOj2HqJQRhEqvqGuU%2Buw6AwgEwxkOO55lxIihoDjKY8pBoThPxmpAYi%2BhKzoeewkTdHkZghMIdCOIhIuHfBMOjxiNLR4KCW1ODAlxSxEAIfkEBQUAAAAsCAAOABgAEgAABGwQyEkrCDgbYvvMoOF5ILaNaIoGKroch9hacD3MFMHUBzMHiBtgwJMBFolDB4GoGGBCACKRcAAUWAmzOWJQExysQsJgWj0KqvKalTiYPhp1LBFTtp10Is6mT5gdVFx1bRN8FTsVCAqDOB9%2BKhEAIfkEBQUAAAAsAgASAB0ADgAABHgQyEmrBePS4bQdQZBdR5IcHmWEgUFQgWKaKbWwwSIhc4LonsXhBSCsQoOSScGQDJiWwOHQnAxWBIYJNXEoFCiEWDI9jCzESey7GwMM5doEwW4jJoypQQ743u1WcTV0CgFzbhJ5XClfHYd%2FEwZnHoYVDgiOfHKQNREAIfkEBQUAAAAsAAAPABkAEQAABGeQqUQruDjrW3vaYCZ5X2ie6EkcKaooTAsi7ytnTq046BBsNcTvItz4AotMwKZBIC6H6CVAJaCcT0CUBTgaTg5nTCu9GKiDEMPJg5YBBOpwlnVzLwtqyKnZagZWahoMB2M3GgsHSRsRACH5BAUFAAAALAEACAARABgAAARcMKR0gL34npkUyyCAcAmyhBijkGi2UW02VHFt33iu7yiDIDaD4%2FerEYGDlu%2FnuBAOJ9Dvc2EcDgFAYIuaXS3bbOh6MIC5IAP5Eh5fk2exC4tpgwZyiyFgvhEMBBEAIfkEBQUAAAAsAAACAA4AHQAABHMQyAnYoViSlFDGXBJ808Ep5KRwV8qEg%2BpRCOeoioKMwJK0Ekcu54h9AoghKgXIMZgAApQZcCCu2Ax2O6NUud2pmJcyHA4L0uDM%2FljYDCnGfGakJQE5YH0wUBYBAUYfBIFkHwaBgxkDgX5lgXpHAXcpBIsRADs%3D");
		$("#preview .icon.print").click(function() {
			window.print();
		});
		$("#preview .icon.back").click(function() {
			$("#preview").hide();
			$("#preview .resume-part").html("");
			var section = $(".six-section .current").closest(".section").attr("url");
			getProfileDetails($(".six-section").attr("profileUrl"), section, false);
			$("#header, #subheader, #left-sidebar, .lastupdate, #results, #footer").show();
		});
	});

	checkDataUpdated = function() {
		var section = $(".six-section .current").closest(".section").attr("url");
		switch (section) {
			case "contactdetails":
				if (contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.FirstName] != $("#FirstName").val() || contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.LastName] != $("#LastName").val() || contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.CountryId] != $("#CountryId option:selected").val() || contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.Location] != $("#Location").val() || contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.EmailAddress] != $("#EmailAddress").val() || contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.SecondaryEmailAddress] != $("#SecondaryEmailAddress").val() || contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhoneNumber] != $("#PhoneNumber").val() || contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhoneNumberType] != $("input[type='radio'][name='PhoneNumberType']:checked").val() || contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.SecondaryPhoneNumber] != $("#SecondaryPhoneNumber").val() || contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.SecondaryPhoneNumberType] != $("input[type='radio'][name='SecondaryPhoneNumberType']:checked").val() || contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.Citizenship] != $("#Citizenship").val() || contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.VisaStatus] != ($("input[type='radio'][name='VisaStatus']:checked").val() == undefined ? "" : $("input[type='radio'][name='VisaStatus']:checked").val()) || contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.Aboriginal] != ($("#Aboriginal:checked").val() == undefined ? "" : $("#Aboriginal:checked").val()) || contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.TorresIslander] != ($("#TorresIslander:checked").val() == undefined ? "" : $("#TorresIslander:checked").val()) || contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.Gender] != $("input[type='radio'][name='Gender']:checked").val() || contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.DateOfBirthMonth] != $("#DateOfBirthMonth option:selected").val() || contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.DateOfBirthYear] != $("#DateOfBirthYear option:selected").val()) return true;
				break;
			case "desiredjob":
				if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.DesiredJobTitle] != $("input[name='DesiredJobTitle']").val() || desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.FullTime] != ($("#FullTime:checked").length > 0 ? $("#FullTime:checked").val() : false) || desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.PartTime] != ($("#PartTime:checked").length > 0 ? $("#PartTime:checked").val() : false) || desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.Contract] != ($("#Contract:checked").length > 0 ? $("#Contract:checked").val() : false) || desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.Temp] != ($("#Temp:checked").length > 0 ? $("#Temp:checked").val() : false) || desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.JobShare] != ($("#JobShare:checked").length > 0 ? $("#JobShare:checked").val() : false) || desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.Status] != ($("input[type='radio'][name='Status']:checked").val()) || desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.DesiredSalaryLowerBound] != $("#SalaryLowerBound").val() || desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.DesiredSalaryRate] != $("input[type='radio'][name='DesiredSalaryRate']:checked").val() || (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.IsSalaryNotVisible] == undefined ? false : desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.IsSalaryNotVisible]) != ($("#IsSalaryNotVisible:checked").length > 0 ? $("#IsSalaryNotVisible:checked").val() : false) || desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.EmailSuggestedJobs] != ($("#SendSuggestedJobs:checked").val() == undefined ? "" : $("#SendSuggestedJobs:checked").val()) || desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.RelocationPreference] != $("input[type='radio'][name='RelocationPreference']:checked").val() || desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.RelocationCountryIds] != ($("#RelocationCountryIds").val() == null ? "" : $("#RelocationCountryIds").val())) return true;
				if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.RelocationPreference] != "No") {
					if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.RelocationCountryLocationIds].length != $("#RelocationCountryLocationIds").val().length) return true;
					var oldRelocation = desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.RelocationCountryLocationIds].sort(function (a, b) { return a - b; });
					var newRelocation = $.map($("#RelocationCountryLocationIds").val().sort(function(a, b) {
						return parseInt(a) - parseInt(b);
					}), function(value, index) {
						return parseInt(value);
					});
					if (oldRelocation.join() != newRelocation.join()) return true;
				}
				break;
			case "careerobjectives":
				if (careerObjectivesMemberModel[candidateProfileKeys.CareerObjectivesKeys.Objective] != $("#Objective").val() || careerObjectivesMemberModel[candidateProfileKeys.CareerObjectivesKeys.Summary] != $("#Summary").val() || careerObjectivesMemberModel[candidateProfileKeys.CareerObjectivesKeys.Skills] != $("#Skills").val()) return true;
				break;
			case "employmenthistory":
				var jobs = employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.Jobs];
				var job;
				$.each(jobs, function() {
					if (this[candidateProfileKeys.EmploymentHistoryKeys.Id] == $(".content.employmenthistory.edit-mode").attr("item_id")) job = this;
				});
				if (employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.RecentProfession] != $("#RecentProfession option:selected").val() || employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.RecentSeniority] != $("#RecentSeniority option:selected").val() || employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.IndustryIds].toString() != ($("#IndustryIds").val() == null ? $("#IndustryIds").serializeArray().toString() : $("#IndustryIds").val().toString()) || (job[candidateProfileKeys.EmploymentHistoryKeys.StartDateMonth] == null ? "" : job[candidateProfileKeys.EmploymentHistoryKeys.StartDateMonth]) != $("#StartDateMonth option:selected").val() || (job[candidateProfileKeys.EmploymentHistoryKeys.StartDateYear] == null ? "" : job[candidateProfileKeys.EmploymentHistoryKeys.StartDateYear]) != $("#StartDateYear option:selected").val() || (job[candidateProfileKeys.EmploymentHistoryKeys.EndDateMonth] == null ? "" : job[candidateProfileKeys.EmploymentHistoryKeys.EndDateMonth]) != $("#EndDateMonth option:selected").val() || (job[candidateProfileKeys.EmploymentHistoryKeys.EndDateYear] == null ? "" : job[candidateProfileKeys.EmploymentHistoryKeys.EndDateYear]) != $("#EndDateYear option:selected").val() || (job[candidateProfileKeys.EmploymentHistoryKeys.IsCurrent] == "" ? true : job[candidateProfileKeys.EmploymentHistoryKeys.IsCurrent]) != ($("#EndDateMonth option[value='']").attr("selected") == "selected") || job[candidateProfileKeys.EmploymentHistoryKeys.Title] != $("#Title").val() || job[candidateProfileKeys.EmploymentHistoryKeys.Company] != $("#Company").val() || job[candidateProfileKeys.EmploymentHistoryKeys.Description] != $("#Description").val()) return true;
				break;
			case "education":
				var schools = educationMemberModel[candidateProfileKeys.EducationKeys.Schools], school;
				$.each(schools, function() {
					if (this[candidateProfileKeys.EducationKeys.Id] == $(".content.education.edit-mode").attr("item_id")) school = this;
				});
				if (educationMemberModel[candidateProfileKeys.EducationKeys.HighestEducationLevel] != $("#HighestEducationLevel option:selected").val() || (school[candidateProfileKeys.EducationKeys.EndDateMonth] == null ? "" : school[candidateProfileKeys.EducationKeys.EndDateMonth]) != $("#EndDateMonth option:selected").val() || (school[candidateProfileKeys.EducationKeys.EndDateYear] == null ? "" : school[candidateProfileKeys.EducationKeys.EndDateYear]) != $("#EndDateYear option:selected").val() || (school[candidateProfileKeys.EducationKeys.IsCurrent] == "" ? true : school[candidateProfileKeys.EducationKeys.IsCurrent]) != ($("#EndDateMonth option[value='']").attr("selected") == "selected") || school[candidateProfileKeys.EducationKeys.Degree] != $("#Degree").val() || school[candidateProfileKeys.EducationKeys.Major] != $("#Major").val() || school[candidateProfileKeys.EducationKeys.Institution] != $("#Institution").val() || school[candidateProfileKeys.EducationKeys.City] != $("#City").val() || school[candidateProfileKeys.EducationKeys.Description] != $("#Description").val()) return true;
				break;
			case "other":
				if (otherMemberModel[candidateProfileKeys.OtherKeys.Courses] != $("#Courses").val() || otherMemberModel[candidateProfileKeys.OtherKeys.Awards] != $("#Awards").val() || otherMemberModel[candidateProfileKeys.OtherKeys.Professional] != $("#Professional").val() || otherMemberModel[candidateProfileKeys.OtherKeys.Interests] != $("#Interests").val() || otherMemberModel[candidateProfileKeys.OtherKeys.Affiliations] != $("#Affiliations").val() || otherMemberModel[candidateProfileKeys.OtherKeys.Other] != $("#Other").val() || otherMemberModel[candidateProfileKeys.OtherKeys.Referees] != $("#Referees").val()) return true;
				break;
		}
		return false;
	}

	showUploadErrorMsg = function(errorMsg) {
		$(".auto-extraction").hide();
		$(".upload-layer .prompt-text").text("");
		$(".errorMsg").show();
		$(".errorMsg span").text(errorMsg);
	}

	updateUploadProgressBar = function(percentage) {
		if (percentage < 4 || percentage > 98) $(".uploadprogressbar .mask").hide();
		else $(".uploadprogressbar .mask").show();
		var width = Math.round($(".uploadprogressbar .bg").width() * percentage / 100);
		$(".uploadprogressbar .fg").width(width);
		$(".uploadprogressbar .mask").css("margin-left", width - 11);
		$(".upload-layer .progress-bar-holder .percent").text(percentage + "%");
	}

	initSectionContent = function() {
		var section = $(".six-section .current").closest(".section").attr("url");
		$(".content:not(.employmenthistory, .education) .view-mode").hover(function() {
			$(".content").addClass("hover");
		}, function() {
			$(".content").removeClass("hover");
		}).click(function() {
			$(".content").addClass("edit-mode");
			assignValueToInput();
		});
		$(".content:not(.employmenthistory, .education) .edit").hover(function() {
			$(".content").addClass("hover");
		}, function() {
			$(".content").removeClass("hover");
		}).click(function() {
			$(".content").addClass("edit-mode");
			assignValueToInput();
		});
		$(".content:not(.employmenthistory, .education) .cancel").hover(function() {
			$(".content").addClass("hover");
		}, function() {
			$(".content").removeClass("hover");
		}).click(function() {
			$(".content").removeClass("edit-mode");
		});
		organiseFields();
		switch (section) {
			case "contactdetails":
				var uploadDialogReturnValue = false;
				var openUploadDialog = true;
				var fileUploadData;
				$(".prompt-layer.upload .yes").click(function() {
					openUploadDialog = false;
					uploadDialogReturnValue = true;
					$(".prompt-layer.upload").dialog("close");
					$(".photo_control .upload").trigger("click");
				});
				$(".prompt-layer.upload .cancel").click(function() {
					openUploadDialog = false;
					uploadDialogReturnValue = false;
					$(".prompt-layer.upload").dialog("close");
					$(".photo_control .upload").click();
				});
				$(".photo_control .browse-button input").change(function() {
					var fakeFilepath = $(this).val();
					var index = fakeFilepath.lastIndexOf("\\");
					var filename = fakeFilepath.substring(index + 1);
					$("#Photo").val(filename);
				});
				$("#fileupload").fileupload({
					url: $("#fileupload").attr("url"),
					type: "POST",
					dataType: "json",
					namespace: "linkme.profile.photo.upload",
					fileInput: $("#fileupload .browse-button input[type='file']"),
					replaceFileInput: false,
					limitMultiFileUploads: 1,
					add: function(e, data) {
						fileUploadData = data;
					},
					progress: function(e, data) {
						var percent = parseInt(data.loaded / data.total * 100, 10);
					},
					done: function(e, data) {
						if (data.result.Success) {
							var newPhotoId = data.result.PhotoId;
							contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhotoId] = newPhotoId;
							if (contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhotoId] && contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhotoId] != "") {
								$(".photo-fg").attr("src", $(".photo-fg.small").attr("url") + "?photoId=" + contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhotoId]).show();
								$(".photo_control .delete").show();
							}
							else {
								$(".photo-fg").hide();
								$(".photo_control .delete").hide();
							}
						}
					},
					fail: function(e, data) {
					},
					start: function(e, data) {
					},
					stop: function(e, data) {
					}
				});
				$(".photo_control .delete").click(function() {
					$(".prompt-layer.delete").dialog({
						height: 150,
						modal: true,
						width: 585,
						closeOnEscape: false,
						resizable: false,
						position: "center",
						title: "Delete uploaded photo",
						dialogClass: "delete-button-confirm-dialog"
					});
				});
				$(".prompt-layer.delete .delete").click(function() {
					$(".prompt-layer.delete").dialog("close");
					deletePhoto($(".photo_control .delete").attr("url"));
				});
				$(".prompt-layer.delete .cancel").click(function() {
					$(".prompt-layer.delete").dialog("close");
				});
				$(".privacy-section .save").click(function() {
					saveContactDetailsData($(this).attr("url"));
				});
				$(".privacy-section .cancel").click(function() {
					$(".content").removeClass("edit-mode");
				});
				$(".photo_control .upload").click(function() {
					if (!(fileUploadData && fileUploadData.files.length > 0)) {
						showErrInfo("noPhotoSelected");
						return false;
					}
					if (contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhotoId] && openUploadDialog) {
						$(".prompt-layer.upload").dialog({
							height: 120,
							modal: true,
							width: 500,
							closeOnEscape: false,
							resizable: false,
							position: "center",
							title: "Upload a new photo",
							dialogClass: "upload-button-confirm-dialog"
						});
					} else openUploadDialog = true;
					if (contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhotoId] && contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhotoId] != "" && !uploadDialogReturnValue) return false;
					var fileSize = fileUploadData.files[0].size;
					var fileType = fileUploadData.files[0].type;
					if ($.browser.msie) {
						var filename = fileUploadData.files[0].name;
						var ext = filename.substring(filename.lastIndexOf("."));
						switch (ext) {
							case ".jpg":
								fileType = "image/jpeg";
								break;
							case ".png":
								fileType = "image/png";
								break;
							case ".gif":
								fileType = "image/gif";
								break;
						}
					}
					if (fileSize > 2097152) {
						showErrInfo("photoSize");
						return;
					}
					if (!(fileType == "image/jpeg" || fileType == "image/png" || fileType == "image/gif")) {
						showErrInfo("photoType");
						return;
					}
					fileUploadData.submit();
				});
				$("#CountryId").parent().dropdown().parent().find(".dropdown-item").click(function() {
					var url = $(this).text() == "" ? "" : apiPartialMatchesUrl.substring(0, apiPartialMatchesUrl.indexOf("countryId=") + 10) + $("#CountryId option:contains('" + $(this).text() + "')").val() + "&location=";
					$("#Location").setOptions({
						url: url
					});
				});
				$("#Location").autocomplete(apiPartialMatchesUrl);
				$("#EmailAddress").closest(".field").append("<div class='username-callout'></div>");
				$(".radiobutton[value='Mobile']").each(function() {
					var MobileRadio = $(this).parent()
					MobileRadio.prependTo(MobileRadio.parent());
				});
				$("#NotApplicable").parent().appendTo(".Visa_control");
				$("#Unspecified").parent().hide();
				var field = $("#DateOfBirthMonth").closest(".field");
				$("<div class='dropdown_control DOBMonth_control control'><div class='textbox-bg'><input type='text' readonly='readonly'></div></div>").appendTo(field).find(".textbox-bg").append($("#DateOfBirthMonth"));
				$("<div class='dropdown_control DOBYear_control control'><div class='textbox-bg'><input type='text' readonly='readonly'></div></div>").appendTo(field).find(".textbox-bg").append($("#DateOfBirthYear"));
				$(".control label", field).parent().remove();
				$("#DateOfBirthMonth").parent().dropdown();
				// for (var i = new Date().getFullYear(); i >= 1900; i--)
				// $("#DateOfBirthYear").append("<option value='" + i + "'>" + i + "</option>");
				$(".DOBMonth_control .dropdown").before("<div class='textbox-left'></div>")
				$(".DOBMonth_control .dropdown-scrollpan").before("<div class='textbox-right'></div>");
				$("#DateOfBirthYear").parent().dropdown();
				$(".DOBYear_control .dropdown").before("<div class='textbox-left'></div>")
				$(".DOBYear_control .dropdown-scrollpan").before("<div class='textbox-right'></div>");
				//help-icon
				addHelpIcon(["Photo", "CountryId", "Location", "EmailAddress", "SecondaryEmail", "PhoneNumber", "SecondaryPhoneNumber", "Citizenship", "NotApplicable", "Female", "DateOfBirthYear"]);
				$("label[for='ResumePhoto']").append("<span class='desc'>(.JPG, .PNG, .GIF)</span>");
				$("label[for='Location']").append("<span class='desc'>e.g. Melbourne, VIC, 3000</span>");
				$("label[for='EmailAddress']").append("<span class='desc red'>LinkMe will never disclose your email address to employers</span>");
				$("label[for='SecondaryEmail']").append("<span class='desc'>(use commas to seperate multiple emails)</span>");
				$("label:contains('Gender')").append("<span class='desc red'>LinkMe will never disclose your gender to employers</span>");
				$("label:contains('Date of birth')").append("<span class='desc red'>LinkMe will never disclose your age or birth date to employers</span>");
				$(".help-icon[helpfor='DateOfBirthYear']").after("<label>Month</label><label for='DateOfBirthYear'>Year</label>");
				if ($.browser.msie && $.browser.version.indexOf("7") == 0) {
					$("#main-body .content .edit-mode .field label[for='Aboriginal'], #main-body .content .edit-mode .field label[for='TorresIslander']").attr("style", "width:75px;");
					$("label[for='DateOfBirthYear']").attr("style", "margin-left:-34px;");
				}
				break;
			case "desiredjob":
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
				$("#results .edit-mode .availability-icon").appendTo(".availability_field");
				$(".availability_control").hide();
				$(".availability-icon").hover(function() {
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
						$(".radiobutton[value='Unspecified']").click();
					} else {
						$(".availability-icon").removeClass("checked");
						$(this).addClass("checked");
						$(".radiobutton[value='" + $(this).attr("value") + "']").click();
					}
				});
				$(".radiobutton[value='SalaryRateYear']").click(function() {
					initializeSalary(0, 0, 250000, 5000);
				});
				$(".radiobutton[value='SalaryRateHour']").click(function() {
					initializeSalary(0, 0, 125, 5);
				});
				var currentSalaryRateType = "SalaryRate" + desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.DesiredSalaryRate];
				var currentSalary = desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.DesiredSalaryLowerBound];
				if (currentSalary) {
					if (currentSalaryRateType == "SalaryRateYear") initializeSalary(currentSalary, 0, 250000, 5000);
					else initializeSalary(currentSalary, 0, 125, 5);
					$(".salary-slider").slider("value", currentSalary);
				} else {
					if (currentSalaryRateType) $(".radiobutton[value='" + currentSalaryRateType + "']").click();
					else $(".radiobutton[value='SalaryRateYear']").click();
				}
				$("#SalaryLowerBound").closest(".field").hide();
				$("#SendSuggestedJobs").closest(".field").addClass("SendSuggestedJobs");
				$("#RelocationPreferenceWouldConsider").parent().addClass("WouldConsider");
				$(".radiobutton[value^='RelocationPreference']").click(function() {
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
				$(".fg .save").click(function() {
					saveDesiredJobData($(this).attr("url"));
				});
				$(".fg .cancel").click(function() {
					$(".content").removeClass("edit-mode");
				});
				//help-icon
				addHelpIcon(["DesiredJobTitle", "AvailableNow", "SendSuggestedJobs", "RelocationPreferenceWouldConsider", "CountryId"]);
				$("label[for='DesiredJobTitle']").append("<span class='desc'>This helps employers match you to jobs you want.</span><span class='desc'>Separate multiple titles with commas</span>");
				$("label:contains('Your availability')").append("<span class='desc'>Keep your work status up to date so employers know if you're looking for work or not</span>");
				$("label[for='DesiredSalary']").append("<span class='desc'>Be realistic. Employers prefer candidates who have reasonable salary expectations</span>");
				$("label[for='SendSuggestedJobs']").append("<span class='desc'>This sets up an automated <br/> e-mail letter for advertised jobs that match what you are looking for. You can cancel this at any time</span>");
				if ($.browser.msie && $.browser.version.indexOf("7") == 0) {
					$("#main-body .content .edit-mode .field label[for='Year']").attr("style", "width:80px;");
					$("#main-body .content .edit-mode .field label[for='WouldConsider']").attr("style", "width:100px;");
				}
				break;
			case "careerobjectives":
				$(".fg .save").click(function() {
					saveCareerObjectivesData($(this).attr("url"));
				});
				$(".fg .cancel").click(function() {
					$(".content").removeClass("edit-mode");
				});
				$(".content.careerobjectives textarea").attr("charsPerLine", "49");
				$("label[for='Objective']").append("<span class='desc'>Remember: be optimistic but also realistic.</span><span class='desc'>Employers prefer candidates who understand their own capabilities</span>");
				$("label[for='Summary']").append("<span class='desc'>Be concise. An employer should be able to read your summary in 15 seconds or less</span>");
				$("label[for='Skills']").append("<span class='desc'>Be specific and make sure they are work related.</span><span class='desc'>Think of the keywords that employers will use to search for candidates like you, and then use those words to talk about your skills</span>");
				break;
			case "employmenthistory":
				$(".fg .save").click(function() {
					saveEmploymentHistoryData($(this).attr("url"));
				});
				$(".fg .cancel").click(function() {
					$(".content").removeClass("edit-mode");
				});
				$("#RecentProfession").parent().dropdown();
				$("#RecentSeniority").find("option[value='None']").appendTo("#RecentSeniority");
				$("#RecentSeniority").parent().dropdown();
				$("#IndustryIds").attr("size", "6").attr("maxcount", "5").parent().listbox();
				var field = $("#StartDateMonth").closest(".field");
				$("<div class='dropdown_control StartDateMonth_control control'><div class='textbox-bg'><input type='text' readonly='readonly'></div></div>").appendTo(field).find(".textbox-bg").append($("#StartDateMonth"));
				$("<div class='dropdown_control StartDateYear_control control'><div class='textbox-bg'><input type='text' readonly='readonly'></div></div>").appendTo(field).find(".textbox-bg").append($("#StartDateYear"));
				$(".control label", field).parent().remove();
				$("#StartDateMonth").parent().dropdown();
				// for (var i = new Date().getFullYear(); i >= 1900; i--)
				// $("#DateOfBirthYear").append("<option value='" + i + "'>" + i + "</option>");
				$(".StartDateMonth_control .dropdown").before("<div class='textbox-left'></div>")
				$(".StartDateMonth_control .dropdown-scrollpan").before("<div class='textbox-right'></div>");
				$("#StartDateYear").parent().dropdown();
				$(".StartDateYear_control .dropdown").before("<div class='textbox-left'></div>")
				$(".StartDateYear_control .dropdown-scrollpan").before("<div class='textbox-right'></div>");
				field = $("#EndDateMonth").closest(".field");
				$("<div class='dropdown_control EndDateMonth_control control'><div class='textbox-bg'><input type='text' readonly='readonly'></div></div>").appendTo(field).find(".textbox-bg").append($("#EndDateMonth"));
				$("<div class='dropdown_control EndDateYear_control control'><div class='textbox-bg'><input type='text' readonly='readonly'></div></div>").appendTo(field).find(".textbox-bg").append($("#EndDateYear"));
				$(".control label", field).parent().remove();
				$("#EndDateMonth").parent().dropdown();
				// for (var i = new Date().getFullYear(); i >= 1900; i--)
				// $("#DateOfBirthYear").append("<option value='" + i + "'>" + i + "</option>");
				$(".EndDateMonth_control .dropdown").before("<div class='textbox-left'></div>")
				$(".EndDateMonth_control .dropdown-scrollpan").before("<div class='textbox-right'></div>");
				$("#EndDateYear").parent().dropdown();
				$(".EndDateYear_control .dropdown").before("<div class='textbox-left'></div>")
				$(".EndDateYear_control .dropdown-scrollpan").before("<div class='textbox-right'></div>");
				$(".EndDateMonth_control .dropdown-item:contains('Now')").click(function() {
					$(".EndDateYear_control").find(".dropdown-item:contains('Year')").click();
					$(".EndDateYear_control").addClass("disabled");
				});
				$(".EndDateMonth_control .dropdown-item:not(:contains('Now'))").click(function() {
					$(".EndDateYear_control").removeClass("disabled");
				});
				$(".content.employmenthistory textarea").attr("charsPerLine", "30");
				addHelpIcon(["RecentProfession", "RecentSeniority", "IndustryIds", "StartDateYear", "EndDateYear", "Title", "Description"]);
				$("label[for='IndustryIds']").append("<br/><span class='desc'>Select up to five.</span>");
				break;
			case "education":
				$(".fg .save").click(function() {
					saveEducationData($(this).attr("url"));
				});
				$(".fg .cancel").click(function() {
					$(".content").removeClass("edit-mode");
				});
				$("#HighestEducationLevel").parent().dropdown();
				var field = $("#EndDateMonth").closest(".field");
				$("<div class='dropdown_control EndDateMonth_control control'><div class='textbox-bg'><input type='text' readonly='readonly'></div></div>").appendTo(field).find(".textbox-bg").append($("#EndDateMonth"));
				$("<div class='dropdown_control EndDateYear_control control'><div class='textbox-bg'><input type='text' readonly='readonly'></div></div>").appendTo(field).find(".textbox-bg").append($("#EndDateYear"));
				$(".control label", field).parent().remove();
				$("#EndDateMonth").parent().dropdown();
				// for (var i = new Date().getFullYear(); i >= 1900; i--)
				// $("#DateOfBirthYear").append("<option value='" + i + "'>" + i + "</option>");
				$(".EndDateMonth_control .dropdown").before("<div class='textbox-left'></div>")
				$(".EndDateMonth_control .dropdown-scrollpan").before("<div class='textbox-right'></div>");
				$("#EndDateYear").parent().dropdown();
				$(".EndDateYear_control .dropdown").before("<div class='textbox-left'></div>")
				$(".EndDateYear_control .dropdown-scrollpan").before("<div class='textbox-right'></div>");
				$(".EndDateMonth_control .dropdown-item:contains('Current')").click(function() {
					$(".EndDateYear_control").find(".dropdown-item:contains('Year')").click();
					$(".EndDateYear_control").addClass("disabled");
				});
				$(".EndDateMonth_control .dropdown-item:not(:contains('Current'))").click(function() {
					$(".EndDateYear_control").removeClass("disabled");
				});
				$(".content.education textarea").attr("charsPerLine", "30");
				addHelpIcon(["HighestEducationLevel", "Degree"]);
				break;
			case "other":
				$(".fg .save").click(function() {
					saveOtherData($(this).attr("url"));
				});
				$(".fg .cancel").click(function() {
					$(".content").removeClass("edit-mode");
				});
				$(".content.other textarea").attr("charsPerLine", "30");
				addHelpIcon(["Courses", "Awards", "Interests", "Other", "Referees"]);
				break;
		}
		updateDisplayText(section);
		if (succInfoType == null) {
			$(".succ-info").hide();
		}
		else {
			showSuccInfo(succInfoType, "");
			succInfoType = null;
		}
		$(".err-info").hide();
		$(".help-icon").hover(function() {
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
	}

	updateDisplayText = function(section) {
		if (!section) section = $(".six-section .current").closest(".section").attr("url");
		switch (section) {
			case "contactdetails":
				if (contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhotoId] && contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhotoId] != "") {
					$(".photo-fg").attr("src", $(".photo-fg.small").attr("url") + "?photoId=" + contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhotoId]).show();
					$(".photo_control .delete").show();
				}
				else {
					$(".photo-fg").hide();
					$(".photo_control .delete").hide();
				}
				$(".view-mode .fg .name").text(contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.FirstName] + " " + contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.LastName]);
				if ($("#CountryId option[value='" + contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.CountryId] + "']").length > 0)
					$(".view-mode .fg .location .desc.country").text($($("#CountryId option[value='" + contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.CountryId] + "']")[0]).text());
				else
					$(".view-mode .fg .location .desc.country").text($("#CountryId option[value='" + contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.CountryId] + "']").text());
				$(".view-mode .fg .location .desc.suburb").text(contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.Location]);
				if (contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhoneNumber] != "") {
					var phoneType = contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhoneNumberType];
					$(".view-mode .fg .primary-phone .title").text((phoneType.substring(0, 1) != "M" ? "Tel " : "") + phoneType);
					$(".view-mode .fg .primary-phone .desc").text(contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhoneNumber]);
				} else {
					$(".view-mode .fg .primary-phone .title").text("Primary phone number");
					$(".view-mode .fg .primary-phone .desc").text("Not specified");
				}
				if (contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.SecondaryPhoneNumber] != "") {
					var phoneType = contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.SecondaryPhoneNumberType];
					$(".view-mode .fg .secondary-phone .title").text((phoneType.substring(0, 1) != "M" ? "Tel " : "") + phoneType);
					$(".view-mode .fg .secondary-phone .desc").text(contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.SecondaryPhoneNumber]);
					$(".view-mode .fg .secondary-phone").show();
				} else $(".view-mode .fg .secondary-phone").hide();
				if (contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.VisaStatus] != "")
					$(".view-mode .fg .visa .desc").text($("input[name='" + candidateProfileKeys.ContactDetailsKeys.VisaStatus + "']").closest(".field").find("label[for='" + contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.VisaStatus] + "']").text());
				else $(".view-mode .fg .visa .desc").text("Not specified");
				$(".view-mode .fg .primary-email .desc").text(contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.EmailAddress]);
				$(".view-mode .fg .secondary-email .desc").text(contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.SecondaryEmailAddress]);
				if (contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.SecondaryEmailAddress] == "")
					$(".view-mode .fg .secondary-email").hide();
				else $(".view-mode .fg .secondary-email").show();
				if (contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.Gender] != "" && contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.Gender] != "Unspecified") {
					$(".view-mode .fg .gender .desc").text(contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.Gender]);
					$(".view-mode .fg .gender").show();
				} else $(".view-mode .fg .gender").hide();
				if (contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.DateOfBirthMonth] != "" && contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.DateOfBirthYear] != "") {
					$(".view-mode .fg .dob .desc").text($("#" + candidateProfileKeys.ContactDetailsKeys.DateOfBirthMonth + " option[value='" + contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.DateOfBirthMonth] + "']").text() + " " + contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.DateOfBirthYear]);
					$(".view-mode .fg .dob").show();
				} else $(".view-mode .fg .dob").hide();
				break;
			case "desiredjob":
				$(".content .view-mode .field .availability-icon").attr("class", "availability-icon").addClass(desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.Status]);
				var statusDesc = { "AvailableNow": "immediately available", "ActivelyLooking": "actively looking", "OpenToOffers": "not looking but happy to talk", "NotLooking": "not looking" };
				$(".content .view-mode .field .title.status").text("I am " + statusDesc[desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.Status]] + (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.DesiredJobTitle] == "" ? "" : " for work as a"));
				$(".content.desiredjob .view-mode .field .job-title").text(desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.DesiredJobTitle]);
				var jobTypeText = "";
				if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.FullTime]) jobTypeText += "full time, ";
				if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.PartTime]) jobTypeText += "part time, ";
				if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.Contract]) jobTypeText += "contract, ";
				if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.Temp]) jobTypeText += "temp, ";
				if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.JobShare]) jobTypeText += "job share, ";
				if (jobTypeText.length > 2) jobTypeText = jobTypeText.substring(0, jobTypeText.length - 2);
				if (jobTypeText != "") {
					jobTypeText += " work";
					$(".content .view-mode .field .job-type .desc").text(jobTypeText);
					$(".content .view-mode .field .job-type").show();
				} else $(".content .view-mode .field .job-type").hide();
				$(".content .view-mode .field .salary .desc").text($(".content .edit-mode .salary-section .salary-desc").text());
				if ($("#IsSalaryNotVisible").is(":checked")) {
					$(".content .view-mode .field .salary").addClass("not-visible");
					$(".content .view-mode .field .salary .red").show();
				}
				else {
					$(".content .view-mode .field .salary").removeClass("not-visible");
					$(".content .view-mode .field .salary .red").hide();
				}
				var relocationText = "";
				if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.RelocationCountryLocationIds] != null)
					$.each(desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.RelocationCountryLocationIds], function(i, n) {
						relocationText += $("#RelocationCountryLocationIds option[value='" + n + "']").text() + ", "
					});
				if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.RelocationCountryIds] != null)
					$.each(desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.RelocationCountryIds], function(i, n) {
						relocationText += $("#RelocationCountryIds option[value='" + n + "']").text() + ", "
					});
				if (relocationText.length > 2) relocationText = relocationText.substring(0, relocationText.length - 2);
				$(".content .view-mode .field .relocation .desc").text(relocationText);
				if (relocationText == "" || desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.RelocationPreference] == "No") $(".content .view-mode .field .relocation").hide();
				else $(".content .view-mode .field .relocation").show();
				break;
			case "careerobjectives":
				if (careerObjectivesMemberModel[candidateProfileKeys.CareerObjectivesKeys.Objective] == "") {
					$(".content .view-mode .objective-incomplete").show();
					$(".content .view-mode .objective").hide();
				} else {
					$(".content .view-mode .objective-incomplete").hide();
					$(".content .view-mode .objective").show();
				}
				$(".content .view-mode .objective .desc").html(careerObjectivesMemberModel[candidateProfileKeys.CareerObjectivesKeys.Objective] == "" ? "<br /><br />" : careerObjectivesMemberModel[candidateProfileKeys.CareerObjectivesKeys.Objective].replace(/\n/gi, "<br />"));
				$(".content .view-mode .summary .desc").html(careerObjectivesMemberModel[candidateProfileKeys.CareerObjectivesKeys.Summary] == "" ? "<br /><br />" : careerObjectivesMemberModel[candidateProfileKeys.CareerObjectivesKeys.Summary].replace(/\n/gi, "<br />"));
				$(".content .view-mode .skills .desc").html(careerObjectivesMemberModel[candidateProfileKeys.CareerObjectivesKeys.Skills] == "" ? "<br /><br />" : careerObjectivesMemberModel[candidateProfileKeys.CareerObjectivesKeys.Skills].replace(/\n/gi, "<br />"));
				break;
			case "employmenthistory":
				//no job details
				if (employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.Jobs][0][candidateProfileKeys.EmploymentHistoryKeys.Id] == "") {
					$(".job-incomplete").show();
					$(".recent-incomplete").hide();
					$(".item-header").hide();
					$(".item-details").hide();
					$(".item").attr("item_id", "");
				} else {
					//have at least one job detail but no new fields
					if (employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.RecentProfession] == "" || employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.RecentSeniority] == "") {
						$(".job-incomplete").hide();
						$(".recent-incomplete").show();
					} else {
						$(".job-incomplete").hide();
						$(".recent-incomplete").hide();
					}
					$(".job-incomplete").hide();
					$(".item-details").show();
					if ($(".content.employmenthistory .view-mode .field .item:eq(0) .item-button").length == 0)
						$(".content.employmenthistory .view-mode .field .item .item-button").prependTo($(".content.employmenthistory .view-mode .field .item:eq(0)"));
					$(".content.employmenthistory .view-mode .field .item:gt(0), .content.employmenthistory .view-mode .field .item-divider:gt(0)").remove();
					$.each(employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.Jobs], function(index, job) {
						var currentItem, currentItemDivider;
						if (index == 0) {
							currentItem = $(".content.employmenthistory .view-mode .field .item:eq(0)");
							currentItemDivider = $(".content.employmenthistory .view-mode .field .item-divider:eq(0)");
							$(".item-header .profession .desc", currentItem).html("<b>My most recent profession is </b>" + $("#RecentProfession option[value='" + employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.RecentProfession] + "']").text() + " (" + $("#RecentSeniority option[value='" + employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.RecentSeniority] + "']").text() + ")");
							var industries = new Array();
							if (employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.IndustryIds])
								$.each(employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.IndustryIds], function() {
									industries.push($("#IndustryIds option[value='" + this + "']").text());
								});
							$(".item-header .industry .desc", currentItem).html("<b>I have experience in the following industries: </b>" + industries.join("; "));
						} else {
							currentItem = $(".content.employmenthistory .view-mode .field .item:eq(0)").clone();
							$(".item-button", currentItem).remove();
							currentItemDivider = $(".content.employmenthistory .view-mode .field .item-divider:eq(0)").clone();
							$(".job-incomplete", currentItem).remove();
							$(".recent-incomplete", currentItem).remove();
							$(".item-header", currentItem).remove();
						}
						currentItem.attr("item_id", this[candidateProfileKeys.EmploymentHistoryKeys.Id]);
						$(".job-title", currentItem).html(this[candidateProfileKeys.EmploymentHistoryKeys.Title]);
						$(".company", currentItem).html(this[candidateProfileKeys.EmploymentHistoryKeys.Company]);
						var startDateMonthValue = this[candidateProfileKeys.EmploymentHistoryKeys.StartDateMonth] == null ? "" : this[candidateProfileKeys.EmploymentHistoryKeys.StartDateMonth];
						var startDateYearValue = this[candidateProfileKeys.EmploymentHistoryKeys.StartDateYear] == null ? "" : this[candidateProfileKeys.EmploymentHistoryKeys.StartDateYear];
						$(".tenure .desc:not(.gray)", currentItem).html((startDateMonthValue == "" ? "" : $("#StartDateMonth option[value='" + this[candidateProfileKeys.EmploymentHistoryKeys.StartDateMonth] + "']").text()) + " " + (startDateYearValue == "" ? "" : $("#StartDateYear option[value='" + this[candidateProfileKeys.EmploymentHistoryKeys.StartDateYear] + "']").text()) + " - " + (this[candidateProfileKeys.EmploymentHistoryKeys.IsCurrent] ? "current" : $("#EndDateMonth option[value='" + this[candidateProfileKeys.EmploymentHistoryKeys.EndDateMonth] + "']").text() + " " + $("#EndDateYear option[value='" + this[candidateProfileKeys.EmploymentHistoryKeys.EndDateYear] + "']").text()));
						if (startDateMonthValue != "" || startDateYearValue != "") {
							var startDate = new Date(startDateYearValue, startDateMonthValue - 1, 1);
							var endDate = this[candidateProfileKeys.EmploymentHistoryKeys.IsCurrent] ? new Date(new Date().getFullYear(), new Date().getMonth(), 1) : new Date(this[candidateProfileKeys.EmploymentHistoryKeys.EndDateYear], this[candidateProfileKeys.EmploymentHistoryKeys.EndDateMonth] - 1, 1);
							var diffInMonth = (endDate.getFullYear() * 12 + endDate.getMonth()) - (startDate.getFullYear() * 12 + startDate.getMonth());
							var diffInMonthYear = parseInt(diffInMonth / 12), diffInMonthMonth = diffInMonth % 12;
							$(".tenure .desc.gray", currentItem).html(diffInMonthYear + " year" + (diffInMonthYear == 1 ? "" : "s") + (diffInMonthMonth == 0 ? "" : (", " + diffInMonthMonth + " month" + (diffInMonthMonth == 1 ? "" : "s"))));
						} else
							$(".tenure .desc.gray", currentItem).html("");
						$(".duties .desc", currentItem).html(this[candidateProfileKeys.EmploymentHistoryKeys.Description] == null ? "" : this[candidateProfileKeys.EmploymentHistoryKeys.Description].replace(/\n/gi, "<br />")).attr("title", this[candidateProfileKeys.EmploymentHistoryKeys.Description] == null ? "" : this[candidateProfileKeys.EmploymentHistoryKeys.Description].replace(/\n/gi, "<br />"));
						currentItem.appendTo($(".content.employmenthistory .view-mode .field"));
						currentItemDivider.appendTo($(".content.employmenthistory .view-mode .field"));
					});
				}
				$(".content.employmenthistory .view-mode .field .item:eq(0)").addClass("first-item");
				$(".content.employmenthistory .view-mode .field .item:gt(0)").removeClass("first-item");
				break;
			case "education":
				//no school details
				if (educationMemberModel[candidateProfileKeys.EducationKeys.Schools][0][candidateProfileKeys.EducationKeys.Id] == "") {
					$(".school-incomplete").show();
					$(".highest-level-incomplete").hide();
					$(".item-header").hide();
					$(".item-details").hide();
					$(".item").attr("item_id", "");
				} else {
					//have at least one job detail but no new fields
					if (educationMemberModel[candidateProfileKeys.EducationKeys.HighestEducationLevel] == "") {
						$(".highest-level-incomplete").show();
						$(".item-header").hide();
					} else {
						$(".highest-level-incomplete").hide();
						$(".item-header").show();
					}
					$(".school-incomplete").hide();
					$(".item-details").show();
					if ($(".content.education .view-mode .field .item:eq(0) .item-button").length == 0)
						$(".content.education .view-mode .field .item .item-button").prependTo($(".content.education .view-mode .field .item:eq(0)"));
					$(".content.education .view-mode .field .item:gt(0), .content.education .view-mode .field .item-divider:gt(0)").remove();
					$.each(educationMemberModel[candidateProfileKeys.EducationKeys.Schools], function(index, school) {
						var currentItem, currentItemDivider;
						if (index == 0) {
							currentItem = $(".content.education .view-mode .field .item:eq(0)");
							currentItemDivider = $(".content.education .view-mode .field .item-divider:eq(0)");
							$(".item-header .highest-level .desc", currentItem).html("<b>My highest education level is </b>" + $("#HighestEducationLevel option[value='" + educationMemberModel[candidateProfileKeys.EducationKeys.HighestEducationLevel] + "']").text());
						} else {
							currentItem = $(".content.education .view-mode .field .item:eq(0)").clone();
							$(".item-button", currentItem).remove();
							currentItemDivider = $(".content.education .view-mode .field .item-divider:eq(0)").clone();
							$(".school-incomplete", currentItem).remove();
							$(".highest-level-incomplete", currentItem).remove();
							$(".item-header", currentItem).remove();
						}
						currentItem.attr("item_id", this[candidateProfileKeys.EducationKeys.Id]);
						$(".degree", currentItem).html(this[candidateProfileKeys.EducationKeys.Degree]);
						$(".major", currentItem).html(this[candidateProfileKeys.EducationKeys.Major]);
						$(".school", currentItem).html(this[candidateProfileKeys.EducationKeys.Institution] + ", " + this[candidateProfileKeys.EducationKeys.City]);

						if (this[candidateProfileKeys.EducationKeys.IsCurrent] || this[candidateProfileKeys.EducationKeys.EndDateYear] != null) {
							$(".completion .desc:not(.gray)", currentItem).html(this[candidateProfileKeys.EducationKeys.IsCurrent] ? "Current" : ($("#EndDateMonth option[value='" + this[candidateProfileKeys.EducationKeys.EndDateMonth] + "']").text() + " " + $("#EndDateYear option[value='" + this[candidateProfileKeys.EducationKeys.EndDateYear] + "']").text()));
							if (this[candidateProfileKeys.EducationKeys.IsCurrent]) {
								$(".completion .desc.gray", currentItem).hide();
							}
							else {
								var end = this[candidateProfileKeys.EducationKeys.EndDateMonth] == null ? new Date(this[candidateProfileKeys.EducationKeys.EndDateYear], 11, 1) : new Date(this[candidateProfileKeys.EducationKeys.EndDateYear], this[candidateProfileKeys.EducationKeys.EndDateMonth] - 1, 1);
								var now = new Date(new Date().getFullYear(), new Date().getMonth(), 1);
								var difference = now.getFullYear() - end.getFullYear();
								if (difference > 0) {
									$(".completion .desc.gray", currentItem).html(difference + " year" + (difference == 1 ? "" : "s") + " ago");
									$(".completion .desc.gray", currentItem).show();
								}
								else {
									$(".completion .desc.gray", currentItem).hide();
								}
							}
						}
						else {
							$(".completion", currentItem).hide();
						}
						$(".description .desc", currentItem).html(this[candidateProfileKeys.EducationKeys.Description] == null ? "" : this[candidateProfileKeys.EducationKeys.Description].replace(/\n/gi, "<br>"));
						currentItem.appendTo($(".content.education .view-mode .field"));
						currentItemDivider.appendTo($(".content.education .view-mode .field"));
					});
				}
				$(".content.education .view-mode .field .item:eq(0)").addClass("first-item");
				$(".content.education .view-mode .field .item:gt(0)").removeClass("first-item");
				break;
			case "other":
				if (otherMemberModel[candidateProfileKeys.OtherKeys.Courses] == "" && otherMemberModel[candidateProfileKeys.OtherKeys.Awards] == "" && otherMemberModel[candidateProfileKeys.OtherKeys.Professional] == "" && otherMemberModel[candidateProfileKeys.OtherKeys.Interests] == "" && otherMemberModel[candidateProfileKeys.OtherKeys.Affiliations] == "" && otherMemberModel[candidateProfileKeys.OtherKeys.Other] == "" && otherMemberModel[candidateProfileKeys.OtherKeys.Referees] == "") {
					$(".content .view-mode .other-incomplete").show();
					$(".content .view-mode .other-info").hide();
				} else {
					$(".content .view-mode .other-incomplete").hide();
					$(".content .view-mode .other-info").show();
				}
				$(".content .view-mode .courses .desc").html(otherMemberModel[candidateProfileKeys.OtherKeys.Courses]);
				$(".content .view-mode .awards .desc").html(otherMemberModel[candidateProfileKeys.OtherKeys.Awards]);
				$(".content .view-mode .certifications .desc").html(otherMemberModel[candidateProfileKeys.OtherKeys.Professional]);
				$(".content .view-mode .interests .desc").html(otherMemberModel[candidateProfileKeys.OtherKeys.Interests]);
				$(".content .view-mode .affiliation .desc").html(otherMemberModel[candidateProfileKeys.OtherKeys.Affiliations]);
				$(".content .view-mode .others .desc").html(otherMemberModel[candidateProfileKeys.OtherKeys.Other]);
				$(".content .view-mode .references .desc").html(otherMemberModel[candidateProfileKeys.OtherKeys.Referees]);
				break;
		}
		//have to put these here after dynamically create the employmenthistory items and education items
		$("#results .content.employmenthistory .view-mode .item, #results .content.education .view-mode .item").unbind("hover").unbind("click").hover(function() {
			$(this).parent().find(".item-button").prependTo($(this)).find(".bottom-edge").height($(this).height() - 249);
			$(this).addClass("hover");
			if ($.browser.msie && $.browser.version.indexOf("9") == 0)
				$(".bg", this).css("margin-top", "-" + ($(".bg", this).height() + 3) + "px");
		}, function() {
			$(this).removeClass("hover");
			if ($.browser.msie && $.browser.version.indexOf("9") == 0)
				$(".bg", this).css("margin-top", "0px");
		}).click(function() {
			$(".content").addClass("edit-mode").attr("item_id", $(this).attr("item_id"));
			assignValueToInput();
		});
		$("#results .content.employmenthistory .view-mode .new, #results .content.education .view-mode .new").unbind("hover").unbind("click").hover(function() {
			$(this).closest(".item").addClass("new-hover");
		}, function() {
			$(this).closest(".item").removeClass("new-hover");
		}).click(function() {
			$(".content").addClass("edit-mode").attr("item_id", "");
			assignValueToInput();
			event.stopPropagation();
		});
		$("#results .content.employmenthistory .view-mode .bottom-new-icon, #results .content.education .view-mode .bottom-new-icon").unbind("click").click(function() {
			$(".content").addClass("edit-mode").attr("item_id", "");
			assignValueToInput();
		});
		$("#results .content.employmenthistory .view-mode .edit, #results .content.education .view-mode .edit").unbind("hover").unbind("click").hover(function() {
			$(this).closest(".item").addClass("edit-hover");
		}, function() {
			$(this).closest(".item").removeClass("edit-hover");
		}).click(function() {
			$(".content").addClass("edit-mode").attr("item_id", $(this).closest(".item").attr("item_id"));
			assignValueToInput();
			event.stopPropagation();
		});
		$("#results .content.employmenthistory .view-mode .cancel, #results .content.education .view-mode .cancel").unbind("hover").unbind("click").hover(function() {
			$(this).closest(".item").addClass("cancel-hover");
		}, function() {
			$(this).closest(".item").removeClass("cancel-hover");
		}).click(function() {
			$(".content").removeClass("edit-mode");
			event.stopPropagation();
		});
		$("#results .content.employmenthistory .view-mode .delete, #results .content.education .view-mode .delete").unbind("hover").unbind("click").hover(function() {
			$(this).closest(".item").addClass("delete-hover");
		}, function() {
			$(this).closest(".item").removeClass("delete-hover");
		}).click(function() {
			if ($(this).closest(".item").attr("item_id") == "") return false;
			showDeleteDialog($(".six-section .current").closest(".section").attr("url"), $(this).closest(".item").attr("item_id"));
			event.stopPropagation();
		});
		$("#results .content.employmenthistory .edit-mode .item-button .new, #results .content.education .edit-mode .item-button .new").unbind("hover").unbind("click").hover(function() {
			$(this).closest(".edit-mode").addClass("new-hover");
		}, function() {
			$(this).closest(".edit-mode").removeClass("new-hover");
		}).click(function() {
			$(".content").addClass("edit-mode").attr("item_id", "");
			assignValueToInput();
			event.stopPropagation();
		});
		$("#results .content.employmenthistory .edit-mode .bottom-new-icon, #results .content.education .edit-mode .bottom-new-icon").unbind("click").click(function() {
			$(".content").addClass("edit-mode").attr("item_id", "");
			assignValueToInput();
		});
		$("#results .content.employmenthistory .edit-mode .edit, #results .content.education .edit-mode .edit").unbind("hover").unbind("click").hover(function() {
			$(this).closest(".edit-mode").addClass("edit-hover");
		}, function() {
			$(this).closest(".edit-mode").removeClass("edit-hover");
		});
		$("#results .content.employmenthistory .edit-mode .item-button .cancel, #results .content.education .edit-mode .item-button .cancel").unbind("hover").unbind("click").hover(function() {
			$(this).closest(".edit-mode").addClass("cancel-hover");
		}, function() {
			$(this).closest(".edit-mode").removeClass("cancel-hover");
		}).click(function() {
			$(".content").removeClass("edit-mode");
		});
		$("#results .content.employmenthistory .edit-mode .delete, #results .content.education .edit-mode .delete").unbind("hover").unbind("click").hover(function() {
			$(this).closest(".edit-mode").addClass("delete-hover");
		}, function() {
			$(this).closest(".edit-mode").removeClass("delete-hover");
		}).click(function() {
			if ($(".content").attr("item_id") == "") return false;
			showDeleteDialog($(".six-section .current").closest(".section").attr("url"), $(".content").attr("item_id"));
		});
	}

	showDeleteDialog = function(type, id) {
		var title;
		switch (type) {
			case "employmenthistory":
				var jobs = employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.Jobs];
				var job;
				$.each(jobs, function() {
					if (this[candidateProfileKeys.EmploymentHistoryKeys.Id] == id) job = this;
				});
				title = "Delete " + job[candidateProfileKeys.EmploymentHistoryKeys.Title] + " at " + job[candidateProfileKeys.EmploymentHistoryKeys.Company] + "?";
				break;
			case "education":
				var schools = educationMemberModel[candidateProfileKeys.EducationKeys.Schools];
				var school;
				$.each(schools, function() {
					if (this[candidateProfileKeys.EducationKeys.Id] == id) school = this;
				});
				title = "Delete " + school[candidateProfileKeys.EducationKeys.Degree] + "?";
				break;
		}
		$(".prompt-layer.delete").dialog({
			height: 150,
			modal: true,
			width: 585,
			closeOnEscape: false,
			resizable: false,
			position: "center",
			title: title,
			dialogClass: "delete-button-confirm-dialog"
		});
		$(".prompt-layer.delete .delete").unbind("click").click(function() {
			$(".prompt-layer.delete").dialog("close");
			switch (type) {
				case "employmenthistory":
					deleteEmploymentHistory(id, $(this).attr("url"));
					break;
				case "education":
					deleteEducation(id, $(this).attr("url"));
					break;
			}
		});
		$(".prompt-layer.delete .cancel").unbind("click").click(function() {
			$(".prompt-layer.delete").dialog("close");
		});
	}

	showSuccInfo = function(type, extra_msg) {
		var message = "";
		if (type == "save") message = "The changes have been saved.";
		if (type == "setCurrent") message = "Your resume has already been set as current.";
		if (type == "saveVisibility") message = "Your visibility settings have been saved.";
		if (type == "switchVisibility") message = "Your visibility setting has been switched " + (candidateVisibilityModel[candidateProfileKeys.VisibilityKeys.ShowResume] ? "on. Employers will now be able to search for your resume." : "off. Employers will now not be able to find you.");
		if (type == "sendResume") message = "A copy of your resume has been emailed to ";
		if (type == "updateStatus") message = "Your status has been updated.";
		if (type == "confirmStatus") message = "Your status has been confirmed.";
		$(".succ-info .fg .info .text:not(.red)").text(message);
		$(".succ-info .fg .info .text.red").text(extra_msg);
		$(".succ-info").show();
		$(".err-info").hide();
		$(".control").removeClass("error");
		$(".field .error-msg").remove();
		$(".content").removeClass("edit-mode");
		if (type == "save") {
			updateDisplayText();
			updateLeftSection();
		}
		if (type == "setCurrent") updateLeftSection();
		if (type == "saveVisbility" || type == "switchVisibility") updateVisibility();
	}

	showErrInfo = function(type, errObj) {
		var message = "Unfortunately we encountered an issue while you did the last action, please try again later.";
		if (type == "save") message = "You need to correct some errors before your changes can be saved. Please review the fields below and try again.";
		if (type == "changeSection") message = "Please Save or Cancel current edit before moving to another section.";
		if (type == "noPhotoSelected") message = "Your should browse a photo file from your computer first.";
		if (type == "photoType") message = "Your photo format is not allowed. Please try another one";
		if (type == "photoSize") message = "Your photo file is too big, that should be smaller than 2M. Please try another one";
		$(".err-info .fg .info .text").text(message);
		$(".err-info").show();
		$(".succ-info").hide();
		if (type == "save") {
			$(".field .error-msg").remove();
			$(".control").removeClass("error");
			$.each(errObj, function() {
				if (this["Key"] == "CompletionDate")
					$("#EndDateYear").closest(".field").append("<span class='error-msg' error-for='" + this["Key"] + "'>" + this["Message"] + "</span>");
				else if (this["Key"] == "VisaStatus")
					$("#NotApplicable").closest(".field").append("<span class='error-msg' error-for='" + this["Key"] + "'>" + this["Message"] + "</span>");
				else if (this["Key"] == "DesiredSalaryLowerBound")
					$(".salary-slider").closest(".field").append("<span class='error-msg' error-for='" + this["Key"] + "'>" + this["Message"] + "</span>");
				else if (this["Key"] == "Date")
					$("#EndDateYear").closest(".field").append("<span class='error-msg' error-for='" + this["Key"] + "'>" + this["Message"] + "</span>");
				else if (this["Key"] == "Gender")
				    $("#Male").closest(".field").append("<span class='error-msg' error-for='" + this["Key"] + "'>" + this["Message"] + "</span>");
				else if (this["Key"] == "DateOfBirth")
				    $("#DateOfBirthYear").closest(".field").append("<span class='error-msg' error-for='" + this["Key"] + "'>" + this["Message"] + "</span>");
				else
					$("#" + this["Key"]).closest(".control").addClass("error").closest(".field").append("<span class='error-msg' error-for='" + this["Key"] + "'>" + this["Message"] + "</span>");
			});
		}
		if (errObj && errObj.length == 1 && errObj[0]["Message"] == "The user is not logged in") {
			window.location.href = $(".six-section").attr("loginurl");
		}
	}

	updateLeftSection = function() {
	    $("#left-sidebar .completion .progressbar-text").text(candidateProfileModel[candidateProfileKeys.MemberStatusKeys.PercentComplete] + "%");
		updateCompletionBar();
		var profileAge = candidateProfileModel[candidateProfileKeys.MemberStatusKeys.Age];
		var resumeAge;
		if (profileAge == 0) resumeAge = "Today";
		else if (profileAge == 1) resumeAge = "Yesterday";
		else if (profileAge < 30) resumeAge = profileAge + " days";
		else {
			var month = Math.round(profileAge / 30);
			resumeAge = month + " month" + (month == 1 ? "" : "s");
		}
        if (candidateProfileModel[candidateProfileKeys.MemberStatusKeys.PromptForResumeUpdate]) {
			$(".resumeage .title").text("Your resume is " + resumeAge + " old.");
			$(".resumeage").show();
		} else $(".resumeage").hide();
		$(".lastupdate").text("last edited: " + ((resumeAge == "Today" || resumeAge == "Yesterday") ? resumeAge : (resumeAge + " ago.")));
		$(".profile-incomplete ul").html("");
		$(".six-section .section").removeClass("incomplete");
		if (candidateProfileModel[candidateProfileKeys.MemberStatusKeys.PercentComplete] < 100) {
		    if (!candidateProfileModel[candidateProfileKeys.MemberStatusKeys.MemberStatus][candidateProfileKeys.MemberStatusKeys.IsAddressComplete] || !candidateProfileModel[candidateProfileKeys.MemberStatusKeys.MemberStatus][candidateProfileKeys.MemberStatusKeys.IsEmailComplete] || !candidateProfileModel[candidateProfileKeys.MemberStatusKeys.MemberStatus][candidateProfileKeys.MemberStatusKeys.IsPhoneComplete] || !candidateProfileModel[candidateProfileKeys.MemberStatusKeys.MemberStatus][candidateProfileKeys.MemberStatusKeys.IsVisaStatusComplete])
				$(".profile-incomplete ul").append("<li><div class='list-icon'></div><span>" + $(".six-section .section:eq(0) .section-title").text() + "</span></li>");
		    if (!candidateProfileModel[candidateProfileKeys.MemberStatusKeys.MemberStatus][candidateProfileKeys.MemberStatusKeys.IsStatusComplete] || !candidateProfileModel[candidateProfileKeys.MemberStatusKeys.MemberStatus][candidateProfileKeys.MemberStatusKeys.IsDesiredSalaryComplete])
				$(".profile-incomplete ul").append("<li><div class='list-icon'></div><span>" + $(".six-section .section:eq(1) .section-title").text() + "</span></li>");
		    if (!candidateProfileModel[candidateProfileKeys.MemberStatusKeys.MemberStatus][candidateProfileKeys.MemberStatusKeys.IsObjectiveComplete])
				$(".profile-incomplete ul").append("<li><div class='list-icon'></div><span>" + $(".six-section .section:eq(2) .section-title").text() + "</span></li>");
		    if (!candidateProfileModel[candidateProfileKeys.MemberStatusKeys.MemberStatus][candidateProfileKeys.MemberStatusKeys.IsIndustriesComplete] || !candidateProfileModel[candidateProfileKeys.MemberStatusKeys.MemberStatus][candidateProfileKeys.MemberStatusKeys.IsJobsComplete] || !candidateProfileModel[candidateProfileKeys.MemberStatusKeys.MemberStatus][candidateProfileKeys.MemberStatusKeys.IsRecentProfessionComplete] || !candidateProfileModel[candidateProfileKeys.MemberStatusKeys.MemberStatus][candidateProfileKeys.MemberStatusKeys.IsRecentSeniorityComplete])
				$(".profile-incomplete ul").append("<li><div class='list-icon'></div><span>" + $(".six-section .section:eq(3) .section-title").text() + "</span></li>");
		    if (!candidateProfileModel[candidateProfileKeys.MemberStatusKeys.MemberStatus][candidateProfileKeys.MemberStatusKeys.IsSchoolsComplete] || !candidateProfileModel[candidateProfileKeys.MemberStatusKeys.MemberStatus][candidateProfileKeys.MemberStatusKeys.IsHighestEducationComplete])
				$(".profile-incomplete ul").append("<li><div class='list-icon'></div><span>" + $(".six-section .section:eq(4) .section-title").text() + "</span></li>");
			$.each($(".profile-incomplete ul li"), function() {
				$(".six-section .section:contains('" + $("span", this).text() + "')").addClass("incomplete");
			});
			$(".profile-incomplete").show();
		} else $(".profile-incomplete").hide();
	}

	updateVisibility = function() {
		if (candidateVisibilityModel[candidateProfileKeys.VisibilityKeys.ShowResume]) {
			$(".set-visibility .text-on").addClass("active");
			$(".set-visibility .switch-icon").addClass("on").removeClass("off");
			$(".set-visibility .text-off").removeClass("active");
		} else {
			$(".set-visibility .text-on").removeClass("active");
			$(".set-visibility .switch-icon").addClass("off").removeClass("on");
			$(".set-visibility .text-off").addClass("active");
		}
		if (candidateVisibilityModel[candidateProfileKeys.VisibilityKeys.ShowName]) {
			$(".visibility .checkbox.ShowName").addClass("checked");
			$("#ShowName").attr("checked", "checked");
		} else {
			$(".visibility .checkbox.ShowName").removeClass("checked");
			$("#ShowName").removeAttr("checked");
		}
		if (candidateVisibilityModel[candidateProfileKeys.VisibilityKeys.ShowPhoneNumbers]) {
			$(".visibility .checkbox.ShowPhoneNumbers").addClass("checked");
			$("#ShowPhoneNumbers").attr("checked", "checked");
		} else {
			$(".visibility .checkbox.ShowPhoneNumbers").removeClass("checked");
			$("#ShowPhoneNumbers").removeAttr("checked");
		}
		if (candidateVisibilityModel[candidateProfileKeys.VisibilityKeys.ShowProfilePhoto]) {
			$(".visibility .checkbox.ShowProfilePhoto").addClass("checked");
			$("#ShowProfilePhoto").attr("checked", "checked");
		} else {
			$(".visibility .checkbox.ShowProfilePhoto").removeClass("checked");
			$("#ShowProfilePhoto").removeAttr("checked");
		}
		if (candidateVisibilityModel[candidateProfileKeys.VisibilityKeys.ShowRecentEmployers]) {
			$(".visibility .checkbox.ShowRecentEmployers").addClass("checked");
			$("#ShowRecentEmployers").attr("checked", "checked");
		} else {
			$(".visibility .checkbox.ShowRecentEmployers").removeClass("checked");
			$("#ShowRecentEmployers").removeAttr("checked");
		}
	}

	assignValueToInput = function() {
		$(".succ-info").hide();
		$(".err-info").hide();
		$(".field .error-msg").remove();
		$(".control").removeClass("error");
		switch ($(".six-section .current").closest(".section").attr("url")) {
			case "contactdetails":
				$("#FirstName").val(contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.FirstName]);
				$("#LastName").val(contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.LastName]);
				$("#CountryId .option").removeAttr("selected");
				$("#CountryId .option[value='" + contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.CountryId] + "']").attr("selected", "selected");
				$("#Location").val(contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.Location]);
				$("#EmailAddress").val(contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.EmailAddress]);
				$("#SecondaryEmail").val(contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.SecondaryEmailAddress]);
				$("#PhoneNumber").val(contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhoneNumber]);
				$("input[name='PhoneNumberType']").removeAttr("checked").prev().removeClass("checked");
				$("input[name='PhoneNumberType'][value='" + contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhoneNumberType] + "']").attr("checked", "checked").prev().addClass("checked");
				$("#SecondaryPhoneNumber").val(contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.SecondaryPhoneNumber]);
				$("input[name='SecondaryPhoneNumberType']").removeAttr("checked").prev().removeClass("checked");
				$("input[name='SecondaryPhoneNumberType'][value='" + contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.SecondaryPhoneNumberType] + "']").attr("checked", "checked").prev().addClass("checked");
				$("#Citizenship").val(contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.Citizenship]);
				$("input[name='VisaStatus']").removeAttr("checked").prev().removeClass("checked");
				$("input[name='VisaStatus'][value='" + contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.VisaStatus] + "']").attr("checked", "checked").prev().addClass("checked");
				if (contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.Aboriginal])
					$("#Aboriginal").attr("checked", "checked").next().addClass("checked");
				else
					$("#Aboriginal").removeAttr("checked").next().removeClass("checked");
				if (contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.TorresIslander])
					$("#TorresIslander").attr("checked", "checked").next().addClass("checked");
				else
					$("#TorresIslander").removeAttr("checked").next().removeClass("checked");
				$("input[name='Gender']").removeAttr("checked").prev().removeClass("checked");
				$("input[name='Gender'][value='" + contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.Gender] + "']").attr("checked", "checked").prev().addClass("checked");
				var dobMonth = $("#DateOfBirthMonth option[value='" + contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.DateOfBirthMonth] + "']").text();
				$(".DOBMonth_control .dropdown-item" + (dobMonth == "" ? ":empty" : ":contains('" + dobMonth + "')")).click();
				var dobYear = $("#DateOfBirthYear option[value='" + contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.DateOfBirthYear] + "']").text();
				$(".DOBYear_control .dropdown-item" + (dobYear == "" ? ":empty" : ":contains('" + dobYear + "')")).click();
				break;
			case "desiredjob":
				$("#DesiredJobTitle").val(desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.DesiredJobTitle]);
				if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.FullTime]) {
					$(".jobtype-icon[value='FullTime']").addClass("checked");
					$("#FullTime").attr("checked", "checked");
				} else {
					$(".jobtype-icon[value='FullTime']").removeClass("checked");
					$("#FullTime").removeAttr("checked");
				}
				if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.PartTime]) {
					$(".jobtype-icon[value='PartTime']").addClass("checked");
					$("#PartTime").attr("checked", "checked");
				} else {
					$(".jobtype-icon[value='PartTime']").removeClass("checked");
					$("#PartTime").removeAttr("checked");
				}
				if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.Contract]) {
					$(".jobtype-icon[value='Contract']").addClass("checked");
					$("#Contract").attr("checked", "checked");
				} else {
					$(".jobtype-icon[value='Contract']").removeClass("checked");
					$("#Contract").removeAttr("checked");
				}
				if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.Temp]) {
					$(".jobtype-icon[value='Temp']").addClass("checked");
					$("#Temp").attr("checked", "checked");
				} else {
					$(".jobtype-icon[value='Temp']").removeClass("checked");
					$("#Temp").removeAttr("checked");
				}
				if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.JobShare]) {
					$(".jobtype-icon[value='JobShare']").addClass("checked");
					$("#JobShare").attr("checked", "checked");
				} else {
					$(".jobtype-icon[value='JobShare']").removeClass("checked");
					$("#JobShare").removeAttr("checked");
				}
				$("input[type='radio'][name='Status']").removeAttr("checked");
				$(".availability-icon").removeClass("checked");
				$("#" + desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.Status]).attr("checked", "checked");
				$(".availability-icon[value='" + desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.Status] + "']").addClass("checked");
				var currentSalaryRateType = "SalaryRate" + desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.DesiredSalaryRate];
				var currentSalary = desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.DesiredSalaryLowerBound];
				if (currentSalaryRateType) $(".radiobutton[value='" + currentSalaryRateType + "']").click();
				else $(".radiobutton[value='SalaryRateYear']").click();
				if (currentSalary) {
					if (currentSalaryRateType == "SalaryRateYear") initializeSalary(currentSalary, 0, 250000, 5000);
					else initializeSalary(currentSalary, 0, 125, 5);
					$(".salary-slider").slider("value", currentSalary);
				}
				if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.IsSalaryNotVisible]) {
					$(".checkbox.IsSalaryNotVisible").addClass("checked");
					$("#IsSalaryNotVisible").attr("checked", "checked");
				} else {
				$(".checkbox.IsSalaryNotVisible").removeClass("checked");
				$("#IsSalaryNotVisible").removeAttr("checked");
				}
				$(".radiobutton[value='RelocationPreference" + desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.RelocationPreference] + "']").click();
				$("#RelocationLocation option").removeAttr("selected");
				if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.RelocationCountryLocationIds] != null)
					$.each(desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.RelocationCountryLocationIds], function(i, n) {
						$("#RelocationCountryLocationIds option[value='" + n + "']").attr("selected", "selected");
					});
				if (desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.RelocationCountryIds] != null)
					$.each(desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.RelocationCountryIds], function(i, n) {
						$("#RelocationCountryIds option[value='" + n + "']").attr("selected", "selected");
					});
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
			case "careerobjectives":
				$("#Objective").val(careerObjectivesMemberModel[candidateProfileKeys.CareerObjectivesKeys.Objective]);
				$("#Summary").val(careerObjectivesMemberModel[candidateProfileKeys.CareerObjectivesKeys.Summary]);
				$("#Skills").html(careerObjectivesMemberModel[candidateProfileKeys.CareerObjectivesKeys.Skills]);
				break;
			case "employmenthistory":
				var recentProfession = $("#RecentProfession option[value='" + employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.RecentProfession] + "']").text();
				$("#RecentProfession").closest(".field").find(".dropdown-item" + (recentProfession == "" ? ":empty" : ":contains('" + recentProfession + "')")).click();
				var recentSeniority = $("#RecentSeniority option[value='" + employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.RecentSeniority] + "']").text();
				$("#RecentSeniority").closest(".field").find(".dropdown-item" + (recentSeniority == "" ? ":empty" : ":contains('" + recentSeniority + "')")).click();
				$("#IndustryIds option").removeAttr("selected").closest(".field").find(".listbox-item").removeClass("selected");
				if (employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.IndustryIds])
					$.each(employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.IndustryIds], function() {
						$("#IndustryIds option[value='" + this + "']").attr("selected", "selected");
						$("#IndustryIds").closest(".field").find(".listbox-item:contains('" + $("#IndustryIds option[value='" + this + "']").text() + "')").addClass("selected");
					});
				if ($(".content.employmenthistory.edit-mode").attr("item_id") && $(".content.employmenthistory.edit-mode").attr("item_id") != "") {
					$(".newtitle").hide();
					var jobs = employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.Jobs];
					var job;
					$.each(jobs, function() {
						if (this[candidateProfileKeys.EmploymentHistoryKeys.Id] == $(".content.employmenthistory.edit-mode").attr("item_id")) job = this;
					});
					$("#StartDateYear").closest(".control").find(".dropdown-item:contains('" + (job[candidateProfileKeys.EmploymentHistoryKeys.StartDateYear] == null ? "Year" : $("#StartDateYear option[value='" + job[candidateProfileKeys.EmploymentHistoryKeys.StartDateYear] + "']").text()) + "')").click();
					$("#StartDateMonth").closest(".control").find(".dropdown-item:contains('" + (job[candidateProfileKeys.EmploymentHistoryKeys.StartDateMonth] == null ? "Month" : $("#StartDateMonth option[value='" + job[candidateProfileKeys.EmploymentHistoryKeys.StartDateMonth] + "']").text()) + "')").click();
					if (job[candidateProfileKeys.EmploymentHistoryKeys.IsCurrent]) {
						$("#EndDateYear").closest(".control").find(".dropdown-item:contains('Year')").click();
						$("#EndDateMonth").closest(".control").find(".dropdown-item:contains('Now')").click();
					} else {
						$("#EndDateMonth").closest(".control").find(".dropdown-item:contains('" + (job[candidateProfileKeys.EmploymentHistoryKeys.EndDateMonth] == null ? "Now" : $("#EndDateMonth option[value='" + job[candidateProfileKeys.EmploymentHistoryKeys.EndDateMonth] + "']").text()) + "')").click();
						$("#EndDateYear").closest(".control").find(".dropdown-item:contains('" + (job[candidateProfileKeys.EmploymentHistoryKeys.EndDateYear] == null ? "Year" : $("#EndDateYear option[value='" + job[candidateProfileKeys.EmploymentHistoryKeys.EndDateYear] + "']").text()) + "')").click();
					}
					$("#Title").val(job[candidateProfileKeys.EmploymentHistoryKeys.Title]);
					$("#Company").val(job[candidateProfileKeys.EmploymentHistoryKeys.Company]);
					$("#Description").val(job[candidateProfileKeys.EmploymentHistoryKeys.Description]);
				} else {
					$(".newtitle").show();
					$("#StartDateYear").closest(".control").find(".dropdown-item:contains('Year')").click();
					$("#StartDateMonth").closest(".control").find(".dropdown-item:contains('Month')").click();
					$("#EndDateYear").closest(".control").find(".dropdown-item:contains('Year')").click();
					$("#EndDateMonth").closest(".control").find(".dropdown-item:contains('Now')").click();
					$("#Title").val("");
					$("#Company").val("");
					$("#Description").val("");
				}
				if ($(".content .view-mode .field .item").index($(".content .view-mode .field .item[item_id='" + $(".content").attr("item_id") + "']")) == 0) $(".content.edit-mode .edit-mode .generalfields").show();
				else $(".content.edit-mode .edit-mode .generalfields").hide();
				$(".content.edit-mode .edit-mode .item-button .bottom-edge").height(0).height($(".content.edit-mode .edit-mode").height() - 302);
				break;
			case "education":
				var highestEducationLevel = $("#HighestEducationLevel option[value='" + educationMemberModel[candidateProfileKeys.EducationKeys.HighestEducationLevel] + "']").text();
				$("#HighestEducationLevel").closest(".field").find(".dropdown-item" + (highestEducationLevel == "" ? ":empty" : ":contains('" + highestEducationLevel + "')")).click();
				if ($(".content.education.edit-mode").attr("item_id") && $(".content.education.edit-mode").attr("item_id") != "") {
					$(".newtitle").hide();
					var schools = educationMemberModel[candidateProfileKeys.EducationKeys.Schools], school;
					$.each(schools, function() {
						if (this[candidateProfileKeys.EducationKeys.Id] == $(".content.education.edit-mode").attr("item_id")) school = this;
					});
					if (school[candidateProfileKeys.EducationKeys.IsCurrent]) {
						$("#EndDateYear").closest(".control").find(".dropdown-item:contains('Year')").click();
						$("#EndDateMonth").closest(".control").find(".dropdown-item:contains('Current')").click();
					} else {
						$("#EndDateMonth").closest(".control").find(".dropdown-item:contains('" + $("#EndDateMonth option[value='" + school[candidateProfileKeys.EducationKeys.EndDateMonth] + "']").text() + "')").click();
						$("#EndDateYear").closest(".control").find(".dropdown-item:contains('" + $("#EndDateYear option[value='" + school[candidateProfileKeys.EducationKeys.EndDateYear] + "']").text() + "')").click();
					}
					$("#Degree").val(school[candidateProfileKeys.EducationKeys.Degree]);
					$("#Major").val(school[candidateProfileKeys.EducationKeys.Major]);
					$("#Institution").val(school[candidateProfileKeys.EducationKeys.Institution]);
					$("#City").val(school[candidateProfileKeys.EducationKeys.City]);
					$("#Description").val(school[candidateProfileKeys.EducationKeys.Description]);
				} else {
					$(".newtitle").show();
					$("#EndDateYear").closest(".control").find(".dropdown-item:contains('Year')").click();
					$("#EndDateMonth").closest(".control").find(".dropdown-item:contains('Current')").click();
					$("#Degree").val("");
					$("#Major").val("");
					$("#Institution").val("");
					$("#City").val("");
					$("#Description").val("");
				}
				if ($(".content .view-mode .field .item").index($(".content .view-mode .field .item[item_id='" + $(".content").attr("item_id") + "']")) == 0) $(".content.edit-mode .edit-mode .generalfields").show();
				else $(".content.edit-mode .edit-mode .generalfields").hide();
				$(".content.edit-mode .edit-mode .item-button .bottom-edge").height(0).height($(".content.edit-mode .edit-mode").height() - 302);
				break;
			case "other":
				$("#Courses").val(otherMemberModel[candidateProfileKeys.OtherKeys.Courses]);
				$("#Awards").val(otherMemberModel[candidateProfileKeys.OtherKeys.Awards]);
				$("#Professional").val(otherMemberModel[candidateProfileKeys.OtherKeys.Professional]);
				$("#Interests").val(otherMemberModel[candidateProfileKeys.OtherKeys.Interests]);
				$("#Affiliations").val(otherMemberModel[candidateProfileKeys.OtherKeys.Affiliations]);
				$("#Other").val(otherMemberModel[candidateProfileKeys.OtherKeys.Other]);
				$("#Referees").val(otherMemberModel[candidateProfileKeys.OtherKeys.Referees]);
				break;
		}
	}

	addHelpIcon = function(helpForIDs) {
		$.each(helpForIDs, function() {
			var helpIcon = $("<div class='help-icon' helpfor='" + this + "'></div>");
			$("#" + this).closest(".field").append(helpIcon);
		});
	}

	organiseFields = function() {
		$("#results .control select:not(.listbox, .month_dropdown, .year_dropdown)").each(function() {
			var textBg = $("<div class='textbox-bg'><input type='text' /></div>");
			var control = $(this).parent();
			$(this).appendTo(textBg);
			control.text("").append(textBg);
			textBg.before("<div class='textbox-left'></div>").after("<div class='textbox-right'></div>");
		});
		$("#results .control:not(.photo_control) .textbox:not(.multiline_textbox)").each(function() {
			var textBg = $("<div class='textbox-bg'></div>");
			var control = $(this).parent();
			$(this).appendTo(textBg);
			control.text("").append(textBg);
			textBg.before("<div class='textbox-left'></div>").after("<div class='textbox-right'></div>");
		});
		$("#results .control .multiline_textbox").each(function() {
			var textBg = $("<div class='textarea-bg'></div>");
			var textarea = $(this);
			var control = textarea.parent();
			var height = parseInt(textarea.css("height"));
			textarea.appendTo(textBg);
			var topArrow = $("<div class='top'><div class='arrow'></div></div>"), bottomArrow = $("<div class='bottom'><div class='arrow'></div></div>"), scrollbarWrap = $("<div class='wrap'></div>"), scrollbar = $("<div class='scrollbar'></div>"), scrollbarOuter = $("<div class='outer'></div>");
			var topBarHeight = 4, bottomBarHeight = 2, arrowHeight = 16, handleHeight = 48;
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
		$("#results .checkbox_control").each(function() {
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
		$("#results .checkboxes_control input[type='checkbox']").each(function() {
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
		$("#results .radiobuttons_control").each(function() {
			var radioButtonGroup = $(this);
			$(".radio_control", this).each(function() {
				var radiobutton = $("<div class='radiobutton'></div>");
				var id = $("input[type='radio']", this).hide().attr("id");
				radiobutton.attr("value", id).prependTo(this);
				if ($("input[type='radio']", this).is(":checked"))
					radiobutton.addClass("checked");
				radiobutton.click(function(event) {
					$(".radiobutton", radioButtonGroup).removeClass("checked");
					$(this).addClass("checked");
					$("input.radio", radioButtonGroup).removeAttr("checked");
					$("#" + id, radioButtonGroup).attr("checked", "checked");
					event.stopPropagation();
				});
			});
		});
		$("#results .compulsory_field").each(function() {
			var mandatory = $("<div class='mandatory'></div>");
			mandatory.insertAfter($("> label", this));
		});
	}

	toFormattedDigits = function(nValue) {
		var num = new Number(nValue);
		var s = num.toFixed().toString();
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
			else label = "$" + toFormattedDigits(lowerBound) + " " + $(".salary-section .radiobutton.checked").parent().find("label").text();

			$(".salary-desc center").html(label);
			//set background position
			var percentage = lowerBound / maxSalary;
			var bgPosX = 0 - 425;
			var max = 100;
			$(".salary-slider .ui-slider-range").css({
				"background-position": (bgPosX - 266 * percentage) + "px -52px",
				"left": (percentage * 100) + "%",
				"width": (max - percentage * 100) + "%"
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

	updateCompletionBar = function(percentage) {
		if (!percentage) {
			var percentageText = $(".completion .fg .progressbar-text").text();
			percentage = percentageText.substring(0, percentageText.length - 1);
		} else $(".completion .fg .progressbar-text").text(percentage + "%");
		if (percentage < 3 || percentage > 98) $(".completion .progressbar .mask").hide();
		else $(".completion .progressbar .mask").show();
		var width = Math.round($(".completion .progressbar .bg").width() * percentage / 100);
		$(".completion .progressbar .fg").width(width);
		$(".completion .progressbar .mask").css("margin-left", width - 4);
	}

	getProfileDetails = function(selfUrl, profilePart, preview, print) {
		(function($) {
			if (!preview && currentRequest) currentRequest.abort();
			currentRequest = $.get(selfUrl + "/" + profilePart,
				null,
				function(data, textStatus, xmlHttpRequest) {
					if (data != "") {
						if (preview) {
							$("#preview .resume-part[partType='" + profilePart + "']").html(data);
							$("#preview .bottom-new-icon, #preview .incomplete-alert .clickon, #preview .incomplete-alert .edit-button, #preview .incomplete-alert .new-button, #preview .incomplete-alert .afterbutton").hide();
							if (profilePart == $(".six-section .current").closest(".section").attr("url"))
								$("#preview .resume-part[partType='" + profilePart + "'] .edit-mode").remove();
							$("#preview .prompt-layer").remove();
							updateDisplayText(profilePart);
							$("#preview .edit-mode").remove();
							$("#preview .item-button").remove();
							$("#preview .content.employmenthistory .view-mode .item, #preview .content.education .view-mode .item").unbind();
							if (profilePart == "desiredjob")
								$("#preview .resume-part[parttype='desiredjob'] .view-mode .availability-icon").append("<img src='../content/images/members/" + desiredJobMemberModel[candidateProfileKeys.DesiredJobKeys.Status] + ".png' />");
							if ($("#preview .resume-part:empty").length > 0) $("#preview .resume-part").hide().parent().find(".loading").show();
							else {
								$("#preview .resume-part").show().parent().find(".loading").hide();
								if (print) window.print();
							}
						} else {
							$("#results").html(data);
							initSectionContent();
						}
					}
					currentRequest = null;
				});
		})(jQuery)
	}

	saveContactDetailsData = function(url) {
		(function($) {
			if (currentRequest)
				currentRequest.abort();

			var requestData = {};
			requestData[candidateProfileKeys.ContactDetailsKeys.FirstName] = $("#FirstName").val();
			requestData[candidateProfileKeys.ContactDetailsKeys.LastName] = $("#LastName").val();
			requestData[candidateProfileKeys.ContactDetailsKeys.CountryId] = $("#CountryId option:selected").val();
			requestData[candidateProfileKeys.ContactDetailsKeys.Location] = $("#Location").val();
			requestData[candidateProfileKeys.ContactDetailsKeys.EmailAddress] = $("#EmailAddress").val();
			requestData[candidateProfileKeys.ContactDetailsKeys.SecondaryEmailAddress] = $("#SecondaryEmailAddress").val();
			requestData[candidateProfileKeys.ContactDetailsKeys.PhoneNumber] = $("#PhoneNumber").val();
			requestData[candidateProfileKeys.ContactDetailsKeys.PhoneNumberType] = $("input[type='radio'][name='PhoneNumberType']:checked").val();
			requestData[candidateProfileKeys.ContactDetailsKeys.SecondaryPhoneNumber] = $("#SecondaryPhoneNumber").val();
			requestData[candidateProfileKeys.ContactDetailsKeys.SecondaryPhoneNumberType] = $("input[type='radio'][name='SecondaryPhoneNumberType']:checked").val();
			requestData[candidateProfileKeys.ContactDetailsKeys.Citizenship] = $("#Citizenship").val();
			requestData[candidateProfileKeys.ContactDetailsKeys.VisaStatus] = $("input[type='radio'][name='VisaStatus']:checked").val();
			requestData[candidateProfileKeys.ContactDetailsKeys.Aboriginal] = $("#Aboriginal:checked").val();
			requestData[candidateProfileKeys.ContactDetailsKeys.TorresIslander] = $("#TorresIslander:checked").val();
			requestData[candidateProfileKeys.ContactDetailsKeys.Gender] = $("input[type='radio'][name='Gender']:checked").val();
			requestData[candidateProfileKeys.ContactDetailsKeys.DateOfBirthMonth] = $("#DateOfBirthMonth option:selected").val();
			requestData[candidateProfileKeys.ContactDetailsKeys.DateOfBirthYear] = $("#DateOfBirthYear option:selected").val();

			currentRequest = $.post(url,
				requestData,
				function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
						showErrInfo();
						return;
					} else if (data.Success) {
						$.extend(contactDetailsMemberModel, requestData);
						$.extend(true, candidateProfileModel, data.Profile);
						showSuccInfo("save", "");
					} else {
						showErrInfo("save", data.Errors);
					}
					currentRequest = null;
				}
			).error(function(error) {
				var data = $.parseJSON(error.responseText);
				if (!data.Success) showErrInfo("save", data.Errors);
			});
		})(jQuery);
	}

	saveDesiredJobData = function(url) {
		(function($) {
			if (currentRequest)
				currentRequest.abort();

			var requestData = {};
			requestData[candidateProfileKeys.DesiredJobKeys.DesiredJobTitle] = $("input[name='DesiredJobTitle']").val();
			requestData[candidateProfileKeys.DesiredJobKeys.FullTime] = $("#FullTime:checked").length > 0 ? $("#FullTime:checked").val() : false;
			requestData[candidateProfileKeys.DesiredJobKeys.PartTime] = $("#PartTime:checked").length > 0 ? $("#PartTime:checked").val() : false;
			requestData[candidateProfileKeys.DesiredJobKeys.Contract] = $("#Contract:checked").length > 0 ? $("#Contract:checked").val() : false;
			requestData[candidateProfileKeys.DesiredJobKeys.Temp] = $("#Temp:checked").length > 0 ? $("#Temp:checked").val() : false;
			requestData[candidateProfileKeys.DesiredJobKeys.JobShare] = $("#JobShare:checked").length > 0 ? $("#JobShare:checked").val() : false;
			requestData[candidateProfileKeys.DesiredJobKeys.Status] = $("input[type='radio'][name='Status']:checked").val();
			requestData[candidateProfileKeys.DesiredJobKeys.DesiredSalaryLowerBound] = $("#SalaryLowerBound").val();
			requestData[candidateProfileKeys.DesiredJobKeys.DesiredSalaryRate] = $("input[type='radio'][name='DesiredSalaryRate']:checked").val();
			requestData[candidateProfileKeys.DesiredJobKeys.IsSalaryNotVisible] = $("#IsSalaryNotVisible:checked").length > 0 ? $("#IsSalaryNotVisible:checked").val() : false;
			requestData[candidateProfileKeys.DesiredJobKeys.EmailSuggestedJobs] = $("#SendSuggestedJobs:checked").length > 0 ? $("#SendSuggestedJobs:checked").val() : false;
			requestData[candidateProfileKeys.DesiredJobKeys.RelocationPreference] = $("input[type='radio'][name='RelocationPreference']:checked").val();
			requestData[candidateProfileKeys.DesiredJobKeys.RelocationCountryIds] = $("#RelocationCountryIds").val();
			requestData[candidateProfileKeys.DesiredJobKeys.RelocationCountryLocationIds] = $("#RelocationCountryLocationIds").val();

			currentRequest = $.post(url,
				requestData,
				function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
						showErrInfo();
						return;
					} else if (data.Success) {
						$.extend(desiredJobMemberModel, requestData);
						$.extend(true, candidateProfileModel, data.Profile);
						showSuccInfo("save", "");
					} else {
						showErrInfo("save", data.Errors);
					}
					currentRequest = null;
				}
			).error(function(error) {
				var data = $.parseJSON(error.responseText);
				if (!data.Success) showErrInfo("save", data.Errors);
			});
		})(jQuery);
	}

	saveCareerObjectivesData = function(url) {
		(function($) {
			if (currentRequest)
				currentRequest.abort();

			var requestData = {};
			requestData[candidateProfileKeys.CareerObjectivesKeys.Objective] = $("#Objective").val();
			requestData[candidateProfileKeys.CareerObjectivesKeys.Summary] = $("#Summary").val();
			requestData[candidateProfileKeys.CareerObjectivesKeys.Skills] = $("#Skills").val();

			currentRequest = $.post(url,
				requestData,
				function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
						showErrInfo();
						return;
					} else if (data.Success) {
						$.extend(careerObjectivesMemberModel, requestData);
						$.extend(true, candidateProfileModel, data.Profile);
						showSuccInfo("save", "");
					} else {
						showErrInfo("save", data.Errors);
					}
					currentRequest = null;
				}
			).error(function(error) {
				var data = $.parseJSON(error.responseText);
				if (!data.Success) showErrInfo("save", data.Errors);
			});
		})(jQuery);
	}

	saveEmploymentHistoryData = function(url) {
		(function($) {
			if (currentRequest)
				currentRequest.abort();

			var requestData = {};
			requestData[candidateProfileKeys.EmploymentHistoryKeys.RecentProfession] = $("#RecentProfession option:selected").val();
			requestData[candidateProfileKeys.EmploymentHistoryKeys.RecentSeniority] = $("#RecentSeniority option:selected").val();
			requestData[candidateProfileKeys.EmploymentHistoryKeys.IndustryIds] = $("#IndustryIds").val();
			requestData[candidateProfileKeys.EmploymentHistoryKeys.Id] = $(".content.employmenthistory").attr("item_id");
			requestData[candidateProfileKeys.EmploymentHistoryKeys.StartDateMonth] = $("#StartDateMonth option:selected").val();
			requestData[candidateProfileKeys.EmploymentHistoryKeys.StartDateYear] = $("#StartDateYear option:selected").val();
			requestData[candidateProfileKeys.EmploymentHistoryKeys.EndDateMonth] = $("#EndDateMonth option:selected").val();
			requestData[candidateProfileKeys.EmploymentHistoryKeys.EndDateYear] = $("#EndDateYear option:selected").val();
			requestData[candidateProfileKeys.EmploymentHistoryKeys.IsCurrent] = $("#EndDateMonth option[value='']").attr("selected") == "selected";
			requestData[candidateProfileKeys.EmploymentHistoryKeys.Title] = $("#Title").val();
			requestData[candidateProfileKeys.EmploymentHistoryKeys.Company] = $("#Company").val();
			requestData[candidateProfileKeys.EmploymentHistoryKeys.Description] = $("#Description").val();

			currentRequest = $.post(url,
				requestData,
				function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
						showErrInfo();
						return;
					} else if (data.Success) {
						employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.RecentProfession] = requestData[candidateProfileKeys.EmploymentHistoryKeys.RecentProfession];
						employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.RecentSeniority] = requestData[candidateProfileKeys.EmploymentHistoryKeys.RecentSeniority];
						employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.IndustryIds] = requestData[candidateProfileKeys.EmploymentHistoryKeys.IndustryIds];
						delete requestData[candidateProfileKeys.EmploymentHistoryKeys.RecentProfession];
						delete requestData[candidateProfileKeys.EmploymentHistoryKeys.RecentSeniority];
						delete requestData[candidateProfileKeys.EmploymentHistoryKeys.IndustryIds];
						var jobs = employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.Jobs], found = false;
						$.each(jobs, function() {
							if (this[candidateProfileKeys.EmploymentHistoryKeys.Id] == requestData[candidateProfileKeys.EmploymentHistoryKeys.Id]) {
								$.extend(this, requestData);
								this[candidateProfileKeys.EmploymentHistoryKeys.Id] = data.JobId;
								found = true;
							};
						});
						if (!found) {
							requestData[candidateProfileKeys.EmploymentHistoryKeys.Id] = data.JobId;
							jobs.push(requestData);
						}
						$.extend(true, candidateProfileModel, data.Profile);
						showSuccInfo("save", "");
					} else {
						showErrInfo("save", data.Errors);
					}
					currentRequest = null;
				}
			).error(function(error) {
				var data = $.parseJSON(error.responseText);
				if (!data.Success) showErrInfo("save", data.Errors);
			});
		})(jQuery);
	}

	saveEducationData = function(url) {
		(function($) {
			if (currentRequest)
				currentRequest.abort();

			var requestData = {};
			requestData[candidateProfileKeys.EducationKeys.HighestEducationLevel] = $("#HighestEducationLevel option:selected").val();
			requestData[candidateProfileKeys.EducationKeys.Id] = $(".content.education").attr("item_id");
			requestData[candidateProfileKeys.EducationKeys.EndDateMonth] = $("#EndDateMonth option:selected").val();
			requestData[candidateProfileKeys.EducationKeys.EndDateYear] = $("#EndDateYear option:selected").val();
			requestData[candidateProfileKeys.EducationKeys.IsCurrent] = $("#EndDateMonth option[value='']").attr("selected") == "selected";
			requestData[candidateProfileKeys.EducationKeys.Degree] = $("#Degree").val();
			requestData[candidateProfileKeys.EducationKeys.Major] = $("#Major").val();
			requestData[candidateProfileKeys.EducationKeys.Institution] = $("#Institution").val();
			requestData[candidateProfileKeys.EducationKeys.City] = $("#City").val();
			requestData[candidateProfileKeys.EducationKeys.Description] = $("#Description").val();

			currentRequest = $.post(url,
				requestData,
				function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
						showErrInfo();
						return;
					} else if (data.Success) {
						educationMemberModel[candidateProfileKeys.EducationKeys.HighestEducationLevel] = requestData[candidateProfileKeys.EducationKeys.HighestEducationLevel];
						delete requestData[candidateProfileKeys.EducationKeys.HighestEducationLevel];
						var schools = educationMemberModel[candidateProfileKeys.EducationKeys.Schools], found = false;
						$.each(schools, function() {
							if (this[candidateProfileKeys.EducationKeys.Id] == requestData[candidateProfileKeys.EducationKeys.Id]) {
								$.extend(this, requestData);
								this[candidateProfileKeys.EducationKeys.Id] = data.SchoolId;
								found = true;
							};
						});
						if (!found) {
							requestData[candidateProfileKeys.EducationKeys.Id] = data.SchoolId;
							schools.push(requestData);
						}
						$.extend(true, candidateProfileModel, data.Profile);
						showSuccInfo("save", "");
					} else {
						showErrInfo("save", data.Errors);
					}
					currentRequest = null;
				}
			).error(function(error) {
				var data = $.parseJSON(error.responseText);
				if (!data.Success) showErrInfo("save", data.Errors);
			});
		})(jQuery);
	}

	saveOtherData = function(url) {
		(function($) {
			if (currentRequest)
				currentRequest.abort();

			var requestData = {};
			requestData[candidateProfileKeys.OtherKeys.Courses] = $("#Courses").val();
			requestData[candidateProfileKeys.OtherKeys.Awards] = $("#Awards").val();
			requestData[candidateProfileKeys.OtherKeys.Professional] = $("#Professional").val();
			requestData[candidateProfileKeys.OtherKeys.Interests] = $("#Interests").val();
			requestData[candidateProfileKeys.OtherKeys.Affiliations] = $("#Affiliations").val();
			requestData[candidateProfileKeys.OtherKeys.Other] = $("#Other").val();
			requestData[candidateProfileKeys.OtherKeys.Referees] = $("#Referees").val();

			currentRequest = $.post(url,
				requestData,
				function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
						showErrInfo();
						return;
					} else if (data.Success) {
						$.extend(otherMemberModel, requestData);
						$.extend(true, candidateProfileModel, data.Profile);
						showSuccInfo("save", "");
					} else {
						showErrInfo("save", data.Errors);
					}
					currentRequest = null;
				}
			).error(function(error) {
				var data = $.parseJSON(error.responseText);
				if (!data.Success) showErrInfo("save", data.Errors);
			});
		})(jQuery);
	}

	saveCandidateProfileData = function(url, type) {
		(function($) {
			if (currentRequest)
				currentRequest.abort();

			var requestData = {};
			requestData[candidateProfileKeys.VisibilityKeys.ShowResume] = $(".set-visibility .text-on").hasClass("active");
			requestData[candidateProfileKeys.VisibilityKeys.ShowName] = $("#ShowName:checked").length > 0 ? true : false;
			requestData[candidateProfileKeys.VisibilityKeys.ShowPhoneNumbers] = $("#ShowPhoneNumbers:checked").length > 0 ? true : false;
			requestData[candidateProfileKeys.VisibilityKeys.ShowProfilePhoto] = $("#ShowProfilePhoto:checked").length > 0 ? true : false;
			requestData[candidateProfileKeys.VisibilityKeys.ShowRecentEmployers] = $("#ShowRecentEmployers:checked").length > 0 ? true : false;

			currentRequest = $.post(url,
				requestData,
				function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
						showErrInfo();
						return;
					} else if (data.Success) {
						$.extend(candidateVisibilityModel, requestData);
						showSuccInfo(type, "");
					} else {
						showErrInfo("save", data.Errors);
					}
					currentRequest = null;
				}
			).error(function(error) {
				var data = $.parseJSON(error.responseText);
				if (!data.Success) showErrInfo("save", data.Errors);
			});
		})(jQuery);
	}

	deletePhoto = function(url) {
		(function($) {
			if (currentRequest) currentRequest.abort();
			var requestData = {};
			currentRequest = $.post(url,
				requestData,
				function(data) {
					if (data == "") {
						showErrInfo();
						return;
					} else if (data.Success) {
						contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhotoId] = "";
						if (contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhotoId] && contactDetailsMemberModel[candidateProfileKeys.ContactDetailsKeys.PhotoId] != "") {
							$(".photo-fg").attr("src", $(".photo-fg.small").attr("url")).show();
							$(".photo_control .delete").show();
						}
						else {
							$(".photo-fg").hide();
							$(".photo_control .delete").hide();
						}
					} else {
						showErrInfo("save", data.Errors);
					}
					currentRequest = null;
				}
			).error(function(error) {
				var data = $.parseJSON(error.responseText);
				if (!data.Success) showErrInfo("save", data.Errors);
			});
		})(jQuery);
	}

	deleteEducation = function(resumeSchoolId, url) {
		(function($) {
			if (currentRequest)
				currentRequest.abort();

			var requestData = {};
			requestData[candidateProfileKeys.EducationKeys.Id] = resumeSchoolId;

			currentRequest = $.post(url,
				requestData,
				function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
						showErrInfo();
						return;
					} else if (data.Success) {
						var schools = educationMemberModel[candidateProfileKeys.EducationKeys.Schools];
						if (schools.length == 1) {
							schools[0][candidateProfileKeys.EducationKeys.Id] = "";
						} else {
							var currentIndex = -1;
							$.each(schools, function(index, element) {
								if (this[candidateProfileKeys.EducationKeys.Id] == requestData[candidateProfileKeys.EducationKeys.Id]) currentIndex = index;
							});
							if (currentIndex > -1) schools.splice(currentIndex, 1);
						}
						$.extend(true, candidateProfileModel, data.Profile);
						showSuccInfo("save", "");
					} else {
						showErrInfo("save", data.Errors);
					}
					currentRequest = null;
				}
			).error(function(error) {
				var data = $.parseJSON(error.responseText);
				if (!data.Success) showErrInfo("save", data.Errors);
			});
		})(jQuery);
	}

	deleteEmploymentHistory = function(resumeJobId, url) {
		(function($) {
			if (currentRequest)
				currentRequest.abort();

			var requestData = {};
			requestData[candidateProfileKeys.EmploymentHistoryKeys.Id] = resumeJobId;

			currentRequest = $.post(url,
				requestData,
				function(data, textStatus, xmlHttpRequest) {
					if (data == "") {
						showErrInfo();
						return;
					} else if (data.Success) {
						var jobs = employmentMemberModel[candidateProfileKeys.EmploymentHistoryKeys.Jobs];
						if (jobs.length == 1) {
							jobs[0][candidateProfileKeys.EmploymentHistoryKeys.Id] = "";
						} else {
							var currentIndex = -1;
							$.each(jobs, function(index, element) {
								if (this[candidateProfileKeys.EmploymentHistoryKeys.Id] == requestData[candidateProfileKeys.EmploymentHistoryKeys.Id]) currentIndex = index;
							});
							if (currentIndex > -1) jobs.splice(currentIndex, 1);
						}
						$.extend(true, candidateProfileModel, data.Profile);
						showSuccInfo("save", "");
					} else {
						showErrInfo("save", data.Errors);
					}
					currentRequest = null;
				}
			).error(function(error) {
				var data = $.parseJSON(error.responseText);
				if (!data.Success) showErrInfo("save", data.Errors);
			});
		})(jQuery);
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
			if (type == "listbox") item = $("<div class='" + type + "-item'><div class='checkbox'></div>" + $(this).text() + "</div>");
			item.click(function (e) {
				if (type == "dropdown") {
					scrollpan.hide();
					var index = $("." + type + "-item", $(this).parent()).index(this);
					$("select option", parent).removeAttr("selected");
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

/*
 * jQuery autoResize (textarea auto-resizer)
 * @copyright James Padolsey http://james.padolsey.com
 * @version 1.04
 */

(function($){
    
    $.fn.autoResize = function(options) {
        
        // Just some abstracted details,
        // to make plugin users happy:
        var settings = $.extend({
            onResize : function(){},
            animate : false,
            animateDuration : 150,
            animateCallback : function(){},
            extraSpace : 8,
            limit: 100000
        }, options);
        
        // Only textarea's auto-resize:
        this.filter('textarea').each(function(){
            
                // Get rid of scrollbars and disable WebKit resizing:
				var textarea = $(this).css({resize:'none','overflow-y':'hidden'}),
            
                // Cache original height, for use later:
                origHeight = textarea.height(),
                
                lastScrollTop = null,
                updateSize = function() {
					var totalLines, currentLine;
					var sContent = $(this).val();
					
					var bGTE = jQuery.browser.mozilla || jQuery.browser.msie;
					
					if ($(this).css('font-family') == 'monospace'           // mozilla
					||  $(this).css('font-family') == '-webkit-monospace'   // Safari
					||  $(this).css('font-family') == '"Courier New"') {    // Opera
						var charsPerLine = $(this).attr("charsPerLine");
						var iChar = 0;
						var iLines = 1;
						var sWord = '';
						var oSelection = $(this).getSelection();
						var aLetters = sContent.split("");
						var aLines = [];

						for (var w in aLetters) {
							if (aLetters[w] == "\n") {
								iChar = 0;
								aLines.push(parseInt(w));
								sWord = '';
							} else if (aLetters[w] == " ") {    
								var wordLength = parseInt(sWord.length);
								
								
								if ((bGTE && iChar + wordLength >= charsPerLine)
								|| (!bGTE && iChar + wordLength > charsPerLine)) {
									iChar = wordLength + 1;
									aLines.push(parseInt(w) - wordLength);
								} else {
									iChar += wordLength + 1; // 1 more char for the space
								}
								
								sWord = '';
							} else if (aLetters[w] == "\t") {
								iChar += 4;
							} else {
								sWord += aLetters[w];     
							}
						}
						
						var iLine = aLines.length;
						for(var i in aLines) {
							if (oSelection.end <= aLines[i]) {
								iLine = parseInt(i) - 1;
								break;
							}
						}
						totalLines = aLines.length + 1;
						currentLine = iLine + 1;
					}
					
					//set scrollbar value
					if (totalLines * 15 > $(this).parent().height()) {
						var percent = currentLine / totalLines * 100;
						$(this).parent().find(".textarea-scrollbar").slider("value", 100 - percent);
						$(this).parent().find(".textarea-scrollbar-outer").show();
					} else {
						$(this).parent().find(".textarea-scrollbar-outer").hide();
						$(this).css("top", "5px");
					}

					// Find the height of text:
                    //var scrollTop = Math.max(clone.scrollTop(), origHeight) + settings.extraSpace,
					var scrollTop = Math.max(totalLines * 15, origHeight) + settings.extraSpace,
                        toChange = $(this);

					// Don't do anything if scrollTip hasen't changed:
                    if (lastScrollTop === scrollTop) { return; }
                    lastScrollTop = scrollTop;
					
                    // Check for limit:
                    if ( scrollTop >= settings.limit ) {
                        $(this).css('overflow-y','');
                        return;
                    }
                    // Fire off callback:
                    settings.onResize.call(this);
					
                    // Either animate or directly apply height:
                    settings.animate && textarea.css('display') === 'block' ?
                        toChange.stop().animate({height:scrollTop}, settings.animateDuration, settings.animateCallback)
                        : toChange.height(scrollTop);
                };
            
            // Bind namespaced handlers to appropriate events:
            textarea
                .unbind('.dynSiz')
                .bind('keyup.dynSiz', updateSize)
                .bind('keydown.dynSiz', updateSize)
                .bind('change.dynSiz', updateSize);
            
        });
        
        // Chain:
        return this;
    };
})(jQuery);

/*
 * jQuery plugin: fieldSelection - v0.1.0 - last change: 2006-12-16
 * (c) 2006 Alex Brem <alex@0xab.cd> - http://blog.0xab.cd
 */

(function() {

    var fieldSelection = {

        getSelection: function() {

            var e = this.jquery ? this[0] : this;

            return (

                /* mozilla / dom 3.0 */
                ('selectionStart' in e && function() {
                    var l = e.selectionEnd - e.selectionStart;
                    return {
                        start: e.selectionStart,
                        end: e.selectionEnd,
                        length: l,
                        text: e.value.substr(e.selectionStart, l)
                    };
                }) ||

                /* exploder */
                (document.selection && function() {

                    e.focus();

                    var r = document.selection.createRange();
                    if (r == null) {
                        return { start: 0, end: e.value.length, length: 0 }
                    }

                    var re = e.createTextRange();
                    var rc = re.duplicate();
                    re.moveToBookmark(r.getBookmark());
                    rc.setEndPoint('EndToStart', re);

                    return { start: rc.text.length, end: rc.text.length + r.text.length, length: r.text.length, text: r.text };
                }) ||

                /* browser not supported */
                function() {
                    return { start: 0, end: e.value.length, length: 0 };
                }

            )();

        },

        replaceSelection: function() {

            var e = this.jquery ? this[0] : this;
            var text = arguments[0] || '';

            return (

                /* mozilla / dom 3.0 */
                ('selectionStart' in e && function() {
                    e.value = e.value.substr(0, e.selectionStart) + text + e.value.substr(e.selectionEnd, e.value.length);
                    return this;
                }) ||

                /* exploder */
                (document.selection && function() {
                    e.focus();
                    document.selection.createRange().text = text;
                    return this;
                }) ||

                /* browser not supported */
                function() {
                    e.value += text;
                    return this;
                }

            )();

        }

    };

    jQuery.each(fieldSelection, function(i) { jQuery.fn[i] = this; });

})(jQuery);
