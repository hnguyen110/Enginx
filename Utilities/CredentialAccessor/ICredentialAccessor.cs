namespace API.Utilities.CredentialAccessor;

public interface ICredentialAccessor
{
    int RetrieveAccountId();
    string? RetrieveAccountName();
}