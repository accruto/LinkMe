using System.Collections.Generic;
using LinkMe.Apps.Agents.Security;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Views
{
    [TestClass]
    public class ViewBugTests
        : ViewsTests
    {
        [TestMethod, Description("Bug 11727")]
        public void TestPartialView()
        {
            var member = new Member
            {
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = "member@test.linkme.net.au" } },
                FirstName = "Nick",
                LastName = "Flinstone",
                DateOfBirth = null,
                Gender = 0,
                VisibilitySettings = new VisibilitySettings
                {
                    Professional =
                    {
                        EmploymentVisibility = (ProfessionalVisibility) 183,
                    }
                },
                Address = new Address { Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), null) },
            };

            _memberAccountsCommand.CreateMember(member, new LoginCredentials {LoginId = "member@test.linkme.net.au", Password = "password"}, null);

            var candidate = _candidatesCommand.GetCandidate(member.Id);
            candidate.Status = (CandidateStatus) 2;
            candidate.DesiredJobTitle = null;
            candidate.DesiredJobTypes = 0;
            candidate.DesiredSalary = null;
            candidate.RelocationPreference = 0;
            candidate.HighestEducationLevel = null;
            candidate.RecentSeniority = null;
            candidate.RecentProfession = null;
            candidate.VisaStatus = null;
            _candidatesCommand.UpdateCandidate(candidate);

            var resume = new Resume
            {
                Awards = new[] { "Curtin University of Technologies \"Best Use of New Media\" Award 1996.  Received award for role a director in the HOUSE@ project. A sixteen part internet serial written in MACROMEDIA's SHOCKWAVE and alternatively in HTML. As part of the project it was archived on CDROM along with digital works from eleven tertiary institutions around Australia. A radio play was also made and broadcast over the duration of the project." },
                Skills = "TECHNICAL SUMMARY                   Languages & Frameworks:  Additional Software:  OBJECTIVE-C, JAVA (swing, j2ee, jsp, jdbc, servlets, jung, j2me, quicktime), JAVASCRIPT, ACTIONSCRIPT, AJAX, HIBERNATE,  HTML, PERL, XML, JSON, CSS, RSS, MAGNOLIA, WEBSOCKETS  O/S & JVM's Linux, MacOS, UNIX, SunOS, SOLARIS, TOMCAT, RED 5, GOOGLE APPS   Databases: ORACLE, POSTGRES, MYSQL  PHOTOSHOP, ILLUSTRATOR, FLASH, INDESIGN, AVID, PREMIERE, AFTER EFFECTS  IDE's and Deployment Tools: ANT, MAVERN, SVN, ECLIPSE, NETBEANS, FLEX, XCODE, GITHUB",

                Jobs = new List<Job>
                           {
                    new Job { Title = "Java Develper/Flash Builder Developer", Description = "Contracted to build the web component of the Six3.tv iphone video messaging service.  Server technologies included Red5/tomcat streaming media server interfacing with a restful service for storage.  Contract was completed on time and the service is now live.", Company = "SixThree", Dates = new PartialDateRange(new PartialDate(2011, 11), new PartialDate(2012, 1)) },
                    new Job { Title = "Flash & HTML Developer", Description = "Initially contracted to build the \"Australian Wool Corporation\" website, remained with Euro RSCG to help specify, prototype and build The \"Les Amis\" suite of smart phone applications for Credit Suisse. Technologies used in this process included Objective-c, jWebsockets and Subversion.  Other roles at Euro RSCG included 3D work for \"Goodwood Revival\" sponsorship, Flash banner work for Citroen and Credit Suisse, and Flash Builder/Flex work for the Chivas Regal global website.", Company = "Euro RSCG", Dates = new PartialDateRange(new PartialDate(2010, 12), new PartialDate(2011, 11)) },
                    new Job { Title = "Flash/Flex Developer", Description = "to assist with completion of Bacardi Global Training Site. The Site is written on a Flex/Flash Builder IDE. Responsibilities ranged from finishing artwork to trouble shooting problems with existing code and creating new flex modules.", Company = "Maverick Design", Dates = new PartialDateRange(new PartialDate(2010, 10), new PartialDate(2010, 12)) },
                    new Job { Title = "Creative Director/Senior Software Engineer", Description = "Responsible for GUI implementation as well as retaining a very hands-on involvement in the underlying development its self.   Most recently for a the Virgin Media Website. Specifically the Electronic Programming Guide (EPG). The TV and Video on Demand sections of the  website use an ORACLE 10i database connected to bespoke JAVA/HIBERNATE/AJAX application. The website also integrates a third party predictive logic search. The content is updated daily through a separate XML Digester application written in JAVA running as a daily CRON job.  Other works carried out while contracting to Equate IT Ltd include: -  GUI written in ACTIONSCRIPT 3 for Sound Index. A BBC/IBM semantic music chart based on social networking.  A Prototype set top box application assembled in ACTIONSCRIPT 3 for Miniweb.  A social network hyper link visualisation application utilising an implementation of a JAVA SWING and JUNG graphing libraries called Rufus. Written for Porter Novelli as a pitch tool. The finished tool had both a GUI and COMMAND LINE interface. It also has an export feature for various standard graph formats.  Wrote International Space Station Invaders in OBJECTIVE-C on an iphone/ipad platform. Application is currently available in the Itunes Store.  An iPhone RSS application written in OBJECTIVE-C to display news feeds for NewzDog. The service is subscribed to by The Independent.   J2ME Software for the Hug Me t-shirt's mobile handsets' bluetooth interface.  Also deploying assorted Content Managed Systems (CMS) using the MAGNOLIA Framework.  Currently working in OBJECTIVE-C on iphone/ipad applications.", Company = "Equate IT Ltd", Dates = new PartialDateRange(new PartialDate(2009, 1), new PartialDate(2010, 10)) },
                    new Job { Title = "Creative Director/Senior Software Engineer", Description = "As a founder of Arishi Ltd in 2002, Nick's work duties carried across several disciplines. As well as playing a role in the management of the company, he was involved as either a project leader or team member on all major projects.   Some Jobs involved in with while at Arishi Ltd, reverse chronologically include: -  The GUI for Arishi Ltd's proprietary Web based FTP server that ran on a standard Jakarta Tomcat install on a Linux platform. The application is written in JAVA and is currently used by Wave Studios, London for audio file management and client approvals.  A photo blog for 2008 European Football Championship sponsored by Sony built in JSP/SERVLETS on TOMCAT 5. The blog included an embedded JAVA SWING utility which allowed for batch uploading of photographs to the application.  The AJAX front end functionality is entirely written in HTML/JAVASCRIPT utilising JSON calls for database access.  Various top to bottom internet video streaming solution using an RMTP server over FLASH Client. Both Adobe's out of the box solution and RED 5.  A mobile video delivery system for Thomas Cook TV written in JAVA. The server side application also included SMS notification.  An ACTIONSCRIPT 2/3 flash video 8 showcase website for ETV Ltd, London.  The GUI for Arishi Ltd's award winning Pagoda audio and video content delivery system.  QUICKTIME for JAVA integration for ProductionWorld. Proprietary software produced by Arishi for TV commercials/Music Video Clip production.  GUI and CMS written in JSP/SERVLETS running on TOMCAT for Vodafone Marketing intranet.", Company = "Arishi Ltd", Dates = new PartialDateRange(new PartialDate(2002, 12), new PartialDate(2009, 1)) },
                    new Job { Title = "Senior Developer", Description = "Worked on bespoke applications written using the RemoteApps Java Framework for a variety of clients including Volkswagen, Arsenal FC, and Football 365.  Also worked on a WAP and SMS polling prototype for the Coke ReactiveAd campaign.  Led the authoring of a Technical and a Functional Specification for PRNewswire's (Now Thompsons) RemoteApps integration.  In 2001 transferred to the RemoteApps Core Framework team to manage the core product's GUIs' W3C compliance. This involved the use of JSP/SERVLETS and CSS.", Company = "Ovate Ltd/RemoteApps Ltd", Dates = new PartialDateRange(new PartialDate(2000, 11), new PartialDate(2002, 12)) },
                    new Job { Title = "Technical Director", Description = "Managed Web and Mail hosting for a variety of clients on LINUX platforms. Also deployed Content Managed Websites in PERL and HTML for clients such as devour.com.au.  Provided technical support to the Concept Centre mentioned below.", Company = "MonkeyMedium", Dates = new PartialDateRange(new PartialDate(1999, 12), new PartialDate(2000, 11)) },
                    new Job { Title = "Technical Director", Description = "Provided Technical Direction on projects for the Concept Centre including House Home-wares online catalogue.  Produced a number of CDRoms using Director and live video mixed in AVID with AFTER EFFECTS.", Company = "Concept Centre", Dates = new PartialDateRange(new PartialDate(1998, 12), new PartialDate(1999, 12)) },
                    new Job { Title = "Art Director", Description = "Produced finished print and digital art for a variety of clients using PHOTOSHOP, ILLUSTRATOR and QUARK.   Produced demo CDRom for E-Match. Produced interactive website for Subway DC Clothing written in HTML.", Company = "The Creative Department", Dates = new PartialDateRange(new PartialDate(1996, 12), new PartialDate(1998, 12)) },
                },

                Schools = new List<School>
                {
                    new School { Institution = "Curtin University of Technology", Description = null, Degree = "BA", Major = "Fine Art", City = null, Country = null, CompletionDate = null },
                }
            };
            _candidateResumesCommand.CreateResume(candidate, resume);

            TestCandidateUrls(member, () => AssertCandidate(member.Id));

            Get(GetCandidatePartialUrl(member.Id));
            AssertCandidate(member.Id);
        }
    }
}
