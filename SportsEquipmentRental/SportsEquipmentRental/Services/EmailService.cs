using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity;
using SportsEquipmentRental.Shared.Models;

namespace SportsEquipmentRental.Services
{
	public class EmailService : IEmailSender<ApplicationUser>
	{
		private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		// Wysyłanie linku potwierdzającego rejestrację
		public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
		{
			var subject = "Potwierdź swoją rejestrację";
			var message = $@"
                <h1>Witaj, {user.UserName}!</h1>
                <p>Aby potwierdzić swoją rejestrację, kliknij w poniższy link:</p>
                <a href='{confirmationLink}'>Potwierdź rejestrację</a>";

			await SendEmailAsync(email, subject, message);
		}

		// Wysyłanie kodu resetowania hasła
		public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
		{
			var subject = "Kod resetowania hasła";
			var message = $@"
                <h1>Resetowanie hasła</h1>
                <p>Oto Twój kod resetowania hasła:</p>
                <h2>{resetCode}</h2>";

			await SendEmailAsync(email, subject, message);
		}

		// Wysyłanie linku resetowania hasła
		public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
		{
			var subject = "Resetowanie hasła";
			var message = $@"
                <h1>Resetowanie hasła</h1>
                <p>Aby zresetować swoje hasło, kliknij w poniższy link:</p>
                <a href='{resetLink}'>Resetuj hasło</a>";

			await SendEmailAsync(email, subject, message);
		}

		// Uniwersalna metoda wysyłania wiadomości e-mail
		private async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			var fromAddress = new MailAddress(_configuration["EmailSettings:FromAddress"]);
			var fromPassword = _configuration["EmailSettings:FromPassword"];
			var toAddress = new MailAddress(email);

			var smtp = new SmtpClient
			{
				Host = _configuration["EmailSettings:SmtpHost"],
				Port = int.Parse(_configuration["EmailSettings:SmtpPort"]),
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
			};

			using var mailMessage = new MailMessage(fromAddress, toAddress)
			{
				Subject = subject,
				Body = htmlMessage,
				IsBodyHtml = true
			};

			await smtp.SendMailAsync(mailMessage);
		}
	}
}
