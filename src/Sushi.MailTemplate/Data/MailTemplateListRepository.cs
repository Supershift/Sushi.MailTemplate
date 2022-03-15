using Sushi.MicroORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sushi.MailTemplate.Data
{
    public class MailTemplateListRepository
    {
        private readonly Connector<MailTemplateList> _connector;

        public MailTemplateListRepository(Connector<MailTemplateList> connector)
        {
            _connector = connector;
        }
        
        /// <summary>
        /// Fetch all mail templates
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<List<MailTemplateList>> FetchAllAsync(string name = "")
        {   
            var filter = _connector.CreateQuery();

            filter.AddOrder(x => x.Name);

            if (!string.IsNullOrEmpty(name))
            {
                string searchLike = $"%{name}%";
                filter.AddSql($"(MailTemplate_Name LIKE @searchLike OR MailTemplate_Identifier LIKE @searchLike)");
                filter.AddParameter("@searchLike", System.Data.SqlDbType.NVarChar, searchLike);
            }

            var result = await _connector.FetchAllAsync(filter);

            return result;
        }

        /// <summary>
        /// Fetch one mail template by identifier
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public async new Task<MailTemplateList> FetchSingleByIdentifierAsync(string identifier)
        {   
            var filter = _connector.CreateQuery();
            filter.Add(x => x.Identifier, identifier, ComparisonOperator.Like);
            var result = await _connector.FetchSingleAsync(filter);
            return result;
        }

        /// <summary>
        /// Fetch one mail template by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async new Task<MailTemplateList> FetchSingleAsync(int id)
        {   
            var result = await _connector.FetchSingleAsync(id);
            return result;
        }
    }
}
