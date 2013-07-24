<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/aes/img/AESHomepagelogo.jpg") %>" alt="AES" />
    </div>
    <div id="main-body-text">
        <h1>Upload your resume, jobs come to you</h1>
        <p>
            AES Indigenous careers connects you with 1000's of employers and recruiters across Australia. 
        </p>
        <p>
          Build your career profile today to be found for your next big opportunity.
          Develop networks and join groups to connect with other PARTNER members.
        </p>
        <p>
          You control the visibility of your profile.
        </p>
        <p>
          It only takes 1 minute to join and it's free
        </p>
    </div>
</div>