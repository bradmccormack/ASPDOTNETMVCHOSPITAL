


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
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](255) UNIQUE NOT NULL,
	[Password] [varchar](255) NOT NULL
)

/* passwords are hashed securely -- atribution goes to https://crackstation.net/hashing-security.htm*/
INSERT INTO [dbo].BookingStaff(UserName, Password) VALUES ('mary', '1000:Nv++5IgtadCriUBFs6szekTEPP7fwWkT:ArozYQ6frh11t1WTbW9c4u1VqPvNp3uV');
INSERT INTO [dbo].BookingStaff(UserName, Password) VALUES ('fred', '1000:lSh4v3MJOTVsTEsXRg0eOvL2NfvVn1EL:8QuN7nmF4cMgaNcGF4igHKS5DNDNmvXx');
INSERT INTO [dbo].BookingStaff(UserName, Password) VALUEs ('brad', '1000:04/bs+cXvSyFplTjFqEp01BEEg4YrKST:yD9QVjZQ9fUXKr6F0WXxXd6gm/5j/pgw');

GO

COMMIT TRANSACTION;

