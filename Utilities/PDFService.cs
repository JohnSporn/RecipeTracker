using UglyToad.PdfPig;
using PDfPig = UglyToad.PdfPig.Content;

namespace RecipeTracker.Utilities
{
    public static class PDFService
    {
        public static IEnumerable<PDfPig.Word> GetWordsFromPdf(string filePath)
        {
            using (PdfDocument document = PdfDocument.Open(filePath))
            {
                foreach (PDfPig.Page page in document.GetPages())
                {
                    IEnumerable<PDfPig.Word> words = page.GetWords();
                    return words;
                }
            }
            return Enumerable.Empty<PDfPig.Word>();
        }
    }
}
