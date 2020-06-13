using Ejercicio2.Api.Transversal.AuthJwt.Session;
using Microsoft.AspNetCore.Mvc;

namespace Ejercicio2.Api.Transversal.AuthJwt.Controllers
{
    public abstract class AuthorizationController : Controller
    {
        public IAppSession AppSession { get { return new AppSession(HttpContext); } }
    }
}
