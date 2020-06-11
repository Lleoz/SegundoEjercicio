using Ejercicio2.Api.Transversal.Email.Entities;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;

namespace Ejercicio2.Api.Transversal.Email
{
    public class MailKitSmtpClient : Interfaces.ISmtpClient
    {
        private readonly IConfiguration _configuration;
        public MailKitSmtpClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(EmailMessage messageTo)
        {
            var message = new MimeMessage();

            string fromName, fromEmail;
            fromName = _configuration["Email:From"];
            fromEmail = _configuration["Email:Dir"];

            message.From.Add(new MailboxAddress(fromName, fromEmail));
            message.To.Add(new MailboxAddress(messageTo.FullName, messageTo.Email));
            message.Subject = messageTo.Subject;

            message.Body = new TextPart(messageTo.Body.SubMimeType)
            {
                Text = messageTo.Body.Text
            };

            string SmtpServerUrl;
            int smtpServerPort;
            bool smtpUseSSL, smtpServerRequireAuth;
            SmtpServerUrl = _configuration["SmtpServer:Url"];
            smtpServerPort = Convert.ToInt32(_configuration["SmtpServer:Port"]);
            smtpUseSSL = Convert.ToBoolean(_configuration["SmtpServer:UseSSL"]);
            smtpServerRequireAuth = Convert.ToBoolean(_configuration["SmtpServer:RequireAuth"]);

            using (var client = new SmtpClient())
            {
                client.Connect(SmtpServerUrl, smtpServerPort, smtpUseSSL);

                if (smtpServerRequireAuth)
                {
                    string user, password;
                    user = _configuration["SmtpServer:user"];
                    password = _configuration["SmtpServer:password"];
                    client.Authenticate(user, password);
                }

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
} 
