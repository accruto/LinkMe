<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<bool>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>

<% var allowExternalLinks = Model; %>

<ol class="terms">
    <li><b>Ownership</b>
        <ol>
        <% if (allowExternalLinks)
           {%>
            <li>Welcome to the LinkMe website which is owned and operated by LinkMe Pty Limited
                ABN 95 111 132 953 (<b>we</b>, <b>us</b>, <b>LinkMe</b> and other similar expressions) under the domain
                name <a href="http://www.linkme.com.au">www.linkme.com.au</a> or any of its related sites, including but not limited to
                <a href="http://www.linkme.com">www.linkme.com</a> and <a href="http://www.linkme.co.nz">www.linkme.co.nz</a> (together and separately the <b>Site</b>). </li>
                <% }
           else
           { %>
            <li>Welcome to the LinkMe website which is owned and operated by LinkMe Pty Limited
                ABN 95 111 132 953 (<b>we</b>, <b>us</b>, <b>LinkMe</b> and other similar expressions) under the domain
                name www.linkme.com.au or any of its related sites, including but not limited to
                www.linkme.com and www.linkme.co.nz (together and separately the <b>Site</b>). </li>
                <% } %>
        </ol>
    </li>
    <li><b>Acceptance</b>
        <ol>
            <li>These are the general terms of use for the Site (<b>General Terms of Use</b>). Before using
                any information, images, text and facilities (<b>Information</b>) available on the Site,
                it is important that you read, understand and accept these General Terms of Use.
                You acknowledge that your use of any of the Information on the Site indicates your
                acceptance of these General Terms of Use. </li>
            <li>Additional terms of use apply to any person who registers as a member on the Site
                (<b>Member Terms of Use</b>) or a recruiter on the Site (<b>Recruiter Terms of Use</b>). You will
                be prompted to accept those terms of use before registering as a member or recruiter.
            </li>
        </ol>
    </li>
    <li><b>Privacy</b>
        <ol>
            <li>You agree that we may collect, use and disclose information about you in accordance
                with our Privacy Statement. </li>
            <li>You agree that we may send emails to the email addresses which you provide to us.
                You also agree to obtain consents from and make disclosures to third parties before
                providing us with information about them (including without limitation their email
                address) in accordance with our Privacy Statement. </li>
            <li>Our Privacy Statement forms part of these General Terms of Use. Our Privacy Statement
                is available online on this Site for you to read and print out. To see our Privacy
                Statement go <% if (allowExternalLinks)
                                {%> <%= Html.RouteRefLink("here", SupportRoutes.Privacy)%> <% } 
                                else 
                                {%> <%= Html.RouteRefLink("here", SupportRoutes.PrivacyDetail)%> <% } %>. </li>
        </ol>
    </li>
    <li><b>Varying these terms</b>
        <ol>
            <li>
                We may change the General Terms of Use at any time by publishing the varied terms on the Site.
                You accept that by us doing this, we have provided you with sufficient notice of the variation and agree to be bound by the most current version of the General Terms of Use published on the Site.
            </li>
        </ol>
    </li>
    <li><b>Description of the LinkMe Service</b>
        <ol>
            <li>
                The LinkMe service, provided through the Site, is aimed at connecting members and recruiters.
                Members can search the LinkMe job board for available opportunities, or upload their resume, create a professional profile and be sought after by recruiters searching the LinkMe database.
                Similarly, recruiters can search the thousands of resumes in the LinkMe candidate database or post a job ad to the LinkMe network of job boards.
                In addition, members can use the LinkMe service to network and communicate with other members and recruiters within the context of making new business contacts, finding business resources and finding business based information.
            </li>
            <li>
                This same interface also allows employers and recruiters to search the LinkMe member database and contact potential candidates.
            </li>
        </ol>
    </li>
    <li><b>Intended use</b>
        <ol>
            <li>
                The Site is intended to be used by the following types of users:
                <ol>
                    <li>
                        individuals either in the workforce or seeking work to establish career networks, source potential employment opportunities, develop their own network of contacts and manage and advance their careers (<b>Members</b>);
                    </li>
                    <li>
                        recruiters and employers to source and contact candidates for potential employment (<b>Recruiters</b>).
                    </li>
                </ol>
            </li>
            <li>
                The LinkMe service is provided for users who are at least 18 years of age. 
            </li>
        </ol>
    </li>
    <li><b>Intellectual Property Rights</b>
        <ol>
            <li>
                The intellectual property rights (including copyright) in all information, text, graphics, icons, code (including HTML), trade marks (including the trade marks of third parties) audio, video and software on the Site (<b>Content</b>) is owned by or licensed to us.
            </li>
            <li>
                We grant you a personal, non-transferable and non-exclusive licence to use the Content on a single computer solely for the purpose of using the Site, subject to these General Terms of Use.
                We may terminate the licence at any time without cause or notice to you.
            </li>
            <li>
                Except as expressly authorised by us, you must not, in any form and by any means, without our prior approval:
                <ol>
                    <li>
                        adapt, reproduce, distribute, print, display, perform, publish or create derivative works from any part of this Site, or 
                    </li>
                    <li>
                        commercialise any Content, products or services obtained from any part of this Site.
                    </li>
                </ol>
            </li>
        </ol>
    </li>
    <li><b>Prohibited conduct</b>
        <ol>
            <li>
                You must not do any of the following when accessing the Site:
                <ol>
                    <li>
                        attempt to decipher, decompile, disassemble or reverse engineer any of the software or HTML code comprising or in any way making up a part of the Site;
                    </li>
                    <li>
                        engage in any crawling, scaping or spidering of any content on the Site;
                    </li>
                    <li>
                        engage in framing, or provide unauthorised interfaces to LinkMe applications.
                    </li>
                    <li>
                        take any action that imposes an unreasonable or disproportionately large load on the Site's infrastructure, including but not limited to "spam" or other such unsolicited mass e-mailing techniques;
                    </li>
                    <li>
                        conduct any activity which compromises or breaches another party's intellectual property rights (including without limitation, copyright or trademark rights); 
                    </li>
                    <li>
                        introduce any virus, worm, Trojan horse, malicious code or other program which may damage computers or other computer based equipment to the Site or to other users; 
                    </li>
                    <li>
                        attempt to disrupt or interfere with the delivery of the LinkMe service or the services of our partners and clients;
                    </li>
                    <li>
                        modify, delete or add to any content on the Site;
                    </li>
                    <li>
                        sell, redistribute or use information contained on the Site for a commercial purpose without our prior written consent; or
                    </li>
                    <li>
                        attempt to gain unauthorised access to any part of the Site.
                    </li>
                </ol>
            </li>
        </ol>
    </li>
    <li><b>Advertisements</b>
        <ol>
            <li>
                Responsibility for the content of advertisements appearing on the Site (including hyperlinks to the websites of advertisers) rests solely with the advertisers.
            </li>
            <li>
                We do not warrant, endorse, guarantee, or assume responsibility for any product or service advertised or offered by a third party through the services or any hyperlinked services featured in any banner or other advertising.
            </li>
            <li>
                We will not be a party to or in any way be responsible for monitoring any transaction between you and third-party providers of products or services.
                As with the purchase of a product or service through any medium or in any environment, you should use your best judgment and exercise caution where appropriate.
            </li>
        </ol>
    </li>
    <li><b>Linked sites</b>
        <ol>
            <li>
                The Site may be linked to other websites over which we have no control.
                Those links are provided for convenience only and may not remain current or be maintained.
            </li>
            <li>
                We do not sponsor, endorse, adopt, confirm, guarantee or approve the content or representations made on those websites.
                We make no representation about the accuracy of content contained on those websites.  We are not liable for the content on those websites.
            </li>
        </ol>
    </li>
    <li><b>Disclaimer</b>
        <ol>
            <li>
                You agree that your use of the Site is at your sole risk.
                To the fullest extent permitted by law, we and our officers, directors, employees, and agents exclude all warranties and guarantees, express or implied, in connection with the Site.
            </li>
            <li>
                To the fullest extent permitted by law, we exclude all warranties, guarantees, conditions, terms or representations about the accuracy or completeness of the Site's content or the content of any sites linked to this Site and we assume no liability or responsibility for any:
                <ol>
                    <li>
                        errors, mistakes, or inaccuracies of content;
                    </li>
                    <li>
                        personal injury or property damage, of any nature whatsoever, resulting from your access to and use of the Site;
                    </li>
                    <li>
                        any unauthorised access to or use of our secure servers and/or any and all personal information and/or financial information stored on our servers;
                    </li>
                    <li>
                        any interruption or cessation of transmission to or from the Site;
                    </li>
                    <li>
                        any bugs, viruses, trojan horses, malicious code or the like which may be transmitted to or through our Site by any third party, and/or
                    </li>
                    <li>
                        any errors or omissions in any content or for any loss or damage of any kind incurred as a result of the use of any content posted, emailed, transmitted, or otherwise made available via the Site.
                    </li>
                </ol>
            </li>
        </ol>
    </li>
    <li><b>Limitation of liability</b>
        <ol>
            <li>
                To the fullest extent permitted by law, in no event will we or our officers, directors, employees, or agents, be liable to you for any direct, indirect, incidental, special, punitive, losses or expenses or consequential damages whatsoever resulting from any:
                <ol>
                    <li>
                        errors, mistakes, or inaccuracies of content on the Site;
                    </li>
                    <li>
                        personal injury or property damage, of any nature whatsoever, resulting from your access to and use of the Site;
                    </li>
                    <li>
                        any unauthorised access to or use of our secure servers and/or any and all personal information and/or financial information stored on our servers;
                    </li>
                    <li>
                        any interruption or cessation of transmission to or from the Site;
                    </li>
                    <li>
                        any bugs, viruses, trojan horses, malicious code or the like, which may be transmitted to or through the Site by any third party, and/or 
                    </li>
                    <li>
                        any errors or omissions in any content or for any loss or damage of any kind incurred as a result of your use of any content posted, emailed, transmitted, or otherwise made available via the Site, whether based on warranty, contract, tort, or any other legal theory, and whether or not we are advised of the possibility of such damages.
                    </li>
                </ol>
            </li>
            <li>
                We understand that, in some jurisdictions, warranties, guarantees, disclaimers and conditions may apply that cannot be legally excluded.
                If that is true in your jurisdiction, then to the extent permitted by law, we limit our liability for any claims under those warranties, guarantees or conditions to either supplying you the services again (or the cost of supplying you the services again).
            </li>
            <li>
                You specifically acknowledge that we are not liable for content of any third party, or the defamatory, offensive, or illegal conduct of any third party, and that the risk of harm or damage from such content or conduct rests entirely with you.
            
            
            </li>
            <li>The Site is controlled and offered by us from our facilities in the State of New
                South Wales, Australia. We make no representations that the Site is appropriate
                or available for use in locations outside Australia. Those who access or use the
                Site from other jurisdictions do so at their own volition and are responsible for
                compliance with local law. </li>
        </ol>
    </li>
    <li><b>Indemnity</b>
        <ol>
            <li>To the extent permitted by applicable law, you agree to defend, indemnify and hold
                harmless us and our officers, directors, employees and agents, from and against
                any and all claims, damages, obligations, losses, liabilities, costs or debt, and
                expenses (including but not limited to attorney's fees) arising from:
                <ol>
                    <li>your use of and access to the Site; </li>
                    <li>your violation of any term of these General Terms of Use, the Member Terms of Use
                        or the Recruiter Terms of Use; </li>
                    <li>your violation of any third party right, including without limitation any copyright,
                        property, or privacy right; and/or </li>
                    <li>any claim that any information, material or content you post on the Site caused
                        damage to a third party. This defense and indemnification obligation will survive
                        these General Terms of Use and your use of the Site. </li>
                </ol>
            </li>
        </ol>
    </li>
    <li><b>Suspension/termination of access to the Site</b>
        <ol>
            <li>We reserve the right to immediately suspend or terminate your use of, or access
                to, the Site at any time if we determine that you have breached these General Terms
                of Use or any relevant law, or you have engaged in any conduct that we consider
                to be inappropriate or unacceptable. </li>
            <li>All restrictions imposed on you, and all our disclaimers, limitations of liability
                and indemnities set out in these General Terms of Use will survive any termination.
            </li>
        </ol>
    </li>
    <li><b>General</b>
        <ol>
            <li>Nothing in these General Terms of Use will create an agency, partnership, joint
                venture, employee-employer or franchisor-franchisee relationship between you and
                us. </li>
            <li>These General Terms of Use contain the entire agreement between you and us about
                your general use of the Site. Additional terms of use apply to any person who registers
                as a Member or Recruiter. You will be prompted to accept those terms of use before
                registering as a Member or Recruiter. </li>
            <li>In the event of any inconsistency between a term in these General Terms of Use and
                a term in the Member Terms of Use or Recruiter Terms of Use, these General Terms
                of Use will prevail. </li>
            <li>No failure or delay of either you or us in exercising any right, power, or privilege
                under these General Terms of Use operates as a waiver or any such right, power of
                privilege. </li>
            <li>If any provision of these General Terms of Use is found by a court of competent
                jurisdiction to be invalid, then the provision is deemed deleted but the court should
                try to give effect to the parties' intentions as reflected in the provision. The
                other provisions of these General Terms of Use are to remain in full force and effect.
            </li>
            <li>Your rights and obligations under these General Terms of Use are personal and may
                not be assigned or dealt with in any way without our approval, which we may withhold
                in our absolute discretion. </li>
            <li>You must do everything reasonably required by us to give full effect to these General
                Terms of Use. </li>
            <li>Where anything requires our consent or approval, that consent or approval may be
                given conditionally or withheld by us as we decide. </li>
            <li>These General Terms of Use are governed by the laws in force in the State of New
                South Wales, Australia. You agree to submit to the non-exclusive jurisdiction of
                the courts in the State of New South Wales, Australia. </li>
        </ol>
    </li>
</ol>
