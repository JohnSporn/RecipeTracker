using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using RecipeTracker.Data.Services;

namespace RecipeTracker
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
            builder.Services.AddMudServices();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "recipes.db");
            builder.Services.AddSingleton(new DatabaseService(dbPath));

            builder.Services.AddSingleton<IRecipeService, RecipeService>();
            builder.Services.AddSingleton<IRecipeImageService, RecipeImageService>();
            builder.Services.AddSingleton<ICategoryService, CategoryService>();

#if WINDOWS
            builder.Services.AddSingleton<IScannerService, WindowsScannerService>();
#endif

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
