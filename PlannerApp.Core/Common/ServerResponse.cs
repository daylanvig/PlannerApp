using System.Collections.Generic;

namespace Shared.Common
{
    public class ServerResponse
    {
        public IEnumerable<KeyValuePair<string, string>> Errors { get; set; }
    }
}
