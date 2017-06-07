using Text.Extract.Model;
using Text.Extract.Tag;
using Text.Search.Model;

namespace Text.Extract.Service
{
    public interface ITextExtractionService
    {
        /// <summary>
        /// Extracts the snippets.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="extractTags">The extract tags.</param>
        /// <returns></returns>
        Snippet[] ExtractSnippets(string document, string searchPhrase, IExtractTag[] extractTags);

        /// <summary>
        /// Extracts the snippet.
        /// </summary>
        /// <param name="searchResult">The search result.</param>
        /// <param name="extractTags">The extract tags.</param>
        /// <returns></returns>
        Snippet ExtractSnippet(SearchResult searchResult, IExtractTag[] extractTags);
    }
}