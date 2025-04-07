namespace RecipeTracker.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ServingSize { get; set; }
        public string PrepTime { get; set; }
        public string CookTime { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public int CategoryId { get; set; }
    }
}
