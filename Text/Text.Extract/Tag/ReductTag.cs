using System.Linq;
using Text.NaturalLanguageProcessing.Model;
using Text.Search.Model;
using Text.Search.Service.Text;

namespace Text.Extract.Tag
{
    public class AnonymiseTag : IExtractTag
    {
        private readonly Word[] _anonymisedWords;
        private readonly string _repleacement;

        public string Format { get; }

        public AnonymiseTag(string format, string replacement,  string[] anonymisedWords, ITextService textService)
        {
            Format = format;
            _anonymisedWords = textService.Words(textService.Sentences(string.Join(" ", anonymisedWords))); ;
            _repleacement = replacement;
        }

        public string Tag(string sourceText, SearchResult searchResult)
        {
            return searchResult.IndexedWords.Where(c => _anonymisedWords.Any(d => c.Stem.Contains(d.Stem))).Select(c => c.Text).Distinct().Aggregate(sourceText, (current, word) => current.Replace(word, $"<{Format}>{_repleacement}</{Format}>"));
        }
    }
}
