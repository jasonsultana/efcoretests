using EFSamples.Data.Entities;
using Microsoft.EntityFrameworkCore;

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
        optionsBuilder.UseMySql(connectionString, serverVersion);
        
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Customer>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
    
    public async Task SaveChangesAsync() => await base.SaveChangesAsync();
}

public interface IEFSamplesContext
{
    public DbSet<Customer> Customers { get; set; }

    Task SaveChangesAsync();
}