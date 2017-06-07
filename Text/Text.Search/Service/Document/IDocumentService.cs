using Text.Search.Model;

namespace Text.Search.Service.Document
{
    public interface IDocumentService
    {
        /// <summary>
        /// Searches the specified document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <returns></returns>
        SearchResult[] Search(string document, string searchPhrase);
    }
}