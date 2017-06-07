namespace Text.NaturalLanguageProcessing.Model
{
    public class Word
    {
        public int SentenceIndex { get; set; }
        public string Text { get; set; }
        public string Stem { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
    }
}
