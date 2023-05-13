using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedFalcon.Application.Interfaces.External
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string fromEmail, string fromName, string toEmail, string toName, string subject, string body);
        public Task SendEmailAsync(string toEmail, string toName, string subject, string body);
        public Task SendMultipleEmailAsync(Dictionary<string, string> toRecepients, string subject, string body);
    }
}
