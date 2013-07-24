<%@ Page Language="C#" Inherits="LinkMe.Web.Views.Errors.ServerError" %>
<%@ Import Namespace="LinkMe.Web.Areas.Errors.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Environment"%>
<%@ Import Namespace="LinkMe.Web.Areas.Errors.Models.Errors"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Content"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">

<html>
    <head>
        <title>Server error</title>
<%  if (!Model.ShowDetails)
    { %>
        <%= StyleSheets.UniversalLayout %>
        <%= StyleSheets.HeaderAndNav %>
        <%= StyleSheets.Forms %>
        <%= StyleSheets.Forms2 %>
        <%= StyleSheets.TextLinksHeadings %>
        <%= StyleSheets.SidebarAbsent %>
<%  } %>

        <style>
            <%= Styles %>
        </style>

    </head>
    <body>
<%  if (Model.ShowDetails)
    {
        if (Model.Report == null)
        { %>
        <h4>The server did not report an exception. Looks like you've reached the error page in error.</h4>
        <p>Alternatively, you might have cookies disabled, so the error report could not be stored.</p>
<%      }
        else
        { %>
        <h4>An exception was thrown on server <%= RuntimeEnvironment.MachineName %>.</h4>
        <p>
            <%= ExceptionDescription %>
        </p>
        <table width="100%">
            <tr>
                <td>Requested URL</td>
                <td><a href="<%= Model.Report.RequestUrl %>"><%= HttpUtility.HtmlEncode(Model.Report.RequestUrl)%></a></td>
            </tr>
            <tr>
                <td>Referrer URL</td>
                <td><%= GetReferrerLink() %></td>
            </tr>
            <tr>
                <td>User Agent</td>
                <td><%= Model.Report.UserAgent == null ? "(none)" : HttpUtility.HtmlEncode(Model.Report.UserAgent) %></td>
            </tr>
            <tr>
                <td>User Host Address</td>
                <td><%= Model.Report.UserHostAddress == null ? "(none)" : HttpUtility.HtmlEncode(Model.Report.UserHostAddress) %></td>
            </tr>
            <tr>
                <td>Request Type</td>
                <td><%= Model.Report.RequestType == null ? "(none)" : HttpUtility.HtmlEncode(Model.Report.RequestType) %></td>
            </tr>
            <tr>
                <td>Authenticated</td>
                <td><%= Model.Report.IsAuthenticated %></td>
            </tr>
        </table>
        <hr />
        Request cookies:
        <pre><%= HttpUtility.HtmlEncode(Model.Report.RequestCookies) %></pre>
        <hr />
        Response cookies:
        <pre><%= HttpUtility.HtmlEncode(Model.Report.ResponseCookies) %></pre>
<%      }
    }
    else
    { %>
        <div id="container">
        
            <div id="header">
                <div id="header-links-container">
                    <script type="text/javascript">
                        function loadPage(url) {
                            window.location = url;
                        }
                    </script>
                    <div id="header-links">
                        <div class="left-section">
                            <div id="logo" onclick="javascript:loadPage('<%= HomeRoutes.Home.GenerateUrl() %>');"></div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="body-container">
            
                <div id="left-sidebar">
                </div>
                
                <div id="main-body">
                    <div id="form-container">

                        <div class="page-title">
                            <h1>Server error</h1>
                        </div>
                        <div class="section-content">
                            <form id="Form" method="post" action="<%= ErrorsRoutes.ServerError.GenerateUrl() %>">
                                <strong>An error has occurred in the LinkMe website. We apologise for the inconvenience.</strong>
                                <p>The error has been logged and will be investigated.</p>
                                <p>
                                    Click the Back button to go to the previous page or return to the
                                    <a href="<%= HomeRoutes.Home.GenerateUrl() %>">home page</a>.
                                </p>
                                <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                                <span style="font-size: smaller; color:#777">
                                    <u>LinkMe staff</u><br />
                                    Enter password to view full error details:
                                    <input id="Password" name="Password" type="password" class="generic-form-input" />
                                    <input id="View" name="View" type="submit" value="View" />
                                </span>
                            </form>
                        </div>
                        
                    </div>
                </div>
                <div class="clearer"></div>
            </div>
        </div>
<%  } %>

	    <input name="txtPageIdentifier" type="text" value="ServerError" id="txtPageIdentifier" style="display: none" />
    
    </body>
</html>
