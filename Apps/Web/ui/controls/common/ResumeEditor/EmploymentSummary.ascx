<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="EmploymentSummary.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.ResumeEditor.EmploymentSummary" %>

<div class="employment-summary_resume_section resume_section section">
    
    <asp:PlaceHolder ID="phEmploymentSummaryHeading" runat="server" Visible="false">
        <div class="section-title">
            <h2>Employment summary</h2>
        </div>
    </asp:PlaceHolder>
    
    <div class="section-content">
        <asp:Repeater id="rptExperience" runat="server">
	        <HeaderTemplate>
	        </HeaderTemplate>
	        <ItemTemplate>
	            <div class="employment-entry">
		            <div class="employment-daterange"> <%# GetDateRange(Container.DataItem) %> </div>
		            <div class="employment-description">
			            <div class="employment-jobtitle"> <%# GetJobTitle(Container.DataItem) %> </div>
			            <div class="employment-employer"> <%# GetEmployer(Container.DataItem) %> </div>
			        </div>
			    </div>
	        </ItemTemplate>
	        <FooterTemplate>
	        </FooterTemplate>
        </asp:Repeater>
    </div>
</div>