using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace ShippingSheets.Domain
{
    [DebuggerDisplay("{Message}")]
    public class ShippingDomainException : Exception
    {
        public ShippingDomainException()
        {
        }

        public ShippingDomainException(string message) : base(message)
        {
        }

        public ShippingDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ShippingDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
