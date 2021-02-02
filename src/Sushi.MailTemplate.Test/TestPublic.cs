using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sushi.MailTemplate.SendGrid;

namespace Sushi.MailTemplate.Test
{
    [TestClass]
    public class TestPublic
    {
        [TestInitialize]
        public void Init()
        {
            string defaultConnectionString = Wim.Data.Common.DatabaseConnection;

            Sushi.MicroORM.DatabaseConfiguration.SetDefaultConnectionString(defaultConnectionString);
        }

        [TestMethod]
        public async Task QueueMail()
        {
            var emailStorageAccount = System.Configuration.ConfigurationManager.AppSettings["EmailStorageAccount"];
            var emailBlobContainer = System.Configuration.ConfigurationManager.AppSettings["EmailBlobContainer"];
            var emailQueueName = System.Configuration.ConfigurationManager.AppSettings["EmailQueueName"];
            var sendGridAPIKey = System.Configuration.ConfigurationManager.AppSettings["SendGridAPIKey"];
            var mailer = new Mailer(emailStorageAccount, emailBlobContainer, emailQueueName, sendGridAPIKey);

            var mail = new Data.MailTemplate
            {
                DefaultSenderEmail = "robin.witteman@supershift.nl",
                DefaultSenderName = "Robin",
                Subject = "Test",
                Body = "body",
                Identifier = "TESTTEMPLATE"
            };

            var result = await mailer.QueueMailAsync(mail, emailTo: "robin.witteman@supershift.nl", customerGUID: null);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Publish()
        {
            var mailTemplate = MailTemplate.Fetch("TEMPLATEONE");
            mailTemplate.Save(1, "Robin", "robin.witteman@supershift.nl");
            mailTemplate.Publish(1, "Robin", "robin.witteman@supershift.nl");
        }

        [TestMethod]
        public async Task SendMailFromBlob()
        {
            try
            {
                //var id = "061fadf4-a488-441b-92d8-b25d4a8ad5c1"; // "d5a19359-ff36-41a0-a7ba-ed55e3d68e42"
                //var id = "d5a19359-ff36-41a0-a7ba-ed55e3d68e42"; 
                var id = "e2c466db-fb14-4d5a-b131-cf00a6193ffa";
                var emailStorageAccount = System.Configuration.ConfigurationManager.AppSettings["EmailStorageAccount"];
                var emailBlobContainer = System.Configuration.ConfigurationManager.AppSettings["EmailBlobContainer"];
                var emailQueueName = System.Configuration.ConfigurationManager.AppSettings["EmailQueueName"];
                var sendGridAPIKey = System.Configuration.ConfigurationManager.AppSettings["SendGridAPIKey"];
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
                var emailStorageAccount = System.Configuration.ConfigurationManager.AppSettings["EmailStorageAccount"];
                var emailBlobContainer = System.Configuration.ConfigurationManager.AppSettings["EmailBlobContainer"];
                var emailQueueName = System.Configuration.ConfigurationManager.AppSettings["EmailQueueName"];
                var sendGridAPIKey = System.Configuration.ConfigurationManager.AppSettings["SendGridAPIKey"];
                var mailer = new Mailer(emailStorageAccount, emailBlobContainer, emailQueueName, sendGridAPIKey);

                var template = MailTemplate.Fetch("TEMPLATEONE");

                template.PlaceholderList.Add("PLACEHOLDER1", "PLACEHOLDER1");
                template.PlaceholderList.Add("REPEATEDPLACEHOLDER", "REPEATEDPLACEHOLDER");
                template.PlaceholderGroupList.Add("REPEATER");
                template.PlaceholderGroupList.AddNewRow();
                template.PlaceholderGroupList.AddNewRowItem("REPEATEDPLACEHOLDER", "REPEATEDPLACEHOLDER 1");
                template.PlaceholderGroupList.AddNewRowItem("REPEATEDPLACEHOLDER", "REPEATEDPLACEHOLDER 2");
                template.OptionalSections.Add("OPTIONAL1");
                template.OptionalSections.Add("OPTIONAL2");

                template = MailTemplate.ApplyPlaceholders(template, Console.Out);

                var email = new Email
                {
                    Body = template.Body,
                    From = template.DefaultSenderEmail,
                    FromName = template.DefaultSenderName,
                    To = "robin.witteman@supershift.nl",
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
        public void GetTemplateAndApplyPlaceholders()
        {
            var template = MailTemplate.Fetch("Robin 6");

            if (template != null && template.ID > 0)
            {
                template.PlaceholderList.Add("NAME", "Test customer name");
                template.PlaceholderList.Add("BOOKINGNUMBER", "123abc");
                template.PlaceholderList.Add("DATESTART", "30 May 2018");
                template.PlaceholderList.Add("HOTEL", "Sunny Beach Hotel");
                template.PlaceholderList.Add("DAYS", "8");

                var mail = MailTemplate.ApplyPlaceholders(template, Console.Out);

                Console.WriteLine(mail);
            }
        }
        [TestMethod]
        public void GetTemplateAndApplyPlaceholdersWithGroups()
        {
            var template = MailTemplate.Fetch("TEMPLATE1");

            if (template != null && template.ID > 0)
            {
                /*[g:rooms]
                      <td>
                        [NAME] stays for [DAYS] days
                      </td>
                      [/g:rooms]*/
                template.PlaceholderGroupList.Add("rooms");
                template.PlaceholderGroupList.AddNewRow();
                template.PlaceholderGroupList.AddNewRowItem("NAME", "Cus Tomer");
                template.PlaceholderGroupList.AddNewRowItem("DAYS", "8");
                template.PlaceholderGroupList.AddNewRow();
                template.PlaceholderGroupList.AddNewRowItem("NAME", "Klan Tje");
                template.PlaceholderGroupList.AddNewRowItem("DAYS", "3");

                template.PlaceholderList.Add("NAME", "Test customer name");
                template.PlaceholderList.Add("BOOKINGNUMBER", "123abc");
                template.PlaceholderList.Add("DATESTART", "30 May 2018");
                template.PlaceholderList.Add("HOTEL", "Sunny Beach Hotel");
                template.PlaceholderList.Add("DAYS", "8");

                var mail = MailTemplate.ApplyPlaceholders(template, Console.Out);

                Console.WriteLine(mail.Body);
            }
        }
        [TestMethod]
        public void GetTemplateAndApplyPlaceholdersWithSections()
        {
            var template = MailTemplate.Fetch("TEMPLATE1");

            if (template != null && template.ID > 0)
            {
                /*
                  [section:OPTIONAL]
                  <tr>
                    <td bgcolor="ffffff">
                      <!-- COPY -->
                      <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td align="left" style="padding: 20px 40px 40px 40px; font-size: 16px; line-height: 20px; font-family: Arial, sans-serif; color: #000000;" class="padding">
                            <center>
                              <span style="width: 140px; font-family: 'Trebuchet MS', sans-serif; color: #000000; font-size: 16px; line-height: 14px; border-radius: 4px; display: block; text-decoration: none;">
                                Optional block
                              </span>
                              <br />
                              <a style="width: 340px; font-family: 'Trebuchet MS', sans-serif; color: #000000; font-size: 56px; line-height: 14px; padding: 15px 60px; border-radius: 4px; display: block; text-decoration: none;">
                                Roomnumber: [HOTEL]
                              </a>
                            </center>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  [/section:OPTIONAL]
                  */
                template.PlaceholderGroupList.Add("rooms");
                template.PlaceholderGroupList.AddNewRow();
                template.PlaceholderGroupList.AddNewRowItem("NAME", "Cus Tomer");
                template.PlaceholderGroupList.AddNewRowItem("DAYS", "8");
                template.PlaceholderGroupList.AddNewRow();
                template.PlaceholderGroupList.AddNewRowItem("NAME", "Klan Tje");
                template.PlaceholderGroupList.AddNewRowItem("DAYS", "3");

                template.PlaceholderList.Add("NAME", "Test customer name");
                template.PlaceholderList.Add("BOOKINGNUMBER", "123abc");
                template.PlaceholderList.Add("DATESTART", "30 May 2018");
                template.PlaceholderList.Add("HOTEL", "Sunny Beach Hotel");
                template.PlaceholderList.Add("DAYS", "8");

                template.OptionalSections.Add("EXPRESS");
                //template.OptionalSections.Add("SUNBEDS");

                var mail = MailTemplate.ApplyPlaceholders(template, Console.Out);

                Console.WriteLine(mail.Body);
            }
        }

        [TestMethod]
        public void GetSectionTags()
        {
            var mailTemplate = MailTemplate.Fetch("TEMPLATE1");
            var sections = Logic.PlaceholderLogic.GetSectionTags(mailTemplate.Body, logger: Console.Out);

        }

        [TestMethod]
        public void DeleteMailTemplate()
        {
            var template = MailTemplate.Fetch("TEMPLATE1");
            Data.MailTemplate.Delete(new List<int> { template.ID });
        }

        [TestMethod]
        public void MailTemplateListFetchAll()
        {
            var filterText = "template";
            var items = Data.MailTemplateList.FetchAll(filterText);
        }


        [TestMethod]
        public void FetchAllByIdentifiers()
        {
            var mailTemplates = Data.MailTemplate.FetchAllByIdentifiers(new string[] { "TEMPLATEONE", "TEMPLATETWO" });

            bool hasTwoTemplates = mailTemplates.Count == 2;
            bool containsFirst = mailTemplates.Any(x => x.Identifier == "TEMPLATEONE");
            bool containsSecond = mailTemplates.Any(x => x.Identifier == "TEMPLATETWO");

            Assert.IsTrue(hasTwoTemplates && containsFirst && containsSecond);
        }

        [TestMethod]
        public void GetTemplateAndApplyPlaceholdersWithMultipleGroups()
        {
            var template = MailTemplate.Fetch("ORDER_CUST_01B_NL");

            if (template != null && template.ID > 0)
            {
                template.PlaceholderList.Add("HOTEL", "Sushi test");
                template.PlaceholderList.Add("NAME", "Mr. Sushi");
                template.PlaceholderList.Add("BOOKINGNUMBER", "114D");
                template.PlaceholderList.Add("DATESTART", DateTime.UtcNow.ToString("dd-MM-yyyy"));
                template.PlaceholderList.Add("DAYS", "666");
                template.PlaceholderList.Add("ORDER_TOTAL_PRICE", "$50");
                template.PlaceholderList.Add("URL", string.Empty);

                // add sunbed section
                template.OptionalSections.Add("SUNBED");
                // add sunbed repeater
                template.PlaceholderGroupList.Add("SUNBEDS");
                
                // add new repeater row 
                template.PlaceholderGroupList.AddNewRow();
                template.PlaceholderGroupList.AddNewRowItem("SUNBED_ROW_COLOR", " background-color:#fef3f4 ");
                template.PlaceholderGroupList.AddNewRowItem("SUNBED_QUANTITY", "1");
                template.PlaceholderGroupList.AddNewRowItem("SUNBED_TYPE", "Sushi Sunbed ");
                template.PlaceholderGroupList.AddNewRowItem("SUNBED_CAPACITY", "1");
                template.PlaceholderGroupList.AddNewRowItem("SUNBED_POOLPLAN", "Pool one");
                template.PlaceholderGroupList.AddNewRowItem("SUNBED_NUMBER", "05");
                template.PlaceholderGroupList.AddNewRowItem("SUNBED_PRICE", "- € 8,00 ");

                // add new repeater row 
                template.PlaceholderGroupList.AddNewRow();
                template.PlaceholderGroupList.AddNewRowItem("SUNBED_ROW_COLOR", " background-color:#f2f9ec ");
                template.PlaceholderGroupList.AddNewRowItem("SUNBED_QUANTITY", "1");
                template.PlaceholderGroupList.AddNewRowItem("SUNBED_TYPE", "Sushi Sunbed ");
                template.PlaceholderGroupList.AddNewRowItem("SUNBED_CAPACITY", "1");
                template.PlaceholderGroupList.AddNewRowItem("SUNBED_POOLPLAN", "Pool one");
                template.PlaceholderGroupList.AddNewRowItem("SUNBED_NUMBER", "01");
                template.PlaceholderGroupList.AddNewRowItem("SUNBED_PRICE", "€ 8,00 ");

              
                // add sunbed section
                template.OptionalSections.Add("WEBSHOP");

                // add sunbed repeater
                template.PlaceholderGroupList.Add("WEBSHOPITEMS");

                template.PlaceholderGroupList.AddNewRow();
                template.PlaceholderGroupList.AddNewRowItem("WEBSHOP_ROW_COLOR", string.Empty);
                template.PlaceholderGroupList.AddNewRowItem("WEBSHOPITEM_QUANTITY", "1");
                template.PlaceholderGroupList.AddNewRowItem("WEBSHOPITEM_PRODUCTNAME", "Fles water 1,5 liter");
                template.PlaceholderGroupList.AddNewRowItem("WEBSHOPITEM_CATEGORY", "Producten");
                template.PlaceholderGroupList.AddNewRowItem("WEBSHOPITEM_PRICE", "Paid");

                template.PlaceholderGroupList.AddNewRow();
                template.PlaceholderGroupList.AddNewRowItem("WEBSHOP_ROW_COLOR", string.Empty);
                template.PlaceholderGroupList.AddNewRowItem("WEBSHOPITEM_QUANTITY", "1");
                template.PlaceholderGroupList.AddNewRowItem("WEBSHOPITEM_PRODUCTNAME", "Spa rood ");
                template.PlaceholderGroupList.AddNewRowItem("WEBSHOPITEM_CATEGORY", "Producten");
                template.PlaceholderGroupList.AddNewRowItem("WEBSHOPITEM_PRICE", "Paid");
              


                var mail = MailTemplate.ApplyPlaceholders(template, Console.Out);

                Console.WriteLine(mail.Body);
            }
        }
    }
}
