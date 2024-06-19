namespace Infinion.Domain.Results;
public class PagedResult<T>
{
    public int TotalCount { get; set; }
    public IEnumerable<T> Items { get; set; }

    public PagedResult(int totalCount, IEnumerable<T> items)
    {
        TotalCount = totalCount;
        Items = items;
    }
}

