using ShippingSheets.Domain;
using System.IO;
using System.Threading.Tasks;

namespace ShippingSheets.Application
{
    public interface ISheetWriter
    {
        Task<SheetFile> WriteRulesAsync(ShippingMethod shippingMethod);
    }
}
