using CSharpFunctionalExtensions;
using ShippingSheets.Domain;
using System.Threading.Tasks;

namespace ShippingSheets.Application
{
    public interface IShippingRepository
    {
        Task<Maybe<ShippingMethod>> FindAsync(string name);

        Task<Result> RemoveAsync(string name);

        Task<Result> UpsertAsync(ShippingMethod shippingMethod);
    }
}
