<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="ResumeTextSections.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.ResumeEditor.ResumeTextSections" %>
<%@ Register TagPrefix="uc1" TagName="PlainTextSection" Src="PlainTextSection.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EmploymentHistory" Src="EmploymentHistory.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Education" Src="Education.ascx" %>

<a name="<%= FIELD_OBJECTIVE %>"></a>
<uc1:PlainTextSection id="ucCareerObjective" SectionName="Objective" SectionDisplayName="Career objectives" runat="server">
    Describe your overall experience in broad terms ( e.g. IT developer with 10 years experience).
    Then briefly explain your short and long term career goals.<br /><br />
    Remember - be optimistic but also realistic.<br />
</uc1:PlainTextSection>

<a name="<%= FIELD_SUMMARY %>"></a>
<uc1:PlainTextSection id="ucSummary" SectionName="Summary" runat="server">
    Provide a summary of your experience and skills.
</uc1:PlainTextSection>

<a name="<%= FIELD_EMPLOYMENT_HISTORY %>"></a>
<uc1:EmploymentHistory id="ucEmploymentHistory" runat="server" />

<a name="<%= FIELD_SKILLS %>"></a>
<uc1:PlainTextSection id="ucSkills" SectionName="Skills" runat="server">
    List your skills - be specific and make sure they are work related.<br />
</uc1:PlainTextSection>

<a name="<%= FIELD_EDUCATION %>"></a>
<uc1:Education id="ucEducationHistory" runat="server" />

<a name="<%= FIELD_COURSES %>"></a>
<uc1:PlainTextSection id="ucCourses" SectionName="Courses" runat="server">
    List relevant courses you have completed.<br />
</uc1:PlainTextSection>

<a name="<%= FIELD_AWARDS %>"></a>
<uc1:PlainTextSection id="ucAwards" SectionName="Awards" runat="server">
    List relevant professional awards. <br />
</uc1:PlainTextSection>

<a name="<%= FIELD_PROFESSIONAL %>"></a>
<uc1:PlainTextSection id="ucProfessional" SectionName="Professional" runat="server">
    List your professional certifications.<br />
</uc1:PlainTextSection>

<a name="<%= FIELD_INTERESTS %>"></a>
<uc1:PlainTextSection id="ucInterests" SectionName="Interests" runat="server">
    List relevant interests - remember companies may use this to determine if you will fit their company culture.<br />
</uc1:PlainTextSection>

<a name="<%= FIELD_CITIZENSHIP %>"></a>
<uc1:PlainTextSection id="ucCitizenship" SectionName="Citizenship" runat="server">
    List your citizenship details.<br />
</uc1:PlainTextSection>

<a name="<%= FIELD_AFFILIATION %>"></a>
<uc1:PlainTextSection id="ucAffiliation" SectionName="Affiliation" runat="server">
    List professional memberships and associations<br />
</uc1:PlainTextSection>

<a name="<%= FIELD_OTHERS %>"></a>
<uc1:PlainTextSection id="ucOther" SectionName="Other" runat="server">
    Add any extra information that you consider relevant.<br />
</uc1:PlainTextSection>

<a name="<%= FIELD_REFEREES %>"></a>
<uc1:PlainTextSection id="ucReferences" SectionName="References" runat="server">
    This should ideally include your current and most recent manager.<br />
    Remember to get their permission. Let them know the type of role you are applying for and prepare them
    for the call.<br /><br />
    (You could also say "Available upon request" if you don't feel comfortable disclosing contact information).<br />
</uc1:PlainTextSection>
