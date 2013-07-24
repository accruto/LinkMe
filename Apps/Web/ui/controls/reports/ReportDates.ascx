<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ReportDates.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Reports.ReportDates" %>
<%@ Register TagPrefix="cc" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>
<%@ Register TagPrefix="uc" TagName="Calendar" Src="~/ui/controls/common/Calendar.ascx" %>

<uc:Calendar id="ucCalendar" runat="server" />

<span class="reportdates_ascx forms_v2">
    <div class="date_field compact_field field">
        <label>Start Date</label>
        <div class="date_control control">
	        <asp:TextBox id="txtStart" class="textbox" runat="server" /><asp:ImageButton id="btnStartCalendar" runat="server" ImageUrl="~/ui/images/buttons/calendar.gif" imagealign="Top" />
	        <cc:LinkMeRequiredFieldValidator id="reqValStart" runat="server" ControlToValidate="txtStart" EnableClientScript="false" ErrorMessage="You must enter a start date." />
	    </div>
    </div>
    <div class="date_field compact_field field">
        <label>End Date</label>
        <div class="date_control control">
	        <asp:TextBox id="txtEnd" class="textbox" runat="server" /><asp:ImageButton id="btnEndCalendar" runat="server" ImageUrl="~/ui/images/buttons/calendar.gif" imagealign="Top" />
	        <cc:LinkMeRequiredFieldValidator id="reqValEnd" runat="server" ControlToValidate="txtEnd" EnableClientScript="false" ErrorMessage="You must enter an end date." />
        </div>
    </div>
</span>