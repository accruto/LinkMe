
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'loadNetworkGraph' 
	   AND 	  type = 'P')
BEGIN
DROP PROCEDURE linkme_owner.LoadNetworkGraph
END
GO


create procedure linkme_owner.LoadNetworkGraph
AS
BEGIN
	select np.id, up.firstName, up.lastName, up.active from linkme_owner.networker_profile 
	np inner join linkme_owner.user_profile up on np.id = up.id
END
GO