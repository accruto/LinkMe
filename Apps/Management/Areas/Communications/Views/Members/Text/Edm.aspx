<%@ Page Language="C#" Inherits="LinkMe.Apps.Management.Areas.Communications.Views.Members.Text.Edm" %>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications.Models.Members"%>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications"%>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications"%>

March 2012

Finding talent has never been so easy!


Hi <%= Model.Member.FirstName %>

We hope you've had a great start to 2012.

As our LinkMe candidate database continues to grow, we thought this would be a good time to remind you that you too can use our database to source talented candidates for your own team.

With over 600,000 Australian job seekers available, we have now introduced an even easier way for employers to search our database - by using 'Candidate Connect', our new LinkMe iPhone app. Read more about LinkMe, the iPhone app and our Autumn Special below.


ACCESS OVER 600,000 CANDIDATES

Search our database of over 600,000 candidates and then contact your potential employees directly. It’s as easy as that!

Search now to find your next team member at <%= GetLogoutUrl(Html, true, "~/employers") %>

AUTUMN SPECIAL: 20% OFF

Take advantage of our exclusive autumn special: Contact 5 candidates for only $144. Save $36

That’s cheaper than posting an ad on an online job board. You don’t have to wade through hundreds of resumes, and you get to speak to your potential employees immediately - you don’t have to wait!

Searching is free – you only pay if you find a potential employee who you would liketo contact.

Use coupon code "LMASD01" in the Purchase flow to redeem this offer

Share this offer. BUY NOW at <%= GetLogoutUrl(Html, true, "~/employers/products/choose", "couponCode", "LMASD01")%>


ALL INDUSTRIES ACROSS AUSTRALIA

We have thousands of candidates immediately available in professions ranging from retail staff to senior professionals, all across Australia.

Over 32,000 Engineers
Over 35,000 Receptionists
Over 63,000 Sales managers


HOW DOES LINKME WORK?

LinkMe is a revolutionary new way to match job seekers with employers who are looking for staff. Our job seekers are waiting for your call. To see how the site works, watch this short video at http://www.youtube.com/watch?v=7H8RrYPDpJo


SIGN UP AS AN EMPLOYER TODAY

Create an employer account* and search, shortlist and contact candidates for your role.

Sign up at <a href="<%= GetLogoutUrl(Html, true, "~/employers/join") %>

Search for candidates
Contact candidates instantly
Place/Employ candidates
*You will not be able to use your LinkMe candidate login

Talent at your fingertips

Candidate connect powered by linkmejobs
Available on the App Store at http://itunes.apple.com/us/app/candidate-connect/id490840013?ls=1&mt=8
find out more at <%= GetLogoutUrl(Html, false, "~/employers/candidateconnect") %>

You have received this email because you have registered at LinkMe.com.au. You can edit your settings at <%= GetLogoutLoginUrl(Html, "~/employers/login", "~/employers/settings") %> to modify the frequency of emails or to unsubscribe at <%= GetLogoutUrl(Html, false, "~/accounts/settings/unsubscribe", "userId", Model.UserId.ToString(), "category", Model.Category) %>.