USE master;  
GO  
IF DB_ID (N'MvcAddUserExample') IS NOT NULL
DROP DATABASE MvcAddUserExample;
GO
CREATE DATABASE MvcAddUserExample;  
GO

USE MvcAddUserExample;

CREATE TABLE [dbo].[Users](
    Id INT IDENTITY NOT NULL PRIMARY KEY,
	Email NVARCHAR(200) UNIQUE NOT NULL,
	PasswordHash NVARCHAR(200) NOT NULL
);
