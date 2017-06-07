using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Text.Extract.Model;
using Text.Extract.Tag;
using Text.Search.Factory;
using Text.Search.Model;
using Text.Search.Service.Document;

namespace Text.Extract.Service
{
    internal class TextExtractionService : ITextExtractionService
    {  
        private readonly IDocumentService _documentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextExtractionService"/> class.
        /// </summary>
        /// <param name="textSearchFactory">The text search factory.</param>
        public TextExtractionService(ITextSearchFactory textSearchFactory)
        {
            _documentService = textSearchFactory.DocumentService;
        }

        /// <summary>
        /// Extracts the snippets.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="extractTags">The extract tags.</param>
        /// <returns></returns>
        public Snippet[] ExtractSnippets(string document, string searchPhrase, IExtractTag[] extractTags)
        {
            var searchResults = _documentService.Search(document, searchPhrase);

            if (searchResults == null)
                return null;

            var snippets = new ConcurrentBag<Snippet>();

            Parallel.ForEach(searchResults, searchResult =>
            {
                snippets.Add(ExtractSnippet(searchResult, extractTags));
            });

            return snippets.OrderByDescending(c => c.Rank).ToArray();
        }

        /// <summary>
        /// Extracts the snippet.
        /// </summary>
        /// <param name="searchResult">The search result.</param>
        /// <param name="extractTags">The extract tags.</param>
        /// <returns></returns>
        public Snippet ExtractSnippet(SearchResult searchResult, IExtractTag[] extractTags)
        {
            var snippet = new Snippet
            {
                Text = searchResult.Text,
                Rank = searchResult.Rank
            };

            foreach (var extractTag in extractTags)
            {
                snippet.Text = extractTag.Tag(snippet.Text, searchResult);
            }

            snippet.Text = snippet.Text.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) + snippet.Text.Substring(1);

            return snippet;
        }
    }
}
