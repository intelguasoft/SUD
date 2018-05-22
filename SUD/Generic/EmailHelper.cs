using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace SUD.Generic
{
    public class EmailHelper
    {
        /// <summary>
        /// Permite el envio de correo a un destinatario especifico, de modo asyncronico.
        /// </summary>
        /// <param name="to">A quien enviaremos el correo</param>
        /// <param name="subject">Titulo del corrreo a enviar</param>
        /// <param name="body">Cuerpo del correo a enviar</param>
        /// <returns>Devuelve la manipulacion de la tarea de manera asincronica.</returns>
        public static async Task SendMail(string to, string subject, string body)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(to));
            message.From = new MailAddress(WebConfigurationManager.AppSettings["AdminUser"]);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = WebConfigurationManager.AppSettings["AdminUser"],
                    Password = WebConfigurationManager.AppSettings["AdminPassword"]
                };

                smtp.Credentials = credential;
                smtp.Host = WebConfigurationManager.AppSettings["SMTPName"];
                smtp.Port = int.Parse(WebConfigurationManager.AppSettings["SMTPPort"]);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }

        /// <summary>
        /// Permite el envio de correo a un destinatario especifico, de modo asyncronico.
        /// </summary>
        /// <param name="mails">Lista de correos a quienes enviaremos dicho correo</param>
        /// <param name="subject">Titulo del corrreo a enviar</param>
        /// <param name="body">Cuerpo del correo a enviar</param>
        /// <returns>Devuelve la manipulacion de la tarea de manera asincronica.</returns>
        public static async Task SendMail(List<string> mails, string subject, string body)
        {
            var message = new MailMessage();

            foreach (var to in mails)
            {
                message.To.Add(new MailAddress(to));
            }

            message.From = new MailAddress(WebConfigurationManager.AppSettings["AdminUser"]);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = WebConfigurationManager.AppSettings["AdminUser"],
                    Password = WebConfigurationManager.AppSettings["AdminPassword"]
                };

                smtp.Credentials = credential;
                smtp.Host = WebConfigurationManager.AppSettings["SMTPName"];
                smtp.Port = int.Parse(WebConfigurationManager.AppSettings["SMTPPort"]);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }

    }
}