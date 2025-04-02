using Dapper;
using Microsoft.Data.Sqlite;
using RecipeTracker.Models;

namespace RecipeTracker.Data.Service
{
    public class RecipeService : IRecipeService
    {
        private readonly DatabaseService _dbService;

        public RecipeService(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        public async Task<IEnumerable<Recipe>> RecipeGetAll()
        {
            try
            {
                using var connection = _dbService.CreateConnection();
                return await connection.QueryAsync<Recipe>("SELECT * FROM Recipes");
            }
            catch(SqliteException ex)
            {
                throw new Exception("Error getting recipes: " + ex.Message);
            }
        }

        public async Task<Recipe?> RecipeGet(int id)
        {
            try
            {
                using var connection = _dbService.CreateConnection();

                string sql = "SELECT * FROM Recipes WHERE Id = @Id";

                return await connection.QueryFirstOrDefaultAsync<Recipe>(sql, new { Id = id });
            }
            catch (SqliteException ex)
            {
                throw new Exception("Error getting recipe: " + ex.Message);
            }
        }

        public async Task<int> RecipeInsert(Recipe recipe)
        {
            try
            {
                using var connection = _dbService.CreateConnection();
                string sql = string.Empty;

                if (recipe.Id != 0)
                {
                    sql = @"
                        UPDATE Recipes
                        SET Name = @Name,
                            Description = @Description,
                            Ingredients = @Ingredients,
                            Instructions = @Instructions,
                            CategoryId = @CategoryId
                        WHERE Id = @Id;
                    ";
                }
                else
                {
                    sql = @"
                        INSERT INTO Recipes (Name, Description, Ingredients, Instructions, CategoryId)
                        VALUES (@Name, @Description, @Ingredients, @Instructions, @CategoryId);";
                }
                return await connection.ExecuteAsync(sql, recipe);
            }
            catch (SqliteException ex)
            {
                throw new Exception("Error saving recipe: " + ex.Message);
            }
        }

        public async Task<int> RecipeDelete(int id)
        {
            try
            {
                using var connection = _dbService.CreateConnection();

                string sql = @"DELETE FROM Recipes WHERE Id = @Id";

                return await connection.ExecuteAsync(sql, new { Id = id });
            }
            catch(SqliteException ex)
            {
                throw new Exception("Error deleting recipe: " + ex.Message);
            }
        }
    }
}
