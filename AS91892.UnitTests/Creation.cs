using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AS91892.Tests;

/// <summary>
/// Creation class to provide the context and loggers for testing
/// </summary>
internal static class Creation
{
    private static readonly ILoggerFactory factory =
        LoggerFactory.Create(x =>
        {
            x.AddConsole();
            x.SetMinimumLevel(LogLevel.Debug);
        });

    /// <summary>
    /// Creates a logger of <typeparamref name="TCategory"/>
    /// </summary>
    /// <typeparam name="TCategory">The category we are logging</typeparam>
    /// <returns>A logger of <typeparamref name="TCategory"/></returns>
    internal static ILogger<TCategory> CreateLogger<TCategory>()
    {
        return factory.CreateLogger<TCategory>();
    }
    /// <summary>
    /// Creates a context and fills it with models
    /// </summary>
    /// <typeparam name="TModel">The type of model to fill</typeparam>
    /// <param name="contextAction">The action to apply to the context</param>
    /// <param name="recordsToAdd">The records to add to the database</param>
    /// <returns>The modified context</returns>
    internal static ApplicationDbContext CreateContext<TModel>(Action<TModel, ApplicationDbContext> contextAction, List<TModel> recordsToAdd)
        where TModel : BaseEntity
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;


        var context = new ApplicationDbContext(options);

        context.Database.EnsureCreated();

        foreach (var item in recordsToAdd)
        {
            contextAction(item, context);
        }

        
        return context;
    }
}
