# Sentence search using NLP

### Setup.

*The language models for Open NLP get automatically published to your bin directory.* 

This can be accessed via: 

> var languageModelPaths = $"{AppDomain.CurrentDomain.BaseDirectory}Bin\\Resource\\Models";

You will beed to add the following nugets:

> * Microsoft.Extensions.DependencyInjection.Abstraction;
> * Microsoft.Extensions.DependencyInjection;

If you are using your own IOC container then you will have to configure your IOC to work with thi.

Once setup use the following code to use the libraries

IServiceCollection serviceCollection = new ServiceCollection();

serviceCollection.RegisterNaturalLanguageProcessingService(languageModelPaths);
serviceCollection.RegisterSearchService();
serviceCollection.RegisterExtractService();

var serviceProvider = serviceCollection.BuildServiceProvider();

var textServiceFactory = serviceProvider.GetService<ITextSearchFactory>();

var textExtractionServiceFactory = serviceProvider.GetService<ITextExtractFactory>();

var extractTags = new IExtractTag[]
{
    new FoundTag("Found"),
    new AnonymiseTag("Anonymised", "*******", model.AnoymisedText.Split(','), textServiceFactory.TextService)
};

var snippets = textExtractionServiceFactory.TextExtract.ExtractSnippets(model.DocumentText, model.SearchText, extractTags);

model.SearchResults = snippets.Select(snippet => new HomeViewModel.SearchViewModel {Rank = snippet.Rank, Text = snippet.Text}).ToArray();
