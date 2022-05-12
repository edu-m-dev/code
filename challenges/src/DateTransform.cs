using System.Globalization;

namespace Challenges;

public class DateTransform
{
    public static List<string> TransformDateFormat(List<string> dates)
    {
        var acceptedFormats = new List<string>() { "yyyy/MM/dd", "dd/MM/yyyy", "MM-dd-yyyy" };
        var transformedDates = new List<string>();
        foreach (var date in dates)
        {
            foreach (var format in acceptedFormats)
            {
                if (DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var _date))
                {
                    transformedDates.Add(_date.ToString("yyyyMMdd"));
                    break;
                }
            }
        }
        return transformedDates;
    }
}
