/****** Object:  View [dbo].[vw_MailTemplates]    Script Date: 1/24/2018 9:25:47 AM ******/
DROP VIEW [dbo].[vw_MailTemplates]
GO

/****** Object:  View [dbo].[vw_MailTemplates]    Script Date: 1/24/2018 9:25:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_MailTemplates]
AS
select t.*
from (select t.*,
             row_number() over (partition by MailTemplate_Identifier order by MailTemplate_VersionMajor desc, MailTemplate_VersionMinor desc) as seqnum
      from wim_MailTemplates t	  
     ) t
where seqnum = 1 and MailTemplate_IsArchived = 0;


GO


