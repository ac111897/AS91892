using Microsoft.EntityFrameworkCore;

namespace AS91892.Core;

/// <summary>
/// Paginated list used for pagination on web pages so we don't hit the database too much
/// </summary>
/// <typeparam name="T">T can be any element to scroll through</typeparam>
public class PaginatedList<T> : List<T>
{
    /// <summary>
    /// The current page index of this list
    /// </summary>
    public int PageIndex { get; private set; }

    /// <summary>
    /// The total amount pages in this list to scroll through
    /// </summary>
    public int TotalPages { get; private set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class
    /// </summary>
    /// <param name="items">The items to fill the list with</param>
    /// <param name="count">The count of items in the list</param>
    /// <param name="pageIndex">The page index the list is on</param>
    /// <param name="pageSize">The amount of elements in each page</param>
    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    /// <summary>
    /// If the list has a previous page
    /// </summary>
    public bool HasPreviousPage => PageIndex > 1;

    /// <summary>
    /// If the list has another page
    /// </summary>
    public bool HasNextPage => PageIndex < TotalPages;

    /// <summary>
    /// Creates a <see cref="PaginatedList{T}"/> from a source of <see cref="IQueryable{T}"/>
    /// </summary>
    /// <param name="source">The data source of the list</param>
    /// <param name="pageIndex">The page index we should be on</param>
    /// <param name="pageSize">The size of each page</param>
    /// <returns>A <see cref="PaginatedList{T}"/></returns>
    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}
