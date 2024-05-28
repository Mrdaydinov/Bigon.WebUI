namespace Bigon.WebUI.Helpers.Services
{
    public interface IEmailService
    {
        Task<bool> SendMailAsync(string to, string subject, string body);
    }
}
