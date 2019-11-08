using Microsoft.Extensions.DependencyInjection;
using Westwind.AspNetCore.Markdown;

namespace HelpDown
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddHelpDown(this IServiceCollection services)
        {
            services.AddMarkdown();
            
            return services;
        }
    }
}