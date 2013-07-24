<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeaturedPartnersAds.ascx.cs" Inherits="Linkme.Web.UI.controls.common.FeaturedPartnersAds" %>

<% if (DisplayAds)
   { %>
    <div class="tile second_centre-tile centre-tile featured-partners-tile">
        <div class="home-editable-section-title">
            <h1>Featured Partners</h1>
        </div>
        <div class="home-editable-section-content">
            <div style="margin-top: 2px; text-align: center;">
                <a href="http://www.ahri.com.au/" style=""><img id="imgAd1" src="~/ui/images/tiles/ahri_78x36.png" alt="Australian Human Resource Institute" title="" runat="server" /></a>
            </div>
            <div style="float: left; clear: left; width: 262px; padding-left: 46px;">
                <a href="http://ahricareers.linkme.com.au/" style="float: left; margin-right: 14px;"><img id="imgAd4" src="~/ui/images/tiles/hrcareers_78x36.png" alt="HRcareers" title="" runat="server" /></a>
                <a href="http://www.rcsa.com.au/" style="float: left; margin-right: 14px;"><img id="imgAd5" src="~/ui/images/tiles/rcsa-affiliate_78x36.png" alt="LinkMe is an RSCA affiliate" title="" runat="server" /></a>
            </div>
        </div>
    </div>
<% } %>

<%-- 
    TODO: Substitute the static tiles above for instances of AdRotator.
    <AdRotator id="adrSpot1" runat="server" />
--%>