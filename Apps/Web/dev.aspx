<%@ Page Language="C#" AutoEventWireup="false" ValidateRequest="false" CodeBehind="dev.aspx.cs" Inherits="LinkMe.Web.dev" MasterPageFile="~/master/BlankMasterPage.master" %>
<%@ Import namespace="LinkMe.Web"%>

<asp:Content ContentPlaceHolderID="Content" runat="server">

    <html>
	    <head>
	        <title>Developer tools - LinkMe</title>
        </head>
        <body>
            <input name="txtPageIdentifier" type="text" value="<%= typeof(dev).FullName %>" id="txtPageIdentifier" style="display: none" />

            <form id="frmDev" runat="server">
                <asp:PlaceHolder id="phAuthRequired" runat="server" Visible="false">
                    Password?
		            <asp:TextBox id="txtPassword" runat="server" class="generic-form-input" textmode="Password" TabIndex="0" />
		            &nbsp;&nbsp;<asp:Button id="btnViewError" runat="server" Text="View" TabIndex="1" />&nbsp;&nbsp;
	                <span style="color: Red">
		                <asp:Label id="lblResponse" runat="server" />
			        </span>
                </asp:PlaceHolder>

                <asp:PlaceHolder id="phAuthenticated" runat="server" Visible="false">
	                <style type="text/css">
	                    table#configTable
	                    {
                            border: solid black 2px;
                            width: 100%;
                            table-layout: fixed;
   	                    }
               	        
   	                    table#configTable td
   	                    {
                            border: solid black 1px;
                            overflow: hidden;
                        }

                        .configName
                        {
                            width: 30%;
                        }
                        
                        .configSource
                        {
                            width: 20%;
                        }
                        
                        .configOverridden
                        {
                            background: LightSkyBlue;
                        }

                        .configPassword
                        {
                            background: Pink;
                        }
	                </style>

                    <span>
                        <strong>Developer tools:</strong>&nbsp;&nbsp;
                        <a href="<%= typeof(dev).Name + ".aspx?" + ActionParam + "=" + ActionInfo %>">server info</a>
                        |
                        <a href="<%= typeof(dev).Name + ".aspx?" + ActionParam + "=" + ActionConfig%>">config settings</a>
                        |
                        <a href="<%= typeof(dev).Name + ".aspx?" + ActionParam + "=" + ActionError %>">throw exception</a>
                        |
                        <a href="<%= typeof(dev).Name + ".aspx?" + ActionParam + "=" + ActionShutdown %>">shut down ASP.NET</a>
                        <br /><br />
                        <%= CommandOutput %>
                    </span>
                </asp:PlaceHolder>
            </form>
        </body>
    </html>

</asp:Content>