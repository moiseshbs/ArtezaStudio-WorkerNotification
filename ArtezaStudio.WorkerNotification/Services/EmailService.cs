using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ArtezaStudio.WorkerNotification.Services
{
    public class EmailService
    {
        private readonly string _apiKey;
        private readonly string _fromEmail;
        private readonly string _fromName;

        public EmailService(IConfiguration config)
        {
            _apiKey = config["SendGrid:ApiKey"];
            _fromEmail = config["SendGrid:FromEmail"];
            _fromName = config["SendGrid:FromName"];
        }

        public async Task EnviarEmailAsync(string para, string assunto, string corpo)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_fromEmail, _fromName);
            var to = new EmailAddress(para);
            var msg = MailHelper.CreateSingleEmail(from, to, assunto, corpo, corpo);
            var response = await client.SendEmailAsync(msg);

            if ((int)response.StatusCode >= 200 && (int)response.StatusCode < 300)
            {
                Console.WriteLine($"E-mail enviado com sucesso para: {para}");
            }
            else
            {
                Console.WriteLine($"Erro ao enviar e-mail para: {para}. Status: {response.StatusCode}");
                var body = await response.Body.ReadAsStringAsync();
                Console.WriteLine($"Detalhes: {body}");
            }
        }
    }
}
