using Text.NaturalLanguageProcessing.Model;

namespace Text.NaturalLanguageProcessing.Service.Text
{
    public interface INatrualLanguageProcessingService
    {
        /// <summary>
        /// Sentences the detect.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        Sentence[] SentenceDetect(string text);

        /// <summary>
        /// Tokenizes the specified sentences.
        /// </summary>
        /// <param name="sentences">The sentences.</param>
        /// <returns></returns>
        Word[] Tokenize(Sentence[] sentences);
    }
}