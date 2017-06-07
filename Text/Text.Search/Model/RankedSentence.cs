using Text.NaturalLanguageProcessing.Model;

namespace Text.Search.Model
{
    public class RankedSentence
    {
        public int SentenceIndex { get; set; }
        public decimal Rank { get; set; }
        public Word[] SearchedWords { get; set; }
        public Word[] MatchedWords { get; set; }
        public Word[] IndexedWords { get; set; }
    }
}
