
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/18/2017 19:07:55
-- Generated from EDMX file: D:\School\Jaar 2\Blok 6\SoProj\SOh-ParkInspect\Database\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ParkInspect Offline];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AvailbilityEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Availabilities] DROP CONSTRAINT [FK_AvailbilityEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_ChecklistQuestionsChecklists]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChecklistQuestions] DROP CONSTRAINT [FK_ChecklistQuestionsChecklists];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeJobTitle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_EmployeeJobTitle];
GO
IF OBJECT_ID(N'[dbo].[FK_InspectionInspectorEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InspectionInspectors] DROP CONSTRAINT [FK_InspectionInspectorEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_WeeklyScheduleEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WeeklySchedules] DROP CONSTRAINT [FK_WeeklyScheduleEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_InspectionInspectorInspection]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InspectionInspectors] DROP CONSTRAINT [FK_InspectionInspectorInspection];
GO
IF OBJECT_ID(N'[dbo].[FK_InspectionTask]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Inspections] DROP CONSTRAINT [FK_InspectionTask];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionAnswerSetValue_AnswerSetValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionAnswerSetValue] DROP CONSTRAINT [FK_QuestionAnswerSetValue_AnswerSetValue];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionAnswerSetValue_Question]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionAnswerSetValue] DROP CONSTRAINT [FK_QuestionAnswerSetValue_Question];
GO
IF OBJECT_ID(N'[dbo].[FK_InspectionChecklist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Inspections] DROP CONSTRAINT [FK_InspectionChecklist];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Customers] DROP CONSTRAINT [FK_CustomerAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerTask]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_CustomerTask];
GO
IF OBJECT_ID(N'[dbo].[FK_AddressParkingLot]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ParkingLots] DROP CONSTRAINT [FK_AddressParkingLot];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_EmployeeAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_AnswerPhoto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Photos] DROP CONSTRAINT [FK_AnswerPhoto];
GO
IF OBJECT_ID(N'[dbo].[FK_PhotoInspection]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Photos] DROP CONSTRAINT [FK_PhotoInspection];
GO
IF OBJECT_ID(N'[dbo].[FK_AnswerQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answers] DROP CONSTRAINT [FK_AnswerQuestion];
GO
IF OBJECT_ID(N'[dbo].[FK_InspectionAnswer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answers] DROP CONSTRAINT [FK_InspectionAnswer];
GO
IF OBJECT_ID(N'[dbo].[FK_AnswerEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answers] DROP CONSTRAINT [FK_AnswerEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_TaskParkingLot]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_TaskParkingLot];
GO
IF OBJECT_ID(N'[dbo].[FK_TaskChecklist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_TaskChecklist];
GO
IF OBJECT_ID(N'[dbo].[FK_ChecklistQuestionQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChecklistQuestions] DROP CONSTRAINT [FK_ChecklistQuestionQuestion];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionQuestionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Questions] DROP CONSTRAINT [FK_QuestionQuestionType];
GO
IF OBJECT_ID(N'[dbo].[FK_TaskScheduleTask]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TaskSchedules] DROP CONSTRAINT [FK_TaskScheduleTask];
GO
IF OBJECT_ID(N'[dbo].[FK_TaskScheduleIntervalFrequency]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TaskSchedules] DROP CONSTRAINT [FK_TaskScheduleIntervalFrequency];
GO
IF OBJECT_ID(N'[dbo].[FK_ChecklistChecklist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Checklists] DROP CONSTRAINT [FK_ChecklistChecklist];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Addresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Addresses];
GO
IF OBJECT_ID(N'[dbo].[Answers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Answers];
GO
IF OBJECT_ID(N'[dbo].[AnswerSetValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnswerSetValues];
GO
IF OBJECT_ID(N'[dbo].[Availabilities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Availabilities];
GO
IF OBJECT_ID(N'[dbo].[Checklists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Checklists];
GO
IF OBJECT_ID(N'[dbo].[ChecklistQuestions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChecklistQuestions];
GO
IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[Inspections]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Inspections];
GO
IF OBJECT_ID(N'[dbo].[InspectionInspectors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InspectionInspectors];
GO
IF OBJECT_ID(N'[dbo].[JobTitles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobTitles];
GO
IF OBJECT_ID(N'[dbo].[ParkingLots]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ParkingLots];
GO
IF OBJECT_ID(N'[dbo].[Photos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Photos];
GO
IF OBJECT_ID(N'[dbo].[Questions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Questions];
GO
IF OBJECT_ID(N'[dbo].[QuestionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTypes];
GO
IF OBJECT_ID(N'[dbo].[Tasks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tasks];
GO
IF OBJECT_ID(N'[dbo].[WeeklySchedules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WeeklySchedules];
GO
IF OBJECT_ID(N'[dbo].[TaskSchedules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TaskSchedules];
GO
IF OBJECT_ID(N'[dbo].[IntervalFrequencies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IntervalFrequencies];
GO
IF OBJECT_ID(N'[dbo].[QuestionAnswerSetValue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionAnswerSetValue];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Addresses'
CREATE TABLE [dbo].[Addresses] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Street] varchar(255)  NULL,
    [Number] varchar(255)  NOT NULL,
    [ZipCode] varchar(255)  NOT NULL,
    [City] varchar(255)  NULL,
    [Country] varchar(255)  NOT NULL,
    [Remarks] nvarchar(max)  NULL,
    [Province] nvarchar(max)  NULL,
    [Hash] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Answers'
CREATE TABLE [dbo].[Answers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [InspectionID] int  NOT NULL,
    [Text] varchar(max)  NOT NULL,
    [Question_ID] int  NOT NULL,
    [Employee_ID] int  NOT NULL
);
GO

-- Creating table 'AnswerSetValues'
CREATE TABLE [dbo].[AnswerSetValues] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Value] varchar(255)  NOT NULL
);
GO

-- Creating table 'Availabilities'
CREATE TABLE [dbo].[Availabilities] (
    [EmployeeID] int  NOT NULL,
    [StartDateTime] datetime  NOT NULL,
    [EndDateTime] datetime  NULL,
    [Available] bit  NOT NULL,
    [Remark] varchar(max)  NULL
);
GO

-- Creating table 'Checklists'
CREATE TABLE [dbo].[Checklists] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(255)  NOT NULL,
    [DateTimeCreated] datetime  NOT NULL,
    [Remarks] nvarchar(max)  NULL,
    [ChecklistID] int  NULL,
    [Hash] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ChecklistQuestions'
CREATE TABLE [dbo].[ChecklistQuestions] (
    [ChecklistID] int  NOT NULL,
    [QuestionID] int  NOT NULL,
    [Order] int  NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(255)  NOT NULL,
    [Contact] varchar(255)  NULL,
    [PhoneNumber] varchar(255)  NULL,
    [Email] varchar(255)  NULL,
    [Remarks] nvarchar(max)  NULL,
    [Hash] nvarchar(max)  NOT NULL,
    [Address_ID] int  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Email] varchar(255)  NOT NULL,
    [Password] varchar(255)  NOT NULL,
    [FirstName] varchar(255)  NOT NULL,
    [LastNamePrefix] varchar(255)  NULL,
    [LastName] varchar(255)  NOT NULL,
    [TelephoneNumber] varchar(255)  NOT NULL,
    [DateOfBirth] datetime  NOT NULL,
    [DateOfEmployment] datetime  NULL,
    [DateOfResignation] datetime  NULL,
    [Remarks] nvarchar(max)  NULL,
    [Hash] nvarchar(max)  NOT NULL,
    [JobTitle_Name] varchar(255)  NOT NULL,
    [Address_ID] int  NOT NULL
);
GO

-- Creating table 'Inspections'
CREATE TABLE [dbo].[Inspections] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [DateTimeStarted] datetime  NULL,
    [DateTimePlanned] datetime  NOT NULL,
    [DateTimeDone] datetime  NULL,
    [AverageTravelTime] time  NOT NULL,
    [Remarks] nvarchar(max)  NULL,
    [Hash] nvarchar(max)  NOT NULL,
    [Task_ID] int  NOT NULL,
    [Checklist_ID] int  NOT NULL
);
GO

-- Creating table 'InspectionInspectors'
CREATE TABLE [dbo].[InspectionInspectors] (
    [InspectorID] int  NOT NULL,
    [InspectionID] int  NOT NULL,
    [NumParkingSpacesOccupied] int  NOT NULL,
    [LastUpdated] datetime  NOT NULL,
    [Remarks] varchar(max)  NULL
);
GO

-- Creating table 'JobTitles'
CREATE TABLE [dbo].[JobTitles] (
    [Name] varchar(255)  NOT NULL,
    [Description] varchar(255)  NULL
);
GO

-- Creating table 'ParkingLots'
CREATE TABLE [dbo].[ParkingLots] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [GeoLatitude] float  NULL,
    [GeoLongitude] float  NULL,
    [NumCameras] int  NULL,
    [NumParkingSpaces] int  NULL,
    [DateOpened] datetime  NULL,
    [DateClosed] datetime  NULL,
    [HasBarrier] bit  NULL,
    [HasFence] bit  NULL,
    [ParkingFeeHourly] int  NULL,
    [ParkingFeeMax] int  NULL,
    [Hash] nvarchar(max)  NOT NULL,
    [Address_ID] int  NULL
);
GO

-- Creating table 'Photos'
CREATE TABLE [dbo].[Photos] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Filename] varchar(max)  NOT NULL,
    [Image] nvarchar(max)  NOT NULL,
    [Remarks] nvarchar(max)  NULL,
    [Answer_ID] int  NULL,
    [Answer_InspectionID] int  NULL,
    [Inspection_ID] int  NOT NULL
);
GO

-- Creating table 'Questions'
CREATE TABLE [dbo].[Questions] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Text] varchar(255)  NOT NULL,
    [AnswerPrefix] varchar(255)  NULL,
    [AnswerSuffix] varchar(255)  NULL,
    [Prebuilt] bit  NOT NULL,
    [Remarks] nvarchar(max)  NULL,
    [Hash] nvarchar(max)  NOT NULL,
    [QuestionType_Name] varchar(255)  NOT NULL
);
GO

-- Creating table 'QuestionTypes'
CREATE TABLE [dbo].[QuestionTypes] (
    [Name] varchar(255)  NOT NULL,
    [Description] varchar(255)  NULL
);
GO

-- Creating table 'Tasks'
CREATE TABLE [dbo].[Tasks] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [DateTimeStart] datetime  NOT NULL,
    [DateTimeEnd] datetime  NULL,
    [Remarks] nvarchar(max)  NULL,
    [DatetimeCreated] datetime  NOT NULL,
    [Hash] nvarchar(max)  NOT NULL,
    [Customer_ID] int  NOT NULL,
    [ParkingLot_ID] int  NOT NULL,
    [Checklist_ID] int  NOT NULL
);
GO

-- Creating table 'WeeklySchedules'
CREATE TABLE [dbo].[WeeklySchedules] (
    [EmployeeID] int  NOT NULL,
    [Day] int  NOT NULL,
    [StartTime] time  NOT NULL,
    [EndTime] time  NOT NULL
);
GO

-- Creating table 'TaskSchedules'
CREATE TABLE [dbo].[TaskSchedules] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [InspectionDateTime] datetime  NOT NULL,
    [Interval] int  NOT NULL,
    [Task_ID] int  NOT NULL,
    [IntervalFrequency_ID] int  NOT NULL
);
GO

-- Creating table 'IntervalFrequencies'
CREATE TABLE [dbo].[IntervalFrequencies] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'QuestionAnswerSetValue'
CREATE TABLE [dbo].[QuestionAnswerSetValue] (
    [AnswerSetValues_ID] int  NOT NULL,
    [Questions_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [PK_Addresses]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID], [InspectionID] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [PK_Answers]
    PRIMARY KEY CLUSTERED ([ID], [InspectionID] ASC);
GO

-- Creating primary key on [ID] in table 'AnswerSetValues'
ALTER TABLE [dbo].[AnswerSetValues]
ADD CONSTRAINT [PK_AnswerSetValues]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [EmployeeID], [StartDateTime] in table 'Availabilities'
ALTER TABLE [dbo].[Availabilities]
ADD CONSTRAINT [PK_Availabilities]
    PRIMARY KEY CLUSTERED ([EmployeeID], [StartDateTime] ASC);
GO

-- Creating primary key on [ID] in table 'Checklists'
ALTER TABLE [dbo].[Checklists]
ADD CONSTRAINT [PK_Checklists]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ChecklistID], [QuestionID] in table 'ChecklistQuestions'
ALTER TABLE [dbo].[ChecklistQuestions]
ADD CONSTRAINT [PK_ChecklistQuestions]
    PRIMARY KEY CLUSTERED ([ChecklistID], [QuestionID] ASC);
GO

-- Creating primary key on [ID] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Inspections'
ALTER TABLE [dbo].[Inspections]
ADD CONSTRAINT [PK_Inspections]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [InspectorID], [InspectionID] in table 'InspectionInspectors'
ALTER TABLE [dbo].[InspectionInspectors]
ADD CONSTRAINT [PK_InspectionInspectors]
    PRIMARY KEY CLUSTERED ([InspectorID], [InspectionID] ASC);
GO

-- Creating primary key on [Name] in table 'JobTitles'
ALTER TABLE [dbo].[JobTitles]
ADD CONSTRAINT [PK_JobTitles]
    PRIMARY KEY CLUSTERED ([Name] ASC);
GO

-- Creating primary key on [ID] in table 'ParkingLots'
ALTER TABLE [dbo].[ParkingLots]
ADD CONSTRAINT [PK_ParkingLots]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Photos'
ALTER TABLE [dbo].[Photos]
ADD CONSTRAINT [PK_Photos]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [PK_Questions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Name] in table 'QuestionTypes'
ALTER TABLE [dbo].[QuestionTypes]
ADD CONSTRAINT [PK_QuestionTypes]
    PRIMARY KEY CLUSTERED ([Name] ASC);
GO

-- Creating primary key on [ID] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [PK_Tasks]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [EmployeeID], [Day] in table 'WeeklySchedules'
ALTER TABLE [dbo].[WeeklySchedules]
ADD CONSTRAINT [PK_WeeklySchedules]
    PRIMARY KEY CLUSTERED ([EmployeeID], [Day] ASC);
GO

-- Creating primary key on [ID] in table 'TaskSchedules'
ALTER TABLE [dbo].[TaskSchedules]
ADD CONSTRAINT [PK_TaskSchedules]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'IntervalFrequencies'
ALTER TABLE [dbo].[IntervalFrequencies]
ADD CONSTRAINT [PK_IntervalFrequencies]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [AnswerSetValues_ID], [Questions_ID] in table 'QuestionAnswerSetValue'
ALTER TABLE [dbo].[QuestionAnswerSetValue]
ADD CONSTRAINT [PK_QuestionAnswerSetValue]
    PRIMARY KEY CLUSTERED ([AnswerSetValues_ID], [Questions_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [EmployeeID] in table 'Availabilities'
ALTER TABLE [dbo].[Availabilities]
ADD CONSTRAINT [FK_AvailbilityEmployee]
    FOREIGN KEY ([EmployeeID])
    REFERENCES [dbo].[Employees]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ChecklistID] in table 'ChecklistQuestions'
ALTER TABLE [dbo].[ChecklistQuestions]
ADD CONSTRAINT [FK_ChecklistQuestionsChecklists]
    FOREIGN KEY ([ChecklistID])
    REFERENCES [dbo].[Checklists]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [JobTitle_Name] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_EmployeeJobTitle]
    FOREIGN KEY ([JobTitle_Name])
    REFERENCES [dbo].[JobTitles]
        ([Name])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeJobTitle'
CREATE INDEX [IX_FK_EmployeeJobTitle]
ON [dbo].[Employees]
    ([JobTitle_Name]);
GO

-- Creating foreign key on [InspectorID] in table 'InspectionInspectors'
ALTER TABLE [dbo].[InspectionInspectors]
ADD CONSTRAINT [FK_InspectionInspectorEmployee]
    FOREIGN KEY ([InspectorID])
    REFERENCES [dbo].[Employees]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EmployeeID] in table 'WeeklySchedules'
ALTER TABLE [dbo].[WeeklySchedules]
ADD CONSTRAINT [FK_WeeklyScheduleEmployee]
    FOREIGN KEY ([EmployeeID])
    REFERENCES [dbo].[Employees]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [InspectionID] in table 'InspectionInspectors'
ALTER TABLE [dbo].[InspectionInspectors]
ADD CONSTRAINT [FK_InspectionInspectorInspection]
    FOREIGN KEY ([InspectionID])
    REFERENCES [dbo].[Inspections]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InspectionInspectorInspection'
CREATE INDEX [IX_FK_InspectionInspectorInspection]
ON [dbo].[InspectionInspectors]
    ([InspectionID]);
GO

-- Creating foreign key on [Task_ID] in table 'Inspections'
ALTER TABLE [dbo].[Inspections]
ADD CONSTRAINT [FK_InspectionTask]
    FOREIGN KEY ([Task_ID])
    REFERENCES [dbo].[Tasks]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InspectionTask'
CREATE INDEX [IX_FK_InspectionTask]
ON [dbo].[Inspections]
    ([Task_ID]);
GO

-- Creating foreign key on [AnswerSetValues_ID] in table 'QuestionAnswerSetValue'
ALTER TABLE [dbo].[QuestionAnswerSetValue]
ADD CONSTRAINT [FK_QuestionAnswerSetValue_AnswerSetValue]
    FOREIGN KEY ([AnswerSetValues_ID])
    REFERENCES [dbo].[AnswerSetValues]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Questions_ID] in table 'QuestionAnswerSetValue'
ALTER TABLE [dbo].[QuestionAnswerSetValue]
ADD CONSTRAINT [FK_QuestionAnswerSetValue_Question]
    FOREIGN KEY ([Questions_ID])
    REFERENCES [dbo].[Questions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionAnswerSetValue_Question'
CREATE INDEX [IX_FK_QuestionAnswerSetValue_Question]
ON [dbo].[QuestionAnswerSetValue]
    ([Questions_ID]);
GO

-- Creating foreign key on [Checklist_ID] in table 'Inspections'
ALTER TABLE [dbo].[Inspections]
ADD CONSTRAINT [FK_InspectionChecklist]
    FOREIGN KEY ([Checklist_ID])
    REFERENCES [dbo].[Checklists]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InspectionChecklist'
CREATE INDEX [IX_FK_InspectionChecklist]
ON [dbo].[Inspections]
    ([Checklist_ID]);
GO

-- Creating foreign key on [Address_ID] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [FK_CustomerAddress]
    FOREIGN KEY ([Address_ID])
    REFERENCES [dbo].[Addresses]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerAddress'
CREATE INDEX [IX_FK_CustomerAddress]
ON [dbo].[Customers]
    ([Address_ID]);
GO

-- Creating foreign key on [Customer_ID] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [FK_CustomerTask]
    FOREIGN KEY ([Customer_ID])
    REFERENCES [dbo].[Customers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerTask'
CREATE INDEX [IX_FK_CustomerTask]
ON [dbo].[Tasks]
    ([Customer_ID]);
GO

-- Creating foreign key on [Address_ID] in table 'ParkingLots'
ALTER TABLE [dbo].[ParkingLots]
ADD CONSTRAINT [FK_AddressParkingLot]
    FOREIGN KEY ([Address_ID])
    REFERENCES [dbo].[Addresses]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AddressParkingLot'
CREATE INDEX [IX_FK_AddressParkingLot]
ON [dbo].[ParkingLots]
    ([Address_ID]);
GO

-- Creating foreign key on [Address_ID] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_EmployeeAddress]
    FOREIGN KEY ([Address_ID])
    REFERENCES [dbo].[Addresses]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeAddress'
CREATE INDEX [IX_FK_EmployeeAddress]
ON [dbo].[Employees]
    ([Address_ID]);
GO

-- Creating foreign key on [Answer_ID], [Answer_InspectionID] in table 'Photos'
ALTER TABLE [dbo].[Photos]
ADD CONSTRAINT [FK_AnswerPhoto]
    FOREIGN KEY ([Answer_ID], [Answer_InspectionID])
    REFERENCES [dbo].[Answers]
        ([ID], [InspectionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnswerPhoto'
CREATE INDEX [IX_FK_AnswerPhoto]
ON [dbo].[Photos]
    ([Answer_ID], [Answer_InspectionID]);
GO

-- Creating foreign key on [Inspection_ID] in table 'Photos'
ALTER TABLE [dbo].[Photos]
ADD CONSTRAINT [FK_PhotoInspection]
    FOREIGN KEY ([Inspection_ID])
    REFERENCES [dbo].[Inspections]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PhotoInspection'
CREATE INDEX [IX_FK_PhotoInspection]
ON [dbo].[Photos]
    ([Inspection_ID]);
GO

-- Creating foreign key on [Question_ID] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK_AnswerQuestion]
    FOREIGN KEY ([Question_ID])
    REFERENCES [dbo].[Questions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnswerQuestion'
CREATE INDEX [IX_FK_AnswerQuestion]
ON [dbo].[Answers]
    ([Question_ID]);
GO

-- Creating foreign key on [InspectionID] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK_InspectionAnswer]
    FOREIGN KEY ([InspectionID])
    REFERENCES [dbo].[Inspections]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InspectionAnswer'
CREATE INDEX [IX_FK_InspectionAnswer]
ON [dbo].[Answers]
    ([InspectionID]);
GO

-- Creating foreign key on [Employee_ID] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK_AnswerEmployee]
    FOREIGN KEY ([Employee_ID])
    REFERENCES [dbo].[Employees]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnswerEmployee'
CREATE INDEX [IX_FK_AnswerEmployee]
ON [dbo].[Answers]
    ([Employee_ID]);
GO

-- Creating foreign key on [ParkingLot_ID] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [FK_TaskParkingLot]
    FOREIGN KEY ([ParkingLot_ID])
    REFERENCES [dbo].[ParkingLots]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TaskParkingLot'
CREATE INDEX [IX_FK_TaskParkingLot]
ON [dbo].[Tasks]
    ([ParkingLot_ID]);
GO

-- Creating foreign key on [Checklist_ID] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [FK_TaskChecklist]
    FOREIGN KEY ([Checklist_ID])
    REFERENCES [dbo].[Checklists]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TaskChecklist'
CREATE INDEX [IX_FK_TaskChecklist]
ON [dbo].[Tasks]
    ([Checklist_ID]);
GO

-- Creating foreign key on [QuestionID] in table 'ChecklistQuestions'
ALTER TABLE [dbo].[ChecklistQuestions]
ADD CONSTRAINT [FK_ChecklistQuestionQuestion]
    FOREIGN KEY ([QuestionID])
    REFERENCES [dbo].[Questions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChecklistQuestionQuestion'
CREATE INDEX [IX_FK_ChecklistQuestionQuestion]
ON [dbo].[ChecklistQuestions]
    ([QuestionID]);
GO

-- Creating foreign key on [QuestionType_Name] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [FK_QuestionQuestionType]
    FOREIGN KEY ([QuestionType_Name])
    REFERENCES [dbo].[QuestionTypes]
        ([Name])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionQuestionType'
CREATE INDEX [IX_FK_QuestionQuestionType]
ON [dbo].[Questions]
    ([QuestionType_Name]);
GO

-- Creating foreign key on [Task_ID] in table 'TaskSchedules'
ALTER TABLE [dbo].[TaskSchedules]
ADD CONSTRAINT [FK_TaskScheduleTask]
    FOREIGN KEY ([Task_ID])
    REFERENCES [dbo].[Tasks]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TaskScheduleTask'
CREATE INDEX [IX_FK_TaskScheduleTask]
ON [dbo].[TaskSchedules]
    ([Task_ID]);
GO

-- Creating foreign key on [IntervalFrequency_ID] in table 'TaskSchedules'
ALTER TABLE [dbo].[TaskSchedules]
ADD CONSTRAINT [FK_TaskScheduleIntervalFrequency]
    FOREIGN KEY ([IntervalFrequency_ID])
    REFERENCES [dbo].[IntervalFrequencies]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TaskScheduleIntervalFrequency'
CREATE INDEX [IX_FK_TaskScheduleIntervalFrequency]
ON [dbo].[TaskSchedules]
    ([IntervalFrequency_ID]);
GO

-- Creating foreign key on [ChecklistID] in table 'Checklists'
ALTER TABLE [dbo].[Checklists]
ADD CONSTRAINT [FK_ChecklistChecklist]
    FOREIGN KEY ([ChecklistID])
    REFERENCES [dbo].[Checklists]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChecklistChecklist'
CREATE INDEX [IX_FK_ChecklistChecklist]
ON [dbo].[Checklists]
    ([ChecklistID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------