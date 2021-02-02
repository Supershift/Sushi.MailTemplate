CREATE TABLE [dbo].[wim_MailTemplateDefaultValuePlaceholders](
	[MailTemplateDefaultValuePlaceholder_Key] [int] IDENTITY(1,1) NOT NULL,
	[MailTemplateDefaultValuePlaceholder_MailTemplate_Key] [int] NOT NULL,
	[MailTemplateDefaultValuePlaceholder_Placeholder] [nvarchar](max) NOT NULL,
	[MailTemplateDefaultValuePlaceholder_Value] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_wim_MailTemplateDefaultValuePlaceholders] PRIMARY KEY CLUSTERED 
(
	[MailTemplateDefaultValuePlaceholder_Key] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[wim_MailTemplateDefaultValuePlaceholders]  WITH CHECK ADD  CONSTRAINT [FK_wim_MailTemplateDefaultValuePlaceholders_wim_MailTemplates] FOREIGN KEY([MailTemplateDefaultValuePlaceholder_MailTemplate_Key])
REFERENCES [dbo].[wim_MailTemplates] ([MailTemplate_Key])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[wim_MailTemplateDefaultValuePlaceholders] CHECK CONSTRAINT [FK_wim_MailTemplateDefaultValuePlaceholders_wim_MailTemplates]
GO

