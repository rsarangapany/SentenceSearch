namespace Text.NaturalLanguageProcessing.Service.Text
{
    public interface IStemmerService
    {
        /// <summary>
        /// Stems the specified word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        string Stem(string word);
    }
}
