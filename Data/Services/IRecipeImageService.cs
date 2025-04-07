using Microsoft.AspNetCore.Components.Forms;

namespace RecipeTracker.Data.Services
{
    public interface IRecipeImageService
    {
        Task<int> RecipeImageInsert(IList<IBrowserFile> files, int id);
        string RecipeImageGet(int id); 
    }
}

