using MediatR;
using ShippingSheets.Application;
using ShippingSheets.Application.CalculateShipping;
using ShippingSheets.Application.ImportSheet;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PlanilhaFretesTeste
{
    class Program
    {
        static async Task Main()
        {
            var dep = Dependencies.Build();
            var mediator = dep.Resolve<IMediator>();

            // Importa a planilha duas vezes para simular vendedor com várias
            await mediator.Send(new ImportSheetCommand("Planilha 1", GetSheetFile()));
            await mediator.Send(new ImportSheetCommand("Planilha 2", GetSheetFile()));

            // Cria um pacote com alguns produtos
            var package = new Package()
                .AddItem(new PackageItem(10, 300, 20, 20, 2))  // camiseta 1
                .AddItem(new PackageItem(15, 300, 20, 20, 2))  // camiseta 2
                .AddItem(new PackageItem(20, 300, 20, 30, 5)); // moletom

            // Calcula opções de frete
            var shippingResults = await mediator.Send(new CalculateShippingQuery(36420000, package));

            // Apresenta os resultados
            foreach (var result in shippingResults.Items)
                Console.WriteLine("{0} - entrega em {1} dias por {2:C2}", result.MethodName, result.DeliveryDays, result.Price);
        }

        static SheetFile GetSheetFile()
        {
            var bytes = Encoding.UTF8.GetBytes(Resources.sheet);
            return new SheetFile("Sheet.csv", "text/csv", bytes);
        }
    }
}
