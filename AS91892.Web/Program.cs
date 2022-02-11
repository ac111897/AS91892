namespace AS91892.Web;

/// <summary>
/// Class for when our program starts
/// </summary>
public class Program
{
    /// <summary>
    /// Main method of the <see cref="Program"/>
    /// </summary>
    /// <param name="args">Args passed to the application</param>
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    /// Creates an <see cref="IHostBuilder"/> for our app
    /// </summary>
    /// <param name="args"></param>
    /// <returns>A <see cref="IHostBuilder"/></returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
}
