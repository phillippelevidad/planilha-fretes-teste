using MediatR;
using ShippingSheets.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShippingSheets.Application.CalculateShipping
{
    public class CalculateShippingQuery : IRequest<CalculateShippingResult>
    {
        public CalculateShippingQuery(int toZipCode, Package package)
        {
            ToZipCode = toZipCode;
            Weight = new Weight(package.Weight);
            Volume = package.Volume;
            Price = package.Price;
        }

        public CalculateShippingQuery(int toZipCode, int weightInGrams, double volume, decimal price)
        {
            ToZipCode = toZipCode;
            Weight = weightInGrams;
            Volume = volume;
            Price = price;
        }

        public int ToZipCode { get; }
        public int Weight { get; }
        public double Volume { get; }
        public decimal Price { get; }
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
            var rules = await shippingQueries.ListMatchingRulesAsync(request.ToZipCode, request.Weight, request.Volume);

            var shippings = rules.Select(rule => new ShippingResultItem(
                rule.MethodName,
                GetPrice(rule.PriceCents, request.Price, rule.AdValorem),
                rule.DeliveryTimeDays));

            return new CalculateShippingResult(shippings);
        }

        private decimal GetPrice(int shippingPriceCents, decimal productPrice, double adValorem)
        {
            return (shippingPriceCents / 100m) + (productPrice * (decimal)adValorem);
        }
    }
}
