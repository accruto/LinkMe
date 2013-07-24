<%@ Control EnableViewState="false" Language="C#" AutoEventWireup="false" Codebehind="WillingnessToRelocate.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.WillingnessToRelocate" %>
<%@ Import Namespace="LinkMe.Domain.Roles.Candidates"%>
<%@ Import namespace="LinkMe.Web.UI.Controls.Networkers"%>
<%@ Import namespace="LinkMe.Utility.Validation"%>
<%@ Import Namespace="LinkMe.Domain.Location"%>
<%@ Import Namespace="LinkMe.Web.UI.Controls.Common" %>
<%@ Import Namespace="LinkMe.Web.Service" %>
<%@ Register TagPrefix="cc" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>

<asp:HiddenField ID="hiddSelectedLocalities" runat="server" />

<div class="willingness-to-relocate_ascx forms_v2">
    <asp:PlaceHolder ID="phFieldsetOpeningTag" runat="server">
        <fieldset class="<%# LabelsOnLeft ? "with-labels-on-left" : ""%>">
    </asp:PlaceHolder>
    <div class="horizontal_radiobuttons_field radiobuttons_field field">
        <label>Would you move for a job?</label>
        <div class="horizontal_radiobuttons_control radiobuttons_control control">
            <div class="radio_control control">
                <asp:RadioButton ID="rbNotWilling" CssClass="radio" runat="server" GroupName="doiwanttorelocate" Checked="true" Text="No"/>
            </div>
            <div class="radio_control control">            
                <asp:RadioButton ID="rbYesWilling" CssClass="radio" runat="server" GroupName="doiwanttorelocate" Text="Yes" />
            </div>
            <div class="radio_control control">            
                <asp:RadioButton ID="rbWouldConsider" CssClass="radio" runat="server" GroupName="doiwanttorelocate" Text="Would consider" />
            </div>
        </div>
    </div>
    <asp:PlaceHolder ID="phFieldsetClosingTag" runat="server">
        </fieldset>
    </asp:PlaceHolder>
    <div id="divRelocationArea" runat="server" class="relocation-area">
        <asp:PlaceHolder ID="phFieldset2OpeningTag" runat="server">
            <fieldset class="<%# LabelsOnLeft ? "with-labels-on-left" : ""%>">
        </asp:PlaceHolder>
        <div class="country_dropdown_field dropdown_field field">
            <label>Where can you relocate?</label>
            <div class="country_dropdown_control dropdown_control control">
                <select id="ddlCountry" class="country_dropdown dropdown">
                    <asp:Repeater ID="rptrCounties" runat="server">
                        <ItemTemplate>
                            <option value="<%# ((Country)Container.DataItem).Id %>"><%# ((Country)Container.DataItem).Name %></option>
                        </ItemTemplate>
                    </asp:Repeater>
                </select>
            </div>      
        </div> 
        <div class="textbox_field field">
            <label>Location</label>
            <div class="textbox_control control">
                <input type="text" id="txtLocationFilter" class="textbox" />
                <img src="" alt="" id="imgFilterLoading" style="display: none; margin-left: -30px;" />
            </div>
        </div>
        <div class="listboxpair_field field">
            <div class="listboxpair_control control">
                <fieldset>
                    <div class="from_listbox_field listbox_field field">
                        <label>Select</label>
                        <div class="from_listbox_control listbox_control control">
                            <select id="selLocalities" multiple="multiple" class="from_listbox listbox" ></select>
                        </div>
                    </div>
                    <div class="buttons_field field">
                        <div class="buttons_control control">
                            <input type="button" id="btnAdd" value="&raquo;" class="right_guillemet_button guillemet_button button" />
                            <input type="button" id="btnRemove" value="&laquo;" class="left_guillemet_button guillemet_button button" />
                        </div>
                    </div>
                    <div class="to_listbox_field listbox_field field">
                        <label>Locations you've selected</label>
                        <div class="to_listbox_control listbox_control control">
                            <asp:ListBox id="selTarget" SelectionMode="Multiple" CssClass="to_listbox listbox" runat="server" />
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
        <asp:PlaceHolder ID="phFieldset2ClosingTag" runat="server">
            </fieldset>
        </asp:PlaceHolder>
    </div>
</div>
<script type="text/javascript">
        function WillingnessToRelocate(params) {            
            this.showLoading = function(doShow) {
                var img = $(this.p['loadingIndicator']);
                img.src = '<%=ApplicationPath %>/ui/images/universal/loading.gif';
                if(doShow)
                    img.show();
                else 
                    img.hide();
            }  
            
            this.GetSelectedCountryOption = function() {
                var ddl = $(this.p['counryDDL']);
                return ddl.options[ddl.selectedIndex];
            }

            this.GetSelectedCountryOptionIndex = function() {
                return $(this.p['counryDDL']).selectedIndex;
            }
                                  
            this.SetSelectedCountryOptionIndex = function(val) {
                $(this.p['counryDDL']).selectedIndex = val;      
            }

            this.onLocationFilterKeyUp = function (evt) {
                if(this.AjaxLocked)
                    return;

                var txt = $(this.p['locationFilter']);

                if(txt.value == this.oldTxtVal)
                    return;
                    
                this.showLoading(true);
                this.oldTxtVal = txt.value;                
                var pars = '<%=GetStructuredLocations.CountryParameter%>=' + 
                            this.GetSelectedCountryOption().value +
                           '&<%=GetStructuredLocations.MaximumParameter%>=10' + 
                           '&<%=GetStructuredLocations.LocationParameter%>=' + txt.value;
                    
                this.AjaxLocked = true;
                
                new Ajax.Request('<%=ApplicationPath %>/service/GetStructuredLocations.ashx', 
                                 { method: 'get', 
                                 parameters: pars, 
                                 onComplete: this.onAjaxCompleted.bindAsEventListener(this) } );
                
            }
            
            this.GetNewOption = function(texValObj) {
                var opt = new Option(texValObj.text, texValObj.value);
                return opt;
            }                      
            
            this.GetNewValueTextObject = function(value, text) {
                var res = new Object();
                res.value = value;
                res.text = text;
                
                return res;
            }
            
            this.ParseOptionValue = function(item) {
                var pItem = new Object();
                var arr = item.split('<%=GetStructuredLocations.NameDivider%>');
                var typeID = arr[0].split('<%=GetStructuredLocations.TypeDivider%>');
                pItem.Id = typeID[0];
                pItem.Type = typeID[1];                
                pItem.Name = arr[1];
                
                pItem.IsCountry = (pItem.Type == '<%= GetStructuredLocations.CountryType%>');
                pItem.IsCountrySubdivision = (pItem.Type == '<%= GetStructuredLocations.CountrySubdivisionType%>');
                pItem.IsRegion = (pItem.Type == '<%= GetStructuredLocations.RegionType%>');
                pItem.IsSuburb = (pItem.Type == '<%= GetStructuredLocations.PostalSuburbType%>');
                return pItem;
            }
            
            this.DisassembleString = function(data) {
                var res = new Object();
                res.country = null;
                res.regions = $A();
                res.countrySubdivs  = $A();              
                res.suburbs = $A();                             

                var values = $A(data.split('<%= GetStructuredLocations.RecordsDivider %>'));
                
                if(values.length == 1 && values[0].length == 0) {
                    res.IsEmpty = true;
                    return res;
                } else {
                    values.each(function(item) {
                        if(item != this.NoValue) {
                            //Value is in following format:
                            // id:type=[$]name
                            var parsedItem = this.ParseOptionValue(item);
                            
                            var newElem = this.GetNewValueTextObject(item, parsedItem.Name);
                            
                            if(parsedItem.IsCountry)
                                res.country = this.GetNewValueTextObject(item, parsedItem.Name);
                            else if(parsedItem.IsCountrySubdivision) 
                                res.countrySubdivs.push(newElem);
                            else if(parsedItem.IsRegion)
                                    res.regions.push(newElem);
                            else if(parsedItem.IsSuburb)
                                    res.suburbs.push(newElem);                                    
                            else if(window.console != null && window.console.log != null)
                                    console.log('Element of type ' + typeID[1] + ' is unknown.');
                        }                        
                    }, this);
                }
                return res;
            }
            
            this.AssembleString = function(options) {
                var res = '';
                for(var i = 0; i < options.length; i++) {
                    res += options[i].value;
                    if(i != options.length -1)
                        res += ';'
                }
                return res;
            }
            
            this.UpdateListOptions = function(list, options, append) {
                if(!append)
                    list.options.length = 0;
                
                options.each(function(item) {
                    var exists = false;
                    for(var i = 0; i < list.options.length; i++) {
                        if(item.value == list.options[i].value && 
                           item.text == list.options[i].text) {
                            exists = true;
                            break;
                        }                                                                
                    }
                    
                    if(!exists) {
                        if(item.parentNode != null) {
                            item.parentNode.removeChild(item);
                        }
                        list.options.add(item);
                    }
                }, this);
            }
            
            this.RenderList = function(parsedData, list) {
                var options = $A();                
                
                if(parsedData.country) 
                    options.push(this.GetNewOption(parsedData.country));
                                        
                if(parsedData.countrySubdivs .length != 0) {
                    options.push(this.GetNewOption(this.GetNewValueTextObject(this.NoValue, '--- States ---')));
                    parsedData.countrySubdivs.each(function(item) {
                        options.push(this.GetNewOption(item));
                    }, this);
                }
                
                if(parsedData.regions.length != 0) {
                    options.push(this.GetNewOption(this.GetNewValueTextObject(this.NoValue, '--- Regions ---')));
                    parsedData.regions.each(
                    function(item) { 
                        options.push(this.GetNewOption(item)); 
                    }, this);                    
                }               
                
                if(parsedData.suburbs.length != 0) {
                    options.push(this.GetNewOption(this.GetNewValueTextObject(this.NoValue, '--- Suburbs ---')));
                    parsedData.suburbs.each(function(item) {
                        options.push(this.GetNewOption(item));
                    }, this);
                }
                
                this.UpdateListOptions(list, options);
            }
            
            this.onAjaxCompleted = function(req) {
                if (req == null || req.responseText == null) 
                    return;
                
                var list = $(this.p['localitiesSource']);                
                
                var parsedData = this.DisassembleString(req.responseText);                
                if(parsedData.IsEmpty) 
                    this.UpdateListOptions(list, $A([this.GetNewOption(this.GetNewValueTextObject(this.NoValue, 'nothing found'))]));                    
                else
                    this.RenderList(parsedData, list);

                this.AjaxLocked = false;
                this.showLoading(false);
            }
            
            this.onRbSelectChange = function (evt) {
                var nW = $(this.p['radioButtonNo']);
                var yW = $(this.p['radioButtonYes']);
                var wC = $(this.p['radioButtonYesWould']);
                
                if(nW.checked) 
                    $(this.p['mainArea']).hide();
                else 
                    $(this.p['mainArea']).show();            
            }
            
            this.onAddClicked = function(evt) {                
                var sList = $(this.p['localitiesSource']);
                var tList = $(this.p['localitiesTarget']);
                var addedItems = $A();
                var leftItems = $A();              

                
                $A(sList.options).each(function(item) {
                    if(item.selected && item.value != this.NoValue)
                        addedItems.push(item);
                    else 
                        leftItems.push(item);
                }, this);
                
                if(addedItems.length == 0)
                    return;
                    
                //Add items to the target list                
                
                var newOptions = $A();                
                addedItems.each(function(item){
                    newOptions.push(this.GetNewOption(item));
                }, this);                
                
                this.UpdateListOptions(tList, newOptions, true);
                
                //Repaint source list
                var leftItemsStr = this.AssembleString(leftItems);                
                var leftItemsData = this.DisassembleString(leftItemsStr);                
                this.RenderList(leftItemsData, sList);
                
                //Store all values from target list to hidden field                
                this.SaveSelection();
            }
            
            this.onRemoveClicked = function(evt) {
                var tList = $(this.p['localitiesTarget']);
                var removedItems = $A();
                
                $A(tList.options).each(function(item) {
                    if(item.selected) {
                        removedItems.push(item);
                        item.parentNode.removeChild(item);
                    }
                }, this);
                
                if(removedItems.length == 0)
                    return;
                
                var sList = $(this.p['localitiesSource']);
                var newOptions = $A(sList.options);                
                removedItems.each(function(item) {newOptions.push(item); });
                                
                var newItemsStr = this.AssembleString(newOptions);                
                var newItemsData = this.DisassembleString(newItemsStr);
                this.RenderList(newItemsData, sList);
                
                //Store all values from target list to hidden field                
                this.SaveSelection();
            }
            
            this.SaveSelection = function() {
                var tList = $(this.p['localitiesTarget']);
                var hF = $(this.p['selectionStorage']);                
                var val = '';
                for(var i = 0; i < tList.options.length; i++) {                    
                    var item = this.ParseOptionValue(tList.options[i].value);
                    val += item.Id + '<%=GetStructuredLocations.NameDivider%>' + item.Name; 
                    if(i != tList.options.length -1)
                        val += '<%= GetStructuredLocations.RecordsDivider%>';
                }
                
                this.SetSelectedLocalities(val);
            }

            this.onDDLSelected = function(evt) {
                this.oldTxtVal = null;
                $(this.p['locationFilter']).value = '';
                this.onLocationFilterKeyUp();
            }
            
            this.IsDoNotWantToRelocateChecked = function() {
                return $(this.p['radioButtonNo']).checked;
            }
            
            this.RELOCATION_PREFERENCE_NO = '<%= RelocationPreference.No %>';
            this.RELOCATION_PREFERENCE_YES = '<%= RelocationPreference.Yes %>';
            this.RELOCATION_PREFERENCE_WOULD = '<%= RelocationPreference.WouldConsider %>';
            
            this.GetRelocationPreference = function() {
                if($(this.p['radioButtonNo']).checked) 
                    return this.RELOCATION_PREFERENCE_NO;
                else if($(this.p['radioButtonYes']).checked)
                    return this.RELOCATION_PREFERENCE_YES;
                else if($(this.p['radioButtonYesWould']).checked)
                    return this.RELOCATION_PREFERENCE_WOULD;
                    
                throw 'No known radiobuttons selected!';
            }

            this.GetSelectedLocalities = function() {
                return $F(this.p['selectionStorage']);
            }
            
            this.SetSelectedLocalities = function(val) {
                return $(this.p['selectionStorage']).value = val;
            }

            this.SaveValue = function() {
                this.SavedValue = this.GetRelocationPreference();
                this.SavedLocalities = this.GetSelectedLocalities();
                this.SavedTargetListOptions = $A($(this.p['localitiesTarget']).options);
                this.SavedSourceListOptions = $A($(this.p['localitiesSource']).options);
                this.SavedCountryIndex = this.GetSelectedCountryOptionIndex();
            }
            
            this.RestoreValue = function() {
                //Restore selection
                if(this.SavedValue == this.RELOCATION_PREFERENCE_NO) 
                    $(this.p['radioButtonNo']).click();
                else if(this.SavedValue == this.RELOCATION_PREFERENCE_YES)
                    $(this.p['radioButtonYes']).click();
                else if(this.SavedValue == this.RELOCATION_PREFERENCE_WOULD)
                    $(this.p['radioButtonYesWould']).click();
                else 
                    throw 'Unkonw value is set as relocation choice!';                
                    
                //Restore lists
                var options = $A();
                this.SavedTargetListOptions.each(function(item) {
                    options.push(this.GetNewOption(item));
                }, this);
                this.UpdateListOptions($(this.p['localitiesTarget']), options);
                
                options = $A();
                this.SavedSourceListOptions.each(function(item) {
                    options.push(this.GetNewOption(item));
                }, this);
                this.UpdateListOptions($(this.p['localitiesSource']), options);
                
                //Restore hidden field
                this.SetSelectedLocalities(this.SavedLocalities);                
                
                //Restore country index
                this.SetSelectedCountryOptionIndex(this.SavedCountryIndex);
            }
            
            this.Validate = function() {
            }

            this.p = params;
            this.AjaxLocked = false;
            this.oldTxtVal = '';
            this.NoValue = '<%= EmptyID %>';
            
            $(this.p['radioButtonNo']).observe('click', this.onRbSelectChange.bindAsEventListener(this));
            $(this.p['radioButtonYes']).observe('click', this.onRbSelectChange.bindAsEventListener(this));
            $(this.p['radioButtonYesWould']).observe('click', this.onRbSelectChange.bindAsEventListener(this));
            
            $(this.p['locationFilter']).observe('keyup', this.onLocationFilterKeyUp.bindAsEventListener(this));
            
            $(this.p['addButton']).observe('click', this.onAddClicked.bindAsEventListener(this));
            $(this.p['removeButton']).observe('click', this.onRemoveClicked.bindAsEventListener(this));
            $(this.p['localitiesSource']).observe('dblclick', this.onAddClicked.bindAsEventListener(this));
            $(this.p['localitiesTarget']).observe('dblclick', this.onRemoveClicked.bindAsEventListener(this));
            $(this.p['counryDDL']).observe('change', this.onDDLSelected.bindAsEventListener(this));
            
            this.oldTxtVal = null;
            this.onLocationFilterKeyUp();
        }
        
        var <%=JSObjectName() %> = new WillingnessToRelocate({
                            selectionStorage : '<%=hiddSelectedLocalities.ClientID %>',
                            loadingIndicator: 'imgFilterLoading',
                            locationFilter : 'txtLocationFilter',
                            radioButtonNo : '<%=rbNotWilling.ClientID%>',
                            radioButtonYes : '<%=rbYesWilling.ClientID%>',
                            radioButtonYesWould : '<%=rbWouldConsider.ClientID%>',
                            mainArea : '<%=divRelocationArea.ClientID %>',
                            counryDDL : 'ddlCountry',
                            localitiesSource : 'selLocalities',
                            localitiesTarget: '<%= selTarget.ClientID %>',
                            addButton : 'btnAdd',
                            removeButton : 'btnRemove'
                        });
</script>
