using Shared.Common;
using System.Net;

namespace ClientApp.Models.HTTP
{
    public class JSONResponse<T> where T : ServerResponse
    {
        public JSONResponse() { }
        public JSONResponse(T response)
        {
            Response = response;
        }
        public T Response { get; set; }
        public bool IsSuccessful { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
