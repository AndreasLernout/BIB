using BIBData.Models;
using Microsoft.EntityFrameworkCore;

namespace BIBData.Repositories;

public class SQLUitleenobjectRepository : IUitleenobjectRepository {
    private readonly BIBDbContext context;
    public SQLUitleenobjectRepository(BIBDbContext context) {
        this.context = context;
    }

    public Uitleenobject? Get(int id) => context.Uitleenobjecten.Find(id);

    public IEnumerable<Uitleenobject> GetAll() => context.Uitleenobjecten.AsNoTracking();

    public Boek? GetBoek(int id) => context.Boeken.Find(id);

    public Device? GetDevice(int id) => 
        context.Devices
        .Include(d => d.OperatingSysteem)
        .FirstOrDefault(x => x.Id == id);

    public void SetStatus(int uitleenobjectId, Status status) {
        var item = context.Uitleenobjecten.Find(uitleenobjectId);
        if (item != null) {
            item.Status = status;
            context.SaveChanges();
        }
    }
}
