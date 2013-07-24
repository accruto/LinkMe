using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Roles.Resumes.Commands
{
    public abstract class ParseResumeXmlCommand
        : IParseResumeXmlCommand
    {
        ParsedResume IParseResumeXmlCommand.ParseResumeXml(string xml)
        {
            return ParseResumeXml(xml);
        }

        protected abstract ParsedResume ParseResumeXml(string xml);

        protected ParsedResume GetParsedResume(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return new ParsedResume { Resume = new Resume() };

            var xmlDoc = new XmlDocument { PreserveWhitespace = true };
            xmlDoc.LoadXml(xml);

            return new ParsedResume
            {
                FirstName = GetFirstName(xmlDoc),
                LastName = GetLastName(xmlDoc),
                Address = GetAddress(xmlDoc),
                DateOfBirth = GetDateOfBirth(xmlDoc),
                EmailAddresses = GetEmailAddresses(xmlDoc),
                PhoneNumbers = GetPhoneNumbers(xmlDoc),
                Resume = new Resume
                {
                    Jobs = GetJobs(xmlDoc),
                    Schools = GetSchools(xmlDoc),
                    Courses = GetCourses(xmlDoc),
                    Awards = GetAwards(xmlDoc),
                    Skills = GetSkills(xmlDoc),
                    Objective = GetObjective(xmlDoc),
                    Summary = GetSummary(xmlDoc),
                    Other = GetOther(xmlDoc),
                    Citizenship = GetCitizenship(xmlDoc),
                    Affiliations = GetAffiliations(xmlDoc),
                    Professional = GetProfessional(xmlDoc),
                    Interests = GetInterests(xmlDoc),
                    Referees = GetReferees(xmlDoc),
                },
            };
        }

        protected abstract string GetFirstName(XmlDocument xmlDoc);
        protected abstract string GetLastName(XmlDocument xmlDoc);
        protected abstract ParsedAddress GetAddress(XmlDocument xmlDoc);
        protected abstract PartialDate? GetDateOfBirth(XmlDocument xmlDoc);
        protected abstract IList<EmailAddress> GetEmailAddresses(XmlDocument xmlDoc);
        protected abstract IList<PhoneNumber> GetPhoneNumbers(XmlDocument xmlDoc);
        protected abstract IList<Job> GetJobs(XmlDocument xmlDoc);
        protected abstract IList<School> GetSchools(XmlDocument xmlDoc);
        protected abstract IList<string> GetCourses(XmlDocument xmlDoc);
        protected abstract IList<string> GetAwards(XmlDocument xmlDoc);
        protected abstract string GetSkills(XmlDocument xmlDoc);
        protected abstract string GetObjective(XmlDocument xmlDoc);
        protected abstract string GetSummary(XmlDocument xmlDoc);
        protected abstract string GetOther(XmlDocument xmlDoc);
        protected abstract string GetCitizenship(XmlDocument xmlDoc);
        protected abstract string GetAffiliations(XmlDocument xmlDoc);
        protected abstract string GetProfessional(XmlDocument xmlDoc);
        protected abstract string GetInterests(XmlDocument xmlDoc);
        protected abstract string GetReferees(XmlDocument xmlDoc);

        protected static string[] GetAll(XmlNode node, params string[] paths)
        {
            var list = new List<string>();
            foreach (var path in paths)
                GetAll(list, node, path);
            return list.ToArray();
        }

        private static void GetAll(ICollection<string> list, XmlNode node, string path)
        {
            var childNodes = node.SelectNodes(path);
            if (childNodes != null)
            {
                foreach (XmlNode childNode in childNodes)
                    GetAllTexts(list, childNode);
            }
        }

        protected static string Get(XmlNode node, string path)
        {
            var childNodes = node.SelectNodes(path);
            if (childNodes == null || childNodes.Count == 0)
                return null;

            foreach (XmlNode childNode in childNodes)
            {
                var text = GetText(childNode);
                if (!string.IsNullOrEmpty(text))
                    return text;
            }

            return null;
        }

        private static string GetText(XmlNode node)
        {
            var sb = new StringBuilder();

            foreach (var childNode in node.ChildNodes)
            {
                string text = null;
                if (childNode is XmlText)
                    text = ((XmlText)childNode).Value;
                else if (childNode is XmlCDataSection)
                    text = ((XmlCDataSection)childNode).Value;

                if (!string.IsNullOrEmpty(text))
                {
                    // Remove all line breaks before adding it.

                    text = text.Replace("\r\n", " ");
                    text = text.Replace("\n", " ");
                    if (sb.Length != 0)
                        sb.Append(" ");
                    sb.Append(text);
                }
            }

            return sb.ToString().CollapseSpaces().Trim().NullIfEmpty();
        }

        private static void GetAllTexts(ICollection<string> list, XmlNode node)
        {
            foreach (var childNode in node.ChildNodes)
            {
                string text = null;
                if (childNode is XmlText)
                    text = ((XmlText)childNode).Value;
                else if (childNode is XmlCDataSection)
                    text = ((XmlCDataSection)childNode).Value;

                if (!string.IsNullOrEmpty(text))
                {
                    // Split at line breaks before adding.

                    var splitTexts = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var splitText in splitTexts)
                    {
                        var trimmedText = splitText.Trim();
                        if (trimmedText.Length > 0)
                        {
                            if (!list.Contains(trimmedText))
                                list.Add(trimmedText);
                        }
                    }
                }
            }
        }
    }
}