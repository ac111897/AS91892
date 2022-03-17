using System.Threading.Tasks;
using AS91892.Data;
using AS91892.Data.Context;
using AS91892.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace AS91892.Tests;

internal static class Creation
{
    private static readonly ILoggerFactory factory =
        LoggerFactory.Create(x =>
        {
            x.AddConsole();
            x.SetMinimumLevel(LogLevel.Debug);
        });
    internal static ILogger<TCategory> CreateLogger<TCategory>()
    {
        return factory.CreateLogger<TCategory>();
    }
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
