using Persistance;

namespace Application.Services.Interfaces.IRepository.Admin
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAllInfoAboutUsersAsync();
    }
}
