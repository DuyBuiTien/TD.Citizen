using TD.CongDan.Application.DTOs.Mail;
using System.Threading.Tasks;

namespace TD.CongDan.Application.Interfaces.Shared
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}