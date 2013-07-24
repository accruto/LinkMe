using System;
using System.Text.RegularExpressions;
using LinkMe.Domain;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;

namespace LinkMe.Apps.Services.JobAds.Salaries
{
    public class JobAdSalariesParserCommand
        : IJobAdSalariesParserCommand
    {
        private readonly IJobAdsQuery _jobAdsQuery;

        private readonly IJobAdsCommand _jobAdsCommand;

        private const string BasicKMatchPattern = @"\$\d?\d\dk";     //$99k or $999k
        private const string BasicMatchPattern = @"\$\d?\d\d,?[5|0]00";     //$99,500, $99,000, $99500 or $99000

        private const string BasicKMatchOptionalDollarPattern = @"\$?\d?\d\dk";     //$99k or $999k or 99k or 999k
        private const string BasicMatchOptionalDollarPattern = @"\$?\d?\d\d,?[5|0]00";     //$99,500, $99,000, $99500 or $99000
        private const string BasicKMatchOptionalKPattern = @"\$\d?\d\dk?";     //$99k or $999k or $99 or $999
        private const string RangePattern = @"\s?-\s?";        //[ ]-[ ]
        private const string ToPattern = @"to\s?";             //to[ ]
        private const string FromPattern = @"from\s?";           //from[ ]

        private const string BasicKRangeMatchPattern = BasicKMatchOptionalKPattern + RangePattern + BasicKMatchOptionalDollarPattern;
        private const string BasicRangeMatchPattern = BasicMatchPattern + RangePattern + BasicMatchOptionalDollarPattern;

        private const string BasicKMatchMinPattern = FromPattern + BasicKMatchPattern;
        private const string BasicKMatchMaxPattern = ToPattern + BasicKMatchPattern;
        private const string BasicMatchMinPattern = FromPattern + BasicMatchPattern;
        private const string BasicMatchMaxPattern = ToPattern + BasicMatchPattern;

        private static readonly Regex BasicKMatchRegEx = new Regex(BasicKMatchPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex BasicKRangeMatchRegEx = new Regex(BasicKRangeMatchPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex BasicMatchRegEx = new Regex(BasicMatchPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex BasicRangeMatchRegEx = new Regex(BasicRangeMatchPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex BasicKMatchMinRegEx = new Regex(BasicKMatchMinPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex BasicKMatchMaxRegEx = new Regex(BasicKMatchMaxPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex BasicMatchMinRegEx = new Regex(BasicMatchMinPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex BasicMatchMaxRegEx = new Regex(BasicMatchMaxPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private const decimal SalaryConversionMidToMin = .8888889M;
        private const decimal SalaryConversionMidToMax = 1.111111M;
        private const decimal SalaryConversionMinToMax = 1.25M;
        private const decimal SalaryConversionMaxToMin = .8M;

        public JobAdSalariesParserCommand(IJobAdsQuery jobAdsQuery, IJobAdsCommand jobAdsCommand)
        {
            _jobAdsQuery = jobAdsQuery;

            _jobAdsCommand = jobAdsCommand;
        }

        public void ParseJobAdSalaries(bool limitToOpenJobAds)
        {
            var jobAdIds = limitToOpenJobAds
                ? _jobAdsQuery.GetOpenJobAdsWithoutSalaries()
                : _jobAdsQuery.GetJobAdsWithoutSalaries();

            if (jobAdIds == null || jobAdIds.Count == 0)
                return;

            foreach (var id in jobAdIds)
            {
                var jobAd = _jobAdsQuery.GetJobAd<JobAd>(id);

                var parsedSalary = new Salary
                                       {
                                           Currency = Currency.AUD,
                                           Rate = SalaryRate.Year
                                       };

                ParseSalaryFromText(jobAd.Description.Content, ref parsedSalary);

                if (parsedSalary == null || parsedSalary.IsEmpty)
                    ParseSalaryFromText(jobAd.Title, ref parsedSalary);

                if (parsedSalary == null || parsedSalary.IsEmpty)
                {
                    continue;
                }

                if (!ReasonableSalary(parsedSalary))
                {
                    continue;
                }

                jobAd.Description.ParsedSalary = parsedSalary;
                _jobAdsCommand.UpdateJobAd(jobAd);
            }
        }

        private static void ParseSalaryFromText(string salaryTextToParse, ref Salary parsedSalary)
        {
            var basicKRangeMatch = BasicKRangeMatchRegEx.Match(salaryTextToParse);
            if (basicKRangeMatch.Success)
            {
                //Found a match to $99k[]-[]$99k
                var salaries = basicKRangeMatch.Value.Split('-');

                var salaryOneValue = ConvertSalary(salaries[0], 1000);
                var salaryTwovalue = ConvertSalary(salaries[1], 1000);

                parsedSalary.LowerBound = salaryOneValue;
                parsedSalary.UpperBound = salaryTwovalue;
            }
            else
            {
                var basicKMatch = BasicKMatchRegEx.Match(salaryTextToParse);
                if (basicKMatch.Success)
                {
                    //Found a match to $99k (but not to $99k[]-[]$99k)
                    var salaryValue = ConvertSalary(basicKMatch.Value, 1000);
                    var minSalaryFactor = SalaryConversionMidToMin;
                    var maxSalaryFactor = SalaryConversionMidToMax;

                    //check for from/to matches
                    if (BasicKMatchMinRegEx.Match(salaryTextToParse).Success)
                    {
                        minSalaryFactor = 1;
                        maxSalaryFactor = SalaryConversionMinToMax;
                    }
                    else if (BasicKMatchMaxRegEx.Match(salaryTextToParse).Success)
                    {
                        minSalaryFactor = SalaryConversionMaxToMin;
                        maxSalaryFactor = 1;
                    }

                    parsedSalary.LowerBound = salaryValue * minSalaryFactor;
                    parsedSalary.UpperBound = salaryValue * maxSalaryFactor;
                }
                else
                {
                    var basicRangeMatch = BasicRangeMatchRegEx.Match(salaryTextToParse);
                    if (basicRangeMatch.Success)
                    {
                        //Found a match to $99[,][5|0]00[]-[]$99[,][5|0]00
                        var salaries = basicRangeMatch.Value.Split('-');

                        var salaryOneValue = ConvertSalary(salaries[0], 1);
                        var salaryTwovalue = ConvertSalary(salaries[1], 1);

                        parsedSalary.LowerBound = salaryOneValue;
                        parsedSalary.UpperBound = salaryTwovalue;
                    }
                    else
                    {
                        var basicMatch = BasicMatchRegEx.Match(salaryTextToParse);
                        if (basicMatch.Success)
                        {
                            //Found a match to $99[,][5|0]00 (but not to $99[,][5|0]00[]-[]$99[,][5|0]00)
                            var salaryValue = ConvertSalary(basicMatch.Value, 1);
                            var minSalaryFactor = SalaryConversionMidToMin;
                            var maxSalaryFactor = SalaryConversionMidToMax;

                            //check for from/to matches
                            if (BasicMatchMinRegEx.Match(salaryTextToParse).Success)
                            {
                                minSalaryFactor = 1;
                                maxSalaryFactor = SalaryConversionMinToMax;
                            }
                            else if (BasicMatchMaxRegEx.Match(salaryTextToParse).Success)
                            {
                                minSalaryFactor = SalaryConversionMaxToMin;
                                maxSalaryFactor = 1;
                            }

                            parsedSalary.LowerBound = salaryValue * minSalaryFactor;
                            parsedSalary.UpperBound = salaryValue * maxSalaryFactor;
                        }
                    }
                }
            }
        }

        private static decimal ConvertSalary(string salary, int factor)
        {
           if (!string.IsNullOrEmpty(salary))
                salary = salary.ToLower()
                    .Replace("k", string.Empty)
                    .Replace(",", string.Empty)
                    .Replace("$", string.Empty);

            var salaryVal = 0;

            if (int.TryParse(salary, out salaryVal))
            {
                if (factor != 0)
                    salaryVal = salaryVal*factor;
            }

            return Convert.ToDecimal(salaryVal);
        }

        private static bool ReasonableSalary(Salary parsedSalary)
        {
            return parsedSalary.HasLowerBound
                ? (parsedSalary.LowerBound > 10000 && parsedSalary.LowerBound < 400000)
                : true
                    &&
                    parsedSalary.HasUpperBound
                    ? (parsedSalary.UpperBound > 10000 && parsedSalary.UpperBound < 400000)
                    : true;

        }
    }
}
