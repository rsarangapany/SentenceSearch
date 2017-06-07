using Text.Search.Model;

namespace Text.Extract.Tag
{
    public interface IExtractTag
    {
        string Format { get; }
        string Tag(string sourceText, SearchResult searchResult);
    }
}
