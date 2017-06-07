using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Text.NaturalLanguageProcessing.Service.Text;

namespace Text.NaturalLanguageProcessing.Factory
{
    public class NaturalLanguageProcessingFactory : INaturalLanguageProcessingFactory
    {
        public INatrualLanguageProcessingService NatrualLanguageProcessingService { get; }
        public IStemmerService StemmerService { get; }

        public NaturalLanguageProcessingFactory(INatrualLanguageProcessingService natrualLanguageProcessingService, IStemmerService stemmerService)
        {
            NatrualLanguageProcessingService = natrualLanguageProcessingService;
            StemmerService = stemmerService;
        }
    }
}
