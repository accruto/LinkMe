<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<Tuple<Resource, System.Collections.Generic.IList<Category>>>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import namespace="LinkMe.Domain.Resources" %>

<% var resourceItemUrl = Model.Item1.GenerateUrl(Model.Item2).AbsoluteUri;
   var shortUrl = Model.Item1.ShortUrl; %>
<div class="google">
    <div class="g-plusone" data-size="medium" data-href="<%= resourceItemUrl %>"></div>
</div>
<div class="facebook">
    <div class="fb-like" data-href="<%= resourceItemUrl %>" data-send="false" data-layout="button_count" data-width="105" data-show-faces="false" data-font="arial"></div>
</div>
<div class="twitter">
    <a href="https://twitter.com/share" class="twitter-share-button" data-url="<%= shortUrl %>" data-counturl="<%= resourceItemUrl %>" data-text="I found this on LinkMe">Tweet</a>
</div>