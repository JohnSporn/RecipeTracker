namespace RecipeTracker.Models
{
    public class RecipeImage
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public int RecipeId { get; set; }
    }
}
