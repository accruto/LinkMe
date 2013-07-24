IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('SurveyAnswer') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE SurveyAnswer
GO

CREATE TABLE SurveyAnswer ( 
	id uniqueidentifier NOT NULL,
	surveyId uniqueidentifier NOT NULL,
	answerText nvarchar(100) NULL
)
GO

ALTER TABLE SurveyAnswer ADD CONSTRAINT PK_SurveyAnswer 
	PRIMARY KEY CLUSTERED (id)
GO

ALTER TABLE SurveyAnswer ADD CONSTRAINT FK_surveyId 
	FOREIGN KEY (surveyId) REFERENCES Survey (id)
GO




