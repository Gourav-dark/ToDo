using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDo.Shared.Data;
using ToDo.Shared.Services;
using ToDo.UI.Authentication;
using ToDo.UI.Services;

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
                dbContext.Database.EnsureCreated();
            }
            //Authentication
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<CustomAuthenticationStateProvider>();
            //Loading Service
            builder.Services.AddSingleton<LoadingService>();
            // Register Generic DataService
            builder.Services.AddScoped(typeof(IDataService<,>), typeof(DataService<,>));
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<ISecureDataService, SecureDataService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
