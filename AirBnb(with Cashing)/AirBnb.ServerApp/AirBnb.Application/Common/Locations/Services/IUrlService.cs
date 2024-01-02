namespace AirBnb.Application.Common.Locations.Services;

public interface IUrlService
{
    ValueTask<string> GetUrlFromRelativePath(string relativePath);
}