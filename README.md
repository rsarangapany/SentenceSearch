# Sentence search using NLP

### Setup.

Add the following nugets:

> * Microsoft.Extensions.DependencyInjection.Abstraction;
> * Microsoft.Extensions.DependencyInjection;

*The language models for Open NLP get automatically published to your bin directory.* 

This can be accessed via: 

```C# 
//For web application
var languageModelPaths = $"{AppDomain.CurrentDomain.BaseDirectory}Bin\\Resource\\Models"; 
```

```C# 
//For console application
var languageModelPaths = $"{AppDomain.CurrentDomain.BaseDirectory}Resource\\Models"; 
```

If you are using your own IOC container then you will have to configure your IOC to work with this.

Add the following namespaces

```c#
using Microsoft.Extensions.DependencyInjection;
using Text.Extract;
using Text.Extract.Tag;
using Text.Extract.Factory;
using Text.Search;
using Text.Search.Factory;
using Text.NaturalLanguageProcessing;
using Text.Prototyping.Models;
```

Once setup use the following code to use the libraries

```C#
IServiceCollection serviceCollection = new ServiceCollection();

//Register the services
serviceCollection.RegisterNaturalLanguageProcessingService(languageModelPaths);
serviceCollection.RegisterSearchService();
serviceCollection.RegisterExtractService();

//Build the service provider
var serviceProvider = serviceCollection.BuildServiceProvider();

//Get the service factories
var textServiceFactory = serviceProvider.GetService<ITextSearchFactory>();
var textExtractionServiceFactory = serviceProvider.GetService<ITextExtractFactory>();

//Setup the extraction rules
//Currently supported rules are FoundTag &  AnonymiseTag
var extractTags = new IExtractTag[]
{
    new FoundTag("Found"),
    new AnonymiseTag("Anonymised", "*******", model.AnoymisedText.Split(','), textServiceFactory.TextService)
};

//Extract the snippets
var snippets = textExtractionServiceFactory.TextExtract.ExtractSnippets(model.DocumentText, model.SearchText, extractTags);

```
