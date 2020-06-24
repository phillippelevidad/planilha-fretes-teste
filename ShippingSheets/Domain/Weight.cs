using CSharpFunctionalExtensions;
using System.Diagnostics;

namespace ShippingSheets.Domain
{
    [DebuggerDisplay("{Grams} grams")]
    public class Weight : ValueObject<Weight>
    {
        public Weight(int grams)
        {
            if (grams < 0) throw new ShippingDomainException("The grams for a Weight must be a positive int.");
            Grams = grams;
        }

        public int Grams { get; }

        public bool IsInRange(Weight min, Weight max) => (min == null || Grams >= min.Grams) && (max == null || Grams <= max.Grams);

        protected override bool EqualsCore(Weight other) => Grams == other.Grams;

        protected override int GetHashCodeCore() => Grams.GetHashCode();

        public static implicit operator int(Weight weight) => weight.Grams;
    }
}
