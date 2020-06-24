using CSharpFunctionalExtensions;
using System;
using System.Diagnostics;

namespace ShippingSheets.Domain
{
    [DebuggerDisplay("{Value}")]
    public class Volume : ValueObject<Volume>
    {
        public Volume(int value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Value must be a positive int.");
            Value = value;
        }

        public int Value { get; }

        public static Volume From(double value)
        {
            return new Volume(Convert.ToInt32(Math.Ceiling(value)));
        }

        protected override bool EqualsCore(Volume other) => Value == other.Value;

        protected override int GetHashCodeCore() => Value.GetHashCode();

        public static implicit operator int(Volume volume) => volume.Value;
    }
}
