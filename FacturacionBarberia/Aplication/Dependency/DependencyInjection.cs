using FacturacionBarberia.Aplication.Interfaces;
using FacturacionBarberia.Aplication.Services;

namespace FacturacionBarberia.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<IUsuario, UsuarioServices>();

            return services;
        }
    }
}