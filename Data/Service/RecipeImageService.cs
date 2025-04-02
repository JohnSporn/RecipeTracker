using Dapper;
using Microsoft.Data.Sqlite;
using RecipeTracker.Models;

namespace RecipeTracker.Data.Service
{
    public class RecipeImageService : IRecipeImageService
    {
        private readonly DatabaseService _dbService;

        public RecipeImageService(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        public string RecipeImageGet(int id)
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "Images", id.ToString());

            if(Directory.Exists(filePath))
            {
                var files = Directory.GetFiles(filePath);
                return files.FirstOrDefault()!;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<int> RecipeImageInsert(IList<FileResult> files, int id)
        {
            try
            {
                string folderPath = Path.Combine(FileSystem.AppDataDirectory, "Images", id.ToString());

                Directory.CreateDirectory(folderPath);

                using var connection = _dbService.CreateConnection();

                string sql = @"INSERT INTO RecipeImage(Path, Name, ContentType, RecipeId)
                               VALUES(@Path, @Name, @ContentType, @RecipeId);";

                foreach (var file in files)
                {
                    string newFilePath = Path.Combine(folderPath, file.FileName);

                    using var inputStream = await file.OpenReadAsync();
                    using var outputStream = File.Create(newFilePath);
                    await inputStream.CopyToAsync(outputStream);

                    var recipeImage = new RecipeImage
                    {
                        Path = newFilePath,
                        Name = file.FileName,
                        ContentType = file.ContentType,

                    };

                    await connection.ExecuteAsync(sql, recipeImage);
                }
                return 1;
            }
            catch (SqliteException ex)
            {
                throw new Exception("Error saving file: " + ex.Message);
            }
        }
    }
}
