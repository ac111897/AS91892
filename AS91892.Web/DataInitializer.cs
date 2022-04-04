using AS91892.Core.MockData;

namespace AS91892.Web;

/// <summary>
/// Static class to add test data to our database
/// </summary>
public static class DataInitializer
{
    /// <summary>
    /// Adds data the specified <see cref="ApplicationDbContext"/>
    /// </summary>
    /// <param name="context">The context to add data to</param>
    /// <param name="provider">The service provider to get data resolvers from</param>
    public static void InitializeData(ref ApplicationDbContext context, IServiceProvider provider)
    {
        var genreResolver = provider.GetService<IMockDataResolver<Genre>>();
        var labelResolver = provider.GetService<IMockDataResolver<RecordLabel>>();
        var artistResolver = provider.GetService<IMockDataResolver<Artist>>();
        var albumResolver = provider.GetService<IMockDataResolver<Album>>();
        var songResolver = provider.GetService<IMockDataResolver<Song>>();

        ThrowIfNull(genreResolver);
        ThrowIfNull(labelResolver);
        ThrowIfNull(artistResolver);
        ThrowIfNull(albumResolver);
        ThrowIfNull(songResolver);

        // we are sure that these resolvers aren't null because if they are we throw an exception

#pragma warning disable CS8602 // Dereference of a possibly null reference. 
        context.Genres.AddRange(genreResolver.GenerateMock());
        context.RecordLabels.AddRange(labelResolver.GenerateMock());

        context.SaveChanges();

        AddAlbumsAndSongsToContext(ref context, 
            artistResolver.GenerateMock(), 
            albumResolver.GenerateMock(), 
            songResolver.GenerateMock());
    }

    private static void AddAlbumsAndSongsToContext(ref ApplicationDbContext context, 
        IEnumerable<Artist> artists, IEnumerable<Album> albums, IEnumerable<Song> songs)
    {
        Random random = new();

        var artistsList = artists.ToList();
        var albumsList = albums.ToList();

        var genresList = context.Genres.ToList();

        foreach (var song in songs)
        {
            song.Genre = genresList[random.Next(genresList.Count)];
            albumsList[random.Next(albumsList.Count)].AlbumSongs.Add(song);
        }

        foreach (var album in albums)
        {
            artistsList[random.Next(artistsList.Count)].Albums.Add(album);
        }

        var labels = context.RecordLabels.ToList();

        for (int i = 0; i < 10; i++)
        {
            labels.Add(null!);
        }


        foreach (var artist in artistsList)
        {
            artist.Label = labels[random.Next(labels.Count)];
        }

        context.Artists.AddRange(artistsList);

        context.SaveChanges();
    }

    private static void ThrowIfNull<T>(T resolver) 
    {
        if (resolver is null)
        {
            throw new InvalidOperationException($"The resolver of {typeof(T).Name} " +
                $"where {nameof(T)} is {typeof(T).GenericTypeArguments.FirstOrDefault().Name} was found null");
        }
    }
}
