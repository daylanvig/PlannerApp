namespace UIComponents.Extensions
{
    public class ParsingExtensions
    {
        public static int? ParseStringAsNullableInt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return int.Parse(value);
        }
    }
}
