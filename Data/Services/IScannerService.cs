namespace RecipeTracker.Data.Services
{
    public interface IScannerService
    {
        Task<string> ScanAsync();
    }
}
