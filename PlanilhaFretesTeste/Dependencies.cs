using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShippingSheets.Application;
using ShippingSheets.Domain;
using ShippingSheets.Infrastructure;
using System;

namespace PlanilhaFretesTeste
{
    public class Dependencies
    {
        private readonly IServiceProvider provider;

        public T Resolve<T>() => provider.GetService<T>();

        private Dependencies(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public static Dependencies Build()
        {
            var services = new ServiceCollection()
                .AddMediatR(typeof(ShippingMethod).Assembly)
                .AddScoped<IShippingQueries, MockShippingQueries>()
                .AddScoped<IShippingRepository, MockShippingRepository>()
                .AddTransient<ISheetReader, CsvSheet>()
                .AddTransient<ISheetWriter, CsvSheet>();

            return new Dependencies(services.BuildServiceProvider());
        }
    }
}
