using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wim.Module.MailTemplate
{
    /// <summary>
    /// Use this class as helper to get mail templates and to apply placeholders
    /// </summary>
    public static class MailTemplate
    {
        /// <summary>
        /// Gets a mail template by identifier
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static Data.MailTemplate Fetch(string identifier)
        {
            return Data.MailTemplate.FetchSingleByIdentifier(identifier);
        }
        /// <summary>
        /// Gets a mail template by identifier
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static async Task<Data.MailTemplate> FetchAsync(string identifier)
        {
            return await Data.MailTemplate.FetchSingleByIdentifierAsync(identifier);
        }
        /// <summary>
        /// Applies the placeholders to the body and the subject properties
        /// </summary>
        /// <param name="mailTemplate"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static Data.MailTemplate ApplyPlaceholders(Data.MailTemplate mailTemplate, System.IO.TextWriter logger = null)
        {
            var placeholderGroupReplacements = mailTemplate.PlaceholderGroupList.PlaceholderGroupDictionary.Values.ToList();
            var placeholderReplacements = mailTemplate.PlaceholderList.PlaceholderDictionary.Values.ToList();
            var optionalSectionsReplacements = mailTemplate.OptionalSections;
            return Logic.PlaceholderLogic.ApplyPlaceholders(mailTemplate, placeholderGroupReplacements, placeholderReplacements, optionalSectionsReplacements, logger);
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
