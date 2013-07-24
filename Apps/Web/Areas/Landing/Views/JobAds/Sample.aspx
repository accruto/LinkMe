<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Landing.Routes"%>

<asp:Content ContentPlaceHolderID="Body" runat="server">

    <div id="LinkMeJobSearchFormDiv">

	    <form id="LinkMeJobSearchForm" method="POST" action="<%= JobAdsRoutes.Search.GenerateUrl() %>">

		    <div class="LinkMeJobSearchFormField" id="LinkMeKeywordsField">
			    <label for="LinkMeKeywords">Keywords</label>
			    <input type="text" id="LinkMeKeywords" name="LinkMeKeywords" />
		    </div>
		    
		    <div class="LinkMeJobSearchFormField" id="LinkMeLocationField">
			    <label for="LinkMeLocation">Location</label>
			    <input type="text" id="LinkMeLocation" name="LinkMeLocation" />
		    </div>
		    
		    <input type="submit" id="LinkMeJobSearchSubmit" name="LinkMeJobSearchSubmit" value="Search" />
		    <br style="clear: both;" />

	    </form>
	    
    </div>
	    
</asp:Content>
