using System.Linq;
using Text.NaturalLanguageProcessing.Model;
using Text.Search.Model;

namespace Text.Search.Service.Text.Rank.Specifications
{
    public class MatchedTermsRankSpecification : ITextRankSpecification
    {
        public decimal Score(IGrouping<int, MatchedWord> matches, Word[] searchWords, Word[] allWords)
        {
            var matchedTerms = matches.Select(c => c.Word.Stem).Distinct().ToArray();

            return matchedTerms.Count(c => searchWords.Any(d => d.Stem.Contains(c.ToLower())));
        }
    }
}
