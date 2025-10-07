namespace PB.Clientes.Api
{
    public static class ServicesExtensions
    {
        public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Domain.ServicesExtensions).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application.ServicesExtensions).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Infra.Data.ServicesExtensions).Assembly));
        }
    }
}
