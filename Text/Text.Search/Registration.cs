using Microsoft.Extensions.DependencyInjection;
using Text.NaturalLanguageProcessing.Factory;
using Text.NaturalLanguageProcessing.Service.Text;
using Text.Search.Factory;
using Text.Search.Service.Document;
using Text.Search.Service.Text;
using Text.Search.Service.Text.Filter.SentenceFilter;
using Text.Search.Service.Text.Filter.WordFilter;
using Text.Search.Service.Text.Rank.Specifications;

namespace Text.Search
{
    public static class Registration
    {
        /// <summary>
        /// Registers the text search service. (Default life style scoped)
        /// </summary>
        /// <param name="services">The services.</param>
        public static void RegisterSearchService(this IServiceCollection services)
        {
            RegisterSearchService(services, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Registers the text search service.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="lifeTime">The life time.</param>
        public static void RegisterSearchService(this IServiceCollection services, ServiceLifetime lifeTime)
        {
            services.Add(new ServiceDescriptor(typeof(IWordFilter[]), (c) => new IWordFilter[]
            {
                new WordStopWordFilter()
            }, lifeTime));
           
            services.Add(new ServiceDescriptor(typeof(ISentenceFilter[]), (c) => new ISentenceFilter[]  
            {
                new SentenceLineBreakFilter(),
                new SentenceTabFilter()
            }, lifeTime));

            services.Add(new ServiceDescriptor(typeof(ITextRankSpecification[]), (c) => new ITextRankSpecification[]
            {
                new CosineSimilarityRankSpecification(),
                new LevenshteinDistanceRankSpecification(),
                new MatchedTermsRankSpecification()
            }, lifeTime));

            services.Add(new ServiceDescriptor(typeof(ITextService), (c) => new TextService(c.GetService<IWordFilter[]>(), 
                c.GetService<ISentenceFilter[]>(), 
                c.GetService<ITextRankSpecification[]>(), 
                c.GetService<INaturalLanguageProcessingFactory>()), lifeTime));

            services.Add(new ServiceDescriptor(typeof(IDocumentService), typeof(DocumentService), lifeTime));

            services.Add(new ServiceDescriptor(typeof(ITextSearchFactory), typeof(TextSearchFactory), lifeTime));
        }
    }
}