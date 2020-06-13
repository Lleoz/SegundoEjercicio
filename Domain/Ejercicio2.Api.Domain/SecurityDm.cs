using Ejercicio2.Api.Domain.Dto;
using Ejercicio2.Api.Domain.Interfaces;
using Ejercicio2.Api.Transversal.Email.Entities;
using Ejercicio2.Api.Transversal.Email.Interfaces;
using System.Text;

namespace Ejercicio2.Api.Domain
{
    public class SecurityDm : ISecurityDm
    {
        private readonly ISmtpClient _smtpClient;
        public SecurityDm(ISmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public void SendPasswordByEmail(UserDto user, string password)
        {
            EmailMessage messageToSend = new EmailMessage
            {
                FullName = user.Name + ' ' + user.LastName,
                Email = user.Email,
                Subject = "Contraseña de acceso Grupo Angular!"
            };
            messageToSend.Body.SubMimeType = ConstsEmail.SUB_MIMETYPE_HTML;
            messageToSend.Body.Text = GenerateHTMLPassWord(password);

            _smtpClient.Send(messageToSend);
        }

        public void SendPasswordByEmail(string name, string email, string password)
        {
            EmailMessage messageToSend = new EmailMessage
            {
                FullName = name,
                Email = email,
                Subject = "Contraseña de acceso Grupo Angular!"
            };
            messageToSend.Body.SubMimeType = ConstsEmail.SUB_MIMETYPE_HTML;
            messageToSend.Body.Text = GenerateHTMLPassWord(password);

            _smtpClient.Send(messageToSend);
        }

        private string GenerateHTMLPassWord(string password)
        {
            StringBuilder sblHtml = new StringBuilder();

            sblHtml.Append("<h2>Muchas gracias por registrarse en el Grupo de Angular!</h2>");
            sblHtml.Append(@"<p style=""font-size: 1.5em;"">Su contraseña para ingresar al sistema es ");
            sblHtml.Append(@"<strong style=""background-color: #317399; padding: 0 5px; color: #fff;"">");
            sblHtml.Append(password);
            sblHtml.Append(@"</strong>");
            sblHtml.Append(@"</p>");
            sblHtml.Append(@"<p>Saludos de parte de todo el equipo!</p>");

            return sblHtml.ToString();
        }
    }
}
