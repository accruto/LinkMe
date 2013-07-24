<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="TinyMCEInit.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.TinyMCE.TinyMCEInit" %>

<script type="text/javascript" src="<%=GetTinyMCEJavaScriptUrl() %>"></script>

<script language="javascript" type="text/javascript">

    tinyMCE.init({
        mode: "specific_textareas",
        editor_selector: "html-editable",
        theme : "advanced",
        plugins : "style,advimage,advlink,searchreplace,print,paste,table",

        theme_advanced_toolbar_location : "top",
        theme_advanced_toolbar_align : "left",
        theme_advanced_buttons1 : "bold,italic,underline,|,justifyleft,justifycenter,justifyright,justifyfull",
        theme_advanced_buttons2 : "cut,copy,paste,pastetext,|,search,|,bullist,numlist,|,link,unlink",
        theme_advanced_buttons3 : "undo,redo,|,code",

        valid_elements: "<%=GetValidElements() %>"
    });
    
</script>
