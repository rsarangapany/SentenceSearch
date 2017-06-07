using System;
using System.Collections.Generic;
using System.Linq;
using Text.NaturalLanguageProcessing.Model;
using Text.Search.Model;

namespace Text.Search.Service.Text.Rank.Specifications
{
    public class CosineSimilarityRankSpecification : ITextRankSpecification
    {
        public decimal Score(IGrouping<int, MatchedWord> matches, Word[] searchWords, Word[] allWords)
        {
            var numberOfSentences = allWords.Select(c => c.SentenceIndex).Distinct().Count();
            var sentence = allWords.Where(c => c.SentenceIndex == matches.Key).ToList();

            var searchTerms = new List<TfIdf>();
            var documentTerms = new List<TfIdf>();

            foreach (var term in matches.Select(c => c.Word.Stem).Distinct())
            {
                var df = allWords.Where(c => c.Stem == term).Select(c => c.SentenceIndex).Distinct().Count();
                var idf = Math.Log((double)numberOfSentences / (1 + df));

                var tf1 = (double)sentence.Count(c => term.Contains(c.Stem)) / sentence.Count;
                var tfIdf1 = tf1 * idf;

                documentTerms.Add(new TfIdf { Term = term, Value = tfIdf1 });

                var tf2 = (double)searchWords.Count(c => term.Contains(c.Stem)) / searchWords.Length;
                var tfIdf2 = tf2 * idf;

                searchTerms.Add(new TfIdf { Term = term, Value = tfIdf2 });
            }

            var similarity = 0D;
            var sqrtDocumentTerms = 0D;
            var sqrtSearchTerms = 0D;

            foreach (var searchTerm in searchTerms)
            {
                var documentTerm = documentTerms.Single(c => c.Term == searchTerm.Term);

                similarity += documentTerm.Value * searchTerm.Value;
                sqrtDocumentTerms += documentTerm.Value * documentTerm.Value;
                sqrtSearchTerms += searchTerm.Value * searchTerm.Value;
            }

            similarity /= Math.Sqrt(sqrtDocumentTerms) * Math.Sqrt(sqrtSearchTerms);

            return (decimal)similarity;
        }

        public class TfIdf
        {
            public string Term { get; set; }
            public double Value { get; set; }
        }
    }
}
