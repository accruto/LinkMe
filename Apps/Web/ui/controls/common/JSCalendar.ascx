<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="JSCalendar.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.JSCalendar" %>
<script src="<%=ApplicationPath%>/js/controls/JSCalendar/scal.js" type="text/javascript"></script>
<div class="jscalendar_ascx self-clearing">
    <input type="text" readonly="readonly" id="txtDate" class="textbox" runat="server"/>
    <input type="button" id="btnToggle" class="button" runat="server" />
    <span style="position: relative; vertical-align: text-top;">
        <div id="divCalculator" class="linkme" runat="server" style="display: none;"></div>
    </span>
</div>
<script type="text/javascript">
    var options = Object.extend({
                            titleformat : 'mmmm yyyy',
                            year : <%=Date.Year%>,
                            month :  <%=Date.Month%>,
                            day: <%=Date.Day%>,
                            closebutton : 'X',
                            dayheadlength : 2,
                            weekdaystart : 0,
                            planner : true
                            });

    var divCalc = $('<%=divCalculator.ClientID %>');
    <%=InstanceId %> = new scal(divCalc, 
                                      function() { 
                                            var date = this.currentdate;                                        
                                            $('<%= txtDate.ClientID %>').value = '' + date.getFullYear() + '-' + (date.getMonth()+1) + '-' + date.getDate();
                                            if(this.element.visible())
                                                this.toggleCalendar();
                                            <%if(!String.IsNullOrEmpty(OnDateSelectedJavascriptCallback)) { %>
                                            <%=OnDateSelectedJavascriptCallback%>(this);
                                            <% } %>                                                                                
                                            this.previousDate = new Date();
                                            this.previousDate.setTime(this.currentdate.getTime());
                                      }, options);
                                      
    
    <%=InstanceId%>.previousDate = new Date();
    <%=InstanceId%>.previousDate.setTime(<%=InstanceId%>.currentdate.getTime());
    
    if(window.calendars == null) {
        window.calendars = $A();
    }
    
    window.calendars.push(<%=InstanceId%>);    
    
    Event.observe($('<%=btnToggle.ClientID %>'), 'click', 
                    function() { 
                        var btn = $('<%=btnToggle.ClientID%>');
                        var cal = $('<%=divCalculator.ClientID %>');
                        cal.setStyle({
                                     left: '0px', 
                                     top: '0px', 
                                     position : 'absolute'});
                                     
                        <%if(HideOtherCalendars) { %>
                        window.calendars.each(function(item) {
                            if(item != <%=InstanceId%> && item.element.visible())
                               item.toggleCalendar();                                
                        });
                        <% } %>
                        
                        <%=InstanceId%>.toggleCalendar();
                    });    
</script>
