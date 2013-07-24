using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;

namespace LinkMe.Apps.Services.Test.Employers.Files
{
    public static class TestEmployerFileRecordExtensions
    {
        public static string GetEmployerFileText(this IEnumerable<Tuple<IEmployer, string>> employers)
        {
            var sb = new StringBuilder();

            // Headings.

            sb.Append("\"OrganisationName\",\"LoginId\",\"FirstName\",\"LastName\",\"Enabled\",\"EmailAddress\"");

            // Employers.

            var count = employers.Count();
            if (count > 0)
            {
                sb.AppendLine();
                foreach (var employer in employers.Take(count - 1))
                    sb.AppendEmployer(employer).AppendLine();
                sb.AppendEmployer(employers.Last());
            }

            return sb.ToString();
        }

        private static StringBuilder AppendEmployer(this StringBuilder sb, Tuple<IEmployer, string> employer)
        {
            sb.Append("\"").Append(employer.Item1.Organisation.FullName.Replace(new string(Organisation.FullNameSeparator, 1), "->"))
                .Append("\",\"").Append(employer.Item2)
                .Append("\",\"").Append(employer.Item1.FirstName)
                .Append("\",\"").Append(employer.Item1.LastName)
                .Append("\",\"").Append(employer.Item1.IsEnabled ? "Enabled" : "Disabled")
                .Append("\",\"").Append(employer.Item1.EmailAddress.Address)
                .Append("\"");
            return sb;
        }
    }
}
