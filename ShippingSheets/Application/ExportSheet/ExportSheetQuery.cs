using MediatR;
using ShippingSheets.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShippingSheets.Application.ExportSheet
{
    public class ExportSheetQuery : IRequest<SheetFile>
    {
        public ExportSheetQuery(string shippingMethodName)
        {
            ShippingMethodName = shippingMethodName;
        }

        public string ShippingMethodName { get; }
    }

    public class ExportSheetQueryHandler : IRequestHandler<ExportSheetQuery, SheetFile>
    {
        private readonly ISheetWriter sheetWriter;
        private readonly IShippingRepository repository;

        public ExportSheetQueryHandler(ISheetWriter sheetWriter, IShippingRepository repository)
        {
            this.sheetWriter = sheetWriter;
            this.repository = repository;
        }

        public async Task<SheetFile> Handle(ExportSheetQuery request, CancellationToken cancellationToken)
        {
            var shipping = await repository.FindAsync(request.ShippingMethodName);

            return await sheetWriter.WriteRulesAsync(shipping.HasValue
                ? shipping.Value : new ShippingMethod(request.ShippingMethodName, Array.Empty<ShippingRule>()));
        }
    }
}
