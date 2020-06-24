using CSharpFunctionalExtensions;
using System.Diagnostics;

namespace ShippingSheets.Domain
{
    [DebuggerDisplay("{Cents} cents")]
    public class Price : ValueObject<Price>
    {
        public Price(int cents)
        {
            if (cents < 0) throw new ShippingDomainException("Cents must be a positive int.");
            Cents = cents;
        }

        public int Cents { get; }

        protected override bool EqualsCore(Price other) => Cents == other.Cents;

        protected override int GetHashCodeCore() => Cents.GetHashCode();

        public static implicit operator int(Price price) => price.Cents;
    }
}
