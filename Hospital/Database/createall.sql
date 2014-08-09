
USE [master]
GO

CREATE DATABASE [Hospital]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Hospital', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Hospital.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Hospital_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Hospital_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

USE [Hospital]
GO

BEGIN TRANSACTION;
CREATE TABLE [dbo].[Bed](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[BedName] [varchar](50) NOT NULL,
	[RatePerDay] [money] NOT NULL,
	[BedType] [varchar](50) NOT NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Patient](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](255) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[Phone] [varchar](20) NOT NULL,
	[EmergencyContact] [varchar](255) NOT NULL,
	[DateOfRegistration] [datetime] NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[Doctor](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](255) NOT NULL,
	[Phone] [varchar](20) NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[Visit](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[PatientType] [bit] NOT NULL,
	[DoctorId] [int] NOT NULL,
	[PatientID] [int] NOT NULL,
	[BedId] [int] NOT NULL,
	[DateofVisit] [datetime] NOT NULL,
	[DateofDischarge] [datetime] NOT NULL,
	[Symptoms] [varchar](1000) NOT NULL,
	[Disease] [varchar](1000) NOT NULL,
	[Treatment] [varchar](1000) NOT NULL
	FOREIGN KEY (DoctorId) REFERENCES Doctor(Id),
	FOREIGN KEY (BedId) REFERENCES Bed(Id),
	FOREIGN KEY (PatientID) REFERENCES Patient(Id)
) ON [PRIMARY]

CREATE TABLE [dbo].[BookingStaff](
	[Id] [int] NOT NULL PRIMARY KEY IDENTITY(1,1),
	[UserName] [varchar](255) UNIQUE NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[SecurityStamp] VARCHAR(MAX)
)

/* passwords are hashed securely -- atribution goes to https://crackstation.net/hashing-security.htm*/
INSERT INTO [dbo].BookingStaff(UserName, Password) VALUES ('mary', '1000:ixhUzK7cOmgI69KTHg2UwoBoOsLBbAWD:HzZU7Z6ZwmEkxL0wsCWSqvV225OV1Dxl');
INSERT INTO [dbo].BookingStaff(UserName, Password) VALUES ('fred', '1000:IQgo73XlxxuZEPOaH8gGEzwInqGkiVUh:+D9V7AECHtuHXmCJPWyBcIz08ATOSkuK');
INSERT INTO [dbo].BookingStaff(UserName, Password) VALUEs ('brad', '1000:uQW3eBzW1Es08tqT39tuXuR3jS0+HUo/:xmH9G4RdsIA7QW/6TZSKKOJLf38kdUdI');

INSERT INTO Patient(Name, Address, DateOfBirth, Phone, EmergencyContact, DateOfRegistration)
VALUES ('Brad McCormack', '221 Princess Highway , Albion Park Rail', '19790926 10:34:09 AM',
'0490078069', 'Sarina McCormack', '09/08/2014');

INSERT INTO Bed(BedName, RatePerDay, BedType) 
VALUES('Hypo-condriac Bed', 50.95, 'Soft Bed');

INSERT INTO Doctor(Name, Address, Phone)
VALUEs ('DR JOE', '54 Smith Street, Wollongong NSW 2527', '63564634');

GO

COMMIT TRANSACTION;

