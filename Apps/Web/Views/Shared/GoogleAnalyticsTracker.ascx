<%@ Control Language="c#" AutoEventWireup="False" Inherits="LinkMe.Web.Views.Shared.GoogleAnalyticsTracker" %>

<% if (TrackersEnabled) { %>
    <!-- Start Google Analytics Tracker (control) -->
    <script type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', '<%= UAcct %>']);
        _gaq.push(['_trackPageview']);

        (function() {
            var ga = document.createElement('script');
            ga.type = 'text/javascript';
            ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0];
            s.parentNode.insertBefore(ga, s);
        })();
    </script>
    <!-- End Google Analytics Tracker (control) -->
<% } %>
