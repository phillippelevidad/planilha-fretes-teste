using ShippingSheets.Domain;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ShippingSheets.Application
{
    public interface IShippingQueries
    {
        Task<ReadOnlyCollection<ShippingRuleDto>> ListMatchingRulesAsync(int fromZipCode, int weightGrams, double volume);
    }

    public class ShippingRuleDto
    {
        public string MethodName { get; set; }
        public int ZipCodeOrigin { get; set; }
        public int ZipCodeRangeFrom { get; set; }
        public int ZipCodeRangeTo { get; set; }
        public int MinWeightGrams { get; set; }
        public int MaxWeightGrams { get; set; }
        public int MaxVolume { get; set; }
        public int DeliveryTimeDays { get; set; }
        public int PriceCents { get; set; }
        public double AdValorem { get; set; }
    }
}
