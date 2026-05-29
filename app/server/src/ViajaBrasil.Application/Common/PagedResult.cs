namespace ViajaBrasil.Application.Common;

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = [];
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public string? Search { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    
    public PagedResult(List<T> items, int totalItems, int pageNumber, int pageSize, string search = null)
    {
        Items = items;
        TotalItems = totalItems;
        CurrentPage = pageNumber;
        PageSize = pageSize;
        Search = search;
    }
}