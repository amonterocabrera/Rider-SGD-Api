using Microsoft.AspNetCore.Hosting;
using SGDPEDIDOS.Application.DTOs.Settings;
using SGDPEDIDOS.Application.Exceptions;
using SGDPEDIDOS.Application.Interfaces.Services;
using SGDPEDIDOS.Application.Wrappers;
using System.Net.Mail;

namespace SGDPEDIDOS.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly IWebHostEnvironment _hostEnviroment;
        public EmailService(EmailSettings emailSettings, IWebHostEnvironment hostEnviroment)
        {
            _emailSettings = emailSettings;
            _hostEnviroment = hostEnviroment;
        }
        public Response<string> Send(string To,
                                 string Subject,
                                 string Body,
                                 bool IsHTMLBody,
                                 string HTMLTittle, string HTMLInfo, string nombreHtml, string boton = null)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(To);
            msg.From = new MailAddress(_emailSettings.From, _emailSettings.From);
            msg.Subject = Subject;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            if (IsHTMLBody && _emailSettings.HTMLTemplateRoute.Length > 0)
            {
                msg.IsBodyHtml = true;
                string route = _hostEnviroment.ContentRootPath + _emailSettings.HTMLTemplateRoute + nombreHtml;
                string text = System.IO.File.ReadAllText(@"" + route + "");
                text = text.Replace("{{Tittle}}", HTMLTittle);
                if (boton != null)
                {
                    text = text.Replace("{{boton}}", boton);
                }
                text = text.Replace("{{Info}}", HTMLInfo);
                msg.Body = text;
            }
            else
            {
                msg.IsBodyHtml = false;
                msg.Body = Body;
                msg.BodyEncoding = System.Text.Encoding.Unicode;
            }
            try
            {
                using (var client = new SmtpClient())
                {
                    client.UseDefaultCredentials = false;
                    client.Timeout = 50000;
                    client.Credentials = new System.Net.NetworkCredential(_emailSettings.From, _emailSettings.Password);
                    client.Port = _emailSettings.Port;
                    client.Host = _emailSettings.Server;
                    client.EnableSsl = true;
                    client.Send(msg);
                }
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw new ApiException("Error sending Email, Ex:" + ex.GetBaseException().ToString());
            }
            return new Response<string>("Email Sent");
        }

       
        
    }
}
