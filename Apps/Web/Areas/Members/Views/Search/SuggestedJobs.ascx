<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<IList<MemberJobAdView>>" %>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Domain.Users.Members.JobAds"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Industries"%>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Web.UI.Registered.Networkers"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Navigation"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Unity"%>
<%@ Import Namespace="LinkMe.Domain.Location.Queries"%>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Domain.Location"%>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Search"%>

<div class="suggestedjobs collapsed">
    <div class="titlebar">
        <div class="leftbar"></div>
        <div class="bg">
            <div class="title">Suggested jobs&nbsp;&nbsp;&nbsp;&nbsp;▼</div>
        </div>
        <div class="rightbar"></div>
    </div>
    <div class="content">
<%  if (Model.Count == 0)
    { %>
            <span class="text">Unfortunately we are unable to match any suitable jobs to your LinkMe profile today.</span>
            <span class="text">The more information you have on your profile, the easier it is for us to find suitable jobs for you. For example, you should ensure you have uploaded or created a detailed resume and have filled in details such as desired job title, recent job titles, salary range, skills and location.</span>
            <span class="text"><%= Html.RouteRefLink("Update your profile now", ProfilesRoutes.Profile) %>.</span>
<%  }
    else
    {
        for (var i = 0; i < 5 && i < Model.Count; i++)
        {
            var job = Model[i]; %>
            <div class="row">
                <div class="hover">
                    <div class="icon types <%= job.Description.JobTypes %>"></div>
                    <div class="titlearea">
                        <a href="<%= job.GenerateJobAdUrl() %>" class="title" title="<%= job.Title %>"><%= job.Title %></a>
                        <div class="company"><%= job.ContactDetails.GetContactDetailsDisplayText() %></div>
                    </div>
                    <div class="location" title="<%= job.Description.Location %>"><%= job.Description.Location%></div>
<%          var salary = job.Description.Salary; %>
                    <div class="salary"><%= salary != null && !salary.IsEmpty && !salary.IsZero ? salary.GetDisplayText() : "" %></div>
                    <div class="date"><%= job.CreatedTime.GetDateAgoText()%></div>
                    <div class="button expand"></div>
                    <div class="details collapsed">
                        <div class="summary">
                            <div class="title">Summary:</div>
<%          if (!job.Description.BulletPoints.IsNullOrEmpty())
            { %>
                            <ul class="bulletpoints">
<%              foreach (var bp in job.Description.BulletPoints)
                { %>
                                <li><div class="point"></div><%= bp%></li>
<%              } %>
                            </ul>
<%          } %>
                        </div>
                        <div class="industry">
                            <div class="title">Industry:</div>
                            <div class="desc"><%= job.Description.Industries == null ? "" : string.Join(", ", (from ind in job.Description.Industries select ind.Name).ToArray())%></div>
                        </div>
                        <div class="description"><%= job.Description.Content.GetContentDisplayText() %></div>
                    </div>
                </div>
            </div>
<%      } %>
        <div class="buttonrow">
            <a class="button seemoresuggestedjobs" href="<%= Html.RouteRefUrl(JobAdsRoutes.Suggested) %>"></a>
        </div>
<%  } %>
    </div>
</div>