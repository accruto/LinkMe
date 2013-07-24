using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Domain.Test.Data
{
    public static class DataTestExtensions
    {
        private const int CommandTimeout = 600;

        // The following tables (staticTables, obsoleteTables and tempStateTables) do not need to be cleared.

        private static readonly string[] StaticTables = new[]
        {
            "[dbo].[IndustryAlias]",
            "[dbo].[Industry]",
            "[dbo].[Credit]",
            "[dbo].[Product]",
            "[dbo].[ProductCreditAdjustment]",
            "[dbo].[ProductDefinition]",
            "[dbo].[Country]",
            "[dbo].[CountrySubdivision]",
            "[dbo].[CountrySubdivisionAlias]",
            "[dbo].[LocalityCountrySubdivision]",
            "[dbo].[Locality]",
            "[dbo].[PostalCode]",
            "[dbo].[PostalSuburb]",
            "[dbo].[Region]",
            "[dbo].[LocalityRegion]",
            "[dbo].[GeographicalArea]",
            "[dbo].[NamedLocation]",
            "[dbo].[DonationRecipient]",
            "[dbo].[LocationAbbreviation]",
            "[dbo].[RelativeLocation]",
            "[dbo].[CommunicationDefinition]",
            "[dbo].[CommunicationCategory]",
            "[dbo].[DatabaseScript]",
            "[dbo].[SpamText]",
            "[dbo].[ReferralSource]",
            "[dbo].[AtsIntegrator]",
            "[dbo].[IntegratorUser]"
        };

        private static readonly string[] ObsoleteTables = new string[0];

        private static readonly string[] TempStateTables =
        {
            "[dbo].[Sequence]"
        };

        // A list of all tables in the LinkMe database in the correct order for deletion.
        private static readonly string[] AllTables =
        {
            "[dbo].[ReferralSource]",
            "[dbo].[CampaignCriteria]",
            "[dbo].[CampaignSearch]",
            "[dbo].[CampaignCriteriaSet]",
            "[dbo].[CampaignTemplate]",
            "[dbo].[Campaign]",
            "[dbo].[CandidateAccessPurchase]",
            "[dbo].[CandidateAssessment]",
            "[dbo].[CandidateDiaryEntry]",
            "[dbo].[CandidateIndustry]",
            "[dbo].[CandidateListEntry]",
            "[dbo].[CandidateNote]",
            "[dbo].[CandidateWorkflow]",
            "[dbo].[CommunicationCategorySettings]",
            "[dbo].[CommunicationDefinitionSettings]",
            "[dbo].[CommunicationDefinition]",
            "[dbo].[CommunicationCategory]",
            "[dbo].[CommunicationSettings]",
            "[dbo].[CommunityAdministrator]",
            "[dbo].[CommunityAssociation]",
            "[dbo].[CommunityEmployerEnquiry]",
            "[dbo].[CommunityMemberData]",
            "[dbo].[CommunityMember]",
            "[dbo].[CommunityOrganisationalUnit]",
            "[dbo].[CommunityOwner]",
            "[dbo].[Community]",
            "[dbo].[ContentDetail]",
            "[dbo].[ContentItem]",
            "[dbo].[CountrySubdivisionAlias]",
            "[dbo].[CreditAdjustment]",
            "[dbo].[CreditAdjustmentReason]",
            "[dbo].[CreditAllocation]",
            "[dbo].[Credit]",
            "[dbo].[DatabaseScript]",
            "[dbo].[DiscussionBoardModerator]",
            "[dbo].[DiscussionBoardSubscriber]",
            "[dbo].[DiscussionPost]",
            "[dbo].[DiscussionSubscriber]",
            "[dbo].[Discussion]",
            "[dbo].[EmailLinkStats]",
            "[dbo].[EmailSendStats]",
            "[dbo].[EmailStats]",
            "[dbo].[EmailVerification]",
            "[dbo].[EmployerIndustry]",
            "[dbo].[EquivalentTerms]",
            "[dbo].[GroupAffiliation]",
            "[dbo].[GroupAllowedEmailDomain]",
            "[dbo].[GroupEventAttendee]",
            "[dbo].[GroupEventCoordinator]",
            "[dbo].[GroupEventInvitation]",
            "[dbo].[GroupEvents]",
            "[dbo].[GroupEvent]",
            "[dbo].[GroupFile]",
            "[dbo].[GroupIndustry]",
            "[dbo].[GroupJoinInvitation]",
            "[dbo].[GroupMembership]",
            "[dbo].[GroupMembershipBanned]",
            "[dbo].[IgnoredNetworkMatch]",
            "[dbo].[IndustryAlias]",
            "[dbo].[JobAdIndustry]",
            "[dbo].[Industry]",
            "[dbo].[JobAdLocation]",
            "[dbo].[JobApplication]",
            "[dbo].[ExternalApplication]",
            "[dbo].[JobSearch]",
            "[dbo].[JobSearchCriteria]",
            "[dbo].[JobSearchResult]",
            "[dbo].[JobSearchResultSet]",
            "[dbo].[JoinReferral]",
            "[dbo].[LocalityCountrySubdivision]",
            "[dbo].[LocalityRegion]",
            "[dbo].[LocationAbbreviation]",
            "[dbo].[MemberIndexing]",
            "[dbo].[Member]",
            "[dbo].[MessageThreadParticipant]",
            "[dbo].[NetworkerReference]",
            "[dbo].[NetworkInvitation]",
            "[dbo].[RepresentativeInvitation]",
            "[dbo].[DonationRequest]",
            "[dbo].[DonationRecipient]",
            "[dbo].[NetworkLink]",
            "[dbo].[NonMemberSettings]",
            "[dbo].[Offer]",
            "[dbo].[OfferCategoryOffering]",
            "[dbo].[OfferingCriteria]",
            "[dbo].[OfferingCriteriaSet]",
            "[dbo].[OfferingLocation]",
            "[dbo].[Offering]",
            "[dbo].[OfferProvider]",
            "[dbo].[OfferRequest]",
            "[dbo].[OfferCategory]",
            "[dbo].[OrganisationalUnit]",
            "[dbo].[PostalSuburb]",
            "[dbo].[PostalCode]",
            "[dbo].[ProductCouponProduct]",
            "[dbo].[ProductCouponRedeemer]",
            "[dbo].[ProductCoupon]",
            "[dbo].[ProductCreditAdjustment]",
            "[dbo].[ProductDefinition]",
            "[dbo].[ProductOrderItem]",
            "[dbo].[ProductOrderAdjustment]",
            "[dbo].[Product]",
            "[dbo].[ProductReceipt]",
            "[dbo].[ProductOrder]",
            "[dbo].[PurchaseTransaction]",
            "[dbo].[Region]",
            "[dbo].[RelativeLocation]",
            "[dbo].[RelocationLocation]",
            "[dbo].[ReportParameter]",
            "[dbo].[ResumeSearch]",
            "[dbo].[ResumeSearchCriteria]",
            "[dbo].[ResumeSearchResult]",
            "[dbo].[ResumeSearchResultSet]",
            "[dbo].[SavedSearchAlertResult]",
            "[dbo].[SavedResumeSearchAlert]",
            "[dbo].[SavedResumeSearch]",
            "[dbo].[ResumeSearchCriteriaSet]",
            "[dbo].[SentReport]",
            "[dbo].[ReportDefinition]",
            "[dbo].[ReportParameterSet]",
            "[dbo].[Administrator]",
            "[dbo].[Sequence]",
            "[dbo].[Spammer]",
            "[dbo].[SpammerReport]",
            "[dbo].[SpamText]",
            "[dbo].[SurveyResponse]",
            "[dbo].[SurveyAnswer]",
            "[dbo].[Survey]",
            "[dbo].[Researcher]",
            "[dbo].[TaskRunnerStats]",
            "[dbo].[TinyUrlMapping]",
            "[dbo].[UserContentRemovalRequest]",
            "[dbo].[UserContentItem]",
            "[dbo].[Group]",
            "[dbo].[DiscussionBoard]",
            "[dbo].[JobAdRefresh]",
            "[dbo].[JobAdIndexing]",
            "[dbo].[JobAdExport]",
            "[dbo].[JobAdStatus]",
            "[dbo].[JobAdViewing]",
            "[dbo].[JobAdNote]",
            "[dbo].[JobAdListEntry]",
            "[dbo].[JobAdList]",
            "[dbo].[JobAd]",
            "[dbo].[AnonymousContact]",
            "[dbo].[AnonymousUser]",
            "[dbo].[ContactDetails]",
            "[dbo].[CandidateList]",
            "[dbo].[Employer]",
            "[dbo].[ServicePartner]",
            "[dbo].[RegisteredUser]",
            "[dbo].[Organisation]",
            "[dbo].[LinkedInProfileIndustry]",
            "[dbo].[LinkedInProfile]",
            "[dbo].[Address]",
            "[dbo].[LocationReference]",
            "[dbo].[CountrySubdivision]",
            "[dbo].[Locality]",
            "[dbo].[GeographicalArea]",
            "[dbo].[NamedLocation]",
            "[dbo].[Country]",
            "[dbo].[JobPoster]",
            "[dbo].[IntegratorUser]",
            "[dbo].[AtsIntegrator]",
            "[dbo].[CandidateResume]",
            "[dbo].[CandidateResumeFile]",
            "[dbo].[Candidate]",
            "[dbo].[ParsedResume]",
            "[dbo].[ResumeJob]",
            "[dbo].[ResumeSchool]",
            "[dbo].[Resume]",
            "[dbo].[SavedJobSearch]",
            "[dbo].[SavedJobSearchAlert]",
            "[dbo].[JobSearchCriteriaSet]",
            "[dbo].[CandidateDiary]",
            "[dbo].[MessageThread]",
            "[dbo].[FileReference]",
            "[dbo].[FileData]",
            "[dbo].[UserToUserRequest]",
            "[dbo].[Vertical]",
            "[dbo].[WhatImDoingHistory]",
            "[dbo].[Networker]",
            "[dbo].[WhiteboardMessage]",
            "[dbo].[Whiteboard]",
            "[dbo].[ExternalUser]",
            "[dbo].[Representative]",
            "[dbo].[MemberViewing]",
            "[dbo].[MemberContact]",
            "[dbo].[UserActivation]",
            "[dbo].[UserDeactivation]",
            "[dbo].[UserEnablement]",
            "[dbo].[UserDisablement]",
            "[dbo].[UserLogin]",
            "[dbo].[EmployerMemberMessageAttachment]",
            "[dbo].[EmployerMemberMessage]",
            "[dbo].[EmployerMemberAttachment]",
            "[dbo].[UserAppleDevice]",
            "[dbo].[ResourceIndexing]",
            "[dbo].[ResourceQuestion]",
            "[dbo].[ResourceViewing]",
            "[dbo].[ResourceArticleUserRating]",
            "[dbo].[ResourcePollAnswerVote]",
            "[dbo].[ResourcePollAnswer]",
            "[dbo].[ResourcePoll]",
            "[dbo].[MemberProfile]",
            "[dbo].[EmployerProfile]",
            "[dbo].[ResumeEvent]",
            "[dbo].[JobAdExportCloseEvent]",
            "[dbo].[JobAdExportPostEvent]",
            "[dbo].[JobAdExportFeedEvent]",
            "[dbo].[JobAdExportFeedIdEvent]",
            "[dbo].[JobAdImportCloseEvent]",
            "[dbo].[JobAdImportPostEvent]",
            "[dbo].[JobAdIntegrationEvent]"
        };

        private static readonly string DeleteStatements
            = GetDeleteStatements(GetListExceptFor(AllTables, StaticTables, ObsoleteTables, TempStateTables));

        public static void DeleteAllTestData(this IDbConnectionFactory connectionFactory)
        {
            DeleteTestData(connectionFactory, DeleteStatements);
            DeleteTestData(connectionFactory, "DELETE dbo.CommunicationDefinition WHERE name LIKE 'Campaign:%'");
            DeleteTestData(connectionFactory, "DELETE dbo.ProductCreditAdjustment WHERE productId IN (SELECT id FROM dbo.Product WHERE name LIKE 'Product%')");
            DeleteTestData(connectionFactory, "DELETE dbo.Product WHERE name LIKE 'Product%'");
            DeleteTestData(connectionFactory, "DELETE dbo.IntegratorUser WHERE integratorId NOT IN (SELECT id FROM dbo.AtsIntegrator WHERE name = 'CareerOne' OR name = 'JobG8' OR name = 'MyCareer' OR name = 'JXT')");
            DeleteTestData(connectionFactory, "DELETE dbo.IntegratorUser WHERE integratorId IN (SELECT id FROM dbo.AtsIntegrator WHERE name = 'JobG8') AND username <> 'JobG8'");
            DeleteTestData(connectionFactory, "DELETE dbo.AtsIntegrator WHERE name <> 'CareerOne' AND name <> 'JobG8' AND name <> 'MyCareer' AND name <> 'JXT'");
            DeleteTestData(connectionFactory, "DELETE dbo.ProductCreditAdjustment WHERE productId IN (SELECT id FROM dbo.Product WHERE name LIKE 'Product%')");
            DeleteTestData(connectionFactory, "UPDATE dbo.FrequentlyAskedQuestion SET helpfulYes = 0, helpfulNo = 0");
        }

        public static void DeleteTestData(this IDbConnectionFactory connectionFactory, params string[] tableNames)
        {
            DeleteTestData(connectionFactory, GetDeleteStatements(tableNames));
        }

        public static void DeleteTestData(this IDbConnectionFactory connectionFactory, string statements)
        {
            if (string.IsNullOrEmpty(statements))
                throw new ArgumentException("The statements must be specified.", "statements");

            TestHelper.VerifyDevEnvironment(connectionFactory);

            using (var connection = connectionFactory.CreateConnection())
            {
                connection.Open();

                using (var command = DatabaseHelper.CreateTextCommand(connection, statements, CommandTimeout, null))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error '{0}' executing query {1}", ex.Message, statements);
                        throw;
                    }
                }
            }
        }

        private static string GetDeleteStatements(IEnumerable<string> tableNames)
        {
            var sb = new StringBuilder();

            foreach (var tableName in tableNames)
            {
                sb.Append("DELETE ");
                sb.Append(tableName);
                sb.Append(";\r\n");
            }

            return sb.ToString();
        }

        private static IEnumerable<string> GetListExceptFor(IEnumerable<string> allItems, params string[][] itemsToExclude)
        {
            var list = new List<string>(allItems);

            foreach (var items in itemsToExclude)
            {
                foreach (var item in items)
                {
                    if (!list.Remove(item))
                        Debug.Fail("The item to be removed, '" + item + "', did not exist in the list.");
                }
            }

            return list.ToArray();
        }
    }
}