using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MassTransit.Services
{
    public static class EmailService
    {
        public static async Task Send(string email, string key)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(email);

                message.Subject = "Chegou sua key!!!";
                message.Body = $"Sua chave está aqui {key}";

                using (var smtpClient = new SmtpClient())
                {
                    await smtpClient.SendMailAsync(message);
                }
            }
            catch (Exception)
            {
                throw;
            }
         
        }


    }
}
