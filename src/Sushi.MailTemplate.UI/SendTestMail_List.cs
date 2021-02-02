using System.Collections.Generic;
using System.Linq;
using Sushi.MailTemplate.Entities;
using System.Threading.Tasks;
using Wim.Framework;

namespace Sushi.MailTemplate.UI
{
    /// <summary>
    /// SendTestMail_List Component List
    /// </summary>
    public class SendTestMail_List : Wim.Framework.ComponentListTemplate
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> Errors { get; set; }
        /// <summary>
        /// SendTestMail_List ctor
        /// </summary>
        public SendTestMail_List()
        {
            wim.OpenInEditMode = true;
            wim.CanContainSingleInstancePerDefinedList = true;
            if(wim.CurrentList != null) wim.CurrentList.Label_Save = "Send";

            ListLoad += SendTestMail_List_ListLoad;
            ListSave += SendTestMail_List_ListSave;
            ListPreRender += SendTestMail_List_ListPreRender;
        }

        private void SendTestMail_List_ListPreRender(IComponentListTemplate sender, ComponentListEventArgs e)
        {
            var idQueryString = Request.QueryString["item"];

            int.TryParse(idQueryString, out int id);
            var mailTemplate = Data.MailTemplate.FetchSingle(id);
            EmailFrom = EmailFrom ?? mailTemplate.DefaultSenderEmail;
            EmailTo = EmailTo ?? wim.CurrentApplicationUser.Email;

            if (string.IsNullOrWhiteSpace(mailTemplate.Subject))
            {
                wim.Notification.AddError("A subject is mandatory, please supply a subject before sending a test e-mail. E-mail can't be sent.");
            }
        }

        private void SendTestMail_List_ListSave(IComponentListTemplate sender, ComponentListEventArgs e)
        {
            var idQueryString = Request.QueryString["item"];

            int.TryParse(idQueryString, out int id);
            SendTestMail(id);

        }
        /// <summary>
        /// Send the current mail template with the provided values to the handler
        /// </summary>
        /// <param name="id"></param>
        public void SendTestMail(int id)
        {
            var mailTemplate = Data.MailTemplate.FetchSingle(id);

            var placeholderSubject = CreatePlaceholderObject(mailTemplate.Subject);
            var placeholderBody = CreatePlaceholderObject(mailTemplate.Body);
            var placeholderGroupSubject = CreatePlaceholderGroupsObject(mailTemplate.Subject);
            var placeholderGroupBody = CreatePlaceholderGroupsObject(mailTemplate.Body);

            var sectionBody = CreateSectionObject(mailTemplate.Body);

            mailTemplate = Logic.PlaceholderLogic.ApplyPlaceholders(mailTemplate, placeholderGroupBody, placeholderBody, sectionBody);

            var body = mailTemplate.Body;
            var subject = mailTemplate.Subject;

            if (string.IsNullOrWhiteSpace(subject))
            {
                // not possible to send an e-mail without a subject
                wim.CurrentVisitor.Data.Apply("wim.note", "A subject is mandatory, please supply a subject before sending a test e-mail. E-mail wasn't sent.");
                wim.CurrentVisitor.Save();
                return;
            }

            var e = new Logic.SendPreviewEmailEventArgs { EmailFrom = EmailFrom, EmailTo = EmailTo, Subject = subject, Body = body, TemplateName = mailTemplate.Identifier, EmailFromName = mailTemplate.DefaultSenderName };
            var handler = new Logic.SendPreviewEmailEventHandler();
            handler.OnSendPreviewEmail(e);

            if (e.IsSuccess)
            {
                // send successfull
                wim.CurrentVisitor.Data.Apply("wim.note", "E-mail has been sent successfully");
                wim.CurrentVisitor.Save();
            }
            else
            {
                wim.CurrentVisitor.Data.Apply("wim.note", "E-mail wasn't sent properly");
                wim.CurrentVisitor.Save();
            }
        }
        
        private void SendTestMail_List_ListLoad(IComponentListTemplate sender, ComponentListEventArgs e)
        {

            var idQueryString = Request.QueryString["item"];

            int.TryParse(idQueryString, out int id);

            var mailTemplate = Data.MailTemplate.FetchSingle(id);
            
            var placeholdersSubject = Logic.PlaceholderLogic.GetPlaceholderTags(mailTemplate.Subject);
            foreach (var placeholder in placeholdersSubject)
            {
                wim.Form.AddElement(this, placeholder, true, true,
                    new Wim.Framework.ContentListItem.TextFieldAttribute(placeholder, 4000, true));
            }

            var placeholdersBody = Logic.PlaceholderLogic.GetPlaceholderTags(mailTemplate.Body);
            foreach (var placeholder in placeholdersBody)
            {
                wim.Form.AddElement(this, placeholder, true, true,
                    new Wim.Framework.ContentListItem.TextFieldAttribute(placeholder, 4000, true));
            }

            var sections = Logic.PlaceholderLogic.GetSectionTags(mailTemplate.Body);
            foreach (var section in sections)
            {
                wim.Form.AddElement(this, section, true, true,
                    new Wim.Framework.ContentListItem.Choice_CheckboxAttribute(section, false));
            }
        }

        private List<Placeholder> CreatePlaceholderObject(string textWithTags)
        {
            var result = new List<Placeholder>();
            var placeholderTags = Logic.PlaceholderLogic.GetPlaceholderTags(textWithTags);

            foreach (var placeholderTag in placeholderTags)
            {
                var placeholders = GetPlaceholdersFromRequest(placeholderTag);
                result.AddRange(placeholders);
            }

            return result;
        }

        private List<string> CreateSectionObject(string textWithTags)
        {
            var result = new List<string>();
            var sectionTags = Logic.PlaceholderLogic.GetSectionTags(textWithTags);

            foreach (var sectionTag in sectionTags)
            {
                var sections = GetSectionsFromRequest(sectionTag);
                result.AddRange(sections);
            }

            return result;
        }

        private List<PlaceholderGroup> CreatePlaceholderGroupsObject(string textWithTags)
        {
            var placeholderGroups = Logic.PlaceholderLogic.GetPlaceholderGroupsWithPlaceholders(textWithTags);

            foreach (var placeholderGroup in placeholderGroups)
            {
                var placeholderReplacers = new Dictionary<string, List<string>>();

                // repeat 3 times so the test e-mail has some content
                for (int i = 0; i < 3; i++)
                {
                    placeholderGroup.AddNewRow();

                    foreach (var placeholderTag in placeholderGroup.PlaceholderTags)
                    {
                        var dict = GetPlaceholdersFromRequest(placeholderTag);

                        foreach (var item in dict)
                        {
                            placeholderGroup.AddNewRowItem(item.Name, $"{item.Value}");
                        }
                    }
                }
            }

            return placeholderGroups;
        }

        private List<Placeholder> GetPlaceholdersFromRequest(string fieldIdentifier)
        {
            var placeholders = new List<Placeholder>();
            if (Request != null && Request.Form != null && Request.Form.HasKeys() && !string.IsNullOrEmpty(fieldIdentifier))
            {
                foreach (var formKey in Request.Form.AllKeys.Where(x => x.EndsWith(fieldIdentifier)))
                {
                    string identifier = formKey.Split(new[] { '_' }, 2).LastOrDefault();
                    if (!string.IsNullOrEmpty(identifier))
                    {
                        string value = Request[formKey];
                        // for test purposes, if supplied with "null", make it a real null so testing with defaults is possible
                        if (value.ToLower() == "null")
                            value = null;

                        placeholders.Add(new Placeholder(identifier, value));
                    }
                }
            }

            return placeholders;
        }

        private List<string> GetSectionsFromRequest(string fieldIdentifier)
        {
            var sections = new List<string>();
            if (Request != null && Request.Form != null && Request.Form.HasKeys() && !string.IsNullOrEmpty(fieldIdentifier))
            {
                foreach (var formKey in Request.Form.AllKeys.Where(x => x.EndsWith(fieldIdentifier)))
                {
                    string identifier = formKey.Split(new[] { '_' }, 2).LastOrDefault();
                    if (!string.IsNullOrEmpty(identifier))
                    {
                        string value = Request[formKey];
                        // a checkbox returns on
                        if (value.ToLower() == "1")
                        {
                            sections.Add(identifier);
                        }
                    }
                }
            }

            return sections;
        }

        /// <summary>
        /// Explanation of this window
        /// </summary>
        [Wim.Framework.ContentListItem.TextLine("")]
        public string Title { get
            {
                return @"Use this page to send a test e-mail to the e-mail address provided. Fill in the placeholders below.
Leave empty to fill with default values. Fill in with ""null"" to remove from the template.";
            }
        }

        /// <summary>
        /// E-mail textbox
        /// </summary>
        [Wim.Framework.ContentListItem.TextField("Email To", 255, IsRequired = true, Mandatory = true)]
        public string EmailTo { get; set; }
        /// <summary>
        /// E-mail textbox
        /// </summary>
        [Wim.Framework.ContentListItem.TextField("Email From", 255, IsRequired = true, Mandatory = true)]
        public string EmailFrom { get; set; }

    }
}
