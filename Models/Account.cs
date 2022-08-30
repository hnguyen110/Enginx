namespace API.Models;

public enum Role
{
    Administrator,
    Owner,
    Customer
}

public class Account
{
    public string? Id { get; set; }
    public bool Approved { get; set; }

    public Role Role { get; set; }
    public string? Username { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }

    public string? ProfilePicture { get; set; }
    public ProfilePicture? ProfilePictureReference { get; set; }

    public string? Address { get; set; }
    public Address? AddressReference { get; set; }

    public string? ContactInformation { get; set; }
    public ContactInformation? ContactInformationReference { get; set; }

    public string? License { get; set; }
    public License? LicenseReference { get; set; }
    public List<BankCard>? BankCards { get; set; }
    public List<Vehicle>? Vehicles { get; set; }

    public List<Transaction>? TransactionSenders { get; set; }
    public List<Transaction>? TransactionReceivers { get; set; }
    public List<Review>? Reviews { get; set; }
}