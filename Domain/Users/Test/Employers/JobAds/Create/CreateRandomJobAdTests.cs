using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds.Create
{
    [TestClass]
    public class CreateRandomJobAdTests
        : CreateJobAdTests
    {
        private const int MaxPackageDetailsLength = 200;
        private const int MaxPositionTitleLength = 200;
        private const int MaxSummaryLength = 300;
        private const int MaxTitleLength = 200;
        private const int MaxEmployerNameLength = Roles.Recruiters.Constants.OrganisationNameMaxLength;
        private const int MaxBulletPoints = 3;
        private const int MaxBulletPointLength = 255;

        // Punctuation characters listed in order from least to most likely to be inserted.

        private static readonly char[] EndSentencePunctuation = new[] { '!', '.' };
        private static readonly char[] AllPunctuation = new[] { ';', ':', '!', '.', ',' };
        private static readonly Random Random = new Random();

        private IList<Industry> _industries;

        private const int MinLocalityId = 20;
        private const int MaxLocalityId = 2817;

        [TestInitialize]
        public void CreateRandomJobAdTestsInitialize()
        {
            _industries = _industriesQuery.GetIndustries();
        }

        [TestMethod]
        public void TestCreateRandomJobAds()
        {
            var poster = new JobPoster { Id = Guid.NewGuid() };
            _jobPostersCommand.CreateJobPoster(poster);

            foreach (var jobAd in GetRandomJobAds(poster.Id, 100))
                _jobAdsCommand.CreateJobAd(jobAd);
        }

        private JobAd[] GetRandomJobAds(Guid posterId, int count)
        {
            var jobAds = new JobAd[count];
            for (var i = 0; i < count; i++)
                jobAds[i] = GetRandomJobAd(posterId, i + 1);
            return jobAds;
        }

        private JobAd GetRandomJobAd(Guid posterId, int number)
        {
            var location = GetRandomLocation();

            var jobAd = new JobAd
                            {
                                CreatedTime = DateTime.Now,
                                PosterId = posterId,
                                Description = { BulletPoints = GetBulletPoints() },
                            };

            jobAd.Description.Content = GetRandomText(Random.Next(10, 301), 5000);
            jobAd.Description.CompanyName = GetRandomEmployerName();
            jobAd.ExpiryTime = DateTime.Now.AddDays(Random.Next(40));
            jobAd.Integration.ExternalReferenceId = "RANDOM" + number;
            jobAd.Description.Industries = GetOptionalRandomListSubset(_industries, 3);
            jobAd.Description.JobTypes = (JobTypes)Random.Next(1, (int)JobTypes.All);
            jobAd.LastUpdatedTime = DateTime.Now;
            jobAd.Description.Location = location;
            jobAd.Description.Package = GetOptionalRandomText(MaxPackageDetailsLength);
            jobAd.Description.PositionTitle = GetRandomText(MaxPositionTitleLength);
            jobAd.Description.ResidencyRequired = !TestHelper.OneInXChance(10);
            jobAd.Description.Salary = GetOptionalRandomSalary();
            jobAd.Status = JobAdStatus.Open;
            jobAd.Description.Summary = GetOptionalRandomText(MaxSummaryLength);
            jobAd.Title = GetRandomText(8, MaxTitleLength);

            string contactFirstName = null, contactLastName = null;
            if (!TestHelper.OneInXChance(4))
            {
                contactFirstName = Capitalise(GetRandomWord(Words.GivenNames));
                contactLastName = Capitalise(GetRandomWord(Words.FamilyNames));
            }
            jobAd.ContactDetails = new ContactDetails
            {
                FirstName = contactFirstName,
                LastName = contactLastName,
                EmailAddress = GetRandomEmail(),
                SecondaryEmailAddresses = null,
                PhoneNumber = GetOptionalRandomPhoneNumber(),
                FaxNumber = GetOptionalRandomPhoneNumber()
            };

            return jobAd;
        }

        private LocationReference GetRandomLocation()
        {
            Exception lastEx = null;

            for (var failCount = 0; failCount < 10; failCount++)
            {
                try
                {
                    var locality = _locationQuery.GetLocality(Random.Next(MinLocalityId, MaxLocalityId), true);
                    return new LocationReference(locality);
                }
                catch (Exception ex)
                {
                    lastEx = ex;
                    failCount++;
                }
            }

            throw new ApplicationException("Failed to get a random Locality after several attempts.", lastEx);
        }

        private static string GetRandomEmployerName()
        {
            return GetRandomText(3, MaxEmployerNameLength);
        }

        private static Salary GetOptionalRandomSalary()
        {
            if (TestHelper.OneInXChance(10))
                return null;

            var minSalary = (TestHelper.OneInXChance(40) ? 0 : 30000);
            var maxSalary = (TestHelper.OneInXChance(40) ? 2000001 : 120001);
            var upper = (TestHelper.OneInXChance(3) ? (int?)null : Random.Next(minSalary, maxSalary));
            var lower = (TestHelper.OneInXChance(3) ? (int?)null : (upper == null ? Random.Next(minSalary, maxSalary) : Random.Next(minSalary, upper.Value)));

            // Generate the salary range as a yearly amount, but then convert it randomly.

            var salary = new Salary { LowerBound = lower, UpperBound = upper, Rate = SalaryRate.Year, Currency = Currency.AUD };
            return salary.ToRate((SalaryRate)Random.Next(1, 5));
        }

        private static List<T> GetOptionalRandomListSubset<T>(IList<T> population, int maxCount)
        {
            if (maxCount > population.Count)
                throw new ArgumentOutOfRangeException("maxCount", maxCount, "maxCount must be <= population.Count");

            var count = Random.Next(maxCount + 1);
            if (count == 0 && TestHelper.OneInXChance(2))
                return null;

            var subset = new T[count];
            var usedIndices = new BitArray(population.Count);

            for (var i = 0; i < count; i++)
            {
                // Do not use the same element from the population array twice.

                var index = Random.Next(population.Count);
                while (usedIndices[index])
                    index = Random.Next(population.Count);

                subset[i] = population[index];
                usedIndices[index] = true;
            }

            return new List<T>(subset);
        }

        private static string GetOptionalRandomText(int maxChars)
        {
            if (TestHelper.OneInXChance(2))
                return (TestHelper.OneInXChance(2) ? null : "");

            return GetRandomText(maxChars);
        }

        private static string[] GetBulletPoints()
        {
            var numBulletPoints = Random.Next(MaxBulletPoints);
            if (numBulletPoints == 0 && TestHelper.OneInXChance(2))
                return null;

            var bulletPoints = new string[numBulletPoints];
            for (var i = 0; i < numBulletPoints; i++)
                bulletPoints[i] = GetRandomText(MaxBulletPointLength);

            return bulletPoints;
        }

        private static string Capitalise(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            if (str.Length == 1)
                return char.ToUpper(str[0]).ToString();
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        private static string GetOptionalRandomPhoneNumber()
        {
            if (TestHelper.OneInXChance(5))
                return (TestHelper.OneInXChance(2) ? null : "");

            return "0" + Random.Next(100000000, 1000000000).ToString("# #### ####");
        }

        private static string GetRandomText(int maxChars)
        {
            return GetRandomText(maxChars / Words.AverageLength, maxChars);
        }

        private static string GetRandomText(int maxWords, int maxChars)
        {
            if (maxWords == 0 || maxChars == 0)
                return (TestHelper.OneInXChance(2) ? null : ""); // Randomly return either null or empty string.

            var sb = new StringBuilder(maxWords * 10);
            var capitalise = true;
            var useLength = 0;

            for (var i = 0; i < maxWords - 1; i++)
            {
                AppendRandomWord(sb, capitalise);
                var appended = AppendRandomChar(sb, AllPunctuation);
                capitalise = (Array.IndexOf(EndSentencePunctuation, appended) != -1);
                sb.Append(' ');

                if (sb.Length >= maxChars)
                    break;

                useLength = sb.Length;
            }

            AppendRandomWord(sb, capitalise);

            if (sb.Length >= maxChars)
            {
                // Truncate after the last letter.

                for (int i = useLength - 1; i >= 0; i--)
                {
                    if (char.IsLetterOrDigit(sb[i]))
                    {
                        useLength = i + 1;
                        break;
                    }
                }

                if (useLength < sb.Length)
                {
                    sb.Remove(useLength, sb.Length - useLength);
                }
            }

            sb.Append(EndSentencePunctuation[Random.Next(EndSentencePunctuation.Length)]);

            return sb.ToString();
        }

        private static string GetRandomWord()
        {
            return GetRandomWord(Words.Keywords);
        }

        private static string GetRandomWord(string[] words)
        {
            return words[Random.Next(words.Length)];
        }

        private static string GetRandomEmail()
        {
            return GetRandomWord() + "@" + GetRandomWord() + ".com";
        }

        private static void AppendRandomWord(StringBuilder sb, bool capitalise)
        {
            var word = GetRandomWord();
            if (capitalise)
                AppendCapitalisedWord(sb, word);
            else
                sb.Append(word);
        }

        private static void AppendCapitalisedWord(StringBuilder sb, string word)
        {
            Debug.Assert(!string.IsNullOrEmpty(word), "!string.IsNullOrEmpty(word)");
            sb.Append(char.ToUpper(word[0]));
            sb.Append(word, 1, word.Length - 1);
        }

        private static char AppendRandomChar(StringBuilder sb, char[] chars)
        {
            var startPower = 30 - chars.Length;
            Debug.Assert(startPower > 10, "startPower > 10");

            var selected = '\0';
            var rand = Random.Next();
            for (var i = chars.Length - 1; i >= 0; i--)
            {
                if (rand < 1 << (startPower + i))
                {
                    selected = chars[i];
                }
            }

            if (selected != '\0')
            {
                sb.Append(selected);
            }

            return selected;
        }
    }
}