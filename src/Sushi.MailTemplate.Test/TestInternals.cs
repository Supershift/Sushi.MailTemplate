//// MOST METHODS HAVE CHANGED FROM PUBLIC TO INTERNAL. TESTS BELOW HAVE TESTED THE INTERNAL METHODS.
//// NEW PUBLIC TESTS ARE IN TestPublic.cs

//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Linq;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Text.RegularExpressions;
//using Sushi.MailTemplate.Entities;

//namespace Sushi.MailTemplate.Test
//{
//    [TestClass]
//    public class TestInternals
//    {
//        [TestInitialize]
//        public void Init()
//        {
//            BodyWithGroups = @"<!DOCTYPE html><html><head><title>A Responsive Email Template</title><meta charset=""utf-8""><meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" /><style type=""text/css"">/* CLIENT-SPECIFIC STYLES */	body, table, td, a {	-webkit-text-size-adjust: 100%;	-ms-text-size-adjust: 100%;	}	/* Prevent WebKit and Windows mobile changing default text sizes */	table, td {	mso-table-lspace: 0pt;	mso-table-rspace: 0pt;	}	/* Remove spacing between tables in Outlook 2007 and up */	img {	-ms-interpolation-mode: bicubic;	}	/* Allow smoother rendering of resized image in Internet Explorer */	/* RESET STYLES */	img {	border: 0;	height: auto;	line-height: 100%;	outline: none;	text-decoration: none;	}	table {	border-collapse: collapse !important;	}	table.diff tr td table tr td table tr td {	padding: 0px !important;	height: 48px !important;	}	table.diff tr td table tr td table tr td.low {	padding: 0px !important;	height: 5px !important;	}	body {	height: 100% !important;	margin: 0 !important;	padding: 0 !important;	width: 100% !important;	}	.heighter {	border-left: solid 2px #eef2f5;	}	/* MOBILE STYLES */	@media screen and (max-width: 525px) {	/* ALLOWS FOR FLUID TABLES */	.wrapper {	width: 100% !important;	max-width: 100% !important;	}	/* ADJUSTS LAYOUT OF LOGO IMAGE */	.logo img {	margin: 0 auto !important;	}	/* USE THESE CLASSES TO HIDE CONTENT ON MOBILE */	.mobile-hide {	display: none !important;	}	.img-max {	max-width: 100% !important;	width: 100% !important;	height: auto !important;	}	/* FULL-WIDTH TABLES */	.responsive-table {	width: 100% !important;	}	/* UTILITY CLASSES FOR ADJUSTING PADDING ON MOBILE */	.padding {	padding: 10px 5% 0px 5% !important;	}	.padding-meta {	padding: 30px 5% 0px 5% !important;	text-align: center;	}	.no-padding {	padding: 0 !important;	}	.section-padding {	padding: 10px 15px 10px 15px !important;	}	.heighter {	height: auto !important;	margin: 0px -17px;	border-bottom: solid 10px #eef2f5;	}	/* ADJUST BUTTONS ON MOBILE */	.mobile-button-container {	margin: 0 auto;	width: 100% !important;	}	.mobile-button {	padding: 15px !important;	border: 0 !important;	font-size: 16px !important;	display: block !important;	}	}	@media screen and (min-device-width: 526px) and (max-device-width: 1070px) {	.wrapper {	overflow: hidden;	float: left !important;	width: 255px !important;	min-width: 25% !important;	}	}</style></head>
//<body bgcolor=""#eef2f5"" style=""margin: 0 !important; padding: 0 !important;""><!-- HEADER --><table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%""><tr><td bgcolor=""#eef2f5"" align=""center""><!--[if (gte mso 9)|(IE)]><table align=""center"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""660""><tr><td align=""center"" valign=""top"" width=""660""><![endif]--><table border=""0"" bgcolor=""#ffca48"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 660px;"" class=""wrapper""><tr><td align=""left"" valign=""top"" style=""padding: 15px 0;"" class=""logo""><a href="""" target=""_blank""><img alt=""Logo"" src=""https://hotelroomselection.blob.core.windows.net/tst/mail/logo.png"" width=""184"" height=""57"" style=""display: block; font-family: Helvetica, Arial, sans-serif; color: #ffffff; font-size: 16px;"" border=""0""></a></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=""#eef2f5"" align=""center"" class=""section-padding""><!--[if (gte mso 9)|(IE)]><table align=""center"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""660""><tr><td align=""center"" valign=""top"" width=""660""><![endif]--><table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 660px;"" class=""responsive-table""><tr><td><!-- HERO IMAGE --><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0""><tr><td align=""center"" bgcolor=""ffffff"" style=""padding-top: 40px;font-family: 'Trebuchet MS', sans-serif;font-size:40px;font-weight: bold;"">Hotelroomselection</td></tr><tr><td bgcolor=""ffffff""><!-- COPY --><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0""><tr><td align=""left"" style=""padding: 20px 40px 40px 40px; font-size: 16px; line-height: 20px; font-family: Arial, sans-serif; color: #000000;"" class=""padding""><center><span style=""width: 140px; font-family: 'Trebuchet MS', sans-serif; color: #000000; font-size: 16px; line-height: 14px; border-radius: 4px; display: block; text-decoration: none;"">You have claimed</span><br /><a style=""width: 340px; font-family: 'Trebuchet MS', sans-serif; color: #000000; font-size: 56px; line-height: 14px; padding: 15px 60px; border-radius: 4px; display: block; text-decoration: none;"">
//Roomnumber: [ROOMNUMBER]
//</a></center></td></tr></table></td></tr><tr><td bgcolor=""ffffff"" align=""center"" style=""padding-bottom: 20px;""></td></tr></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=""#eef2f5"" align=""center"" style=""padding: 0px 0px 0px 0px;"" class=""section-padding""><!--[if (gte mso 9)|(IE)]><table align=""center"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""660"" class=""diff""><tr><td align=""center"" valign=""middle"" width=""660""><![endif]--><table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 660px;"" bgcolor=""ffe186"" class=""responsive-table""><tr><td align=""left"" height=""100%"" valign=""top"" style=""padding:40px;font-family: Arial, sans-serif; color: #000000;"" width=""100%""><table border=""0"" width=""100%"" cellspacing=""0"" cellpadding=""0""><tbody><tr><td style=""display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""48"" height=""48"">
//<img src=""https://hotelroomselection.blob.core.windows.net/tst/mail/iconHash.png"" 
//alt=""[BOOKINGNUMBER]"" 
//width=""48"" height=""48"" border=""0"" /></td><td style=""display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""212"" height=""48"">
//&amp;nbsp;[BOOKINGNUMBER]
//</td><td style=""width: 10px;"" align=""left"" valign=""top"" width=""10"" height=""48"">&amp;nbsp;</td><td style=""display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""48"" height=""48"">
//<img src=""https://hotelroomselection.blob.core.windows.net/tst/mail/iconPlane.png"" 
//alt=""[DATESTART]""
//width=""48"" height=""48"" border=""0"" /></td><td style=""display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""212"" height=""48"">&amp;nbsp;[DATESTART]</td></tr><tr><td class=""low"" style=""height: 5px; bgcolor: ffe186;"" colspan=""3"">&amp;nbsp;</td></tr><tr><td style=""display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""48"" height=""48"">
//<img src=""https://hotelroomselection.blob.core.windows.net/tst/mail/iconSleep.png"" 
//alt=""[HOTEL]""
//width=""48"" height=""48"" border=""0"" /></td><td style=""display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""212"" height=""48"">&amp;nbsp;[HOTEL]</td><td style=""width: 10px;"" align=""left"" valign=""top"" width=""10"" height=""48"">&amp;nbsp;</td><td style=""display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""48"" height=""48"">
//<img src=""https://hotelroomselection.blob.core.windows.net/tst/mail/iconCalendar.png"" 
//alt=""[DAYS]""
//width=""48"" height=""48"" border=""0"" />
//</td><td style=""display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""212"" height=""48"">
//&amp;nbsp;[DAYS]
//nights</td></tr><tr><td class=""low"" style=""height: 5px; bgcolor: ffe186;"" colspan=""3"">&amp;nbsp;</td>
//[g:rooms]<td>[NAME] stays for [DAYS] days</td>[/g:rooms]</tr>
//<tr><td style=""display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""48"" height=""48""><img src=""https://hotelroomselection.blob.core.windows.net/tst/mail/iconPerson.png"" alt=""Insert alt text here"" width=""48"" height=""48"" border=""0"" /></td><td style=""display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""212"" height=""48"">
//&amp;nbsp;[NAME]</td><td style=""width: 10px;"" align=""left"" valign=""top"" width=""10"" height=""48"">&amp;nbsp;</td><td style=""display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""48"" height=""48""><img src=""https://hotelroomselection.blob.core.windows.net/tst/mail/iconKey.png"" alt=""Insert alt text here"" width=""48"" height=""48"" border=""0"" /></td><td style=""display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""212"" height=""48"">
//&amp;nbsp;[ROOMTYPE]</td>
//</tr></tbody></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=""#eef2f5"" align=""center"" style=""padding: 0px 0px 0px 0px;"" class=""section-padding""><!--[if (gte mso 9)|(IE)]><table align=""center"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""660""><tr><td align=""center"" valign=""top"" width=""660""><![endif]--><table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" bgcolor=""#ffca48"" style=""padding-bottom: 20px; max-width:660px;"" class=""responsive-table""><!-- TITLE --><tr><td align=""center"" height=""100%"" style=""padding: 40px;"" valign=""top"" width=""100%"" colspan=""2""><table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%""><tr><td style=""font-family: Arial, sans-serif; color: #000000;line-height: 20px;"" class=""padding""><br /><br />Kind regards,<br /><img src=""https://hotelroomselection.blob.core.windows.net/tst/mail/signature.png"" width=""298"" height=""81"" border=""0"" alt=""Insert alt text here"" style=""display: block; color: #000000;  font-family: arial, sans-serif; font-size: 16px;"" class=""img-max""></td></tr><tr><td style=""font-family: Arial, sans-serif; color: #000000;font-size: 10px;line-height: 14px;"" class=""padding"">
//Click [unsubscribe]here[/unsubscribe] to never receive the e-mail again</td>
//</tr></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr></table></body></html>";

//            TextWithPlaceholdersAndBrackets = "<html><body>I just want a [ bracket here and [ another and this is not ] a placeholder. Should be fine like 1.). [CUSTOMER][ROOM]<td>[NAME] [ROOMNUMBER]</td></body></html>";
//            TextWithPlaceholdersAndPlaceholderGroupsAndBrackets = "<html><body>I just want a [ bracket here and [ another and this is not ] a placeholder. Should be fine like 1.). [CUSTOMER][ROOM][g:rooms]<td>[NAME] [ROOMNUMBER]</td>[/g:rooms]</body></html>";
//        }

//        public Wim.Data.ApplicationUser User
//        {
//            get
//            {
//                var user = new Wim.Data.ApplicationUser
//                {
//                    Displayname = "Test User",
//                    ID = 9999,
//                    Email = "test@supershift.nl"
//                };
//                return user;
//            }
//        }

//        public string BodyWithGroups { get; set; }

//        public string TextWithPlaceholdersAndBrackets { get; set; }
//        public string TextWithPlaceholdersAndPlaceholderGroupsAndBrackets { get; set; }

//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest1")]
//        public void GetReplacedText()
//        {
//            //var mailTemplates = Data.MailTemplate.SelectAll();

//            //if(mailTemplates.Count > 0)
//            {
//                //var mailTemplate = mailTemplates.First();

//                string body = @"
//                        <!DOCTYPE html><html><head><title>A Responsive Email Template</title><meta charset=""utf-8""><meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" /><style type=""text/css"">/* CLIENT-SPECIFIC STYLES */	body, table, td, a {	-webkit-text-size-adjust: 100%;	-ms-text-size-adjust: 100%;	}	/* Prevent WebKit and Windows mobile changing default text sizes */	table, td {	mso-table-lspace: 0pt;	mso-table-rspace: 0pt;	}	/* Remove spacing between tables in Outlook 2007 and up */	img {	-ms-interpolation-mode: bicubic;	}	/* Allow smoother rendering of resized image in Internet Explorer */	/* RESET STYLES */	img {	border: 0;	height: auto;	line-height: 100%;	outline: none;	text-decoration: none;	}	table {	border-collapse: collapse !important;	}	table.diff tr td table tr td table tr td {	padding: 0px !important;	height: 48px !important;	}	table.diff tr td table tr td table tr td.low {	padding: 0px !important;	height: 5px !important;	}	body {	height: 100% !important;	margin: 0 !important;	padding: 0 !important;	width: 100% !important;	}	.heighter {	border-left: solid 2px #eef2f5;	}	/* MOBILE STYLES */	@media screen and (max-width: 525px) {	/* ALLOWS FOR FLUID TABLES */	.wrapper {	width: 100% !important;	max-width: 100% !important;	}	/* ADJUSTS LAYOUT OF LOGO IMAGE */	.logo img {	margin: 0 auto !important;	}	/* USE THESE CLASSES TO HIDE CONTENT ON MOBILE */	.mobile-hide {	display: none !important;	}	.img-max {	max-width: 100% !important;	width: 100% !important;	height: auto !important;	}	/* FULL-WIDTH TABLES */	.responsive-table {	width: 100% !important;	}	/* UTILITY CLASSES FOR ADJUSTING PADDING ON MOBILE */	.padding {	padding: 10px 5% 0px 5% !important;	}	.padding-meta {	padding: 30px 5% 0px 5% !important;	text-align: center;	}	.no-padding {	padding: 0 !important;	}	.section-padding {	padding: 10px 15px 10px 15px !important;	}	.heighter {	height: auto !important;	margin: 0px -17px;	border-bottom: solid 10px #eef2f5;	}	/* ADJUST BUTTONS ON MOBILE */	.mobile-button-container {	margin: 0 auto;	width: 100% !important;	}	.mobile-button {	padding: 15px !important;	border: 0 !important;	font-size: 16px !important;	display: block !important;	}	}	@media screen and (min-device-width: 526px) and (max-device-width: 1070px) {	.wrapper {	overflow: hidden;	float: left !important;	width: 255px !important;	min-width: 25% !important;	}	}</style></head>
//                        <body bgcolor=""#eef2f5"" style=""margin: 0 !important; padding: 0 !important;""><!-- HEADER --><table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%""><tr><td bgcolor=""#eef2f5"" align=""center""><!--[if (gte mso 9)|(IE)]><table align=""center"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""660""><tr><td align=""center"" valign=""top"" width=""660""><![endif]--><table border=""0"" bgcolor=""#ffca48"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 660px;"" class=""wrapper""><tr><td align=""left"" valign=""top"" style=""padding: 15px 0;"" class=""logo""><a href="""" target=""_blank""><img alt=""Logo"" src=""https://hotelroomselection.blob.core.windows.net/tst/mail/logo.png"" width=""184"" height=""57"" style=""display: block; font-family: Helvetica, Arial, sans-serif; color: #ffffff; font-size: 16px;"" border=""0""></a></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=""#eef2f5"" align=""center"" class=""section-padding""><!--[if (gte mso 9)|(IE)]><table align=""center"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""660""><tr><td align=""center"" valign=""top"" width=""660""><![endif]--><table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 660px;"" class=""responsive-table""><tr><td><!-- HERO IMAGE --><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0""><tr><td align=""center"" bgcolor=""ffffff"" style=""padding-top: 40px;font-family: 'Trebuchet MS', sans-serif;font-size:40px;font-weight: bold;"">Hotelroomselection</td></tr><tr><td bgcolor=""ffffff""><!-- COPY --><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0""><tr><td align=""left"" style=""padding: 20px 40px 40px 40px; font-size: 16px; line-height: 20px; font-family: Arial, sans-serif; color: #000000;"" class=""padding""><center><span style=""width: 140px; font-family: 'Trebuchet MS', sans-serif; color: #000000; font-size: 16px; line-height: 14px; border-radius: 4px; display: block; text-decoration: none;"">You have claimed</span><br />
//                        <a style=""width: 340px; font-family: 'Trebuchet MS', sans-serif; color: #000000; font-size: 56px; line-height: 14px; padding: 15px 60px; border-radius: 4px; display: block; text-decoration: none;"">
//                        Roomnumber: [ROOMNUMBER]</a></center></td></tr></table></td></tr><tr><td bgcolor=""ffffff"" align=""center"" style=""padding-bottom: 20px;""></td></tr></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=""#eef2f5"" align=""center"" style=""padding: 0px 0px 0px 0px;"" class=""section-padding""><!--[if (gte mso 9)|(IE)]><table align=""center"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""660"" class=""diff""><tr><td align=""center"" valign=""middle"" width=""660""><![endif]--><table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 660px;"" bgcolor=""ffe186"" class=""responsive-table""><tr><td align=""left"" height=""100%"" valign=""top"" style=""padding:40px;font-family: Arial, sans-serif; color: #000000;"" width=""100%""><table border=""0"" width=""100%"" cellspacing=""0"" cellpadding=""0""><tbody><tr><td style=""display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""48"" height=""48"">
//                        <img src=""https://hotelroomselection.blob.core.windows.net/tst/mail/iconHash.png"" alt=""[BOOKINGNUMBER]"" width=""48"" height=""48"" border=""0"" /></td><td style=""display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""212"" height=""48"">
//                        &amp;nbsp;[BOOKINGNUMBER]</td><td style=""width: 10px;"" align=""left"" valign=""top"" width=""10"" height=""48"">&amp;nbsp;</td><td style=""display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""48"" height=""48"">
//                        <img src=""https://hotelroomselection.blob.core.windows.net/tst/mail/iconPlane.png"" alt=""[DATESTART]"" width=""48"" height=""48"" border=""0"" /></td><td style=""display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""212"" height=""48"">
//                        &amp;nbsp;[DATESTART]</td></tr><tr><td class=""low"" style=""height: 5px; bgcolor: ffe186;"" colspan=""3"">&amp;nbsp;</td></tr><tr><td style=""display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""48"" height=""48"">
//                        <img src=""https://hotelroomselection.blob.core.windows.net/tst/mail/iconSleep.png"" alt=""[HOTEL]"" width=""48"" height=""48"" border=""0"" /></td><td style=""display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""212"" height=""48"">
//                        &amp;nbsp;[HOTEL]</td><td style=""width: 10px;"" align=""left"" valign=""top"" width=""10"" height=""48"">&amp;nbsp;</td><td style=""display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""48"" height=""48"">
//                        <img src=""https://hotelroomselection.blob.core.windows.net/tst/mail/iconCalendar.png"" alt=""[DAYS] "" width=""48"" height=""48"" border=""0"" /></td><td style=""display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""212"" height=""48"">
//                        &amp;nbsp;[DAYS] nights</td></tr><tr><td class=""low"" style=""height: 5px; bgcolor: ffe186;"" colspan=""3"">&amp;nbsp;</td>
//                        [g:rooms]<td>[NAME] stays for [DAYS] days</td>[/g:rooms]
//                        </tr><tr><td style=""display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""48"" height=""48""><img src=""https://hotelroomselection.blob.core.windows.net/tst/mail/iconPerson.png"" alt=""Insert alt text here"" width=""48"" height=""48"" border=""0"" /></td><td style=""display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""212"" height=""48"">
//                        &amp;nbsp;[NAME]</td><td style=""width: 10px;"" align=""left"" valign=""top"" width=""10"" height=""48"">&amp;nbsp;</td><td style=""display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""48"" height=""48""><img src=""https://hotelroomselection.blob.core.windows.net/tst/mail/iconKey.png"" alt=""Insert alt text here"" width=""48"" height=""48"" border=""0"" /></td><td style=""display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;"" align=""left"" valign=""middle"" bgcolor=""#ffffff"" width=""212"" height=""48"">
//                        &amp;nbsp;[ROOMTYPE]</td></tr></tbody></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=""#eef2f5"" align=""center"" style=""padding: 0px 0px 0px 0px;"" class=""section-padding""><!--[if (gte mso 9)|(IE)]><table align=""center"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""660""><tr><td align=""center"" valign=""top"" width=""660""><![endif]--><table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" bgcolor=""#ffca48"" style=""padding-bottom: 20px; max-width:660px;"" class=""responsive-table""><!-- TITLE --><tr><td align=""center"" height=""100%"" style=""padding: 40px;"" valign=""top"" width=""100%"" colspan=""2""><table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%""><tr><td style=""font-family: Arial, sans-serif; color: #000000;line-height: 20px;"" class=""padding""><br /><br />Kind regards,<br /><img src=""https://hotelroomselection.blob.core.windows.net/tst/mail/signature.png"" width=""298"" height=""81"" border=""0"" alt=""Insert alt text here"" style=""display: block; color: #000000;  font-family: arial, sans-serif; font-size: 16px;"" class=""img-max""></td></tr><tr><td style=""font-family: Arial, sans-serif; color: #000000;font-size: 10px;line-height: 14px;"" class=""padding"">
//                        Click [unsubscribe]here[/unsubscribe] to never receive the e-mail again</td>
//                        </tr></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr></table></body></html>";

//                var listOfPlaceholderGroups = new List<PlaceholderGroup>();

//                var phgRooms = new PlaceholderGroup { Name = "rooms" };

//                phgRooms.AddNewRow();
//                phgRooms.AddNewRowItem("NAME", "Pietje Puk");
//                phgRooms.AddNewRowItem("DAYS", "8");

//                phgRooms.AddNewRow();
//                phgRooms.AddNewRowItem("NAME", "Marietje Muis");
//                phgRooms.AddNewRowItem("DAYS", "3");

//                phgRooms.AddNewRow();
//                phgRooms.AddNewRowItem("NAME", "Keesje Kabel");
//                phgRooms.AddNewRowItem("DAYS", "15");

//                phgRooms.AddNewRow();
//                phgRooms.AddNewRowItem("NAME", "Suusje Sabel");
//                phgRooms.AddNewRowItem("DAYS", "8");

//                listOfPlaceholderGroups.Add(phgRooms);

//                foreach (var placeholderGroup in listOfPlaceholderGroups)
//                {
//                    Console.WriteLine($"group name: {placeholderGroup.Name},");

//                    foreach (var row in placeholderGroup.PlaceholderRows)
//                    {
//                        foreach (var placeholder in row.Placeholders)
//                        {
//                            Console.WriteLine($"label: {placeholder.Name}, value: {placeholder.Value}");
//                        }
//                    }
//                }


//                var placeholders1 = new List<Placeholder>();
//                placeholders1.Add(new Placeholder("BOOKINGNUMBER", "1"));
//                placeholders1.Add(new Placeholder("rOOmNUMBER", "2"));
//                placeholders1.Add(new Placeholder("HOTEL", "Sunny Day Beach Hotel"));
//                placeholders1.Add(new Placeholder("DAYS", "10"));
//                placeholders1.Add(new Placeholder("unsubscribe", "https://supershift.nl/unsubscribe.aspx"));

//                var placeholderGroups = new Dictionary<string, Dictionary<string, List<string>>>();
//                var placeholderGroupPlaceholders = new Dictionary<string, List<string>>();

//                var placeholderValues = new List<string>();
//                placeholderValues.Add("Pietje Puk");
//                placeholderValues.Add("Marietje Muis");
//                placeholderValues.Add("Keesje Kabel");
//                placeholderValues.Add("Suusje Sabel");
//                placeholderGroupPlaceholders.Add("NAME", placeholderValues);

//                placeholderValues = new List<string>();
//                placeholderValues.Add("8");
//                placeholderValues.Add("3");
//                placeholderValues.Add("15");
//                placeholderValues.Add("8");
//                placeholderGroupPlaceholders.Add("DAYS", placeholderValues);

//                placeholderGroups.Add("rooms", placeholderGroupPlaceholders);


//                var placeholders = new Dictionary<string, string>();
//                placeholders.Add("BOOKINGNUMBER", "1");
//                placeholders.Add("rOOmNUMBER", "2");
//                placeholders.Add("HOTEL", "Sunny Day Beach Hotel");
//                placeholders.Add("DAYS", "10");
//                placeholders.Add("unsubscribe", "https://supershift.nl/unsubscribe.aspx");

//                var output1 = Logic.PlaceholderLogic.ApplyPlaceholders(body, listOfPlaceholderGroups, placeholders1);
//                //var output = Logic.PlaceholderLogic.ApplyPlaceholders(body, placeholderGroups, placeholders);

//                //Debug.WriteLine(output);
//                Debug.WriteLine(output1);

//                foreach (var item in placeholders1)
//                {
//                    //Assert.IsTrue(output.Contains(item.Value));
//                }

//                foreach (var item in placeholders)
//                {
//                    Assert.IsTrue(output1.Contains(item.Value));
//                }

//                //Assert.IsTrue(output == output1);
//            }
//        }

//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest1")]
//        public void GetReplacedTextWithNoPlaceholderSupplied()
//        {
//            //string body = "<!DOCTYPE html><html><head><title>A Responsive Email Template</title><meta charset=\"utf-8\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" /><style type=\"text/css\">/* CLIENT-SPECIFIC STYLES */	body, table, td, a {	-webkit-text-size-adjust: 100%;	-ms-text-size-adjust: 100%;	}	/* Prevent WebKit and Windows mobile changing default text sizes */	table, td {	mso-table-lspace: 0pt;	mso-table-rspace: 0pt;	}	/* Remove spacing between tables in Outlook 2007 and up */	img {	-ms-interpolation-mode: bicubic;	}	/* Allow smoother rendering of resized image in Internet Explorer */	/* RESET STYLES */	img {	border: 0;	height: auto;	line-height: 100%;	outline: none;	text-decoration: none;	}	table {	border-collapse: collapse !important;	}	table.diff tr td table tr td table tr td {	padding: 0px !important;	height: 48px !important;	}	table.diff tr td table tr td table tr td.low {	padding: 0px !important;	height: 5px !important;	}	body {	height: 100% !important;	margin: 0 !important;	padding: 0 !important;	width: 100% !important;	}	.heighter {	border-left: solid 2px #eef2f5;	}	/* MOBILE STYLES */	@media screen and (max-width: 525px) {	/* ALLOWS FOR FLUID TABLES */	.wrapper {	width: 100% !important;	max-width: 100% !important;	}	/* ADJUSTS LAYOUT OF LOGO IMAGE */	.logo img {	margin: 0 auto !important;	}	/* USE THESE CLASSES TO HIDE CONTENT ON MOBILE */	.mobile-hide {	display: none !important;	}	.img-max {	max-width: 100% !important;	width: 100% !important;	height: auto !important;	}	/* FULL-WIDTH TABLES */	.responsive-table {	width: 100% !important;	}	/* UTILITY CLASSES FOR ADJUSTING PADDING ON MOBILE */	.padding {	padding: 10px 5% 0px 5% !important;	}	.padding-meta {	padding: 30px 5% 0px 5% !important;	text-align: center;	}	.no-padding {	padding: 0 !important;	}	.section-padding {	padding: 10px 15px 10px 15px !important;	}	.heighter {	height: auto !important;	margin: 0px -17px;	border-bottom: solid 10px #eef2f5;	}	/* ADJUST BUTTONS ON MOBILE */	.mobile-button-container {	margin: 0 auto;	width: 100% !important;	}	.mobile-button {	padding: 15px !important;	border: 0 !important;	font-size: 16px !important;	display: block !important;	}	}	@media screen and (min-device-width: 526px) and (max-device-width: 1070px) {	.wrapper {	overflow: hidden;	float: left !important;	width: 255px !important;	min-width: 25% !important;	}	}</style></head><body bgcolor=\"#eef2f5\" style=\"margin: 0 !important; padding: 0 !important;\"><!-- HEADER --><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td bgcolor=\"#eef2f5\" align=\"center\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\"><tr><td align=\"center\" valign=\"top\" width=\"660\"><![endif]--><table border=\"0\" bgcolor=\"#ffca48\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 660px;\" class=\"wrapper\"><tr><td align=\"left\" valign=\"top\" style=\"padding: 15px 0;\" class=\"logo\"><a href=\"\" target=\"_blank\"><img alt=\"Logo\" src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/logo.png\" width=\"184\" height=\"57\" style=\"display: block; font-family: Helvetica, Arial, sans-serif; color: #ffffff; font-size: 16px;\" border=\"0\"></a></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=\"#eef2f5\" align=\"center\" class=\"section-padding\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\"><tr><td align=\"center\" valign=\"top\" width=\"660\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 660px;\" class=\"responsive-table\"><tr><td><!-- HERO IMAGE --><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"center\" bgcolor=\"ffffff\" style=\"padding-top: 40px;font-family: 'Trebuchet MS', sans-serif;font-size:40px;font-weight: bold;\">Hotelroomselection</td></tr><tr><td bgcolor=\"ffffff\"><!-- COPY --><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"left\" style=\"padding: 20px 40px 40px 40px; font-size: 16px; line-height: 20px; font-family: Arial, sans-serif; color: #000000;\" class=\"padding\"><center><span style=\"width: 140px; font-family: 'Trebuchet MS', sans-serif; color: #000000; font-size: 16px; line-height: 14px; border-radius: 4px; display: block; text-decoration: none;\">You have claimed</span><br /><a style=\"width: 340px; font-family: 'Trebuchet MS', sans-serif; color: #000000; font-size: 56px; line-height: 14px; padding: 15px 60px; border-radius: 4px; display: block; text-decoration: none;\">Roomnumber: [ROOMNUMBER]</a></center></td></tr></table></td></tr><tr><td bgcolor=\"ffffff\" align=\"center\" style=\"padding-bottom: 20px;\"></td></tr></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=\"#eef2f5\" align=\"center\" style=\"padding: 0px 0px 0px 0px;\" class=\"section-padding\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\" class=\"diff\"><tr><td align=\"center\" valign=\"middle\" width=\"660\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 660px;\" bgcolor=\"ffe186\" class=\"responsive-table\"><tr><td align=\"left\" height=\"100%\" valign=\"top\" style=\"padding:40px;font-family: Arial, sans-serif; color: #000000;\" width=\"100%\"><table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconHash.png\" alt=\"[BOOKINGNUMBER]\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[BOOKINGNUMBER]</td><td style=\"width: 10px;\" align=\"left\" valign=\"top\" width=\"10\" height=\"48\">&amp;nbsp;</td><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconPlane.png\" alt=\"[DATESTART]\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[DATESTART]</td></tr><tr><td class=\"low\" style=\"height: 5px; bgcolor: ffe186;\" colspan=\"3\">&amp;nbsp;</td></tr><tr><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconSleep.png\" alt=\"[HOTEL]\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[HOTEL]</td><td style=\"width: 10px;\" align=\"left\" valign=\"top\" width=\"10\" height=\"48\">&amp;nbsp;</td><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconCalendar.png\" alt=\"[DAYS] \" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[DAYS] nights</td></tr><tr><td class=\"low\" style=\"height: 5px; bgcolor: ffe186;\" colspan=\"3\">&amp;nbsp;</td>[g:rooms]<td>[NAME] stays for [DAYS] days</td>[/g:rooms]</tr><tr><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconPerson.png\" alt=\"Insert alt text here\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[NAME]</td><td style=\"width: 10px;\" align=\"left\" valign=\"top\" width=\"10\" height=\"48\">&amp;nbsp;</td><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconKey.png\" alt=\"Insert alt text here\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[ROOMTYPE]</td></tr></tbody></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=\"#eef2f5\" align=\"center\" style=\"padding: 0px 0px 0px 0px;\" class=\"section-padding\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\"><tr><td align=\"center\" valign=\"top\" width=\"660\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#ffca48\" style=\"padding-bottom: 20px; max-width:660px;\" class=\"responsive-table\"><!-- TITLE --><tr><td align=\"center\" height=\"100%\" style=\"padding: 40px;\" valign=\"top\" width=\"100%\" colspan=\"2\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td style=\"font-family: Arial, sans-serif; color: #000000;line-height: 20px;\" class=\"padding\"><br /><br />Kind regards,<br /><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/signature.png\" width=\"298\" height=\"81\" border=\"0\" alt=\"Insert alt text here\" style=\"display: block; color: #000000;  font-family: arial, sans-serif; font-size: 16px;\" class=\"img-max\"></td></tr><tr><td style=\"font-family: Arial, sans-serif; color: #000000;font-size: 10px;line-height: 14px;\" class=\"padding\">Click [unsubscribe]here[/unsubscribe] to never receive the e-mail again</td></tr></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr></table></body></html>";
//            var body = TextWithPlaceholdersAndPlaceholderGroupsAndBrackets;

//            var placeholderTags = Logic.PlaceholderLogic.GetPlaceholderTags(body, false);

//            foreach (var tag in placeholderTags)
//            {
//                Assert.IsTrue(body.Contains(tag));
//            }

//            var output = Logic.PlaceholderLogic.ApplyPlaceholders(body);

//            Debug.WriteLine(output);

//            Assert.AreNotEqual(output, body);

//            foreach (var tag in placeholderTags)
//            {
//                Assert.IsFalse(output.Contains(tag));
//            }
//        }

//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest3")]
//        public void GetPlaceholders()
//        {
//            string body = "<!DOCTYPE html><html><head><title>A Responsive Email Template</title><meta charset=\"utf-8\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" /><style type=\"text/css\">/* CLIENT-SPECIFIC STYLES */	body, table, td, a {	-webkit-text-size-adjust: 100%;	-ms-text-size-adjust: 100%;	}	/* Prevent WebKit and Windows mobile changing default text sizes */	table, td {	mso-table-lspace: 0pt;	mso-table-rspace: 0pt;	}	/* Remove spacing between tables in Outlook 2007 and up */	img {	-ms-interpolation-mode: bicubic;	}	/* Allow smoother rendering of resized image in Internet Explorer */	/* RESET STYLES */	img {	border: 0;	height: auto;	line-height: 100%;	outline: none;	text-decoration: none;	}	table {	border-collapse: collapse !important;	}	table.diff tr td table tr td table tr td {	padding: 0px !important;	height: 48px !important;	}	table.diff tr td table tr td table tr td.low {	padding: 0px !important;	height: 5px !important;	}	body {	height: 100% !important;	margin: 0 !important;	padding: 0 !important;	width: 100% !important;	}	.heighter {	border-left: solid 2px #eef2f5;	}	/* MOBILE STYLES */	@media screen and (max-width: 525px) {	/* ALLOWS FOR FLUID TABLES */	.wrapper {	width: 100% !important;	max-width: 100% !important;	}	/* ADJUSTS LAYOUT OF LOGO IMAGE */	.logo img {	margin: 0 auto !important;	}	/* USE THESE CLASSES TO HIDE CONTENT ON MOBILE */	.mobile-hide {	display: none !important;	}	.img-max {	max-width: 100% !important;	width: 100% !important;	height: auto !important;	}	/* FULL-WIDTH TABLES */	.responsive-table {	width: 100% !important;	}	/* UTILITY CLASSES FOR ADJUSTING PADDING ON MOBILE */	.padding {	padding: 10px 5% 0px 5% !important;	}	.padding-meta {	padding: 30px 5% 0px 5% !important;	text-align: center;	}	.no-padding {	padding: 0 !important;	}	.section-padding {	padding: 10px 15px 10px 15px !important;	}	.heighter {	height: auto !important;	margin: 0px -17px;	border-bottom: solid 10px #eef2f5;	}	/* ADJUST BUTTONS ON MOBILE */	.mobile-button-container {	margin: 0 auto;	width: 100% !important;	}	.mobile-button {	padding: 15px !important;	border: 0 !important;	font-size: 16px !important;	display: block !important;	}	}	@media screen and (min-device-width: 526px) and (max-device-width: 1070px) {	.wrapper {	overflow: hidden;	float: left !important;	width: 255px !important;	min-width: 25% !important;	}	}</style></head><body bgcolor=\"#eef2f5\" style=\"margin: 0 !important; padding: 0 !important;\"><!-- HEADER --><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td bgcolor=\"#eef2f5\" align=\"center\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\"><tr><td align=\"center\" valign=\"top\" width=\"660\"><![endif]--><table border=\"0\" bgcolor=\"#ffca48\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 660px;\" class=\"wrapper\"><tr><td align=\"left\" valign=\"top\" style=\"padding: 15px 0;\" class=\"logo\"><a href=\"\" target=\"_blank\"><img alt=\"Logo\" src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/logo.png\" width=\"184\" height=\"57\" style=\"display: block; font-family: Helvetica, Arial, sans-serif; color: #ffffff; font-size: 16px;\" border=\"0\"></a></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=\"#eef2f5\" align=\"center\" class=\"section-padding\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\"><tr><td align=\"center\" valign=\"top\" width=\"660\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 660px;\" class=\"responsive-table\"><tr><td><!-- HERO IMAGE --><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"center\" bgcolor=\"ffffff\" style=\"padding-top: 40px;font-family: 'Trebuchet MS', sans-serif;font-size:40px;font-weight: bold;\">Hotelroomselection</td></tr><tr><td bgcolor=\"ffffff\"><!-- COPY --><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"left\" style=\"padding: 20px 40px 40px 40px; font-size: 16px; line-height: 20px; font-family: Arial, sans-serif; color: #000000;\" class=\"padding\"><center><span style=\"width: 140px; font-family: 'Trebuchet MS', sans-serif; color: #000000; font-size: 16px; line-height: 14px; border-radius: 4px; display: block; text-decoration: none;\">You have claimed</span><br /><a style=\"width: 340px; font-family: 'Trebuchet MS', sans-serif; color: #000000; font-size: 56px; line-height: 14px; padding: 15px 60px; border-radius: 4px; display: block; text-decoration: none;\">[ROOMNUMBER]</a></center></td></tr></table></td></tr><tr><td bgcolor=\"ffffff\" align=\"center\" style=\"padding-bottom: 20px;\"></td></tr></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=\"#eef2f5\" align=\"center\" style=\"padding: 0px 0px 0px 0px;\" class=\"section-padding\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\" class=\"diff\"><tr><td align=\"center\" valign=\"middle\" width=\"660\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 660px;\" bgcolor=\"ffe186\" class=\"responsive-table\"><tr><td align=\"left\" height=\"100%\" valign=\"top\" style=\"padding:40px;font-family: Arial, sans-serif; color: #000000;\" width=\"100%\"><table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconHash.png\" alt=\"[BOOKINGNUMBER]\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[BOOKINGNUMBER]</td><td style=\"width: 10px;\" align=\"left\" valign=\"top\" width=\"10\" height=\"48\">&amp;nbsp;</td><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconPlane.png\" alt=\"[DATESTART]\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[DATESTART]</td></tr><tr><td class=\"low\" style=\"height: 5px; bgcolor: ffe186;\" colspan=\"3\">&amp;nbsp;</td></tr><tr><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconSleep.png\" alt=\"[HOTEL]\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[HOTEL]</td><td style=\"width: 10px;\" align=\"left\" valign=\"top\" width=\"10\" height=\"48\">&amp;nbsp;</td><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconCalendar.png\" alt=\"[DAYS] \" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[DAYS] nights</td></tr><tr><td class=\"low\" style=\"height: 5px; bgcolor: ffe186;\" colspan=\"3\">&amp;nbsp;</td>[g:rooms]<td>[NAME]</td>[/g:rooms]</tr><tr><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconPerson.png\" alt=\"Insert alt text here\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[NAME]</td><td style=\"width: 10px;\" align=\"left\" valign=\"top\" width=\"10\" height=\"48\">&amp;nbsp;</td><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconKey.png\" alt=\"Insert alt text here\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[ROOMTYPE]</td></tr></tbody></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=\"#eef2f5\" align=\"center\" style=\"padding: 0px 0px 0px 0px;\" class=\"section-padding\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\"><tr><td align=\"center\" valign=\"top\" width=\"660\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#ffca48\" style=\"padding-bottom: 20px; max-width:660px;\" class=\"responsive-table\"><!-- TITLE --><tr><td align=\"center\" height=\"100%\" style=\"padding: 40px;\" valign=\"top\" width=\"100%\" colspan=\"2\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td style=\"font-family: Arial, sans-serif; color: #000000;line-height: 20px;\" class=\"padding\"><br /><br />Kind regards,<br /><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/signature.png\" width=\"298\" height=\"81\" border=\"0\" alt=\"Insert alt text here\" style=\"display: block; color: #000000;  font-family: arial, sans-serif; font-size: 16px;\" class=\"img-max\"></td></tr><tr><td style=\"font-family: Arial, sans-serif; color: #000000;font-size: 10px;line-height: 14px;\" class=\"padding\">Click [unsubscribe]here[/unsubscribe] to never receive the e-mail again</td></tr></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr></table></body></html>";

//            var list = Logic.PlaceholderLogic.GetPlaceholderTags(body);

//            foreach (var item in list)
//            {
//                Console.WriteLine(item);
//                Debug.WriteLine(item);
//            }

//            Assert.IsTrue(list.Count == 8);
//        }
//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest3")]
//        public void GetPlaceholdersWithTags()
//        {
//            string body = "<!DOCTYPE html><html><head><title>A Responsive Email Template</title><meta charset=\"utf-8\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" /><style type=\"text/css\">/* CLIENT-SPECIFIC STYLES */	body, table, td, a {	-webkit-text-size-adjust: 100%;	-ms-text-size-adjust: 100%;	}	/* Prevent WebKit and Windows mobile changing default text sizes */	table, td {	mso-table-lspace: 0pt;	mso-table-rspace: 0pt;	}	/* Remove spacing between tables in Outlook 2007 and up */	img {	-ms-interpolation-mode: bicubic;	}	/* Allow smoother rendering of resized image in Internet Explorer */	/* RESET STYLES */	img {	border: 0;	height: auto;	line-height: 100%;	outline: none;	text-decoration: none;	}	table {	border-collapse: collapse !important;	}	table.diff tr td table tr td table tr td {	padding: 0px !important;	height: 48px !important;	}	table.diff tr td table tr td table tr td.low {	padding: 0px !important;	height: 5px !important;	}	body {	height: 100% !important;	margin: 0 !important;	padding: 0 !important;	width: 100% !important;	}	.heighter {	border-left: solid 2px #eef2f5;	}	/* MOBILE STYLES */	@media screen and (max-width: 525px) {	/* ALLOWS FOR FLUID TABLES */	.wrapper {	width: 100% !important;	max-width: 100% !important;	}	/* ADJUSTS LAYOUT OF LOGO IMAGE */	.logo img {	margin: 0 auto !important;	}	/* USE THESE CLASSES TO HIDE CONTENT ON MOBILE */	.mobile-hide {	display: none !important;	}	.img-max {	max-width: 100% !important;	width: 100% !important;	height: auto !important;	}	/* FULL-WIDTH TABLES */	.responsive-table {	width: 100% !important;	}	/* UTILITY CLASSES FOR ADJUSTING PADDING ON MOBILE */	.padding {	padding: 10px 5% 0px 5% !important;	}	.padding-meta {	padding: 30px 5% 0px 5% !important;	text-align: center;	}	.no-padding {	padding: 0 !important;	}	.section-padding {	padding: 10px 15px 10px 15px !important;	}	.heighter {	height: auto !important;	margin: 0px -17px;	border-bottom: solid 10px #eef2f5;	}	/* ADJUST BUTTONS ON MOBILE */	.mobile-button-container {	margin: 0 auto;	width: 100% !important;	}	.mobile-button {	padding: 15px !important;	border: 0 !important;	font-size: 16px !important;	display: block !important;	}	}	@media screen and (min-device-width: 526px) and (max-device-width: 1070px) {	.wrapper {	overflow: hidden;	float: left !important;	width: 255px !important;	min-width: 25% !important;	}	}</style></head><body bgcolor=\"#eef2f5\" style=\"margin: 0 !important; padding: 0 !important;\"><!-- HEADER --><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td bgcolor=\"#eef2f5\" align=\"center\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\"><tr><td align=\"center\" valign=\"top\" width=\"660\"><![endif]--><table border=\"0\" bgcolor=\"#ffca48\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 660px;\" class=\"wrapper\"><tr><td align=\"left\" valign=\"top\" style=\"padding: 15px 0;\" class=\"logo\"><a href=\"\" target=\"_blank\"><img alt=\"Logo\" src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/logo.png\" width=\"184\" height=\"57\" style=\"display: block; font-family: Helvetica, Arial, sans-serif; color: #ffffff; font-size: 16px;\" border=\"0\"></a></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=\"#eef2f5\" align=\"center\" class=\"section-padding\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\"><tr><td align=\"center\" valign=\"top\" width=\"660\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 660px;\" class=\"responsive-table\"><tr><td><!-- HERO IMAGE --><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"center\" bgcolor=\"ffffff\" style=\"padding-top: 40px;font-family: 'Trebuchet MS', sans-serif;font-size:40px;font-weight: bold;\">Hotelroomselection</td></tr><tr><td bgcolor=\"ffffff\"><!-- COPY --><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"left\" style=\"padding: 20px 40px 40px 40px; font-size: 16px; line-height: 20px; font-family: Arial, sans-serif; color: #000000;\" class=\"padding\"><center><span style=\"width: 140px; font-family: 'Trebuchet MS', sans-serif; color: #000000; font-size: 16px; line-height: 14px; border-radius: 4px; display: block; text-decoration: none;\">You have claimed</span><br /><a style=\"width: 340px; font-family: 'Trebuchet MS', sans-serif; color: #000000; font-size: 56px; line-height: 14px; padding: 15px 60px; border-radius: 4px; display: block; text-decoration: none;\">[ROOMNUMBER]</a></center></td></tr></table></td></tr><tr><td bgcolor=\"ffffff\" align=\"center\" style=\"padding-bottom: 20px;\"></td></tr></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=\"#eef2f5\" align=\"center\" style=\"padding: 0px 0px 0px 0px;\" class=\"section-padding\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\" class=\"diff\"><tr><td align=\"center\" valign=\"middle\" width=\"660\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 660px;\" bgcolor=\"ffe186\" class=\"responsive-table\"><tr><td align=\"left\" height=\"100%\" valign=\"top\" style=\"padding:40px;font-family: Arial, sans-serif; color: #000000;\" width=\"100%\"><table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconHash.png\" alt=\"[BOOKINGNUMBER]\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[BOOKINGNUMBER]</td><td style=\"width: 10px;\" align=\"left\" valign=\"top\" width=\"10\" height=\"48\">&amp;nbsp;</td><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconPlane.png\" alt=\"[DATESTART]\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[DATESTART]</td></tr><tr><td class=\"low\" style=\"height: 5px; bgcolor: ffe186;\" colspan=\"3\">&amp;nbsp;</td></tr><tr><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconSleep.png\" alt=\"[HOTEL]\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[HOTEL]</td><td style=\"width: 10px;\" align=\"left\" valign=\"top\" width=\"10\" height=\"48\">&amp;nbsp;</td><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconCalendar.png\" alt=\"[DAYS] \" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[DAYS] nights</td></tr><tr><td class=\"low\" style=\"height: 5px; bgcolor: ffe186;\" colspan=\"3\">&amp;nbsp;</td>[g:rooms]<td>[NAME]</td>[/g:rooms]</tr><tr><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconPerson.png\" alt=\"Insert alt text here\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[NAME]</td><td style=\"width: 10px;\" align=\"left\" valign=\"top\" width=\"10\" height=\"48\">&amp;nbsp;</td><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconKey.png\" alt=\"Insert alt text here\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[ROOMTYPE]</td></tr></tbody></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=\"#eef2f5\" align=\"center\" style=\"padding: 0px 0px 0px 0px;\" class=\"section-padding\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\"><tr><td align=\"center\" valign=\"top\" width=\"660\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#ffca48\" style=\"padding-bottom: 20px; max-width:660px;\" class=\"responsive-table\"><!-- TITLE --><tr><td align=\"center\" height=\"100%\" style=\"padding: 40px;\" valign=\"top\" width=\"100%\" colspan=\"2\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td style=\"font-family: Arial, sans-serif; color: #000000;line-height: 20px;\" class=\"padding\"><br /><br />Kind regards,<br /><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/signature.png\" width=\"298\" height=\"81\" border=\"0\" alt=\"Insert alt text here\" style=\"display: block; color: #000000;  font-family: arial, sans-serif; font-size: 16px;\" class=\"img-max\"></td></tr><tr><td style=\"font-family: Arial, sans-serif; color: #000000;font-size: 10px;line-height: 14px;\" class=\"padding\">Click [unsubscribe]here[/unsubscribe] to never receive the e-mail again</td></tr></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr></table></body></html>";

//            var list = Logic.PlaceholderLogic.GetPlaceholderTags(body, false);

//            foreach (var item in list)
//            {
//                Console.WriteLine(item);
//                Debug.WriteLine(item);
//            }

//            Assert.IsTrue(list.Count == 8);

//            Assert.IsTrue(list.All(x => x.StartsWith("[") && x.EndsWith("]")));
//        }

//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest4")]
//        public void GetPlaceholderGroupsWithBrackets()
//        {
//            string body = "<!DOCTYPE html><html><head><title>A Responsive Email Template</title><meta charset=\"utf-8\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" /><style type=\"text/css\">/* CLIENT-SPECIFIC STYLES */	body, table, td, a {	-webkit-text-size-adjust: 100%;	-ms-text-size-adjust: 100%;	}	/* Prevent WebKit and Windows mobile changing default text sizes */	table, td {	mso-table-lspace: 0pt;	mso-table-rspace: 0pt;	}	/* Remove spacing between tables in Outlook 2007 and up */	img {	-ms-interpolation-mode: bicubic;	}	/* Allow smoother rendering of resized image in Internet Explorer */	/* RESET STYLES */	img {	border: 0;	height: auto;	line-height: 100%;	outline: none;	text-decoration: none;	}	table {	border-collapse: collapse !important;	}	table.diff tr td table tr td table tr td {	padding: 0px !important;	height: 48px !important;	}	table.diff tr td table tr td table tr td.low {	padding: 0px !important;	height: 5px !important;	}	body {	height: 100% !important;	margin: 0 !important;	padding: 0 !important;	width: 100% !important;	}	.heighter {	border-left: solid 2px #eef2f5;	}	/* MOBILE STYLES */	@media screen and (max-width: 525px) {	/* ALLOWS FOR FLUID TABLES */	.wrapper {	width: 100% !important;	max-width: 100% !important;	}	/* ADJUSTS LAYOUT OF LOGO IMAGE */	.logo img {	margin: 0 auto !important;	}	/* USE THESE CLASSES TO HIDE CONTENT ON MOBILE */	.mobile-hide {	display: none !important;	}	.img-max {	max-width: 100% !important;	width: 100% !important;	height: auto !important;	}	/* FULL-WIDTH TABLES */	.responsive-table {	width: 100% !important;	}	/* UTILITY CLASSES FOR ADJUSTING PADDING ON MOBILE */	.padding {	padding: 10px 5% 0px 5% !important;	}	.padding-meta {	padding: 30px 5% 0px 5% !important;	text-align: center;	}	.no-padding {	padding: 0 !important;	}	.section-padding {	padding: 10px 15px 10px 15px !important;	}	.heighter {	height: auto !important;	margin: 0px -17px;	border-bottom: solid 10px #eef2f5;	}	/* ADJUST BUTTONS ON MOBILE */	.mobile-button-container {	margin: 0 auto;	width: 100% !important;	}	.mobile-button {	padding: 15px !important;	border: 0 !important;	font-size: 16px !important;	display: block !important;	}	}	@media screen and (min-device-width: 526px) and (max-device-width: 1070px) {	.wrapper {	overflow: hidden;	float: left !important;	width: 255px !important;	min-width: 25% !important;	}	}</style></head><body bgcolor=\"#eef2f5\" style=\"margin: 0 !important; padding: 0 !important;\"><!-- HEADER --><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td bgcolor=\"#eef2f5\" align=\"center\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\"><tr><td align=\"center\" valign=\"top\" width=\"660\"><![endif]--><table border=\"0\" bgcolor=\"#ffca48\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 660px;\" class=\"wrapper\"><tr><td align=\"left\" valign=\"top\" style=\"padding: 15px 0;\" class=\"logo\"><a href=\"\" target=\"_blank\"><img alt=\"Logo\" src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/logo.png\" width=\"184\" height=\"57\" style=\"display: block; font-family: Helvetica, Arial, sans-serif; color: #ffffff; font-size: 16px;\" border=\"0\"></a></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=\"#eef2f5\" align=\"center\" class=\"section-padding\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\"><tr><td align=\"center\" valign=\"top\" width=\"660\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 660px;\" class=\"responsive-table\"><tr><td><!-- HERO IMAGE --><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"center\" bgcolor=\"ffffff\" style=\"padding-top: 40px;font-family: 'Trebuchet MS', sans-serif;font-size:40px;font-weight: bold;\">Hotelroomselection</td></tr><tr><td bgcolor=\"ffffff\"><!-- COPY --><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"left\" style=\"padding: 20px 40px 40px 40px; font-size: 16px; line-height: 20px; font-family: Arial, sans-serif; color: #000000;\" class=\"padding\"><center><span style=\"width: 140px; font-family: 'Trebuchet MS', sans-serif; color: #000000; font-size: 16px; line-height: 14px; border-radius: 4px; display: block; text-decoration: none;\">You have claimed</span><br /><a style=\"width: 340px; font-family: 'Trebuchet MS', sans-serif; color: #000000; font-size: 56px; line-height: 14px; padding: 15px 60px; border-radius: 4px; display: block; text-decoration: none;\">[ROOMNUMBER]</a></center></td></tr></table></td></tr><tr><td bgcolor=\"ffffff\" align=\"center\" style=\"padding-bottom: 20px;\"></td></tr></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=\"#eef2f5\" align=\"center\" style=\"padding: 0px 0px 0px 0px;\" class=\"section-padding\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\" class=\"diff\"><tr><td align=\"center\" valign=\"middle\" width=\"660\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 660px;\" bgcolor=\"ffe186\" class=\"responsive-table\"><tr><td align=\"left\" height=\"100%\" valign=\"top\" style=\"padding:40px;font-family: Arial, sans-serif; color: #000000;\" width=\"100%\"><table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconHash.png\" alt=\"[BOOKINGNUMBER]\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[BOOKINGNUMBER]</td><td style=\"width: 10px;\" align=\"left\" valign=\"top\" width=\"10\" height=\"48\">&amp;nbsp;</td><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconPlane.png\" alt=\"[DATESTART]\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[DATESTART]</td></tr><tr><td class=\"low\" style=\"height: 5px; bgcolor: ffe186;\" colspan=\"3\">&amp;nbsp;</td></tr><tr><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconSleep.png\" alt=\"[HOTEL]\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[HOTEL]</td><td style=\"width: 10px;\" align=\"left\" valign=\"top\" width=\"10\" height=\"48\">&amp;nbsp;</td><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconCalendar.png\" alt=\"[DAYS] \" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[DAYS] nights</td></tr><tr><td class=\"low\" style=\"height: 5px; bgcolor: ffe186;\" colspan=\"3\">&amp;nbsp;</td>[g:rooms]<td>[NAME]</td>[/g:rooms]</tr><tr><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconPerson.png\" alt=\"Insert alt text here\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[NAME]</td><td style=\"width: 10px;\" align=\"left\" valign=\"top\" width=\"10\" height=\"48\">&amp;nbsp;</td><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconKey.png\" alt=\"Insert alt text here\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[ROOMTYPE]</td></tr></tbody></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=\"#eef2f5\" align=\"center\" style=\"padding: 0px 0px 0px 0px;\" class=\"section-padding\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\"><tr><td align=\"center\" valign=\"top\" width=\"660\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#ffca48\" style=\"padding-bottom: 20px; max-width:660px;\" class=\"responsive-table\"><!-- TITLE --><tr><td align=\"center\" height=\"100%\" style=\"padding: 40px;\" valign=\"top\" width=\"100%\" colspan=\"2\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td style=\"font-family: Arial, sans-serif; color: #000000;line-height: 20px;\" class=\"padding\"><br /><br />Kind regards,<br /><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/signature.png\" width=\"298\" height=\"81\" border=\"0\" alt=\"Insert alt text here\" style=\"display: block; color: #000000;  font-family: arial, sans-serif; font-size: 16px;\" class=\"img-max\"></td></tr><tr><td style=\"font-family: Arial, sans-serif; color: #000000;font-size: 10px;line-height: 14px;\" class=\"padding\">Click [unsubscribe]here[/unsubscribe] to never receive the e-mail again</td></tr></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr></table></body></html>";

//            var list = Logic.PlaceholderLogic.GetPlaceholderGroups(body);

//            foreach (var item in list)
//            {
//                Console.WriteLine(item);
//                Debug.WriteLine(item);
//            }

//            var l = new List<string>();

//            Assert.IsTrue(list.Count == 1);

//            Assert.IsTrue(list.All(x => x.StartsWith("[g:") && x.EndsWith("]")));
//        }

//        [TestMethod]
//        public void GetPlaceholderGroupsWithPlaceholders()
//        {
//            string body = "<!DOCTYPE html><html><head><title>A Responsive Email Template</title><meta charset=\"utf-8\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" /><style type=\"text/css\">/* CLIENT-SPECIFIC STYLES */	body, table, td, a {	-webkit-text-size-adjust: 100%;	-ms-text-size-adjust: 100%;	}	/* Prevent WebKit and Windows mobile changing default text sizes */	table, td {	mso-table-lspace: 0pt;	mso-table-rspace: 0pt;	}	/* Remove spacing between tables in Outlook 2007 and up */	img {	-ms-interpolation-mode: bicubic;	}	/* Allow smoother rendering of resized image in Internet Explorer */	/* RESET STYLES */	img {	border: 0;	height: auto;	line-height: 100%;	outline: none;	text-decoration: none;	}	table {	border-collapse: collapse !important;	}	table.diff tr td table tr td table tr td {	padding: 0px !important;	height: 48px !important;	}	table.diff tr td table tr td table tr td.low {	padding: 0px !important;	height: 5px !important;	}	body {	height: 100% !important;	margin: 0 !important;	padding: 0 !important;	width: 100% !important;	}	.heighter {	border-left: solid 2px #eef2f5;	}	/* MOBILE STYLES */	@media screen and (max-width: 525px) {	/* ALLOWS FOR FLUID TABLES */	.wrapper {	width: 100% !important;	max-width: 100% !important;	}	/* ADJUSTS LAYOUT OF LOGO IMAGE */	.logo img {	margin: 0 auto !important;	}	/* USE THESE CLASSES TO HIDE CONTENT ON MOBILE */	.mobile-hide {	display: none !important;	}	.img-max {	max-width: 100% !important;	width: 100% !important;	height: auto !important;	}	/* FULL-WIDTH TABLES */	.responsive-table {	width: 100% !important;	}	/* UTILITY CLASSES FOR ADJUSTING PADDING ON MOBILE */	.padding {	padding: 10px 5% 0px 5% !important;	}	.padding-meta {	padding: 30px 5% 0px 5% !important;	text-align: center;	}	.no-padding {	padding: 0 !important;	}	.section-padding {	padding: 10px 15px 10px 15px !important;	}	.heighter {	height: auto !important;	margin: 0px -17px;	border-bottom: solid 10px #eef2f5;	}	/* ADJUST BUTTONS ON MOBILE */	.mobile-button-container {	margin: 0 auto;	width: 100% !important;	}	.mobile-button {	padding: 15px !important;	border: 0 !important;	font-size: 16px !important;	display: block !important;	}	}	@media screen and (min-device-width: 526px) and (max-device-width: 1070px) {	.wrapper {	overflow: hidden;	float: left !important;	width: 255px !important;	min-width: 25% !important;	}	}</style></head><body bgcolor=\"#eef2f5\" style=\"margin: 0 !important; padding: 0 !important;\"><!-- HEADER --><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td bgcolor=\"#eef2f5\" align=\"center\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\"><tr><td align=\"center\" valign=\"top\" width=\"660\"><![endif]--><table border=\"0\" bgcolor=\"#ffca48\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 660px;\" class=\"wrapper\"><tr><td align=\"left\" valign=\"top\" style=\"padding: 15px 0;\" class=\"logo\"><a href=\"\" target=\"_blank\"><img alt=\"Logo\" src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/logo.png\" width=\"184\" height=\"57\" style=\"display: block; font-family: Helvetica, Arial, sans-serif; color: #ffffff; font-size: 16px;\" border=\"0\"></a></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=\"#eef2f5\" align=\"center\" class=\"section-padding\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\"><tr><td align=\"center\" valign=\"top\" width=\"660\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 660px;\" class=\"responsive-table\"><tr><td><!-- HERO IMAGE --><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"center\" bgcolor=\"ffffff\" style=\"padding-top: 40px;font-family: 'Trebuchet MS', sans-serif;font-size:40px;font-weight: bold;\">Hotelroomselection</td></tr><tr><td bgcolor=\"ffffff\"><!-- COPY --><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"left\" style=\"padding: 20px 40px 40px 40px; font-size: 16px; line-height: 20px; font-family: Arial, sans-serif; color: #000000;\" class=\"padding\"><center><span style=\"width: 140px; font-family: 'Trebuchet MS', sans-serif; color: #000000; font-size: 16px; line-height: 14px; border-radius: 4px; display: block; text-decoration: none;\">You have claimed</span><br /><a style=\"width: 340px; font-family: 'Trebuchet MS', sans-serif; color: #000000; font-size: 56px; line-height: 14px; padding: 15px 60px; border-radius: 4px; display: block; text-decoration: none;\">[ROOMNUMBER]</a></center></td></tr></table></td></tr><tr><td bgcolor=\"ffffff\" align=\"center\" style=\"padding-bottom: 20px;\"></td></tr></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=\"#eef2f5\" align=\"center\" style=\"padding: 0px 0px 0px 0px;\" class=\"section-padding\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\" class=\"diff\"><tr><td align=\"center\" valign=\"middle\" width=\"660\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 660px;\" bgcolor=\"ffe186\" class=\"responsive-table\"><tr><td align=\"left\" height=\"100%\" valign=\"top\" style=\"padding:40px;font-family: Arial, sans-serif; color: #000000;\" width=\"100%\"><table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconHash.png\" alt=\"[BOOKINGNUMBER]\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[BOOKINGNUMBER]</td><td style=\"width: 10px;\" align=\"left\" valign=\"top\" width=\"10\" height=\"48\">&amp;nbsp;</td><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconPlane.png\" alt=\"[DATESTART]\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[DATESTART]</td></tr><tr><td class=\"low\" style=\"height: 5px; bgcolor: ffe186;\" colspan=\"3\">&amp;nbsp;</td></tr><tr><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconSleep.png\" alt=\"[HOTEL]\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[HOTEL]</td><td style=\"width: 10px;\" align=\"left\" valign=\"top\" width=\"10\" height=\"48\">&amp;nbsp;</td><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconCalendar.png\" alt=\"[DAYS] \" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[DAYS] nights</td></tr><tr><td class=\"low\" style=\"height: 5px; bgcolor: ffe186;\" colspan=\"3\">&amp;nbsp;</td>[g:rooms]<td>[NAME] [DAYS]</td>[/g:rooms]</tr><tr><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconPerson.png\" alt=\"Insert alt text here\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[NAME]</td><td style=\"width: 10px;\" align=\"left\" valign=\"top\" width=\"10\" height=\"48\">&amp;nbsp;</td><td style=\"display: inline-block; height: 48px; width: 48px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"48\" height=\"48\"><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/iconKey.png\" alt=\"Insert alt text here\" width=\"48\" height=\"48\" border=\"0\" /></td><td style=\"display: inline-block; height: 32px; width: 212px; padding: 16px 0px 0px 20px; font-family: Arial, sans-serif;\" align=\"left\" valign=\"middle\" bgcolor=\"#ffffff\" width=\"212\" height=\"48\">&amp;nbsp;[ROOMTYPE]</td></tr></tbody></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><tr><td bgcolor=\"#eef2f5\" align=\"center\" style=\"padding: 0px 0px 0px 0px;\" class=\"section-padding\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"660\"><tr><td align=\"center\" valign=\"top\" width=\"660\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#ffca48\" style=\"padding-bottom: 20px; max-width:660px;\" class=\"responsive-table\"><!-- TITLE --><tr><td align=\"center\" height=\"100%\" style=\"padding: 40px;\" valign=\"top\" width=\"100%\" colspan=\"2\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td style=\"font-family: Arial, sans-serif; color: #000000;line-height: 20px;\" class=\"padding\"><br /><br />Kind regards,<br /><img src=\"https://hotelroomselection.blob.core.windows.net/tst/mail/signature.png\" width=\"298\" height=\"81\" border=\"0\" alt=\"Insert alt text here\" style=\"display: block; color: #000000;  font-family: arial, sans-serif; font-size: 16px;\" class=\"img-max\"></td></tr><tr><td style=\"font-family: Arial, sans-serif; color: #000000;font-size: 10px;line-height: 14px;\" class=\"padding\">Click [unsubscribe]here[/unsubscribe] to never receive the e-mail again</td></tr></table></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr></table></body></html>";

//            var list = Logic.PlaceholderLogic.GetPlaceholderGroupsWithPlaceholders(body);

//            var placeholderCount = 0;

//            foreach (var item in list)
//            {
//                placeholderCount += item.PlaceholderTags.Count;
//                Console.WriteLine($"{item.Name} {string.Join(", ", item.PlaceholderTags)}");
//            }

//            Assert.IsTrue(list.Count == 1);
//            Assert.IsTrue(placeholderCount == 2);
//        }

//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest5")]
//        public void GetPlaceholderTagsForGroup()
//        {
//            var list = Logic.PlaceholderLogic.GetPlaceholderGroups(BodyWithGroups);

//            var placeholderCount = 0;

//            foreach (var item in list)
//            {
//                Console.WriteLine($"Found group {item}");

//                var placeholderTags = Logic.PlaceholderLogic.GetPlaceholderTags(item);

//                foreach (var placeholderTag in placeholderTags)
//                {
//                    placeholderCount++;

//                    Console.WriteLine($"  - Tag {placeholderTag}");
//                }
//            }

//            Assert.IsTrue(list.Count == 1);
//            Assert.IsTrue(placeholderCount == 2);
//        }

//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest6")]
//        public void IsValidTemplate_Valid()
//        {
//            var isValid = Logic.Helper.IsValidTemplate(BodyWithGroups, "subject");

//            Assert.IsTrue(isValid);
//        }

//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest7")]
//        public void IsValidTemplate_Valid_WithComplexBrackets()
//        {
//            var textWithPlaceholderTags = "Hello World, [TEST!(Here's some text that is invalid, with ((I assume) working parentheses.)]";
//            var isValid = Logic.Helper.IsValidTemplate(textWithPlaceholderTags, "Subject");

//            Assert.IsTrue(isValid);
//        }

//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest8")]
//        public void IsValidTemplate_ValidWithPlaceholdersAndBrackets()
//        {
//            var isValid = Logic.Helper.IsValidTemplate(TextWithPlaceholdersAndBrackets, "subject");

//            Assert.IsTrue(isValid);
//        }

//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest9")]
//        public void IsPlaceholder_Valid_StringEmpty()
//        {
//            var placeholder = string.Empty;
//            var isValid = Logic.Helper.IsPlaceholder(placeholder);
//            Assert.IsTrue(isValid);
//        }

//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest10")]
//        public void IsPlaceholder_Valid_Simple()
//        {
//            var placeholder = "[TEST]";
//            var isValid = Logic.Helper.IsPlaceholder(placeholder) &&
//                Logic.Helper.IsValidPlaceholder(placeholder);;
//            Assert.IsTrue(isValid);
//        }

//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest11")]
//        public void IsPlaceholder_Invalid_Simple()
//        {
//            var placeholder = "[TEST!]";
//            var isValid = Logic.Helper.IsPlaceholder(placeholder) &&
//                Logic.Helper.IsValidPlaceholder(placeholder);
//            Assert.IsFalse(isValid);
//        }

//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest15")]
//        public void IsPlaceholder_Valid_WithComplexBrackets()
//        {
//            var textWithPlaceholderTags = "<html><body>I just want a [ bracket here and [ another and this is not ] a placeholder. Should be fine like 1.). [CUSTOMER][ROOM]<td>[NAME] [ROOMNUMBER]</td></body></html>";

//            var reg = new Regex(Logic.PlaceholderLogic.Pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
//            var matches = reg.Matches(textWithPlaceholderTags);

//            foreach (Match match in matches)
//            {
//                Console.WriteLine(match.Value);
//                var isValid = false;

//                if (Logic.Helper.IsPlaceholder(match.Value))
//                    isValid = Logic.Helper.IsValidPlaceholder(match.Value);

//                Assert.IsTrue(isValid);
//            }

//        }

//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest17")]
//        public void IsPlaceholder_Valid_WithGroups()
//        {
//            var textWithPlaceholderTags = TextWithPlaceholdersAndPlaceholderGroupsAndBrackets;

//            var pattern = Logic.PlaceholderLogic.Pattern;

//            var reg = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
//            var matches = reg.Matches(textWithPlaceholderTags);

//            bool isValid = true;
//            foreach (Match match in matches)
//            {
//                Console.WriteLine(match.Value);
//                if(Logic.Helper.IsPlaceholder(match.Value))
//                    isValid = Logic.Helper.IsValidPlaceholder(match.Value);

//                if (!isValid)
//                    break;
//            }

//            Assert.IsTrue(isValid);
//        }


//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest18")]
//        public void IsPlaceholderGroup_Valid_WithGroups()
//        {
//            var textWithPlaceholderTags = TextWithPlaceholdersAndPlaceholderGroupsAndBrackets;

//            var pattern = Logic.PlaceholderLogic.PatternGroup;

//            var reg = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
//            var matches = reg.Matches(textWithPlaceholderTags);

//            bool isValid = true;
//            foreach (Match match in matches)
//            {
//                Console.WriteLine(match.Value);
//                isValid = Logic.Helper.IsPlaceholderGroup(match.Value) &&
//                    Logic.Helper.IsValidPlaceholderGroup(match.Value);

//                if (!isValid)
//                    break;
//            }

//            Assert.IsTrue(isValid);
//        }


//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest19")]
//        public void IsPlaceholderGroup_InValid_DifferentClosingTag()
//        {
//            var textWithPlaceholderTags = "<html><body>I just want a [ bracket here and [ another and this is not ] a placeholder. Should be fine like 1.). [CUSTOMER][ROOM][g:rooms]<td>[NAME] [ROOMNUMBER]</td>[/g:brooms]</body></html>";

//            var pattern = Logic.PlaceholderLogic.PatternGroup;

//            var reg = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
//            var matches = reg.Matches(textWithPlaceholderTags);

//            bool isValid = true;
//            foreach (Match match in matches)
//            {
//                Console.WriteLine(match.Value);
//                isValid = Logic.Helper.IsPlaceholderGroup(match.Value) &&
//                    Logic.Helper.IsValidPlaceholderGroup(match.Value);

//                if (!isValid)
//                    break;
//            }

//            Assert.IsFalse(isValid);
//        }

//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest20")]
//        public void IsPlaceholder_Valid_Small()
//        {
//            var placeholder = "asdf [T] aasdf";
//            var isValid = Logic.Helper.IsValidTemplate(placeholder, "subject");
//            Assert.IsTrue(isValid);

//        }

//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest21")]
//        public void TemplateSave_Invalid_IdentifierNotUnique()
//        {
//            var template = new MailTemplate.Data.MailTemplate();

//            template.BCCReceivers = "robin.witteman@supershift.nl";
//            template.Body = BodyWithGroups;
//            template.DateCreated = DateTime.UtcNow;
//            template.DateLastUpdated = DateTime.UtcNow;
//            template.DefaultSenderEmail = "robin.witteman@supershift.nl";
//            template.DefaultSenderName = "Robin Witteman";
//            template.Identifier = "TEMPLATE1";
//            template.IsArchived = false;
//            template.IsPublished = true;
//            template.Name = "template one";
//            template.Subject = "Template one subject";
//            template.VersionMajor = 1;
//            template.VersionMinor = 0;

//            var isSuccess = template.Save(User);

//            Assert.IsFalse(isSuccess > 0);
//        }
//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest22")]
//        public void TemplateSave_Valid()
//        {
//            var template = new MailTemplate.Data.MailTemplate();

//            template.BCCReceivers = "robin.witteman@supershift.nl";
//            template.Body = BodyWithGroups;
//            template.DateCreated = DateTime.UtcNow;
//            template.DateLastUpdated = DateTime.UtcNow;
//            template.DefaultSenderEmail = "robin.witteman@supershift.nl";
//            template.DefaultSenderName = "Robin Witteman";
//            template.Description = "Template Description";
//            template.Identifier = $"TEMPLATE-{Guid.NewGuid()}";
//            template.IsArchived = false;
//            template.IsPublished = true;
//            template.Name = "template Generated GUID";
//            template.Subject = "Template Generated GUID";
//            template.VersionMajor = 1;
//            template.VersionMinor = 0;

//            var isSuccess = template.Save(User);

//            Assert.IsTrue(isSuccess > 0);
//        }
//        [TestMethod, TestCategory("Functional Tests"), TestCategory("zTest22")]
//        public void TemplateEdit_Valid()
//        {
//            var template = new MailTemplate.Data.MailTemplate();
//            template.ID = 1;
//            template.BCCReceivers = "robin.witteman@supershift.nl";
//            template.Body = BodyWithGroups;
//            template.DateCreated = DateTime.UtcNow;
//            template.DateLastUpdated = DateTime.UtcNow;
//            template.DefaultSenderEmail = "robin.witteman@supershift.nl";
//            template.DefaultSenderName = "Robin Witteman";
//            template.Description = "Template Description";
//            template.Identifier = $"TEMPLATE-{Guid.NewGuid()}";
//            template.IsArchived = false;
//            template.IsPublished = true;
//            template.Name = "template Generated GUID";
//            template.Subject = "Template Generated GUID";
//            template.VersionMajor = 1;
//            template.VersionMinor = 0;

//            var isSuccess = template.Save(User);

//            Assert.IsTrue(isSuccess > 0);
//        }

//        //[TestMethod]
//        public void SendMail()
//        {
//            // fails because it's missing Request object etc..
//            try
//            {
//                var s = new UI.SendTestMail_List();
//                s.SendTestMail(4);
//            }
//            catch (Exception ex)
//            {

//                Assert.Fail(ex.ToString());
//            }
//        }

//        [TestMethod]
//        public void GetDefaultValueForTemplate_Robin6()
//        {
//            try
//            {
//                var templates = Data.MailTemplate.SelectAllByIdentifier("Robin 6");

//                var guid = templates.First().GUID;

//                var defaultvalues = Data.DefaultValuePlaceholder.SelectAllByMailTemplateGUID(guid);

//                var count = 0;

//                foreach (var defaultvalue in defaultvalues)
//                {
//                    count++;
//                    Debug.WriteLine($"{defaultvalue.Placeholder} - {defaultvalue.Value}");
//                }

//                Assert.IsTrue(count == 2);
//            }
//            catch (Exception ex)
//            {

//                Assert.Fail(ex.ToString());
//            }
//        }

//        [TestMethod]
//        public void GetDefaultValueAndReplaceForTemplate_Robin6()
//        {
//            try
//            {
//                var templates = Data.MailTemplate.SelectAllByIdentifier("Robin 6");

//                var guid = templates.First().GUID;

//                var defaultvalues = Data.DefaultValuePlaceholder.SelectAllByMailTemplateGUID(guid);

//                foreach (var defaultvalue in defaultvalues)
//                {
//                    Debug.WriteLine($"{defaultvalue.Placeholder} - {defaultvalue.Value}");
//                }

//                var replaced = Logic.PlaceholderLogic.ApplyPlaceholders(templates.Last().ID);

//                foreach (var defaultvalue in defaultvalues)
//                {
//                    Assert.IsTrue(replaced.Body.Contains(defaultvalue.Value));
//                }
//            }
//            catch (Exception ex)
//            {

//                Assert.Fail(ex.ToString());
//            }
//        }
//    }
//}
