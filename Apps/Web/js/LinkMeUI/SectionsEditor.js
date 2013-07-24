if(!window.console) {
    window.console = new Object();
    window.console.log = function() {};
}

//TODO 3.4: move to a separate .js file, and include it only for dev/uat
LinkMeUI.Editor.Stats = {
    divLogInitialised : false, 
    results : $H(),
    usages : $H(),
    
    TempTest : function(repeatCount) {
        if(!repeatCount)
            repeatCount = 10;
            
        LinkMeUI.Editor.Stats.Reset();
        var a = $A(['editing_resume-section-view', 'displaying_resume-section-view', 'resume_error', 'empty_resume-section-view', 'js_button-update', 'js_button-cancel']);
        for(var i = 0; i < repeatCount; i++)
            a.each(
            function(cName) {
                LinkMeUI.Editor.Stats.Begin('GetElementsByClassName');
                var v = LinkMeUI.LocateHelper.GetElementsByClassName(document.body, cName);
                LinkMeUI.Editor.Stats.End();
            });        

        for(var i = 0; i < repeatCount; i++)
            a.each(
            function(cName) {
                LinkMeUI.Editor.Stats.Begin('.select');
                var v = Element.select(document.body, '.' + cName);
                LinkMeUI.Editor.Stats.End();            
            }); 
        
        LinkMeUI.Editor.Stats.Display();
    },
    
    LogUsage: function(name)  {
        if(this.usages.get(name) != null) {
            this.usages.set(name, this.usages.get(name) + 1);
        } else {
            this.usages.set(name, 1);
        }
    },
    
    PrintUsages : function() {
        this.CheckLogCreated();
        $('divLog').update('');
        this.usages.each(function(pair) {
            $('divLog').update($('divLog').innerHTML + pair.key + ': ' + pair.value + '<br/>');
        });
    },
    
    Reset : function() {
        this.results = $H();
        if($('divLog')) {$('divLog').update('');}
    },
    
    Begin : function(name) {
        this.startDate = new Date();
        this.name = name;
    },
    
    End : function(doDebug) {
            
        this.endDate = new Date();
        var s = this.endDate.getSeconds() - this.startDate.getSeconds();
        var ms = 0;
        if(s != 0) {            
            ms = (1000 - this.startDate.getMilliseconds()) + 
                     this.endDate.getMilliseconds();
        } else {
            ms = this.endDate.getMilliseconds() - this.startDate.getMilliseconds();
        }
        
        if(doDebug)
            debugger;
                    
        if(!this.results.get(this.name)) {
            this.results.set(this.name, new Object());
            this.results.get(this.name).seconds = s;
            this.results.get(this.name).mseconds = ms;
        } else {
            this.results.get(this.name).seconds += s;
            this.results.get(this.name).mseconds += ms;
            
            var addSec = (this.results.get(this.name).mseconds - (this.results.get(this.name).mseconds % 1000))/1000;
            if(addSec != 0) {
                this.results.get(this.name).mseconds = this.results.get(this.name).seconds % 1000;
                this.results.get(this.name).seconds += addSec;                
            }
        }        
    },
    
    CheckLogCreated : function() {
        if(!$('divLog')) {
            var dl = $(document.createElement('div'));
            dl.id = 'divLog';            
            document.body.appendChild(dl);            
        }

        if(!this.divLogInitialised) {
            $('divLog').setStyle('position: absolute; top: 0px; left: 0px; width: 400px; z-index: 100; background-color: Green; color: White; font-size: 12px;');
            Event.observe(window, 'scroll', function() { 
                $('divLog').setStyle({top : ((document.documentElement && document.documentElement.scrollTop) ? document.documentElement.scrollTop : document.body.scrollTop) +'px'});
            });
            this.divLogInitialised = true;
        }
    },   
     
    Display : function(clear) {
        this.CheckLogCreated();        
        
        var totalsecs = 0;
        var totalmsecs = 0;
        this.results.each(function(pair) {
            $('divLog').update($('divLog').innerHTML + 
                                pair.key + ': ' + pair.value.seconds + ' sec, ' + pair.value.mseconds +' msec<br/>');
            totalsecs += pair.value.seconds;
            totalmsecs += pair.value.mseconds;
        });
        
        var addSec = (totalmsecs - (totalmsecs % 1000))/1000;
        if(addSec != 0) {
            totalmsecs = totalmsecs % 1000;
            totalsecs += addSec;                
        }
        $('divLog').update((clear != true ? $('divLog').innerHTML : '') + 'Total sec: ' + totalsecs + ' total msec: ' + totalmsecs);
    }    
};

LinkMeUI.Editor.BaseRegistrator = Class.create();
LinkMeUI.Editor.BaseRegistrator.prototype = {
    //AS: This function is here to keep the execution of the main script (page scope)
    //when an exception is being thrown
    initialize : function() {},
    
    ThrowError : function(message) {
        throw message;
    },
    
    CheckRequiredElementConditions : function(elements, cssClassName) {
        if(!elements || elements.length == 0) {
            this.ThrowError('Element with class name ' + cssClassName + ' has not been found inside control with ID ' + this.Control.id);
        }        
        if(elements.length != 1) {
            this.ThrowError('More than one element with class ' + cssClassName + ' has been found inside control with ID ' + this.Control.id);
        } else {        
            return elements[0];
        }
        return null;
    }    
};

LinkMeUI.Editor.Stats.Reset();

LinkMeUI.Editor.RegisterEditableControl = function(parameters) {

    if (!this.EditableSection) {
        this.EditableSection = Class.create();
        this.EditableSection.prototype = Object.extend(new LinkMeUI.Editor.BaseRegistrator(), {

            CreateVerticalDiv: function(id) {
                var div = $(document.createElement('div'));
                div.setStyle('opacity: 0.7');
                div.setStyle({ backgroundColor: 'white',
                    position: 'relative',
                    cssFloat: 'left',
                    position: 'absolute',
                    left: '0px',
                    top: '0px',
                    display: 'none'
                });
                div.id = id;
                document.body.appendChild(div);
                return div;
            },

            GetDivCoverTop: function() {
                return $(this.ELEMENT_DIV_COVER_TOP) || this.CreateVerticalDiv(this.ELEMENT_DIV_COVER_TOP);
            },

            GetDivCoverBottom: function() {
                return $(this.ELEMENT_DIV_COVER_BOTTOM) || this.CreateVerticalDiv(this.ELEMENT_DIV_COVER_BOTTOM);
            },

            GetDivCoverLeft: function() {
                return $(this.ELEMENT_DIV_COVER_LEFT) || this.CreateVerticalDiv(this.ELEMENT_DIV_COVER_LEFT);
            },

            GetDivCoverRight: function() {
                return $(this.ELEMENT_DIV_COVER_RIGHT) || this.CreateVerticalDiv(this.ELEMENT_DIV_COVER_RIGHT);
            },

            SaveCurrentScrollPos: function() {
                this.PostionBeforeEditing = parseInt((document.documentElement && document.documentElement.scrollTop) ? document.documentElement.scrollTop : document.body.scrollTop);
            },

            RestoreCurrentScrollPos: function() {
                if (document.documentElement && document.documentElement.scrollTop)
                    document.documentElement.scrollTop = this.PostionBeforeEditing;
                else if (document.body && document.body.scrollTop)
                    document.body.scrollTop = this.PostionBeforeEditing;
            },

            EnableAllEditorControls: function() {
                this.GetDivCoverTop().hide();
                this.GetDivCoverLeft().hide();
                this.GetDivCoverBottom().hide();
                this.GetDivCoverRight().hide();
                //TODO: If we are still using highliting - enable that
                //highlightHelper.Activate();
            },

            GetPixs: function(number) {
                return number.toString() + 'px';
            },

            DisableOtherEditorControls: function(disableAll) {
                if (disableAll) {
                    //Disable everything. Need to do that when deleting things.
                    var divCoverTop = this.GetDivCoverTop();

                    divCoverTop.setStyle({
                        height: this.GetPixs(document.body.scrollHeight),
                        width: this.GetPixs(document.body.scrollWidth)
                    });
                    divCoverTop.show();
                } else {
                    var pos = LinkMeUI.LocateHelper.GetObjectPos(this.Control);
                    var divCoverTop = this.GetDivCoverTop();

                    divCoverTop.setStyle({
                        height: this.GetPixs(pos[1]),
                        width: this.GetPixs(document.body.scrollWidth)
                    });

                    var divCoverBottom = this.GetDivCoverBottom();

                    divCoverBottom.setStyle({
                        width: this.GetPixs(document.body.scrollWidth),
                        top: this.GetPixs(pos[1] + parseInt(this.Control.offsetHeight)),
                        height: this.GetPixs(parseInt(document.body.scrollHeight) - parseInt(divCoverBottom.style.top))
                    });

                    var divCoverLeft = this.GetDivCoverLeft();
                    divCoverLeft.setStyle({
                        top: this.GetPixs(pos[1]),
                        width: this.GetPixs(pos[0]),
                        height: this.GetPixs(parseInt(this.Control.offsetHeight))
                    });

                    var divCoverRight = this.GetDivCoverRight();
                    divCoverRight.setStyle({
                        top: this.GetPixs(pos[1].toString()),
                        left: this.GetPixs((pos[0] + parseInt(this.Control.offsetWidth)).toString()),
                        width: this.GetPixs((parseInt(document.body.scrollWidth) - (pos[0] + parseInt(this.Control.offsetWidth))).toString()),
                        height: this.GetPixs((parseInt(this.Control.offsetHeight)).toString())
                    });

                    divCoverTop.show();
                    divCoverBottom.show();
                    divCoverRight.show();
                    divCoverLeft.show();
                }
            },

            HideErrorArea: function() {
                this.ErrorArea.hide();
            },

            HideLastVisible: function(HideLastVisible) {
                this.HideErrorArea();
                if (HideLastVisible) {
                    var showArea = this.EditArea;
                    var hideArea = this.LastVisible;
                } else {
                    var showArea = this.LastVisible;
                    var hideArea = this.EditArea;
                }
                showArea.show();
                hideArea.hide();
            },

            ShowEditor: function() {
                this.HideLastVisible(true);
                if (this.DisableOtherControls) {
                    this.DisableOtherEditorControls();
                }
            },

            ShowError: function(errorMsg) {
                if (this.ErrorArea) {
                    this.ErrorArea.show();
                    this.ErrorArea.update(errorMsg);

                    if (!this.NoMessageHighlighting) {
                        new Effect.Highlight(this.ErrorArea, { duration: 1.5, startcolor: this.COLOR_ERROR_START, endcolor: this.COLOR_ERROR_END });
                    }

                    //Re-drawing gray overlays because size of the control might have changed 
                    if (this.DisableOtherControls) {
                        this.DisableOtherEditorControls();
                    }
                } else {
                    alert(errorMsg);
                }
            },

            ShowInfo: function(infoMsg) {
                if (this.InfoArea) {
                    this.InfoArea.show();
                    this.InfoArea.update(infoMsg);
                    /*new Effect.Highlight(this.InfoArea, { duration: 2, 
                    startcolor:this.COLOR_INFO_START, 
                    endcolor:this.COLOR_INFO_END,
                    queue: {position: 'end', scope: 'info'} });*/
                    new Effect.Fade(this.InfoArea, { duration: 3, delay: 2,
                        queue: { position: 'end', scope: 'info'}
                    });
                }
            },

            SetEditLinkVisibility: function(visible) {
                LinkMeUI.LocateHelper.GetElementsByClassName(this.Control, this.CSS_NAME_EDIT_LINK).each(function(elem) {
                    elem.setStyle({
                        visibility: visible ? 'visible' : 'hidden'
                    });
                });
            },

            FocusFirstControl: function() {
                var inputs = this.Control.getElementsByTagName('input');
                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].type && inputs[i].type == 'text') {
                        try {
                            inputs[i].focus();
                        } catch (e) {
                            //do nothing, cause sometimes IE stuffs it up when focusing invisible control
                        }
                        if (this.FocusFirstControlSelectAll)
                            $(inputs[i]).select();
                        return;
                    }
                }

                var textAreas = this.Control.getElementsByTagName('textarea');
                if (textAreas && textAreas.length != 0) {
                    textAreas[0].focus();
                    if (this.FocusFirstControlSelectAll)
                        $(textAreas[0]).select();
                }
            },

            GetProgressDiv: function() {
                var img = null;
                if (!this.ProgressDiv) {
                    //Create the whole thing
                    var targetClassName = 'transparent50';
                    this.ProgressDiv = $(document.createElement('div'));
                    this.ProgressDiv.setStyle('opacity: 0.5');
                    this.ProgressDiv.setStyle('background-color : white');
                    this.ProgressDiv.setStyle('position : relative');
                    this.ProgressDiv.setStyle('float : left');

                    //TODO: This create image bit might be refactored into a function
                    //if LinkMeUI.Ajax.LOADING_IMAGE_PATH is used all over the place later on.
                    img = $(document.createElement('img'));
                    img.src = LinkMeUI.Ajax.LOADING_IMAGE_PATH;

                    this.ProgressDiv.appendChild(img);
                    this.Control.appendChild(this.ProgressDiv);
                } else {
                    img = this.ProgressDiv.getElementsByTagName('img')[0];
                }

                //Just recalculate the size of it and image position
                var height = parseInt(this.Control.offsetHeight);
                this.ProgressDiv.setStyle({
                    marginTop: this.GetPixs(-height),
                    height: this.GetPixs(height),
                    width: this.GetPixs(parseInt(this.Control.offsetWidth))
                });

                img.setStyle({
                    position: 'relative',
                    marginLeft: this.GetPixs((parseInt(this.ProgressDiv.style.width) / 2) - img.width / 2),
                    marginTop: this.GetPixs((parseInt(this.ProgressDiv.style.height) / 2) - img.height / 2)
                });
                return this.ProgressDiv;
            },

            ShowProgress: function() {
                this.GetProgressDiv().show();
            },

            HideProgress: function() {
                this.GetProgressDiv().hide();
            },

            ShowNoContent: function() {
                if (this.RecordsetAreaNoContent) {
                    this.RecordsetAreaNoContent.show();
                } else {
                    //This is a simple control, show it's own 'no content' section
                    this.HideErrorArea();
                    this.LastVisible = this.NoContentsArea;
                    this.HideLastVisible(false);
                }
            },

            ShowDisplay: function() {
                if (this.RecordsetAreaNoContent && this.RecordsetAreaNoContent.visible()) {
                    this.RecordsetAreaNoContent.hide();
                }
                this.HideErrorArea();
                this.LastVisible = this.DisplayArea;
                this.HideLastVisible(false);
            },

            EvaluateFunction: function(functionName, required, functionDescription) {
                if (!functionName) {
                    if (required == true) {
                        this.ThrowError('No "' + functionDescription + '" user function has been supplied for the control with ID '
                                + this.Control.ID);
                    }
                    return null;
                }

                if (typeof functionName == 'string')
                    f = eval(functionName);
                else if (typeof functionName == 'function')
                    f = functionName;

                if (!f && required) {
                    this.ThrowError('Cannot find user function with name ' + functionName);
                }
                return f;
            },

            //TODO: Ask EP, what is this for?
            // EP: It's to enable hiding a whole section of the page when there is no data to show, eg. hiding the whole Industries
            // section if a member has no Industries.

            ShowHideIfEmpty: function(element, isEmpty, levels) {
                if (levels == 0) {
                    return;
                }

                var parent = element.parentNode;
                if (!parent) {
                    return;
                }
                if (parent.hasClassName && parent.hasClassName(this.CSS_NAME_HIDE_IF_PROPERTY_EMPTY)) {
                    parent.style.display = isEmpty ? 'none' : 'block';
                } else {
                    this.ShowHideIfEmpty(parent, isEmpty, levels - 1);
                }
            },

            SetLabelFromEditControl: function(label, edit) {
                var isEmpty = false;

                if (edit.type && edit.type == 'checkbox') {
                    label.checked = edit.checked;

                    // If the label contains a checkbox then we copy the value of the edit checkbox to it.

                    var displayCheckbox = this.GetChildCheckbox(label);
                    if (displayCheckbox == null) {
                        this.ThrowError('Label ' + labels[i].id + ' for checkbox control '
                        + labels[i].htmlFor + ' does not have a checkbox inside it.');
                    }

                    displayCheckbox.checked = edit.checked;

                    // If there's also a <span> after the checkbox then show it only if the checkbox is checked. This is how
                    // EmploymentHistoryRecord works.

                    var sibs = displayCheckbox.nextSiblings();
                    if (sibs.length > 0 && sibs[0].tagName.toLowerCase() == 'span') {
                        if (edit.checked) {
                            sibs[0].show();
                        } else {
                            sibs[0].hide();
                        }
                    }
                } else if (edit.type && (edit.type == 'select-multiple' || edit.type == 'select-one')) {
                    // Listbox    
                    label.innerHTML = '';

                    for (var i = 0; i < edit.options.length; i++) {
                        if (edit.options[i].selected) {

                            // Only set the text if the value is set.
                            if (edit.options[i].value != null && edit.options[i].value != '') {
                                var itemText = edit.options[i].text;
                                if (label.innerHTML.length > 0) {
                                    itemText = this.LIST_ITEM_DISPLAY_SEPARATOR + itemText;
                                }
                                label.innerHTML += LinkMeUI.StringUtils.TextToHtml(itemText);
                            }
                        }
                    }
                    isEmpty = (label.innerHTML.length == 0);
                } else if (edit.type && edit.type == 'radio') {
                    $$('input[type="radio"][name="' + edit['name'] + '"]').each(
                    function(radioElem) {
                        if (radioElem.checked) {
                            label.update(radioElem.value);
                            throw $break;
                        }
                    });
                } else {
                    label.update(LinkMeUI.StringUtils.TextToHtml($F(edit)));
                    isEmpty = (label.innerHTML.length == 0);
                }

                // Check up to 3 levels upwards whether we need to hide the parent element.
                this.ShowHideIfEmpty(label, isEmpty, 3);
            },

            UpdateElementsFromSaveData: function(userData) {
                if (userData.ElementNames == null || userData.ElementNames.length == 0) {
                    return;
                }

                for (var i = 0; i < userData.ElementNames.length; i++) {
                    var element = $(userData.ElementNames[i]);
                    if (!element) {
                        console.log('Failed to find an element named "' + userData.ElementNames[i] + '" returned in callback UserData.');
                    } else if (userData.ElementValues[i] != null) {
                        element.update(userData.ElementValues[i]);
                        this.ShowHideIfEmpty(element, (element.innerHTML.length == 0), 3);
                    }
                }
            },

            CallUserPostUpdateFunction: function() {
                if (this.UserPostUpdateFunction)
                    this.UserPostUpdateFunction();
            },

            CallUserPostAddFunction: function() {
                if (this.UserPostAddFunction)
                    this.UserPostAddFunction();

            },

            CallUserPostDeleteFunction: function() {
                if (this.UserPostDeleteFunction)
                    this.UserPostDeleteFunction();
            },

            CallUserPostCancelUpdateFunction: function() {
                if (this.UserPostCancelUpdateFunction)
                    this.UserPostCancelUpdateFunction();
            },

            CallUserPostCancelAddFunction: function() {
                if (this.UserPostCancelAddFunction)
                    this.UserPostCancelAddFunction();
            },

            ResortSectionsFromSaveData: function(userData) {
                if (userData.IDs == null || userData.IDs.length == 0)
                    return;

                var sortableSections = LinkMeUI.LocateHelper.GetElementsByClassName(this.RecordsetContainer, this.CSS_NAME_CONTAINER);
                var sectHash = $H();

                userData.IDs.each(function(lensID) {
                    sortableSections.each(function(sect) {
                        var labels = LinkMeUI.LocateHelper.GetElementsByClassName(sect, this.CSS_NAME_RECORD_ID);
                        if (labels[0].innerHTML == lensID) {
                            sectHash.set(lensID, sect);
                            throw $break;
                        }
                    }, this);
                }, this);

                sectHash.each(function(pair) {
                    pair.value.parentNode.removeChild(pair.value);
                    this.RecordsetContainer.appendChild(pair.value);
                }, this);
            },

            GetChildCheckbox: function(parentElement) {
                var childElements = parentElement.getElementsByTagName('input');

                if (childElements.length > 0) {
                    if (childElements[0].type && childElements[0].type == 'checkbox')
                        return childElements[0];
                }

                return null;
            },

            IgnoreClick: function(event) {
                if (event) {
                    Event.stop(event);
                }
            },

            ToggleSubmit: function(halt) {
                $A(document.forms).each(function(form) {
                    if (halt) {
                        var f = function(e) {
                            Event.stop(e);
                            this.Update();
                        };

                        form.LinkMeUIHandler = f.bindAsEventListener(this);
                        Event.observe($(form), 'submit', form.LinkMeUIHandler);
                        return;
                    }

                    Event.stopObserving($(form), 'submit', form.LinkMeUIHandler);
                }, this);
            },

            UpdateStriping: function() {
                this.RecordsetContainer.select("." + this.CSS_NAME_CONTAINER).each((function(el, n) {
                    if (n % 2)
                        el.addClassName("odd_" + this.CSS_NAME_CONTAINER)
                    else
                        el.removeClassName("odd_" + this.CSS_NAME_CONTAINER);
                }).bind(this));
            },

            Edit: function(event) {
                if (event && typeof event == 'object') {
                    Event.stop(event);
                }

                if (this.UserPreEditFunction) {
                    this.UserPreEditFunction();
                }
                //document.body.addEventListener('keypress', this.ClickUpdate, false);
                this.ToggleSubmit(true);

                //LinkMeUI.Editor.Stats.Reset();
                //LinkMeUI.Editor.Stats.Begin('[for]')
                var labels = this.DisplayArea.select('[for]');  /* It's the "for" attribute we're really after; we'll call */
                //LinkMeUI.Editor.Stats.End();                  /* them 'labels' for brevity, but they could be anything */

                //LinkMeUI.Editor.Stats.Begin('SetEditControls');
                for (var i = 0; i < labels.length; i++) {
                    var lblID = LinkMeUI.LocateHelper.GetForAttribute(labels[i]);
                    if (lblID == null || lblID.length == 0) {
                        continue;
                    }

                    var edit = $(lblID);
                    if (!edit) {
                        this.ThrowError(this.MSG_CANNOT_LOCATE_CONTROL + lblID);
                    }

                    if (edit.type && edit.type == 'checkbox') {
                        // Checkbox

                        var displayCheckbox = this.GetChildCheckbox(labels[i]);
                        if (displayCheckbox == null) {
                            this.ThrowError('Label ' + labels[i].id + ' for checkbox control '
                            + labels[i].htmlFor + ' does not have a checkbox inside it.');
                        }

                        edit.checked = displayCheckbox.checked;
                    } else if (edit.type &&
                          (edit.type == 'select-multiple' || edit.type == 'select-one')) {
                        // Listbox - select all items names (not values) listed in the text. Assumes the label values
                        // are in the same order as the listbox values.

                        var selectedNames = LinkMeUI.StringUtils.HtmlToText(labels[i].innerHTML).split(this.LIST_ITEM_DISPLAY_SEPARATOR);
                        var listOptions = edit.options;
                        var iOptions = 0;

                        for (var iSelNames = 0; iSelNames < selectedNames.length; iSelNames++) {
                            while (iOptions < listOptions.length) {
                                var match = (listOptions[iOptions].text == selectedNames[iSelNames]);
                                listOptions[iOptions].selected = match;

                                iOptions++;
                                if (match)
                                    break;
                            }
                        }

                        while (iOptions < listOptions.length) {
                            listOptions[iOptions.selected] = false;
                            iOptions++;
                        }
                    } else if (edit.type && edit.type == 'radio') {
                        var actVal = LinkMeUI.StringUtils.HtmlToText(labels[i].innerHTML);
                        $$('input[type="radio"][name="' + edit['name'] + '"]').each(
                        function(radioElem) {
                            if (radioElem.value == actVal) {
                                radioElem.checked = true;
                                throw $break;
                            }
                        }
                    );
                    } else {
                        edit.value = LinkMeUI.StringUtils.HtmlToText(labels[i].innerHTML);

                        // Handle "empty" appearance for 'no-content' enabled text boxes
                        if (edit.attributes["nocontent"]) {
                            edit.isEmptyFunction.bind(edit).defer(edit);
                        }

                        // Textarea automatic resizing
                        if (edit.type && edit.type == 'textarea') {
                            // Invoke resizing once
                            edit.resizeFunction.bind(edit).defer(edit);   // MF (2009-02-12) TODO: remove flickering here
                        }
                    }
                }
                //LinkMeUI.Editor.Stats.End();

                //Hide all 'edit' links
                //LinkMeUI.Editor.Stats.Begin('LinkVis');
                this.SetEditLinkVisibility(false);
                //LinkMeUI.Editor.Stats.End();
                //LinkMeUI.Editor.Stats.Begin('ShowEditor');
                this.ShowEditor();
                //LinkMeUI.Editor.Stats.End();
                //LinkMeUI.Editor.Stats.Begin('History');
                if (this.DisableOtherControls) {
                    SaveHistoryStep();
                }
                //LinkMeUI.Editor.Stats.End();
                //LinkMeUI.Editor.Stats.Begin('FocusFirst');
                this.FocusFirstControl();
                //LinkMeUI.Editor.Stats.End();

                //TODO 3.1: We need this to trace history. Possibly remove when history library used 
                LinkMeUI.Editor.CurrentControl = this;
                if (this.DisableOtherControls) {
                    this.SaveCurrentScrollPos();
                }
                //LinkMeUI.Editor.Stats.End();
                //LinkMeUI.Editor.Stats.Display();            
            },

            Cancel: function() {
                //LinkMeUI.Editor.Stats.Reset();
                this.ToggleSubmit(false);
                //LinkMeUI.Editor.Stats.Begin('CancelLabels');
                var labels = this.DisplayArea.select('[for]');
                labels.each(function(lbl) {
                    var lblID = LinkMeUI.LocateHelper.GetForAttribute(lbl);
                    if (lblID != null && lblID.length != 0) {
                        Validation.reset($(lblID));
                    }
                });
                //LinkMeUI.Editor.Stats.End();

                //LinkMeUI.Editor.Stats.Begin('HideEnableShow');
                this.HideLastVisible(false);
                this.EnableAllEditorControls();
                this.SetEditLinkVisibility(true);
                //LinkMeUI.Editor.Stats.End();

                //LinkMeUI.Editor.Stats.Begin('RemoveChild');
                if (this.IsNewControl) {
                    this.Control.parentNode.removeChild(this.Control);
                    this.UpdateStriping();
                }
                //LinkMeUI.Editor.Stats.End();


                //LinkMeUI.Editor.Stats.Begin('NoContent');            
                //Are there any records? Checking for == 1 because first one is always a template;
                //If no records are there, then display relevant No Contents areas
                if (this.RecordsetContainer &&
                this.RecordsetContainer.select('.' + this.CSS_NAME_CONTAINER).length == 1) {
                    this.ShowNoContent();
                }
                //LinkMeUI.Editor.Stats.End();

                //LinkMeUI.Editor.Stats.Begin('History');
                //TODO 3.1: Possibly use history management library
                ClearHistory();
                //LinkMeUI.Editor.Stats.End();
                //LinkMeUI.Editor.Stats.Display();
                LinkMeUI.Editor.CurrentControl = null;
                if (this.DisableOtherControls) {
                    this.RestoreCurrentScrollPos();
                }

                //LinkMeUI.Editor.Stats.Begin('UserCancel');
                if (this.IsNewControl)
                    this.CallUserPostCancelAddFunction();
                else
                    this.CallUserPostCancelUpdateFunction();

                //LinkMeUI.Editor.Stats.End();
            },

            OnTimeout: function() {
                this.ShowError('Server timeout, please try again later.');
                this.HideProgress();
                return true;
            },

            Update: function() {
                var labels = this.DisplayArea.select('[for]');
                var passed = true;
                //Run Validation library checks - they are based on the class names assigned to the controls
                labels.each(function(lbl) {
                    var editID = LinkMeUI.LocateHelper.GetForAttribute(lbl);
                    if (editID != null && editID.length != 0) {
                        var edit = $(editID);
                        if (!Validation.validate(edit)) {
                            passed = false;
                        }
                    }
                });
                //Clear all server-produced validation messages in edit area
                this.ErrorArea.update('');
                //Call custom validations, specified by
                if (this.UserValidateFunction) {
                    passed = this.UserValidateFunction();
                }
                if (!passed) {
                    //Redraw disabling divs, because validation messages may change the size of
                    //editable area
                    if (this.DisableOtherControls) {
                        this.DisableOtherEditorControls();
                    }
                    return;
                }

                this.ShowProgress();

                var uC = this.UpdateCallback.bindAsEventListener(this);

                if (typeof AjaxPro != "undefined")
                    AjaxPro.onTimeout = this.OnTimeout.bindAsEventListener(this);

                this.UserUpdateFunction(uC, this);
                this.ToggleSubmit(false);
            },

            GetErrorObject: function(msg) {
                var r = new Object();
                r.value = new Object();
                r.value.ResultCode = LinkMeUI.Ajax.FAILURE;
                r.value.Message = msg;
                return r;
            },

            UpdateCallback: function(result) {
                var isSuccess = false;
                if (result == null || result.value == null) {
                    this.ShowError(this.MSG_NO_SERVER_RESPONSE);
                } else {
                    if (result.value.ResultCode & LinkMeUI.Ajax.SUCCESS_CODE) {
                        isSuccess = true;
                        // Set label values, even if the text is empty (for the next edit).
                        var labels = this.DisplayArea.select('[for]');
                        labels.each(function(label) {
                            var editID = LinkMeUI.LocateHelper.GetForAttribute(label);
                            if (editID != null && editID.length != 0) {
                                this.SetLabelFromEditControl(label, $(editID));
                            }
                        }, this);

                        if (result.value.ResultCode & LinkMeUI.Ajax.EMPTY_CODE) {
                            this.ShowNoContent();
                        } else {
                            this.ShowDisplay();
                            if (result.value.UserData != null) {
                                this.UpdateElementsFromSaveData(result.value.UserData);
                                this.ResortSectionsFromSaveData(result.value.UserData);
                                if (this.RecordsetContainer) {
                                    this.UpdateStriping();
                                }
                            }
                            //Set ID for the new section and resort them
                            if (this.IsNewControl) {
                                this.RecordIDContainer.innerHTML = result.value.Message;
                                this.CurrentSectionControls.push(this.ControlsMap);
                            }
                        }
                        this.EnableAllEditorControls();
                        this.SetEditLinkVisibility(true);
                    } else {
                        if (result.value.Message == null || result.value.Message.length == 0) {
                            this.ShowError('Unexpected error occured. Please try again later.');
                        } else {
                            this.ShowError(result.value.Message);
                        }
                    }
                }

                if (isSuccess) {
                    if (this.IsNewControl)
                        this.CallUserPostAddFunction();
                    else
                        this.CallUserPostUpdateFunction();

                    this.IsNewControl = false;
                    ClearHistory();
                }

                this.HideProgress();
                if (isSuccess) {
                    this.ShowInfo('Saved OK');
                }
                LinkMeUI.Editor.CurrentControl = null;

                if (this.DisableOtherControls) {
                    this.RestoreCurrentScrollPos();
                }
            },

            Delete: function(event) {
                if (event) {
                    Event.stop(event);
                }

                if (this.DisableOtherControls) {
                    this.DisableOtherEditorControls(true);
                }
                if (confirm('Are you sure you want to delete this item?')) {
                    this.ShowProgress();
                    this.UserDeleteFunction(this.RecordIDContainer.innerHTML, this.DeleteCallBack.bindAsEventListener(this), this);
                } else {
                    if (this.DisableOtherControls) {
                        this.EnableAllEditorControls();
                    }
                }
            },

            DeleteCallBack: function(result) {
                if (result == null || result.value == null) {
                    this.ShowError(this.MSG_NO_SERVER_RESPONSE);

                } else if (result.value.ResultCode & LinkMeUI.Ajax.SUCCESS_CODE) {
                    if (result.value.ResultCode & LinkMeUI.Ajax.EMPTY_CODE) {
                        this.ShowNoContent();
                    } else {
                        this.CurrentSectionControls.each(function(item) {
                            var doBreak = false;
                            item.each(function(pair) {
                                if (pair.value == this.RecordIDContainer.id) {
                                    this.CurrentSectionControls = this.CurrentSectionControls.without(item);
                                    doBreak = true;
                                    throw $break;
                                }
                            }, this);
                            if (doBreak) {
                                throw $break;
                            }
                        }, this);
                    }

                    this.Control.parentNode.removeChild(this.Control);
                    this.UpdateStriping();

                    if (result.value.UserData != null) {
                        this.UpdateElementsFromSaveData(result.value.UserData);
                        this.ResortSectionsFromSaveData(result.value.UserData);
                    }
                } else {
                    this.ShowError(result.value.Message);
                }
                //TODO: need to rework global errors displaying
                //DisplayErrors();    
                this.HideProgress();
                this.EnableAllEditorControls();
                this.CallUserPostDeleteFunction();
            },

            OnResize: function(event) {
                if (LinkMeUI.Editor.CurrentControl != this || this.EditArea.visible() != true)
                    return;

                var newHeight = this.EditArea.getHeight();
                if (this.DisableOtherControls &&
               (event.type == 'resize' || (event.type == 'change' && newHeight != this.EditAreaHeight))) {
                    this.DisableOtherEditorControls();
                }

                this.EditAreaHeight = newHeight;
            },

            GetFirstArrayElem: function(arr) {
                if (arr != null && arr.length == 1) {
                    return arr[0];
                }

                return null;
            },

            ScrollToUpdateButton: function() {
                if (this.UpdateButton) this.UpdateButton.scrollTo();
            },

            initialize: function(parameters) {

                //CONSTANTS
                //LinkMeUI.Editor.Stats.Begin('SetContstants');
                this.LIST_ITEM_DISPLAY_SEPARATOR = '\n';
                this.CSS_NAME_CUSTOM_VALIDATION_FUNCTION = 'custom_js_validation';
                this.CSS_NAME_HIDE_IF_PROPERTY_EMPTY = 'property-hide-if-empty';
                this.ELEMENT_DIV_COVER_TOP = 'divCoverTop';
                this.ELEMENT_DIV_COVER_BOTTOM = 'divCoverBottom';
                this.ELEMENT_DIV_COVER_RIGHT = 'divCoverRight';
                this.ELEMENT_DIV_COVER_LEFT = 'divCoverLeft';
                this.COLOR_ERROR_START = '#f55050';
                this.COLOR_ERROR_END = '#ffffff';

                this.COLOR_INFO_START = '#628C46';
                this.COLOR_INFO_END = '#ffffff';

                //Messages
                this.MSG_NO_SERVER_RESPONSE = 'No answer received from the server!';
                this.MSG_CANNOT_LOCATE_CONTROL = 'Cannot locate control with ID ';

                //Class vars
                if (!parameters) {
                    this.ThrowError('No hash array with parameters specified for RegisterEditableControl')
                }

                this.Control = $(parameters['control']);
                this.CSS_NAME_CONTAINER = parameters['classNameContainingControl'];
                this.CSS_NAME_EDITABLE = parameters['classNameEditable'];
                this.CSS_NAME_DISPLAY = parameters['classNameDisplay'];
                this.CSS_NAME_NOCONTENTS = parameters['classNameNoContents'];
                this.CSS_NAME_ERRORS = parameters['classNameError'];
                this.CSS_NAME_INFO = parameters['classNameInfo'];
                this.CSS_NAME_EDIT_LINK = parameters['classNameEditLink'];
                this.CSS_NAME_CANCEL_LINK = parameters['classNameCancelLink'];
                this.CSS_NAME_UPDATE_LINK = parameters['classNameUpdateLink'];
                this.CSS_NAME_IGNORE_CLICKS = parameters['classNameIgnoreClicks'];
                this.CSS_NAME_DELETE_LINK = parameters['classNameDeleteRecordLink'];
                this.CSS_NAME_BUTTON_UPDATE = parameters['classNameButtonUpdate'];
                this.CSS_NAME_BUTTON_CANCEL = parameters['classNameButtonCancel'];
                this.CSS_NAME_RECORDS_COLLECTION = parameters['classNameRecordsCollection'];
                this.CSS_NAME_RECORDS_NO_CONTENT = parameters['classNameRecordsNoContent'];
                this.CSS_NAME_RECORD_ID = parameters['classNameRecordId'];
                this.DisableOtherControls = parameters['disableOtherControls'];
                this.IsNewControl = parameters['isNewControl'];
                this.ControlsMap = parameters['controlsMap'];
                this.CurrentSectionControls = parameters['currentSectionControls'];
                // If false clicking on the display or no content area starts an edit. If true only explicit edit links start an edit.
                this.NoImplicitEditClicks = parameters['noImplicitEditClicks'];
                this.NoMessageHighlighting = parameters['noMessageHighlighting'];
                this.FocusFirstControlSelectAll = parameters['focusFirstControlSelectAll'];

                if (!this.Control) {
                    this.ThrowError('Cannot locate control with ID ' + parameters['control']);
                }
                //LinkMeUI.Editor.Stats.End();

                //LinkMeUI.Editor.Stats.Begin('ControlID');
                if (this.CSS_NAME_RECORD_ID) {
                    this.RecordIDContainer = this.CheckRequiredElementConditions(LinkMeUI.LocateHelper.GetElementsByClassName(this.Control,
                                                                this.CSS_NAME_RECORD_ID),
                                                             this.CSS_NAME_RECORD_ID);

                    //If this.ControlsMap is null but there is record id label,
                    //we should be able to locate our controls map                
                    if (!this.ControlsMap) {
                        this.CurrentSectionControls.each(function(item) {
                            var doBreak = false;
                            item.each(function(pair) {
                                if (pair.value == this.RecordIDContainer.id) {
                                    this.ControlsMap = item;
                                    doBreak = true;
                                    throw $break;
                                }
                            }, this);
                            if (doBreak) {
                                throw $break;
                            }
                        }, this);
                    }
                }
                //LinkMeUI.Editor.Stats.End();

                //LinkMeUI.Editor.Stats.Begin('FindAreas_mine');
                this.EditArea = this.CheckRequiredElementConditions(LinkMeUI.LocateHelper.GetElementsByClassName(this.Control,
                                                                this.CSS_NAME_EDITABLE),
                                                             this.CSS_NAME_EDITABLE);
                this.EditAreaHeight = this.EditArea.getHeight();

                //this 'onchange' works in FF only, 'resize' works in IE only
                //that's why we need both!
                this.EditArea.observe('change', this.OnResize.bindAsEventListener(this));
                this.EditArea.observe('resize', this.OnResize.bindAsEventListener(this));

                Event.observe(window, 'resize', this.OnResize.bindAsEventListener(this));

                this.DisplayArea = this.CheckRequiredElementConditions(LinkMeUI.LocateHelper.GetElementsByClassName(this.Control,
                                                                    this.CSS_NAME_DISPLAY),
                                                                this.CSS_NAME_DISPLAY);

                this.ErrorArea = this.CheckRequiredElementConditions(LinkMeUI.LocateHelper.GetElementsByClassName(this.Control,
                                                                    this.CSS_NAME_ERRORS),
                                                              this.CSS_NAME_ERRORS);

                this.InfoArea = this.GetFirstArrayElem(LinkMeUI.LocateHelper.GetElementsByClassName(this.Control, this.CSS_NAME_INFO));
                this.NoContentsArea = this.GetFirstArrayElem(LinkMeUI.LocateHelper.GetElementsByClassName(this.Control, this.CSS_NAME_NOCONTENTS));
                //LinkMeUI.Editor.Stats.End();


                //Is this control a part of the recordset?
                //If so, keep the recordset reference.
                if (this.CSS_NAME_RECORDS_COLLECTION) {
                    //LinkMeUI.Editor.Stats.Begin('FindAreasRecordset1');  
                    this.RecordsetContainer = LinkMeUI.LocateHelper.GetParentControlDivByClassName(this.Control,
                                                                                    this.CSS_NAME_RECORDS_COLLECTION);
                    //LinkMeUI.Editor.Stats.End();                
                    if (this.RecordsetContainer) {
                        //LinkMeUI.Editor.Stats.Begin('FindAreasRecordset2');
                        var noContArr = LinkMeUI.LocateHelper.GetElementsByClassName(this.RecordsetContainer, this.CSS_NAME_RECORDS_NO_CONTENT);

                        //LinkMeUI.Editor.Stats.End();
                        //LinkMeUI.Editor.Stats.Begin('FindAreasRecordset3');                    
                        if (noContArr.length == 1) {
                            this.RecordsetAreaNoContent = $(noContArr[0]);
                        } else if (noContArr.length > 1) {
                            this.ThrowError('More than one element with class name ' + this.CSS_NAME_RECORDS_NO_CONTENT +
                        ' has been found in recordset container for control with ID' + this.Control.id);
                        }
                        //LinkMeUI.Editor.Stats.End();
                    }

                }
                ////LinkMeUI.Editor.Stats.End();

                //LinkMeUI.Editor.Stats.Begin('Buttons');
                if (this.CSS_NAME_BUTTON_UPDATE)
                    this.UpdateButton = this.CheckRequiredElementConditions(
                                            LinkMeUI.LocateHelper.GetElementsByClassName(this.Control,
                                                                           this.CSS_NAME_BUTTON_UPDATE),
                                            this.CSS_NAME_BUTTON_UPDATE);

                if (this.CSS_NAME_BUTTON_CANCEL)
                    this.CancelButton = this.CheckRequiredElementConditions(
                                            LinkMeUI.LocateHelper.GetElementsByClassName(this.Control,
                                                                           this.CSS_NAME_BUTTON_CANCEL),
                                            this.CSS_NAME_BUTTON_CANCEL);
                //LinkMeUI.Editor.Stats.End();                                        

                //Evaluate function references
                //LinkMeUI.Editor.Stats.Begin('Functions');
                this.UserUpdateFunction = this.EvaluateFunction(parameters['userUpdateFunction'], true, 'update');
                this.UserDeleteFunction = this.EvaluateFunction(parameters['userDeleteFunction'], false);
                this.UserPostUpdateFunction = this.EvaluateFunction(parameters['userPostUpdateFunction'], false);
                this.UserPostAddFunction = this.EvaluateFunction(parameters['userPostAddFunction'], false);
                this.UserPostDeleteFunction = this.EvaluateFunction(parameters['userPostDeleteFunction'], false);
                this.UserPreEditFunction = this.EvaluateFunction(parameters['userPreEditFunction'], false);
                this.UserPostCancelUpdateFunction = this.EvaluateFunction(parameters['userPostCancelUpdateFunction'], false);
                this.UserPostCancelAddFunction = this.EvaluateFunction(parameters['userPostCancelAddFunction'], false);
                this.UserValidateFunction = this.EvaluateFunction(parameters['userValidateFunction'], false);
                //LinkMeUI.Editor.Stats.End();

                //LinkMeUI.Editor.Stats.Begin('OnClickHandlers');
                //Assign ignore actions, if any
                if (this.CSS_NAME_IGNORE_CLICKS != null) {
                    $A(this.Control.select('.' + this.CSS_NAME_IGNORE_CLICKS)).each(
                    function(elem) {
                        $(elem).observe('click', this.IgnoreClick);
                    }, this);
                }

                //Assign onclick event handlers for sections
                if (!this.NoImplicitEditClicks) {
                    this.DisplayArea.observe('click', this.Edit.bindAsEventListener(this));
                    if (this.NoContentsArea) {
                        this.NoContentsArea.observe('click', this.Edit.bindAsEventListener(this));
                    }
                }

                //Assign onclick event handlers for edit/cancel/update links.
                var editLinks = LinkMeUI.LocateHelper.GetElementsByClassName(this.Control, this.CSS_NAME_EDIT_LINK);
                if (editLinks) editLinks.invoke('observe', 'click', this.Edit.bindAsEventListener(this));
                var cancelLinks = LinkMeUI.LocateHelper.GetElementsByClassName(this.Control, this.CSS_NAME_CANCEL_LINK);
                if (cancelLinks) cancelLinks.invoke('observe', 'click', this.Cancel.bindAsEventListener(this));
                var updateLinks = LinkMeUI.LocateHelper.GetElementsByClassName(this.Control, this.CSS_NAME_UPDATE_LINK);
                if (updateLinks) updateLinks.invoke('observe', 'click', this.Update.bindAsEventListener(this));

                //Assign update/cancel function call to the buttons
                if (this.UpdateButton) this.UpdateButton.observe('click', this.Update.bindAsEventListener(this));
                if (this.CancelButton) this.CancelButton.observe('click', this.Cancel.bindAsEventListener(this));

                //Assign delete section event handers, if any
                if (this.CSS_NAME_DELETE_LINK && this.UserDeleteFunction) {
                    LinkMeUI.LocateHelper.GetElementsByClassName(this.Control, this.CSS_NAME_DELETE_LINK).invoke('observe', 'click', this.Delete.bindAsEventListener(this));
                }
                //LinkMeUI.Editor.Stats.End();

                //LinkMeUI.Editor.Stats.Begin('Visibility');
                if (this.DisplayArea.visible()) {
                    this.LastVisible = this.DisplayArea;
                } else if (this.NoContentsArea && this.NoContentsArea.visible()) {
                    this.LastVisible = this.NoContentsArea;
                }

                if (!this.Control.visible()) {
                    !this.Control.show();
                }
                //LinkMeUI.Editor.Stats.End();

                // Zebra-stripe related records
                if (this.CSS_NAME_RECORDS_COLLECTION) {
                    this.UpdateStriping();
                }

                var labels = this.DisplayArea.select('[for]');

                for (var i = 0; i < labels.length; i++) {
                    var lblID = LinkMeUI.LocateHelper.GetForAttribute(labels[i]);
                    if (lblID == null || lblID.length == 0) {
                        continue;
                    }

                    var edit = $(lblID);
                    if (!edit) {
                        this.ThrowError(this.MSG_CANNOT_LOCATE_CONTROL + lblID);
                    }

                    if (edit.type && edit.type == 'text' || edit.type == 'textarea') {
                        var noContent = edit.attributes["nocontent"];

                        // If there is a nocontent attribute, make it appear
                        if (noContent) {
                            strNoContentText = noContent.nodeValue;

                            elNoContent = new Element('div');
                            elNoContent.setStyle({ display: "none", cursor: "text" });
                            elNoContent.className = "empty-textbox-overlay textbox";
                            elNoContent.associatedTextbox = edit;
                            elNoContent.update(strNoContentText);
                            edit.insert({ "after": elNoContent });
                            edit.parentNode.style.position = "relative";    // Force overlay to anchor properly.

                            elNoContent.observe('click',
                            (function() {
                                this.associatedTextbox.focus();
                            }).bindAsEventListener(elNoContent)
                        );

                            edit.isEmptyFunction = function(event) {
                                (function() {
                                    if (!(this.value)) {
                                        this.noContentElement.show();
                                    } else {
                                        this.noContentElement.hide();
                                    }
                                }).bind(this, event).defer(event);
                            }

                            edit.noContentElement = elNoContent;
                            edit.observe('keypress', edit.isEmptyFunction.bindAsEventListener(edit));
                            edit.observe('keydown', edit.isEmptyFunction.bindAsEventListener(edit));
                        }
                    }

                    if (edit.type && edit.type == 'textarea') {
                        var elWidget = new Element('div');
                        var elSpace = new Element('span');
                        style = { position: "absolute", display: "block", top: "0px", left: "100px",
                            visibility: "hidden", border: "none", padding: "0"
                        }

                        elWidget.setStyle(style);
                        elWidget.className = "multiline_textbox textbox";

                        elSpace.setStyle(style);
                        elSpace.className = "multiline_textbox textbox";
                        elSpace.innerHTML = "&nbsp;";
                        elSpace.style.display = "inline";
                        elSpace.style.width = "auto";

                        edit.insert({ 'after': elWidget });
                        edit.insert({ 'after': elSpace });

                        edit.resizeFunction = function(event) {
                            (function() {
                                this.heightTemplateElement.innerHTML = LinkMeUI.StringUtils.TextToHtml(this.value) + "<br /><br /><br />";    /* Extra couple of line-breaks to always give it that multiline look */
                                (function() {
                                    // We're sizing the textarea the facebook way - match it to a DIV
                                    elWidget = this.heightTemplateElement;
                                    elSpace = this.widthOfASpaceTemplateElement;
                                    this.style.overflow = "hidden"; // Stops scrollbars from "causing themselves"
                                    this.style.width = (elWidget.offsetWidth + elSpace.getWidth() + 1) + "px"; /* Yes, that's right, textarea wrapping *includes* a space at the end of a line, plus ONE pixel :-) */
                                    this.style.height = (elWidget.offsetHeight) + "px";
                                }).bind(this, event).defer(event);
                            }).bind(this, event).defer(event);
                        }

                        // Keep tabs on those temporary elements for future resizing
                        edit.heightTemplateElement = elWidget;
                        edit.widthOfASpaceTemplateElement = elSpace;
                        edit.associatedLabel = labels[i];

                        // Resize after innerHTML is ready
                        edit.observe('keypress', edit.resizeFunction.bindAsEventListener(edit));
                        edit.observe('keydown', edit.resizeFunction.bindAsEventListener(edit));
                    }
                }
            }
        });
    }

    return new this.EditableSection(parameters);
}

LinkMeUI.Editor.RegisterAddControlCall = function(parameters) {
    if(!this.AddControlCall) {
        this.AddControlCall = Class.create();
        this.AddControlCall.prototype = Object.extend(new LinkMeUI.Editor.BaseRegistrator(), {
            //Though it is not a real GUID, it should be unique enough for current purposes...
            GenerateGuid : function() {
                var result, i, j;
                result = '';
                for(j=0; j<32; j++) {
                    if( j == 8 || j == 12|| j == 16|| j == 20)
                        result = result + '-';
                    i = Math.floor(Math.random()*16).toString(16).toUpperCase();
                    result = result + i;
                }
                return result;
            },
            
            initialize : function(parameters) {
                    
                this.ADD_CONTROLS_TO_END = parameters['addControlsToEnd'] == null ? true : parameters['addControlsToEnd'];
                this.CSS_NAME_RECORDS_COLLECTION = parameters['classNameRecordsCollection'];
                this.CSS_NAME_RECORDS_TEMPLATE = parameters['classNameRecordTemplate'];
                this.CSS_NAME_NO_CONTENTS = parameters['classNameRecordsNoContent'];
                this.CSS_NAME_CONTAINER = parameters['classNameContainingControl'];
                
                this.EnableScrollingToNewControl = parameters['enableScrollingToNewControl'] == null ? true : parameters['enableScrollingToNewControl'];
                                
                this.TemplateMap = parameters['templateMap'];
                
                this.Control = $(parameters['control']);    
                if(!this.Control) {
                    this.ThrowError('No control "' + control + '"has been found');
                } 

                this.TemplateArea =    this.CheckRequiredElementConditions(LinkMeUI.LocateHelper.GetElementsByClassName(this.Control, 
                                                                            this.CSS_NAME_RECORDS_TEMPLATE),
                                                                        this.CSS_NAME_RECORDS_TEMPLATE); 
                
                this.NoContentsArea = this.CheckRequiredElementConditions(LinkMeUI.LocateHelper.GetElementsByClassName(this.Control, 
                                                                            this.CSS_NAME_NO_CONTENTS),    this.CSS_NAME_NO_CONTENTS);                

                var templateControls = LinkMeUI.LocateHelper.GetElementsByClassName(this.TemplateArea, this.CSS_NAME_CONTAINER);
                if (templateControls.length == 0) {
                    throw 'Container "' + this.TemplateArea.id + '" does not contain a template element with CSS class "'
                        + this.CSS_NAME_CONTAINER + '".';
                }
                
                this.NewControl = templateControls[0].cloneNode(true);
                var newPostfix = this.GenerateGuid();
                this.NewControl.id = this.NewControl.id + newPostfix;
                
                if (this.TemplateMap == null)
                    throw 'The TemplateMap is null (undefined).';

                //This is to call native $H().clone() method in case TemplateMap is hash,
                //or just clone it in all other cases 
                if(this.TemplateMap.clone != null)
                    this.ControlsMap = this.TemplateMap.clone();
                else 
                    this.ControlsMap = Object.clone(this.TemplateMap);
                    
                var labels = this.NewControl.select('[for]');                                
                
                this.ControlsMap.each(function(pair) {
                    var ctrl = null;
                    var label = null;
                    this.NewControl.descendants().each(function(elem) {
                        if(elem.id == pair.value) {
                            ctrl = elem;
                            if(label != null) {
                                throw $break;
                            }
                        }
                        if(LinkMeUI.LocateHelper.GetForAttribute(elem) == pair.value) {
                            label = elem;
                            if(ctrl != null) {
                                throw $break;
                            }
                        }    
                    });        
                    
                    if(ctrl != null) {
                        if(label == null) {
                            //No corresponding label found for control, just change the ID
                            ctrl.id = ctrl.id + newPostfix;
                        } else {
                            ctrl.id = ctrl.id + newPostfix;
                            label.id = label.id + newPostfix;
                            LinkMeUI.LocateHelper.SetForAttribute(label, ctrl.id);
                        }
                        this.ControlsMap.set(pair.key, ctrl.id);
                    }        
                }, this);
                
                // MF (2009-02-06): Changed this.Control to this.RecordsetContainer, which is more correct.
                this.RecordsetContainer = LinkMeUI.LocateHelper.GetParentControlDivByClassName(this.Control, this.CSS_NAME_RECORDS_COLLECTION);
                if (!this.RecordsetContainer) {
                    this.RecordsetContainer = LinkMeUI.LocateHelper.GetElementsByClassName(this.Control, this.CSS_NAME_RECORDS_COLLECTION)[0];
                }

                if(this.ADD_CONTROLS_TO_END || this.RecordsetContainer.childNodes.length == 0) {
                    this.RecordsetContainer.appendChild(this.NewControl);            
                } else {                    
                    this.RecordsetContainer.insertBefore(this.NewControl, this.RecordsetContainer.childNodes[0]);            
                }
                
                if(this.NoContentsArea) {
                    this.NoContentsArea.hide();
                }

                var registeredControl = LinkMeUI.Editor.RegisterEditableControl({
                         classNameContainingControl : parameters['classNameContainingControl'], 
                         classNameEditable : parameters['classNameEditable'], 
                         classNameDisplay : parameters['classNameDisplay'], 
                         classNameNoContents : parameters['classNameNoContents'], 
                         classNameError : parameters['classNameError'], 
                         classNameEditLink : parameters['classNameEditLink'], 
                         classNameCancelLink : parameters['classNameCancelLink'],
                         classNameUpdateLink : parameters['classNameUpdateLink'],
                         classNameIgnoreClicks : parameters['classNameIgnoreClicks'],
                         classNameButtonUpdate : parameters['classNameButtonUpdate'], 
                         classNameButtonCancel : parameters['classNameButtonCancel'], 
                         classNameRecordsCollection : parameters['classNameRecordsCollection'], 
                         classNameRecordsNoContent : parameters['classNameRecordsNoContent'],
                         classNameRecordId : parameters['classNameRecordId'],                         
                         currentSectionControls : parameters['currentSectionControls'],
                         disableOtherControls : parameters['disableOtherControls'],
                         control : this.NewControl, 
                         controlsMap : this.ControlsMap,
                         isNewControl : true,
                         classNameDeleteRecordLink: parameters['classNameDeleteRecordLink'],
                         userValidateFunction : parameters['userValidateFunction'],
                         userUpdateFunction : parameters['userUpdateFunction'],
                         userDeleteFunction : parameters['userDeleteFunction'],
                         userPostUpdateFunction : parameters['userPostUpdateFunction'],
                         userPostAddFunction : parameters['userPostAddFunction'],
                         userPostDeleteFunction : parameters['userPostDeleteFunction'],
                         userPreEditFunction : parameters['userPreEditFunction'],
                         userPostCancelAddFunction : parameters['userPostCancelAddFunction'],
                         userPostCancelUpdateFunction : parameters['userPostCancelUpdateFunction'],
                         noImplicitEditClicks : parameters['noImplicitEditClicks'],
                         noMessageHighlighting : parameters['noMessageHighlighting'] });
                
                //AS: This dodgy setTimeout is here to show editable area in IE when it does 
                //just show it as a white rectange. That was happening in CandidateNotes only,
                //nothing in ResumeEditor. Can be caused by Prototype 1.6.
                //Retest on Prototype upgrade.
                setTimeout(registeredControl.Edit.bind(registeredControl), 0);
                
                if(this.EnableScrollingToNewControl)
                    new Effect.ScrollTo(registeredControl.Control, {offset: -70});                    
            }
        });
    }
    
    //OnclickHandler is a simple proxy class, containing all the variables needed
    //to create a new editable section
    if(!this.OnClickHandler) {
        this.OnClickHandler = function(parameters) {
            
            this.Handler = function() {

                return new this.parameters['addControlCall'](
                            {control : this.parameters['control'], 
                            classNameRecordsCollection : this.parameters['classNameRecordsCollection'], 
                            classNameRecordTemplate : this.parameters['classNameRecordTemplate'],
                            classNameRecordsNoContent : this.parameters['classNameRecordsNoContent'],
                            classNameRecordId : this.parameters['classNameRecordId'],
                            classNameContainingControl : this.parameters['classNameContainingControl'],
                            classNameDeleteRecordLink: parameters['classNameDeleteRecordLink'], 
                            classNameEditable : this.parameters['classNameEditable'], 
                            classNameDisplay : this.parameters['classNameDisplay'], 
                            classNameNoContents : this.parameters['classNameNoContents'], 
                            classNameError : this.parameters['classNameError'], 
                            classNameEditLink : this.parameters['classNameEditLink'], 
                            classNameCancelLink : parameters['classNameCancelLink'],
                            classNameUpdateLink : parameters['classNameUpdateLink'],
                            classNameIgnoreClicks : this.parameters['classNameIgnoreClicks'],
                            classNameButtonUpdate : this.parameters['classNameButtonUpdate'], 
                            classNameButtonCancel : this.parameters['classNameButtonCancel'],                        
                            disableOtherControls : this.parameters['disableOtherControls'],                             
                            templateMap : this.parameters['templateMap'], 
                            currentSectionControls : this.parameters['currentSectionControls'],
                            userValidateFunction : this.parameters['userValidateFunction'],
                            userDeleteFunction : this.parameters['userDeleteFunction'],
                            userUpdateFunction : this.parameters['userUpdateFunction'], 
                            userPostUpdateFunction : this.parameters['userPostUpdateFunction'],
                            userPostAddFunction : parameters['userPostAddFunction'],
                            userPostDeleteFunction : this.parameters['userPostDeleteFunction'],
                            userPreEditFunction : this.parameters['userPreEditFunction'],
                            userPostCancelAddFunction : parameters['userPostCancelAddFunction'],
                            userPostCancelUpdateFunction : parameters['userPostCancelUpdateFunction'],
                            addControlsToEnd : this.parameters['addControlsToEnd'],
                            enableScrollingToNewControl : this.parameters['enableScrollingToNewControl'],
                            noImplicitEditClicks : this.parameters['noImplicitEditClicks'],
                            noMessageHighlighting : this.parameters['noMessageHighlighting'] });
                            
            }
            
            this.parameters = parameters;
            
            LinkMeUI.LocateHelper.GetElementsByClassName($(this.parameters['control']), 
                                                         parameters['classNameAddSection']).invoke('observe', 
                                                         'click', 
                                                         this.Handler.bindAsEventListener(this));    
        }
    }
    
    return new this.OnClickHandler({
                            addControlCall : this.AddControlCall, 
                            control : parameters['control'], 
                            classNameAddSection : parameters['classNameAddSection'], 
                            classNameRecordsCollection : parameters['classNameRecordsCollection'],    
                            classNameRecordTemplate : parameters['classNameRecordTemplate'],
                            classNameRecordsNoContent : parameters['classNameRecordsNoContent'],
                            classNameRecordId : parameters['classNameRecordId'],
                                  classNameContainingControl : parameters['classNameContainingControl'],
                            classNameDeleteRecordLink: parameters['classNameDeleteRecordLink'], 
                            classNameEditable : parameters['classNameEditable'], 
                            classNameDisplay : parameters['classNameDisplay'], 
                            classNameNoContents : parameters['classNameNoContents'],
                            classNameError : parameters['classNameError'], 
                            classNameEditLink : parameters['classNameEditLink'],
                            classNameCancelLink : parameters['classNameCancelLink'],
                            classNameUpdateLink : parameters['classNameUpdateLink'],
                            classNameIgnoreClicks : parameters['classNameIgnoreClicks'],
                            classNameButtonUpdate : parameters['classNameButtonUpdate'], 
                            classNameButtonCancel : parameters['classNameButtonCancel'], 
                            disableOtherControls : parameters['disableOtherControls'],                             
                            templateMap : parameters['templateMap'], 
                            currentSectionControls: parameters['currentSectionControls'],
                            userValidateFunction : parameters['userValidateFunction'],
                            userUpdateFunction : parameters['userUpdateFunction'], 
                            userDeleteFunction : parameters['userDeleteFunction'],
                            userPostUpdateFunction : parameters['userPostUpdateFunction'],
                            userPostAddFunction : parameters['userPostAddFunction'],
                            userPostDeleteFunction : parameters['userPostDeleteFunction'],
                            userPreEditFunction : parameters['userPreEditFunction'],
                            userPostCancelAddFunction : parameters['userPostCancelAddFunction'],
                            userPostCancelUpdateFunction : parameters['userPostCancelUpdateFunction'],
                            addControlsToEnd : parameters['addControlsToEnd'], 
                            enableScrollingToNewControl : parameters['enableScrollingToNewControl'],
                            noImplicitEditClicks : parameters['noImplicitEditClicks'],
                            noMessageHighlighting : parameters['noMessageHighlighting'] });
    }

//TODO 3.3: Use simpleHistory library for the whole history management thing
var isHistoryEnabled = false;

function GetIrameDoc() {
    var frame = $('iframeAjaxHistory');    
    if (!frame)
        throw 'The document must contain an <iframe> with ID "iframeAjaxHistory".';
    return frame.contentDocument || frame.contentWindow.document;
}

function GoBack() {
    if(!LinkMeUI.LocateHelper.IsMSIE()) {
        //need to do that for mozilla
        history.back();
    }
}

function ClearHistory() {}

function SaveHistoryStep() {    
    isHistoryEnabled = false;
    var doc = GetIrameDoc();
    doc.open();
    doc.write('<html><head><script language="javascript">var pWindow = parent; if(pWindow.isHistoryEnabled == true && pWindow.LinkMeUI.Editor.CurrentControl){pWindow.LinkMeUI.Editor.CurrentControl.Cancel(); pWindow.GoBack();}</script></head><body>1</body></html>');
    doc.close();
    doc.open();
    doc.write('<html><head></head><body></body></html>');
    doc.close();
    isHistoryEnabled = true;
}
