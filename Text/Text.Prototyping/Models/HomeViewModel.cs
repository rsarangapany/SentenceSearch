using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Text.Prototyping.Models
{
    public class HomeViewModel
    {
        public string DocumentText { get; set; }

        public string AnoymisedText { get; set; }

        public string SearchText { get; set; }

        public SearchViewModel[] SearchResults { get; set; }

        public class SearchViewModel
        {
            public decimal Rank { get; set; }

            public string Text { get; set; }
        }
    }
}