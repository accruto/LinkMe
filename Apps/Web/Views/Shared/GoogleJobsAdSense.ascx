<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdSearchCriteria>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Converters"%>
<%@ Import Namespace="LinkMe.Query.Search.JobAds"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models.Converters"%>

<%  var query = "";
    if (Model != null) {
        query = new AdSenseQueryGenerator(new JobAdSearchCriteriaAdSenseConverter()).GenerateAdSenseQuery(Model);
    } %>
<div id="gainlist"></div>
<div id="gainpagination"></div>
<div id="gainemptylist"></div>
<script src="https://www.google.com/adsense/search/ads.js" type="text/javascript"></script> 
<script type="text/javascript" charset="utf-8">
    var pageOptions = {
        'pubId': 'pub-4806942146566841',
        'query': '<%= query %>',
        'hl' : 'en'
    };

    var adblock1 = {
        'container' : 'gainlist',
        'number' : '2',
        'width' : '695px',
        'lines' : '2',
        'fontFamily' : 'arial',
        'fontSizeTitle' : '14px'
    };

    var adblock2 = {
        'container' : 'gainpagination',
        'number' : '3',
        'width' : '695px',
        'lines' : '2',
        'fontFamily' : 'arial',
        'fontSizeTitle' : '14px'
    };

    var adblock3 = {
        'container': 'gainemptylist',
        'number': '4',
        'width': '695px',
        'lines': '2',
        'fontFamily': 'arial',
        'fontSizeTitle': '14px'
    };
</script>

