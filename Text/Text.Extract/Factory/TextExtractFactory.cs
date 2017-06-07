using Text.Extract.Service;

namespace Text.Extract.Factory
{
    internal class TextExtractFactory: ITextExtractFactory
    {
        /// <summary>
        /// Gets the text extract.
        /// </summary>
        /// <value>
        /// The text extract.
        /// </value>
        public ITextExtractionService TextExtract { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextExtractFactory"/> class.
        /// </summary>
        /// <param name="textExtractionService">The text extraction service.</param>
        public TextExtractFactory(ITextExtractionService textExtractionService)
        {
            TextExtract = textExtractionService;
        }
    }
}
