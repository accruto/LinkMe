<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="RequestContentRemovalLink.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.RequestContentRemovalLink" %>

<a href="javascript:void(0);" class="<%= CssClass %>" id="<%= ClientID %>_lnkRequestContentRemoval"><%= Text %></a>

<script type="text/javascript">
    $('<%= ClientID %>_lnkRequestContentRemoval').observe('click', function(e) {
        <%= GetActionJs() %>
    });
</script>