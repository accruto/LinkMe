using System;
using System.Collections.Generic;
using System.IO;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Resumes;

namespace LinkMe.Domain.Roles.Test.Candidates.Mocks
{
    public enum TestResumeType
    {
        Complete,
        Empty,
        Invalid,
        Unavailable,
        NoName,
        NoEmailAddress,
        NoPhoneNumber,
        NoLocation,
        InvalidLocation,
        NoSchoolDate,
        FutureSchoolDate,
        FutureJobDate,
    }

    public class TestResume
    {
        private readonly TestResumeType _type;
        private readonly string _fileName;
        private readonly Func<ParsedResume> _getParsedResume;
        private readonly string _lensXml;

        public static readonly TestResume Complete = new TestResume(TestResumeType.Complete, "Complete.doc", GetCompleteResume, "<ResDoc><contact><name><givenname><![CDATA[Homer]]></givenname><surname><![CDATA[Simpson]]></surname></name><address><street><![CDATA[1414 Evergreen Terrace]]></street><city><![CDATA[South Yarra VIC 3141]]></city></address><phone><![CDATA[5555 5555]]></phone><email><![CDATA[hsimpson@test.linkme.net.au]]></email><personal><dob><![CDATA[November 1960]]></dob></personal></contact><resume><objective><![CDATA[Some objective]]></objective><summary><![CDATA[Some summary]]></summary><other><![CDATA[Some other]]></other><professional><![CDATA[Some professional]]></professional><skills><![CDATA[Some skills]]></skills><statements><citizenship><![CDATA[American]]></citizenship><affiliations><![CDATA[Some affiliations]]></affiliations><activities><![CDATA[Some interests]]></activities><references><![CDATA[Some referees]]></references></statements><experience><job><title><![CDATA[The nuclear technician]]></title><description><![CDATA[Nuclear technician]]></description><employer><![CDATA[Springfield Nuclear]]></employer><daterange><start><![CDATA[February 1990]]></start><end><![CDATA[current]]></end></daterange></job></experience><education><school><daterange><end><![CDATA[May 1984]]></end></daterange><institution><![CDATA[Mann Eye Institute]]></institution><degree><![CDATA[Science]]></degree><major><![CDATA[Physics]]></major><description><![CDATA[Nuclear physics]]></description><address><city><![CDATA[Springfield]]></city><country><![CDATA[USA]]></country></address></school></education><education><courses><![CDATA[Course1]]></courses><courses><![CDATA[Course2]]></courses></education><statements><honors><![CDATA[Award1]]></honors><honors><![CDATA[Award2]]></honors></statements></resume></ResDoc>");
        public static readonly TestResume Empty = new TestResume(TestResumeType.Empty, "Empty.doc", () => new ParsedResume { Resume = new Resume() }, "<ResDoc><contact></contact><resume></resume></ResDoc>");
        public static readonly TestResume Invalid = new TestResume(TestResumeType.Invalid, "Invalid.doc", () => null, "<ResDoc><contact></contact><resume></resume></ResDoc>");
        public static readonly TestResume Unavailable = new TestResume(TestResumeType.Unavailable, "Unavailable.doc", () => null, "<ResDoc><contact></contact><resume></resume></ResDoc>");
        public static readonly TestResume NoName = new TestResume(TestResumeType.NoName, "NoName.doc", GetNoNameResume, "<ResDoc><contact><address><street><![CDATA[1414 Evergreen Terrace]]></street><city><![CDATA[South Yarra VIC 3141]]></city></address><phone><![CDATA[5555 5555]]></phone><email><![CDATA[hsimpson@test.linkme.net.au]]></email><personal><dob><![CDATA[November 1960]]></dob></personal></contact><resume><objective><![CDATA[Some objective]]></objective><summary><![CDATA[Some summary]]></summary><other><![CDATA[Some other]]></other><professional><![CDATA[Some professional]]></professional><skills><![CDATA[Some skills]]></skills><statements><citizenship><![CDATA[American]]></citizenship><affiliations><![CDATA[Some affiliations]]></affiliations><activities><![CDATA[Some interests]]></activities><references><![CDATA[Some referees]]></references></statements><experience><job><title><![CDATA[The nuclear technician]]></title><description><![CDATA[Nuclear technician]]></description><employer><![CDATA[Springfield Nuclear]]></employer><daterange><start><![CDATA[February 1990]]></start><end><![CDATA[current]]></end></daterange></job></experience><education><school><daterange><end><![CDATA[May 1984]]></end></daterange><institution><![CDATA[Mann Eye Institute]]></institution><degree><![CDATA[Science]]></degree><major><![CDATA[Physics]]></major><description><![CDATA[Nuclear physics]]></description><address><city><![CDATA[Springfield]]></city><country><![CDATA[USA]]></country></address></school></education><education><courses><![CDATA[Course1]]></courses><courses><![CDATA[Course2]]></courses></education><statements><honors><![CDATA[Award1]]></honors><honors><![CDATA[Award2]]></honors></statements></resume></ResDoc>");
        public static readonly TestResume NoEmailAddress = new TestResume(TestResumeType.NoEmailAddress, "NoEmailAddress.doc", GetNoEmailAddressResume, "<ResDoc><contact><name><givenname><![CDATA[Homer]]></givenname><surname><![CDATA[Simpson]]></surname></name><address><street><![CDATA[1414 Evergreen Terrace]]></street><city><![CDATA[South Yarra VIC 3141]]></city></address><phone><![CDATA[5555 5555]]></phone><personal><dob><![CDATA[November 1960]]></dob></personal></contact><resume><objective><![CDATA[Some objective]]></objective><summary><![CDATA[Some summary]]></summary><other><![CDATA[Some other]]></other><professional><![CDATA[Some professional]]></professional><skills><![CDATA[Some skills]]></skills><statements><citizenship><![CDATA[American]]></citizenship><affiliations><![CDATA[Some affiliations]]></affiliations><activities><![CDATA[Some interests]]></activities><references><![CDATA[Some referees]]></references></statements><experience><job><title><![CDATA[The nuclear technician]]></title><description><![CDATA[Nuclear technician]]></description><employer><![CDATA[Springfield Nuclear]]></employer><daterange><start><![CDATA[February 1990]]></start><end><![CDATA[current]]></end></daterange></job></experience><education><school><daterange><end><![CDATA[May 1984]]></end></daterange><institution><![CDATA[Mann Eye Institute]]></institution><degree><![CDATA[Science]]></degree><major><![CDATA[Physics]]></major><description><![CDATA[Nuclear physics]]></description><address><city><![CDATA[Springfield]]></city><country><![CDATA[USA]]></country></address></school></education><education><courses><![CDATA[Course1]]></courses><courses><![CDATA[Course2]]></courses></education><statements><honors><![CDATA[Award1]]></honors><honors><![CDATA[Award2]]></honors></statements></resume></ResDoc>");
        public static readonly TestResume NoPhoneNumber = new TestResume(TestResumeType.NoPhoneNumber, "NoPhoneNumber.doc", GetNoPhoneNumberResume, "<ResDoc><contact><name><givenname><![CDATA[Homer]]></givenname><surname><![CDATA[Simpson]]></surname></name><address><street><![CDATA[1414 Evergreen Terrace]]></street><city><![CDATA[South Yarra VIC 3141]]></city></address><email><![CDATA[hsimpson@test.linkme.net.au]]></email><personal><dob><![CDATA[November 1960]]></dob></personal></contact><resume><objective><![CDATA[Some objective]]></objective><summary><![CDATA[Some summary]]></summary><other><![CDATA[Some other]]></other><professional><![CDATA[Some professional]]></professional><skills><![CDATA[Some skills]]></skills><statements><citizenship><![CDATA[American]]></citizenship><affiliations><![CDATA[Some affiliations]]></affiliations><activities><![CDATA[Some interests]]></activities><references><![CDATA[Some referees]]></references></statements><experience><job><title><![CDATA[The nuclear technician]]></title><description><![CDATA[Nuclear technician]]></description><employer><![CDATA[Springfield Nuclear]]></employer><daterange><start><![CDATA[February 1990]]></start><end><![CDATA[current]]></end></daterange></job></experience><education><school><daterange><end><![CDATA[May 1984]]></end></daterange><institution><![CDATA[Mann Eye Institute]]></institution><degree><![CDATA[Science]]></degree><major><![CDATA[Physics]]></major><description><![CDATA[Nuclear physics]]></description><address><city><![CDATA[Springfield]]></city><country><![CDATA[USA]]></country></address></school></education><education><courses><![CDATA[Course1]]></courses><courses><![CDATA[Course2]]></courses></education><statements><honors><![CDATA[Award1]]></honors><honors><![CDATA[Award2]]></honors></statements></resume></ResDoc>");
        public static readonly TestResume NoLocation = new TestResume(TestResumeType.NoLocation, "NoLocation.doc", GetNoLocationResume, "<ResDoc><contact><name><givenname><![CDATA[Homer]]></givenname><surname><![CDATA[Simpson]]></surname></name><address><street><![CDATA[1414 Evergreen Terrace]]></street></address><phone><![CDATA[5555 5555]]></phone><email><![CDATA[hsimpson@test.linkme.net.au]]></email><personal><dob><![CDATA[November 1960]]></dob></personal></contact><resume><objective><![CDATA[Some objective]]></objective><summary><![CDATA[Some summary]]></summary><other><![CDATA[Some other]]></other><professional><![CDATA[Some professional]]></professional><skills><![CDATA[Some skills]]></skills><statements><citizenship><![CDATA[American]]></citizenship><affiliations><![CDATA[Some affiliations]]></affiliations><activities><![CDATA[Some interests]]></activities><references><![CDATA[Some referees]]></references></statements><experience><job><title><![CDATA[The nuclear technician]]></title><description><![CDATA[Nuclear technician]]></description><employer><![CDATA[Springfield Nuclear]]></employer><daterange><start><![CDATA[February 1990]]></start><end><![CDATA[current]]></end></daterange></job></experience><education><school><daterange><end><![CDATA[May 1984]]></end></daterange><institution><![CDATA[Mann Eye Institute]]></institution><degree><![CDATA[Science]]></degree><major><![CDATA[Physics]]></major><description><![CDATA[Nuclear physics]]></description><address><city><![CDATA[Springfield]]></city><country><![CDATA[USA]]></country></address></school></education><education><courses><![CDATA[Course1]]></courses><courses><![CDATA[Course2]]></courses></education><statements><honors><![CDATA[Award1]]></honors><honors><![CDATA[Award2]]></honors></statements></resume></ResDoc>");
        public static readonly TestResume InvalidLocation = new TestResume(TestResumeType.InvalidLocation, "InvalidLocation.doc", GetInvalidLocationResume, "<ResDoc><contact><name><givenname><![CDATA[Homer]]></givenname><surname><![CDATA[Simpson]]></surname></name><address><street><![CDATA[1414 Evergreen Terrace]]></street><city><![CDATA[London]]></city></address><phone><![CDATA[5555 5555]]></phone><email><![CDATA[hsimpson@test.linkme.net.au]]></email><personal><dob><![CDATA[November 1960]]></dob></personal></contact><resume><objective><![CDATA[Some objective]]></objective><summary><![CDATA[Some summary]]></summary><other><![CDATA[Some other]]></other><professional><![CDATA[Some professional]]></professional><skills><![CDATA[Some skills]]></skills><statements><citizenship><![CDATA[American]]></citizenship><affiliations><![CDATA[Some affiliations]]></affiliations><activities><![CDATA[Some interests]]></activities><references><![CDATA[Some referees]]></references></statements><experience><job><title><![CDATA[The nuclear technician]]></title><description><![CDATA[Nuclear technician]]></description><employer><![CDATA[Springfield Nuclear]]></employer><daterange><start><![CDATA[February 1990]]></start><end><![CDATA[current]]></end></daterange></job></experience><education><school><daterange><end><![CDATA[May 1984]]></end></daterange><institution><![CDATA[Mann Eye Institute]]></institution><degree><![CDATA[Science]]></degree><major><![CDATA[Physics]]></major><description><![CDATA[Nuclear physics]]></description><address><city><![CDATA[Springfield]]></city><country><![CDATA[USA]]></country></address></school></education><education><courses><![CDATA[Course1]]></courses><courses><![CDATA[Course2]]></courses></education><statements><honors><![CDATA[Award1]]></honors><honors><![CDATA[Award2]]></honors></statements></resume></ResDoc>");
        public static readonly TestResume NoSchoolDate = new TestResume(TestResumeType.NoSchoolDate, "NoSchoolDate.doc", GetNoSchoolDateResume, "<ResDoc><contact><name><givenname><![CDATA[Homer]]></givenname><surname><![CDATA[Simpson]]></surname></name><address><street><![CDATA[1414 Evergreen Terrace]]></street><city><![CDATA[South Yarra VIC 3141]]></city></address><phone><![CDATA[5555 5555]]></phone><email><![CDATA[hsimpson@test.linkme.net.au]]></email><personal><dob><![CDATA[November 1960]]></dob></personal></contact><resume><objective><![CDATA[Some objective]]></objective><summary><![CDATA[Some summary]]></summary><other><![CDATA[Some other]]></other><professional><![CDATA[Some professional]]></professional><skills><![CDATA[Some skills]]></skills><statements><citizenship><![CDATA[American]]></citizenship><affiliations><![CDATA[Some affiliations]]></affiliations><activities><![CDATA[Some interests]]></activities><references><![CDATA[Some referees]]></references></statements><experience><job><title><![CDATA[The nuclear technician]]></title><description><![CDATA[Nuclear technician]]></description><employer><![CDATA[Springfield Nuclear]]></employer><daterange><start><![CDATA[February 1990]]></start><end><![CDATA[current]]></end></daterange></job></experience><education><school><institution><![CDATA[Mann Eye Institute]]></institution><degree><![CDATA[Science]]></degree><major><![CDATA[Physics]]></major><description><![CDATA[Nuclear physics]]></description><address><city><![CDATA[Springfield]]></city><country><![CDATA[USA]]></country></address></school></education><education><courses><![CDATA[Course1]]></courses><courses><![CDATA[Course2]]></courses></education><statements><honors><![CDATA[Award1]]></honors><honors><![CDATA[Award2]]></honors></statements></resume></ResDoc>");
        public static readonly TestResume FutureSchoolDate = new TestResume(TestResumeType.FutureSchoolDate, "FutureSchoolDate.doc", GetFutureSchoolDateResume, "<ResDoc><contact><name><givenname><![CDATA[Homer]]></givenname><surname><![CDATA[Simpson]]></surname></name><address><street><![CDATA[1414 Evergreen Terrace]]></street><city><![CDATA[South Yarra VIC 3141]]></city></address><phone><![CDATA[5555 5555]]></phone><email><![CDATA[hsimpson@test.linkme.net.au]]></email><personal><dob><![CDATA[November 1960]]></dob></personal></contact><resume><objective><![CDATA[Some objective]]></objective><summary><![CDATA[Some summary]]></summary><other><![CDATA[Some other]]></other><professional><![CDATA[Some professional]]></professional><skills><![CDATA[Some skills]]></skills><statements><citizenship><![CDATA[American]]></citizenship><affiliations><![CDATA[Some affiliations]]></affiliations><activities><![CDATA[Some interests]]></activities><references><![CDATA[Some referees]]></references></statements><experience><job><title><![CDATA[The nuclear technician]]></title><description><![CDATA[Nuclear technician]]></description><employer><![CDATA[Springfield Nuclear]]></employer><daterange><start><![CDATA[February 1990]]></start><end><![CDATA[current]]></end></daterange></job></experience><education><school><daterange><end><![CDATA[2014]]></end></daterange><institution><![CDATA[Mann Eye Institute]]></institution><degree><![CDATA[Science]]></degree><major><![CDATA[Physics]]></major><description><![CDATA[Nuclear physics]]></description><address><city><![CDATA[Springfield]]></city><country><![CDATA[USA]]></country></address></school></education><education><courses><![CDATA[Course1]]></courses><courses><![CDATA[Course2]]></courses></education><statements><honors><![CDATA[Award1]]></honors><honors><![CDATA[Award2]]></honors></statements></resume></ResDoc>");
        public static readonly TestResume FutureJobDate = new TestResume(TestResumeType.FutureJobDate, "FutureJobDate.doc", GetFutureJobDateResume, "<ResDoc><contact><name><givenname><![CDATA[Homer]]></givenname><surname><![CDATA[Simpson]]></surname></name><address><street><![CDATA[1414 Evergreen Terrace]]></street><city><![CDATA[South Yarra VIC 3141]]></city></address><phone><![CDATA[5555 5555]]></phone><email><![CDATA[hsimpson@test.linkme.net.au]]></email><personal><dob><![CDATA[November 1960]]></dob></personal></contact><resume><objective><![CDATA[Some objective]]></objective><summary><![CDATA[Some summary]]></summary><other><![CDATA[Some other]]></other><professional><![CDATA[Some professional]]></professional><skills><![CDATA[Some skills]]></skills><statements><citizenship><![CDATA[American]]></citizenship><affiliations><![CDATA[Some affiliations]]></affiliations><activities><![CDATA[Some interests]]></activities><references><![CDATA[Some referees]]></references></statements><experience><job><title><![CDATA[The nuclear technician]]></title><description><![CDATA[Nuclear technician]]></description><employer><![CDATA[Springfield Nuclear]]></employer><daterange><start><![CDATA[2010]]></start><end><![CDATA[2014]]></end></daterange></job></experience><education><school><daterange><end><![CDATA[May 1984]]></end></daterange><institution><![CDATA[Mann Eye Institute]]></institution><degree><![CDATA[Science]]></degree><major><![CDATA[Physics]]></major><description><![CDATA[Nuclear physics]]></description><address><city><![CDATA[Springfield]]></city><country><![CDATA[USA]]></country></address></school></education><education><courses><![CDATA[Course1]]></courses><courses><![CDATA[Course2]]></courses></education><statements><honors><![CDATA[Award1]]></honors><honors><![CDATA[Award2]]></honors></statements></resume></ResDoc>");

        public static readonly TestResume[] AllResumes = new[]
        {
            Complete, Empty, Invalid, Unavailable, NoName, NoEmailAddress, NoPhoneNumber, NoLocation, InvalidLocation, FutureSchoolDate, FutureJobDate
        };

        private TestResume(TestResumeType type, string fileName, Func<ParsedResume> getParsedResume, string lensXml)
        {
            _type = type;
            _fileName = fileName;
            _getParsedResume = getParsedResume;
            _lensXml = lensXml;
        }

        public TestResumeType Type
        {
            get { return _type; }
        }

        public string FileName
        {
            get { return _fileName; }
        }

        public string DocType
        {
            get { return Path.GetExtension(_fileName); }
        }

        public ParsedResume GetParsedResume()
        {
            return _getParsedResume();
        }

        public byte[] GetData()
        {
            return BitConverter.GetBytes((int)_type);
        }

        public string GetLensXml()
        {
            return _lensXml;
        }

        private static ParsedResume GetCompleteResume()
        {
            return new ParsedResume
            {
                FirstName = "Homer",
                LastName = "Simpson",
                DateOfBirth = new PartialDate(1960, 11),
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = "hsimpson@test.linkme.net.au", IsVerified = false } },
                Address = new ParsedAddress { Location = "South Yarra VIC 3141", Street = "1414 Evergreen Terrace" },
                PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = "5555 5555", Type = PhoneNumberType.Work } },
                Resume = new Resume
                {
                    Affiliations = "Some affiliations",
                    Awards = new List<string> { "Award1", "Award2" },
                    Citizenship = "American",
                    Courses = new List<string> { "Course1", "Course2" },
                    Interests = "Some interests",
                    Objective = "Some objective",
                    Other = "Some other",
                    Professional = "Some professional",
                    Referees = "Some referees",
                    Skills = "Some skills",
                    Summary = "Some summary",
                    Jobs = new List<Job>
                    {
                        new Job
                        {
                            Company = "Springfield Nuclear",
                            Description = "Nuclear technician",
                            Title = "The nuclear technician",
                            Dates = new PartialDateRange(new PartialDate(1990, 2)),
                        }
                    },
                    Schools = new List<School>
                    {
                        new School
                        {
                            City = "Springfield",
                            Country = "USA",
                            CompletionDate = new PartialCompletionDate(new PartialDate(1984, 5)),
                            Degree = "Science",
                            Description = "Nuclear physics",
                            Institution = "Mann Eye Institute",
                            Major = "Physics",
                        }
                    }
                }
            };
        }

        private static ParsedResume GetNoNameResume()
        {
            var parsedResume = GetCompleteResume();
            parsedResume.FirstName = null;
            parsedResume.LastName = null;
            return parsedResume;
        }

        private static ParsedResume GetNoEmailAddressResume()
        {
            var parsedResume = GetCompleteResume();
            parsedResume.EmailAddresses = null;
            return parsedResume;
        }
        
        private static ParsedResume GetNoPhoneNumberResume()
        {
            var parsedResume = GetCompleteResume();
            parsedResume.PhoneNumbers = null;
            return parsedResume;
        }

        private static ParsedResume GetNoLocationResume()
        {
            var parsedResume = GetCompleteResume();
            parsedResume.Address.Location = null;
            return parsedResume;
        }

        private static ParsedResume GetInvalidLocationResume()
        {
            var parsedResume = GetCompleteResume();
            parsedResume.Address.Location = "London";
            return parsedResume;
        }

        private static ParsedResume GetNoSchoolDateResume()
        {
            var parsedResume = GetCompleteResume();
            parsedResume.Resume.Schools[0].CompletionDate = null;
            return parsedResume;
        }

        private static ParsedResume GetFutureSchoolDateResume()
        {
            var parsedResume = GetCompleteResume();
            parsedResume.Resume.Schools[0].CompletionDate = new PartialCompletionDate(new PartialDate(DateTime.Now.AddYears(2).Year));
            return parsedResume;
        }

        private static ParsedResume GetFutureJobDateResume()
        {
            var parsedResume = GetCompleteResume();
            parsedResume.Resume.Jobs[0].Dates = new PartialDateRange(new PartialDate(DateTime.Now.AddYears(-2).Year), new PartialDate(DateTime.Now.AddYears(2).Year));
            return parsedResume;
        }
    }
}
