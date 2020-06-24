using CSharpFunctionalExtensions;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using ShippingSheets.Application;
using ShippingSheets.Domain;
using ShippingSheets.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShippingSheets.Infrastructure
{
    public class CsvSheet : ISheetReader, ISheetWriter
    {
        public async Task<ReadOnlyCollection<ShippingRule>> ReadRulesAsync(SheetFile file)
        {
            using (var stream = new MemoryStream(file.Content))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = new List<ShippingRuleRecord>();

                await csv.ReadAsync();
                csv.ReadHeader();
                
                while (await csv.ReadAsync()) records.Add(csv.GetRecord<ShippingRuleRecord>());
                return records.Select(record => record.ToShippingRule()).ToReadOnly();
            }
        }

        public async Task<SheetFile> WriteRulesAsync(ShippingMethod shippingMethod)
        {
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                var records = shippingMethod.Rules.Select(rule => ShippingRuleRecord.FromShippingRule(rule));
                csv.WriteHeader<ShippingRuleRecord>();
                await csv.WriteRecordsAsync(records);

                return new SheetFile($"{shippingMethod.Name}.csv", "text/csv", stream.ToArray());
            }
        }

        private class ShippingRuleRecord
        {
            [Index(0)]
            [Default("0")]
            public string ZipCodeOrigin { get; set; }

            [Index(1)]
            [Default("0")]
            public string ZipCodeRangeFrom { get; set; }

            [Index(2)]
            [Default("0")]
            public string ZipCodeRangeTo { get; set; }

            [Index(3)]
            [Default(0)]
            public int MinWeightGrams { get; set; }

            [Index(4)]
            [Default(0)]
            public int MaxWeightGrams { get; set; }

            [Index(5)]
            [Default(10_000_000)]
            public int MaxVolume { get; set; }

            [Index(6)]
            [Default(10)]
            public int DeliveryTimeDays { get; set; }

            [Index(7)]
            public int PriceCents { get; set; }

            [Index(8)]
            public double? AdValorem { get; set; }

            public static ShippingRuleRecord FromShippingRule(ShippingRule rule)
            {
                return new ShippingRuleRecord
                {
                    ZipCodeOrigin = rule.Origin.ToString(),
                    ZipCodeRangeFrom = rule.RangeFrom.ToString(),
                    ZipCodeRangeTo = rule.RangeTo.ToString(),
                    MinWeightGrams = rule.MinWeight,
                    MaxWeightGrams = rule.MaxWeight,
                    MaxVolume = rule.MaxVolume,
                    DeliveryTimeDays = rule.DeliveryTime,
                    PriceCents = rule.Price,
                    AdValorem = rule.AdValorem
                };
            }

            public ShippingRule ToShippingRule()
            {
                return new ShippingRule(
                    ZipCode.Parse(ZipCodeOrigin),
                    ZipCode.Parse(ZipCodeRangeFrom),
                    ZipCode.Parse(ZipCodeRangeTo),
                    new Weight(MinWeightGrams),
                    new Weight(MaxWeightGrams),
                    new Volume(MaxVolume),
                    new DeliveryTime(DeliveryTimeDays),
                    new Price(PriceCents),
                    new Percent(AdValorem ?? 0));
            }
        }
    }
}
