using CSharpFunctionalExtensions;
using ShippingSheets.Application;
using ShippingSheets.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PlanilhaFretesTeste
{
    class MockShippingRepository : IShippingRepository
    {
        private readonly List<ShippingMethod> methods = new List<ShippingMethod>();

        public Task<Maybe<ShippingMethod>> FindAsync(string name)
        {
            var method = methods.FirstOrDefault(m => m.Name == name);
            var maybe = method == null ? Maybe<ShippingMethod>.None : Maybe<ShippingMethod>.From(method);
            return Task.FromResult(maybe);
        }

        public Task<Result> RemoveAsync(string name)
        {
            var found = methods.Where(m => m.Name == name);
            foreach (var method in found) methods.Remove(method);
            return Task.FromResult(Result.Ok());
        }

        public Task<Result> UpsertAsync(ShippingMethod shippingMethod)
        {
            if (methods.Contains(shippingMethod))
                methods.Remove(shippingMethod);
            methods.Add(shippingMethod);
            return Task.FromResult(Result.Ok());
        }

        internal ReadOnlyCollection<ShippingMethod> GetAllMethods() => methods.AsReadOnly();
    }
}
