using AS91892.Core.ImageConversion;
using AS91892.Core.MockData;
using AS91892.Data.Context;
using AS91892.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AS91892.Web;

/// <summary>
/// The <see cref="Startup"/> class configures the application services and our request pipline
/// </summary>
public class Startup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// The <see cref="IConfiguration"/> for the <see cref="Startup"/> class
    /// </summary>
    public IConfiguration Configuration { get; }


    private bool IsTest => Configuration.GetSection("Data").GetValue<bool>("Enable-Test-Data");

    // This method gets called by the runtime. Use this method to add services to the container.
    /// <summary>
    /// Configures services to be used in the application
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {        
        services.AddControllersWithViews();
        services.AddMvc();

        services.AddScoped<IMockDataResolver<Artist>, ArtistMockResolver>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            // If Enable-Test-Data is set to true then Add-Migration [migration-name] won't as this is an in memory db
            if (IsTest)
            {
                options.UseInMemoryDatabase($"Tests");
                return;
            }

            options.UseSqlServer(Configuration.GetConnectionString(nameof(ApplicationDbContext)), x => x.MigrationsAssembly("AS91892.Data"));
            
        });

        ServicesSetup.ConfigureSingletons(services);
        ServicesSetup.ConfigureScoped(services);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// <summary>
    /// Configures the application
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="context"></param>
    /// <param name="resolver"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context, IMockDataResolver<Artist> resolver)
    {
        if (IsTest) // adds our dummy data to the app
        {
            context.Artists.AddRange(resolver.GenerateMock());
            context.SaveChanges();
        }

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseStatusCodePagesWithReExecute("/Home/HandleError/{0}");

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"));
    }
}
