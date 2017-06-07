using Text.Search.Service.Document;
using Text.Search.Service.Text;

namespace Text.Search.Factory
{
    internal class TextSearchFactory : ITextSearchFactory
    {
        public ITextService TextService { get; }
        public IDocumentService DocumentService { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextSearchFactory"/> class.
        /// </summary>
        /// <param name="textService">The text service.</param>
        /// <param name="documentService">The document service.</param>
        public TextSearchFactory(ITextService textService, IDocumentService documentService)
        {
            TextService = textService;
            DocumentService = documentService;
        }
    }
}
