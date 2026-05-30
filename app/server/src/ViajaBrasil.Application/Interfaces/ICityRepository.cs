using ViajaBrasil.Domain.Entities;
using ViajaBrasil.Domain.Enums;

namespace ViajaBrasil.Application.Interfaces;

public interface ICityRepository
{
    Task<City?> GetByIbgeCodeAsync(string ibgeCode);
    Task<IReadOnlyCollection<City>> GetByStateAsync(State state);
}