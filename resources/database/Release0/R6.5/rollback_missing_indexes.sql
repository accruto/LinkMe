
-- adds new indicies to the DB Schema



-- ACCOUNT TRANSACTION

DROP INDEX linkme_owner.account_transaction.ix_account_transaction_acctId_date
DROP INDEX linkme_owner.account_transaction.ix_account_transaction_abn_date
GO
-- ACCOUNT TRANSACTION LINE ITEM

DROP INDEX linkme_owner.account_transaction_line_item.ix_account_transaction_line_item_accTransactionId
GO
-- EMPLOYER PROFILE

DROP INDEX linkme_owner.employer_profile.ix_employer_profile_email_address
GO
-- endorsement
DROP INDEX linkme_owner.endorsement.ix_endorsement_endorser_endorsee
GO

-- invoice sequence (PK) 
ALTER TABLE linkme_owner.invoice_sequence 
DROP CONSTRAINT pk_invoice_sequence
GO
-- linkme_mail_queue

DROP INDEX linkme_owner.linkme_mail_queue.ix_linkme_mail_queue_date_sent
GO

-- networker_jobs

ALTER TABLE linkme_owner.networker_jobs 
DROP CONSTRAINT pk_networker_jobs
GO

DROP INDEX linkme_owner.networker_jobs.ix_networker_jobs_networkerId
GO

-- networker_overlooked_result(PK) 

ALTER TABLE linkme_owner.networker_overlooked_result 
DROP CONSTRAINT pk_networker_overlooked_result
GO

DROP INDEX linkme_owner.networker_overlooked_result.ix_networker_overlooked_result_networkerId
GO

-- networker_overlooked_search_criteria

ALTER TABLE linkme_owner.networker_overlooked_search_criteria 
DROP CONSTRAINT pk_networker_overlooked_search_criteria
GO

DROP INDEX linkme_owner.networker_overlooked_search_criteria.i_searches_criteria_id
GO

-- networker_overlooked_summary

ALTER TABLE linkme_owner.networker_overlooked_summary 
DROP CONSTRAINT pk_networker_overlooked_summary
GO

DROP INDEX linkme_owner.networker_overlooked_summary.ix_networker_overlooked_summary_employerId
GO

-- networker_purchased_result
DROP INDEX linkme_owner.networker_purchased_result.ix_networker_purchased_result_Id
GO

ALTER TABLE linkme_owner.networker_purchased_result 
DROP CONSTRAINT pk_networker_purchased_result
GO

-- user_profile_ideal_job

ALTER TABLE linkme_owner.user_profile_ideal_job 
DROP CONSTRAINT pk_user_profile_ideal_job
GO
--user_alert_parameters
ALTER TABLE linkme_owner.user_alert_parameters 
DROP CONSTRAINT pk_user_alert_parameters
GO

--user_alert_data

ALTER TABLE linkme_owner.user_alert_data 
DROP CONSTRAINT pk_user_alert_data
GO

 -- searches

ALTER TABLE linkme_owner.searches 
DROP CONSTRAINT pk_searches
GO

-- search_result 

ALTER TABLE linkme_owner.search_result 
DROP CONSTRAINT pk_search_result
GO

DROP INDEX linkme_owner.search_result.ix_search_result_savedSearchId
GO

-- search_criteria

ALTER TABLE linkme_owner.search_criteria
DROP CONSTRAINT pk_search_criteria
GO

-- ALTER TABLE MyTable ALTER COLUMN NullCOl NVARCHAR(20) NOT NULL

ALTER TABLE linkme_owner.search_criteria
alter column CriteriaType VARCHAR(50) NOT NULL
GO

