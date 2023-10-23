using System.Net.Mail;
using System.Net;
using System.Text;

namespace Scholarit.Utils
{
    public static class EmailHelper
    {

        public static void sendEmail(string email, string subject, string body)
        {

            IConfiguration config = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json")
                                    .Build();
            string myEmail = config.GetSection("email")["myMail"];
            string pass = config.GetSection("email")["pass"];
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(myEmail, pass),
                EnableSsl = true,
            };

            var message = new MailMessage
            {
                From = new MailAddress(myEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            message.To.Add(email);

            try
            {
                smtpClient.Send(message);
                Console.WriteLine("Email đã được gửi thành công.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Lỗi: " + e.Message);
            }
        }

        public static string GenerateOTP(int length)
        {
            const string characters = "0123456789";
            Random random = new Random();
            var otp = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                otp.Append(characters[random.Next(characters.Length)]);
            }

            return otp.ToString();
        }
    }
}
