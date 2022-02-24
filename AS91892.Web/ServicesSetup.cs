namespace AS91892.Web;

/// <summary>
/// Class to setup an <see cref="IServiceCollection"/> in a clean manner
/// </summary>
internal static class ServicesSetup
{
    /// <summary>
    /// Configures the <see cref="IServiceCollection"/> for scoped services
    /// </summary>
    /// <param name="services">An <see cref="IServiceCollection"/> to configure the services</param>
    internal static void ConfigureScoped(IServiceCollection services)
    {
        // configure our data repositories
        services.AddScoped<IArtistRepository, ArtistRepository>();
        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<ILabelRepository, LabelRepository>();
        services.AddScoped<ISongRepository, SongRepository>(); 
    }

    /// <summary>
    /// Configures the <see cref="IServiceCollection"/> for singleton services
    /// </summary>
    /// <param name="services">An <see cref="IServiceCollection"/> to configure the services</param>
    internal static void ConfigureSingletons(IServiceCollection services)
    {
        services.AddSingleton<IImageConverter<Guid>, ImageConverter>();
    }
}
