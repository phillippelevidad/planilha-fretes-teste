using ShippingSheets.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShippingSheets.Domain
{
    [DebuggerDisplay("{Name}")]
    public class ShippingMethod
    {
        public ShippingMethod(string name, IEnumerable<ShippingRule> rules)
        {
            Name = name;
            Rules = rules.ToReadOnly();
        }

        public string Name { get; }
        public ReadOnlyCollection<ShippingRule> Rules { get; }
    }
}
