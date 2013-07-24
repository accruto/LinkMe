<%@ Import namespace="LinkMe.Web"%>
<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="EmployerPrivacy.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.EmployerPrivacy" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Register TagPrefix="uc" TagName="CommunityCandidateImage" Src="~/ui/controls/employers/CommunityCandidateImage.ascx" %>

<asp:PlaceHolder ID="phPrivacyBlurb" runat="server">
    <p>
        LinkMe will never reveal your personal address or email address to employers unless you actually 
        apply for a job they have advertised. See our 
        <a href="<%= SupportRoutes.Privacy.GenerateUrl() %>">Privacy statement</a> for more details.
    </p>
</asp:PlaceHolder>
    
<asp:PlaceHolder ID="phFieldsetOpeningTag" runat="server">
    <span class="forms_v2">
        <fieldset class="<%# LabelsOnLeft ? "wider_with-labels-on-left with-labels-on-left" : "" %>">
</asp:PlaceHolder>

<div class="checkbox_field field">
    <div class="checkbox_control control">
        <asp:CheckBox ID="chkAnonResume" CssClass="checkbox" Text="Allow employers to find my resume" runat="server" />
    </div>
</div>

<div class="checkboxes_field indented_field field">
    <label>Allow employers to see my<asp:PlaceHolder ID="phColon" Visible="<%# !LabelsOnLeft %>" runat="server">:</asp:PlaceHolder></label>
    <div class="checkboxes_control control">
        <div class="checkbox_control control">
            <asp:CheckBox ID="chkName" CssClass="checkbox" Text="Name" runat="server" />
        </div>
        <div class="checkbox_control control">
            <asp:CheckBox ID="chkPhone" CssClass="checkbox" Text="Phone numbers" runat="server" />
        </div>
        <asp:PlaceHolder ID="phShowPhotoOption" runat="server">
            <div class="checkbox_control control">
                <asp:CheckBox ID="chkPhoto" CssClass="checkbox" Text="Photo" runat="server" />
            </div>
        </asp:PlaceHolder>
        <div class="checkbox_control control">
            <asp:CheckBox ID="chkRecentEmployers" CssClass="checkbox" Text="Current &amp; previous employers" runat="server" />
        </div>
        
        <asp:PlaceHolder id="phCommunity" runat="server">
            <div class="checkbox_control control">
                <asp:CheckBox ID="chkCommunity" CssClass="checkbox" runat="server" />
                <p>
                    You joined through <asp:Literal ID="litCommunity" runat="server" />
                    and therefore you can let employers see their logo beside your name.
                </p>
                <p>
                    <uc:CommunityCandidateImage ID="ucCommunityCandidateImage" runat="server" />
                </p>
            </div>
        </asp:PlaceHolder>

    </div>
</div>

<asp:Panel id="pnlRecentEmployersMessage" CssClass="indented_field field" runat="server">
    <div class="control">
        <p>The names of your current employer(s) and your most recent previous employer will be hidden. 
        Be careful not to include your employers' names in your resume's job descriptions.</p>
    </div>
</asp:Panel>

<asp:PlaceHolder ID="phFieldsetClosingTag" runat="server">
        </fieldset>
    </span>
</asp:PlaceHolder>

<script type="text/javascript">

function SetEmployerPrivacyCheckBoxesEnabled()
{
    $('<%= chkName.ClientID %>').disabled = !$('<%= chkAnonResume.ClientID %>').checked;
    $('<%= chkPhone.ClientID %>').disabled = !$('<%= chkAnonResume.ClientID %>').checked;
    <% if (phShowPhotoOption.Visible) { %>$('<%= chkPhoto.ClientID %>').disabled = !$('<%= chkAnonResume.ClientID %>').checked;<% } %>
    $('<%= chkRecentEmployers.ClientID %>').disabled = !$('<%= chkAnonResume.ClientID %>').checked;
    <% if (phCommunity.Visible) { %>$('<%=chkCommunity.ClientID%>').disabled = !$('<%=chkAnonResume.ClientID%>').checked;<% } %>
}

function SetEmployerPrivacyCheckBoxesValues()
{
    if ($('<%= chkAnonResume.ClientID %>').checked) {
        if (!$('<%= chkName.ClientID %>').checked
            && !$('<%= chkPhone.ClientID %>').checked
            <% if (phShowPhotoOption.Visible) { %>&& !$('<%= chkPhoto.ClientID %>').checked<% } %>
            <% if (phCommunity.Visible) { %>&& !$('<%=chkCommunity.ClientID%>').checked<% } %>
            && !$('<%= chkRecentEmployers.ClientID %>').checked) {
                $('<%= chkName.ClientID %>').checked = true;
                $('<%= chkPhone.ClientID %>').checked = true;
                <% if (phShowPhotoOption.Visible) { %>$('<%= chkPhoto.ClientID %>').checked = true;<% } %>
                $('<%= chkRecentEmployers.ClientID %>').checked = true;
                <% if (phCommunity.Visible) { %>$('<%= chkCommunity.ClientID %>').checked = true;<% } %>
        }
    }
}

function RestoreEmployerPrivacyDefaults()
{
    $('<%= chkAnonResume.ClientID %>').checked = true;
    SetEmployerPrivacyCheckBoxesEnabled();
    $('<%= chkName.ClientID %>').checked = true;
    $('<%= chkPhone.ClientID %>').checked = true;
    <% if (phShowPhotoOption.Visible) { %>$('<%= chkPhoto.ClientID %>').checked = true;<% } %>
    $('<%= chkRecentEmployers.ClientID %>').checked = true;
    <% if (phCommunity.Visible) { %>$('<%= chkCommunity.ClientID %>').checked = true;<% } %>
    
    RestoreWorkStatusDefault();
}

function EmployerPrivacyHasChanged()
{
    if (WorkStatusSettingsHaveChanged())
    {
        return true;
    }
    else
    {
        var now = CurrentEmployerPrivacyValues();
        
        for (var i = 0; i < now.length; i++)
        {
            if (now[i] != originalEmployerPrivacyValues[i])
            {
                return true;
            }
        }
        
        return false;
    }
}

var originalEmployerPrivacyValues = [
    <%= chkAnonResume.Checked.ToString().ToLower() %>,
    <%= chkName.Checked.ToString().ToLower() %>,
    <%= chkPhone.Checked.ToString().ToLower() %>,
    <%= chkPhoto.Checked.ToString().ToLower() %>,
    <%= chkRecentEmployers.Checked.ToString().ToLower() %>,
    <%= chkCommunity.Checked.ToString().ToLower() %>
];

function CurrentEmployerPrivacyValues()
{
    return [
        $('<%= chkAnonResume.ClientID %>').checked,
        $('<%= chkName.ClientID %>').checked,
        $('<%= chkPhone.ClientID %>').checked,
        $('<%= chkPhoto.ClientID %>').checked,
        $('<%= chkRecentEmployers.ClientID %>').checked,
        $('<%= chkCommunity.ClientID %>').checked
    ];
}

function SetRecentEmployersMessageVisibility()
{
    if ($('<%= chkAnonResume.ClientID %>').checked && !$('<%= chkRecentEmployers.ClientID %>').checked)
        $('<%= pnlRecentEmployersMessage.ClientID %>').style['display'] = 'block';
    else
        $('<%= pnlRecentEmployersMessage.ClientID %>').style['display'] = 'none';
}
</script>
