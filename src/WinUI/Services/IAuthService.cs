using System.Threading.Tasks;

namespace WinUI.Services
{
    public interface IAuthService
    {
        string GetAccessToken();
        Task<string> LoginAsync(string username, string password);
    }
}