DROP INDEX linkme_owner.account_transaction.ix_account_transaction_abn_date
GO

ALTER TABLE linkme_owner.account_transaction
DROP COLUMN abn
GO
