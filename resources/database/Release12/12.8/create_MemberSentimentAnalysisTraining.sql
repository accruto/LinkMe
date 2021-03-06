CREATE TABLE [dbo].[MemberSentimentAnalysisTraining](
	[id] [uniqueidentifier] NOT NULL,
	[resumeId] [uniqueidentifier] NOT NULL,
	[classification] tinyint NOT NULL,
	[memberTrainingText] [text] NOT NULL
 CONSTRAINT [PK_MemberSentimentAnalysisTraining] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)
