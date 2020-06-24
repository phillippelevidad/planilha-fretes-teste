using ShippingSheets.Domain;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ShippingSheets.Application
{
    public interface ISheetReader
    {
        Task<ReadOnlyCollection<ShippingRule>> ReadRulesAsync(SheetFile file);
    }
}
