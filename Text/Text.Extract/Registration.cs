using Microsoft.Extensions.DependencyInjection;
using Text.Extract.Factory;
using Text.Extract.Service;

namespace Text.Extract
{
    public static class Registration
    {
        /// <summary>
        /// Registers the text extract service. (Default scoped life style)
        /// </summary>
        /// <param name="services">The services.</param>
        public static void RegisterExtractService(this IServiceCollection services)
        {
            RegisterExtractService(services, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Registers the text extract service.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="lifetime">The lifetime.</param>
        public static void RegisterExtractService(this IServiceCollection services, ServiceLifetime lifetime)
        {
           services.Add(new ServiceDescriptor(typeof(ITextExtractionService), typeof(TextExtractionService), lifetime));

           services.Add(new ServiceDescriptor(typeof(ITextExtractFactory), typeof(TextExtractFactory), lifetime));
        }
    }
}