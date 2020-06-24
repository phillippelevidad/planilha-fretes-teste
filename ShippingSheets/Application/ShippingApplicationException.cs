using System;
using System.Runtime.Serialization;

namespace ShippingSheets.Application
{
    public class ShippingApplicationException : Exception
    {
        public ShippingApplicationException()
        {
        }

        public ShippingApplicationException(string message) : base(message)
        {
        }

        public ShippingApplicationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ShippingApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
