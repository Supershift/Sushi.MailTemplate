CREATE UNIQUE INDEX IX_wim_MailTemplates_Identifier 
ON wim_MailTemplates (MailTemplate_Identifier)
WHERE MailTemplate_IsPublished = 1
