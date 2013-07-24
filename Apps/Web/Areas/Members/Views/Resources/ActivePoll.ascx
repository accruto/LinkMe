<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LinkMe.Web.Areas.Members.Models.Resources.PollModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Views.Resources" %>

<div id="<%= Model.Poll.Name %>VoteControl" class="VoteControl">
    <div class="VoteControlQuestion"><%= Model.Poll.Question %></div>
    <div class="divider"></div>
    <div class="VoteControlInnerBox">
        <div id="<%= Model.Poll.Name %>Questions">
            <ul id="<%= Model.Poll.Name %>QuestionList">
                <% //This is the questions, which are radio buttons with labels
                    for (var rowIndex = 0; rowIndex < Model.Poll.Answers.Count; rowIndex++)
                    { %>
                <li class='<%= (rowIndex%2 == 0) ? "VoteControlNormalRow" : "VoteControlAlternateRow" %>'>
                    <div class="radiobutton"></div>
                    <input id="<%= Model.Poll.Name %>Radio<%= rowIndex + 1 %>" type="radio"
                           name="<%= Model.Poll.Name %>Questions" class="VoteControlRadioButton" />
                    <label for="<%= Model.Poll.Name %>Radio<%= rowIndex + 1 %>">
                        <%= Model.Poll.Answers[rowIndex].Text %>
                    </label>
                    <div class="VoteControlPollBarSpacer">&nbsp;</div>
                </li>
                <% } %>
            </ul>
            <div class="divider"></div>
            <div class="buttonarea">
                Vote and view your result <a href="#" onclick='VoteControlDoVote("<%= Url.Content("~/members/resources/api/") %>", "<%= Model.Poll.Name %>");return false;' class="button vote"></a>
            </div>
        </div>
        <div id="<%= Model.Poll.Name %>AnswersWithPercent" style="display:none">
            <ul>
                <%
                    //This is the answers with percentages, including a bar underneath with a length proportional to the percentage
                    for (var index = 0; index < Model.Poll.Answers.Count; index++)
                    {
                %>
                <li class='<%= (index%2 == 0) ? "VoteControlNormalRow" : "VoteControlAlternateRow" %>'>
                    <label class="VoteControlPollLabel"><%= Model.Poll.Answers[index].Text %></label>
                    <div class="VoteControlPollBar">
                        <div class="bg"></div>
                        <div class="fg r<%= index %>" style="width: <%= Model.GetBarLength(Model.Poll.Answers[index].Id, 39) %>px"></div>
                    </div>
                    <div class="VoteControlPollPercent"><%= Model.Votes[Model.Poll.Answers[index].Id] %> Vote<%= (Model.Votes[Model.Poll.Answers[index].Id] > 1) ? "s" : "" %></div>
                </li>
                <% } %>
            </ul>
        </div>
    </div>
</div>
