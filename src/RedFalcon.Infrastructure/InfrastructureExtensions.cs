﻿using FluentEmail.MailKitSmtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedFalcon.Application.Interfaces.Data;
using RedFalcon.Application.Interfaces.External;
using RedFalcon.Infrastructure.Data;
using RedFalcon.Infrastructure.Data.Context;
using RedFalcon.Infrastructure.Data.Repositories;
using RedFalcon.Infrastructure.Services;

namespace RedFalcon.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySQL(configuration.GetConnectionString("DbConnection") ?? "");
            });

            services.AddScoped<DatabaseSession>();

            services.AddTransient<IContactRepository, ContactRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var senderName = configuration.GetSection("Email:Sender:Name").Value;
            var senderEmail = configuration.GetSection("Email:Sender:Email").Value;
            var smtpServer = configuration.GetSection("Email:Smtp:Server").Value;
            var smtpPort = int.Parse(configuration.GetSection("Email:Smtp:Port").Value ?? "465");
            var smtpUser = configuration.GetSection("Email:Smtp:User").Value;
            var smtpPassword = configuration.GetSection("Email:Smtp:Password").Value;
            var smtpAuth = configuration.GetSection("Email:Smtp:RequiresAuthentication").Value == "true";
            var smtpSsl = configuration.GetSection("Email:Smtp:UseSsl").Value == "true";
            services.AddFluentEmail(senderEmail, senderName)
            .AddMailKitSender(new SmtpClientOptions()
            {
                Server = smtpServer,
                Port = smtpPort,
                User = smtpUser,
                Password = smtpPassword,
                RequiresAuthentication = smtpAuth,
                UseSsl = smtpSsl,
            });

            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
