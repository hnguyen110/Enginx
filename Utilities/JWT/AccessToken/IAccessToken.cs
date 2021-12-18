using API.Models;

namespace API.Utilities.JWT.AccessToken;

public interface IAccessToken
{
    string CreateAccessToken(Credential credential);
}