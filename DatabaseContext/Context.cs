using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DatabaseContext;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public DbSet<Credential>? Credential { get; set; }
    
    public DbSet<Credential>? Address { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}