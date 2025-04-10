using Microsoft.Maui.Controls;
using Microsoft.UI.Xaml.Controls;
using System.Threading;
using System;
using Windows.Devices.Enumeration;
using Windows.Devices.Scanners;
using Windows.Storage;

namespace RecipeTracker.Data.Services
{
    public class WindowsScannerService : IScannerService
    {
        public async Task<string?> ScanAsync()
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
            if(!scanner.IsScanSourceSupported(ImageScannerScanSource.Feeder))
            {
                return "Scanner does not support document feeder scanning";
            }

            var result = await scanner.ScanFilesToFolderAsync(ImageScannerScanSource.Flatbed, await StorageFolder.GetFolderFromPathAsync(@"C:\ScannedImages"));

            return result.ScannedFiles.FirstOrDefault()?.Path ?? "No file scanned";
        }
    }   
}
