using RecipeTracker.Models;

namespace RecipeTracker.Data.Services
{
    public interface ICategoryService
    {
        public Task<int> CategoryUpsert(Category recipe);
        public Task<int> CategoryDelete(int id);
        public Task<Category?> CategoryGet(int id);
        public Task<IEnumerable<Category>> CategoryGetAll();
    }
}
