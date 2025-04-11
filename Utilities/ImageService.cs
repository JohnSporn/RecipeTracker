using Tesseract;

namespace RecipeTracker.Utilities
{
    public static class ImageService
    {
        public static string GetImageText(string filePath)
        {
            using (var engine = new TesseractEngine("tessdata", "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(filePath))
                {
                    using (var page = engine.Process(img))
                    {
                        var text = page.GetText();
                        Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                        using (var itr = page.GetIterator())
                        {
                            itr.Begin();
                            do
                            {
                                var line = itr.GetText(PageIteratorLevel.TextLine);
                            } while (itr.Next(PageIteratorLevel.TextLine));
                        }
                        return text;
                    }
                }
            }
        }
    }
}
