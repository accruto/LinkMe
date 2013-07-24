<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<CheckBoxValue>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models"%>

<%= Html.CheckBox("authoriseCreditCard", Model.IsChecked, new { @class = "checkbox" })%>
<label for="authoriseCreditCard">I authorise this charge on this credit card</label>
