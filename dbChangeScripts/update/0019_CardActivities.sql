﻿/*
Run this script on:

        localhost.KokugenDataVersioned    -  This database will be modified

to synchronize it with:

        localhost.KokugenData

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.1.0 from Red Gate Software Ltd at 4/21/2010 9:20:42 PM

*/
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
IF EXISTS (SELECT * FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#tmpErrors')) DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRANSACTION
GO
PRINT N'Creating [dbo].[CardActivities]'
GO
CREATE TABLE [dbo].[CardActivities]
(
[Id] [uniqueidentifier] NOT NULL,
[StartTime] [datetime] NULL,
[EndTime] [datetime] NULL,
[Duration] [decimal] (19, 5) NULL,
[ActivityId] [int] NULL,
[Created] [datetime] NULL,
[LastUpdated] [datetime] NULL,
[Card_id] [uniqueidentifier] NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__CardActi__3214EC0731EC6D26] on [dbo].[CardActivities]'
GO
ALTER TABLE [dbo].[CardActivities] ADD CONSTRAINT [PK__CardActi__3214EC0731EC6D26] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[CardActivities]'
GO
ALTER TABLE [dbo].[CardActivities] ADD
CONSTRAINT [fk_card_to_activity] FOREIGN KEY ([Card_id]) REFERENCES [dbo].[Cards] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT 'The database update succeeded'
COMMIT TRANSACTION
END
ELSE PRINT 'The database update failed'
GO
DROP TABLE #tmpErrors
GO
