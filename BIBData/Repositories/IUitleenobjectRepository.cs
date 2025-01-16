using BIBData.Models;

namespace BIBData.Repositories;

public interface IUitleenobjectRepository {
    Uitleenobject? Get(int id);
    IEnumerable<Uitleenobject> GetAll();
    Boek? GetBoek(int id);
    Device? GetDevice(int id);
    void SetStatus(int uitleenobjectId, Status status);
}
