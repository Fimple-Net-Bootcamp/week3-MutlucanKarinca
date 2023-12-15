using VirtualPetAPI.Entity;

namespace VirtualPetAPI.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;

public class VirtualPetDbContext:DbContext
{
    public VirtualPetDbContext(DbContextOptions<VirtualPetDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
    
    public DbSet<Activity> Activities { get; set; }

    public DbSet<Food> Foods { get; set; }

    public DbSet<Health> Healths { get; set; }

    public DbSet<Pet> Pets { get; set; }

    public DbSet<User> Users { get; set; }
}