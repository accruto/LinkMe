IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('SurveyResponse') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE SurveyResponse
GO

CREATE TABLE SurveyResponse ( 
	id uniqueidentifier NOT NULL,
	time datetime NOT NULL,
	answerId uniqueidentifier NOT NULL,
	researcherId uniqueidentifier NOT NULL
)
GO

ALTER TABLE SurveyResponse ADD CONSTRAINT PK_SurveyResponse 
	PRIMARY KEY CLUSTERED (id)
GO

ALTER TABLE SurveyResponse ADD CONSTRAINT FK_SurveyResponse_Researcher 
	FOREIGN KEY (researcherId) REFERENCES Researcher (id)
GO

ALTER TABLE SurveyResponse ADD CONSTRAINT FK_SurveyResponse_SurveyAnswer 
	FOREIGN KEY (answerId) REFERENCES SurveyAnswer (id)
GO





