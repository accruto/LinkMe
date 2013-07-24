using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Framework.Utility.Files;

namespace LinkMe.Apps.Services.Employers.Files
{
    public class EmployerFileRecord
    {
        [FileItemOrder(1)]
        public string OrganisationName { get; set; }

        [FileItemOrder(2)]
        public string LoginId { get; set; }

        [FileItemOrder(3)]
        public string FirstName { get; set; }

        [FileItemOrder(4)]
        public string LastName { get; set; }

        public bool IsEnabled { get; set; }

        [FileItemOrder(5)]
        public string Enabled
        {
            get { return (IsEnabled ? "Enabled" : "Disabled"); }
        }

        [FileItemOrder(6)]
        public string EmailAddress { get; set; }
    }

    public static class EmployerFileRecordExtensions
    {
        private static IList<EmployerFileRecord> ToFileRecords(this IEnumerable<Tuple<IEmployer, string>> employers)
        {
            return (from e in employers
                    select new EmployerFileRecord
                    {
                        OrganisationName = e.Item1.Organisation.FullName.Replace(new string(Organisation.FullNameSeparator, 1), "->"),
                        LoginId = e.Item2,
                        FirstName = e.Item1.FirstName,
                        LastName = e.Item1.LastName,
                        IsEnabled = e.Item1.IsEnabled,
                        EmailAddress = e.Item1.EmailAddress.Address
                    }).ToList();
        }

        public static Stream ToFileStream(this IEnumerable<Tuple<IEmployer, string>> employers)
        {
            // Create the stream.

            var stream = new MemoryStream();
            var formatter = new FileFormatter(new DelimitedFormatProvider(Delimiter.Comma));
            formatter.Format(employers.ToFileRecords(), stream);

            // Write it out.

            stream.Position = 0;
            return stream;
        }
    }
}
