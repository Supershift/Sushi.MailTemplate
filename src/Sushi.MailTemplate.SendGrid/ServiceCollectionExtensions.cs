using Microsoft.Extensions.Configuration;
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

            AddSushiMailTemplateSendgrid(services);
        }

        public static void AddSushiMailTemplateSendgrid(this IServiceCollection services, IConfiguration namedConfigurationSection)
        {
            services.Configure<SendGridMailerOptions>(namedConfigurationSection);

            AddSushiMailTemplateSendgrid(services);
        }

        private static void AddSushiMailTemplateSendgrid(IServiceCollection services)
        {
            services.AddTransient<Mailer>();
            services.AddTransient<Logic.ISendPreviewEmailEventHandler, Mailer>();
        }
    }
}
