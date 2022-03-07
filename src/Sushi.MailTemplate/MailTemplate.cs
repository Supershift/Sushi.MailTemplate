using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sushi.MailTemplate
{
    /// <summary>
    /// Use this class as helper to get mail templates and to apply placeholders
    /// </summary>
    public static class MailTemplate
    {

        /// <summary>
        /// Gets a published mail template by its identifier
        /// </summary>
        /// <param name="identifier">The Mailtemplate identifier</param>
        /// <returns></returns>
        public static async Task<Data.MailTemplate> FetchAsync(string identifier)
        {
            return await Data.MailTemplate.FetchSingleByIdentifierAsync(identifier, true);
        }

        /// <summary>
        /// Gets a mail template by its identifier
        /// </summary>
        /// <param name="identifier">The Mailtemplate identifier</param>
        /// <param name="onlyPublished">Should we only return published mailtemplates</param>
        /// <returns></returns>
        public static async Task<Data.MailTemplate> FetchAsync(string identifier, bool onlyPublished)
        {
            return await Data.MailTemplate.FetchSingleByIdentifierAsync(identifier, onlyPublished);
        }

        /// <summary>
        /// Applies the placeholders to the body and the subject properties
        /// </summary>
        /// <param name="mailTemplate"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static async Task<Data.MailTemplate> ApplyPlaceholdersAsync(Data.MailTemplate mailTemplate, System.IO.TextWriter logger = null)
        {
            var placeholderGroupReplacements = mailTemplate.PlaceholderGroupList.PlaceholderGroupDictionary.Values.ToList();
            var placeholderReplacements = mailTemplate.PlaceholderList.PlaceholderDictionary.Values.ToList();
            var optionalSectionsReplacements = mailTemplate.OptionalSections;
            return await Logic.PlaceholderLogic.ApplyPlaceholdersAsync(mailTemplate, placeholderGroupReplacements, placeholderReplacements, optionalSectionsReplacements, logger);
        }
    }
}
