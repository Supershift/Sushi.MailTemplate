using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sushi.MailTemplate.SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sushi.MailTemplate.Tests
{
    [TestClass]
    public class TestPublic
    {
        IConfigurationRoot Configuration;

        private static string TEST_SENDER_EMAIL = "[YOUR_TEST_EMAIL_ADDRESS]";
        private static string TEST_SENDER_NAME = "[YOUR_TEST_NAME]";
        private static string TEST_RECEIVER_EMAIL = "[YOUR_TEST_EMAIL_ADDRESS]";
        private static string TEST_USER_NAME = "[YOUR_TEST_NAME]";
        private static string TEST_USER_EMAIL = "[YOUR_TEST_EMAIL_ADDRESS]";
        private static string TEST_TEMPLATE_ID = "INTERNAL-TEST";
        private static string TEST_TEMPLATE_ID_GROUPS = "INTERNAL-TEST-GROUPS";
        private static string TEST_TEMPLATE_ID_SECTIONS = "INTERNAL-TEST-SECTIONS";
        private static string TEST_QUEUED_MAIL_ID = "[YOUR_QUEUED_EMAIL_IDENTIFIER]";

        private static string[] TEST_TEMPLATES = new string[] 
        { 
            TEST_TEMPLATE_ID, 
            TEST_TEMPLATE_ID_GROUPS, 
            TEST_TEMPLATE_ID_SECTIONS 
        };

        [TestInitialize]
        public void Init()
        {
            Configuration = new ConfigurationBuilder()                                                          
                                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                                .AddUserSecrets<TestPublic>()
                                .AddEnvironmentVariables()
                                .Build();            
            
            MicroORM.DatabaseConfiguration.SetDefaultConnectionString(Configuration.GetConnectionString("datastore"));
        }

        [TestMethod]
        public async Task QueueMail()
        {
            var emailStorageAccount = Configuration["EmailStorageAccount"];
            var emailBlobContainer = Configuration["EmailBlobContainer"];
            var emailQueueName = Configuration["EmailQueueName"];
            var sendGridAPIKey = Configuration["SendGridAPIKey"];
            var mailer = new Mailer(emailStorageAccount, emailBlobContainer, emailQueueName, sendGridAPIKey);

            var mail = new Data.MailTemplate
            {
                DefaultSenderEmail = TEST_SENDER_EMAIL,
                DefaultSenderName = TEST_SENDER_NAME,
                Subject = "Test",
                Body = "body",
                Identifier = TEST_TEMPLATE_ID
            };

            var result = await mailer.QueueMailAsync(mail, emailTo: TEST_RECEIVER_EMAIL, customerGUID: null);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task PublishAllTestTemplates()
        {
            foreach (var item in TEST_TEMPLATES)
            {
                var mailTemplate = await MailTemplate.FetchAsync(item, false);
                if (mailTemplate?.ID > 0)
                {
                    await mailTemplate.SaveAsync(mailTemplate.UserID.Value, TEST_USER_NAME, TEST_USER_EMAIL);
                    await mailTemplate.PublishAsync(mailTemplate.UserID.Value, TEST_USER_NAME, TEST_USER_EMAIL);
                }
                else 
                {
                    Assert.Fail($"Mailtemplate '{item}' wasn't found");
                }
            }
            
        }

        [TestMethod]
        public async Task SendMailFromBlob()
        {
            try
            {
                var id = TEST_QUEUED_MAIL_ID;
                var emailStorageAccount = Configuration["EmailStorageAccount"];
                var emailBlobContainer = Configuration["EmailBlobContainer"];
                var emailQueueName = Configuration["EmailQueueName"];
                var sendGridAPIKey = Configuration["SendGridAPIKey"];
                var mailer = new Mailer(emailStorageAccount, emailBlobContainer, emailQueueName, sendGridAPIKey);
                var result = await mailer.SendMailFromBlobAsync(id);

                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public async Task SendMailAsync()
        {
            try
            {
                var emailStorageAccount = Configuration["EmailStorageAccount"];
                var emailBlobContainer = Configuration["EmailBlobContainer"];
                var emailQueueName = Configuration["EmailQueueName"];
                var sendGridAPIKey = Configuration["SendGridAPIKey"];
                var mailer = new Mailer(emailStorageAccount, emailBlobContainer, emailQueueName, sendGridAPIKey);

                var template = await MailTemplate.FetchAsync(TEST_TEMPLATE_ID);

                template.PlaceholderList.Add("PREVIEWTEXT", "TEST: This is the previewtext");
                template.PlaceholderList.Add("INTRO", "TEST: This is the intro text");
                template.PlaceholderList.Add("BUTTONLINK", "https://www.google.nl");
                template.PlaceholderList.Add("BUTTONLABEL", "TEST: This is the Button label");
                template.PlaceholderList.Add("OUTRO", "TEST: This is the outro text");
                template.PlaceholderList.Add("FOOTERTITLE", "TEST: This is the footer title");

                template.PlaceholderGroupList.Add("REPEATER");
                template.PlaceholderGroupList.AddNewRow();
                template.PlaceholderGroupList.AddNewRowItem("REPEATEDPLACEHOLDER", "TEST: This is a repeated placeholder 1");
                template.PlaceholderGroupList.AddNewRow();
                template.PlaceholderGroupList.AddNewRowItem("REPEATEDPLACEHOLDER", "TEST: This is a repeated placeholder 2");
                template.PlaceholderGroupList.AddNewRow();
                template.PlaceholderGroupList.AddNewRowItem("REPEATEDPLACEHOLDER", "TEST: This is a repeated placeholder 3");

                template = await MailTemplate.ApplyPlaceholdersAsync(template, Console.Out);

                var email = new Email
                {
                    Body = template.Body,
                    From = template.DefaultSenderEmail,
                    FromName = template.DefaultSenderName,
                    To = TEST_RECEIVER_EMAIL,
                    ID = Guid.NewGuid(),
                    TemplateName = template.Identifier,
                    Subject = template.Subject
                };

                var result = await mailer.SendMailAsync(email);

                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public async Task GetTemplateAndApplyPlaceholders()
        {
            var template = await MailTemplate.FetchAsync(TEST_TEMPLATE_ID);

            if (template?.ID > 0)
            {
                template.PlaceholderList.Add("PREVIEWTEXT", "TEST: This is the previewtext");
                template.PlaceholderList.Add("INTRO", "TEST: This is the intro text");
                template.PlaceholderList.Add("BUTTONLINK", "https://www.google.nl");
                template.PlaceholderList.Add("BUTTONLABEL", "TEST: This is the Button label");
                template.PlaceholderList.Add("OUTRO", "TEST: This is the outro text");
                template.PlaceholderList.Add("FOOTERTITLE", "TEST: This is the footer title");

                var mail = await MailTemplate.ApplyPlaceholdersAsync(template, Console.Out);

                Console.WriteLine(mail);

                Assert.IsFalse(string.IsNullOrWhiteSpace(mail.Body));
            }
            else
            {
                Assert.Fail($"Mailtemplate '{TEST_TEMPLATE_ID}' wasn't found");
            }
        }

        [TestMethod]
        public async Task GetTemplateAndApplyPlaceholdersWithGroups()
        {
            var template = await MailTemplate.FetchAsync(TEST_TEMPLATE_ID_GROUPS);

            if (template?.ID > 0)
            {
                template.PlaceholderList.Add("PREVIEWTEXT", "TEST: This is the previewtext");
                template.PlaceholderList.Add("INTRO", "TEST: This is the intro text");
                template.PlaceholderList.Add("BUTTONLINK", "https://www.google.nl");
                template.PlaceholderList.Add("BUTTONLABEL", "TEST: This is the Button label");
                template.PlaceholderList.Add("OUTRO", "TEST: This is the outro text");
                template.PlaceholderList.Add("FOOTERTITLE", "TEST: This is the footer title");

                template.PlaceholderGroupList.Add("REPEATER");
                template.PlaceholderGroupList.AddNewRow();
                template.PlaceholderGroupList.AddNewRowItem("REPEATEDPLACEHOLDER", "TEST: This is a repeated placeholder 1");
                template.PlaceholderGroupList.AddNewRow();
                template.PlaceholderGroupList.AddNewRowItem("REPEATEDPLACEHOLDER", "TEST: This is a repeated placeholder 2");
                template.PlaceholderGroupList.AddNewRow();
                template.PlaceholderGroupList.AddNewRowItem("REPEATEDPLACEHOLDER", "TEST: This is a repeated placeholder 3");

                var mail = await MailTemplate.ApplyPlaceholdersAsync(template, Console.Out);

                Console.WriteLine(mail.Body);

                Assert.IsFalse(string.IsNullOrWhiteSpace(mail.Body));
            }
            else
            {
                Assert.Fail($"Mailtemplate '{TEST_TEMPLATE_ID_GROUPS}' wasn't found");
            }
        }

        [TestMethod]
        public async Task GetTemplateAndApplyPlaceholdersWithSections()
        {
            var template = await MailTemplate.FetchAsync(TEST_TEMPLATE_ID_SECTIONS);

            if (template?.ID > 0)
            {
                template.PlaceholderList.Add("PREVIEWTEXT", "TEST: This is the previewtext");
                template.PlaceholderList.Add("INTRO", "TEST: This is the intro text");
                template.PlaceholderList.Add("BUTTONLINK", "https://www.google.nl");
                template.PlaceholderList.Add("BUTTONLABEL", "TEST: This is the Button label");
                template.PlaceholderList.Add("OUTRO", "TEST: This is the outro text");
                template.PlaceholderList.Add("FOOTERTITLE", "TEST: This is the footer title");
                template.PlaceholderList.Add("SECTIONNAME", "TEST: This is the optional section name");
                
                template.OptionalSections.Add("OPTIONALSECTION");

                var mail = await MailTemplate.ApplyPlaceholdersAsync(template, Console.Out);

                Console.WriteLine(mail.Body);

                Assert.IsFalse(string.IsNullOrWhiteSpace(mail.Body));
            }
            else
            {
                Assert.Fail($"Mailtemplate '{TEST_TEMPLATE_ID_SECTIONS}' wasn't found");
            }
        }

        [TestMethod]
        public async Task GetSectionTags()
        {
            var mailTemplate = await MailTemplate.FetchAsync(TEST_TEMPLATE_ID_SECTIONS);
            var sections = Logic.PlaceholderLogic.GetSectionTags(mailTemplate.Body, logger: Console.Out);
            
            Assert.IsTrue(sections.Any());
        }

        [TestMethod]
        public async Task DeleteMailTemplate()
        {
            var template = await MailTemplate.FetchAsync("TEMPLATE1");
            var deleted = await Data.MailTemplate.DeleteAsync(new List<int> { template.ID });

            Assert.IsTrue(deleted);
        }

        [TestMethod]
        public async Task MailTemplateListFetchAll()
        {
            var items = await Data.MailTemplateList.FetchAllAsync();

            Assert.IsTrue(items.Any());
        }


        [TestMethod]
        public async Task FetchAllByIdentifiers()
        {
            var mailTemplates = await Data.MailTemplate.FetchAllByIdentifiersAsync(TEST_TEMPLATES, false);

            bool containsFirst = mailTemplates.Any(x => x.Identifier == TEST_TEMPLATE_ID);
            bool containsSecond = mailTemplates.Any(x => x.Identifier == TEST_TEMPLATE_ID_GROUPS);
            bool containsThird = mailTemplates.Any(x => x.Identifier == TEST_TEMPLATE_ID_SECTIONS);

            Assert.IsTrue(containsFirst && containsSecond && containsThird);
        }
    }
}
