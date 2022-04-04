using AS91892.Core.MockData;
using AS91892.Web.ControllerFinder;

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
    internal static void ConfigureScoped(this IServiceCollection services)
    {
        services.AddScoped<IMockDataResolver<Artist>, ArtistMockResolver>();
        services.AddScoped<IMockDataResolver<RecordLabel>, LabelMockResolver>();
        services.AddScoped<IMockDataResolver<Genre>, GenreMockResolver>();
        services.AddScoped<IMockDataResolver<Album>, AlbumMockResolver>();
        services.AddScoped<IMockDataResolver<Song>, SongMockResolver>();

        // configure our data repositories
        services.AddScoped<IArtistRepository, ArtistRepository>();
        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<ILabelRepository, LabelRepository>();
        services.AddScoped<ISongRepository, SongRepository>(); 
        services.AddScoped<IImageRepository, ImageRepository>();
    }

    /// <summary>
    /// Configures the <see cref="IServiceCollection"/> for singleton services
    /// </summary>
    /// <param name="services">An <see cref="IServiceCollection"/> to configure the services</param>
    internal static void ConfigureSingletons(this IServiceCollection services)
    {
        // add image converter to convert an IFormFile to an image and save it to our wwwroot in the project to be referenced later
        services.AddSingleton<IImageConverter<Guid>, ImageConverter>();
        services.AddSingleton<IControllerNameFinder, ControllerNameFinder>();
    }
}
