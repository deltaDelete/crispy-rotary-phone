using WebApplication1;
using AppContext = WebApplication1.AppContext;

internal static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.ConfigureServices();
        
        var app = builder.Build();
        app.ConfigureApp();

        app.Run();
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        // services.AddDbContext<AppContext>();
        services.AddDbContextFactory<AppContext>();
        services.AddControllers();
    }

    public static void ConfigureApp(this WebApplication app)
    {
        app.MapControllers();
    }
}