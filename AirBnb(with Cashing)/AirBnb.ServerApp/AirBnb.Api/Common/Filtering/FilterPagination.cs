namespace AirBnb.Api.Common.Filtering;

public class FilterPagination
{
    public int PageSize { get; set; } = 50;

    public int PageToken { get; set; } = 1;
}