using Sushi.MicroORM;
using Sushi.MicroORM.Mapping;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wim.Module.MailTemplate.Data
{
    /// <summary>
    /// DefaultValuePlaceholder data entity
    /// </summary>
    [DataMap(typeof(DefaultValuePlaceholderMap))]
    public class DefaultValuePlaceholder
    {
        /// <summary>
        /// Sushi.MicroORM Mapper class
        /// </summary>
        public class DefaultValuePlaceholderMap : DataMap<DefaultValuePlaceholder>
        {
            /// <summary>
            /// Sushi.MicroORM Mapper ctor
            /// </summary>
            public DefaultValuePlaceholderMap()
            {
                Table("wim_MailTemplateDefaultValuePlaceholders");
                Id(x => x.ID, "MailTemplateDefaultValuePlaceholder_Key");
                Map(x => x.MailTemplateID, "MailTemplateDefaultValuePlaceholder_MailTemplate_Key"); // DBType System.Int
                Map(x => x.Placeholder, "MailTemplateDefaultValuePlaceholder_Placeholder"); // DBType System.String
                Map(x => x.Value, "MailTemplateDefaultValuePlaceholder_Value"); // DBType System.String

            }
        }
        /// <summary>
        /// ID of the current default value
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// GUID  of the current default value
        /// </summary>
        public int MailTemplateID { get; set; }
        /// <summary>
        /// Placeholder of the current default value
        /// </summary>
        public string Placeholder { get; set; }
        /// <summary>
        /// Value of the current default value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Fetch all default values
        /// </summary>
        /// <returns></returns>
        public static List<DefaultValuePlaceholder> FetchAll()
        {
            var connector = new Connector<DefaultValuePlaceholder>();
            var filter = connector.CreateDataFilter();
            var result = connector.FetchAll(filter);
            return result;
        }
        /// <summary>
        /// Fetch one default value by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DefaultValuePlaceholder FetchSingle(int id)
        {
            var connector = new Connector<DefaultValuePlaceholder>();
            var result = connector.FetchSingle(id);
            return result;
        }
        /// <summary>
        /// Saves the current default value
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            var connector = new Connector<DefaultValuePlaceholder>();
            connector.Save(this);
            return this.ID;
        }
        /// <summary>
        /// Deletes the current default value
        /// </summary>
        public void Delete()
        {
            var connector = new Connector<DefaultValuePlaceholder>();
            connector.Delete(this);
        }
        /// <summary>
        /// Fetch all default values by mail template GUID
        /// </summary>
        /// <param name="mailTemplateID"></param>
        /// <returns></returns>
        public static List<DefaultValuePlaceholder> FetchAllByMailTemplate(int mailTemplateID)
        {
            var connector = new Connector<DefaultValuePlaceholder>();
            var filter = connector.CreateDataFilter();
            filter.Add(x => x.MailTemplateID, mailTemplateID);

            return connector.FetchAll(filter);
        }
        /// <summary>
        /// Fetch all default values by mail template GUID
        /// </summary>
        /// <param name="mailTemplateID"></param>
        /// <returns></returns>
        public static async Task<List<DefaultValuePlaceholder>> FetchAllByMailTemplateAsync(int mailTemplateID)
        {
            var connector = new Connector<DefaultValuePlaceholder>();
            var filter = connector.CreateDataFilter();
            filter.Add(x => x.MailTemplateID, mailTemplateID);

            return await connector.FetchAllAsync(filter);
        }

    }
}