namespace SpaceEngineers.Application.Interfaces;
public interface IUnitOfWork
{
    Task PrepareDatabase();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
