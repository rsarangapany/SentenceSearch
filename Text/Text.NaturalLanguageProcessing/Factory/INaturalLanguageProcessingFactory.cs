using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Text.NaturalLanguageProcessing.Service.Text;

namespace Text.NaturalLanguageProcessing.Factory
{
    public interface INaturalLanguageProcessingFactory
    {
        INatrualLanguageProcessingService NatrualLanguageProcessingService { get; }

        IStemmerService StemmerService { get; }
    }
}
