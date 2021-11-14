using System.Threading.Tasks;

namespace WinUI.Services
{
    public interface IApiService
    {
        Task<T> GetAsync<T>(string path);
    }
}