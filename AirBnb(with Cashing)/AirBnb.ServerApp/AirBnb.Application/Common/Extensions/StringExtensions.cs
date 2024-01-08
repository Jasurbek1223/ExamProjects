namespace AirBnb.Application.Common.Extensions;

public static class StringExtensions
{
    public static string ToUrl(this string path)
    {
        return path.Replace(@"\", @"/");
    }

    public static string ToPath(this string url)
    {
        return url.Replace(@"/", @"\");
    }
}