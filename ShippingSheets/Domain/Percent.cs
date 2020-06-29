using CSharpFunctionalExtensions;
using System.Diagnostics;

namespace ShippingSheets.Domain
{
    [DebuggerDisplay("{Value}")]
    public class Percent : ValueObject<Percent>
    {
        public static readonly Percent Zero = new Percent(0);

        public Percent(double value)
        {
            Value = value;
        }

        public double Value { get; }

        protected override bool EqualsCore(Percent other) => Value == other.Value;

        protected override int GetHashCodeCore() => Value.GetHashCode();

        public static implicit operator double(Percent percent) => percent.Value;
    }
}
