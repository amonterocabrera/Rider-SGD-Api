using SGDPEDIDOS.Application.Wrappers;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface IEmailService
    {
       Response<string> Send(string To,
                                 string Subject,
                                 string Body,
                                 bool IsHTMLBody,
                                 string HTMLTittle, string HTMLInfo, string nombreHtml, string boton = null);

    }
}
