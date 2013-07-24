<%@ Control Language="C#" Inherits="LinkMe.Apps.Management.Areas.Communications.Views.Members.Html.JobAlerts" %>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications.Views.Members.Html"%>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications"%>

<% if (Model.Count == 0)
   { %>

        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td background="<%= Html.ImageUrl("sidebar_section_subtitle_bg.png") %>" style="padding: 6px; border-bottom: 1px solid #FFF; font-size: 8.5pt;" bgcolor="#F0F0F0">
                    You currently have no email alerts.<br />
                    <a href="<%= Html.TinyUrl(true, "~/ui/registered/networkers/CreateJobSearchEmailAlert.aspx") %>">
                        Set up job alerts
                    </a>
                    to see hot jobs as they hit the market.
                </td>
            </tr>
        </table>

<% }
   else
   { %>

        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td background="<%= Html.ImageUrl("sidebar_section_subtitle_bg.png") %>" style="padding: 6px; border-bottom: 1px solid #FFF; font-size: 8.5pt;" bgcolor="#F0F0F0">
                    You have set up the following alerts:
                </td>
            </tr>
        </table>
        
    <% for (var index = 0; index < Model.Count; ++index)
       {
           var search = Model[index];
           if (index % 2 == 0)
           { %>

                <div style="padding: 6px; border-top: 1px solid #DDD;">
                    <div>
                        <img src="<%= Html.ImageUrl("event_icon.png")%>" alt="" width="16" height="15" align="absmiddle" style="padding-right: 6px"/>
                        <a href="<%= Html.TinyUrl(true, "~/members/searches/saved") %>">
                            <%= search.FirstPartDescription() %>
                        </a>
                    </div>
                    <div style="padding-top: 2px; padding-left: 22px;">
                        <%= search.SecondPartDescription() %></div>
                </div>
            
        <% }
           else
           { %>
                <div style="padding: 6px; border-top: 1px solid #DDD; background: #FAFAFA;">
                    <div>
                        <img src="<%= Html.ImageUrl("event_icon.png")%>" alt="" width="16" height="15" align="absmiddle" style="padding-right: 6px"/>
                        <a href="<%= Html.TinyUrl(true, "~/members/searches/saved") %>">
                            <%= search.FirstPartDescription() %>
                        </a>
                    </div>
                    <div style="padding-top: 2px; padding-left: 22px;">
                        <%= search.SecondPartDescription() %>
                    </div>
                </div>
        <% }
       }
   } %>
