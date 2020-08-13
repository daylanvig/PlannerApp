using System.Threading.Tasks;

namespace PresentationClient.Services
{
    public interface ICacheService
    {
        Task<T> GetItem<T>(string key);
        Task SetItem<T>(string key, T item);
        Task ClearCachedItem(string key);
    }
}