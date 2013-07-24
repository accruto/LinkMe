<%@ Control Language="C#" Inherits="LinkMe.Web.Views.Shared.JobAdViewCustomContent" %>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds"%>

<div id="JobAd">
    <div align="right"></div>
    <div id="JobDescription">
        <asp:PlaceHolder id="phDisplayJobAd" runat="server">
	        <div id="Ad">
	            <asp:PlaceHolder id="phCompanyLogo" runat="server">
		            <div id="brandingLogo">
		                <asp:Image id="imgCompanyLogo"  Runat="server"  />
		            </div>
		        </asp:PlaceHolder>
		        <div id="JobAdHeading">
		            <asp:PlaceHolder ID="phDirtyHTMLJobHeader" runat="server">
		                <h1><%= Model.Title %></h1>
		                <p><strong><%= Details %></strong></p>
		            </asp:PlaceHolder>
        		    
		            <asp:Table width="60%" id="tblDetails" CssClass="details" runat="server">
        		    
		                <asp:TableRow CssClass="industry">
		                    <asp:TableCell><label>Industry</label></asp:TableCell>
		                    <asp:TableCell><%= JobIndustries %></asp:TableCell>
		                </asp:TableRow>
        		        
		                <asp:TableRow CssClass="jobtype">
		                    <asp:TableCell><label>Job Type</label></asp:TableCell>
		                    <asp:TableCell><%= JobTypes %></asp:TableCell>
		                </asp:TableRow>
        		        
		                <asp:TableRow CssClass="location">
		                    <asp:TableCell><label>Location</label></asp:TableCell>
		                    <asp:TableCell><%= Model.GetLocationDisplayText() %></asp:TableCell>
		                </asp:TableRow>
        		        
	                    <asp:TableRow id="trExternalReference" CssClass="refno">
	                        <asp:TableCell><label>Ref No.</label></asp:TableCell>
	                        <asp:TableCell><%= Model.Integration.ExternalReferenceId %></asp:TableCell>
	                    </asp:TableRow>
        	            
	                    <asp:TableRow id="trCompanyName" CssClass="companyname">
	                        <asp:TableCell><label>Contact</label></asp:TableCell>
	                        <asp:TableCell><%= Model.ContactDetails.GetContactDetailsDisplayText() %></asp:TableCell>
	                    </asp:TableRow>
        	            
                        <asp:TableRow id="trAdvertiser" CssClass="advertiser">
                            <asp:TableCell><label>Advertiser</label></asp:TableCell>
                            <asp:TableCell><%= Model.ContactDetails == null ? null : Model.ContactDetails.CompanyName %></asp:TableCell>
	                    </asp:TableRow>
        	            
		                <asp:TableRow id="trPackage" CssClass="package">
	                        <asp:TableCell><label>Package</label></asp:TableCell>
	                        <asp:TableCell><%= Model.Description.Package %></asp:TableCell>
		                </asp:TableRow>
        		        
		                <asp:TableRow CssClass="posteddate">
		                    <asp:TableCell><label>Posted date</label></asp:TableCell>
		                    <asp:TableCell><%= PostedDate %> (<%= Model.GetPostedDisplayText() %>)</asp:TableCell>
		                </asp:TableRow>
        		        
		            </asp:Table>
        		    
		            <ul id="ulBulletPoints" class="bulletpoints" runat="server"><%= BulletPoints %></ul>
		        </div>
        		
		        <%-- Backwards-compatibility with all existing custom stylesheets. --%>
		        <asp:Placeholder id="phDirtyHTML" runat="server">
		            <p><%= Model.Description.Content.GetContentDisplayHtml(Context.Request.IsSecureConnection)  %></p>	
		            <asp:Placeholder id="phLocalApplicationsOnlyDirty" runat="server" visible="false">
		                <div>
			                <br />
			                Only people with Australia/NZ non-restricted visa should apply.<br />
			            </div>
		            </asp:Placeholder>
		            <br />
	                <div id="LogoFooter">&nbsp;</div>
		        </asp:Placeholder>
        	</div>
            <script language="javascript" type="text/javascript"><!--
                $(document).ready(function () {

                    //center align for customized jobad
                    if ($(".main-content #JobAd").length > 0) {
                        var margin = ($(".main-content #JobAd").parent().width() - $(".main-content #JobAd").width()) / 2;
                        $(".main-content #JobAd").css({
                            "margin-left": margin + "px",
                            "margin-right": margin + "px"
                        });
                    }

                    var h1 = $('.jobheader h1')[0];
                    if (!h1)
                        return;

                    var divMeasure = h1.insert({ after: "<h1>I</h1>" }).next('h1');     // Make 1 test line of H1.

                    var lineTotalHeight = divMeasure.getHeight();          // How tall (in real px) is 1 line of H1?
                    divMeasure.setStyle({ borderTop: "0px",
                        borderBottom: "0px",
                        paddingTop: "0px",
                        paddingBottom: "0px"
                    });
                    var lineTextHeight = divMeasure.getHeight();           // How tall (in real px) is 1 line of H1 (less 'extras')?
                    var lineAuxHeight = lineTotalHeight - lineTextHeight;  // How tall (in real px) is H1 'extras'?

                    divMeasure.remove();

                    var h1LineTextHeight = lineTextHeight;                  // Init h1's current line size.
                    var h1MinLineTextHeight = lineTextHeight * 0.66;        // Smallest allowed 

                    while (
                            ((h1.offsetHeight - lineAuxHeight) >= lineTextHeight) &&  // If current h1 is more than one line
                            ((h1LineTextHeight - 1) >= h1MinLineTextHeight)            // (and we are allowed to go smaller)
                          ) {
                        h1LineTextHeight--;                                               // Shrink h1 by 1 pixel.
                        h1.setStyle({ fontSize: (h1LineTextHeight / 1.2) + 'px' });       // (Convert real px to font px)
                    }
                });
            //-->
            </script>
        </asp:PlaceHolder>
    </div>
</div>

