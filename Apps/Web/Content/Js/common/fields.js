(function($) {
	organiseFields = function(context) {
		if (!context) context = $("body");
		//text box
		$(".control .textbox:not(.password_textbox, .multiline_textbox, .autoexpand)", context).each(function() {
			var textBg = $("<div class='bg'></div>");
			var control = $(this).parent();
			$(this).appendTo(textBg);
			control.append(textBg);
			textBg.before("<div class='leftbar'></div>").after("<div class='rightbar'></div>");
			if ($(this).val() == "" && ($(this).attr("data-watermark") != undefined && $(this).attr("data-watermark") != "")) $(this).val($(this).attr("data-watermark")).addClass("mask");
			$.fn.extend({
				realValue : function() {
					return $(this).val() == $(this).attr("data-watermark") ? "" : $(this).val();
				}
			});
		}).focus(function() {
			if ($(this).attr("readonly") == "readonly") return;
			if ($(this).val() == $(this).attr("data-watermark")) $(this).val("").removeClass("mask");
			$(this).closest(".control").addClass("active");
		}).blur(function() {
			if ($(this).val() == "" && ($(this).attr("data-watermark") != undefined && $(this).attr("data-watermark") != "")) $(this).val($(this).attr("data-watermark")).addClass("mask");
			$(this).closest(".control").removeClass("active");
		});
		$(".control select:not(.listbox, .month_dropdown, .year_dropdown)", context).each(function() {
			var textBg = $("<div class='bg'><input type='text' /></div>");
			var control = $(this).parent();
			$(this).appendTo(textBg);
			control.text("").append(textBg);
			textBg.before("<div class='leftbar'></div>").after("<div class='rightbar'></div>");
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
		$(".checkboxes_control input[type='checkbox']", context).each(function() {
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
		$(".compulsory_field", context).each(function() {
			var mandatory = $("<div class='mandatory'></div>");
			mandatory.insertAfter($("> label", this));
		});
		$(".control .multiline_textbox:not(.autoexpand)", context).each(function() {
			var textBg = $("<div class='textarea-bg'></div>");
			var textarea = $(this);
			var control = textarea.parent();
			var height = parseInt(textarea.css("height"));
			textarea.appendTo(textBg);
			var topArrow = $("<div class='top'><div class='arrow'></div></div>"), bottomArrow = $("<div class='bottom'><div class='arrow'></div></div>"), scrollbarWrap = $("<div class='wrap'></div>"), scrollbar = $("<div class='scrollbar'></div>"), scrollbarOuter = $("<div class='outer'></div>");
			var topBarHeight = 0, bottomBarHeight = 2, arrowHeight = 17, handleHeight = 48, paddingTop = 5, paddingBottom = 5;
			scrollbarOuter.append(topArrow).append(scrollbarWrap).append(bottomArrow);
			textBg.append(scrollbarOuter);
			scrollbarWrap.append(scrollbar).height(height + topBarHeight + bottomBarHeight + paddingTop + paddingBottom - (arrowHeight * 2));
			scrollbar.height(height + topBarHeight + bottomBarHeight + paddingTop + paddingBottom - (arrowHeight * 2) - handleHeight);
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
		$(".control .textbox.autoexpand", context).each(function() {
			var textBg = $("<div class='bg'></div>");
			var textarea = $(this);
			var control = $(this).parent().addClass("autoexpand");
			$(this).appendTo(textBg);
			control.append(textBg);
			textBg.before("<div class='topbar'></div>").after("<div class='bottombar'></div>");
			if ($(this).val() == "" && ($(this).attr("data-watermark") != undefined && $(this).attr("data-watermark") != "")) $(this).val($(this).attr("data-watermark")).addClass("mask");
			$.fn.extend({
				realValue : function() {
					return $(this).val() == $(this).attr("data-watermark") ? "" : $(this).val();
				}
			});
			//init textarea event
			change = function() {
				if (this.scrollHeight == this.clientHeight) $(this).height("0px");
				$(this).height((this.scrollHeight - 8) + "px");
			}
			textarea.change(change).keyup(change).keydown(change).keypress(change);
		}).focus(function() {
			if ($(this).attr("readonly") == "readonly") return;
			if ($(this).val() == $(this).attr("data-watermark")) $(this).val("").removeClass("mask");
			$(this).closest(".control").addClass("active");
		}).blur(function() {
			if ($(this).val() == "" && ($(this).attr("data-watermark") != undefined && $(this).attr("data-watermark") != "")) $(this).val($(this).attr("data-watermark")).addClass("mask");
			$(this).closest(".control").removeClass("active");
		});
	}
	
	showSuccInfo = function(type, context, extra_msg) {
		if (!context) context = $("body");
		var message = "Your operation has been successfully performed.";
		if (extra_msg != undefined && extra_msg != "") message = extra_msg;
		$(".succinfo", context).show().find(".prompt").text(message);
		$(".validationerror", context).hide();
		$(".control", context).removeClass("error");
		$(".field .errormsg", context).remove();
	}

	showErrInfo = function(type, context, errObj, extMessage) {
		if (!context) context = $("body");
		var message = "Please review the errors below";
		$(".validationerror .prompt", context).text(message);
		$(".validationerror", context).show();
		$(".succinfo", context).hide();
		$(".field .errormsg", context).remove();
		$.each(errObj, function() {
			$("#" + this["Key"], context).closest(".control").addClass("error").closest(".field").find(".help-area").addClass("error").end().append("<span class='errormsg' errorfor='" + this["Key"] + "'>" + this["Message"] + "</span>");
		});
		$(".validationerror ul", context).text("");
		$.each(errObj, function() {
			$(".validationerror ul", context).append("<li><div class='listicon'></div><div class='text'>" + this["Message"] + "</div></li>");
		});
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
			scrollpan.hide().bind("mousedownoutside", function(event) {
				if ($(event.target).closest(".dropdown_control").length > 0) return;
				$(this).hide();
				event.stopPropagation();
			});
			
			dd.click(function (event) {
				if (scrollpan.css("display") == "none") {
					$(".dropdown-scrollpan").hide();
					var parentWidth = dd.parent().width();
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
			if ($(this).attr("disabled") == "disabled") item.addClass("disabled");
			item.click(function (e) {
				if (item.hasClass("disabled")) return;
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
			var handleHeight = 49;
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
