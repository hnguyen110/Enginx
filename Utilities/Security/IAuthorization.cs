namespace API.Utilities.Security;

public interface IAuthorization
{
    public Task<bool> IsAdministrator(string id);
}