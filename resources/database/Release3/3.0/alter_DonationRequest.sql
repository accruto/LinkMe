EXEC sp_changeobjectowner 'linkme_owner.DonationRequest', dbo
GO

EXEC sp_rename 'dbo.DonationRequest.UQ_recipient_amount', 'UQ_DonationRequest_recipient_amount', 'INDEX'
GO
