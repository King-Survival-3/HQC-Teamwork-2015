namespace KingSurvival.Chess.Formatter
{
    using KingSurvival.Chess.Formatter.Contracts;

    public class StandardFormatter : IFormatter
    {
        public string Format(string message)
        {
            return string.Format("{0}", message);
        }
    }
}
