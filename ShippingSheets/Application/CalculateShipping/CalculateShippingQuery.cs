using MediatR;
using ShippingSheets.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShippingSheets.Application.CalculateShipping
{
    public class CalculateShippingQuery : IRequest<CalculateShippingResult>
    {
        public CalculateShippingQuery(int fromZipCode, Package package)
        {
            FromZipCode = fromZipCode;
            Weight = new Weight(package.Weight);
            Volume = package.Volume;
            PriceCents = package.PriceCents;
        }

        public CalculateShippingQuery(int fromZipCode, int weightInGrams, double volume, int priceCents)
        {
            FromZipCode = fromZipCode;
            Weight = weightInGrams;
            Volume = volume;
            PriceCents = priceCents;
        }

        public int FromZipCode { get; }
        public int Weight { get; }
        public double Volume { get; }
        public int PriceCents { get; }
    }

    public class CalculateShippingQueryHandler : IRequestHandler<CalculateShippingQuery, CalculateShippingResult>
    {
        private readonly IShippingQueries shippingQueries;

        public CalculateShippingQueryHandler(IShippingQueries shippingQueries)
        {
            this.shippingQueries = shippingQueries;
        }

        public async Task<CalculateShippingResult> Handle(CalculateShippingQuery request, CancellationToken cancellationToken)
        {
            var rules = await shippingQueries.ListMatchingRulesAsync(request.FromZipCode, request.Weight, request.Volume);

            var shippings = rules.Select(rule => new ShippingResultItem(
                rule.MethodName,
                GetPrice(rule.PriceCents, request.PriceCents, rule.AdValorem),
                rule.DeliveryTimeDays));

            return new CalculateShippingResult(shippings);
        }

        private decimal GetPrice(int shippingPriceCents, int productPriceCents, double adValorem)
        {
            return (shippingPriceCents / 100m) + (productPriceCents / 100m * (decimal)adValorem);
        }
    }
}
