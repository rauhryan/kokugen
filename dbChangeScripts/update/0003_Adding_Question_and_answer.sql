/*
Run this script on:

        localhost.KokugenDataVersioned    -  This database will be modified

to synchronize it with:

        localhost.KokugenData

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.0.0 from Red Gate Software Ltd at 4/14/2010 4:16:04 PM

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
PRINT N'Altering [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] ADD
[Question] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Answer] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[DailyTimeRecords]'
GO
ALTER TABLE [dbo].[DailyTimeRecords] ADD
[User_id] [uniqueidentifier] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[DailyTimeRecords]'
GO
ALTER TABLE [dbo].[DailyTimeRecords] ADD
CONSTRAINT [fk_DailyTimeRecord_to_User] FOREIGN KEY ([User_id]) REFERENCES [dbo].[Users] ([Id])
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
