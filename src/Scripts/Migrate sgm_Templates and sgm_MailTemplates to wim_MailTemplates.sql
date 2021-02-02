
begin transaction

--select * from wim_MailTemplates

--select * from wim_MailTemplateDefaultValuePlaceholders

delete from wim_MailTemplateDefaultValuePlaceholders
delete from wim_MailTemplates


INSERT INTO [dbo].[wim_MailTemplates]
           (
            [MailTemplate_Body]
		   ,[MailTemplate_Name]
           ,[MailTemplate_Description]
           ,[MailTemplate_Identifier]
           ,[MailTemplate_DefaultSenderEmail]
           ,[MailTemplate_DefaultSenderName]
           ,[MailTemplate_BCCReceivers]
           ,[MailTemplate_Subject]
           ,[MailTemplate_DateCreated]
           ,[MailTemplate_DateLastUpdated]
           ,[MailTemplate_IsArchived]
           ,[MailTemplate_IsPublished]
           ,[MailTemplate_VersionMajor]
           ,[MailTemplate_VersionMinor]
           ,[MailTemplate_UserID]
           ,[MailTemplate_UserName])
 select 
REPLACE(
	REPLACE(
		REPLACE(
			REPLACE(
				REPLACE(CAST(mailtemplate_html as nvarchar(MAX)), '[BODY]', CAST(Template_MailHTML as nvarchar(MAX))) 
				, '[INTRO]'
				, template_content.query('ArrayOfData/Data[Property="[INTRO]"]').query('Data/Value').value('.', 'nvarchar(max)'))
			, '[OUTRO]'
			, template_content.query('ArrayOfData/Data[Property="[OUTRO]"]').query('Data/Value').value('.', 'nvarchar(max)')
		)
		, '[BODY2]'
		, template_content.query('ArrayOfData/Data[Property="[BODY2]"]').query('Data/Value').value('.', 'nvarchar(max)')
	)
	, '[BODY3]'
	, template_content.query('ArrayOfData/Data[Property="[BODY3]"]').query('Data/Value').value('.', 'nvarchar(max)')
)

	
as [MailTemplate_Body],

           [MailTemplate_Name]
           ,[MailTemplate_Description]
           ,Template_Identifier as [MailTemplate_Identifier]
           ,Template_FromEmail as [MailTemplate_DefaultSenderEmail]
           ,Template_FromName as [MailTemplate_DefaultSenderName]
           ,Template_BCCReceivers as [MailTemplate_BCCReceivers]
           ,Template_MailSubject as [MailTemplate_Subject]
           ,MailTemplate_Created as [MailTemplate_DateCreated]
           ,GETDATE() as [MailTemplate_DateLastUpdated]
           ,0 as [MailTemplate_IsArchived]
           ,1 as [MailTemplate_IsPublished]
           ,1 as [MailTemplate_VersionMajor]
           ,0 as[MailTemplate_VersionMinor]
           ,1 as [MailTemplate_UserID]
           ,'System import' as [MailTemplate_UserName]
from [dbo].[sgm_Templates] t join sgm_MailTemplates mt on t.Template_MailTemplate_Key = mt.MailTemplate_Key
where MailTemplate_IsActive = 1 and MailTemplate_IsActiveVersion = 1 and Template_IsActive = 1 and Template_IsActiveVersion = 1
order by MailTemplate_Identifier


select * from wim_MailTemplates

commit