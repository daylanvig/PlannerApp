using ClientApp.Models.HTTP;
using Shared.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Shared.Common;

namespace ClientApp.Extensions
{
    public static class HTTPClientExtensions
    {
        /// <summary>
        /// Post JSON request to server, without throwing if theres a bad request
        /// </summary>
        /// <typeparam name="T">Model type to post</typeparam>
        /// <typeparam name="R">Expected result type</typeparam>
        /// <param name="client">The client.</param>
        /// <param name="URL"></param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static async Task<JSONResponse<R>> PostJSONAsync<T, R>(this HttpClient client, string URL, T model) where R : ServerResponse
        {
            var serializedModel = JsonSerializer.Serialize<T>(model);
            var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(URL, content);
            var response = new JSONResponse<R>
            {
                IsSuccessful = result.IsSuccessStatusCode,
                StatusCode = result.StatusCode
            };
            string stringContent = await result.Content.ReadAsStringAsync();
            try
            {
                response.Response = JsonSerializer.Deserialize<R>(stringContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch
            {
                var serverResponse = Serialization.Deserialize<ServerResponse>(stringContent);
                response.Response = default;
                response.Response.Errors = serverResponse.Errors;
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
