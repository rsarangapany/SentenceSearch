using Text.Extract.Service;

namespace Text.Extract.Factory
{
    public interface ITextExtractFactory
    {
        /// <summary>
        /// Gets the text extract.
        /// </summary>
        /// <value>
        /// The text extract.
        /// </value>
        ITextExtractionService TextExtract { get; }
    }
}