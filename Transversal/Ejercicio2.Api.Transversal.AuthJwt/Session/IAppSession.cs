using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Ejercicio2.Api.Transversal.AuthJwt.Session
{
    public interface IAppSession
    {
        TValue GetPropertyValue<TValue>(string propertyName);
    }

    internal class AppSession : IAppSession
    {
        private HttpContext _httpContext { get; }

        public AppSession(HttpContext httpContext)
        {
            this._httpContext = httpContext;
        }

        public TValue GetPropertyValue<TValue>(string propertyName)
        {
            var claims = this._httpContext.User.Claims;
            var value = claims.Where(x => x.Type == propertyName).Select(x => x.Value).FirstOrDefault();
            return this.GetValue<TValue>(value);
        }

        private T GetValue<T>(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
