<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LinkMe.Web.Areas.Employers.Models.JobAds.EditJobAdModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes" %>

<%  var hasFile = Model.JobAd.Logo != null && Model.JobAd.Logo.FileReferenceId != null; %>

<div class="field">
    <label for="Logo">Logo</label>
<%  if (hasFile)
    { %>    
    <div id="display-logo" class="control image_control">
        <img src="<%= JobAdsRoutes.Logo.GenerateUrl(new { fileId = Model.JobAd.Logo.FileReferenceId.Value }) %>" alt="logo" />
        <input id="LogoId" name="logoId" type="hidden" value="<%= Model.JobAd.Logo.FileReferenceId.Value %>" />
        <ul class="corner-inline-actions actions">
            <li><a href="javascript:void(0);" id="delete-logo" class="delete-action js-delete-logo">Remove</a></li>
        </ul>
    </div>
<%  } %>
    <div id="upload-logo" class="control" <%= hasFile ? "style=\"display:none;\"" : "" %>>
        <input id="Logo" name="logo" type="file" value="" />
    </div>
    <div id="upload-logo-help" class="helptext example_helptext" <%= hasFile ? "style=\"display:none;\"" : "" %>><%= LinkMe.Domain.Roles.JobAds.Constants.LogoMaxSize.Width %>px by <%= LinkMe.Domain.Roles.JobAds.Constants.LogoMaxSize.Height %>px maximum</div>
</div>
