using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeTracker.Data.Service
{
    public interface IScannerService
    {
        Task<string> ScanAsync();
    }
}
