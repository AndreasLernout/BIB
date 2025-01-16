using BIBData.Models;
using Microsoft.EntityFrameworkCore;
namespace BIBData.Repositories;

public class SQLLenerRepository : ILenerRepository {

    private readonly BIBDbContext context;
    public SQLLenerRepository(BIBDbContext context) {
        this.context = context;
    }

    public Lener? Get(int id) => context.Leners.Find(id);

    public IEnumerable<Lener> GetAll() => context.Leners.AsNoTracking();
}
