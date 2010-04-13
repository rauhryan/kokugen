/*
Run this script on:

        localhost.KokugenDataVersioned    -  This database will be modified

to synchronize it with:

        localhost.KokugenData

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.0.0 from Red Gate Software Ltd at 4/13/2010 2:17:05 PM

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
PRINT N'Dropping foreign keys from [dbo].[Cards]'
GO
ALTER TABLE [dbo].[Cards] DROP
CONSTRAINT [fk_card_to_column],
CONSTRAINT [fk_column_to_card]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[Projects]'
GO
ALTER TABLE [dbo].[Projects] DROP
CONSTRAINT [fk_Project_to_Archive],
CONSTRAINT [fk_Project_to_Backlog],
CONSTRAINT [fk_Project_to_Company]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[CustomBoardColumn]'
GO
ALTER TABLE [dbo].[CustomBoardColumn] DROP
CONSTRAINT [FKD562296A86C5D437],
CONSTRAINT [FK_Project_To_Board_Columns]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[TimeRecords]'
GO
ALTER TABLE [dbo].[TimeRecords] DROP
CONSTRAINT [fk_TimeRecord_to_Card],
CONSTRAINT [FK_Project_To_Time_Record],
CONSTRAINT [fk_TimeRecord_to_Task]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[RoleToUser]'
GO
ALTER TABLE [dbo].[RoleToUser] DROP
CONSTRAINT [FK132F056D6AA1A457],
CONSTRAINT [FK132F056D834DE057]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[BoardColumns]'
GO
ALTER TABLE [dbo].[BoardColumns] DROP CONSTRAINT [PK__BoardCol__3214EC072FCF1A8A]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[Cards]'
GO
ALTER TABLE [dbo].[Cards] DROP CONSTRAINT [PK__Cards__3214EC0737703C52]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[Companies]'
GO
ALTER TABLE [dbo].[Companies] DROP CONSTRAINT [PK__Companie__3214EC073B40CD36]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[CustomBoardColumn]'
GO
ALTER TABLE [dbo].[CustomBoardColumn] DROP CONSTRAINT [PK__CustomBo__3214EC07339FAB6E]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[Projects]'
GO
ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [PK__Projects__3214EC074A8310C6]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[Roles]'
GO
ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [PK__Roles__3214EC073F115E1A]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[Roles]'
GO
ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [UQ__Roles__737584F641EDCAC5]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[TaskCategories]'
GO
ALTER TABLE [dbo].[TaskCategories] DROP CONSTRAINT [PK__TaskCate__3214EC0746B27FE2]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[TimeRecords]'
GO
ALTER TABLE [dbo].[TimeRecords] DROP CONSTRAINT [PK__TimeReco__3214EC074E53A1AA]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [PK__Users__3214EC075224328E]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [UQ__Users__A9D1053455009F39]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [UQ__Users__C9F2845657DD0BE4]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] ADD
[IsLocked] [bit] NULL,
[IsActivated] [bit] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Users__3214EC0769FBBC1F] on [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [PK__Users__3214EC0769FBBC1F] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__BoardCol__3214EC0747A6A41B] on [dbo].[BoardColumns]'
GO
ALTER TABLE [dbo].[BoardColumns] ADD CONSTRAINT [PK__BoardCol__3214EC0747A6A41B] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Cards__3214EC074F47C5E3] on [dbo].[Cards]'
GO
ALTER TABLE [dbo].[Cards] ADD CONSTRAINT [PK__Cards__3214EC074F47C5E3] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Companie__3214EC07531856C7] on [dbo].[Companies]'
GO
ALTER TABLE [dbo].[Companies] ADD CONSTRAINT [PK__Companie__3214EC07531856C7] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__CustomBo__3214EC074B7734FF] on [dbo].[CustomBoardColumn]'
GO
ALTER TABLE [dbo].[CustomBoardColumn] ADD CONSTRAINT [PK__CustomBo__3214EC074B7734FF] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Projects__3214EC07625A9A57] on [dbo].[Projects]'
GO
ALTER TABLE [dbo].[Projects] ADD CONSTRAINT [PK__Projects__3214EC07625A9A57] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Roles__3214EC0756E8E7AB] on [dbo].[Roles]'
GO
ALTER TABLE [dbo].[Roles] ADD CONSTRAINT [PK__Roles__3214EC0756E8E7AB] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__TaskCate__3214EC075E8A0973] on [dbo].[TaskCategories]'
GO
ALTER TABLE [dbo].[TaskCategories] ADD CONSTRAINT [PK__TaskCate__3214EC075E8A0973] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__TimeReco__3214EC07662B2B3B] on [dbo].[TimeRecords]'
GO
ALTER TABLE [dbo].[TimeRecords] ADD CONSTRAINT [PK__TimeReco__3214EC07662B2B3B] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[Roles]'
GO
ALTER TABLE [dbo].[Roles] ADD CONSTRAINT [UQ__Roles__737584F659C55456] UNIQUE NONCLUSTERED  ([Name])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [UQ__Users__A9D105346CD828CA] UNIQUE NONCLUSTERED  ([Email])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [UQ__Users__C9F284566FB49575] UNIQUE NONCLUSTERED  ([UserName])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Cards]'
GO
ALTER TABLE [dbo].[Cards] ADD
CONSTRAINT [fk_card_to_column] FOREIGN KEY ([Column_id]) REFERENCES [dbo].[BoardColumns] ([Id]),
CONSTRAINT [fk_column_to_card] FOREIGN KEY ([Project_id]) REFERENCES [dbo].[Projects] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Projects]'
GO
ALTER TABLE [dbo].[Projects] ADD
CONSTRAINT [fk_Project_to_Archive] FOREIGN KEY ([Archive_id]) REFERENCES [dbo].[BoardColumns] ([Id]),
CONSTRAINT [fk_Project_to_Backlog] FOREIGN KEY ([Backlog_id]) REFERENCES [dbo].[BoardColumns] ([Id]),
CONSTRAINT [fk_Project_to_Company] FOREIGN KEY ([Company_id]) REFERENCES [dbo].[Companies] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[CustomBoardColumn]'
GO
ALTER TABLE [dbo].[CustomBoardColumn] ADD
CONSTRAINT [FKD562296A86C5D437] FOREIGN KEY ([Id]) REFERENCES [dbo].[BoardColumns] ([Id]),
CONSTRAINT [FK_Project_To_Board_Columns] FOREIGN KEY ([Project_id]) REFERENCES [dbo].[Projects] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[TimeRecords]'
GO
ALTER TABLE [dbo].[TimeRecords] ADD
CONSTRAINT [fk_TimeRecord_to_Card] FOREIGN KEY ([Card_id]) REFERENCES [dbo].[Cards] ([Id]),
CONSTRAINT [FK_Project_To_Time_Record] FOREIGN KEY ([Project_id]) REFERENCES [dbo].[Projects] ([Id]),
CONSTRAINT [fk_TimeRecord_to_Task] FOREIGN KEY ([Task_id]) REFERENCES [dbo].[TaskCategories] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[RoleToUser]'
GO
ALTER TABLE [dbo].[RoleToUser] ADD
CONSTRAINT [FK132F056D6AA1A457] FOREIGN KEY ([Role_id]) REFERENCES [dbo].[Roles] ([Id]),
CONSTRAINT [FK132F056D834DE057] FOREIGN KEY ([User_id]) REFERENCES [dbo].[Users] ([Id])
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
