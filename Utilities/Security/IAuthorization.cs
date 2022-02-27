namespace API.Utilities.Security;

public interface IAuthorization
{
    public Task<bool> IsAdministrator();
    public Task<bool> IsOwner();
}