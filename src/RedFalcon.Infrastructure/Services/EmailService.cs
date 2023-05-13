using FluentEmail.Core;
using FluentEmail.Core.Models;
using RedFalcon.Application.Interfaces.External;

namespace RedFalcon.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _email;
        public EmailService(IFluentEmail email)
        {
            _email = email;
        }

        public async Task SendEmailAsync(string fromEmail, string fromName, string toEmail, string toName, string subject, string body)
        {
            _email.Data.FromAddress = new Address() { Name = fromName, EmailAddress = fromEmail };

            await SendEmailAsync(toEmail, toName, subject, body);
        }

        public async Task SendEmailAsync(string toEmail, string toName, string subject, string body)
        {
            _email.Data.ToAddresses = new List<Address>()
            {
                new Address() {Name=toName, EmailAddress=toEmail},
            };

            _email.Data.Subject = subject;
            _email.Data.Body = body;

            await _email.SendAsync();

        }

        public async Task SendMultipleEmailAsync(Dictionary<string, string> toRecepients, string subject, string body)
        {
            foreach (var recepient in toRecepients)
            {
                _email.Data.ToAddresses.Add(new Address { Name = recepient.Value, EmailAddress = recepient.Key });
            }

            _email.Data.Subject = subject;
            _email.Data.Body = body;

            await _email.SendAsync();

        }
    }
}
