namespace KingSurvival.Chess.Formatter
{
    using KingSurvival.Chess.Formatter.Contracts;

    public class FancyFormatter : IFormatter
    {
        public string Format(string message)
        {
            return string.Format("-= {0} =-", message);
        }
    }
}
