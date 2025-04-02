using RecipeTracker.Models;

namespace RecipeTracker.Data.Service
{
    public interface IRecipeImageService
    {
        Task<int> RecipeImageInsert(IList<FileResult> files, int id);
        string RecipeImageGet(int id); 
    }
}

