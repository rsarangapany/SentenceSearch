using Text.NaturalLanguageProcessing.Model;

namespace Text.Search.Service.Text.Filter.WordFilter
{
    public interface IWordFilter
    {
        Word[] Filter(Word[] words);
    }
}
