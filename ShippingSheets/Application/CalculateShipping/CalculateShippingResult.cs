using ShippingSheets.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ShippingSheets.Application.CalculateShipping
{
    public class CalculateShippingResult
    {
        public CalculateShippingResult(IEnumerable<ShippingResultItem> items)
        {
            Items = (items ?? Array.Empty<ShippingResultItem>()).ToReadOnly();
        }

        public ReadOnlyCollection<ShippingResultItem> Items { get; }
    }

    public class ShippingResultItem
    {
        public ShippingResultItem(string methodName, decimal price, int deliveryDays)
        {
            MethodName = methodName;
            Price = price;
            DeliveryDays = deliveryDays;
        }

        public string MethodName { get; }
        public decimal Price { get; }
        public int DeliveryDays { get; }
    }
}
