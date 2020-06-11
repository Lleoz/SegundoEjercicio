using Ejercicio2.Api.Entities;

namespace Ejercicio2.Api.Transversal.Email
{
    public interface ISmtpClient
    {
        void Send(EmailMessage messageTo);
    }
}
