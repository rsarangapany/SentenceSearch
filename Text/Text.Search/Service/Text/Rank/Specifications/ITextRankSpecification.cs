using System.Linq;
using Text.NaturalLanguageProcessing.Model;
using Text.Search.Model;

namespace Text.Search.Service.Text.Rank.Specifications
{
    public interface ITextRankSpecification
    {
        decimal Score(IGrouping<int, MatchedWord> matches, Word[] searchWords, Word[] allWords);
    }
}
