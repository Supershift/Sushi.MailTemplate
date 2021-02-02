using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sushi.MailTemplate.Entities
{
    /// <summary>
    /// Placeholder row, used in placeholder groups
    /// </summary>
    public class PlaceholderRow
    {
        /// <summary>
        /// List of placeholders in this placeholder row
        /// </summary>
        public List<Placeholder> Placeholders { get; set; } = new List<Placeholder>();
    }
}
