<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<bool>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>

<% var allowExternalLinks = Model; %>

<ol class="terms">
    <li><b>Ownership</b>
        <ol>
            <li>
<%  if (allowExternalLinks)
    { %>
                The LinkMe website is owned and operated by LinkMe Pty Limited ABN 95 111 132 953 (<b>we</b>, <b>us</b>, <b>LinkMe</b> and other similar expressions) under the domain name <a href="http://www.linkme.com.au">www.linkme.com.au</a> or any of its related sites, including but not limited to <a href="http://www.linkme.com">www.linkme.com</a> and <a href="http://www.linkme.co.nz">www.linkme.co.nz</a> (together and separately the <b>Site</b>).
<%  }
    else
    { %>
                The LinkMe website is owned and operated by LinkMe Pty Limited ABN 95 111 132 953 (<b>we</b>, <b>us</b>, <b>LinkMe</b> and other similar expressions) under the domain name www.linkme.com.au or any of its related sites, including but not limited to www.linkme.com and www.linkme.co.nz (together and separately the <b>Site</b>).
<%  } %>
            </li>
        </ol>
    </li>
    <li><b>Acceptance</b>
        <ol>
            <li>
                These are the terms of use that apply to each Recruiter (<b>Recruiter Terms of Use</b>).
                For the purposes of these Recruiter Terms of Use, <b>Recruiter</b> or <b>you</b> means (jointly and severally) the party named as the Recruiter in the registration details on the Site or LinkMe Customer Agreement, and either:
                <ol>
                    <li>
                        an individual recruiter or employer who has registered directly on the Site and accepted the Recruiter Terms of Use at the time of registration; or
                    </li>
                    <li>
                        an individual user granted the rights to register on the Site under the terms of a LinkMe Customer Agreement, and in doing so, agrees to be bound by these Recruiter Terms of Use, who would like to source and contact candidates for potential employment.
                    </li>
                </ol>
            </li>
            <li>
                Before registering as a Recruiter on the Site, it is important that you read, understand and accept these Recruiter Terms of Use.
            </li>
            <li>
                By accepting the Recruiter Terms of Use you also accept the <% if (allowExternalLinks) { %><%= Html.RouteRefLink("General Terms of Use", SupportRoutes.Terms) %><%  } else { %><%= Html.RouteRefLink("General Terms of Use", SupportRoutes.TermsDetail) %><% } %>.
            </li>
        </ol>
    </li>
    <li><b>Privacy</b>
        <ol>
            <li>
                Please review our Privacy Policy available <% if (allowExternalLinks) { %><%= Html.RouteRefLink("here", SupportRoutes.Privacy) %><% } else { %><%= Html.RouteRefLink("here", SupportRoutes.PrivacyDetail) %><% } %>,
                which forms part of these Recruiter Terms of Use.
            </li>
        </ol>
    </li>
    <li><b>Varying the terms</b>
        <ol>
            <li>
                We may change the Recruiter Terms of Use at any time by publishing the varied terms on the Site.
                You accept that by us doing this, we have provided you with sufficient notice of the variation and agree to be bound by the most current version of the Recruiter Terms of Use published on the Site.
            </li>
        </ol>
    </li>
    <li><b>Restricted use</b>
        <ol>
            <li>
                As a registered Recruiter on the Site, you are permitted to use the Site to post job advertisements, and source and contact candidates for potential employment strictly in accordance with these Recruiter Terms of Use (<b>Permitted Purpose</b>).
                You must not:
                <ol>
                    <li>
                        copy or distribute any content available on the Site, without our prior written approval;
                    </li>
                    <li>
                        alter or modify any part of the Site;
                    </li>
                    <li>
                        use the Site in a way that we reasonably determine abuses the Site including but not limited to using any of the Site's infrastructure (including the Communications Facilities) to 'spam' Members or engage in other such unsolicited mass e-mail or electronic communication techniques;
                    </li>
                    <li>
                        use the Site to contact Members on a general basis (i.e. for any other reason other than in respect of a <b>specific</b> employment opportunity);
                    </li>
                    <li>
                        allow anyone other than you to access your Recruiter account; or
                    </li>
                    <li>
                        use the Site for any other purpose other than the Permitted Purpose (including but not limited to any unlawful purpose).
                    </li>
                </ol>
            </li>
        </ol>
    </li>
    <li><b>Registration as a Recruiter</b>
        <ol>
            <li>
                In order to access and use the Recruiter features and facilities on the Site, you must register as a Recruiter and create a Recruiter account by completing your registration details as required.
            </li>
            <li>
                You must ensure that your registration details are true and accurate at all times and must notify us of any change to your registration details as originally supplied.
                You must be aged 18 years or over.
            </li>
            <li>
                You must nominate a user name and password for your Recruiter account.
                You must keep your user name and password confidential and secure.
                You are responsible for all activity under your Recruiter account.
                You must notify us of any unauthorised use of your Recruiter account or any other breach of security.
            </li>
            <li>
                An individual may only have one Recruiter account at any one time.
                You must not register multiple or fictitious identities.
            </li>
        </ol>
    </li>
    <li><b>Recruiter Services</b>
        <ol>
            <li>
                There are two services that Recruiters can utilise (collectively the <b>Services</b>).
                They are:
                <ol>
                    <li>
                        the ability to, amongst other things, search, view, shortlist, manage, set up email alerts, contact Members and download Members resumes (<b>Resume Database Service</b>); and 
                    </li>
                    <li>
                        the ability to, amongst other things, post job advertisements on the Site, and receive job applications from interested persons (<b>Job Board Service</b>).
                    </li>
                </ol>
            </li>
            <li>
                In accordance with the details on the Site, some components of each service are free, while others require a fee to be paid.
            </li>
            <li>
                In relation to the Resume Database Service, the Member details we provide you will be those details we have obtained from the Member.
                While we will use our best efforts to keep the details up to date, we cannot warrant that the details will be up to date or correct, and do not accept any liability if you are not able to get in contact with a Member.
            </li>
        </ol>
    </li>
    <li><b>Fees - users accessing Site under terms of LinkMe Customer Agreement</b>
        <ol>
            <li>
                If you are a user accessing the Site under the terms of a separate LinkMe Customer Agreement, then the fees payable will be set out in that agreement and <b>clauses 9, 10, 11, and 12 in these Recruiter Terms of Use do not apply.</b>
            </li>
        </ol>
    </li>
    <li><b>Fees - Resume Database Service</b>
        <ol>
            <li>
                A fee is only required to be paid when you are ready to contact a Member or download their resume.
                The fee to contact a Member or download their resume is as per the pricing details on the Site.
                The fee may be amended by us at any time.
            </li>
            <li>
                Contacting a Member involves unlocking their record to reveal personal details and their phone number.
                Unlocking also allows you to contact the Member directly by email.
            </li>
            <li>
                Once you pay us the relevant fee, you can contact that Member again as often as you like, within a 12 month period.
            </li>
        </ol>
    </li>
    <li><b>Fees - Job Board Service</b>
        <ol>
            <li>
                Credits for job applications received are required to be purchased in advance in accordance with the pricing details on the Site.
                The fee may be amended by us at any time.
            </li>
        </ol>
    </li>
    <li><b>Fees - Third party products (as applicable)</b>
        <ol>
            <li>
                If we supply you with any third party products (as agreed between us), the third party terms and conditions are incorporated by reference and form part of these Recruiter Terms of Use.
            </li>
            <li>
                You will pay us any fee (as agreed or as detailed on the Site) in consideration of the provision of the third party products.
            </li>
        </ol>
    </li>
    <li><b>Fees - General terms</b>
        <ol>
            <li>
                Any marketing material or price list published by us on the Site is not an offer from us, but merely an invitation to you to order the Services from us.
                Each order must be accepted by us before it becomes binding.
                We are not required to accept any order from you.
            </li>
            <li>
                Unless otherwise expressly stated, all prices are in Australian dollars and are exclusive of GST.
            </li>
            <li>
                Payment must be made by a MasterCard, Visa or American Express.
                All credit card/debit card holders are subject to validation checks and authorisation by the card issuer.
            </li>
            <li>
                We reserve the right to refuse credit/debit card payments in our reasonable discretion.
            </li>
            <li>
                We will process your payment on a secure site.
            </li>
            <li>
                Payments must be made in advance and are non-refundable.
            </li>
            <li>
                We will provide the Services and third party products (if applicable) with due care and skill, but we do not guarantee that they will be continuous or fault free.
            </li>
        </ol>
    </li>
    <li><b>GST</b>
        <ol>
            <li>
                All consideration provided for any taxable supply under these Recruiter Terms of Use are exclusive of GST unless the contrary is clear.
                The amount of that consideration must be increased by an additional amount equal to the GST on that taxable supply.
                You must pay it at the same time as the consideration in respect of that taxable supply becomes due or, if we have to pay (or allow credit against) the relevant GST before then, the additional amount must be paid at that earlier time.
            </li>
        </ol>
    </li>
    <li><b>Confidentiality and Privacy</b>
        <ol>
            <li>
                Each party must treat as Confidential Information all information provided by the other and must not disclose the other party's Confidential Information to any person except:
                <ol>
                    <li>
                        its employees, contractors, professional advisors and auditors on a "need to know" basis provided those persons first agree to observe the confidentiality of the information;
                    </li>
                    <li>
                        with the other party's written consent;
                    </li>
                    <li>
                        if required by law or any stock exchange on which its securities are listed; or
                    </li>
                    <li>
                        if it is in the public domain.
                    </li>
                </ol>
            </li>
            <li>
                For the purposes of this clause 14, <b>Confidential Information</b> means all confidential information, non public or proprietary information regardless of how the information is stored or delivered, exchanged between the parties before, on or after the date the date you register as a Recruiter relating to the business, relating to the business technology, customers or other affairs of the disclosing party, and includes any information in relation to Members.
                It excludes information which:
                <ol>
                    <li>
                        is in or becomes part of the public domain other than through breach of these Recruiter Terms of Use or an obligation of confidence owed to the disclosing party; or
                    </li>
                    <li>
                        the recipient can prove by all contemporaneous written documentation it was already known to it at the time of disclosure by the disclosing party, unless such knowledge arose from disclosure of information in breach of an obligation of confidentiality; or 
                    </li>
                    <li>
                        the recipient acquires from a source other than the disclosing party or any related entity or representative of the disclosing party where such a source is entitled to disclose it.
                    </li>
                </ol>
            </li>
        </ol>
    </li>
    <li><b>Communication facilities</b>
        <ol>
            <li>
                As a Recruiter, you are able to utilise the Site's communication facilities which allow you to communicate with Members electronically and directly (<b>Communication Facilities</b>).
            </li>
            <li>
                When using the Communication Facilities, any information, image, text or other material of any kind whatsoever (<b>Material</b>) that you post, email, transmit, or otherwise make available on the Site must comply with these Recruiter Terms of Use.
            </li>
            <li>
                We may, but are under no obligation to, monitor or review Material on the Site or our Communication Facilities and may edit, refuse to post or to remove Materials (in whole or in part) that in our sole discretion is in any way objectionable or in violation of any applicable law or these Recruiter Terms of Use.
            </li>
            <li>
                Responsibility for Material posted, emailed, transmitted, or otherwise made available on the Site rests solely with the Recruiter on whose Recruiter account it is made available.  Where Material contains opinions or judgements of third parties, we do not purport to endorse those opinions or judgements, nor the accuracy or reliability of them.
            </li>
            <li>
                We make no representation nor give any guarantee about:
                <ol>
                    <li>
                        the identity of any other user with whom you may interact in the course of using the Communication Facilities;
                    </li>
                    <li>
                        the authenticity of any data which any other user may submit for publication on the Site; or
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
                You retain ownership in the Material that you post, email, transmit, or otherwise make available on the Site.
                However, by making available the Material on the Site, you grant to us a non-exclusive, royalty-free, unrestricted, perpetual right to use (including but not limited to the right to edit and reproduce) or otherwise exploit the Material in all media throughout the world.
            </li>
            <li>
                The rights granted in clause 16.1 include the right to exploit all proprietary rights in the Material including but not limited to rights under copyright, trade mark, service mark or patent laws under any jurisdiction worldwide.
            </li>
            <li>
                You represent and warrant that you have the right to grant the licence set forth in clause 16.1 and that posting your content on or through the Site does not violate any privacy laws or any other rights of any other person.
            </li>
            <li>
                We may reformat, excerpt, or translate any Material or other content submitted by you.
                All the information that you submit for publication or that you email to others using the Site is your sole responsibility.
                We are not liable for any errors or omissions in any information which we hold on your behalf or on behalf of other users or which passes through the Site during the course of user interaction.
            </li>
        </ol>
    </li>
    <li><b>Prohibited conduct</b>
        <ol>
            <li>
                In addition to the prohibited conduct detailed in the General Terms of Use, you must not do any of the following when accessing the Site as a Recruiter, including without limitation when using the Communication Facilities:
                <ol>
                    <li>
                        use your Recruiter account for any purpose that is unlawful or prohibited by these Recruiter Terms of Use, including without limitation the posting or transmitting any threatening, libelous, defamatory, obscene, scandalous, inflammatory, pornographic, profane or spam material;
                    </li>
                    <li>
                        mislead or deceive others through any act or omission or make a false representation about your identity, including the impersonation of a real or fictitious person or using an alternative identity or pseudonym;
                    </li>
                    <li>
                        engage in any fraudulent conduct;
                    </li>
                    <li>
                        contact Members by any other means other than through the Resume Database Service and without first paying us the relevant fee;
                    </li>
                    <li>
                        publish advertising material of any kind or market any goods or services directly to other users or Members by any other means other than through the Job Board Service and without first paying us the relevant fee; 
                    </li>
                    <li>
                        stalk or harass anyone; 
                    </li>
                    <li>
                        use the details of other users for anything other than the use expressly permitted by those users or Members;
                    </li>
                    <li>
                        pass on your user name and/or password information to anyone else;
                    </li>
                    <li>
                        allow your Recruiter account to be accessed by anyone other than you, or, in the case of a Recruiter that is an entity, anyone other than the Authorised Person; or 
                    </li>
                    <li>
                        download (in isolation or in bulk), access, use, scrape or harvest the personal or professional details of Members (including but not limited to a Member's employment history, education history, personal contact details and resume) except as specifically permitted by us in accordance with the directions on the Site or by us in writing.
                    </li>
                </ol>
            </li>
        </ol>
    </li>
    <li><b>Monitoring and enforcement</b>
        <ol>
            <li>
                We are not able to control and actively monitor content at all times.  Despite our best efforts, you may be exposed to objectionable content and we urge Recruiters to contact our customer service department <% if (allowExternalLinks) {%><%=Html.RouteRefLink("here", SupportRoutes.ContactUs)%><%}%> in order to report objectionable content.
            </li>
            <li>
                If we suspect that you are responsible for submitting objectionable content for publication on the Site, your access to the Site and your Recruiter account may be suspended.  However, we are not obliged to take any action against those responsible for objectionable content.
            </li>
            <li>
                If we suspend your access to your Recruiter account we are not liable for any loss or damage suffered by you or any other party.
            </li>
            <li>
                We use cookies to monitor the activity on Recruiter accounts and enable us to continue to provide a high quality service. Some parts of Recruiter's accounts may not function fully if you disallow cookies. If requested by us, you must enable cookies so that we can monitor your activity. 
            </li>
		</ol>
	<li><b>Suspension</b>
		<ol>
            <li>
                We may immediately suspend your Recruiter account and your access to your Recruiter account if:
                <ol>
                    <li>
                        we reasonably determine that you are in breach of these Recruiter Terms of Use or the General Terms of Use;
                    </li>
                    <li>
                        there are any faults, service outages or other technical problems that inhibit our ability to grant you access to your Recruiter account;
                    </li>
                    <li>
                        a request to suspend your access is received from a law enforcement agency or government body; or
                    </li>
                    <li>
                        if you do not access your Recruiter account for a period of 365 consecutive days.
                    </li>
                </ol>
            </li>
            <li>
                If we suspend your Recruiter account, we will not reimburse you with any fees paid in advance prior to the date of suspension.
            </li>
        </ol>
    </li>
    <li><b>Termination</b>
        <ol>
            <li>
                We may terminate our agreement with you immediately by written notice to you, and recover any loss or damage suffered, if you:
                <ol>
                    <li>
                        become insolvent;
                    </li>
                    <li>
                        are in breach of clause 5 (Restricted Use) or 17 (Prohibited Conduct); or
                    </li>
                    <li>
                        breach a term of these Recruiter Terms of Use and that breach:
                        <ol>
                            <li>
                                is not capable of being cured; or
                            </li>
                            <li>
                                is capable of being cured and you fail to cure the breach within 14 days of being notified of the breach by us in writing.
                            </li>
                        </ol>
                    </li>
                </ol>
            </li>
            <li>
                To the extent that we have not already suspended your Recruiter account under clause 19, if our agreement with you is terminated, then from the date of termination you will not be able to access your Recruiter account and you will not be reimbursed with any fees paid in advance prior to the termination date.
            </li>
        </ol>
    </li>
    <li><b>General</b>
        <ol>
            <li>
                Nothing in these Recruiter Terms of Use will create an agency, partnership, joint venture, employee-employer or franchisor-franchisee relationship between you and us.
            </li>
            <li>
                By accepting these Recruiter Terms of Use you accept the General Terms of Use.  In the event of any inconsistency between a term in these Member Terms of Use and the General Terms of Use, the General Terms of Use will prevail.
            </li>
            <li>
                No failure or delay of either you or us in exercising any right, power, or privilege under these Recruiter Terms of Use operates as a waiver or any such right, power of privilege. 
            </li>
            <li>
                If any provision of these Recruiter Terms of Use is found by a court of competent jurisdiction to be invalid, then the provision is deemed deleted but the court should try to give effect to the parties' intentions as reflected in the provision. The other provisions of these Recruiter Terms of Use are to remain in full force and effect.
            </li>
            <li>
                Your rights and obligations under these Recruiter Terms of Use are personal and may not be assigned or dealt with in any way without our approval, which we may withhold in our absolute discretion.
            </li>
            <li>
                You must do everything reasonably required by us to give full effect to these Recruiter Terms of Use.
            </li>
            <li>
                Where anything requires our consent or approval, that consent or approval may be given conditionally or withheld by us as we decide.
            </li>
            <li>
                These Recruiter Terms of Use are governed by the laws in force in the State of New South Wales, Australia. You agree to submit to the non-exclusive jurisdiction of the courts in the State of New South Wales, Australia.
            </li>
        </ol>
    </li>
</ol>

