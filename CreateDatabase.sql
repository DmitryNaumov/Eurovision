CREATE DATABASE [Eurovision]
GO

USE [Eurovision]
GO

CREATE TABLE [dbo].[VotingResults](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Votes] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_VotingResults] PRIMARY KEY CLUSTERED ([Id] ASC))
GO

INSERT INTO [dbo].[VotingResults] (Id, Name) VALUES (1, 'Great Britain')
INSERT INTO [dbo].[VotingResults] (Id, Name) VALUES (2, 'Germany')
INSERT INTO [dbo].[VotingResults] (Id, Name) VALUES (3, 'Italy')
INSERT INTO [dbo].[VotingResults] (Id, Name) VALUES (4, 'Canada')
INSERT INTO [dbo].[VotingResults] (Id, Name) VALUES (5, 'Russia')
INSERT INTO [dbo].[VotingResults] (Id, Name) VALUES (6, 'USA')
INSERT INTO [dbo].[VotingResults] (Id, Name) VALUES (7, 'France')
INSERT INTO [dbo].[VotingResults] (Id, Name) VALUES (8, 'Japan')
GO


