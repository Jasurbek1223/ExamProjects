using AirBnb.Application.Common.Extensions;
using AirBnb.Application.Common.Locations.Services;
using AirBnb.Infrastructure.Common.Settings;
using Microsoft.Extensions.Options;
    
namespace AirBnb.Infrastructure.Common.Locations.Services;

public class UrlService(IOptions<UrlSettings> options) : IUrlService
{
    public ValueTask<string> GetUrlFromRelativePath(string relativePath)
    {
        return new(Path.Combine(options.Value.BaseUrl, relativePath.ToUrl()));
    }
}