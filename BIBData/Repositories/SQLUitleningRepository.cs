using BIBData.Models;
using Microsoft.EntityFrameworkCore;

namespace BIBData.Repositories;

public class SQLUitleningRepository : IUitleningRepository {
    private readonly BIBDbContext context;
    public SQLUitleningRepository(BIBDbContext context) {
        this.context = context;
    }

    public void Add(Uitlening nieuweUitlening) {
        context.Uitleningen.Add(nieuweUitlening); //of context.Add(nieuweUitlening);
        context.SaveChanges();
    }

    public IEnumerable<Uitlening> GetAll() => context.Uitleningen;
    
    public Uitlening? Get(int uitleenId) => context.Uitleningen.Find(uitleenId);

    public void SetReturnDate(int uitleenobjectId, DateTime dateTime) {
        var uitlening = context.Uitleningen
                                .Where(u => u.Tot == null)
                                .Where(u => u.Uitleenobject.Id == uitleenobjectId)
                                .FirstOrDefault();
        if (uitlening != null) {
            uitlening.Tot = dateTime;
            context.SaveChanges();
        }
    }

    public void SetStatus(int uitleenobjectId, Status status) {
        var item = context.Uitleenobjecten.Find(uitleenobjectId);
        if (item != null) {
            item.Status = status;
            context.SaveChanges();
        }
    }

    public Uitlening? GetOpenstaandeUitleningVoorUitleenobject(int uitleenobjectId) {
        return context.Uitleningen
                        .Include(u => u.Uitleenobject)
                        .Include(u => u.Lener)
                        .Where(u => u.Tot == null)
                        .FirstOrDefault(u => u.Uitleenobject.Id == uitleenobjectId);
    }

    public IEnumerable<Uitlening> GetOpenstaandeUitleningenVanLener(int lenerId) {
        return context.Uitleningen
                        .Include(u => u.Lener)
                        .Include(u => u.Uitleenobject)
                        .Where(u => u.Lener.Id == lenerId && u.Tot == null)
                        .OrderBy(u => u.Van);
    }

}
