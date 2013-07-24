<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Employers.Views.Shared.LinkedInAuth" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing" %>
<%@ Import Namespace="LinkMe.Web.Context" %>

<script type="text/javascript">
    function onLinkedInAuth() {
        IN.API.Profile("me")
            .fields("id", "first-name", "last-name", "location", "industry", "positions")
            .result(function(profiles) {

                // Create a LinkedIn employer to send to the server.

                var profile = profiles.values[0];

                var employer = {
                    Id: profile.id,
                    FirstName: profile.firstName,
                    LastName: profile.lastName,
                    Industry: profile.industry
                };

                if (profile.location) {
                    employer.Location = profile.location.name;
                    if (profile.location.country) {
                        employer.Country = profile.location.country.code;
                    }
                }

                if (profile.positions && profile.positions.values && profile.positions.values.length > 0) {
                    for (var index = 0; index < profile.positions.values.length; ++index) {
                        var position = profile.positions.values[index];
                        if (position.isCurrent && position.company) {
                            employer.OrganisationName = position.company.name;
                            break;
                        }
                    }
                }

                $.ajax({
                    type: "POST",
                    url: "<%= LinkMe.Web.Areas.Accounts.Routes.AccountsRoutes.ApiLinkedInLogin.GenerateUrl() %>",
                    data: employer,
                    async: false,
                    success: function (data) {
                        if (data.Success) {
                            window.location = data.Status == "Authenticated" ? "<%= GetAuthenticatedUrl() %>" : "<%= GetAccountUrl() %>";
                        }
                    }});
            });
    }

    function onLinkedInLoad() {
        IN.Event.on(IN, "auth", onLinkedInAuth);
    }
</script>
    
<script type="text/javascript" src="https://platform.linkedin.com/in.js">
    api_key: ur5v6u2b8wnd
    onLoad: onLinkedInLoad
<%  if (CurrentRegisteredUser == null && !ViewContext.HttpContext.GetAnonymousUserContext().HasLoggedOut)
    { %>
    authorize: true
<%  } %>
</script>

