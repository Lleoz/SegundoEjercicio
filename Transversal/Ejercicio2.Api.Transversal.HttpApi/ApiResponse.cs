using System.Net;

namespace Ejercicio2.Api.Transversal.HttpApi
{
    public class ApiResponse<T> : ApiResponse
    {
        public T Result { get; set; }
    }

    public class ApiResponse
    {
        public string Error { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
