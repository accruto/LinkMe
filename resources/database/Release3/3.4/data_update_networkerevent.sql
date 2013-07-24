select *
from NetworkerEvent
where type = 63

go

declare wmCursor cursor for
select ne.id, wm.senderId, wm.text
from WhiteboardMessage wm
	inner join
		NetworkerEvent ne
	on ne.actorId = wm.boardOwnerId
	and datediff(s, wm.time, ne.time) between 0 and 1
	and wm.time < ne.time
where ne.type = 63 and not exists
(
	select *
	from NetworkerEventDelta
	where eventId = ne.id
)

open wmCursor

declare @id uniqueidentifier
declare @senderId uniqueidentifier
declare @text varchar(400)

fetch next from wmCursor
into @id, @senderId, @text

while @@FETCH_STATUS = 0
begin

	update NetworkerEvent
	set actionedNetworkerId = @senderId
	where id = @id

	insert NetworkerEventDelta ( eventId, [from], [to] )
	values ( @id, null, left(@text, 100) )

	fetch next from wmCursor
	into @id, @senderId, @text

end

close wmCursor
deallocate wmCursor

go

select *
from NetworkerEvent ne inner join
	NetworkerEventDelta ned on ne.id = ned.eventId
where type = 63

