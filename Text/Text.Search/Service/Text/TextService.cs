using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Text.NaturalLanguageProcessing.Factory;
using Text.NaturalLanguageProcessing.Model;
using Text.NaturalLanguageProcessing.Service.Text;
using Text.Search.Model;
using Text.Search.Service.Text.Filter.SentenceFilter;
using Text.Search.Service.Text.Filter.WordFilter;
using Text.Search.Service.Text.Rank.Specifications;

namespace Text.Search.Service.Text
{
    internal class TextService : ITextService
    {
        private readonly IWordFilter[] _wordFilters;
        private readonly ISentenceFilter[] _sentenceFilters;
        private readonly ITextRankSpecification[] _rankSpecifications;
        private readonly INatrualLanguageProcessingService _natrualLanguageProcessingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextService"/> class.
        /// </summary>
        /// <param name="wordFilters">The word filters.</param>
        /// <param name="sentenceFilters">The sentence filters.</param>
        /// <param name="rankSpecifications">The rank specifications.</param>
        /// <param name="natrualLanguageProcessingFactory">The natrual language processing factory.</param>
        public TextService(IWordFilter[] wordFilters, ISentenceFilter[] sentenceFilters, ITextRankSpecification[] rankSpecifications, INaturalLanguageProcessingFactory natrualLanguageProcessingFactory)
        {
            _wordFilters = wordFilters;
            _sentenceFilters = sentenceFilters;
            _rankSpecifications = rankSpecifications;
            _natrualLanguageProcessingService = natrualLanguageProcessingFactory.NatrualLanguageProcessingService;
        }

        /// <summary>
        /// Sentenceses the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public Sentence[] Sentences(string text)
        {
            var sentences = _natrualLanguageProcessingService.SentenceDetect(text);

            return _sentenceFilters?.Aggregate(sentences.ToArray(), (current, sentenceFilter) => sentenceFilter.Filter(current));
        }

        /// <summary>
        /// Wordses the specified sentences.
        /// </summary>
        /// <param name="sentences">The sentences.</param>
        /// <returns></returns>
        public Word[] Words(Sentence[] sentences)
        {
            if (sentences == null)
                return null;

            var results = _natrualLanguageProcessingService.Tokenize(sentences);

            return _wordFilters.Aggregate(results.ToArray(), (current, wordFilter) => wordFilter.Filter(current));
        }

        /// <summary>
        /// Ranks the specified search words.
        /// </summary>
        /// <param name="searchWords">The search words.</param>
        /// <param name="allWords">All words.</param>
        /// <returns></returns>
        public RankedSentence[] Rank(Word[] searchWords, Word[] allWords)
        {
            if (searchWords == null)
                return null;

            if (allWords == null)
                return null;

            var matches = Match(searchWords, allWords);

            var rankResults = new ConcurrentBag<RankedSentence>();

            foreach (var match in matches.GroupBy(c => c.Word.SentenceIndex))
            {
                var rank = _rankSpecifications.Aggregate(0.00m, (current, rankSpecification) => current + rankSpecification.Score(match, searchWords, allWords));

                var searchResult = new RankedSentence
                {
                    SentenceIndex = match.First().Word.SentenceIndex,
                    Rank = rank,
                    SearchedWords = searchWords,
                    MatchedWords = match.Select(c => c.Word).ToArray(),
                    IndexedWords = allWords.Where(c => c.SentenceIndex == match.Key).ToArray()
                };

                rankResults.Add(searchResult);
            }

            return rankResults.OrderByDescending(c => c.Rank).ToArray();
        }

        /// <summary>
        /// Matches the specified search words.
        /// </summary>
        /// <param name="searchWords">The search words.</param>
        /// <param name="allWords">All words.</param>
        /// <returns></returns>
        public MatchedWord[] Match(Word[] searchWords, Word[] allWords)
        {
            if (searchWords == null)
                return null;

            if (allWords == null)
                return null;

            var oprationalSearchWords = searchWords.Select(c => new Word
            {
                SentenceIndex = c.SentenceIndex,
                Text = c.Text,
                Stem = c.Stem,
                Start = c.Start,
                End = c.End
            }).ToArray();

            var oprationalWords = allWords.Select(c => new Word
            {
                SentenceIndex = c.SentenceIndex,
                Text = c.Text,
                Stem = c.Stem,
                Start = c.Start,
                End = c.End
            }).ToArray();

            return (from searchWord in oprationalSearchWords from matchedWord in oprationalWords.Where(c => c.Stem.Contains(searchWord.Stem)) select new MatchedWord 
            {
                Word = matchedWord,
                SearchWord = searchWord
            }).Distinct().OrderBy(c => c.Word.SentenceIndex).ThenBy(c => c.Word.Start).ToArray();
            
        }
    }
}
