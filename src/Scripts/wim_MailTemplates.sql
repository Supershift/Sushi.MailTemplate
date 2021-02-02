/****** Object:  Table [dbo].[wim_MailTemplates]    Script Date: 4/24/2018 9:25:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[wim_MailTemplates](
	[MailTemplate_Key] [int] IDENTITY(1,1) NOT NULL,
	[MailTemplate_GUID] [uniqueidentifier] NOT NULL,
	[MailTemplate_Name] [nvarchar](50) NULL,
	[MailTemplate_Description] [nvarchar](255) NULL,
	[MailTemplate_Identifier] [nvarchar](50) NULL,
	[MailTemplate_DefaultSenderEmail] [nvarchar](255) NULL,
	[MailTemplate_DefaultSenderName] [nvarchar](50) NULL,
	[MailTemplate_BCCReceivers] [nvarchar](max) NULL,
	[MailTemplate_Subject] [nvarchar](512) NULL,
	[MailTemplate_Body] [nvarchar](max) NULL,
	[MailTemplate_DateCreated] [datetime] NULL,
	[MailTemplate_DateLastUpdated] [datetime] NULL,
	[MailTemplate_IsArchived] [bit] NULL,
	[MailTemplate_IsPublished] [bit] NULL,
	[MailTemplate_VersionMajor] [int] NULL,
	[MailTemplate_VersionMinor] [int] NULL,
	[MailTemplate_UserID] [int] NULL,
	[MailTemplate_UserName] [nvarchar](512) NULL,
 CONSTRAINT [PK_wim_MailTemplates] PRIMARY KEY CLUSTERED 
(
	[MailTemplate_Key] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[wim_MailTemplates] ADD  DEFAULT (newid()) FOR [MailTemplate_GUID]
GO


