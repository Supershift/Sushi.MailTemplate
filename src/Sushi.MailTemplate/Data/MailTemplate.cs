using Sushi.MicroORM;
using Sushi.MicroORM.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sushi.MailTemplate.Data
{
    /// <summary>
    /// MailTemplate data entity
    /// </summary>
    [DataMap(typeof(MailTemplateMap))]
    public class MailTemplate
    {
        /// <summary>
        /// Sushi.MicroORM Mapper class
        /// </summary>
        public class MailTemplateMap : DataMap<MailTemplate>
        {
            /// <summary>
            /// Sushi.MicroORM Mapper ctor
            /// </summary>
            public MailTemplateMap()
            {
                Table("wim_MailTemplates");
                Id(x => x.ID, "MailTemplate_Key");
                Map(x => x.Name, "MailTemplate_Name").Length(50); // DBType System.String
                Map(x => x.Description, "MailTemplate_Description").Length(255); // DBType System.String
                Map(x => x.Identifier, "MailTemplate_Identifier").Length(50); // DBType System.String
                Map(x => x.DefaultSenderEmail, "MailTemplate_DefaultSenderEmail").Length(255); // DBType System.String
                Map(x => x.DefaultSenderName, "MailTemplate_DefaultSenderName").Length(50); // DBType System.String
                Map(x => x.BCCReceivers, "MailTemplate_BCCReceivers"); // DBType System.String
                Map(x => x.Subject, "MailTemplate_Subject").Length(512); // DBType System.String
                Map(x => x.Body, "MailTemplate_Body"); // DBType System.String
                Map(x => x.DateCreated, "MailTemplate_DateCreated"); // DBType System.DateTime
                Map(x => x.DateLastUpdated, "MailTemplate_DateLastUpdated"); // DBType System.DateTime
                Map(x => x.IsArchived, "MailTemplate_IsArchived"); // DBType System.Boolean
                Map(x => x.IsPublished, "MailTemplate_IsPublished"); // DBType System.Boolean
                Map(x => x.VersionMajor, "MailTemplate_VersionMajor"); // DBType System.Int32
                Map(x => x.VersionMinor, "MailTemplate_VersionMinor"); // DBType System.Int32
                Map(x => x.UserID, "MailTemplate_UserID"); // DBType System.Int32
                Map(x => x.UserName, "MailTemplate_UserName").Length(512); // DBType System.String
                Map(x => x.GUID, "MailTemplate_GUID"); // DBType System.Guid

            }
        }

        /// <summary>
        /// The ID of the current mail template
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The name of the current mail template, which does not have to unique.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The description of the current mail template.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The identifier of the current mail template, which is unique.
        /// </summary>
        public string Identifier { get; set; }
        /// <summary>
        /// The e-mail address of the sender of the e-mail
        /// </summary>
        public string DefaultSenderEmail { get; set; }
        /// <summary>
        /// The name of the sender of the e-mail
        /// </summary>
        public string DefaultSenderName { get; set; }
        /// <summary>
        /// The bcc receivers, as ";"-separated list, like info@supershift.com;support@supershift.nl
        /// </summary>
        public string BCCReceivers { get; set; }
        /// <summary>
        /// The e-mail subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// The e-mail body
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// The date of creation of the current mail template.
        /// </summary>
        public DateTime? DateCreated { get; set; }
        /// <summary>
        /// The date of the last modification of the current mail template.
        /// </summary>
        public DateTime? DateLastUpdated { get; set; }
        /// <summary>
        /// Returns if the current mail template is archived.
        /// </summary>
        public bool? IsArchived { get; set; }
        /// <summary>
        /// Returns if the current mail template is published.
        /// </summary>
        public bool? IsPublished { get; set; }
        /// <summary>
        /// The major version like 2.x
        /// </summary>
        public int VersionMajor { get; set; }
        /// <summary>
        /// The minor verion like x.1
        /// </summary>
        public int VersionMinor { get; set; }
        /// <summary>
        /// The UserID of the creator
        /// </summary>
        public int? UserID { get; set; }
        /// <summary>
        /// The Username of the creator
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// The GUID of the current mail template
        /// </summary>
        public Guid GUID { get; set; }
        /// <summary>
        /// HasPublishedVersion property is true if the mail template has at least one major version
        /// </summary>
        public bool HasPublishedVersion
        {
            get
            {
                return VersionMajor > 0;
            }
        }
        /// <summary>
        /// Fetch all mail templates
        /// </summary>
        /// <returns></returns>
        public static List<MailTemplate> FetchAll()
        {
            var connector = new Connector<MailTemplate>();
            var filter = connector.CreateDataFilter();
            var result = connector.FetchAll(filter);
            return result;
        }
        /// <summary>
        /// Fetch a single mail template by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MailTemplate FetchSingle(int id)
        {
            var connector = new Connector<MailTemplate>();
            var result = connector.FetchSingle(id);
            return result;
        }

        /// <summary>
        /// Retrieves all MailTemplates that matches the specified identifiers
        /// </summary>
        /// <param name="identifiers">Collection of identifiers</param>
        /// <param name="onlyPublished"></param>
        /// <returns></returns>
        public static List<MailTemplate> FetchAllByIdentifiers(IEnumerable<string> identifiers, bool onlyPublished = true)
        {
            var connector = new Connector<MailTemplate>();
            var filter = connector.CreateDataFilter();
            filter.Add(x => x.Identifier, identifiers, ComparisonOperator.In);

            if (onlyPublished)
                filter.Add(x => x.IsPublished, onlyPublished);

            var result = connector.FetchAll(filter);
            return result;
        }

        internal static MailTemplate FetchSingle(string identifier, int versionMajor)
        {
            var connector = new Connector<MailTemplate>();
            var filter = connector.CreateDataFilter();
            filter.Add(x => x.VersionMajor, versionMajor);
            filter.Add(x => x.Identifier, identifier);
            var result = connector.FetchSingle(filter);
            return result;
        }
        internal static MailTemplate FetchSingleByIdentifier(string identifier)
        {
            var connector = new Connector<MailTemplate>();
            var filter = connector.CreateDataFilter();
            filter.Add(x => x.Identifier, identifier);
            filter.Add(x => x.IsPublished, true);
            filter.Add(x => x.VersionMinor, 0);
            filter.AddOrder(x => x.VersionMajor, SortOrder.DESC);
            var result = connector.FetchSingle(filter);
            return result;
        }
        internal static async Task<MailTemplate> FetchSingleByIdentifierAsync(string identifier)
        {
            var connector = new Connector<MailTemplate>();
            var filter = connector.CreateDataFilter();
            filter.Add(x => x.Identifier, identifier);
            filter.Add(x => x.IsPublished, true);
            filter.Add(x => x.VersionMinor, 0);
            filter.AddOrder(x => x.VersionMajor, SortOrder.DESC);
            var result = await connector.FetchSingleAsync(filter);
            return result;
        }
        internal static List<MailTemplate> FetchAllByIdentifier(string identifier)
        {
            var connector = new Connector<MailTemplate>();
            var filter = connector.CreateDataFilter();
            filter.Add(x => x.Identifier, identifier);
            var result = connector.FetchAll(filter);
            return result;
        }
        /// <summary>
        /// Saves the current mail template to the database with checks on the validity of placeholders in the body and subject.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userDisplayname"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public int Save(int userID, string userDisplayname, string userEmail)
        {
            if (Logic.Helper.IsValidTemplate(Body, Subject))
            {
                Body = Logic.Helper.ReplaceLegacyUnsubscribe(Body);
                Subject = Logic.Helper.ReplaceLegacyUnsubscribe(Subject);

                var connector = new Connector<MailTemplate>();
                var currentTemplateInDatabase = MailTemplateList.FetchSingle(ID);
                var result = MailTemplateList.FetchSingleByIdentifier(Identifier);

                if (result != null && result.ID != ID)
                {
                    return 0;
                }

                if (currentTemplateInDatabase != null && currentTemplateInDatabase.Identifier != Identifier)
                {
                    // user changed the identifier, special case. All versions need to be updated
                    var mailTemplatesToUpdate = MailTemplate.FetchAllByIdentifier(currentTemplateInDatabase.Identifier);

                    foreach (var mailTemplate in mailTemplatesToUpdate)
                    {
                        mailTemplate.Identifier = Identifier;
                        connector.Save(mailTemplate);
                    }
                }

                if (ID > 0)
                {
                    // existing template, save as new minor version
                    VersionMinor++;
                    ID = 0;
                    UserID = userID;
                    UserName = $"{userDisplayname} ({userEmail})";
                    DateLastUpdated = DateTime.UtcNow;
                    // saving template, not publishing, new minor, so not published
                    IsPublished = false;
                }
                else
                {
                    // new, not archived, first minor version
                    IsPublished = false;
                    IsArchived = false;
                    VersionMajor = 0;
                    VersionMinor = 1;
                    DateLastUpdated = DateTime.UtcNow;
                    DateCreated = DateTime.UtcNow;
                    GUID = Guid.NewGuid();
                }

                connector.Save(this);

                return this.ID;
            }

            return 0;
        }

        /// <summary>
        /// Revert the current mail template to the previous major version.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userDisplayname"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public bool Revert(int userID, string userDisplayname, string userEmail)
        {
            var connector = new Connector<MailTemplate>();
            var result = MailTemplate.FetchSingle(Identifier, VersionMajor);

            if (result != null && result.ID != ID)
            {
                // unpublish current version
                result.IsPublished = false;
                connector.Save(result);

                // create new version from old published template and publish directly
                // this way the edit is saved as well
                result.ID = 0;
                result.IsPublished = true;
                result.VersionMajor++;
                result.UserID = userID;
                result.UserName = $"{userDisplayname} ({userEmail})";
                connector.Save(result);
            }

            return true;
        }

        /// <summary>
        /// Publishes the current mail template by making it a major version.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userDisplayname"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public bool Publish(int userID, string userDisplayname, string userEmail)
        {
            if (Logic.Helper.IsValidTemplate(Body, Subject))
            {
                var connector = new Connector<MailTemplate>();

                // unpublish all previous
                var filter = connector.CreateDataFilter();
                filter.AddParameter("@identifier", System.Data.SqlDbType.NVarChar, Identifier);
                var query = $@"UPDATE wim_MailTemplates SET MailTemplate_IsPublished = 0 WHERE MailTemplate_Identifier = @identifier";

                connector.ExecuteNonQuery(query, filter);

                // create new version
                VersionMajor++;
                VersionMinor = 0;
                IsPublished = true;
                IsArchived = false;
                UserID = userID;
                UserName = $"{userDisplayname} ({userEmail})";
                ID = 0;
                DateLastUpdated = DateTime.UtcNow;
                DateCreated = DateTime.UtcNow;
                connector.Save(this);

                return true;
            }

            return false;
        }
        /// <summary>
        /// Deletes mail templates that are identified by their id.
        /// </summary>
        /// <param name="mailTemplateIDs"></param>
        /// <returns></returns>
        public static bool Delete(List<int> mailTemplateIDs)
        {
            if (mailTemplateIDs != null && mailTemplateIDs.Count > 0)
            {
                var joinedMailTemplateIDs = string.Join(",", mailTemplateIDs);

                var connector = new Connector<MailTemplate>();
                var query = $@"UPDATE wim_MailTemplates SET MailTemplate_IsArchived = 1 WHERE MailTemplate_Key IN ({joinedMailTemplateIDs})";

                connector.ExecuteNonQuery(query);

                return true;
            }
            return false;
        }
        /// <summary>
        /// Add placeholders to this list, like template.PlaceholderList.Add("HOTEL", "Sunny Beach Hotel");
        /// </summary>
        public PlaceholderList PlaceholderList { get; set; } = new PlaceholderList();
        /// <summary>
        /// Add placeholder groups to this list, like template.PlaceholderGroupList.Add("rooms");
        /// template.PlaceholderGroupList.AddNewRow();
        /// template.PlaceholderGroupList.AddNewRowItem("NAME", "Cus Tomer");
        /// </summary>
        public PlaceholderGroupList PlaceholderGroupList { get; set; } = new PlaceholderGroupList();

        /// <summary>
        /// Optional sections are useful for instance to create invitation mail templates with optional blocks for rooms, sunbeds and express checkins. 
        /// Leaving a section out has as result that the section is removed from the e-mail.
        /// </summary>
        public List<string> OptionalSections { get; set; } = new List<string>();
    }
    /// <summary>
    /// PlaceholderGroupList class, with methods to add items
    /// </summary>
    public class PlaceholderGroupList
    {
        internal Dictionary<string, Entities.PlaceholderGroup> PlaceholderGroupDictionary { get; set; } = new Dictionary<string, Entities.PlaceholderGroup>();
        /// <summary>
        /// Add a group to the placeholder groups, like template.PlaceholderGroupList.Add("rooms");
        /// </summary>
        /// <param name="groupName"></param>
        public void Add(string groupName)
        {
            PlaceholderGroupDictionary.Add(groupName, new Entities.PlaceholderGroup(groupName));
        }

        /// <summary>
        /// Add a new row to the placeholder group, so it can hold items, like template.PlaceholderGroupList.AddNewRow();
        /// </summary>
        public void AddNewRow()
        {
            PlaceholderGroupDictionary.Last().Value.AddNewRow();
        }
        /// <summary>
        /// Add a new item to the row of a placeholder group, like template.PlaceholderGroupList.AddNewRowItem("NAME", "Cus Tomer");
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddNewRowItem(string name, string value)
        {
            PlaceholderGroupDictionary.Last().Value.AddNewRowItem(name, value);
        }
    }
    /// <summary>
    /// PlaceholderList class
    /// </summary>
    public class PlaceholderList
    {
        internal Dictionary<string, Entities.Placeholder> PlaceholderDictionary { get; set; } = new Dictionary<string, Entities.Placeholder>();
        /// <summary>
        /// Add name-value pairs to the placeholder list, like template.PlaceholderList.Add("NAME", "Cus Tomer");
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Add(string name, string value)
        {
            PlaceholderDictionary.Add(name, new Entities.Placeholder(name, value));
        }
    }
}
