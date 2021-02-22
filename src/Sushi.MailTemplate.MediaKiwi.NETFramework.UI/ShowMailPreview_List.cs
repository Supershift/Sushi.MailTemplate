using System;
using System.Web;
using Wim.Framework;

namespace Sushi.MailTemplate.MediaKiwi.UI.MK
{
    /// <summary>
    /// ShowMailPreview_List Component List
    /// </summary>
    public class ShowMailPreview_List : ComponentListTemplate
    {
        /// <summary>
        /// ShowMailPreview_List ctor
        /// </summary>
        public ShowMailPreview_List()
        {
            wim.CanContainSingleInstancePerDefinedList = true;
            wim.OpenInEditMode = true;
            ListLoad += ShowMailPreview_ListLoad;
        }

        void ShowMailPreview_ListLoad(object sender, ComponentListEventArgs e)
        {
            var mailTemplateKey = Context.Request["MailTemplateID"];
            if (!string.IsNullOrEmpty(mailTemplateKey))
            {
                var mailTemplate = Data.MailTemplate.FetchSingle(int.Parse(mailTemplateKey));

                var body = HttpUtility.HtmlDecode(mailTemplate.Body);

                //with the mail charset an Â shows instead of &nbsp: http://stackoverflow.com/questions/1461907/html-encoding-issues-%C3%82-character-showing-up-instead-of-nbsp
                body = body?.Replace(@"<meta http-equiv=""Content-Type"" content=""text/html; charset=ISO-8859-1"" />", @"<meta http-equiv=""Content-Type"" content=""text/html;charset=utf-8"" />");

                var subject = mailTemplate.Subject;

                Response.Clear();
                Response.Write($"<div style='margin-left:10px'><h3 style='margin-left: 50px'>{subject}</h3><p>{body}</p></div>");
                Response.End();
            }
        }
    }
}
