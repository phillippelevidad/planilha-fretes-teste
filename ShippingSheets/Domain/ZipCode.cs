using CSharpFunctionalExtensions;
using ShippingSheets.Utility;
using System;
using System.Diagnostics;

namespace ShippingSheets.Domain
{
    [DebuggerDisplay("{Value}")]
    public class ZipCode : ValueObject<ZipCode>
    {
        public ZipCode(int value)
        {
            if (value < 0) throw new ShippingDomainException("The value for a ZipCode must be a positive int.");
            Value = value;
        }

        public int Value { get; }

        public static ZipCode Parse(string value)
        {
            if (!TryParse(value, out ZipCode zipCode)) throw new ArgumentException($"Value {value} is not valid as a ZipCode.");
            return zipCode;
        }

        public static bool TryParse(string value, out ZipCode zipCode)
        {
            zipCode = null;
            value = (value ?? "").OnlyNumbers();
            if (!int.TryParse(value, out int parsedInt)) return false;
            zipCode = new ZipCode(parsedInt);
            return true;
        }

        public bool IsInRange(ZipCode from, ZipCode to) => (from == null || Value >= from.Value) && (to == null || Value <= to.Value);

        protected override bool EqualsCore(ZipCode other) => Value == other.Value;

        protected override int GetHashCodeCore() => Value.GetHashCode();

        public static implicit operator int(ZipCode zipCode) => zipCode.Value;
    }
}
