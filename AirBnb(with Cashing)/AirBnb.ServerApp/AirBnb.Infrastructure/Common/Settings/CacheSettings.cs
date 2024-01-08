namespace AirBnb.Infrastructure.Common.Settings;

public class CacheSettings
{
    public int AbsoluteExpirationTimeInMinutes { get; set; }

    public int SlidingExpirationTimeInMinutes { get; set; }
}