using Text.NaturalLanguageProcessing.Model;

namespace Text.Search.Service.Text.Filter.SentenceFilter
{
    public interface ISentenceFilter
    {
        Sentence[] Filter(Sentence[] sentences);
    }
}
