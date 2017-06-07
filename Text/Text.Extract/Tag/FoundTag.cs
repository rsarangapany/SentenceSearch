using System.Linq;
using Text.Search.Model;

namespace Text.Extract.Tag
{
    public class FoundTag : IExtractTag
    {
        public string Format { get; set; }

        public FoundTag(string format)
        {
            Format = format;
        }

        public string Tag(string sourceText, SearchResult searchResult)
        {
            return searchResult.MatchedWords.Select(c => c.Text).Distinct().Aggregate(sourceText, (current, word) => current.Replace(word, $"<{Format}>{word}</{Format}>"));
        }
    }
}
