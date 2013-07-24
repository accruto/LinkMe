<%@ Import namespace="LinkMe.Web.Configuration"%>
<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="AjaxPhotoUpload.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.AjaxPhotoUpload" %>

<asp:PlaceHolder id="phPhoto" runat="server">
    <div id="divUploadPhoto" class="uploadphoto_ascx">
        <%--// No spaces between tags because we're using inline-block to shrinkwrap --%>
        <div id="divPhotoContainer" class="member-photo-container" runat="server" Visible="false"
            ><div class="member-photo-container-inner"
                ><asp:Image id="imgPhoto" CssClass="member-photo" runat="server"
             /></div
        ></div>

        <asp:PlaceHolder id="phEdit" runat="server" visible="false">
            <script type="text/javascript">   
                function IsSafari()
                {
                    if (navigator && navigator.vendor)
                    {
                        if (navigator.vendor.indexOf('Apple') != -1)
                            return true;
                    }
                    return false;
                }

                function ShowUploadControls() {
                    $('divUpload').style.visibility = "hidden";
                    $('divPhotoUploadStatus').style.visibility = "hidden";
                    $('divPhotoUploadCtrl').show();
                }

                function CancelFileUpload() {
                    $('divPhotoUploadCtrl').hide();
                    $('divUpload').style.visibility = "visible";
                }

                function SubmitFileUpload(form) {   
                    var fileCtrl = $('<%= filPhotoUpload.ClientID %>');	        
                    if($F(fileCtrl) == '') {
                        alert('Please select a file.');
                        return;
                    }

                    var divCtrl = $('divPhotoUploadCtrl');
                    new Effect.Fade(divCtrl, {duration: 0.3 });
                    var loadingCtrl = $('divPhotoUploadProgress');
                    new Effect.Appear(loadingCtrl, {duration: 0.3, queue: 'end'});

                    var form = $('fileUploadForm');
                    if(IsSafari()) {              
                        $('submitFormButton').click();
                        return;
                    }

                    <%--// Need to have control name the same as in the target form for this to work. --%>
                    fileCtrl.name = '<%= TargetFormFileInputName %>';

                    form.appendChild(fileCtrl);
                    form.action = '<% =TargetUploadUrl%>';
                    form.submit();

                    var btnCtrl = $('submitFile');
                    $('js_inputHolder').appendChild(fileCtrl);
                }

                function ShowUploadResult(statusText, imgUrl) {
                    if(imgUrl)
                        $('<%= imgPhoto.ClientID%>').src = imgUrl;
                        
                    var status = $('divPhotoUploadStatus');
                    new Effect.Fade($('divPhotoUploadProgress'), {duration: 0.3 });
	                $('divUpload').style.visibility = "visible";
	                status.style.visibility = "visible";
	                if(statusText != null && statusText.length != 0) {
	                    status.update(statusText);	                
	                    new Effect.Highlight(status, {duration: 1.5, queue: 'end', startcolor: '#ffffff', endcolor: '#F7C0C0'});
	                } else {
	                    status.update('');
	                    $('<%= divRemovePhoto.ClientID %>').show();
	                }
                }

                function ShowFileName() {
                    var fileName = $F('<%= filPhotoUpload.ClientID %>');
                    var arr = fileName.split('\\');
                    $('js_selectedFileName').update(arr[arr.length - 1]);
                }
                
                function RemovePhoto() {
                    if(!confirm('Do you really want to remove your photo?'))
                        return;
                        
                    if(IsSafari()) {                        
                        $('<%= inputDelete.ClientID %>').value = '<%= DeleteParam %>';                        
                        $('submitFormButton').click();
                        return;
                    }
                    
                    var form = $('fileUploadForm');
                    form.action = '<% =TargetDeleteUrl%>';
                    form.submit();
                }
                
                function RenderRemovedPhoto() {
                    $('<%= divRemovePhoto.ClientID %>').hide();
                    $('<%= imgPhoto.ClientID%>').src = '<% =WebConstants.ProfilePhotoPlaceholderUrl.ToString() %>';
                }
            </script>

            <div id="uploadDiv" class="photo-controls">
                <div id="divUpload" class="action-list-holder">
                    <ul class="plain_inline_action-list inline_action-list action-list">
                        <li><a class="upload-photo-action" href="javascript:ShowUploadControls()">Upload</a></li>
                        <li id="divRemovePhoto" runat="server"><a class="delete-action" href="javascript:RemovePhoto()">Clear</a></li></ul>
                </div>
                <div id="divPhotoUploadCtrl" class="browse-form" style="display:none;">
                    <div id="js_inputHolder" class="photo-input-container">
                        <input id="filPhotoUpload" type="file" runat="server" onchange="ShowFileName()" class="photo-input" />
                    </div>
                    <div id="js_selectedFileName" class="no-file-selected">No file selected</div>
                    <ul class="button_action-list action-list forms_v2">
                        <li><input id="submitFile" class="upload_button button" type="button" onclick="javascript:SubmitFileUpload(this)" value="Upload" /></li>
                        <li><input id="cancelFile" class="cancel-button button" type="button" onclick="javascript:CancelFileUpload(this)" value="Cancel" /></li>
                    </ul>
                    <input id="inputDelete" value="" type="hidden" runat="server" />
                    <input id="submitFormButton" type="submit" value="Submit" style="display:none;" />                        
                </div>
                <div id="divPhotoUploadProgress" class="uploading" style="display:none;">
                    Uploading...
                </div>
                <span id="divPhotoUploadStatus" style="display:none;"><%= FileUploadErrorMessage %></span>
            </div>
            <%-- 
            This piece of code is needed to remove negative margins and hide helper text field 
            for upload photo contorl in Safari.
            --%>
            <script language="javascript" type="text/javascript">
                if (IsSafari())
                {
                    $('js_selectedFileName').hide();
                    elPhotoInput = $('<%= filPhotoUpload.ClientID %>');
                    elPhotoInput.removeClassName('photo-input');
                    $(elPhotoInput.parentNode).removeClassName('photo-input-container');
                }
            </script>
        </asp:PlaceHolder>
    </div>
</asp:PlaceHolder>

<%-- The "AJAX" file upload hack. The iframe source must be set to SOMETHING, otherwise IE brings up the stupid
    "Do you want to display non-secure items?" dialog. Also, this stuff must be OUTSIDE of the main <form>,
    because you cannot have a <form> inside another <form>. The rest of this control, of course, needs to be
    INSIDE the main <form>. So the second hack is to dynamically move the literal below to the parent page
    below its main form. --%>

<asp:PlaceHolder id="phUploadForm" runat="server">
    <%-- Hidden iframe and form for photo uploads --%>
    <div style="display:none;">
        <iframe id="UploadTarget1" style="" src="javascript:false;" name="UploadTarget1"></iframe>
        <form id="fileUploadForm" method="post" enctype="multipart/form-data" action="<%= TargetUploadUrl %>" target="UploadTarget1">
        </form>
    </div>
</asp:PlaceHolder>
