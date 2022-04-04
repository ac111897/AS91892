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
    /// <param name="configuration">Web application configuration</param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// The <see cref="IConfiguration"/> for the <see cref="Startup"/> class
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// If the test data should be enabled
    /// </summary>
    private bool IsTest => Configuration.GetSection("Data").GetValue<bool>("Enable-Test-Data");

    // This method gets called by the runtime. Use this method to add services to the container.
    /// <summary>
    /// Configures services to be used in the application
    /// </summary>
    /// <param name="services">The service collection to add services to</param>
    public void ConfigureServices(IServiceCollection services)
    {        
        services.AddControllersWithViews();
        services.AddMvc();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            // If Enable-Test-Data is set to true then Add-Migration [migration-name] won't as this is an in memory db
            if (IsTest)
            {
                options.UseInMemoryDatabase($"Tests");
            }
            else
            {
                options.UseSqlServer(Configuration.GetConnectionString(nameof(ApplicationDbContext)), x => x.MigrationsAssembly("AS91892.Data"));
            }
        });

        services.ConfigureSingletons();
        services.ConfigureScoped();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// <summary>
    /// Configures the application
    /// </summary>
    /// <param name="app">The application and it's settings</param>
    /// <param name="env">The environment our web app is run on</param>
    /// <param name="context">Our context passed in by DI</param>
    /// <param name="provider">Our service provider to retrieve services whilst in the configure method</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context, IServiceProvider provider)
    {

        if (IsTest) // adds our dummy data to the app
        {
            DataInitializer.InitializeData(ref context, provider);
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

        //app.UseStatusCodePagesWithReExecute("/Home/HandleError/{0}");

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"));
    }
}
