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
    public DbSet<VehiclePicture>? VehiclePicture { get; set; }
    public DbSet<Reservation>? Reservation { get; set; }
    public DbSet<Transaction>? Transaction { get; set; }
    public DbSet<Review>? Review { get; set; }
    public DbSet<Insurance>? Insurance { get; set; }

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

        builder.Entity<VehiclePicture>()
            .HasOne(e => e.VehicleReference)
            .WithMany(e => e.VehiclePictures)
            .HasForeignKey(e => e.Vehicle)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<BankCard>()
            .HasOne(e => e.AccountReference)
            .WithMany(e => e.BankCards)
            .HasForeignKey(e => e.Account)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Vehicle>()
            .HasOne(e => e.OwnerReference)
            .WithMany(e => e.Vehicles)
            .HasForeignKey(e => e.Owner)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Reservation>()
            .HasOne(e => e.VehicleReference)
            .WithMany(e => e.Reservations)
            .HasForeignKey(e => e.Vehicle)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Reservation>()
            .HasOne(e => e.InsuranceReference)
            .WithMany(e => e.Reservations)
            .HasForeignKey(e => e.Insurance)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Transaction>()
            .HasOne(e => e.Reservation)
            .WithOne(e => e.TransactionReference)
            .HasForeignKey<Reservation>(e => e.Transaction)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Transaction>()
            .HasOne(e => e.SenderReference)
            .WithMany(e => e.TransactionSenders)
            .HasForeignKey(e => e.Sender)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Transaction>()
            .HasOne(e => e.ReceiverReference)
            .WithMany(e => e.TransactionReceivers)
            .HasForeignKey(e => e.Receiver)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Review>()
            .HasOne(e => e.ReviewerReference)
            .WithMany(e => e.Reviews)
            .HasForeignKey(e => e.Reviewer)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Review>()
            .HasOne(e => e.VehicleReference)
            .WithMany(e => e.Reviews)
            .HasForeignKey(e => e.Vehicle)
            .OnDelete(DeleteBehavior.Cascade);
    }
}