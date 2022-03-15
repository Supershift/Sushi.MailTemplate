using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sushi.MailTemplate.SendGrid
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSushiMailTemplateSendgrid(this IServiceCollection services, Action<SendGridMailerOptions> configureOptions)
        {
            services.Configure(configureOptions);

            services.AddTransient<Mailer>();
            services.AddTransient<Logic.ISendPreviewEmailEventHandler, Mailer>();
            
        }
    }
}
