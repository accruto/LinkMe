ALTER TABLE linkme_owner.networker_invites
ADD donationRequestId uniqueIdentifier NULL
GO

ALTER TABLE linkme_owner.networker_invites
ADD CONSTRAINT FK_networker_invites_DonationRequest
	FOREIGN KEY (donationRequestId) REFERENCES linkme_owner.DonationRequest (id)
GO