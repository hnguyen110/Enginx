using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DatabaseContext;

public class Context : DbContext
{
    public DbSet<Credential>? Credential { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}