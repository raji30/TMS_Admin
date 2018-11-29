CREATE SCHEMA dbo
/****** Object:  Table dbo.Address    Script Date: 9/26/2018 11:24:30 PM ******/
CREATE TABLE dbo.Address(
	AddrKey UUID NOT NULL PRIMARY KEY,
	AddrName varchar(255) NOT NULL,
	Address1 varchar(255) NOT NULL,
	Address2 varchar(255) NOT NULL,
	City varchar(255) NULL,
	State varchar(255) NULL,
	ZipCode varchar(50) NULL,
	Country char(3) NULL,
	Website varchar(255) NULL,
	Phone varchar(20) NULL,
	Email varchar(255) NULL,
	Fax varchar(20) NULL)
	

GO
/****** Object:  table dbo.Apps    Script Date: 9/26/2018 11:24:30 PM ******/

CREATE TABLE dbo.Apps(
	AppKey UUID NOT NULL PRIMARY KEY,
	AppName varchar(255) NOT NULL,
	CreateDate TIMESTAMP NOT NULL,
	Version varchar(50) NOT NULL,
	UpdateDate TIMESTAMP NOT NULL)

GO

CREATE TABLE dbo.Company(
	CompanyKey UUID NOT NULL PRIMARY KEY,
	CompanyName varchar(255) NOT NULL,
	AddrKey UUID NOT NULL,
	ParentCompanyKey UUID NULL
 )

CREATE TABLE dbo.CompanyApps(
	CompanyKey UUID NOT NULL,
	AppKey UUID NOT NULL,
	Status smallint NOT NULL,
	StatusDatetime  TIMESTAMP NOT NULL,
	RegistrationString varchar(50) NULL,
	RegistrationDate TIMESTAMP NULL,
	ConnectionString varchar(150) NULL,
 PRIMARY KEY
(
	CompanyKey ,AppKey 
))
/****** Object:  Table dbo.UserActivity    Script Date: 9/26/2018 11:24:30 PM ******/

CREATE TABLE dbo.UserActivity(
	ActivityKey UUID NOT NULL PRIMARY KEY,
	UserKey UUID NULL,
	ActivityTIMESTAMP TIMESTAMP NULL,
	RefNo varchar(255) NULL,
	Comments varchar NULL)
 
GO

CREATE TABLE dbo.UserCompany(
	UserKey UUID NOT NULL,
	CompanyKey UUID NOT NULL,
  PRIMARY KEY 
(
	UserKey ,
	CompanyKey 
))
GO

CREATE TABLE dbo.UserInfo(
	UserKey UUID NOT NULL PRIMARY KEY,
	UserId varchar(255) UNIQUE,
	Password varchar(50) NOT NULL,
	PasswordExpiryDate TIMESTAMP NULL,
	FirstName varchar(255) NOT NULL,
	LastName varchar(255) NOT NULL,
	AddrKey UUID NOT NULL,
	Status smallint NOT NULL,
	StatusDate TIMESTAMP NOT NULL,
	ApprovedBy UUID NOT NULL,
	ApprovedTIMESTAMP TIMESTAMP NOT NULL,
	CreateDate TIMESTAMP NOT NULL,
	LastLoginDate TIMESTAMP NULL,
	LoginAttempts smallint NULL,
	PasswordTemp varchar(50) NULL
 )
GO
ALTER TABLE dbo.Company ADD CONSTRAINT FK_Company_Address FOREIGN KEY(AddrKey)
REFERENCES dbo.Address (AddrKey)
GO
ALTER TABLE dbo.Company  ADD  CONSTRAINT FK_Company_Company FOREIGN KEY(ParentCompanyKey)
REFERENCES dbo.Company (CompanyKey)
GO

ALTER TABLE dbo.CompanyApps  ADD  CONSTRAINT FK_CompanyApps_Apps FOREIGN KEY(AppKey)
REFERENCES dbo.Apps (AppKey)
GO
--ALTER TABLE dbo.CompanyApps CHECK CONSTRAINT FK_CompanyApps_Apps
GO
ALTER TABLE dbo.CompanyApps  ADD  CONSTRAINT FK_CompanyApps_Company FOREIGN KEY(CompanyKey)
REFERENCES dbo.Company (CompanyKey)
GO
--ALTER TABLE dbo.CompanyApps CHECK CONSTRAINT FK_CompanyApps_Company
GO
ALTER TABLE dbo.UserActivity  ADD  CONSTRAINT FK_UserActivity_UserInfo1 FOREIGN KEY(UserKey)
REFERENCES dbo.UserInfo (UserKey)
GO
--ALTER TABLE dbo.UserActivity CHECK CONSTRAINT FK_UserActivity_UserInfo1
GO
ALTER TABLE dbo.UserCompany  ADD  CONSTRAINT FK_UserCompany_Company FOREIGN KEY(CompanyKey)
REFERENCES dbo.Company (CompanyKey)
GO
--ALTER TABLE dbo.UserCompany CHECK CONSTRAINT FK_UserCompany_Company
GO
ALTER TABLE dbo.UserCompany  ADD  CONSTRAINT FK_UserCompany_UserInfo FOREIGN KEY(UserKey)
REFERENCES dbo.UserInfo (UserKey)
GO
--ALTER TABLE dbo.UserCompany CHECK CONSTRAINT FK_UserCompany_UserInfo
GO
ALTER TABLE dbo.UserInfo  ADD  CONSTRAINT FK_UserInfo_Address FOREIGN KEY(AddrKey)
REFERENCES dbo.Address (AddrKey)
GO
--ALTER TABLE dbo.UserInfo CHECK CONSTRAINT FK_UserInfo_Address
GO