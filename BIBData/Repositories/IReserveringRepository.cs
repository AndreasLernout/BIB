using BIBData.Models;

namespace BIBData.Repositories;

public interface IReserveringRepository {
    void Add(Reservering reservering);
    IEnumerable<Reservering> GetReserveringenVoorUitleenobject(int uitleenobjectId);
    bool IsGereserveerd(int uitleenobjectId);
    void VerwijderReservering(int itemId, int lenerId);
    IEnumerable<Reservering> GetReserveringenVanLener(int lenerId);
}