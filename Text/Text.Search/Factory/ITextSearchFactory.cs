using Text.Search.Service.Document;
using Text.Search.Service.Text;

namespace Text.Search.Factory
{
    public interface ITextSearchFactory
    {
        ITextService TextService { get; }

        IDocumentService DocumentService { get; }
    }
}