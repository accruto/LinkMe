<%@ Control Language="c#" AutoEventWireup="False" Codebehind="Calendar.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.Calendar" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ OutputCache duration="3600" varyByParam="none" %> 
<SCRIPT src="<%= ApplicationPath %>/js/calendar.js" type=text/javascript></SCRIPT>
<SCRIPT src="<%= ApplicationPath %>/js/calendarHelper.js" type=text/javascript></SCRIPT>
<SCRIPT src="<%= ApplicationPath %>/js/calendarInit.js" type=text/javascript></SCRIPT>
<LINK media="screen" href="<%= ApplicationPath %>/ui/styles/calendar.css" type="text/css" rel="stylesheet">