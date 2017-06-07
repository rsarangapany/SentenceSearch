using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using OpenNLP.Tools.SentenceDetect;
using OpenNLP.Tools.Tokenize;
using Text.NaturalLanguageProcessing.Model;

namespace Text.NaturalLanguageProcessing.Service.Text
{
    internal class NatrualLanguageProcessingService: INatrualLanguageProcessingService
    {
        private readonly IStemmerService _stemmer;
        private readonly ITokenizer _tokenizer;
        private readonly ISentenceDetector _sentenceDetector;

        /// <summary>
        /// Initializes a new instance of the <see cref="NatrualLanguageProcessingService"/> class.
        /// </summary>
        /// <param name="stemmer">The stemmer.</param>
        /// <param name="tokanizer">The tokanizer.</param>
        /// <param name="sentenceDetector">The sentence detector.</param>
        public NatrualLanguageProcessingService(IStemmerService stemmer, ITokenizer tokanizer, ISentenceDetector sentenceDetector)
        {
            _stemmer = stemmer;
            _tokenizer = tokanizer;
            _sentenceDetector = sentenceDetector;
        }

        /// <summary>
        /// Sentences the detect.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public Sentence[] SentenceDetect(string text)
        {  
            return _sentenceDetector.SentenceDetect(text).Select((sentence, index) => new Sentence { Index = index, Text = sentence.ToLower() }).ToArray();
        }

        /// <summary>
        /// Tokenizes the specified sentences.
        /// </summary>
        /// <param name="sentences">The sentences.</param>
        /// <returns></returns>
        public Word[] Tokenize(Sentence[] sentences)
        {
            var results = new ConcurrentBag<Word>();

            Parallel.ForEach(sentences, sentence =>
            {
                var tokens = _tokenizer.Tokenize(sentence.Text);
                var positions = _tokenizer.TokenizePositions(sentence.Text);

                for (var count = 0; count < tokens.Length; count++)
                {
                    if (!string.IsNullOrEmpty(tokens[count]))
                    {
                        results.Add(new Word
                        {
                            SentenceIndex = sentence.Index,
                            Text = tokens[count].ToLower(),
                            Stem = _stemmer.Stem(tokens[count].ToLower()),
                            Start = positions[count].Start,
                            End = positions[count].End
                        });
                    }
                }
            });

            return results.ToArray();
        }
    }
}
