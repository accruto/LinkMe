using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Services.External.Monster.Schema;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Services.External.Monster
{
    public class JobAdMapper
        : IJobFeedMapper<Job>
    {
        private readonly ILocationQuery _locationQuery;
        private readonly string[] _ignoredCompanies;
        private readonly Country _australia;
        private readonly IDictionary<MonsterIndustriesIdEnum, Industry> _industryMap;

        public JobAdMapper(ILocationQuery locationQuery, IIndustriesQuery industriesQuery, string[] ignoredCompanies)
        {
            _locationQuery = locationQuery;
            _ignoredCompanies = ignoredCompanies;
            _australia = _locationQuery.GetCountry("Australia");
            _industryMap = CreateIndustryMap(industriesQuery);
         }

        string IJobFeedMapper<Job>.GetPostId(Job post)
        {
            return post.JobPostings[0].postingId;
        }

        bool IJobFeedMapper<Job>.IsIgnored(Job post)
        {
            if (post.CompanyReference == null || post.CompanyReference.CompanyXCode == null || _ignoredCompanies == null)
                return false;

            return _ignoredCompanies.Contains(post.CompanyReference.CompanyXCode);
        }

        void IJobFeedMapper<Job>.ApplyPostData(Job post, JobAd jobAd)
        {
            jobAd.Integration.ExternalApplyUrl = string.Format("http://jobview.careerone.com.au/GetJob.aspx?JobID={0}&WT.mc_n=AFC_linkme", post.JobPostings[0].postingId);
            jobAd.Integration.ExternalReferenceId = post.jobRefCode;

            var posting = post.JobPostings[0];
            jobAd.CreatedTime = posting.JobPostingDates.JobPostDate.Value;
            jobAd.LastUpdatedTime = posting.JobPostingDates.JobModifiedDate.Value;  

            var postInfo = post.JobInformation;

            jobAd.ExpiryTime = posting.JobPostingDates.JobExpireDate.Value;
            jobAd.Title = HttpUtility.HtmlDecode(postInfo.JobTitle);

            if (postInfo.Contact == null)
            {
                jobAd.ContactDetails = null;
            }
            else
            {
                jobAd.Visibility.HideContactDetails = postInfo.Contact.hideAll || postInfo.Contact.hideContactInfoField;

                var contactDetails = new ContactDetails();
                contactDetails.ParseEmailAddresses(postInfo.Contact.Email);

                // Check the email address and simply ignore it if it is invalid.

                if (!string.IsNullOrEmpty(contactDetails.EmailAddress))
                {
                    var validator = (IValidator) new EmailAddressesValidator();
                    if (!validator.IsValid(new[] { new EmailAddress { Address = contactDetails.EmailAddress } }))
                    {
                        contactDetails.EmailAddress = null;
                        contactDetails.SecondaryEmailAddresses = null;
                    }
                }

                if (!postInfo.Contact.hideCompanyName)
                    contactDetails.CompanyName = postInfo.Contact.CompanyName;

                if (postInfo.Contact.Phones != null)
                {
                    if (!postInfo.Contact.hidePhone)
                        contactDetails.PhoneNumber = (from p in postInfo.Contact.Phones
                                                      where p.phoneType == PhoneTypePhoneType.contact
                                                      let n = p.Value.GetPhoneNumber()
                                                      where n != null
                                                      select n).SingleOrDefault();

                    if (!postInfo.Contact.hideFax)
                        contactDetails.FaxNumber = (from p in postInfo.Contact.Phones
                                                    where p.phoneType == PhoneTypePhoneType.fax
                                                    let n = p.Value.GetPhoneNumber()
                                                    where n != null
                                                    select n).SingleOrDefault();
                }

                jobAd.ContactDetails = contactDetails.IsEmpty ? null : contactDetails;
            }

            // Company

            jobAd.Visibility.HideCompany = postInfo.HideCompanyInfoSpecified && postInfo.HideCompanyInfo;
            jobAd.Description.CompanyName = post.CompanyReference.CompanyName;

            // Content

            jobAd.Description.Content = postInfo.JobBody.Value;

            // JobTypes

            jobAd.Description.JobTypes = JobTypes.FullTime;

            if (postInfo.JobStatus != null)
            {
                switch (postInfo.JobStatus[0].monsterId)
                {
                    case JobStatusIdEnum.FullTime:
                        jobAd.Description.JobTypes = JobTypes.FullTime;
                        break;

                    case JobStatusIdEnum.PartTime:
                        jobAd.Description.JobTypes = JobTypes.PartTime;
                        break;

                    case JobStatusIdEnum.DailyOrHourlyRate:
                        jobAd.Description.JobTypes = JobTypes.Temp;
                        break;
                }
            }

            if (postInfo.JobType != null)
            {
                switch (postInfo.JobType[0].monsterId)
                {
                    case JobTypeIdEnum.TemporaryOrContract:
                        jobAd.Description.JobTypes = JobTypes.Contract;
                        break;

                    case JobTypeIdEnum.Casual:
                        jobAd.Description.JobTypes = JobTypes.Temp;
                        break;
                }
            }

            // Location

            jobAd.Description.Location = _locationQuery.ResolveLocation(_australia, string.Format("{0} {1} {2}",
                postInfo.PhysicalAddress.City, postInfo.PhysicalAddress.State, postInfo.PhysicalAddress.PostalCode));

            //Salary

            if (postInfo.Salary != null)
            {
                jobAd.Description.Salary = new Salary {Currency = Currency.AUD};

                if (postInfo.Salary.SalaryMinSpecified)
                    jobAd.Description.Salary.LowerBound = postInfo.Salary.SalaryMin;

                if (postInfo.Salary.SalaryMaxSpecified)
                    jobAd.Description.Salary.UpperBound = postInfo.Salary.SalaryMax;

                if (postInfo.Salary.SalaryMinSpecified || postInfo.Salary.SalaryMaxSpecified)
                {
                    jobAd.Description.Salary.Rate = SalaryRate.Year;

                    if (postInfo.Salary.CompensationType != null)
                    {
                        switch (postInfo.Salary.CompensationType.monsterId)
                        {
                            case CompensationTypeIdEnum.PerYear:
                                jobAd.Description.Salary.Rate = SalaryRate.Year;
                                break;

                            case CompensationTypeIdEnum.PerHour:
                                jobAd.Description.Salary.Rate = SalaryRate.Hour;
                                break;

                            case CompensationTypeIdEnum.PerWeek:
                                jobAd.Description.Salary.Rate = SalaryRate.Week;
                                break;

                            case CompensationTypeIdEnum.PerMonth:
                                jobAd.Description.Salary.Rate = SalaryRate.Month;
                                break;

                            case CompensationTypeIdEnum.PerDay:
                                jobAd.Description.Salary.Rate = SalaryRate.Day;
                                break;

                            case CompensationTypeIdEnum.Biweekly:
                                jobAd.Description.Salary.Rate = SalaryRate.Week;
                                jobAd.Description.Salary.LowerBound /= 2;
                                jobAd.Description.Salary.UpperBound /= 2;
                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }

            // Industries

            if (posting.Industries != null)
            {
                var industries = posting.Industries.Where(i => i.IndustryName.monsterId != MonsterIndustriesIdEnum.All).ToArray();

                if (industries.Any())
                    jobAd.Description.Industries = Array.ConvertAll(industries, mi => _industryMap[mi.IndustryName.monsterId]);
            }
        }

        private static IDictionary<MonsterIndustriesIdEnum, Industry> CreateIndustryMap(IIndustriesQuery industriesQuery)
        {
            var industryMap = new[]
            {
                Tuple.Create(MonsterIndustriesIdEnum.Accounting, "accounting"),              
                Tuple.Create(MonsterIndustriesIdEnum.Administrative, "administration"),
                Tuple.Create(MonsterIndustriesIdEnum.Advertising, "advertising-media-entertainment"),
                Tuple.Create(MonsterIndustriesIdEnum.AerospaceDefense, "government-defence"),
                Tuple.Create(MonsterIndustriesIdEnum.Agriculture, "primary-industry"),
                Tuple.Create(MonsterIndustriesIdEnum.Architecture, "construction"),
                Tuple.Create(MonsterIndustriesIdEnum.AutomotiveMfg, "manufacturing-operations"),
                Tuple.Create(MonsterIndustriesIdEnum.AutomotiveServices, "automotive"),
                Tuple.Create(MonsterIndustriesIdEnum.Banking, "banking-financial-services"),
                Tuple.Create(MonsterIndustriesIdEnum.BiotechnologyPharmaceuticals, "healthcare-medical-pharmaceutical"),
                Tuple.Create(MonsterIndustriesIdEnum.BroadcastingMusicFilm, "advertising-media-entertainment"),
                Tuple.Create(MonsterIndustriesIdEnum.BusinessServices, "consulting-corporate-strategy"),
                Tuple.Create(MonsterIndustriesIdEnum.Chemicals, "manufacturing-operations"),
                Tuple.Create(MonsterIndustriesIdEnum.ComputerHardware, "it-telecommunications"),
                Tuple.Create(MonsterIndustriesIdEnum.ComputerSoftware, "it-telecommunications"),
                Tuple.Create(MonsterIndustriesIdEnum.ComputerServices, "it-telecommunications"),
                Tuple.Create(MonsterIndustriesIdEnum.ConstructionIndustrial, "construction"),
                Tuple.Create(MonsterIndustriesIdEnum.ConstructionResidential, "construction"),
                Tuple.Create(MonsterIndustriesIdEnum.Education, "education-training"),
                Tuple.Create(MonsterIndustriesIdEnum.ElectronicsMfg, "manufacturing-operations"),
                Tuple.Create(MonsterIndustriesIdEnum.EnergyUtilities, "primary-industry"),
                Tuple.Create(MonsterIndustriesIdEnum.Engineering, "engineering"),
                Tuple.Create(MonsterIndustriesIdEnum.EntertainmentVenues, "advertising-media-entertainment"),
                Tuple.Create(MonsterIndustriesIdEnum.FinancialServices, "banking-financial-services"),
                Tuple.Create(MonsterIndustriesIdEnum.FoodProduction, "primary-industry"),
                Tuple.Create(MonsterIndustriesIdEnum.GovernmentMilitary, "government-defence"),
                Tuple.Create(MonsterIndustriesIdEnum.GovernmentDefense, "government-defence"),
                Tuple.Create(MonsterIndustriesIdEnum.GovernmentNational, "government-defence"),
                Tuple.Create(MonsterIndustriesIdEnum.Grocery, "retail-consumer-products"),
                Tuple.Create(MonsterIndustriesIdEnum.Healthcare, "healthcare-medical-pharmaceutical"),
                Tuple.Create(MonsterIndustriesIdEnum.Hotels, "hospitality-tourism"),
                Tuple.Create(MonsterIndustriesIdEnum.Insurance, "insurance-superannuation"),
                Tuple.Create(MonsterIndustriesIdEnum.Internet, "it-telecommunications"),
                Tuple.Create(MonsterIndustriesIdEnum.Legal, "legal"),
                Tuple.Create(MonsterIndustriesIdEnum.ManagementCompanies, "administration"),
                Tuple.Create(MonsterIndustriesIdEnum.ManagementServices, "administration"),
                Tuple.Create(MonsterIndustriesIdEnum.Manufacturing, "manufacturing-operations"),
                Tuple.Create(MonsterIndustriesIdEnum.Marine, "hospitality-tourism"),
                Tuple.Create(MonsterIndustriesIdEnum.MedicalSupplies, "healthcare-medical-pharmaceutical"),
                Tuple.Create(MonsterIndustriesIdEnum.MetalsMinerals, "mining-oil-gas"),
                Tuple.Create(MonsterIndustriesIdEnum.Mortgage, "banking-financial-services"),
                Tuple.Create(MonsterIndustriesIdEnum.NonprofitOrganizations, "other"),
                Tuple.Create(MonsterIndustriesIdEnum.Nursing, "healthcare-medical-pharmaceutical"),
                Tuple.Create(MonsterIndustriesIdEnum.Other, "other"),
                Tuple.Create(MonsterIndustriesIdEnum.PerformingArts, "advertising-media-entertainment"),
                Tuple.Create(MonsterIndustriesIdEnum.PersonalServices, "other"),
                Tuple.Create(MonsterIndustriesIdEnum.PersonalCare, "trades-services"),
                Tuple.Create(MonsterIndustriesIdEnum.Printing, "advertising-media-entertainment"),
                Tuple.Create(MonsterIndustriesIdEnum.RealEstate, "real-estate-property"),
                Tuple.Create(MonsterIndustriesIdEnum.RentalServices, "trades-services"),
                Tuple.Create(MonsterIndustriesIdEnum.RepairServices, "trades-services"),
                Tuple.Create(MonsterIndustriesIdEnum.Restaurant, "hospitality-tourism"),
                Tuple.Create(MonsterIndustriesIdEnum.Retail, "retail-consumer-products"),
                Tuple.Create(MonsterIndustriesIdEnum.Security, "trades-services"),
                Tuple.Create(MonsterIndustriesIdEnum.SocialServices, "community-sport"),
                Tuple.Create(MonsterIndustriesIdEnum.SportsRecreation, "community-sport"),
                Tuple.Create(MonsterIndustriesIdEnum.EmploymentAgencies, "hr-recruitment"),
                Tuple.Create(MonsterIndustriesIdEnum.Telecommunications, "it-telecommunications"),
                Tuple.Create(MonsterIndustriesIdEnum.ClothingManufacturing, "manufacturing-operations"),
                Tuple.Create(MonsterIndustriesIdEnum.TradeContractors, "trades-services"),
                Tuple.Create(MonsterIndustriesIdEnum.TransportStorage, "transport-logistics"),
                Tuple.Create(MonsterIndustriesIdEnum.Travel, "hospitality-tourism"),
                Tuple.Create(MonsterIndustriesIdEnum.WasteManagement, "manufacturing-operations"),
                Tuple.Create(MonsterIndustriesIdEnum.Wholesale, "transport-logistics")
            };

            return industryMap.ToDictionary(t => t.Item1, t => industriesQuery.GetIndustryByUrlName(t.Item2));
        }
    }
}
