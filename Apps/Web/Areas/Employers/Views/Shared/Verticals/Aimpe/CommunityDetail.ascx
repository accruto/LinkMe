<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Community>" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Affiliations.Communities"%>

<div class="community-partner_link-container">This candidate is a member of <a href="javascript:void(0);" class="community-partner_anchor" onmouseover="onMouseOverCommunity(this);" onmouseout="onMouseOutCommunity(this);"><%= Model.Name %></a></div>
<div class="community-partner_inner-container">
    <div class="community-partner_container">
        <div class="arrow"></div>
        <div class="holder">
            <div class="content">
                <div class="logo">
                    <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/aimpe/img/logo.jpg") %>" />
                </div>
            </div>
        </div>
    </div>
</div> 

