using System;
using System.Linq;
using Text.NaturalLanguageProcessing.Model;

namespace Text.Search.Service.Text.Filter.SentenceFilter
{
    public class SentenceTabFilter: ISentenceFilter
    {
        public Sentence[] Filter(Sentence[] sentences)
        {
            foreach (var sentence in sentences)
            {
                sentence.Text = string.Join(Environment.NewLine, sentence.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(line => line.Trim()));
            }

            return sentences;
        }
    }
}
