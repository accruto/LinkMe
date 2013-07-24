<%@ Page Language="C#" Inherits="LinkMe.Apps.Management.Areas.Communications.Views.Employers.Newsletter" %>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta name="LMEP_OK" content="LMEP_OK" />
        <title>Your LinkMe performance and ranking: are you getting the most out of LinkMe?</title>
    </head>

    <body>
        <table width="600" border="0" cellpadding="20" cellspacing="0" style="border: 1px solid #EEEEEE;">
            <tr>
                <td>
                    <table width="560" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="10" align="left"><img src="<%= Html.ImageUrl("blank.gif") %>" width="10" height="10" /></td>
                            <td width="400" align="left">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td><img src="<%= Html.ImageUrl("linkme-jobs-logo.gif") %>" width="149" height="54" alt="LinkMe Jobs" /></td>
                                    </tr>
                                    <tr>
                                        <td align="left"><div style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #333333; padding-top: 20px;">Dear <%= Html.Encode(Model.Employer.FirstName) %>,</div></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" valign="top"><div style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #333333; padding-top: 10px;">Your organisation has provided you with access to LinkMe, Australia's leading candidate database. The statistics below are designed to help you get the most out of your subscription.</div></td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right" valign="top"><img src="<%= Html.ImageUrl("linkme-guys.gif") %>" alt="" width="127" height="189" /></td>
                        </tr>
                        <tr>
                            <td valign="top">&nbsp;</td>
                            <td colspan="3" valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td><img src="<%= Html.ImageUrl("blank.gif") %>" alt="" width="1" height="30" /></td>
                                    </tr>
                                    <tr>
                                        <td><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #000033; font-weight: bold;">Candidate numbers based on recent searches by you or your organisation</span></td>
                                    </tr>
                                    <tr>
                                        <td><img src="<%= Html.ImageUrl("blank.gif") %>" alt="" width="1" height="10" /></td>
                                    </tr>
                                </table>
                                <table id="previous" width="100%" border="0" cellpadding="0" cellspacing="5">
                                
<% if (Model.PreviousSearches.Count == 0)
   { %>                                    
                                    <tr>
                                        <td width="160" align="left" valign="top"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #336699;">No previous activity registered</span></td>
                                        <td width="50" align="center" valign="top"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #336699;"></span></td>
                                        <td width="50" align="center" valign="top"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #336699;"></span></td>
                                    </tr>
<% }
   else
   { %>
                                    <tr>
                                        <td width="160" align="left" valign="top"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #336699;">Search criteria</span></td>
                                        <td width="50" align="center" valign="top"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #336699;">Number of candidates</span></td>
                                        <td width="50" align="center" valign="top"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #336699;">New last month</span></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" nowrap="nowrap" bgcolor="#D0D0D0"><img src="<%=Html.ImageUrl("blank.gif")%>" width="1" height="1" alt="------" /></td>
                                    </tr>

 <% foreach (var search in Model.PreviousSearches)
    { %>
                                    <tr>
                                        <td width="160" align="left" valign="top"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #333333;"><%= search.Criteria %></span></td>
                                        <td width="50" align="center" valign="top"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #FF6600"><%= search.Results.ToString("N0") %></span></td>
                                        <td width="50" align="center" valign="top"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #FF6600"><%= search.NewLastMonth.ToString("N0") %></span></td>
                                    </tr>
 <% }
 } %>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">&nbsp;</td>
                            <td colspan="3" valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td><img src="<%= Html.ImageUrl("blank.gif") %>" width="1" height="30" /></td>
                                    </tr>
                                    <tr>
                                        <td><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #000033; font-weight: bold;">Sample candidate numbers</span></td>
                                    </tr>
                                    <tr>
                                        <td><img src="<%= Html.ImageUrl("blank.gif") %>" alt="" width="1" height="10" /></td>
                                    </tr>
                                </table>
                                <table id="samples" width="100%" border="0" cellpadding="0" cellspacing="5">
                                    <tr>
                                        <td width="160" align="left" valign="top" nowrap="nowrap"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #336699;">Search criteria</span></td>
                                        <td width="50" align="center" valign="top"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #336699;">Number of candidates</span></td>
                                        <td width="50" align="center" valign="top"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #336699;">New last month</span></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" nowrap="nowrap" bgcolor="#D0D0D0"><img src="<%= Html.ImageUrl("blank.gif") %>" width="1" height="1" alt="------" /></td>
                                    </tr>
                                    
<% foreach (var search in Model.SampleSearches)
   { %>
                                    <tr>
                                        <td align="left" valign="top" nowrap="nowrap"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #333333;"><%= search.Criteria %></span></td>
                                        <td align="center" valign="top"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #FF6600;"><%= search.Results.ToString("N0") %></span></td>
                                        <td align="center" valign="top"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #FF6600;"><%= search.NewLastMonth.ToString("N0") %></span></td>
                                    </tr>
<% } %>

                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">&nbsp;</td>
                            <td colspan="3" valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td><img src="<%= Html.ImageUrl("blank.gif") %>" alt="" width="1" height="30" /></td>
                                    </tr>
                                    <tr>
                                        <td><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #000033; font-weight: bold;">See how you rank</span></td>
                                    </tr>
                                    <tr>
                                        <td><img src="<%= Html.ImageUrl("blank.gif") %>" alt="" width="1" height="10" /></td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" cellpadding="0" cellspacing="5">
                                    <tr>
                                        <td width="170" valign="bottom" nowrap="nowrap"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #336699;">Your activity</span></td>
<% for (var index = 3; index >= 1; --index)
   { %>                                        
                                        <td width="45" align="center" valign="bottom" nowrap="nowrap"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #336699;"><%= DateTime.Now.AddMonths(-1 * index).ToString("MMM") %></span></td>
<% } %>                                        
                                        <td width="81" align="center" valign="bottom" nowrap="nowrap"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 70%; color: #336699;">Top usage<br />org-wide</span></td>
                                        <td width="139" align="center" valign="bottom" nowrap="nowrap"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 70%; color: #336699;">Your usage rank in<br />your organisation</span></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" align="right" nowrap="nowrap" bgcolor="#D0D0D0"><img src="<%= Html.ImageUrl("blank.gif") %>" width="1" height="1" alt="------" /></td>
                                    </tr>
<% foreach (var rank in Model.Ranks)
   { %>
                                    <tr>
                                        <td align="left" nowrap="nowrap"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #333333;"><%= Html.Encode(rank.Description) %></span></td>
    <% var index = 0;
       if (rank.PreviousMonths.Count > 2)
       { %>
                                        <td align="center" nowrap="nowrap"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #333333;"><%= rank.PreviousMonths[index++] %></span></td>
    <% }
       else
       { %>
                                        <td align="center" nowrap="nowrap"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #333333;"></span></td>
    <% }
       
       if (rank.PreviousMonths.Count > 1)
       { %>
                                        <td align="center" nowrap="nowrap"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #333333;"><%= rank.PreviousMonths[index++] %></span></td>
    <% }
       else
       { %>
                                        <td align="center" nowrap="nowrap"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #333333;"></span></td>
    <% } %>

                                        <td align="center" nowrap="nowrap"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #333333;"><%= rank.PreviousMonths[index] %></span></td>

                                        <td align="center" nowrap="nowrap"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #333333;"><%= rank.TopInOrganisation %></span></td>
                                        <td align="center" nowrap="nowrap"><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #333333;">#<%= rank.Rank %></span></td>
                                    </tr>
<% } %>
                                </table>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td><img src="<%= Html.ImageUrl("blank.gif") %>" alt="" width="1" height="30" /></td>
                                    </tr>
                                    <tr>
                                        <td><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #000033; font-weight: bold;">Improve your LinkMe performance</span></td>
                                    </tr>
                                    <tr>
                                        <td><img src="<%= Html.ImageUrl("blank.gif") %>" alt="" width="1" height="10" /></td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td><img src="<%= Html.ImageUrl("blank.gif") %>" alt="" width="1" height="10" /></td>
                                    </tr>
                                    <tr>
                                        <td><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #000033;">Your user name is: </span><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #000033; font-weight: bold;"><%= Html.Encode(Model.LoginId) %></span></td>
                                    </tr>
                                    <tr>
                                        <td><img src="<%= Html.ImageUrl("blank.gif") %>" alt="" width="1" height="10" /></td>
                                    </tr>
                                </table>
                                <table border="0" cellspacing="3" cellpadding="0">
                                    <tr>
                                        <td width="17" valign="middle" nowrap="nowrap"><img src="<%= Html.ImageUrl("action-list-bullet.gif") %>" alt="*" width="5" height="9" vspace="5" /></td>
                                        <td nowrap="nowrap"><a style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #1155EE;" href="<%= Html.TinyUrl(true, "~/employers/login", "userId", Model.LoginId) %>">Log in now</a></td>
                                    </tr>
                                    <tr>
                                        <td width="17" valign="middle" nowrap="nowrap"><img src="<%= Html.ImageUrl("action-list-bullet.gif") %>" alt="*" width="5" height="9" vspace="5" /></td>
                                        <td nowrap="nowrap"><a style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #1155EE;" href="<%= Html.TinyUrl(false, "~/search/candidates") %>">Perform a new search</a></td>
                                    </tr>
                                    <tr>
                                        <td valign="middle" nowrap="nowrap"><img src="<%= Html.ImageUrl("action-list-bullet.gif") %>" alt="*" width="5" height="9" vspace="5" /></td>
                                        <td nowrap="nowrap"><a style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #1155EE;" href="<%= Html.TinyUrl(true, "~/search/candidates") %>">Create a new candidate alert</a></td>
                                    </tr>
                                    <tr>
                                        <td valign="middle" nowrap="nowrap"><img src="<%= Html.ImageUrl("action-list-bullet.gif") %>" alt="*" width="5" height="9" vspace="5" /></td>
                                        <td nowrap="nowrap"><a style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #1155EE;" href="<%= Html.TinyUrl(true, "~/search/candidates") %>">Create a new saved search</a></td>
                                    </tr>
                                    <tr>
                                        <td valign="middle" nowrap="nowrap"><img src="<%= Html.ImageUrl("action-list-bullet.gif") %>" alt="*" width="5" height="9" vspace="5" /></td>
                                        <td nowrap="nowrap"><a style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; color: #1155EE;" href="<%= Html.TinyUrl(true, "~/employers/jobads/jobad") %>">Post a job ad across the LinkMe network</a></td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td><img src="<%= Html.ImageUrl("blank.gif") %>" alt="" width="1" height="30" /></td>
                                    </tr>
                                    <tr>
                                        <td><img src="<%= Html.ImageUrl("tagline.gif") %>" width="303" height="20" align="right" alt="Great candidates at your fingertips." /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
			                <td colspan="3" align="center" valign="top" alt="" style="padding: 20px">
			                    <div style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 75%; padding-bottom: 12px">
			                        <% Html.RenderPartial("Unsubscribe", Model); %>
                                </div>
			                </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <img src="<%= Html.TrackingPixelUrl(Model.ContextId) %>" width="1" height="1" />
    </body>
</html>
