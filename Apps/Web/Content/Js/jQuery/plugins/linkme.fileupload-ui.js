(function($) {

    var undef = 'undefined',
        func = 'function',
        UploadHandler,
        methods,

        LocalImage = function(file, imageTypes) {
            var img,
                fileReader;
            if (!imageTypes.test(file.type)) {
                return null;
            }
            img = document.createElement('img');
            if (typeof URL !== undef && typeof URL.createObjectURL === func) {
                img.src = URL.createObjectURL(file);
                img.onload = function() {
                    URL.revokeObjectURL(this.src);
                };
                return img;
            }
            if (typeof FileReader !== undef) {
                fileReader = new FileReader();
                if (typeof fileReader.readAsDataURL === func) {
                    fileReader.onload = function(e) {
                        img.src = e.target.result;
                    };
                    fileReader.readAsDataURL(file);
                    return img;
                }
            }
            return null;
        };

    UploadHandler = function(container, options) {
        var uploadHandler = this,
            dragOverTimeout,
            isDropZoneEnlarged;

        this.dropZone = container;
        this.imageTypes = /^image\/(gif|jpeg|png)$/;
        this.previewSelector = '.file_upload_preview';
        this.progressSelector = '.file_upload_progress div';
        this.cancelSelector = '.file_upload_cancel button';
        this.cssClassSmall = 'file_upload_small';
        this.cssClassLarge = 'file_upload_large';
        this.cssClassHighlight = 'file_upload_highlight';
        this.dropEffect = 'highlight';
        this.uploadTable = this.downloadTable = null;

        this.buildUploadRow = this.buildDownloadRow = function() {
            return null;
        };

        this.addNode = function(parentNode, node, callBack) {
            if (node) {
                node.css('display', 'none').appendTo(parentNode).fadeIn(function() {
                    if (typeof callBack === func) {
                        try {
                            callBack();
                        } catch (e) {
                            // Fix endless exception loop:
                            $(this).stop();
                            throw e;
                        }
                    }
                });
            } else if (typeof callBack === func) {
                callBack();
            }
        };

        this.removeNode = function(node, callBack) {
            if (node) {
                node.fadeOut(function() {
                    $(this).remove();
                    if (typeof callBack === func) {
                        try {
                            callBack();
                        } catch (e) {
                            // Fix endless exception loop:
                            $(this).stop();
                            throw e;
                        }
                    }
                });
            } else if (typeof callBack === func) {
                callBack();
            }
        };

        this.onAbort = function(event, files, index, xhr, handler) {
            handler.removeNode(handler.uploadRow);
        };

        this.cancelUpload = function(event, files, index, xhr, handler) {
            var readyState = xhr.readyState;
            xhr.abort();
            // If readyState is below 2, abort() has no effect:
            if (isNaN(readyState) || readyState < 2) {
                handler.onAbort(event, files, index, xhr, handler);
            }
        };

        this.initProgressBar = function(node, value) {
            if (typeof node.progressbar === func) {
                return node.progressbar({
                    value: value
                });
            } else {
                var progressbar = $('<progress value="' + value + '" max="100"/>').appendTo(node);
                progressbar.progressbar = function(key, value) {
                    progressbar.attr('value', value);
                };
                return progressbar;
            }
        };

        this.initUploadRow = function(event, files, index, xhr, handler, callBack) {
            var uploadRow = handler.uploadRow = handler.buildUploadRow(files, index, handler);
            if (uploadRow) {
                handler.progressbar = handler.initProgressBar(
                    uploadRow.find(handler.progressSelector),
                    (xhr.upload ? 0 : 100)
                );
                uploadRow.find(handler.cancelSelector).click(function(e) {
                    handler.cancelUpload(e, files, index, xhr, handler);
                });
                uploadRow.find(handler.previewSelector).each(function() {
                    $(this).append(new LocalImage(files[index], handler.imageTypes));
                });
            }
            handler.addNode(
                (typeof handler.uploadTable === func ? handler.uploadTable(handler) : handler.uploadTable),
                uploadRow,
                callBack
            );
        };

        this.initUpload = function(event, files, index, xhr, handler, callBack) {
            handler.initUploadRow(event, files, index, xhr, handler, function() {
                if (typeof handler.beforeSend === func) {
                    handler.beforeSend(event, files, index, xhr, handler, callBack);
                } else {
                    callBack();
                }
            });
        };

        this.onProgress = function(event, files, index, xhr, handler) {
            if (handler.progressbar) {
                handler.progressbar.progressbar(
                    'value',
                    parseInt(event.loaded / event.total * 100, 10)
                );
            }
        };

        this.parseResponse = function(xhr) {
            if (typeof xhr.responseText !== undef) {
                return $.parseJSON(xhr.responseText);
            } else {
                // Instead of an XHR object, an iframe is used for legacy browsers:
                return $.parseJSON(xhr.contents().text());
            }
        };

        this.initDownloadRow = function(event, files, index, xhr, handler, callBack) {
            var json, downloadRow;
            try {
                json = handler.response = handler.parseResponse(xhr);
                downloadRow = handler.downloadRow = handler.buildDownloadRow(json, handler);
                handler.addNode(
                    (typeof handler.downloadTable === func ? handler.downloadTable(handler) : handler.downloadTable),
                    downloadRow,
                    callBack
                );
            } catch (e) {
                if (typeof handler.onError === func) {
                    handler.originalEvent = event;
                    handler.onError(e, files, index, xhr, handler);
                } else {
                    throw e;
                }
            }
        };

        this.onLoad = function(event, files, index, xhr, handler) {
            handler.removeNode(handler.uploadRow, function() {
                handler.initDownloadRow(event, files, index, xhr, handler, function() {
                    if (typeof handler.onComplete === func) {
                        handler.onComplete(event, files, index, xhr, handler);
                    }
                });
            });
        };

        this.dropZoneEnlarge = function() {
            if (!isDropZoneEnlarged) {
                if (typeof uploadHandler.dropZone.switchClass === func) {
                    uploadHandler.dropZone.switchClass(
                        uploadHandler.cssClassSmall,
                        uploadHandler.cssClassLarge
                    );
                } else {
                    uploadHandler.dropZone.addClass(uploadHandler.cssClassLarge);
                    uploadHandler.dropZone.removeClass(uploadHandler.cssClassSmall);
                }
                isDropZoneEnlarged = true;
            }
        };

        this.dropZoneReduce = function() {
            if (typeof uploadHandler.dropZone.switchClass === func) {
                uploadHandler.dropZone.switchClass(
                    uploadHandler.cssClassLarge,
                    uploadHandler.cssClassSmall
                );
            } else {
                uploadHandler.dropZone.addClass(uploadHandler.cssClassSmall);
                uploadHandler.dropZone.removeClass(uploadHandler.cssClassLarge);
            }
            isDropZoneEnlarged = false;
        };

        this.onDocumentDragEnter = function(event) {
            uploadHandler.dropZoneEnlarge();
        };

        this.onDocumentDragOver = function(event) {
            if (dragOverTimeout) {
                clearTimeout(dragOverTimeout);
            }
            dragOverTimeout = setTimeout(function() {
                uploadHandler.dropZoneReduce();
            }, 200);
        };

        this.onDragEnter = this.onDragLeave = function(event) {
            uploadHandler.dropZone.toggleClass(uploadHandler.cssClassHighlight);
        };

        this.onDrop = function(event) {
            if (dragOverTimeout) {
                clearTimeout(dragOverTimeout);
            }
            if (uploadHandler.dropEffect && typeof uploadHandler.dropZone.effect === func) {
                uploadHandler.dropZone.effect(uploadHandler.dropEffect, function() {
                    uploadHandler.dropZone.removeClass(uploadHandler.cssClassHighlight);
                    uploadHandler.dropZoneReduce();
                });
            } else {
                uploadHandler.dropZone.removeClass(uploadHandler.cssClassHighlight);
                uploadHandler.dropZoneReduce();
            }
        };

        $.extend(this, options);
    };

    methods = {
        init: function(options) {
            return this.each(function() {
                $(this).fileUpload(new UploadHandler($(this), options));
            });
        },

        option: function(option, value, namespace) {
            if (typeof option === undef || (typeof option === 'string' && typeof value === undef)) {
                return $(this).fileUpload('option', option, value, namespace);
            }
            return this.each(function() {
                alert("optionea options :" + options + "| value:" + value + "| namespace :" + namespace);
                $(this).fileUpload('option', option, value, namespace);
            });
        },

        destroy: function(namespace) {
            return this.each(function() {
                $(this).fileUpload('destroy', namespace);
            });
        }
    };

    $.fn.fileUploadUI = function(method) {
        if (methods[method]) {
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            return methods.init.apply(this, arguments);
        } else {
            $.error('Method ' + method + ' does not exist on jQuery.fileUploadUI');
        }
    };

    formatFileName = function(fileName) {
        // Remove any path information:
        return fileName.replace(/^.*[\/\\]/, '');
    };

    $.fn.fileUploadAction = function() {
        $(this).fileUploadUI({
            uploadTable: $('#files'),
            downloadTable: $('#files'),
            beforeSend: function(event, files, index, xhr, handler, callBack) {
                if (files[index].size > 2097152) { //If individual file size exceeds 2MB (2097152 Bytes)
                    alert("The size of the file exceeds the maximum allowed of 2MB.");
                    handler.cancelUpload(event, files, index, xhr, handler);
                    return false;
                }

                var attachmentIds = new Array();
                var totalFileSize = 0;
                $("#files").find(".file").each(function() {
                    attachmentIds.push($(this).attr("id"));
                    totalFileSize = totalFileSize + parseInt($(this).find(".file-size").text());
                });
                totalFileSize = totalFileSize + files[index].size;
                if (totalFileSize > 5242880) { //If total file size exceeds 5MB (5242880 Bytes)
                    alert("The total size of all files exceeds the maximum allowed of 5MB.");
                    handler.cancelUpload(event, files, index, xhr, handler);
                    return false;
                }
                // file object can be accessed as JSON : {"file":[{"name":"","type":"","size":""}]}

                var url = apiAttachUrl;
                if (attachmentIds.length != 0) {
                    url = url + "?" + api.getArrayQueryString("attachmentId", attachmentIds);
                }

                $("#file_uploader").hide();

                handler.url = url;
                callBack();
            },
            buildUploadRow: function(files, index) {
                $("#send-message").addClass("in-progress");
                var buildUploadRowHtml = "";
                buildUploadRowHtml = '<div>' +
                        '<span class="filename">' + formatFileName(files[index].name) + '<\/span>' +
                        '<span class="file_upload_progress"><span>&nbsp;<\/span><\/span>' +
                        '<span class="file_upload_cancel" title="Cancel"><span class="ui-icon ui-icon-cancel">Cancel<\/span><\/span>' +
				     '<\/div>';

                return $(buildUploadRowHtml);
            },
            buildDownloadRow: function(file) {
                if (file.Success) {
                    return $('<div id="' + file.Id + '" class="file">' +
                            '<span class="filename">' + file.Name + '<\/span> <span class="filesize">&nbsp;(<span class="file-size">' + file.Size + '<\/span>B)<\/span>' +
                            '<span class="file_delete" title="Delete"><span class="icon icon-delete">Delete<\/span><\/span>' +
					     '<\/div>');
                }
                else {
                    alert(file.Errors[0].Message);
                    return "";
                }
            },
            onComplete: function(event, files, index, xhr, handler) {
                $("#send-message").removeClass("in-progress");
                $(".file_delete").click(function() {
                    var detachFileId = $(this).closest("div").attr("id");
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        url: apiDetachUrl + "?attachmentId=" + $(this).closest("div").attr("id"),
                        success: function(data) {
                            if (!(data == null)) {
                                if (data.Success) {
                                    $("#" + detachFileId).remove();
                                }
                            }
                        }
                    });
                });
            }
        });
    }

} (jQuery));