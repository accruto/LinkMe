using System;
using System.Text;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Services.External.JobSearch
{
    public static class Mappings
    {
        public static void MapPhoneNumber(this ContactDetails contact, bool hideContactDetails, out string phoneAreaCode, out string phoneLocalNumber)
        {
            phoneAreaCode = "00";
            phoneLocalNumber = "00000000";

            if (contact == null || hideContactDetails || string.IsNullOrEmpty(contact.PhoneNumber))
                return;

            var phoneNumber = new StringBuilder(contact.PhoneNumber);
            phoneNumber.Replace(" ", string.Empty);
            phoneNumber.Replace("(", string.Empty);
            phoneNumber.Replace(")", string.Empty);
            phoneNumber.Replace("-", string.Empty);

            if (phoneNumber[0] != '+')
            {
                ParseFnn(phoneNumber.ToString(), contact.PhoneNumber, out phoneAreaCode, out phoneLocalNumber);
            }
            else
            {
                if (phoneNumber.Length <= 3)
                {
                    throw new ArgumentException(string.Format("'{0}' is an invalid phone number.", contact.PhoneNumber));
                }

                if (phoneNumber[1] != '6' || phoneNumber[2] != '1')
                {
                    throw new ArgumentException(string.Format("'{0}' is not an Australian phone number.",
                        contact.PhoneNumber));
                }

                phoneNumber.Remove(0, 3); // remove country code

                if (phoneNumber[0] != '1' && phoneNumber[0] != '0')
                    phoneNumber.Insert(0, '0');

                ParseFnn(phoneNumber.ToString(), contact.PhoneNumber, out phoneAreaCode, out phoneLocalNumber);
            }
        }

        public static void Map(this LocationReference location, out string state, out string postcode, out string suburb)
        {
            state = null;
            postcode = null;
            suburb = null;

            if (location == null)
                return;

            if (location.Suburb != null)
                suburb = location.Suburb.ToUpper();

            postcode = location.Postcode;
            state = location.CountrySubdivision.ShortName;

            if (!string.IsNullOrEmpty(postcode) && !string.IsNullOrEmpty(suburb) && !string.IsNullOrEmpty(state))
                return;

            // All of postcode, suburb and state are required
            // If any are missing set to the main location for the state

            var urlName = location.Region != null
                ? location.Region.UrlName
                : location.CountrySubdivision.UrlName;

            if (string.IsNullOrEmpty(urlName))
                return;

            if (urlName.Equals("australian-capital-territory") || urlName.StartsWith("canberra") || urlName.StartsWith("regional-act"))
            {
                postcode = "2600";
                suburb = "CANBERRA";
                state = "ACT";
            }
            else if (urlName.Equals("new-south-wales") || urlName.StartsWith("sydney") || urlName.StartsWith("regional-nsw"))
            {
                postcode = "2000";
                suburb = "SYDNEY";
                state = "NSW";
            }
            else if (urlName.Equals("northern-territory") || urlName.StartsWith("darwin") || urlName.StartsWith("regional-nt"))
            {
                postcode = "0800";
                suburb = "DARWIN";
                state = "NT";
            }
            else if (urlName.Equals("queensland") || urlName.StartsWith("brisbane") || urlName.StartsWith("regional-qld"))
            {
                postcode = "4000";
                suburb = "BRISBANE";
                state = "QLD";
            }
            else if (urlName.Equals("south-australia") || urlName.StartsWith("adelaide") || urlName.StartsWith("regional-sa"))
            {
                postcode = "5000";
                suburb = "ADELAIDE";
                state = "SA";
            }
            else if (urlName.Equals("tasmania") || urlName.StartsWith("hobart") || urlName.StartsWith("regional-tas"))
            {
                postcode = "7000";
                suburb = "HOBART";
                state = "TAS";
            }
            else if (urlName.Equals("victoria") || urlName.StartsWith("melbourne") || urlName.StartsWith("regional-vic"))
            {
                postcode = "3000";
                suburb = "MELBOURNE";
                state = "VIC";
            }
            else if (urlName.Equals("western-australia") || urlName.StartsWith("perth") || urlName.StartsWith("regional-wa"))
            {
                postcode = "6000";
                suburb = "PERTH";
                state = "WA";
            }
            else if (urlName.Equals("gold-coast"))
            {
                postcode = "4217";
                suburb = "GOLD COAST";
                state = "QLD";
            }
        }

        public static void Map(this JobTypes jobType, out string workType, out string durationType)
        {
            workType = "F"; // Full time, Part time, Casual

            if (jobType.IsFlagSet(JobTypes.PartTime))
                workType = "P";

            durationType = "P"; // Permanent, Temporary, coNtract

            if (jobType.IsFlagSet(JobTypes.Contract))
                durationType = "N";
            else if (jobType.IsFlagSet(JobTypes.Temp))
                durationType = "T";
        }

        private static void ParseFnn(string phoneNumber, string phoneNumberOriginal, out string phoneAreaCode, out string phoneLocalNumber)
        {
            if (phoneNumber[0] == '0')
            {
                if (phoneNumber.Length != 10)
                    throw new ArgumentException(string.Format("'{0}' is an invalid phone number.", phoneNumberOriginal));

                phoneAreaCode = phoneNumber.Substring(0, 2);
                phoneLocalNumber = phoneNumber.Substring(2, phoneNumber.Length - 2);
            }
            else
            {
                if (phoneNumber.Length <= 4)
                    throw new ArgumentException(string.Format("'{0}' is an invalid phone number.", phoneNumberOriginal));

                phoneAreaCode = phoneNumber.Substring(0, 4);
                phoneLocalNumber = phoneNumber.Substring(4, phoneNumber.Length - 4);
            }
        }
    }
}
