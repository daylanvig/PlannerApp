using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services
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
