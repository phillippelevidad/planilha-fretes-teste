using ShippingSheets.Application;
using ShippingSheets.Domain;
using ShippingSheets.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PlanilhaFretesTeste
{
    public class MockShippingQueries : IShippingQueries
    {
        private readonly MockShippingRepository repository;

        public MockShippingQueries(IShippingRepository repository)
        {
            this.repository = repository as MockShippingRepository ?? throw new ArgumentException("For the purposes of testing, MockShippingQueries requires an instance of MockingShippingRepository.", nameof(repository));
        }

        public Task<ReadOnlyCollection<ShippingRuleDto>> ListMatchingRulesAsync(int fromZipCode, int weightGrams, double volume)
        {
            var origin = new ZipCode(fromZipCode);
            var weight = new Weight(weightGrams);

            var items = repository.GetAllMethods()
                .SelectMany(method => method.Rules.Select(rule => new KeyValuePair<string, ShippingRule>(method.Name, rule)))
                .Where(item => origin.IsInRange(item.Value.RangeFrom, item.Value.RangeTo))
                .Where(item => weight.IsInRange(item.Value.MinWeight, item.Value.MaxWeight))
                .Where(item => volume <= item.Value.MaxVolume)
                .Select(item => new ShippingRuleDto
                {
                    MethodName = item.Key,
                    ZipCodeOrigin = item.Value.Origin,
                    ZipCodeRangeFrom = item.Value.RangeFrom,
                    ZipCodeRangeTo = item.Value.RangeTo,
                    MinWeightGrams = item.Value.MinWeight,
                    MaxWeightGrams = item.Value.MaxWeight,
                    MaxVolume = item.Value.MaxVolume,
                    DeliveryTimeDays = item.Value.DeliveryTime,
                    PriceCents = item.Value.Price,
                    AdValorem = item.Value.AdValorem
                });

            return Task.FromResult(items.ToReadOnly());
        }
    }
}
