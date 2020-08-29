using System.Text.Json;

namespace Shared.Common
{
    public class Serialization
    {

        public static T Deserialize<T>(string content)
        {
            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
