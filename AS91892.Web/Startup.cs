using AS91892.Data.Context;
using AS91892.Data.Repositories;

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

    // This method gets called by the runtime. Use this method to add services to the container.
    /// <summary>
    /// Configures services to be used in the application
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped<IArtistRepository, ArtistRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// <summary>
    /// Configures the application
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"));
    }
}
