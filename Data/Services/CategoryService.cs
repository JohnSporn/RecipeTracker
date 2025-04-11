using Dapper;
using Microsoft.Data.Sqlite;
using RecipeTracker.Models;

namespace RecipeTracker.Data.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseService _dbService;

        public CategoryService(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        public async Task<IEnumerable<Category>> CategoryGetAll()
        {
            try
            {
                using var connection = _dbService.CreateConnection();

                return await connection.QueryAsync<Category>("SELECT * FROM Category");
            }
            catch (SqliteException ex)
            {
                throw new Exception("Error getting Categories: " + ex.Message);
            }
        }

        public async Task<Category> CategoryGet(int id)
        {
            try
            {
                using var connection = _dbService.CreateConnection();

                string sql = "SELECT * FROM Category WHERE Id = @Id";

                return await connection.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });
            }
            catch (SqliteException ex)
            {
                throw new Exception("Error getting Category: " + ex.Message);
            }
        }

        public async Task<int> CategoryUpsert(Category Category)
        {
            try
            {
                using var connection = _dbService.CreateConnection();
                string sql = string.Empty;

                if (Category.Id != 0)
                {
                    sql = @"
                        UPDATE Category
                        SET Name = @Name
                        WHERE Id = @Id;
                    ";
                }
                else
                {
                    sql = @"
                        INSERT INTO Category (Name)
                        VALUES (@Name);
    
                        SELECT LAST_INSERT_ROWID(); 
                        ";
                }
                return await connection.QuerySingleAsync<int>(sql, Category);
            }
            catch (SqliteException ex)
            {
                throw new Exception("Error saving Category: " + ex.Message);
            }
        }

        public async Task<int> CategoryDelete(int id)
        {
            try
            {
                using var connection = _dbService.CreateConnection();

                string sql = @"DELETE FROM Category WHERE Id = @Id";

                return await connection.ExecuteAsync(sql, new { Id = id });
            }
            catch (SqliteException ex)
            {
                throw new Exception("Error deleting Category: " + ex.Message);
            }
        }
    }
}
