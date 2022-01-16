using EFSamples.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFSamples.Data;

public class EFSamplesContext : DbContext, IEFSamplesContext
{
    public EFSamplesContext()
    {
        
    }

    public EFSamplesContext(DbContextOptions<EFSamplesContext> options)
        : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Environment.GetEnvironmentVariable("ef_connectionstring");
        var serverVersion = ServerVersion.AutoDetect(connectionString);

        optionsBuilder
            .UseMySql(connectionString, serverVersion)
            .LogTo(Console.WriteLine)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
        
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Customer>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
    
    public async Task SaveChangesAsync() => await base.SaveChangesAsync();
    
    public EntityEntry Entry<T>(T entity) => base.Entry(entity);
    
    public DbSet<Customer> Customers { get; set; }
}

public interface IEFSamplesContext
{
    public DbSet<Customer> Customers { get; set; }

    Task SaveChangesAsync();

    EntityEntry Entry<T>(T entity);
}