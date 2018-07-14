using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memberships.Extensions
{
    using System.Configuration;
    using System.Net;
    using System.Net.Mail;

    using Microsoft.AspNet.Identity;

    public static class EmailExtensions
    {
        public static void Send(this IdentityMessage message)
        {
            try
            {
                var password = ConfigurationManager.AppSettings["password"];
                var from = ConfigurationManager.AppSettings["from"];
                var host = ConfigurationManager.AppSettings["host"];
                var port = ConfigurationManager.AppSettings["port"];

                var email = new MailMessage(from, message.Destination, message.Subject, message.Body);
                email.IsBodyHtml = true;

                var smtpClient = new SmtpClient(host, Convert.ToInt32(port));
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(from, password);

                smtpClient.Send(email);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}