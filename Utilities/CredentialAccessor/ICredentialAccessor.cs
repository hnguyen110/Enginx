namespace API.Utilities.CredentialAccessor;

public interface ICredentialAccessor
{
    string RetrieveAccountId();
    string? RetrieveAccountName();
}