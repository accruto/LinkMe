<%@ Control Language="C#" Inherits="LinkMe.Apps.Management.Areas.Communications.Views.Members.Html.LeftSection" %>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications"%>

<table cellspacing="0" cellpadding="0" border="0" width="346">
    <tbody>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="border-style: solid none solid solid; border-color: #E8E8E8 black #CCCCCC #E8E8E8; border-width: 1px 0 1px 1px;">
                    <tbody>
                        <tr>
                            <td bgcolor="#F8F8F8" background="<%= Html.ImageUrl("column_section_titlebar_bg.png") %>"{0}" align="left" style="padding: 4px 6px 4px 5px; color: #000; font-family: Arial,Helvetica,sans-serif; font-weight: bold; font-size: 13pt;">

<% if (!string.IsNullOrEmpty(Model.HeadingLink) && !string.IsNullOrEmpty(Model.HeadingLinkText))
   { %>
                                <div style="font-family: Verdana,Arial,Helvetica,sans-serif; font-size: 8.5pt; font-weight: normal; float: right; padding-top: 2px;">
                                    <a href="<%= Model.HeadingLink %>"><%= Model.HeadingLinkText%></a>
                                </div>
<% } %>

                                <%= Model.Heading %>
                                
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding: 9px 9px 20px 6px; font-size: 8.5pt;">
            
                <% Html.RenderPartial(Model.View, Model.ViewModel); %>

            </td>
        </tr>
        
    </tbody>
</table>
