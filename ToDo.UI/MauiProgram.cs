using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDo.Shared.Data;
using ToDo.Shared.Services;

namespace ToDo.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            //Sql lite connection for device storeage
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                // Configure SQLite to use a local file in the device's storage
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoDataCenter.db");
                options.UseSqlite($"Filename={databasePath}");
            });

            // Ensure that the database is created on startup
            using (var scope = builder.Services.BuildServiceProvider().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.EnsureCreated(); // Creates the database if it doesn't exist
            }
            // Register Generic DataService
            builder.Services.AddScoped(typeof(IDataService<,>), typeof(DataService<,>));

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
