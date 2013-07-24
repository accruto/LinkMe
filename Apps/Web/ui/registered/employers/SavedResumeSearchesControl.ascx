<%@ Import Namespace="System.ComponentModel"%>
<%@ Control Language="c#" EnableViewState="false" AutoEventWireup="False" Codebehind="SavedResumeSearchesControl.ascx.cs" Inherits="LinkMe.Web.UI.Registered.Employers.SavedResumeSearchesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>

<asp:PlaceHolder id="phNoItems" runat="server" visible="false">
    <div class="section-content">
        You currently have no saved searches.
    </div>
</asp:PlaceHolder>

<asp:PlaceHolder id="phNotLoggedIn" runat="server" visible="false">
    <div class="section-content">
        When you login as an employer or recruiter, you can access your saved searches quickly from here.
    </div>
</asp:PlaceHolder>

<asp:PlaceHolder ID="phNotColumnar" Visible="<%# !ColumnarLayout %>" runat="server">

    <div class="list_section-content section-content">

        <asp:Repeater id="rptSavedResumeSearches" runat="server" visible="false" OnItemCommand="rptSavedResumeSearches_ItemCommand">
            <HeaderTemplate>
                <table class="saved-searches list">
            </HeaderTemplate>
            <ItemTemplate>
                <tbody class="js_csavedsearch_control" id="divSavedSearches<%# Container.ItemIndex %>">
                    <%-- Display view --%>
                    <tr class="csavedsearch_display named_saved-search hotlist_saved-search saved-search item">
                        <td class="description">
                            <asp:LinkButton id="lbExecute" runat="server" commandname="Execute" CssClass='<%# !IsNamed(Container.DataItem) ? "search-terms execute" : "search-name execute" %>'
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'><span id="lblSavedSearchName<%# Container.ItemIndex %>"
                            for="txtSavedSearchName<%# Container.ItemIndex %>"><asp:Literal ID="litDescription" Runat="server" /></span></asp:LinkButton>
                            <input id="hidSavedSearchId<%# Container.ItemIndex %>" type="hidden" value="<%# DataBinder.Eval(Container.DataItem, "Id") %>" />
    			        </td>
    			        <td class="actions">
                            <ul class="plain_inline_action-list inline_action-list action-list" id="alSavedSearchActions">
                                <li><asp:HyperLink runat="server" class="edit-action js_edit" href="javascript:void(0);" text="Rename" id="hlRename" /></li>
                                <li><asp:LinkButton CssClass="delete-action" id="lbRemove" runat="server" commandname="Remove"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' Text="Remove" /></li>
                            </ul>
                            <asp:Label ID="lblAlertExists" CssClass="alert-created" runat="server">Alert created</asp:Label>
                            <asp:PlaceHolder ID="phCreateAlert" runat="server">
                                <ul class="create-alert_action-list plain_inline_action-list inline_action-list action-list">
                                    <li><asp:LinkButton CssClass="candidate-alert-action" id="lbCreateAlert" runat="server"
                                        commandname="CreateEmailAlert" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'>
                                            <asp:Literal ID="lblCreateEmailAlert" Runat="server" Text="Create email alert" />
                                        </asp:LinkButton></li>
                                </ul>
                            </asp:PlaceHolder>
                        </td>
                    </tr>
                    <%-- Rename folder view --%>
                    <tr style="display:none;" class="csavedsearch_editable named_saved-search hotlist_saved-search saved-search item">
                        <td class="description forms_v2" colspan="2">
                            <fieldset class="row">
                                <div class="textbox_field field">
                                    <div class="textbox_control control">
                                        <input id="txtSavedSearchName<%# Container.ItemIndex %>" type="text" class="textbox"
                                            value="<%# DataBinder.Eval(Container.DataItem, "Name") %>"
                                            maxlength="<%= LinkMe.Query.Search.Members.Constants.MemberSearchNameMaxLength %>"
                                            onkeypress="
                                            if (event.keyCode == 13) {
                                                $('btnSaveSavedSearchName<%# Container.ItemIndex %>').click();
                                                return CancelEvent(event);
                                            } else if (event.keyCode == 27) {
                                                $('btnCancelFolderName<%# Container.ItemIndex %>').click();
                                            }"
                                        />
                                    </div>
                                </div>
                                <div class="buttons_field field">
                                    <div class="buttons_control control">
                                        <input id="btnSaveSavedSearchName<%# Container.ItemIndex %>" class="js_save save-button button" type="button" />
                                        <input id="btnCancelSavedSearchName<%# Container.ItemIndex %>" class="js_cancel cancel-button button" type="button" />
                                    </div>
                                </div>
                            </fieldset>
                            <div class="error-message csavedsearch_error" style="display: none;"></div>
                        </td>
                    </tr>
                </tbody>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

    </div>

</asp:PlaceHolder>

<asp:PlaceHolder ID="phColumnar" Visible="<%# ColumnarLayout %>" runat="server">
    <div class="list_section-content section-content">
        <div class="columns">
            <asp:Repeater ID="rptSavedResumeSearchesColumnar" runat="server" Visible="false" OnItemCommand="rptSavedResumeSearches_ItemCommand">
                <ItemTemplate>
                    <%# IsNewColumn(Container.ItemIndex, 2) ? "<div class=\"column\"><ul class=\"saved-searches minilist\">" : "" %>
                        <div class="js_csavedsearch_control" id="divSavedSearches<%# Container.ItemIndex %>">
                            <li class="<%# IsNamed(Container.DataItem) ? "named" : "unnamed" %>_saved-search csavedsearch_display hotlist_saved-search saved-search item item_<%# (Container.ItemIndex % 2) == 0 ? "odd" : "even" %>"
                             onmouseout="$(this).removeClassName('<%# IsNamed(Container.DataItem) ? "named" : "unnamed" %>_saved-search_hover'); $(this).removeClassName('saved-search_hover');"
                             onmouseover="$(this).addClassName('<%# IsNamed(Container.DataItem) ? "named" : "unnamed" %>_saved-search_hover'); $(this).addClassName('saved-search_hover');">
                                <asp:LinkButton id="lbExecute" runat="server" commandname="Execute" CssClass='<%# !IsNamed(Container.DataItem) ? "search-terms execute" : "search-name execute" %>'
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'><span
                                id="lblSavedSearchName<%# Container.ItemIndex %>" for="txtSavedSearchName<%# Container.ItemIndex %>"><asp:Literal
                                ID="litDescription" Runat="server" /></span></asp:LinkButton>
                                <ul class="corner_inline_action-list inline_action-list action-list">
                                    <li><asp:HyperLink runat="server" class="edit-action js_edit" href="javascript:void(0);" text="Rename" id="hlRename" /></li>
                                    <li><asp:LinkButton CssClass="delete_button button" id="lbRemove" runat="server" commandname="Remove"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' Text="Remove" /></li>
                                </ul>
                            </li>
                            <li class="forms_v2 csavedsearch_editable hotlist_saved-search saved-search
                            item item_<%# (Container.ItemIndex % 2) == 0 ? "odd" : "even" %>"
                            style="display: none">
                                <fieldset class="row">
                                    <div class="textbox_field field">
                                        <div class="textbox_control control">
                                            <input id="txtSavedSearchName<%# Container.ItemIndex %>" type="text" class="textbox"
                                                value="<%# DataBinder.Eval(Container.DataItem, "Name") %>"
                                                maxlength="<%= LinkMe.Query.Search.Members.Constants.MemberSearchNameMaxLength %>"
                                                onkeypress="
                                                if (event.keyCode == 13) {
                                                    $('btnSaveSavedSearchName<%# Container.ItemIndex %>').click();
                                                    return CancelEvent(event);
                                                } else if (event.keyCode == 27) {
                                                    $('btnCancelFolderName<%# Container.ItemIndex %>').click();
                                                }"
                                            />
                                        </div>
                                    </div>
                                    <div class="buttons_field field">
                                        <div class="buttons_control control">
                                            <input id="btnSaveSavedSearchName<%# Container.ItemIndex %>" class="js_save save-button button" type="button" />
                                            <input id="btnCancelSavedSearchName<%# Container.ItemIndex %>" class="js_cancel cancel-button button" type="button" />
                                        </div>
                                    </div>
                                    <input id="hidSavedSearchId<%# Container.ItemIndex %>" type="hidden" value="<%# DataBinder.Eval(Container.DataItem, "Id") %>" />
                                </fieldset>
                                <div class="error-message csavedsearch_error" style="display: none;"></div>
                            </li>
                        </div>
                    <%# IsNewColumn(Container.ItemIndex+1, 2) ? "</ul></div>" : "" %>        
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>    

</asp:PlaceHolder>

<script type="text/javascript">
    // Functions used by Rename and Remove links
    
    function SaveSavedSearchName(callback, index) {
        return LinkMe.Web.UI.Controls.Employers.AjaxMemberSearchEditor.SetMemberSearchName(
            $F('hidSavedSearchId' + index), $F('txtSavedSearchName' + index), callback);
    }

    function GetSaveFunction(index) {
        return function(callback) {
            return SaveSavedSearchName(callback, index);
        };
    }
    
    function UpdateLabel(callback, index) {
        var text = LinkMe.Web.UI.Controls.Employers.AjaxMemberSearchEditor
            .GetMemberSearchDisplayHtml($F('hidSavedSearchId' + index), callback).value.Message;
        var unnamed = $('txtSavedSearchName' + index).value == "";
        $('lblSavedSearchName' + index).innerHTML = text;
        $('lblSavedSearchName' + index).up().removeClassName("search-terms");
        $('lblSavedSearchName' + index).up().addClassName(unnamed ? "search-terms" : "search-name");
        $('lblSavedSearchName' + index).up().up().removeClassName("named_saved-search");
        $('lblSavedSearchName' + index).up().up().removeClassName("unnamed_saved-search");
        $('lblSavedSearchName' + index).up().up().addClassName(unnamed ? "unnamed_saved-search" : "named_saved-search");
    }

    function GetUpdateLabelFunction(index) {
        return function(callback) {
            return UpdateLabel(callback, index);
        };
    }
    
    // Wire up Rename and Remove links

    var index;
    for (index = 0; index < <%= MemberSearchCount() %>; index++) {
        LinkMeUI.Editor.RegisterEditableControl({
            classNameContainingControl: 'js_csavedsearch_control', 
            classNameEditable : 'csavedsearch_editable', 
            classNameDisplay: 'csavedsearch_display', 
            classNameError : 'csavedsearch_error', 
            classNameEditLink : 'js_edit',
            classNameButtonUpdate : 'js_save',
            classNameButtonCancel : 'js_cancel',
            disableOtherControls : false,
            noImplicitEditClicks : true,
            noMessageHighlighting : true,
            control : 'divSavedSearches' + index,
            userUpdateFunction : GetSaveFunction(index),
            userPostUpdateFunction : GetUpdateLabelFunction(index),
            focusFirstControlSelectAll : true
        });

        //$('lnkRemove' + index).observe('click', GetRemoveFunction(index));
    }
</script>