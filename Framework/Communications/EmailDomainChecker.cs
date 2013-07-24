using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using LinkMe.Environment;

namespace LinkMe.Framework.Communications
{
    [Serializable]
    public class EmailDomainNotAllowedException
        : ApplicationException
    {
        internal EmailDomainNotAllowedException(string disallowedEmailAddress, string allowedEmailDomains)
            : base(string.Format("An email was addressed to '{0}', which is not in the list of allowed email domains for the {1} environment. If it should be, add the domain to the '{2}' application setting.", disallowedEmailAddress, RuntimeEnvironment.EnvironmentName, allowedEmailDomains))
        {
        }

        internal EmailDomainNotAllowedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    public class EmailDomainChecker
    {
        public static void Check(Communication communication, string[] allowedDomains)
        {
            if (RuntimeEnvironment.Environment == ApplicationEnvironment.Dev || RuntimeEnvironment.Environment == ApplicationEnvironment.Uat)
            {
                CheckEmailDomain(allowedDomains, communication.To.EmailAddress);

                if (communication.Copy != null)
                {
                    foreach (var recipient in communication.Copy)
                        CheckEmailDomain(allowedDomains, recipient.EmailAddress);
                }

                if (communication.BlindCopy != null)
                {
                    foreach (var recipient in communication.BlindCopy)
                        CheckEmailDomain(allowedDomains, recipient.EmailAddress);
                }
            }
        }

        private static void CheckEmailDomain(string[] allowedDomains, string addresses)
        {
            if (string.IsNullOrEmpty(addresses))
                return;

            foreach (var address in addresses.Split(',', ';'))
            {
                var trimmed = address.TrimEnd(' ', '>');
                if (!EndsWithAny(trimmed, allowedDomains))
                    throw new EmailDomainNotAllowedException(address, string.Join(",", allowedDomains));
            }
        }

        private static bool EndsWithAny(string address, IEnumerable<string> allowedDomains)
        {
            foreach (var domain in allowedDomains)
            {
                if (address.EndsWith(domain, StringComparison.CurrentCultureIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}