using System.Collections.Generic;
using System.IO;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Candidates.Lens
{
    [TestClass]
    public class DeserializeTests
        : TestClass
    {
        private readonly IParseResumeXmlCommand _parseResumeXmlCommand = Resolve<IParseResumeXmlCommand>();

        private static readonly string ResumeFile = FileSystem.GetAbsolutePath(@"Test\Data\Resumes\Resume.xml", RuntimeEnvironment.GetSourceFolder());
        private static readonly string Resume5File = FileSystem.GetAbsolutePath(@"Test\Data\Resumes\TestResume5.xml", RuntimeEnvironment.GetSourceFolder());

        private const string FirstName = "TOM";
        private const string LastName = "KIDMAN";
        private static readonly PartialDate DateOfBirth = new PartialDate(1978, 5);
        private const string EmailAddress = "allan@yahoo.com";
        private const string Street = "16 Horne St";
        private const string Location = "3056 Melbourne Vic";
        private const string PhoneNumber = "(02) 9863 4408";

        private const string FirstName5 = "Kate";
        private const string LastName5 = "Flinstone";
        private const string EmailAddress5 = "kate@test.linkme.net.au";
        private const string Street5 = "57 Bedrock St";
        private const string Location5 = "2021 Sydney NSW";
        private const string PhoneNumber5 = "0400 999 999";

        private static readonly string[] Courses = new[]
        {
            "AUS, 2000Sun Certified Java Programmer (1.4)",
            "tutor for the Department of Computer Science at RMIT University, teaching Software Engineering."
        };

        private const string Objective = "CONSULTANT SUMMARY OF TOM KIDMAN Consultant/Software Engineer";
        private const string Summary = "Summary of Expertise\r\nTom Kidman joined Object Consulting in 2020\r\neager to begin work designing, developing and maintaining leading edge software applications.\r\nSince then he has accumulated technical expertise in technologies including J2EE, Web Services and EAI, and also has exposure to numerous tools such as CVS, Ant and Cruisecontrol. Tom's work demonstrates initiative, showing him to be productive with a minimum of managerial support required.At Object Consulting, Tom has worked on projects that include the use of the following technologies:";
        private const string Skills = "J2EE: Developing and modifying J2EE components.Web Services: Developing components accessing and providing services over HTTP using SOAP.Application servers: Deploying and executing components within J2EE environments such as SunONE 8 and WebSphere 4.0 & 5.0.Java Server Pages, including JSTL.XML: Developing schemas and use of SAX and DOM parsers.Apache Struts and Tiles.EAI: Worked on a complex transformation system to enable the integration of two end-systems extending the TIBCO suite of products with core Java components.Java: Extended and enhanced a complex automated testing tool, and developed a suite of utilities to automate routine tasks.Unix: Configured and maintained environments residing on Solaris machines.Perl: Writing scripts for various file-processing tasks with use of regular expressions.Tom is a Sun certified Java programmer, completing the exam in October, 2003.In addition, Tom has\r\nSummary of Technical Skills\r\nOperating Systems\r\nWindows 9x, NT, 2000, XP\r\nSolaris\r\nLanguagesJ2EE 1.4Java 1.4PerlXMLJSTLSQLC++CVisual BasicMethodologiesProcess MeNtORUMLAgileDatabase Management SystemsOracleToolsIntelliJ IDEAWSADCVSEclipseAntSAX & DOM XML parsersJunitcruisecontrolTIBCO IM and RendezvousApplication PackagesSunONE 8IBM Websphere 4, 5";

        private static readonly IList<Job> Jobs = new List<Job>
        {
            new Job {Dates = new PartialDateRange(new PartialDate(2004, 9)), Title = "Mentor Business Analyst Course", Company = "LinkMe Australia", Description = "Web Services\r\ninterface\r\nfor TXUTelstra's EAI environment is large and complex, and regression testing even a small change in the EAI infrastructure creates a huge financial cost.  Tom is currently working in a small team,"},
            new Job {Dates = new PartialDateRange(new PartialDate(2003, 8)), Title = null, Company = null, Description = "Item Tracking OnLine (WITOL)The WITOL project is an online tracking system for use by Sensis sales consultants that is currently being developed.  Tom was involved in setting up an 'Agile' continuous build environment for WITOL using cruisecontrol and ANT.  The build process compiles the classes, runs regression tests and creates a distributable war file that can be deployed to a web application server such as Tomcat.  This role exposed Tom to J2EE web applications.Courses/Training"},
            new Job {Dates = new PartialDateRange(new PartialDate(2004, 7), new PartialDate(2004, 8)), Title = "Superman", Company = "Special Forces of LinkMe UK", Description = "EAI Test Harness, TelstraTelstra's EAI environment is large and complex, and regression testing even a small change in the EAI infrastructure creates a huge financial cost.  Tom is currently working in a small team, rewriting and redesigning the Automated Message Testing Tool developed as part of the Sensis Transformation Engine project, so that an automated suite of regression tests can be developed for the Telstra EAI environment.  Once complete, this will significantly reduce the financial burden faced by Telstra when running regression tests.  The Automated Message Testing Tool is written in java, and each test case is defined through an XML file.   Tom is responsible for extending the Testing Tool to use the new XML test case files, exposing Tom to XSDs as well as SAX and DOM XML parsers.  Tom has also obtained further experience in TIBCO IM and Rendezvous through working on this project."},
            new Job {Dates = new PartialDateRange(new PartialDate(2004, 4), new PartialDate(2004, 7)), Title = null, Company = null, Description = "Land Exchange, Victorian GovernmentDuring this time Tom worked in the SPEAR division of the Land Exchange project, developing a complex, large scale J2EE web application to streamline the process of subdividing lots.  This work exposed Tom to early lifecycle development within a large team environment and has required Tom to use Struts and Tiles, develop Entity and Session beans, and create JSPs with JSTL.  Tom was responsible for implementing large portions of functionality including a complex notification system triggered by many different system events.Tom also worked on the A2A sub-project of SPEAR. This project's goal was to expose the entire functionality of SPEAR through Web Services, and the interfaces implemented by Tom further advanced his knowledge and experience in this new technology.  Tom also became familiar with deploying the SPEAR application to WebSphere AppServer 5.0, and using WSAD as a development environment."},
            new Job {Dates = new PartialDateRange(new PartialDate(2003, 10), new PartialDate(2004, 3)), Title = null, Company = null, Description = "Over this period Tom worked on a number of Telstra J2EE projects.  One such project was Customer Access, which provides an interface to Telstra legacy systems and makes use of Java Beans and Web Services through SOAP over HTTP.  Tom successfully implemented a large functional extension to this component, and was responsible for managing the release of the new version.  In order to complete the extension Tom had to set up and deploy Customer Access on multiple SunONE and WebSphere application servers.Tom successfully completed a logging change request within the Fast Selfcare project, which required an in-depth understanding of the code and process flow.  He also made use of his knowledge of Perl by writing scripts to aid in the analysis of logged data.  Tom has also been writing ant build tasks for the Mobile Pre-Paid Activation project, and helping with Telstra Agile work."},
            new Job {Dates = new PartialDateRange(new PartialDate(2002, 12), new PartialDate(2003, 10)), Title = null, Company = null, Description = "Sensis EAI Transformation EngineThe Transformation Engine transforms messages sent between a number of software systems so that they can be handled in the format of each receiving system, and was developed using TIBCO products. Tom is responsible for ensuring that the production system is constantly operational and for resolving messaging errors that arise. Using Java, he has developed an extensive suite of utilities to automate daily tasks and error resolution activities.  These utilities make sound use of object-oriented principles. Tom also maintains and supports 7 testing environments, and performs upgrades and deployments.  The Transformation Engine environments reside on Solaris machines and require a solid knowledge of Unix and Oracle, and Tom has written many Perl scripts for various tasks.Enhancing and extending the Automated Message Testing Tool (AMTT) has provided Tom with development experience and has exposed him to tools such as ANT, cruisecontrol and Intellij IDEA. AMTT was built in Java and interfaces with the TIBCO messaging infrastructure to allow developers to execute complex tests in a controlled and automated environment.  AMTT required a large refactoring and debugging effort that was especially challenging given the n-ary tree data structures AMTT uses when making comparisons.  The Transformation Engine is now built after any check-in to CVS, and AMTT runs through its entire regression suite against this build. Statistics for each test are recorded and displayed online using Tomcat."},
            new Job {Dates = null, Title = "Mentor Business Analyst Course", Company = null, Description = null},
            new Job {Dates = null, Title = null, Company = "Copyright Object Consulting Pty Ltd", Description = null},
            new Job {Dates = null, Title = null, Company = "Copyright Object Consulting Pty Ltd", Description = null},
        };

        private static readonly IList<School> Schools = new List<School>
        {
            new School {Degree = "Master of Technology", Institution = "RMIT University", Major = "Software Engineering"},
        };

        private const string Interests5 = "ECC Thailand (Nonthaburi Province locations)  2009  TEFL/TESOL/TOEFL Teacher  Profile: Taught young learners to adults English as a Second Language in a Thailand tutoring and development school. Conducted classes and private lessons with all age groups and language levels, student placement testing and overall progress testing.  Achievements:";
        private const string Referees5 = "Referees: Available on request";
        private const string Summary5 = "Career Background  Summary: I graduated from a BA (Media & Communications) at the University of Sydney in 2006 and I have over five years experience as a copywriter, editor and proof-reader. I have developed my editorial skills in a number of disciplines including magazine and book publishing, direct and online marketing, website and print design. My editorial experience has included editing cooking, lifestyle and children's books, online travel writing, proof-reading television manuscripts, writing direct and e-marketing material, website copy layout, picture selection and various freelance editing. I am highly focused and dedicate myself to projects where I exercise my aptitude for the English language, meticulously and with great attention to detail, and utilise my creative and supportive abilities.  Specialist areas are:  * Editing";
        private const string Skills5 = "* Copywriting * Proof-reading * Design & layout * Creative\r\ncontribution Programs: * Adobe InDesign * Quark Xpress * Dreamweaver\r\n* Incorporating appropriate activities and lesson progression, especially in mixed ability classes  * Preparing private students for TOEFL examinations, especially in grammar awareness, tenses and test procedures and skills  * Building a strong rapport with all students and colleagues through collaborative interaction";

        private static readonly IList<Job> Jobs5 = new List<Job>
        {
            new Job {Dates = new PartialDateRange(new PartialDate(2008, 1), new PartialDate(2009, 12)), Title = "Personal Assistant", Company = "Mindspace Pty Ltd", Description = "Profile: Mindspace is a city-based psychology practice that specialises in clinical, vocational and cognitive behavioural therapy. Achievements: * Writing, proofing, editing and word processing evaluation reports and psychological dissertations * Scoring psychometric appraisals and ensuring confidentiality and privacy is upheld * Maintaining administrative records and arranging appointments * Ad hoc duties  Vocational study and qualification (TEFL/TESOL"},
            new Job {Dates = new PartialDateRange(new PartialDate(2008, 1), new PartialDate(2009, 12)), Title = null, Company = null, Description = "Travelled around Europe and Australasia 2008  Imagine Digital Marketing  2007 - 2008  Copywriter  Profile: Writing copy for travel, technology, arts/media/entertainment and other specialty clients. Written marketing material includes websites, brochures, emails, advertisements and client submissions. Other promotional work involves creative trade giveaways, slogans and AdWords campaigns.  Achievements:  * Developing successful creative trade promotions and tactical websites  * Formulating AdWords campaigns and client submissions for travel companies, in particular  * Writing website copy for a range of clients and specialties  * Editing, proofing and cross-checking marketing, promotional and website copy  * Picture and photo selection and placement for a range of projects  * Liaising with clients face-to-face, via phone and email; overall customer service"},
            new Job {Dates = new PartialDateRange(new PartialDate(2006, 1), new PartialDate(2007, 12)), Title = "Editorial & Design Assistant", Company = "Australian Consolidated Press Books", Description = "I was assigned increasing responsibilities and tasks. I also liaised with projects involving other ACP titles including Woman's Day, and other publishing houses including Random House in Australia and affiliates in the UK and France.  Achievements:  * Retrieving and compiling content for books, typing copy and researching stories  * Production Assistant on photo shoots - primarily an organizational role  * Composing grids and choosing content for newly proposed books for a variety of markets  * Interacting with people from a range of sectors; via phone, email and in-person; overall customer service   Australian Consolidated Press: Cosmopolitan Magazine & ACP Books  2006 Internship placements Profile: Cosmopolitan is one of the best-selling magazines to date and part of the largest publishing house in Australia. ACP Books publishes cook books, special celebration books and home/design magazines.  Achievements:  * Typing copy and researching stories, writing analyses of magazine structure and content * Retrieving and compiling data including Rights lists, mail and filing  * Interacting with sector representatives; via phone, email and in-person; overall customer service"},
            new Job {Dates = new PartialDateRange(new PartialDate(2001, 1), new PartialDate(2005, 12)), Title = "Administrative Assistant", Company = "Interim Pty Ltd (now Randstad", Description = "Profile: Interim is a national, specialist Outplacement and Career Coaching consultancy, now part of Vedior, the world's third largest (by revenue) Human Capital company. It is also the Australian partner of the global group, Career Partners International.  Achievements:  * Review, edit and contribute to print and e-learning program content and promotional material.  * Score psychometric appraisals, word process appraisal reports, liaise with Chief Psychologist, Chief Coach and coaching team regarding candidate feedback.  * Client reporting, maintain confidentiality and candidates' privacy.  * Administration duties: assist with co-ordinating (large scale) national assignments driven by M&As, restructuring and downsizing, staff travel, general support to staff; overall customer service"},
            new Job {Dates = null, Title = null, Company = "Paramount Recruitment", Description = ": Paramount specialise in medical, health, pharmaceutical and scientific recruitment for medical communications agencies. Achievements: * Top performing Resource Executive in company history * Profiling candidates for a range of niche agencies * Writing, editing and proof-reading vacancy advertisements * Establishing client relationships and new business development * Setting up interviews, terms and conditions and negotiating offers  Jade Solutions Creative Limited  2009 - 2010 Copywriter & Editor Profile: Jade Solutions is a creative agency primarily involved in websites and print design. I worked on copywriting and editing material for Jade Solutions clients, specifically Birmingham City Council. Achievements: * Writing and editing copy for online and print publication purposes * Writing and editing marketing and promotional material for internal and external functions * Proofing and fact-checking specialised marketing copy and client profiles * Working alongside design and IT staff devising aesthetic appearance of projects, including picture selection,   colour schemes, information layout, copy size and font * Maintaining client relations via phone, email and face-to-face * Ad hoc duties"},
            new Job {Dates = null, Title = "Advertising Sales Co-ordinator", Company = null, Description = "Profile: Assisting three advertising sales teams in an administration, creative and strategic role. Responsibilities include company and client database maintenance and collation, managing client bookings and contracts, mailing, filing, combining sourced facts and figures with creative copy for client information and company marketing.  Achievements:  * Writing promotional copy for flyers and special report feature brochures  * Creating and designing pitch presentations  * Representing the publication at client and agency via phone, email and face-to-face appointments; overall customer service  * Sole organization and hosting of client functions on a monthly basis"},
        };

        private static readonly IList<School> Schools5 = new List<School>
        {
            new School {City = "Sydney", Country = "Australia", Degree = "BA", Description = "Desktop Training (Adobe Accredited), 2011 InDesign Essentials Course Including: Interface & Preferences, Document Setup & Navigation, Document Shortcuts, Working with Text including Styles, Working with Objects (including images), Working with Colour, Designing Forms, Creating Tables, Importing Graphics, Automation & Troubleshooting including Master Pages, Preparing for Print & Exporting PDFs English & Computer College (ECC) Thailand, 2009  Introduction to TESOL: 100-hour practical course qualification  Techniques including: lesson planning, mixed-ability classes, interactive learning, phonology, cultural sensitivities, classroom management, practical development, effective communication  i-to-i Australia, 2008  100-hour combined TEFL course  Including: 20-hour weekend course; 40-hour Grammar course; Specialist Certificates in Teaching: Young Learners, Large Classes, Business English, With Limited Resources\r\n: Media and History, Minor: Anthropology; Thesis: focus on Photography Copyright issues and its progression in the print media industry.  Kincoppal-Rose Bay\r\nHSC subjects: 4U English, 3u Modern History, 2U Ancient History, 2U Religion. UAI:", Institution = "University of", Major = "Media & Communications", CompletionDate = new PartialCompletionDate(new PartialDate(2006, 12)) },
        };

        [TestMethod]
        public void TestReadResumeFile()
        {
            var xml = ReadFile(ResumeFile);
            var resume = _parseResumeXmlCommand.ParseResumeXml(xml);

            Assert.AreEqual(FirstName, resume.FirstName);
            Assert.AreEqual(LastName, resume.LastName);
            Assert.AreEqual(DateOfBirth, resume.DateOfBirth);
            Assert.AreEqual(Street, resume.Address.Street);
            Assert.AreEqual(Location, resume.Address.Location);
            Assert.IsTrue(new List<EmailAddress> { new EmailAddress { Address = EmailAddress, IsVerified = false } }.NullableCollectionEqual(resume.EmailAddresses));
            Assert.IsTrue(new List<PhoneNumber> { new PhoneNumber { Number = PhoneNumber, Type = PhoneNumberType.Work } }.NullableCollectionEqual(resume.PhoneNumbers));

            Assert.AreEqual(null, resume.Resume.Affiliations);
            AssertAreEqual(null, resume.Resume.Awards);
            Assert.AreEqual(null, resume.Resume.Citizenship);
            AssertAreEqual(Courses, resume.Resume.Courses);
            Assert.AreEqual(null, resume.Resume.Interests);
            Assert.AreEqual(Objective, resume.Resume.Objective);
            Assert.AreEqual(null, resume.Resume.Other);
            Assert.AreEqual(null, resume.Resume.Professional);
            Assert.AreEqual(null, resume.Resume.Referees);
            Assert.AreEqual(Summary, resume.Resume.Summary);
            Assert.AreEqual(Skills, resume.Resume.Skills);
            AssertAreEqual(Jobs, resume.Resume.Jobs);
            AssertAreEqual(Schools, resume.Resume.Schools);
        }

        [TestMethod]
        public void TestReadResume5File()
        {
            var xml = ReadFile(Resume5File);
            var resume = _parseResumeXmlCommand.ParseResumeXml(xml);

            Assert.AreEqual(FirstName5, resume.FirstName);
            Assert.AreEqual(LastName5, resume.LastName);
            Assert.AreEqual(null, resume.DateOfBirth);
            Assert.IsTrue(new List<EmailAddress> { new EmailAddress { Address = EmailAddress5, IsVerified = false } }.NullableCollectionEqual(resume.EmailAddresses));
            Assert.AreEqual(Street5, resume.Address.Street);
            Assert.AreEqual(Location5, resume.Address.Location);
            Assert.IsTrue(new List<PhoneNumber> { new PhoneNumber { Number = PhoneNumber5, Type = PhoneNumberType.Mobile } }.NullableCollectionEqual(resume.PhoneNumbers));

            Assert.AreEqual(null, resume.Resume.Affiliations);
            AssertAreEqual(null, resume.Resume.Awards);
            Assert.AreEqual(null, resume.Resume.Citizenship);
            AssertAreEqual(null, resume.Resume.Courses);
            Assert.AreEqual(Interests5, resume.Resume.Interests);
            Assert.AreEqual(null, resume.Resume.Objective);
            Assert.AreEqual(null, resume.Resume.Other);
            Assert.AreEqual(null, resume.Resume.Professional);
            Assert.AreEqual(Referees5, resume.Resume.Referees);
            Assert.AreEqual(Summary5, resume.Resume.Summary);
            Assert.AreEqual(Skills5, resume.Resume.Skills);
            AssertAreEqual(Jobs5, resume.Resume.Jobs);
            AssertAreEqual(Schools5, resume.Resume.Schools);
        }

        private static void AssertAreEqual(IList<School> expectedSchools, IList<School> schools)
        {
            if (expectedSchools == null)
            {
                Assert.IsNull(schools);
            }
            else
            {
                Assert.AreEqual(expectedSchools.Count, schools.Count);
                for (var index = 0; index < expectedSchools.Count; ++index)
                    Assert.AreEqual(expectedSchools[index], schools[index]);
            }
        }

        private static void AssertAreEqual(IList<Job> expectedJobs, IList<Job> jobs)
        {
            if (expectedJobs == null)
            {
                Assert.IsNull(jobs);
            }
            else
            {
                Assert.AreEqual(expectedJobs.Count, jobs.Count);
                for (var index = 0; index < expectedJobs.Count; ++index)
                    AssertAreEqual(expectedJobs[index], jobs[index]);
            }
        }

        private static void AssertAreEqual(IJob expectedJob, IJob job)
        {
            Assert.AreEqual(expectedJob.Title, job.Title);
            Assert.AreEqual(expectedJob.Company, job.Company);
            Assert.AreEqual(expectedJob.Description, job.Description);
            Assert.AreEqual(expectedJob.Dates, job.Dates);
        }

        private static void AssertAreEqual(IList<string> expectedStrings, IList<string> strings)
        {
            if (expectedStrings == null)
            {
                Assert.IsNull(strings);
            }
            else
            {
                Assert.AreEqual(expectedStrings.Count, strings.Count);
                for (var index = 0; index < expectedStrings.Count; ++index)
                    Assert.AreEqual(expectedStrings[index], strings[index]);
            }
        }

        private static string ReadFile(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }
    }
}