DELETE dbo.ChannelApp
DELETE dbo.Channel
DELETE dbo.App

DECLARE @channelId UNIQUEIDENTIFIER
DECLARE @channelName NVARCHAR(50)
DECLARE @appId UNIQUEIDENTIFIER
DECLARE @appName NVARCHAR(50)

-- Web

SET @channelId = '{DCFC49ED-B576-410d-A08C-765FB71CE4F7}'
SET @channelName = 'Web'

SET @appId = '{55C84372-826B-42ef-8C5F-FAF6118A300C}'
SET @appName = 'Web'

INSERT
	dbo.Channel (id, name)
VALUES
	(@channelId, @channelName)

INSERT
	dbo.App (id, name)
VALUES
	(@appId, @appName)

INSERT
	dbo.ChannelApp (channelId, appId)
VALUES
	(@channelId, @appId)

-- API

SET @channelId = '{1A1FBC00-DC5E-4ed5-AB07-1A02C424F973}'
SET @channelName = 'API'

SET @appId = '{9E81E008-8D64-4b45-A3ED-A78EAA5FCFE9}'
SET @appName = 'iOS'

INSERT
	dbo.Channel (id, name)
VALUES
	(@channelId, @channelName)

INSERT
	dbo.App (id, name)
VALUES
	(@appId, @appName)

INSERT
	dbo.ChannelApp (channelId, appId)
VALUES
	(@channelId, @appId)

