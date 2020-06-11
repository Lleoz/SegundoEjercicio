using Ejercicio2.Api.Transversal.Email.Entities;

namespace Ejercicio2.Api.Transversal.Email.Interfaces
{
    public interface ISmtpClient
    {
        void Send(EmailMessage messageTo);
    }
}
