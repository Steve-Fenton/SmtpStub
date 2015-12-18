using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Mail;

namespace SmtpTest
{
    [TestClass]
    public class SmtpTest
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void SendMessage()
        {
            var guid = Guid.NewGuid().ToString();

            using (var message = new MailMessage("test@example.com", "user@example.com"))
            using (var client = new SmtpClient())
            {
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "localhost";

                message.Subject = "This is a test email.";
                message.Body = $"This is my test email body {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}.";
                message.Headers.Add("Message-Id", $"<{guid}@localhost>");

                client.Send(message);
            }
        }
    }
}
