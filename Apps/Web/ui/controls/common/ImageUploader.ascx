<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ImageUploader.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.ImageUploader" %>
<%@ Import Namespace="LinkMe.Framework.Utility"%>

<div id="new-image" class="file_field field" style="<%= HideIf(phExistingImage.Visible) %>">
    <label> <%= TextUtil.ToSentenceCase(ImageDescription) %> </label>
    <div class="file_control control">
        <asp:FileUpload ID="fileAddLogo" CssClass="file" runat="server" />
    </div>
    <div class="helptext">Upload new <%= ImageDescription %></div>
</div>

<asp:PlaceHolder runat="server" ID="phExistingImage">
    <div id="existing-image" class="image_field field">
        <label><%= TextUtil.ToSentenceCase(ImageDescription) %></label>
        <div class="image_control control">
            <asp:Image id="imgExisting" CssClass="image" runat="server" />
            <a href="javascript:void(0)" id="remove-image">Remove</a>
        </div>
        <asp:HiddenField ID="ImageRemoved" runat="server" />
    </div>
</asp:PlaceHolder>

<script type="text/javascript">
    if ($('remove-image')) {
        $('remove-image').observe('click', function() { 
            $('existing-image').hide();
            $('<%= ImageRemoved.ClientID %>').value = 'removed';
            $('new-image').show();
        });
    }
</script>