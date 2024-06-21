namespace Application.Services.Interfaces.IServices.Admin
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
