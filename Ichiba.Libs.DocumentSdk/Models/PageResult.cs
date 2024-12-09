using Microsoft.EntityFrameworkCore;

namespace Ichiba.Libs.DocumentSdk.Models;

public class PageResult<T>
{
    public PageResult(List<T> items, int totalPages, int totalRecords)
    {
        TotalPages = totalPages;
        TotalRecords = totalRecords;
        Items = items;
    }

    public IEnumerable<T> Items { get; }
    public int TotalPages { get; }
    public int TotalRecords { get; }

    public static async Task<PageResult<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);
        var totalRecords = count;
        return new PageResult<T>(items, totalPages, totalRecords);
    }
}
