using System.Threading.Tasks;
using AS91892.Data;
using AS91892.Data.Context;
using AS91892.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AS91892.Tests;

internal static class Creation
{
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
