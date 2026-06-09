namespace FacturacionBarberia.Infraestructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
