using FacturacionBarberia.Domain.Models.Entities;
using FacturacionBarberia.Infraestructure.PatronRepository.GenericRepository;
using FacturacionBarberia.Infraestructure.UnitOfWork;

namespace FacturacionBarberia.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped(
                typeof(IRepository<>),
                typeof(Repository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}