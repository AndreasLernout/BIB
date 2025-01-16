using BIBData.Models;

namespace BIBData.Repositories;

public interface ILenerRepository {
    Lener? Get(int id);
    IEnumerable<Lener> GetAll();
}