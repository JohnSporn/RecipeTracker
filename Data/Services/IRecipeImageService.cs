namespace RecipeTracker.Data.Services
{
    public interface IRecipeImageService
    {
        Task<int> RecipeImageInsert(IList<FileResult> files, int id);
        string RecipeImageGet(int id); 
    }
}

