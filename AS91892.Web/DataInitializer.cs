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
        // get all our resolvers and validate they exist
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

        // add relationship-less data
        context.Genres.AddRange(genreResolver.GenerateMock());
        context.RecordLabels.AddRange(labelResolver.GenerateMock());

        context.SaveChanges();

        // generate all our data
        AddAlbumsAndSongsToContext(ref context, 
            artistResolver.GenerateMock(), 
            albumResolver.GenerateMock(), 
            songResolver.GenerateMock());
    }

    /// <summary>
    /// Complex function to randomise and add records to the database
    /// </summary>
    /// <param name="context">The context</param>
    /// <param name="artists">Artists to add</param>
    /// <param name="albums">Albums to add</param>
    /// <param name="songs">Songs to add</param>
    private static void AddAlbumsAndSongsToContext(ref ApplicationDbContext context, 
        IEnumerable<Artist> artists, IEnumerable<Album> albums, IEnumerable<Song> songs)
    {
        Random random = new(); // our random generator


        // put all the records we need in a list so we can access their index
        var artistsList = artists.ToList();
        var albumsList = albums.ToList();
        var genresList = context.Genres.ToList();
        var labels = context.RecordLabels.ToList();


        foreach (var song in songs) // iterate over the songs randomly add a genre to the song, then add it to a random album
        {
            song.Genre = genresList[random.Next(genresList.Count)];
            albumsList[random.Next(albumsList.Count)].AlbumSongs.Add(song);
        }

        foreach (var album in albums) // iterate over albums and randomly add them to an artists
        {
            artistsList[random.Next(artistsList.Count)].Albums.Add(album);
        }

        

        for (int i = 0; i < 10; i++) // add some null values to the labels collection, as label can be nullable
        {
            labels.Add(null!);
        }


        foreach (var artist in artistsList)
        {
            artist.Label = labels[random.Next(labels.Count)]; // randomly add a label to the artists
        }

        context.Artists.AddRange(artistsList); // add the artists to the database which will include all the other records

        context.SaveChanges();
    }

    /// <summary>
    /// Throw an invalid operation if our resolver is null as we can not perform initialization without the specified resolver
    /// </summary>
    /// <typeparam name="T">The resolver's type</typeparam>
    /// <param name="resolver">The resolver to check for null</param>
    /// <exception cref="InvalidOperationException">Exception thats thrown if its null</exception>
    private static void ThrowIfNull<T>(T resolver) 
    {
        // this could be used by anything thats a not ref struct but it is a private method

        if (resolver is null)
        {
            throw new InvalidOperationException($"The resolver of {typeof(T).Name} " +
                $"where {nameof(T)} is {typeof(T).GenericTypeArguments.FirstOrDefault().Name} was found null");
        }
    }
}
