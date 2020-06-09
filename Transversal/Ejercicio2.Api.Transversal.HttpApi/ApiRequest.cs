namespace Ejercicio2.Api.Transversal.HttpApi
{
    public class ApiRequest<T> where T : class
    {
        public T Data { get; set; }
    }
}
