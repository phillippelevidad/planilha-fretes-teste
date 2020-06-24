using CSharpFunctionalExtensions;
using System.Diagnostics;

namespace ShippingSheets.Domain
{
    [DebuggerDisplay("{Origin} - from {RangeFrom} to {RangeTo}")]
    public class ShippingRule : ValueObject<ShippingRule>
    {
        public ShippingRule(ZipCode origin, ZipCode rangeFrom, ZipCode rangeTo, Weight minWeight, Weight maxWeight, Volume maxVolume, DeliveryTime deliveryTime, Price price, Percent adValorem)
        {
            Origin = origin;
            RangeFrom = rangeFrom;
            RangeTo = rangeTo;
            MinWeight = minWeight;
            MaxWeight = maxWeight;
            MaxVolume = maxVolume;
            DeliveryTime = deliveryTime;
            Price = price;
            AdValorem = adValorem;
        }

        public ZipCode Origin { get; }
        public ZipCode RangeFrom { get; }
        public ZipCode RangeTo { get; }
        public Weight MinWeight { get; }
        public Weight MaxWeight { get; }
        public Volume MaxVolume { get; }
        public DeliveryTime DeliveryTime { get; }
        public Price Price { get; }
        public Percent AdValorem { get; }

        protected override bool EqualsCore(ShippingRule other) => (Origin, RangeFrom, RangeTo, MinWeight, MaxWeight) == (other.Origin, other.RangeFrom, other.RangeTo, other.MinWeight, other.MaxWeight);

        protected override int GetHashCodeCore() => (Origin, RangeFrom, RangeTo, MinWeight, MaxWeight).GetHashCode();
    }
}
