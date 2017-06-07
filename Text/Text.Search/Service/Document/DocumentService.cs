using System.Linq;
using Text.Search.Model;
using Text.Search.Service.Text;

namespace Text.Search.Service.Document
{
    internal class DocumentService : IDocumentService
    {
        private readonly ITextService _textProcessing;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentService"/> class.
        /// </summary>
        /// <param name="textService">The text service.</param>
        public DocumentService(ITextService textService)
        {
            _textProcessing = textService;
        }

        /// <summary>
        /// Searches the specified document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <returns></returns>
        public SearchResult[] Search(string document, string searchPhrase)
        {
            var sentences = _textProcessing.Sentences(document);
            var words = _textProcessing.Words(sentences);

            if (searchPhrase == null)
                return null;

            var searchWords = _textProcessing.Words(_textProcessing.Sentences(searchPhrase));

            if (searchWords == null)
                return null;

            return _textProcessing.Rank(searchWords, words).Select(c => new SearchResult
            {
                Index = c.SentenceIndex,
                Text = sentences[c.SentenceIndex].Text,
                Rank = c.Rank,
                SearchedWords = c.SearchedWords,
                IndexedWords = c.IndexedWords,
                MatchedWords = c.MatchedWords
            }).OrderByDescending(c => c.Rank).ToArray();
        }

    }
}
