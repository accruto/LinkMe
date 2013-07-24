<%@ Page Language="C#" MasterPageFile="~/master/SiteMasterPage.master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Landing) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="Body" runat="server">

    <div id="top-container">
    
        <div id="job-search-form-holder">
        
            <div id="LinkMeJobSearchFormDiv">
            
<%  using (Html.RenderForm("LinkMeJobSearchForm"))
    { %>
    
                    <div class="LinkMeJobSearchFormField" id="LinkMeKeywordsField">
                        <label for="ctl00_Body_ucJobSearch_txtKeywords">Keywords</label>
                        <input name="ctl00$Body$ucJobSearch$txtKeywords" type="text" id="ctl00_Body_ucJobSearch_txtKeywords" />
                    </div>
                    <div class="LinkMeJobSearchFormField" id="LinkMeLocationField">
                        <label for="ctl00_Body_ucJobSearch_txtLocation">Location</label>
                        <input name="ctl00$Body$ucJobSearch$txtLocation" type="text" id="ctl00_Body_ucJobSearch_txtLocation" />
                    </div>
                    
                    <div id="LinkMeJobSearchSubmit">
                        <input type="submit" name="ctl00$Body$btnSearch" value="Search" id="ctl00_Body_btnSearch" />
                    </div>

                    <br style="clear:both;" />
     
<%  } %>
                
            </div>
        </div>
    </div>
    
</asp:Content>

