using CSharpFunctionalExtensions;
using MediatR;
using ShippingSheets.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace ShippingSheets.Application.ImportSheet
{
    public class ImportSheetCommand : IRequest<Result>
    {
        public ImportSheetCommand(string shippingMethodName, SheetFile file)
        {
            ShippingMethodName = shippingMethodName;
            File = file;
        }

        public string ShippingMethodName { get; }
        public SheetFile File { get; }
    }

    public class ImportSheetCommandHandler : IRequestHandler<ImportSheetCommand, Result>
    {
        private readonly ISheetReader sheetReader;
        private readonly IShippingRepository repository;

        public ImportSheetCommandHandler(ISheetReader sheetReader, IShippingRepository repository)
        {
            this.sheetReader = sheetReader;
            this.repository = repository;
        }

        public async Task<Result> Handle(ImportSheetCommand request, CancellationToken cancellationToken)
        {
            var rules = await sheetReader.ReadRulesAsync(request.File);
            var method = new ShippingMethod(request.ShippingMethodName, rules);
            return await repository.UpsertAsync(method);
        }
    }
}
