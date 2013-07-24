CREATE FUNCTION dbo.TicksToDateTime
(
      @ticks BIGINT
)
RETURNS DATETIME
AS
BEGIN
      DECLARE @days  BIGINT
      DECLARE @daysBefore1753 BIGINT
      DECLARE @timeTicks BIGINT
      DECLARE @seconds BIGINT

      SET @days = @ticks / CONVERT(BIGINT, 864000000000)
      SET @daysBefore1753 = CONVERT(BIGINT, 639905)
      SET @timeTicks = @ticks % CONVERT(BIGINT, 864000000000)
      SET @seconds = @timeTicks / CONVERT(BIGINT, 10000000)

      RETURN DATEADD(s, @seconds, DATEADD(d, @days - @daysBefore1753, CONVERT(DATETIME,'1/1/1753')))
END
