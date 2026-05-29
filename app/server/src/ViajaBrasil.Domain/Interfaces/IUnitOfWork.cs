namespace ViajaBrasil.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<bool> Commit();
}