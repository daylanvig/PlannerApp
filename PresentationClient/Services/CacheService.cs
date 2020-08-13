using Blazored.LocalStorage;
using System;
using System.Threading.Tasks;

namespace PresentationClient.Services
{
    public class CacheService : ICacheService
    {
        private class CacheItem<T>
        {
            public T Model { get; set; }
            public DateTime CacheTime { get; set; }
        }

        private readonly ILocalStorageService localStorage;
        private const int CACHEMINUTES = 5;
        public CacheService(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task ClearCachedItem(string key)
        {
            await localStorage.RemoveItemAsync(key);
        }

        public async Task<T> GetItem<T>(string key)
        {
            var item = await localStorage.GetItemAsync<CacheItem<T>>(key);
            if (item == null || (DateTime.Now - item.CacheTime).TotalMinutes > CACHEMINUTES)
            {
                return default;
            }
            return item.Model;
        }

        public async Task SetItem<T>(string key, T item)
        {
            var model = new CacheItem<T>
            {
                Model = item,
                CacheTime = DateTime.Now
            };
            await localStorage.SetItemAsync<CacheItem<T>>(key, model);
        }
    }
}
