namespace AS91892.Core;

/// <summary>
/// Extensions class used for methods that aren't to specific to one dll and could be used elsewhere
/// </summary>
public static class Extensions
{
    // from https://stackoverflow.com/a/2575603 (slightly modified)
    /// <summary>
    /// Places the item at the specified index to the front of the list
    /// </summary>
    /// <typeparam name="T">The type of elements in the list</typeparam>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <param name="list">The source list</param>
    /// <param name="index">The index of the item to move</param>
    public static void MoveToFront<T>(this List<T> list, int index) 
    {
        ArgumentNullException.ThrowIfNull(list, nameof(list));

        if (list.Count - 1 < index || index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        T item = list[index];
        list.RemoveAt(index);
        list.Insert(0, item);
    }
}
