
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/10/2017 14:31:10
-- Generated from EDMX file: C:\Users\Daan\School\PROG6\SOh-ParkInspect\Database\Model.edmx
--Jobtitle
--
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ParkInspect]
GO

INSERT INTO [dbo].[Employees]
           ([Email]
           ,[Password]
           ,[FirstName]
           ,[LastNamePrefix]
           ,[LastName]
           ,[TelephoneNumber]
           ,[DateOfBirth]
           ,[DateOfEmployment]
           ,[DateOfResignation]
           ,[Remarks]
           ,[JobTitle_Name]
           ,[Address_ID])
     VALUES
           ('admin@admin.nl'
           ,'$2a$10$JaIqBIVhtYyK6mwVDdtxFOuTLD/RCZv7wV6uqQWCpX0FMbYzgynTC'
           ,'Admin'
           ,'Von'
           ,'Avans'
           ,'062103819'
           ,'11/14/1997'
           ,'11/14/1997'
           ,null
           ,null
           ,'Admin'
           ,1)
GO





