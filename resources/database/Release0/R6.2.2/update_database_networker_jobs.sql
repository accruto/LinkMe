ALTER TABLE linkme_owner.networker_jobs
ADD idx int null
GO

SELECT DISTINCT ID FROM linkme_owner.networker_jobs
GO

DROP TABLE #DISTINCT_JOBIDS
GO

select distinct [id], COUNT(id) jobcount
into #DISTINCT_JOBIDS
from linkme_owner.networker_jobs
GROUP BY [ID]

DECLARE @JOBID VARCHAR(50)
DECLARE @JOBCOUNT INT

DECLARE IDCURSOR  CURSOR  FOR
SELECT  [id], jobcount FROM #DISTINCT_JOBIDS

OPEN IDCURSOR
FETCH NEXT FROM IDCURSOR
INTO @JOBID, @JOBCOUNT

WHILE @@FETCH_STATUS = 0
BEGIN
        DECLARE @IDX INT
        SET @IDX = 0

        SET ROWCOUNT 1

        WHILE @IDX <= @JOBCOUNT
        BEGIN
                UPDATE linkme_owner.networker_jobs SET IDX = (@IDX)
                WHERE IDX is NULL
                AND ID = @JOBID
                SET @IDX = @IDX + 1
        END

        SET ROWCOUNT 0
        FETCH NEXT FROM IDCURSOR
        INTO @JOBID, @JOBCOUNT
END
CLOSE IDCURSOR
DEALLOCATE IDCURSOR
GO
SELECT * FROM linkme_owner.networker_jobs
GO
DELETE FROM linkme_owner.networker_jobs WHERE IDX > 0
GO
SELECT * FROM linkme_owner.networker_jobs
GO 

ALTER TABLE linkme_owner.networker_jobs
DROP COLUMN idx 
GO