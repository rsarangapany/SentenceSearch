using Microsoft.Extensions.DependencyInjection;
using OpenNLP.Tools.SentenceDetect;
using OpenNLP.Tools.Tokenize;
using Text.NaturalLanguageProcessing.Factory;
using Text.NaturalLanguageProcessing.Service.Text;

namespace Text.NaturalLanguageProcessing
{
    public static class Registration
    {
        public static void RegisterNaturalLanguageProcessingService(this IServiceCollection services, string resourcesDirectoryLocation)
        {
            RegisterNaturalLanguageProcessingService(services, resourcesDirectoryLocation, ServiceLifetime.Scoped);
        }

        public static void RegisterNaturalLanguageProcessingService(this IServiceCollection services, string resourcesDirectoryLocation, ServiceLifetime lifeTime)
        {
            services.Add(new ServiceDescriptor(typeof(ITokenizer), (c) => new EnglishMaximumEntropyTokenizer($"{resourcesDirectoryLocation}\\EnglishSD.nbin"), lifeTime));

            services.Add(new ServiceDescriptor(typeof(ISentenceDetector), (c) => new EnglishMaximumEntropySentenceDetector($"{resourcesDirectoryLocation}\\EnglishSD.nbin"), lifeTime));

            services.Add(new ServiceDescriptor(typeof(IStemmerService), typeof(EnglishStemmerService), lifeTime));

            services.Add(new ServiceDescriptor(typeof(INatrualLanguageProcessingService), typeof(NatrualLanguageProcessingService), lifeTime));

            services.Add(new ServiceDescriptor(typeof(INaturalLanguageProcessingFactory), typeof(NaturalLanguageProcessingFactory), lifeTime));
        }
    }
}
