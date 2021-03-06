USE [FinancialPlanner]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 1/6/2016 9:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 1/6/2016 9:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 1/6/2016 9:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 1/6/2016 9:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 1/6/2016 9:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 1/6/2016 9:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [ItemDetail].[Credits]    Script Date: 1/6/2016 9:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ItemDetail].[Credits](
	[PkCredit] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](75) NOT NULL,
	[Name] [nvarchar](75) NULL,
	[Amount] [money] NULL,
	[FkPeriod] [int] NULL,
	[BeginDate] [date] NULL,
	[EndDate] [date] NULL,
	[WeeklyDOW] [int] NULL,
	[EverOtherWeekDOW] [int] NULL,
	[BiMonthlyDay1] [int] NULL,
	[BiMonthlyDay2] [int] NULL,
	[MonthlyDOM] [int] NULL,
	[Quarterly1Month] [int] NULL,
	[Quarterly1Day] [int] NULL,
	[Quarterly2Month] [int] NULL,
	[Quarterly2Day] [int] NULL,
	[Quarterly3Month] [int] NULL,
	[Quarterly3Day] [int] NULL,
	[Quarterly4Month] [int] NULL,
	[Quarterly4Day] [int] NULL,
	[SemiAnnual1Month] [int] NULL,
	[SemiAnnual1Day] [int] NULL,
	[SemiAnnual2Month] [int] NULL,
	[SemiAnnual2Day] [int] NULL,
	[AnnualMOY] [int] NULL,
	[AnnualDOM] [int] NULL,
	[DateRangeReq] [bit] NOT NULL CONSTRAINT [DF_Credits_DateRangeReq]  DEFAULT ((0)),
 CONSTRAINT [PK_Credits] PRIMARY KEY CLUSTERED 
(
	[PkCredit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ItemDetail].[Debits]    Script Date: 1/6/2016 9:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ItemDetail].[Debits](
	[PkDebit] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](75) NOT NULL,
	[Name] [nvarchar](75) NULL,
	[Amount] [money] NULL,
	[FkPeriod] [int] NULL,
	[BeginDate] [date] NULL,
	[EndDate] [date] NULL,
	[WeeklyDOW] [int] NULL,
	[EverOtherWeekDOW] [int] NULL,
	[BiMonthlyDay1] [int] NULL,
	[BiMonthlyDay2] [int] NULL,
	[MonthlyDOM] [int] NULL,
	[Quarterly1Month] [int] NULL,
	[Quarterly1Day] [int] NULL,
	[Quarterly2Month] [int] NULL,
	[Quarterly2Day] [int] NULL,
	[Quarterly3Month] [int] NULL,
	[Quarterly3Day] [int] NULL,
	[Quarterly4Month] [int] NULL,
	[Quarterly4Day] [int] NULL,
	[SemiAnnual1Month] [int] NULL,
	[SemiAnnual1Day] [int] NULL,
	[SemiAnnual2Month] [int] NULL,
	[SemiAnnual2Day] [int] NULL,
	[AnnualMOY] [int] NULL,
	[AnnualDOM] [int] NULL,
	[DateRangeReq] [bit] NOT NULL CONSTRAINT [DF_Debits_DateRangeReq]  DEFAULT ((0)),
 CONSTRAINT [PK_Debits] PRIMARY KEY CLUSTERED 
(
	[PkDebit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ItemDetail].[InitialAmount]    Script Date: 1/6/2016 9:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ItemDetail].[InitialAmount](
	[PkID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](75) NOT NULL,
	[Amount] [money] NULL,
	[BeginDate] [date] NULL,
 CONSTRAINT [PK_InitialAmount] PRIMARY KEY CLUSTERED 
(
	[PkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ItemDetail].[Periods]    Script Date: 1/6/2016 9:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ItemDetail].[Periods](
	[PkPeriod] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](75) NOT NULL,
 CONSTRAINT [PK_Periods] PRIMARY KEY CLUSTERED 
(
	[PkPeriod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [ItemDetail].[Credits]  WITH CHECK ADD  CONSTRAINT [FK_Credits_Periods] FOREIGN KEY([FkPeriod])
REFERENCES [ItemDetail].[Periods] ([PkPeriod])
ON UPDATE CASCADE
GO
ALTER TABLE [ItemDetail].[Credits] CHECK CONSTRAINT [FK_Credits_Periods]
GO
ALTER TABLE [ItemDetail].[Debits]  WITH CHECK ADD  CONSTRAINT [FK_Debits_Periods] FOREIGN KEY([FkPeriod])
REFERENCES [ItemDetail].[Periods] ([PkPeriod])
ON UPDATE CASCADE
GO
ALTER TABLE [ItemDetail].[Debits] CHECK CONSTRAINT [FK_Debits_Periods]
GO
