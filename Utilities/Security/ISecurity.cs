namespace API.Utilities.Security;

public interface ISecurity
{
    byte[] CreatePasswordSalt();

    byte[] CreatePasswordHash(string? password, byte[]? salt);

    bool Compare(string? password, byte[]? salt, byte[]? hash);
}