using System;
using System.Threading.Tasks;

namespace Sushi.MailTemplate.Logic
{
    /// <summary>
    /// SendPreviewEmailEventHandler class, to handle preview e-mails. This needs implementation on the referencing project like 
    /// Sushi.MailTemplate.Logic.SendPreviewEmailEventHandler.SendPreviewEmail += OnSendPreviewEmail;
    /// </summary>
    public class SendPreviewEmailEventHandler
    {

        /// <summary>
        /// Async version of the SendPreviewEmailAsync method
        /// </summary>
        public static event Func<object, SendPreviewEmailEventArgs, Task> SendPreviewEmailAsync;

        /// <summary>
        /// Async invokation of the SendPreviewEmailAsync
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public virtual async Task OnSendPreviewEmailAsync(SendPreviewEmailEventArgs e)
        {
            var task = SendPreviewEmailAsync?.Invoke(this, e);

            if (task != null)
            {
                await task.ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// SendPreviewEmailEventArgs class
    /// </summary>
    public class SendPreviewEmailEventArgs : EventArgs
    {
        /// <summary>
        /// E-mail address to send from
        /// </summary>
        public string EmailFrom { get; set; }
        /// <summary>
        /// Name to send from
        /// </summary>
        public string EmailFromName { get; set; }
        /// <summary>
        /// E-mail address to send to
        /// </summary>
        public string EmailTo { get; set; }
        /// <summary>
        /// Subject of the e-mail
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Body of the e-mail
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Template name, as identifier
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// Shows if sending the preview was successful
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}
