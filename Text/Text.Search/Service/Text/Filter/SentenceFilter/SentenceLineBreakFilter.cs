using System;
using Text.NaturalLanguageProcessing.Model;

namespace Text.Search.Service.Text.Filter.SentenceFilter
{
    public class SentenceLineBreakFilter: ISentenceFilter
    {
        public Sentence[] Filter(Sentence[] sentences)
        {
            foreach (var sentence in sentences)
            {
                sentence.Text = sentence.Text.Replace(Environment.NewLine, " ");
            }

            return sentences;
        }
    }
}
