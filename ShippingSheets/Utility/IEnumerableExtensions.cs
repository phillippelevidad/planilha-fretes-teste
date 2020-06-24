using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ShippingSheets.Utility
{
    public static class IEnumerableExtensions
    {
        public static ReadOnlyCollection<T> ToReadOnly<T>(this IEnumerable<T> items) => items.ToList().AsReadOnly();
    }
}
