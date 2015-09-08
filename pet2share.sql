USE [Pet2Share]
GO
/****** Object:  Table [dbo].[User]    Script Date: 09/05/2015 10:43:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varbinary](100) NOT NULL,
	[PersonId] [int] NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[AlternateEmail] [varchar](100) NULL,
	[Phone] [varchar](10) NULL,
	[SocialMediaSourceId] [int] NULL,
	[SocialMediaId] [varchar](100) NULL,
	[DateAdded] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SocialMediaSource]    Script Date: 09/05/2015 10:43:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SocialMediaSource](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[SocialMediaSource] ON
INSERT [dbo].[SocialMediaSource] ([Id], [Name], [IsDeleted]) VALUES (1, N'Facebook', 0)
INSERT [dbo].[SocialMediaSource] ([Id], [Name], [IsDeleted]) VALUES (2, N'Google+', 0)
INSERT [dbo].[SocialMediaSource] ([Id], [Name], [IsDeleted]) VALUES (3, N'Twitter', 0)
SET IDENTITY_INSERT [dbo].[SocialMediaSource] OFF
/****** Object:  Table [dbo].[PetType]    Script Date: 09/05/2015 10:43:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PetType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[DateAdded] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PetProfile]    Script Date: 09/05/2015 10:43:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PetProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[FamilyName] [varchar](50) NULL,
	[UserId] [int] NULL,
	[PetTypeId] [int] NULL,
	[DOB] [date] NULL,
	[ProfilePicture] [varchar](200) NULL,
	[DateAdded] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Person]    Script Date: 09/05/2015 10:43:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[DOB] [date] NOT NULL,
	[AddressId] [int] NULL,
	[PrimaryPhone] [varchar](10) NULL,
	[SecondaryPhone] [varchar](10) NULL,
	[Avatar] [varchar](200) NULL,
	[DateAdded] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Address]    Script Date: 09/05/2015 10:43:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Address](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AddressLine1] [varchar](100) NULL,
	[AddressLine2] [varchar](100) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[IsBillingAddress] [bit] NULL,
	[IsShippingAddress] [bit] NULL,
	[DateAdded] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[UserLogin]    Script Date: 09/05/2015 10:43:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserLogin]
	@Username varchar(50),
	@Password varchar(50)
AS
BEGIN
	SELECT ISNULL(Id, 0) 
	FROM dbo.[User]
	WHERE [Username] = @Username AND [Password] = HASHBYTES('MD5', @Password)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateUser]    Script Date: 09/05/2015 10:43:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateUser]
	@Username varchar(50),
	@Password varchar(50),
	@PersonId int,
	@Email varchar(100),
	@Phone varchar(10),
	@AlternateEmail varchar(100) = NULL,
	@SocialMediaSourceId int = 0,
	@SocialMediaId varchar(100) = NULL
AS
BEGIN
	INSERT INTO dbo.[User]
	(Username, [Password], PersonId, Email, Phone, AlternateEmail, SocialMediaSourceId, SocialMediaId)
	VALUES
	(@Username, HASHBYTES('MD5', @Password), @PersonId, @Email, @Phone, @AlternateEmail, @SocialMediaSourceId, @SocialMediaId)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertUpdatePerson]    Script Date: 09/05/2015 10:43:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdatePerson]
	@Id int,
	@FirstName varchar(50),
	@LastName varchar(50),
	@Email varchar(100),
	@DOB datetime,
	@AddressId int,
	@PrimaryPhone varchar(10),
	@SecondaryPhone varchar(10)
AS
BEGIN
	INSERT INTO dbo.[Person]
	(FirstName, LastName, Email, DOB, AddressId, PrimaryPhone, SecondaryPhone)
	VALUES
	(@FirstName, @LastName, @Email, @DOB, @AddressId, @PrimaryPhone, @SecondaryPhone)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateAddress]    Script Date: 09/05/2015 10:43:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateAddress]
	@Id int,
	@AddressLine1 varchar(100),
	@AddressLine2 varchar(100),
	@City varchar(50),
	@State varchar(50),
	@Country varchar(50)
AS
BEGIN
	INSERT INTO dbo.[Address]
	(AddressLine1, AddressLine2, City, State, Country)
	VALUES
	(@AddressLine1, @AddressLine2, @City, @State, @Country)
END
GO
/****** Object:  Default [DF_Address_IsBillingAddress]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [DF_Address_IsBillingAddress]  DEFAULT ((1)) FOR [IsBillingAddress]
GO
/****** Object:  Default [DF_Address_IsShippingAddress]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [DF_Address_IsShippingAddress]  DEFAULT ((1)) FOR [IsShippingAddress]
GO
/****** Object:  Default [DF_Address_DateAdded]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [DF_Address_DateAdded]  DEFAULT (getdate()) FOR [DateAdded]
GO
/****** Object:  Default [DF_Address_DateModified]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [DF_Address_DateModified]  DEFAULT (getdate()) FOR [DateModified]
GO
/****** Object:  Default [DF_Address_IsDeleted]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [DF_Address_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_Person_DateAdded]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_DateAdded]  DEFAULT (getdate()) FOR [DateAdded]
GO
/****** Object:  Default [DF_Person_DateModified]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_DateModified]  DEFAULT (getdate()) FOR [DateModified]
GO
/****** Object:  Default [DF_Person_IsActive]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  Default [DF_Person_IsDeleted]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_PetProfile_DateAdded]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[PetProfile] ADD  CONSTRAINT [DF_PetProfile_DateAdded]  DEFAULT (getdate()) FOR [DateAdded]
GO
/****** Object:  Default [DF_PetProfile_DateModified]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[PetProfile] ADD  CONSTRAINT [DF_PetProfile_DateModified]  DEFAULT (getdate()) FOR [DateModified]
GO
/****** Object:  Default [DF_PetProfile_IsActive]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[PetProfile] ADD  CONSTRAINT [DF_PetProfile_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  Default [DF_PetProfile_IsDeleted]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[PetProfile] ADD  CONSTRAINT [DF_PetProfile_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_PetType_IsActive]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[PetType] ADD  CONSTRAINT [DF_PetType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  Default [DF_PetType_IsDeleted]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[PetType] ADD  CONSTRAINT [DF_PetType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_SocialMediaSource_IsDeleted]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[SocialMediaSource] ADD  CONSTRAINT [DF_SocialMediaSource_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_User_DateAdded]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_DateAdded]  DEFAULT (getdate()) FOR [DateAdded]
GO
/****** Object:  Default [DF_User_DateModified]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_DateModified]  DEFAULT (getdate()) FOR [DateModified]
GO
/****** Object:  Default [DF_User_IsActive]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  Default [DF_User_IsDeleted]    Script Date: 09/05/2015 10:43:05 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
