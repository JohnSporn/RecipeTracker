using Microsoft.Maui.Controls;
using Microsoft.UI.Xaml.Controls;
using RecipeTracker.Utilities;
using Windows.Devices.Enumeration;
using Windows.Devices.Scanners;
using Windows.Storage;

namespace RecipeTracker.Data.Services
{
    public class WindowsScannerService : IScannerService
    {
        public async Task<string> ScanAsync()
        {
            var selector = ImageScanner.GetDeviceSelector();
            var devices = await DeviceInformation.FindAllAsync(selector);

            if (devices.Count == 0)
            {
                return "No scanner found";
            }

            var scanner = await ImageScanner.FromIdAsync(devices[0].Id);

            if (!scanner.IsScanSourceSupported(ImageScannerScanSource.Flatbed))
            {
                return "Scanner does not support flatbed scanning";
            }

            ImageScannerScanResult path = await scanner.ScanFilesToFolderAsync(ImageScannerScanSource.Flatbed, await StorageFolder.GetFolderFromPathAsync(@"C:\ScannedImages"));

            /* If file extension is PDF
                    PDFService.GetWordsFromPdf(path.ScannedFiles.FirstOrDefault()!.Path);
                else if file extension is image
            */

            ImageService.GetImageText(path.ScannedFiles.FirstOrDefault()!.Path);

            return path.ScannedFiles.FirstOrDefault()?.Path ?? "No file scanned";
        }
    }   
}
