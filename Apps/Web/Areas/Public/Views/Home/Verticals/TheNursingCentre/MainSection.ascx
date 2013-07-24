<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/thenursingcentre/img/imagehomepageblue.png") %>" alt="The Nursing Centre" />
    </div>
    <div id="main-body-text">
        <h1>A new direction in nursing career development and it's FREE!</h1>
        <p>The <strong>Nursing Centre Career Network</strong> is for <strong>ALL NURSES at ALL LEVELS </strong>whether you're studying, just graduated, re-entering the workforce, in career transition or building your career.</p>
        <p>Join in three easy steps to:</p>
        <ul>
            <li><strong>Manage your nursing career and resume</strong> with our extensive range of online tools.</li>
            <li><strong>Record your&nbsp;continuing professional development</strong>.</li>
            <li><strong>Connect with other nurses, colleagues and friends across Australia</strong> via online groups and networking tools.</li>
            <li><strong>Get great nursing jobs</strong> when you need them.</li>
        </ul>
        <p>Plus - why not join <strong>The Nursing Centre Discussion Group</strong> online?&nbsp; It's FREE - just click on Join Now.</p>
    </div>
</div>