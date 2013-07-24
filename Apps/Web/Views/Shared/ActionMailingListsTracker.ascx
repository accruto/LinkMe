<%@ Control Language="c#" AutoEventWireup="False" Inherits="LinkMe.Web.Views.Shared.TrackerUserControl"%>

<% if (TrackersEnabled) { %>
    <!-- begin Action Mailing Lists tracking pixel -->
    <script src="https://connect.zoomdirect.com.au/lead_third/109/OPTIONAL_INFORMATION"></script>
	<noscript><img src="https://connect.zoomdirect.com.au/track_lead/109/OPTIONAL_INFORMATION" /></noscript>
    <img width="1" height="1" src="https://connect.zoomdirect.com.au/track_lead/61/" border="0" />
    <!-- end Action Mailing Lists tracking pixel -->
<% } %>