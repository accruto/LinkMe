IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_overlooked_result'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	CREATE TABLE networker_overlooked_result
	(
		id varchar(50),
		networkerOverlookedSummaryId varchar(50) NULL,
		networkerProfileId varchar(50) NULL,
		score integer NULL,
		rank integer NULL,
		resumeDownloaded bit NULL
	)

	
	CREATE CLUSTERED INDEX ix_networker_overlooked_result_Id on networker_overlooked_result(id);
	CREATE NONCLUSTERED INDEX ix_networker_overlooked_result_networker_Profile_Id on networker_overlooked_result(networkerProfileId);
	

END
GO