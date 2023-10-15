namespace Mango.Web.Service.Interface;

public interface ITokenProvider
{
    void SetToken(string token);
    string? GetToken();
    void ClearToken();
}
