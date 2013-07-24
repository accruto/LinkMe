
-- adds new indicies to the DB Schema



-- ACCOUNT TRANSACTION

DROP INDEX linkme_owner.account_transaction.ix_account_transaction_acctId_date
DROP INDEX linkme_owner.account_transaction.ix_account_transaction_abn_date

GO

create index ix_account_transaction_acctId_date on linkme_owner.account_transaction
(
	userAccountId,
	transactionDate
)
GO
create index ix_account_transaction_abn_date on linkme_owner.account_transaction
(
	abn,
	transactionDate
)
GO

-- ACCOUNT TRANSACTION LINE ITEM

DROP INDEX linkme_owner.account_transaction_line_item.ix_account_transaction_line_item_accTransactionId
GO

CREATE index ix_account_transaction_line_item_accTransactionId ON linkme_owner.account_transaction_line_item
(
	accTransactionId
)
Go

-- EMPLOYER PROFILE

DROP INDEX linkme_owner.employer_profile.ix_employer_profile_email_address
GO

CREATE index ix_employer_profile_email_address ON linkme_owner.employer_profile
(
	emailAddress
)
GO

-- endorsement
DROP INDEX linkme_owner.endorsement.ix_endorsement_endorser_endorsee
GO

CREATE index ix_endorsement_endorser_endorsee ON linkme_owner.endorsement
(
	endorserId,
	endorseeId
)
GO

-- invoice sequence (PK) 
ALTER TABLE linkme_owner.invoice_sequence 
DROP CONSTRAINT pk_invoice_sequence
GO
ALTER TABLE linkme_owner.invoice_sequence 
ADD CONSTRAINT pk_invoice_sequence PRIMARY KEY ([Id])
GO

-- linkme_mail_queue

DROP INDEX linkme_owner.linkme_mail_queue.ix_linkme_mail_queue_date_sent
GO

CREATE index ix_linkme_mail_queue_date_sent ON linkme_owner.linkme_mail_queue
(
	dateSent DESC
)
GO


-- networker_jobs

ALTER TABLE linkme_owner.networker_jobs 
DROP CONSTRAINT pk_networker_jobs
GO
ALTER TABLE linkme_owner.networker_jobs 
ADD CONSTRAINT pk_networker_jobs PRIMARY KEY ([Id])
GO

DROP INDEX linkme_owner.networker_jobs.ix_networker_jobs_networkerId
GO

CREATE index ix_networker_jobs_networkerId ON linkme_owner.networker_jobs
(
	networkerId
)
GO



-- networker_overlooked_result(PK) 

ALTER TABLE linkme_owner.networker_overlooked_result 
DROP CONSTRAINT pk_networker_overlooked_result
GO
ALTER TABLE linkme_owner.networker_overlooked_result 
ADD CONSTRAINT pk_networker_overlooked_result PRIMARY KEY ([Id])
GO

DROP INDEX linkme_owner.networker_overlooked_result.ix_networker_overlooked_result_networkerId
GO

CREATE index ix_networker_overlooked_result_networkerId ON linkme_owner.networker_overlooked_result
(
	networkerProfileId
)
GO


-- networker_overlooked_search_criteria

ALTER TABLE linkme_owner.networker_overlooked_search_criteria 
DROP CONSTRAINT pk_networker_overlooked_search_criteria
GO

DROP INDEX linkme_owner.networker_overlooked_search_criteria.i_searches_criteria_id
GO

ALTER TABLE linkme_owner.networker_overlooked_search_criteria
alter column CriteriaType VARCHAR(50) NOT NULL
GO


ALTER TABLE linkme_owner.networker_overlooked_search_criteria 
ADD CONSTRAINT pk_networker_overlooked_search_criteria PRIMARY KEY ([Id],CriteriaType)
GO

CREATE index i_searches_criteria_id ON linkme_owner.networker_overlooked_search_criteria
(
	CriteriaType
)
GO


-- networker_overlooked_summary

ALTER TABLE linkme_owner.networker_overlooked_summary 
DROP CONSTRAINT pk_networker_overlooked_summary
GO
ALTER TABLE linkme_owner.networker_overlooked_summary 
ADD CONSTRAINT pk_networker_overlooked_summary PRIMARY KEY ([Id])
GO

DROP INDEX linkme_owner.networker_overlooked_summary.ix_networker_overlooked_summary_employerId
GO

CREATE index ix_networker_overlooked_summary_employerId ON linkme_owner.networker_overlooked_summary
(
	employerProfileId
)
GO


-- networker_purchased_result
--DROP INDEX networker_purchased_result.ix_networker_purchased_result_Id
--GO

ALTER TABLE linkme_owner.networker_purchased_result 
DROP CONSTRAINT pk_networker_purchased_result
GO
ALTER TABLE linkme_owner.networker_purchased_result 
ADD CONSTRAINT pk_networker_purchased_result PRIMARY KEY ([Id])
GO



-- user_profile_ideal_job


ALTER TABLE linkme_owner.user_profile_ideal_job 
DROP CONSTRAINT pk_user_profile_ideal_job
GO
ALTER TABLE linkme_owner.user_profile_ideal_job 
ADD CONSTRAINT pk_user_profile_ideal_job PRIMARY KEY ([Id])
GO


--user_alert_parameters
ALTER TABLE linkme_owner.user_alert_parameters 
DROP CONSTRAINT pk_user_alert_parameters
GO

DROP INDEX linkme_owner.user_alert_parameters.ix_user_alert_parameters_parameterName
GO

ALTER TABLE linkme_owner.user_alert_parameters
alter column ParameterName VARCHAR(50) NOT NULL
GO

ALTER TABLE linkme_owner.user_alert_parameters 
ADD CONSTRAINT pk_user_alert_parameters PRIMARY KEY ([Id],ParameterName)
GO

CREATE index ix_user_alert_parameters_parameterName ON linkme_owner.user_alert_parameters
(
	ParameterName
)
GO


--user_alert_data

ALTER TABLE linkme_owner.user_alert_data 
DROP CONSTRAINT pk_user_alert_data
GO
ALTER TABLE linkme_owner.user_alert_data
ADD CONSTRAINT pk_user_alert_data PRIMARY KEY ([Id])
GO


 -- searches

ALTER TABLE linkme_owner.searches 
DROP CONSTRAINT pk_searches
GO
ALTER TABLE linkme_owner.searches
ADD CONSTRAINT pk_searches PRIMARY KEY ([Id])
GO


-- search_result 

ALTER TABLE linkme_owner.search_result 
DROP CONSTRAINT pk_search_result
GO
ALTER TABLE linkme_owner.search_result
ADD CONSTRAINT pk_search_result PRIMARY KEY ([Id])
GO


DROP INDEX linkme_owner.search_result.ix_search_result_savedSearchId
GO

CREATE index ix_search_result_savedSearchId ON linkme_owner.search_result
(
	savedSearchId
)
GO


-- search_criteria

ALTER TABLE linkme_owner.search_criteria
DROP CONSTRAINT pk_search_criteria
GO

DROP INDEX linkme_owner.search_criteria.i_searches_criteria_id
GO
-- ALTER TABLE MyTable ALTER COLUMN NullCOl NVARCHAR(20) NOT NULL

ALTER TABLE linkme_owner.search_criteria
alter column CriteriaType VARCHAR(50) NOT NULL
GO

ALTER TABLE linkme_owner.search_criteria
ADD CONSTRAINT pk_search_criteria PRIMARY KEY ([Id], CriteriaType)
GO
CREATE index i_searches_criteria_id ON linkme_owner.search_criteria
(
	CriteriaType
)
GO


