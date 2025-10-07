using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PB.Clientes.Domain.Clientes.Services;
using PB.Clientes.Infra.Data.Clientes;
using PB.Clientes.Infra.Data.Context;
using PB.Commons.Infra.Kernel.Data;

namespace PB.Clientes.Infra.Data
{
    public static class ServicesExtensions
    {
        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IUnityOfWork, UnityOfWork>();

            services.AddDbContext<PBClientesDBContext>((provider, opt) =>
            {
                opt.UseInMemoryDatabase("memorydatabase");
            });
        }
    }
}
