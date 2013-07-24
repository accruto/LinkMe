<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<ResourcesModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Resources"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Resources"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Views" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.RenderStyles(StyleBundles.Resources)%>
        <%= Html.StyleSheet(StyleSheets.JQueryCustom) %>
    </mvc:RegisterStyleSheets>
</asp:Content>
<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom)%>
        <%= Html.RenderScripts(ScriptBundles.Resources)%>
        <%= Html.JavaScript(JavaScripts.PlusOne) %>
        <%= Html.JavaScript(JavaScripts.SwfObject) %>
    </mvc:RegisterJavaScripts>
</asp:Content>
<asp:Content ContentPlaceHolderID="PageSubHeader" runat="server">
    <% Html.RenderPartial("Search", Model.Categories); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div id="top-section">
        <div class="slidesshow">
<%  foreach (var fa in Model.FeaturedArticles)
    {
        if (fa.FeaturedResource.FeaturedResourceType == FeaturedResourceType.Slideshow)
        { %>
                <div class="bg <%= fa.FeaturedResource.CssClass %>">
                    <div class="text">
                        <div class="nametitle">
                            <span class="name"><%= Model.Categories.GetCategoryBySubcategory(fa.Article.SubcategoryId).Name.ToUpper() %></span>
                            <span class="title" title="<%= fa.Article.Title.ToUpper() %>"><%= fa.Article.Title.ToUpper() %></span>
                        </div>
                        <div class="content"><%= fa.Article.Text %></div>
                        <a class="readmore" href="<%= fa.Article.GenerateUrl(Model.Categories) %>">Read more</a>
                    </div>
                </div>
<%      }
    } %>
            <div class="balls">
                <div class="number one"></div>
                <div class="number two"></div>
                <div class="number three"></div>
                <div class="number four"></div>
                <div class="number five"></div>
            </div>
        </div>
<%  if (Model.FeaturedVideo != null)
    { %>
        <div class="featuredvideo">
            <div class="titlebar">
                <div class="leftbar"></div>
                <div class="bg">
                    Featured video
                    <div class="icon"></div>
                </div>
                <div class="rightbar"></div>
            </div>
            <div class="bg">
                <img src="http://i.ytimg.com/vi/<%= Model.FeaturedVideo.ExternalVideoId %>/0.jpg" class="preview" />
                <a href="<%= Model.FeaturedVideo.GenerateUrl(Model.Categories) %>"><%= Model.FeaturedVideo.Title %></a>
            </div>
            <div class="bottombar"></div>
        </div>
<%  } %>
    </div>

    <div class="leftcontent">
        <% Html.RenderPartial("SideNav", Model.Categories); %>
        <% Html.RenderPartial("HelpfulDocs"); %>
    </div>

    <div class="middlepart">
<%  if (Model.FeaturedQnA != null)
    { %>
        <div class="featuredquestion">
            <div class="titlebar">
                <div class="leftbar"></div>
                <div class="bg">
                    Featured question
                    <div class="icon" url="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.Resources)) %>"></div>
                </div>
                <div class="rightbar"></div>
            </div>
            <div class="bg">
                <div class="viewed">
                    <div class="leftbar"></div>
                    <div class="number"><%= Model.FeaturedQnAViews %></div>
                    <div class="rightbar"></div>
                </div>
                <div class="commented">
                    <div class="leftbar"></div>
                    <div class="number"><%= Model.FeaturedQnAComments %></div>
                    <div class="rightbar"></div>
                </div>
                <div class="title"><%= Model.FeaturedQnA.Title %></div>
                <div class="divider"></div>
                <div class="answeredby"></div>
                <div class="content"><%= Model.FeaturedQnA.Text %></div>
                <a class="readfull" href="<%= Model.FeaturedQnA.GenerateUrl(Model.Categories) %>">Read full</a>
            </div>
            <div class="bottombar"></div>
        </div>
<%  } %>
        <div class="newarticles">
            <div class="titlebar">
                <div class="leftbar"></div>
                <div class="bg">
                    New articles
                    <div class="icon"></div>
                </div>
                <div class="rightbar"></div>
            </div>
<%  foreach (var a in Model.FeaturedArticles)
    {
        if (a.FeaturedResource.FeaturedResourceType == FeaturedResourceType.New)
        { %>
            <div class="bg <%= a.FeaturedResource.CssClass %>">
                <div class="title" title="<%= a.Article.Title %>"><%= a.Article.Title %></div>
                <div class="content"><%= a.Article.Text %></div>
                <a class="readfull" href="<%= a.Article.GenerateUrl(Model.Categories) %>">Read full</a>
            </div>
<%      }
    } %>
        </div>
        <% Html.RenderPartial("Overlays", Model.Categories); %>
    </div>
    <div class="rightpart">
<%  if (Model.ActivePoll.Poll != null)
    { %>
        <div class="vote">
            <div class="titlebar">
                <div class="leftbar"></div>
                <div class="bg">
                    This week's hot topic
                    <div class="icon"></div>
                </div>
                <div class="rightbar"></div>
            </div>
			<div class="bg">
				<% Html.RenderPartial("ActivePoll", Model.ActivePoll); %>
			</div>
			<div class="bottombar"></div>
        </div>
<%  } %>
        <div class="RSR"><div class="bg"></div></div>
        <% Html.RenderPartial("ExposeYourself"); %>
    </div>
</asp:Content>