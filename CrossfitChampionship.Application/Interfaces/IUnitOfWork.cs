namespace CrossfitChampionship.Application.Interfaces;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
    Task RollbackAsync();
}
