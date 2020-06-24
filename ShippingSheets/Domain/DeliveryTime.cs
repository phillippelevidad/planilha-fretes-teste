using CSharpFunctionalExtensions;
using System.Diagnostics;

namespace ShippingSheets.Domain
{
    [DebuggerDisplay("{Days} days")]
    public class DeliveryTime : ValueObject<DeliveryTime>
    {
        public DeliveryTime(int days)
        {
            if (days < 0) throw new ShippingDomainException("The days for a DeliveryDate must be a positive int.");
            Days = days;
        }

        public int Days { get; }

        protected override bool EqualsCore(DeliveryTime other) => Days == other.Days;

        protected override int GetHashCodeCore() => Days.GetHashCode();

        public static implicit operator int(DeliveryTime deliveryTime) => deliveryTime.Days;
    }
}
