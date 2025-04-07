using Dapper;
using Microsoft.Data.Sqlite;
using RecipeTracker.Models;

namespace RecipeTracker.Data.Services
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
                return await connection.QueryAsync<Recipe>("SELECT * FROM Recipe");
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

                string sql = "SELECT * FROM Recipe WHERE Id = @Id";

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
                        UPDATE Recipe
                        SET Title = @Title,
                            ServingSize = @ServingSize,
                            PrepTime = @PrepTime,
                            CookTime = @CookTime,
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
                        INSERT INTO Recipe (Title, ServingSize, PrepTime, CookTime, Description, Ingredients, Instructions, CategoryId)
                        VALUES (@Title, @ServingSize, @PrepTime, @CookTime, @Description, @Ingredients, @Instructions, @CategoryId);

                        SELECT LAST_INSERT_ROWID();
                        ";
                }
                return await connection.QuerySingleAsync<int>(sql, recipe);
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

                string sql = @"DELETE FROM Recipe WHERE Id = @Id";

                return await connection.ExecuteAsync(sql, new { Id = id });
            }
            catch(SqliteException ex)
            {
                throw new Exception("Error deleting recipe: " + ex.Message);
            }
        }
    }
}
