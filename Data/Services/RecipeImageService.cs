using Dapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Data.Sqlite;
using RecipeTracker.Models;

namespace RecipeTracker.Data.Services
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

            if (Directory.Exists(filePath))
            {
                var files = Directory.GetFiles(filePath);

                if(files is null || files.Length == 0)
                {
                    return string.Empty;
                }

                // Read the image file as byte array
                var imageBytes = File.ReadAllBytes(files.FirstOrDefault());
                // Convert to Base64 string
                var base64Image = Convert.ToBase64String(imageBytes);
                // Return the data URL for the image
                return $"data:image/jpeg;base64,{base64Image}";
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<int> RecipeImageInsert(IList<IBrowserFile> images, int id)
        {
            try
            {
                string folderPath = Path.Combine(FileSystem.AppDataDirectory, "Images", id.ToString());

                Directory.CreateDirectory(folderPath);

                using var connection = _dbService.CreateConnection();

                string sql = @"INSERT INTO RecipeImage(Path, Name, ContentType, RecipeId)
                               VALUES(@Path, @Name, @ContentType, @RecipeId);";

                foreach (var image in images)
                {
                    string newFilePath = Path.Combine(folderPath, image.Name);

                    using var inputStream = image.OpenReadStream();
                    using var outputStream = File.Create(newFilePath);
                    await inputStream.CopyToAsync(outputStream);

                    var recipeImage = new RecipeImage
                    {
                        Path = newFilePath,
                        Name = image.Name,
                        ContentType = image.ContentType,
                        RecipeId = id
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
