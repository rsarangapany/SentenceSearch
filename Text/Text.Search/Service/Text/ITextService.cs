using Text.NaturalLanguageProcessing.Model;
using Text.Search.Model;

namespace Text.Search.Service.Text
{
    public interface ITextService
    {
        /// <summary>
        /// Sentenceses the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        Sentence[] Sentences(string text);

        /// <summary>
        /// Wordses the specified sentences.
        /// </summary>
        /// <param name="sentences">The sentences.</param>
        /// <returns></returns>
        Word[] Words(Sentence[] sentences);

        /// <summary>
        /// Ranks the specified search words.
        /// </summary>
        /// <param name="searchWords">The search words.</param>
        /// <param name="allWords">All words.</param>
        /// <returns></returns>
        RankedSentence[] Rank(Word[] searchWords, Word[] allWords);

        /// <summary>
        /// Matches the specified search words.
        /// </summary>
        /// <param name="searchWords">The search words.</param>
        /// <param name="allWords">All words.</param>
        /// <returns></returns>
        MatchedWord[] Match(Word[] searchWords, Word[] allWords);
    }
}