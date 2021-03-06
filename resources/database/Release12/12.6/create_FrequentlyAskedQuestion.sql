CREATE TABLE [dbo].[FrequentlyAskedQuestion](
	[id] [uniqueidentifier] NOT NULL,
	[faqSubcategoryId] [uniqueidentifier] NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[text] [text] NOT NULL,
	[createdTime] [datetime] NOT NULL,
	[helpfulYes] [int] NOT NULL CONSTRAINT [DF_FrequentlyAskedQuestions_helpfulYes]  DEFAULT ((0)),
	[helpfulNo] [int] NOT NULL CONSTRAINT [DF_FrequentlyAskedQuestions_helpflNo]  DEFAULT ((0)),
 CONSTRAINT [PK_FrequentlyAskedQuestion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)

GO
ALTER TABLE [dbo].[FrequentlyAskedQuestion]  WITH CHECK ADD  CONSTRAINT [FK_FrequentlyAskedQuestion_faqSubcategory] FOREIGN KEY([faqSubcategoryId])
REFERENCES [dbo].[FAQSubcategory] ([id])
GO
ALTER TABLE [dbo].[FrequentlyAskedQuestion] CHECK CONSTRAINT [FK_FrequentlyAskedQuestion_faqSubcategory]