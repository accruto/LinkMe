<%@ Control Language="C#" Inherits="LinkMe.Apps.Management.Areas.Communications.Views.Members.Html.RightSection" %>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications"%>

<table cellspacing="0" cellpadding="0" border="0" bgcolor="#FFFFFF" width="100%">
    <tbody>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" border="0" bgcolor="#60A0C6" width="100%" style="border-style: solid; border-color: #5C88A4 #497A99; border-width: 1px;">
                    <tbody>
                        <tr><td height="1"><img height="1" width="237" src="<%= Html.ImageUrl("sidebar_section_titlebar_top.png") %>"/></td></tr>
                        <tr>
                            <td background="<%= Html.ImageUrl("sidebar_section_titlebar_bg.jpg") %>" align="left" style="padding: 3px 5px; color: #FFF; font-size: 10pt;">

<% if (!string.IsNullOrEmpty(Model.HeadingLink) && !string.IsNullOrEmpty(Model.HeadingLinkText))
   { %>
            	                <div style="font-family: Verdana,Arial,Helvetica,sans-serif; font-size: 8.5pt; font-weight: normal; float: right; padding-top: 2px;">
                                    <a href="<%= Model.HeadingLink %>" style="color: #FFF;"><%= Model.HeadingLinkText%></a>
                                </div>
<% } %>

                                <%= Model.Heading %>
                                
                            </td>
                        </tr>
                        <tr><td height="1"><img height="1" width="237" src="<%= Html.ImageUrl("sidebar_section_titlebar_bottom.png") %>"/></td></tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td style="border-style: none solid solid; border-color: #CDD0D1; border-width: 0 1px 1px; font-size: 8.5pt;">

                <% Html.RenderPartial(Model.View, Model.ViewModel); %>
                
            </td>
        </tr>
    </tbody>
</table>

<img src="<%= Html.ImageUrl("spacer.gif") %>" alt="" width="1" height="19"/>
