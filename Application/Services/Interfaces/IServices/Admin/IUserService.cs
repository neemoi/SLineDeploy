using Application.DtoModels.Response.User.Profile;

namespace Application.Services.Interfaces.IServices.Admin
{
    public interface IUserService
    {
        Task<List<ProfileResposneDto>> GetAllInfoAboutUsersAsync();
    }
}
