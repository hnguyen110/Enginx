using System.Security.Cryptography;
using System.Text;

namespace API.Utilities.Security;

public class Security : ISecurity
{
    public byte[] CreatePasswordSalt()
    {
        return new HMACSHA512().Key;
    }

    public byte[] CreatePasswordHash(string? password, byte[]? salt)
    {
        return new HMACSHA512(salt!).ComputeHash(Encoding.UTF8.GetBytes(password!));
    }

    public bool Compare(string? password, byte[]? salt, byte[]? hash)
    {
        var computedHash = CreatePasswordHash(password, salt);
        return !hash!.Where((character, i) => character != computedHash[i]).Any();
    }
}