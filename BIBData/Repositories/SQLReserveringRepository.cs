using BIBData.Models;
using Microsoft.EntityFrameworkCore;

namespace BIBData.Repositories;

public class SQLReserveringRepository : IReserveringRepository {
    
    private readonly BIBDbContext context;
    
    public SQLReserveringRepository(BIBDbContext context) {
        this.context = context;
    }

    public void Add(Reservering reservering) {
        context.Reserveringen.Add(reservering);
        context.SaveChanges();
    }

    public IEnumerable<Reservering> GetReserveringenVoorUitleenobject(int uitleenobjectId) {
        return context.Reserveringen
                        .Include(r => r.Lener)
                        .Where(r => r.Uitleenobject.Id == uitleenobjectId)
                        .OrderBy(r => r.GereserveerdOp);
    }

    public bool IsGereserveerd(int uitleenobjectId) {
        var reservering = context.Reserveringen
                                    .Include(r => r.Uitleenobject)
                                    .FirstOrDefault(r => r.Uitleenobject.Id == uitleenobjectId);
        return reservering != null;
    }

    public void VerwijderReservering(int itemId, int lenerId) {
        var reservering = context.Reserveringen
                                    .Include(r => r.Uitleenobject)
                                    .Include(r => r.Lener)
                                    .FirstOrDefault(r => r.Uitleenobject.Id == itemId && r.Lener.Id == lenerId);
        if (reservering != null) {
            context.Remove(reservering);
            context.SaveChanges();
        }
    }

    public IEnumerable<Reservering> GetReserveringenVanLener(int lenerId) {
        return context.Reserveringen
                        .Include(r => r.Lener)
                        .Include(r => r.Uitleenobject)
                        .Where(r => r.Lener.Id == lenerId)
                        .OrderBy(r => r.GereserveerdOp);
    }
}
