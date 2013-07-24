<%@ Control Language="C#" Inherits="LinkMe.Web.Views.Shared.AcceptTerms" %>

<%= Html.CheckBox("AcceptTerms", Model.IsChecked, new {@class = "checkbox"} ) %>
<label for="AcceptTerms">I accept the <%= GetPopupHtml() %></label>
