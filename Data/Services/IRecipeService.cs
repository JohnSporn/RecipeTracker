using RecipeTracker.Models;

namespace RecipeTracker.Data.Services
{
    public interface IRecipeService
    {
        public Task<int> RecipeInsert(Recipe recipe);
        public Task<int> RecipeDelete(int id);
        public Task<Recipe?> RecipeGet(int id);
        public Task<IEnumerable<Recipe>> RecipeGetAll();
    }
}
