<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<bool>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>

<% var allowExternalLinks = Model; %>

<ol class="terms">
    <li><b>Ownership</b>
        <ol>
            <li>
            <%if (allowExternalLinks)
              {%>
                The LinkMe website is owned and operated by LinkMe Pty Limited ABN 95 111 132 953 (<b>we</b>, <b>us</b>, <b>LinkMe</b> and other similar expressions) under the domain name <a href="http://www.linkme.com.au">www.linkme.com.au</a> or any of its related sites, including but not limited to <a href="http://www.linkme.com">www.linkme.com</a> and <a href="http://www.linkme.co.nz">www.linkme.co.nz</a> (together and separately the <b>Site</b>).
                <%}
                else
                {%>
                The LinkMe website is owned and operated by LinkMe Pty Limited ABN 95 111 132 953 (<b>we</b>, <b>us</b>, <b>LinkMe</b> and other similar expressions) under the domain name www.linkme.com.au or any of its related sites, including but not limited to www.linkme.com and www.linkme.co.nz (together and separately the <b>Site</b>).
                <%} %>
            </li>
        </ol>
    </li>
    <li><b>Acceptance</b>
        <ol>
            <li>
                These are the terms of use for individuals who have registered as a Member of the Site (<b>Member Terms of Use</b>).  Before registering as a Member of the Site, it is important that you read, understand and accept these Member Terms of Use. For the purposes of these Member Terms of Use, <b>Member</b> or <b>you</b> means a person either in the workforce or seeking work to establish a career network, source potential employment opportunities, or develop their own network of contacts and manage their careers. 
            </li>
            <li>
                By accepting the Member Terms of Use you also accept the <% if (allowExternalLinks)
                                                                               {%><%= Html.RouteRefLink("General Terms of Use", SupportRoutes.Terms)%><%}
                                                                               else
                                                                               {%><%= Html.RouteRefLink("General Terms of Use", SupportRoutes.TermsDetail)%><%} %>.
            </li>
        </ol>
    </li>
    <li><b>Privacy</b>
        <ol>
            <li>
                Please review our Privacy Statement available <% if (allowExternalLinks)
                                                                               {%><%= Html.RouteRefLink("here", SupportRoutes.Privacy)%><%}
                                                                               else
                                                                               {%><%= Html.RouteRefLink("here", SupportRoutes.PrivacyDetail)%><%} %>, which forms part of these Member Terms of Use.
            </li>
        </ol>
    </li>
    <li><b>Varying these terms</b>
        <ol>
            <li>
                We may change the Member Terms of Use at any time by publishing the varied terms on the Site.  You accept that by us doing this, we have provided you with sufficient notice of the variation and agree to be bound by the most current version of the Member Terms of Use published on the Site.
            </li>
        </ol>
    </li>
    <li><b>Restricted use</b>
        <ol>
            <li>
                As a Member of the Site, you are permitted to communicate with other Members within the context of making new business contacts, find business resources, find business based information, search the Job Board for employment opportunities, and communicate with Recruiters in order to secure employment (<b>Permitted Purpose</b>).
            </li>
            <li>
                We allow you access to the Member section of the Site for your personal use only, and you must not:
                <ol>
                    <li>
                    copy or distribute any content available on the Site, without our prior written approval;
                    </li>
                    <li>
                    alter or modify any part of the Site; or
                    </li>
                    <li>
                    use the Site for any other purpose other than the Permitted Purpose (including but not limited to any commercial purposes or unlawful purpose).
                    </li>
                </ol>
            </li>
        </ol>
    </li>
    <li><b>Registration as a Member</b>
        <ol>
            <li>
                In order to access and use the Member features and facilities on the Site, you must register as a Member and create a Member account.  Registration as a Member is free.
            </li>
            <li>
                To become a Member you must complete your registration details as required on our online Member registration form located in the Member part of the Site.  You must ensure that that your registration details are true and accurate at all times and must notify us of any change to your registration details as originally supplied.  Members must be aged 18 years or over.
            </li>
            <li>
                Members must nominate a user name and password for their Member account.  Each Member must keep his or her user name and password confidential and secure.  Each Member is responsible for all activity under his or her Member account.  A Member must notify us of any unauthorised use of his or her Member account or any other breach of security.
            </li>
            <li>
                An individual may register as a Member only once.  You must not register multiple or fictitious identities. 
            </li>
        </ol>
    </li>
    <li><b>Personal Profile</b>
        <ol>
            <li>
                When you register as a Member of the Site you will be asked to create a professional profile (<b>Professional Profile</b>).  Your Professional Profile will contain personal information, including but not limited to your education history, employment history, skills and your resume (<b>Member Profile Content</b>).
            </li>
        </ol>
    </li>
    <li><b>Communication facilities</b>
        <ol>
            <li>
                As a Member, you are able to utilise the Site's communication facilities which allow you to communicate with other Members and Recruiters electronically and directly (<b>Communication Facilities</b>).
            </li>
            <li>
                When using the Communication Facilities, any information, image, text or other material of any kind whatsoever (<b>Material</b>) that you post, email, transmit, or otherwise make available on the Site must comply with these Member Terms of Use.
            </li>
            <li>
                We may, but are under no obligation to, monitor or review the Material on the Site or our Communication Facilities and may edit, refuse to post or to remove any materials (in whole or in part) that in our sole discretion is in any way objectionable or in violation of any applicable law or these Member Terms of Use.
            </li>
            <li>
                Responsibility for the Material posted, emailed, transmitted, or otherwise made available on the Site rests solely with the Member on whose Member account it is made available.  Where that Material contains opinions or judgements of third parties, we do not purport to endorse those opinions or judgements, nor the accuracy or reliability of them.
            </li>
            <li>
                We make no representation nor give any guarantee about:
                <ol>
                    <li>
                    the identity of any other user with whom you may interact in the course of using the Communication Facilities; or
                    </li>
                    <li>
                    the authenticity of any data which any other user may submit for publication on the Site.; or
                    </li>
                    <li>
                    the suitability or accuracy of matching of any Member for a particular purpose.
                    </li>
                </ol>
            </li>
        </ol>
    </li>
    <li><b>Intellectual Property Rights</b>
        <ol>
            <li>
                You retain ownership in your Member Profile Content and the Material that you post, email, transmit, or otherwise make available on the Site.  However, by making available your Member Profile Content and Material on the Site, you grant to us a non-exclusive, royalty-free, unrestricted, perpetual right to use (including but not limited to the right to edit and reproduce), or otherwise exploit your Member Profile Content and Material in all media throughout the world.
            </li>
            <li>
                The rights granted in clause 9.1 include the right to exploit all proprietary rights in the Member Profile Content and the Material including but not limited to rights under copyright, trade mark, service mark or patent laws under any jurisdiction worldwide. 
            </li>
            <li>
                You represent and warrant that you have the right to grant the licence set out in clause 9.1 and that posting your content on or through the Site does not violate any privacy laws or any other rights of any other person.
            </li>
            <li>
                We may reformat, excerpt, or translate any Material or other content submitted by you.  All the information that you submit for publication or that you email to others using the Site is your sole responsibility.  We are not liable for any errors or omissions in any information which we hold on your behalf or on behalf of other users or which passes through the Site during the course of user interaction.
            </li>
        </ol>
    </li>
    <li><b>Future marketing activities</b>
        <ol>
            <li>
                You consent to us sending you future electronic messages including but not limited to SMS, MMS, and email regarding any promotional, marketing and publicity activities that we may offer.
            </li>
            <li>
                You consent to our selected third party partners sending you future electronic messages including but not limited to SMS, MMS, and email regarding any promotional, marketing and publicity activities that they may offer.
            </li>
            <li>
                You have the right to unsubscribe from receiving any further electronic messages of the kind described in clause 10.1 and 10.2 by:
                <ol>
                    <li>
                    <% if (allowExternalLinks) {%>
                    emailing us <%=Html.RouteRefLink("here", SupportRoutes.ContactUs)%>; or 
                    <% } %>
                    </li>
                    <li>
                    by following the unsubscribe directions in the electronic message; or
                    </li>
                    <li>
                    by updating your notification settings in account settings; or
                    </li>
                    <li>
                    by contacting the third party directly (in the case of electronic messages from third party partners).
                    </li>
                </ol>
            </li>
        </ol>
    </li>
    <li><b>Member account suspension/termination</b>
        <ol>
            <li>
                We may immediately suspend or terminate your Member account and your access to your Member account if:
                <ol>
                    <li>
                    we reasonably determine that you are in breach of these Member Terms of Use or the General Terms of Use;
                    </li>
                    <li>
                    there are any faults, service outages or other technical problems that inhibit our ability to grant you access to your Member account;
                    </li>
                    <li>
                    a request to suspend your access is received from a law enforcement agency or government body; or
                    </li>
                    <li>
                    if you do not access your Member account for a period of 365 consecutive days.
                    </li>
                </ol>
            </li>
        </ol>
    </li>
    <li><b>Prohibited conduct</b>
        <ol>
            <li>
                In addition to the prohibited conduct detailed in the General Terms of Use, you must not do any of the following when accessing the Site as a Member, including without limitation when using the Communication Facilities:
                <ol>
                    <li>
                    use your Member account for any purpose that is unlawful or prohibited by these Member Terms of Use, including without limitation posting or transmitting any threatening, libelous, defamatory, obscene, scandalous, inflammatory, pornographic, profane or spam material;
                    </li>
                    <li>
                    mislead or deceive others through any act or omission or make a false representation about your identity, including the impersonation of a real or fictitious person or using an alternative identity or pseudonym;
                    </li>
                    <li>
                    copy, collect or save information about other users including but not limited to user details, skills, employment or education history;
                    </li>
                    <li>
                    publish advertising material of any kind or market any goods or services directly to other users or Members; 
                    </li>
                    <li>
                    stalk or harass anyone; 
                    </li>
                    <li>
                    use the details of other users for anything other than the use expressly permitted by those users or Members;
                    </li>
                    <li>
                    use the Site in a way that we reasonably determine abuses the Site including but not limited to using any of the Site's infrastructure (including the Communications Facilities) to 'spam' Members or engage in other such unsolicited mass e-mail or electronic communication techniques.
                    </li>
                    <li>
                    pass on your user name and/or password information to anyone else; or
                    </li>
                    <li>
                    allow your Member account to be accessed by anyone other than you.
                    </li>
                </ol>
            </li>
        </ol>
    </li>
    <li><b>Monitoring and enforcement</b>
        <ol>
            <li>
                We are not able to control and actively monitor content at all times.
                Despite our best efforts, you may be exposed to objectionable content and we urge Members to contact our customer service department
                <% if (allowExternalLinks) {%><%=Html.RouteRefLink("here", SupportRoutes.ContactUs)%><%}%> in order to report objectionable content.
            </li>
            <li>
                If we suspect that you are responsible for submitting objectionable content for publication on the Site, your access to the Site and your Member account may be suspended.  However, we are not obliged to take any action against those responsible for objectionable content.
            </li>
            <li>
                If we suspend your access to your Member account we are not liable for any loss or damage suffered by you or any other party. 
            </li>
        </ol>
    </li>
    <li><b>General</b>
        <ol>
            <li>
                Nothing in these Member Terms of Use will create an agency, partnership, joint venture, employee-employer or franchisor-franchisee relationship between you and us.
            </li>
            <li>
                By accepting these Member Terms of Use you also accept the General Terms of Use.  In the event of any inconsistency between a term in these Member Terms of Use and the General Terms of Use, the General Terms of Use will prevail.
            </li>
            <li>
                No failure or delay of either you or us in exercising any right, power, or privilege under these Member Terms of Use operates as a waiver or any such right, power of privilege. 
            </li>
            <li>
                If any provision of these Member Terms of Use is found by a court of competent jurisdiction to be invalid, then the provision is deemed deleted but the court should try to give effect to the parties' intentions as reflected in the provision. The other provisions of these Member Terms of Use are to remain in full force and effect.
            </li>
            <li>
                Your rights and obligations under these Member Terms of Use are personal and may not be assigned or dealt with in any way without our approval, which we may withhold in our absolute discretion.
            </li>
            <li>
                You must do everything reasonably required by us to give full effect to these Member Terms of Use.
            </li>
            <li>
                Where anything requires our consent or approval, that consent or approval may be given conditionally or withheld by us as we decide.
            </li>
            <li>
                These Member Terms of Use are governed by the laws in force in the State of New South Wales, Australia. You agree to submit to the non-exclusive jurisdiction of the courts in the State of New South Wales, Australia.
            </li>
        </ol>
    </li>
</ol>
