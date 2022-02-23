using Sushi.MicroORM;
using Sushi.MicroORM.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sushi.MailTemplate.Data
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
        /// Saves the current default value
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveAsync()
        {
            var connector = new Connector<DefaultValuePlaceholder>();
            await connector.SaveAsync(this);
            return ID;
        }

        /// <summary>
        /// Deletes the current default value
        /// </summary>
        public async Task DeleteAsync()
        {
            var connector = new Connector<DefaultValuePlaceholder>();
            await connector.DeleteAsync(this);
        }

   
        /// <summary>
        /// Fetch all default values by mail template identifier
        /// </summary>
        /// <param name="mailTemplateIdentifier"></param>
        /// <returns></returns>
        public static async Task<List<DefaultValuePlaceholder>> FetchAllByMailTemplateAsync(string mailTemplateIdentifier)
        {
            var connector = new Connector<DefaultValuePlaceholder>();
            var filter = connector.CreateQuery();
            filter.AddSql(@"EXISTS (SELECT NULL FROM wim_MailTemplates where MailTemplate_Identifier = @mailTemplateIdentifier)");
            filter.AddParameter("@mailTemplateIdentifier", mailTemplateIdentifier);

            return await connector.FetchAllAsync(filter);
        }

    }
}