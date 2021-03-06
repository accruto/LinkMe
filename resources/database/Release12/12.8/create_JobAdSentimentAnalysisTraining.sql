CREATE TABLE [dbo].[JobAdSentimentAnalysisTraining](
	[id] [uniqueidentifier] NOT NULL,
	[jobAdId] [uniqueidentifier] NOT NULL,
	[classification] tinyint NOT NULL,
	[jobAdTrainingText] [text] NOT NULL
 CONSTRAINT [PK_JobAdSentimentAnalysisTraining] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) 
