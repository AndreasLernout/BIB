using Microsoft.EntityFrameworkCore;

namespace BIBData.Models; 
public class BIBDbContext : DbContext {
    
    public DbSet<Lener> Leners { get; set; }
    public DbSet<Uitleenobject> Uitleenobjecten { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Boek> Boeken { get; set; }
    public DbSet<Reservering> Reserveringen { get; set; }
    public DbSet<Uitlening> Uitleningen { get; set; }
    public DbSet<OperatingSysteem> Operatingsystemen { get; set; }

    public BIBDbContext() { }
    public BIBDbContext(DbContextOptions options) : base(options) { }
}
