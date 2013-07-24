using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.PhoneNumbers.Queries;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Domain.Roles.Resumes.Lens
{
    public class LensParseResumeXmlCommand
        : ParseResumeXmlCommand
    {
        private const string PostingTag = "<posting>";
        private static readonly Regex TaggedRegEx = new Regex(@"<\?xml version='1\.0'.*\?>", RegexOptions.Compiled);
        private static readonly Regex PostingRegEx = new Regex(@"<posting.*?>", RegexOptions.Compiled);
        private const string ResumeTemplateXml = "<ResDoc><contact></contact><resume></resume></ResDoc>";
        private static readonly char[] JobDateSeparatorChars = new[] { ' ', '\t', ':', '-' };
        private const string CurrentDateText = "current";
        private const string PresentDateText = "present";
        private const int SentinelYear = 1950;
        private const string Country = "Australia";
        private static readonly EventSource EventSource = new EventSource<LensParseResumeXmlCommand>();

        private readonly IPhoneNumbersQuery _phoneNumbersQuery;
        private readonly ILocationQuery _locationQuery;

        public LensParseResumeXmlCommand(IPhoneNumbersQuery phoneNumbersQuery, ILocationQuery locationQuery)
        {
            _phoneNumbersQuery = phoneNumbersQuery;
            _locationQuery = locationQuery;
        }

        protected override ParsedResume ParseResumeXml(string xml)
        {
            const string method = "ParseResumeXml";

            try
            {
                // Do some clean up of the xml first.

                xml = TaggedRegEx.Replace(xml, "");

                var pos = xml.IndexOf("<JobDoc>");
                if (pos > 0)
                {
                    xml = xml.Replace("<JobDoc>", "");
                    xml = xml.Replace("</JobDoc>", "");
                    xml = xml.Replace("<title>", "");
                    xml = xml.Replace("</title>", "");
                    xml = PostingRegEx.Replace(xml, PostingTag);
                }

                if (xml == null || xml.Trim().Length == 0)
                    xml = ResumeTemplateXml;
                return GetParsedResume(xml.StripInvalidAsciiCharsForXml());
            }
            catch (LensInvalidDocumentException ex)
            {
                EventSource.Raise(Event.Warning, method, ex, null);
                throw new InvalidResumeException(ex);
            }
            catch (LensException ex)
            {
                EventSource.Raise(Event.Error, method, ex, null);
                throw new ParserUnavailableException(ex);
            }
        }

        protected override string GetFirstName(XmlDocument xmlDoc)
        {
            return Get(xmlDoc, "//contact/name/givenname");
        }

        protected override string GetLastName(XmlDocument xmlDoc)
        {
            return Get(xmlDoc, "//contact/name/surname");
        }

        protected override ParsedAddress GetAddress(XmlDocument xmlDoc)
        {
            var addressNode = xmlDoc.SelectSingleNode("//contact/address");
            if (addressNode == null)
                return null;

            var street = Get(addressNode, "street");
            var city = Get(addressNode, "city");
            var state = Get(addressNode, "state");
            var postalCode = Get(addressNode, "postalcode");

            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(postalCode))
                sb.Append(sb.Length == 0 ? postalCode : " " + postalCode);
            if (!string.IsNullOrEmpty(city))
                sb.Append(sb.Length == 0 ? city : " " + city);
            if (!string.IsNullOrEmpty(state))
                sb.Append(sb.Length == 0 ? state : " " + state);
            var location = sb.ToString().NullIfEmpty();

            return string.IsNullOrEmpty(street) && string.IsNullOrEmpty(location)
                ? null
                : new ParsedAddress { Street = street, Location = location };
        }

        protected override PartialDate? GetDateOfBirth(XmlDocument xmlDoc)
        {
            var dob = Get(xmlDoc, "//contact/personal/dob");
            if (string.IsNullOrEmpty(dob))
            {
                dob = Get(xmlDoc, "//resume/statements/personal/dob");
                if (string.IsNullOrEmpty(dob))
                    return null;
            }

            PartialDate? date;
            PartialDate.TryParse(dob, out date);
            return date;
        }

        protected override IList<EmailAddress> GetEmailAddresses(XmlDocument xmlDoc)
        {
            var emailAddress = Get(xmlDoc, "//contact/email");
            if (string.IsNullOrEmpty(emailAddress))
                return null;

            return new List<EmailAddress>
            {
                new EmailAddress
                {
                    Address = emailAddress,
                    IsVerified = false,
                }
            };
        }

        protected override IList<PhoneNumber> GetPhoneNumbers(XmlDocument xmlDoc)
        {
            var phoneNumber = Get(xmlDoc, "//contact/phone");
            if (string.IsNullOrEmpty(phoneNumber))
                return null;

            return new List<PhoneNumber>
            {
                new PhoneNumber
                {
                    Number = phoneNumber,
                    Type = _phoneNumbersQuery.GetPhoneNumberType(phoneNumber, _locationQuery.GetCountry(Country)),
                }
            };
        }

        protected override IList<Job> GetJobs(XmlDocument xmlDoc)
        {
            IList<Job> jobs = new List<Job>();

            var jobNodes = xmlDoc.SelectNodes("//resume/experience/job");
            if (jobNodes == null || jobNodes.Count == 0)
                return jobs;

            foreach (XmlNode jobNode in jobNodes)
            {
                var description = GetAll(jobNode, "description");
                var title = Get(jobNode, "title");
                if (title != null)
                    title = title.TrimStart(JobDateSeparatorChars);
                var company = Get(jobNode, "employer");
                var start = Get(jobNode, "daterange/start");
                var end = Get(jobNode, "daterange/end");

                if ((description != null && description.Length > 0) || !string.IsNullOrEmpty(title) || !string.IsNullOrEmpty(company) || !string.IsNullOrEmpty(start) || !string.IsNullOrEmpty(end))
                {
                    var job = new Job
                    {
                        Title = title,
                        Description = description == null || description.Length == 0 ? null : string.Join("\r\n", description),
                        Company = company,
                        Dates = GetDates(start, end),
                    };
                    jobs.Add(job);
                }
            }

            return jobs;
        }

        protected override IList<School> GetSchools(XmlDocument xmlDoc)
        {
            var schools = new List<School>();

            var schoolNodes = xmlDoc.SelectNodes("//resume/education/school");
            if (schoolNodes == null || schoolNodes.Count == 0)
                return schools;

            foreach (XmlNode schoolNode in schoolNodes)
            {
                var start = Get(schoolNode, "daterange/start");
                var end = Get(schoolNode, "daterange/end");
                var institution = Get(schoolNode, "institution");
                var degree = Get(schoolNode, "degree");
                var major = Get(schoolNode, "major");
                var description = GetAll(schoolNode, "description");
                var city = Get(schoolNode, "address/city");
                var country = Get(schoolNode, "address/country");

                if ((description != null && description.Length > 0) || !string.IsNullOrEmpty(end) || !string.IsNullOrEmpty(institution) || !string.IsNullOrEmpty(degree) || !string.IsNullOrEmpty(major))
                {
                    var dates = GetDates(start, end);
                    var school = new School
                    {
                        CompletionDate = dates == null
                            ? null
                            : dates.End == null
                                ? new PartialCompletionDate()
                                : new PartialCompletionDate(dates.End.Value),
                        Institution = institution,
                        Degree = degree,
                        Major = major,
                        Description = description == null || description.Length == 0 ? null : string.Join("\r\n", description),
                        City = city,
                        Country = country
                    };
                    schools.Add(school);
                }
            }

            return schools;
        }

        protected override IList<string> GetCourses(XmlDocument xmlDoc)
        {
            return GetAll(xmlDoc, "//resume/education/courses", "//resume/education/school/courses", "//resume/skills/courses", "//resume/professional/courses", "//resume/experience/job/courses");
        }

        protected override IList<string> GetAwards(XmlDocument xmlDoc)
        {
            return GetAll(xmlDoc, "//resume/education/honors", "//resume/education/honors", "//resume/professional/honors", "//resume/statements/honors", "//resume/education/school/honors");
        }

        protected override string GetSkills(XmlDocument xmlDoc)
        {
            return string.Join("\r\n", GetAll(xmlDoc, "//skills", "//skill", "//resume/skills/skills")).NullIfEmpty();
        }

        protected override string GetObjective(XmlDocument xmlDoc)
        {
            return string.Join("\r\n", GetAll(xmlDoc, "//resume/objective", "//resume/summary/objective")).NullIfEmpty();
        }

        protected override string GetSummary(XmlDocument xmlDoc)
        {
            return string.Join("\r\n", GetAll(xmlDoc, "//resume/summary", "//resume/summary/summary")).NullIfEmpty();
        }

        protected override string GetOther(XmlDocument xmlDoc)
        {
            return string.Join("\r\n", GetAll(xmlDoc, "//resume/other")).NullIfEmpty();
        }

        protected override string GetCitizenship(XmlDocument xmlDoc)
        {
            return string.Join("\r\n", GetAll(xmlDoc, "//resume/statements/citizenship", "//resume/statements/personal/citizenship")).NullIfEmpty();
        }

        protected override string GetAffiliations(XmlDocument xmlDoc)
        {
            return string.Join("\r\n", GetAll(xmlDoc, "//resume/statements/affiliations", "//resume/professional/affiliations")).NullIfEmpty();
        }

        protected override string GetProfessional(XmlDocument xmlDoc)
        {
            return string.Join("\r\n", GetAll(xmlDoc, "//resume/professional")).NullIfEmpty();
        }

        protected override string GetInterests(XmlDocument xmlDoc)
        {
            return string.Join("\r\n", GetAll(xmlDoc, "//resume/statements/activities")).NullIfEmpty();
        }

        protected override string GetReferees(XmlDocument xmlDoc)
        {
            return string.Join("\r\n", GetAll(xmlDoc, "//resume/statements/references", "//references")).NullIfEmpty();
        }

        private static PartialDateRange GetDates(string start, string end)
        {
            // Everything from her on is to deal with LENS and what seems to be a free-text field at one stage.

            // If both are empty then don't return anything.

            if (string.IsNullOrEmpty(start) && string.IsNullOrEmpty(end))
                return null;

            try
            {
                PartialDate? startDate;
                if (!TryGetDate(start, true, out startDate))
                    return null;

                PartialDate? endDate;
                if (!TryGetDate(end, false, out endDate))
                    return null;

                // Check for the right order.

                if (startDate != null && endDate != null)
                {
                    if (startDate.Value.CompareTo(endDate.Value) > 0)
                    {
                        // Just swap around.

                        var temp = endDate;
                        endDate = startDate;
                        startDate = temp;
                    }
                }

                if (startDate == null && endDate == null)
                    return null;

                if (startDate != null && endDate == null)
                    return new PartialDateRange(startDate.Value);

                return new PartialDateRange(startDate, endDate.Value);
            }
            catch (Exception)
            {
                // Just return nothing if there is any sort of error trying to parse the dates.

                return null;
            }
        }

        private static bool TryGetDate(string dateString, bool isStart, out PartialDate? date)
        {
            dateString = Clean(dateString);

            if (string.Compare(dateString, CurrentDateText, StringComparison.InvariantCultureIgnoreCase) == 0
                || string.Compare(dateString, PresentDateText, StringComparison.InvariantCultureIgnoreCase) == 0
                || string.IsNullOrEmpty(dateString))
            {
                date = null;
                return true;
            }

            // Try to parse as DateTime directly. Only interested in month and year.

            DateTime datetime;
            if (DateTime.TryParse(dateString, out datetime))
            {
                if (datetime.Year <= SentinelYear)
                {
                    date = null;
                    return true;
                }

                date = new PartialDate(datetime.Year, datetime.Month);
                return true;
            }

            // May be just a year so try to parse that.

            int year;
            if (int.TryParse(dateString, out year))
            {
                if (year <= SentinelYear)
                {
                    date = null;
                    return true;
                }

                date = new PartialDate(year, isStart ? 1 : 12);
                return true;
            }

            date = null;
            return false;
        }

        private static string Clean(string dt)
        {
            // Sometimes dates are in the form of e.g. Dec'05, change to Dec 05.

            return dt == null ? null : dt.Replace('\'', ' ').Replace('`', ' ').Replace(':', ' ');
        }
    }
}