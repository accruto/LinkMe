IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ResourcePollAnswerVote]') AND name = N'PK_resourcePollAnswer')
ALTER TABLE [dbo].[ResourcePollAnswerVote] DROP CONSTRAINT [PK_resourcePollAnswer]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_resourcePollAnswer_ResourcePoll]') AND parent_object_id = OBJECT_ID(N'[dbo].[ResourcePollAnswerVote]'))
ALTER TABLE [dbo].[ResourcePollAnswerVote] DROP CONSTRAINT [FK_resourcePollAnswer_ResourcePoll]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ResourcePollAnswer]') AND name = N'PK_ResourcePoll')
ALTER TABLE [dbo].[ResourcePollAnswer] DROP CONSTRAINT [PK_ResourcePoll]
GO

-- ResourcePoll

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.ResourcePoll') AND type in (N'U'))
DROP TABLE dbo.ResourcePoll
GO

CREATE TABLE dbo.ResourcePoll
(
	id UNIQUEIDENTIFIER NOT NULL,
	name NVARCHAR(50) NOT NULL,
	active BIT NOT NULL,
	question NVARCHAR(1024) NOT NULL
)

ALTER TABLE dbo.ResourcePoll
ADD CONSTRAINT PK_ResourcePoll PRIMARY KEY NONCLUSTERED
(
	id
)

CREATE UNIQUE INDEX [IX_ResourcePoll_name] ON [dbo].[ResourcePoll]
(
	name ASC
)
GO

-- Poll data

INSERT
	dbo.ResourcePoll (id, name, active, question)
VALUES
	('{CAEA0C17-E357-4450-B927-B3EAC28CFFCC}', 'Test', 0, 'What is the main reason to begin searching for a new job?')

INSERT
	dbo.ResourcePoll (id, name, active, question)
VALUES
	('{B102C3CE-EA28-4207-B436-2285F616E79C}', 'Mar6', 0, 'What are the 5 hardest problems people face when writing their resume?')

INSERT
	dbo.ResourcePoll (id, name, active, question)
VALUES
	('{DB9C28B4-E95C-4B43-8783-6F66C0EBE1D5}', 'Apr30', 1, 'What are the most common job interview questions?')

-- ResourcePollAnswer

ALTER TABLE dbo.ResourcePollAnswer
ADD pollId UNIQUEIDENTIFIER NULL
GO

ALTER TABLE dbo.ResourcePollAnswer
ADD text NVARCHAR(1024) NULL
GO

ALTER TABLE dbo.ResourcePollAnswer
DROP COLUMN voteCount
GO

-- ResourcePollAnswer data

UPDATE
	dbo.ResourcePollAnswer
SET
	pollId = (SELECT id FROM dbo.ResourcePoll WHERE name = pollName)
GO

ALTER TABLE dbo.ResourcePollAnswer
DROP COLUMN pollName
GO

ALTER TABLE dbo.ResourcePollAnswer
ALTER COLUMN pollId UNIQUEIDENTIFIER NOT NULL
GO

UPDATE
	dbo.ResourcePollAnswer
SET
	text = 'Change in career'
WHERE
	pollId = '{CAEA0C17-E357-4450-B927-B3EAC28CFFCC}' AND answerNumber = 0

UPDATE
	dbo.ResourcePollAnswer
SET
	text = 'More money'
WHERE
	pollId = '{CAEA0C17-E357-4450-B927-B3EAC28CFFCC}' AND answerNumber = 1

UPDATE
	dbo.ResourcePollAnswer
SET
	text = 'Promotional opportunities'
WHERE
	pollId = '{CAEA0C17-E357-4450-B927-B3EAC28CFFCC}' AND answerNumber = 2

UPDATE
	dbo.ResourcePollAnswer
SET
	text = 'Dislike your boss'
WHERE
	pollId = '{CAEA0C17-E357-4450-B927-B3EAC28CFFCC}' AND answerNumber = 3

UPDATE
	dbo.ResourcePollAnswer
SET
	text = 'Fear of losing your job'
WHERE
	pollId = '{CAEA0C17-E357-4450-B927-B3EAC28CFFCC}' AND answerNumber = 4

UPDATE
	dbo.ResourcePollAnswer
SET
	text = 'What information to include'
WHERE
	pollId = '{B102C3CE-EA28-4207-B436-2285F616E79C}' AND answerNumber = 0

UPDATE
	dbo.ResourcePollAnswer
SET
	text = 'Listing achievements'
WHERE
	pollId = '{B102C3CE-EA28-4207-B436-2285F616E79C}' AND answerNumber = 1

UPDATE
	dbo.ResourcePollAnswer
SET
	text = 'Formatting and presentation'
WHERE
	pollId = '{B102C3CE-EA28-4207-B436-2285F616E79C}' AND answerNumber = 2

UPDATE
	dbo.ResourcePollAnswer
SET
	text = 'The length of the resume'
WHERE
	pollId = '{B102C3CE-EA28-4207-B436-2285F616E79C}' AND answerNumber = 3

UPDATE
	dbo.ResourcePollAnswer
SET
	text = 'Spelling and grammar'
WHERE
	pollId = '{B102C3CE-EA28-4207-B436-2285F616E79C}' AND answerNumber = 4

UPDATE
	dbo.ResourcePollAnswer
SET
	text = 'Tell me about yourself'
WHERE
	pollId = '{DB9C28B4-E95C-4B43-8783-6F66C0EBE1D5}' AND answerNumber = 0

UPDATE
	dbo.ResourcePollAnswer
SET
	text = 'What are your strengths and weaknesses?'
WHERE
	pollId = '{DB9C28B4-E95C-4B43-8783-6F66C0EBE1D5}' AND answerNumber = 1

UPDATE
	dbo.ResourcePollAnswer
SET
	text = 'Where do you see yourself in 5 years?'
WHERE
	pollId = '{DB9C28B4-E95C-4B43-8783-6F66C0EBE1D5}' AND answerNumber = 2

UPDATE
	dbo.ResourcePollAnswer
SET
	text = 'What would your co-workers say about you?'
WHERE
	pollId = '{DB9C28B4-E95C-4B43-8783-6F66C0EBE1D5}' AND answerNumber = 3

UPDATE
	dbo.ResourcePollAnswer
SET
	text = 'Do you work well under pressure?'
WHERE
	pollId = '{DB9C28B4-E95C-4B43-8783-6F66C0EBE1D5}' AND answerNumber = 4

ALTER TABLE dbo.ResourcePollAnswer
ADD CONSTRAINT PK_ResourcePollAnswer PRIMARY KEY NONCLUSTERED
(
	id
)

ALTER TABLE dbo.ResourcePollAnswer
ADD CONSTRAINT FK_ResourcePollAnswer_ResourcePoll FOREIGN KEY (pollId)
REFERENCES dbo.ResourcePoll (id)

CREATE UNIQUE CLUSTERED INDEX [IX_ResourcePollAnswer_number] ON [dbo].[ResourcePollAnswer]
(
	pollId,
	answerNumber
)
GO

ALTER TABLE dbo.ResourcePollAnswer
ALTER COLUMN text NVARCHAR(1024) NOT NULL
GO

-- ResourcePollAnswerVote

ALTER TABLE dbo.ResourcePollAnswerVote
ADD CONSTRAINT PK_ResourcePollAnswerVote PRIMARY KEY NONCLUSTERED
(
	id
)
GO

CREATE UNIQUE CLUSTERED INDEX [IX_ResourcePollAnswerVote] ON [dbo].[ResourcePollAnswerVote]
(
	resourcePollAnswerId,
	userid
)
GO

ALTER TABLE dbo.ResourcePollAnswerVote
ADD CONSTRAINT FK_ResourcePollAnswerVote_ResourcePollAnswer FOREIGN KEY (resourcePollAnswerId)
REFERENCES dbo.ResourcePollAnswer (id)
GO

