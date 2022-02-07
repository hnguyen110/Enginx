using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DatabaseContext;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public DbSet<Account>? Account { get; set; }
    public DbSet<Address>? Address { get; set; }
    public DbSet<ContactInformation>? ContactInformation { get; set; }
    public DbSet<License>? License { get; set; }
    public DbSet<ProfilePicture>? ProfilePicture { get; set; }
    public DbSet<BankCard>? BankCard { get; set; }
    public DbSet<Vehicle>? Vehicle { get; set; }
    
    public DbSet<Reservation>? Reservation { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Address>()
            .HasOne(e => e.Account)
            .WithOne(e => e.AddressReference)
            .HasForeignKey<Account>(e => e.Address);

        builder.Entity<ContactInformation>()
            .HasOne(e => e.Account)
            .WithOne(e => e.ContactInformationReference)
            .HasForeignKey<Account>(e => e.ContactInformation);

        builder.Entity<ProfilePicture>()
            .HasOne(e => e.Account)
            .WithOne(e => e.ProfilePictureReference)
            .HasForeignKey<Account>(e => e.ProfilePicture);

        builder.Entity<License>()
            .HasOne(e => e.Account)
            .WithOne(e => e.LicenseReference)
            .HasForeignKey<Account>(e => e.License);

        builder.Entity<BankCard>()
            .HasOne(e => e.AccountReference)
            .WithMany(e => e.BankCards)
            .HasForeignKey(e => e.Account);

        builder.Entity<Vehicle>()
            .HasOne(e => e.OwnerReference)
            .WithMany(e => e.Vehicles)
            .HasForeignKey(e => e.Owner);
        
        builder.Entity<Reservation>()
            .HasOne(e => e.VehicleReference)
            .WithMany(e => e.Reservations)
            .HasForeignKey(e => e.Vehicle);
    }
}