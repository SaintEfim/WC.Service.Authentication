namespace WC.Service.Authentication.Domain.Helpers;

public interface IHashHelper
{
    public string Hash(string data);
    public bool Verify(string data, string hashedData);
}